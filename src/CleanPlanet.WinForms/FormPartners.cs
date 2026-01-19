using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CleanPlanet.WinForms.Data; 


namespace CleanPlanet.WinForms
{
    public partial class FormPartners : Form
    {
        public FormPartners()
        {
            InitializeComponent();

            Load += FormPartners_Load;
            btnMaterials.Click += (_, __) => OpenMaterialCalc();
            btnSearch.Click += (_, __) => ApplyFilter();
            btnRefresh.Click += (_, __) => { tbSearch.Text = ""; LoadPartners(); };
            btnAdd.Click += (_, __) => AddPartner();
            btnHistory.Click += (_, __) => OpenPartnerHistory();
            btnEdit.Click += (_, __) => EditSelectedPartner();
            dgvPartners.CellDoubleClick += (_, __) => EditSelectedPartner();
            tbSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) { ApplyFilter(); e.Handled = true; e.SuppressKeyPress = true; } };
        }


        private void FormPartners_Load(object? sender, EventArgs e)
        {
            LoadPartners();
        }

        private void LoadPartners(string? term = null)
        {
            try
            {
                using var db = new CleanPlanetContext();

                var query =
                    from p in db.partners
                    join pt in db.partner_types on p.partner_type_id equals pt.id
                    orderby p.id
                    select new
                    {
                        p.id,
                        Тип = pt.name,
                        Наименование = p.name,
                        Руководитель = p.head,
                        Email = p.email,
                        Телефон = p.phone,
                        Адрес = p.legal_address,
                        ИНН = p.inn,
                        Рейтинг = p.rating
                    };

                if (!string.IsNullOrWhiteSpace(term))
                {
                    var t = term.Trim();
                    query = query.Where(x =>
                        (x.Наименование != null && x.Наименование.Contains(t)) ||
                        (x.Email != null && x.Email.Contains(t)) ||
                        (x.Телефон != null && x.Телефон.Contains(t)));
                }

                var rows = query.ToList();
                dgvPartners.DataSource = rows;

                if (dgvPartners.Rows.Count > 0)
                {
                    dgvPartners.ClearSelection();
                    dgvPartners.Rows[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Ошибка загрузки партнёров:\r\n" + ex.Message,
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyFilter()
        {
            LoadPartners(tbSearch.Text);
        }


        private void AddPartner()
        {
            using var db = new CleanPlanetContext();
            var partnerTypes = db.partner_types.OrderBy(x => x.name).ToList();
            if (partnerTypes.Count == 0)
            {
                MessageBox.Show(this, "Не найдены типы партнёров.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var dlg = new PartnerEditForm(partnerTypes);
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    var p = new partners
                    {
                        partner_type_id = dlg.SelectedPartnerTypeId,
                        name = dlg.NameValue,
                        head = dlg.HeadValue,
                        email = dlg.EmailValue,
                        phone = dlg.PhoneValue,
                        legal_address = dlg.AddressValue,
                        inn = dlg.INNValue,
                        rating = dlg.RatingValue
                    };

                    db.partners.Add(p);
                    db.SaveChanges();

                    LoadPartners(tbSearch.Text);
                    SelectRowById(p.id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Ошибка добавления:\r\n" + ex.Message,
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void EditSelectedPartner()
        {
            if (dgvPartners.CurrentRow == null)
            {
                MessageBox.Show(this, "Выберите партнёра в списке.", "Подсказка",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var idObj = dgvPartners.CurrentRow.Cells["id"]?.Value; 
            if (idObj == null || !int.TryParse(idObj.ToString(), out int partnerId))
            {
                MessageBox.Show(this, "Не удалось определить идентификатор партнёра.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using var db = new CleanPlanetContext();
            var partner = db.partners.FirstOrDefault(x => x.id == partnerId);
            if (partner == null)
            {
                MessageBox.Show(this, "Партнёр не найден.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var partnerTypes = db.partner_types.OrderBy(x => x.name).ToList();
            var dlg = new PartnerEditForm(partnerTypes,
                                          partner.partner_type_id,
                                          partner.name ?? "",
                                          partner.head ?? "",
                                          partner.email ?? "",
                                          partner.phone ?? "",
                                          partner.legal_address ?? "",
                                          partner.inn ?? "",
                                          partner.rating);

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    partner.partner_type_id = dlg.SelectedPartnerTypeId;
                    partner.name = dlg.NameValue;
                    partner.head = dlg.HeadValue;
                    partner.email = dlg.EmailValue;
                    partner.phone = dlg.PhoneValue;
                    partner.legal_address = dlg.AddressValue;
                    partner.inn = dlg.INNValue;
                    partner.rating = dlg.RatingValue;

                    db.SaveChanges();
                    LoadPartners(tbSearch.Text);
                    SelectRowById(partner.id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Ошибка сохранения:\r\n" + ex.Message,
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SelectRowById(int id)
        {
            foreach (DataGridViewRow row in dgvPartners.Rows)
            {
                var val = row.Cells["id"]?.Value;
                if (val != null && int.TryParse(val.ToString(), out int rowId) && rowId == id)
                {
                    dgvPartners.ClearSelection();
                    row.Selected = true;
                    dgvPartners.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }


        private sealed class PartnerEditForm : Form
        {
            public int SelectedPartnerTypeId => ((ComboItem)cmbType.SelectedItem!).Id;
            public string NameValue => tbName.Text.Trim();
            public string HeadValue => tbHead.Text.Trim();
            public string EmailValue => tbEmail.Text.Trim();
            public string PhoneValue => tbPhone.Text.Trim();
            public string AddressValue => tbAddress.Text.Trim();
            public string INNValue => tbINN.Text.Trim();
            public byte? RatingValue => (byte?)nudRating.Value;

            private ComboBox cmbType;
            private TextBox tbName, tbHead, tbEmail, tbPhone, tbAddress, tbINN;
            private NumericUpDown nudRating;
            private Button btnOk, btnCancel;
            private Label lblType, lblName, lblHead, lblEmail, lblPhone, lblAddress, lblINN, lblRating;

            public PartnerEditForm(System.Collections.Generic.IEnumerable<partner_types> types,
                                   int? typeId = null,
                                   string name = "", string head = "", string email = "",
                                   string phone = "", string address = "", string inn = "", byte? rating = null)
            {
                Text = string.IsNullOrEmpty(name) ? "Добавить партнёра" : "Редактировать партнёра";
                StartPosition = FormStartPosition.CenterParent;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                MaximizeBox = false; MinimizeBox = false;
                ClientSize = new System.Drawing.Size(640, 360);
                Padding = new Padding(12);

                // controls
                lblType = new Label() { Text = "Тип партнёра:", Left = 12, Top = 16, Width = 140 };
                cmbType = new ComboBox() { Left = 160, Top = 12, Width = 450, DropDownStyle = ComboBoxStyle.DropDownList };

                lblName = new Label() { Text = "Наименование*:", Left = 12, Top = 52, Width = 140 };
                tbName = new TextBox() { Left = 160, Top = 48, Width = 450, Text = name };

                lblHead = new Label() { Text = "Руководитель:", Left = 12, Top = 84, Width = 140 };
                tbHead = new TextBox() { Left = 160, Top = 80, Width = 450, Text = head };

                lblEmail = new Label() { Text = "Email:", Left = 12, Top = 116, Width = 140 };
                tbEmail = new TextBox() { Left = 160, Top = 112, Width = 450, Text = email };

                lblPhone = new Label() { Text = "Телефон:", Left = 12, Top = 148, Width = 140 };
                tbPhone = new TextBox() { Left = 160, Top = 144, Width = 450, Text = phone };

                lblAddress = new Label() { Text = "Юр. адрес:", Left = 12, Top = 180, Width = 140 };
                tbAddress = new TextBox() { Left = 160, Top = 176, Width = 450, Text = address };

                lblINN = new Label() { Text = "ИНН (10/12 цифр):", Left = 12, Top = 212, Width = 140 };
                tbINN = new TextBox() { Left = 160, Top = 208, Width = 450, Text = inn };

                lblRating = new Label() { Text = "Рейтинг (0..10):", Left = 12, Top = 244, Width = 140 };
                nudRating = new NumericUpDown() { Left = 160, Top = 240, Width = 100, Minimum = 0, Maximum = 10, Value = rating ?? 0 };

                btnOk = new Button() { Text = "Сохранить", Left = 388, Width = 100, Top = 300, DialogResult = DialogResult.None };
                btnCancel = new Button() { Text = "Отмена", Left = 510, Width = 100, Top = 300, DialogResult = DialogResult.Cancel };

                Controls.AddRange(new Control[] {
                    lblType, cmbType, lblName, tbName, lblHead, tbHead, lblEmail, tbEmail,
                    lblPhone, tbPhone, lblAddress, tbAddress, lblINN, tbINN, lblRating, nudRating,
                    btnOk, btnCancel
                });

                // заполнение типов
                foreach (var t in types)
                    cmbType.Items.Add(new ComboItem(t.id, t.name));

                if (cmbType.Items.Count > 0)
                {
                    if (typeId.HasValue)
                    {
                        for (int i = 0; i < cmbType.Items.Count; i++)
                            if (((ComboItem)cmbType.Items[i]).Id == typeId.Value) { cmbType.SelectedIndex = i; break; }
                    }
                    if (cmbType.SelectedIndex < 0) cmbType.SelectedIndex = 0;
                }

                btnOk.Click += (_, __) =>
                {
                    if (!ValidateInputs()) return;
                    DialogResult = DialogResult.OK;
                    Close();
                };
                btnCancel.Click += (_, __) => { DialogResult = DialogResult.Cancel; Close(); };
            }

            private bool ValidateInputs()
            {
                if (string.IsNullOrWhiteSpace(tbName.Text))
                {
                    MessageBox.Show(this, "Наименование обязательно.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbName.Focus();
                    return false;
                }

                if (!string.IsNullOrWhiteSpace(tbEmail.Text))
                {
                    var ok = Regex.IsMatch(tbEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                    if (!ok)
                    {
                        MessageBox.Show(this, "Некорректный email.", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbEmail.Focus();
                        return false;
                    }
                }

 
                if (!string.IsNullOrWhiteSpace(tbINN.Text))
                {
                    var s = tbINN.Text.Trim();
                    if (!(Regex.IsMatch(s, @"^\d{10}$") || Regex.IsMatch(s, @"^\d{12}$")))
                    {
                        MessageBox.Show(this, "ИНН должен содержать 10 или 12 цифр.", "Проверка",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbINN.Focus();
                        return false;
                    }
                }

                return true;
            }

            private sealed class ComboItem
            {
                public int Id { get; }
                public string Text { get; }
                public ComboItem(int id, string text) { Id = id; Text = text; }
                public override string ToString() => Text;
            }
        }
        private void OpenPartnerHistory()
        {
            if (dgvPartners.CurrentRow == null)
            {
                MessageBox.Show(this, "Выберите партнёра в списке.", "Подсказка",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var idObj = dgvPartners.CurrentRow.Cells["id"]?.Value;
            var nameObj = dgvPartners.CurrentRow.Cells["Наименование"]?.Value;

            if (idObj == null || !int.TryParse(idObj.ToString(), out int partnerId))
            {
                MessageBox.Show(this, "Не удалось определить идентификатор партнёра.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var partnerName = nameObj?.ToString() ?? $"Партнёр #{partnerId}";

            using var dlg = new FormPartnerHistory(partnerId, partnerName);
            dlg.ShowDialog(this);
        }

        private void OpenMaterialCalc()
        {
            using var dlg = new FormMaterialCalc();
            dlg.ShowDialog(this);
        }


    }
}
