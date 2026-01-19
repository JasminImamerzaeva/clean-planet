using System;
using System.Linq;
using System.Windows.Forms;
using CleanPlanet.WinForms.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;


namespace CleanPlanet.WinForms
{
    public partial class FormMaterialCalc : Form
    {
        public FormMaterialCalc()
        {
            InitializeComponent();

            Load += (_, __) => LoadServices();
            btnCalc.Click += (_, __) => Calculate();
            btnClose.Click += (_, __) => Close();
        }

        private sealed class ComboItem
        {
            public int Id { get; }
            public string Text { get; }
            public ComboItem(int id, string text) { Id = id; Text = text; }
            public override string ToString() => Text;
        }

        private void LoadServices()
        {
            try
            {
                using var db = new CleanPlanetContext();
                var services = db.services
                                 .OrderBy(s => s.name)
                                 .Select(s => new { s.id, s.name })
                                 .ToList();

                cbService.Items.Clear();
                foreach (var s in services)
                    cbService.Items.Add(new ComboItem(s.id, s.name ?? $"Услуга #{s.id}"));

                if (cbService.Items.Count > 0)
                    cbService.SelectedIndex = 0;

                dgvResult.DataSource = null;
                lblHint.Text = "Подсказка: расчёт учитывает нормы на услугу и процент перерасхода по типу материала.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Ошибка загрузки услуг:\r\n" + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Calculate()
        {
            if (cbService.SelectedItem is not ComboItem item)
            {
                MessageBox.Show(this, "Выберите услугу.", "Проверка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbService.Focus();
                return;
            }

            var qty = (int)nudQty.Value;
            if (qty <= 0)
            {
                MessageBox.Show(this, "Количество должно быть > 0.", "Проверка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudQty.Focus();
                return;
            }

            try
            {
                using var db = new CleanPlanetContext();

                var rows = (from n in db.service_material_norms
                            join mt in db.material_types on n.material_type_id equals mt.id
                            join mp in db.material_prices on mt.id equals mp.material_type_id into mpj
                            from mp in mpj.DefaultIfEmpty()
                            where n.service_id == item.Id
                            select new
                            {
                                material_name = mt.name,
                                qty_per_unit = n.qty_per_unit,             
                                overuse_percent = mt.overuse_percent,        
                                price_per_unit = (decimal?)mp.price_per_unit 
                            })
                           .ToList();

                if (rows.Count == 0)
                {
                    dgvResult.DataSource = null;
                    lblHint.Text = "Для выбранной услуги не заданы нормы материалов.";
                    return;
                }


                var result = rows.Select(r =>
                {
                    decimal baseQty = Math.Round(r.qty_per_unit * qty, 3);
                    decimal coef = 1m + (r.overuse_percent / 100m);
                    decimal requiredQty = Math.Round(r.qty_per_unit * qty * coef, 3);

                    decimal? unitPrice = r.price_per_unit; 
                    decimal? cost = unitPrice.HasValue
                                          ? Math.Round(requiredQty * unitPrice.Value, 2)
                                          : (decimal?)null;

                    return new
                    {
                        Материал = r.material_name,
                        Норма_на_1 = r.qty_per_unit,
                        База = baseQty,
                        Перерасход_pct = r.overuse_percent,
                        Требуется = requiredQty,
                        Цена_за_ед = unitPrice,
                        Стоимость = cost
                    };
                })
                .ToList();

                dgvResult.DataSource = result;

                decimal total = result.Where(x => x.Стоимость.HasValue)
                                      .Sum(x => x.Стоимость!.Value);

                decimal? serviceCost = null;
                using (var db2 = new CleanPlanetContext())
                {
                    var conn = db2.Database.GetDbConnection();
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();

                    using var cmd = conn.CreateCommand();
                    cmd.CommandText = "SELECT CONVERT(decimal(18,2), cp.fn_calc_service_cost(@sid))";
                    var p = cmd.CreateParameter();
                    p.ParameterName = "@sid";
                    p.Value = item.Id;
                    cmd.Parameters.Add(p);

                    var scalar = cmd.ExecuteScalar();
                    if (scalar != null && scalar != DBNull.Value)
                        serviceCost = Convert.ToDecimal(scalar);
                }

                lblHint.Text = $"Итоговая стоимость материалов: {total:0.00} RUB.  " +
                               $"Себестоимость услуги: {(serviceCost.HasValue ? serviceCost.Value.ToString("0.00") + " RUB" : "н/д")}." +
                               $"  Подсказка: расчёт = Норма*Кол-во*(1+перерасход%).";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Ошибка расчёта:\r\n" + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
