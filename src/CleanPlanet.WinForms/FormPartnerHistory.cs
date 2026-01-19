using System;
using System.Linq;
using System.Windows.Forms;
using CleanPlanet.WinForms.Data; 

namespace CleanPlanet.WinForms
{
    public partial class FormPartnerHistory : Form
    {
        private readonly int _partnerId;
        private readonly string _partnerName;

        public FormPartnerHistory(int partnerId, string partnerName)
        {
            InitializeComponent();

            _partnerId = partnerId;
            _partnerName = partnerName;

            lblPartner.Text = _partnerName;

            dtFrom.Value = new DateTime(2000, 1, 1);
            dtTo.Value = DateTime.Today;


            Load += (_, __) => LoadHistory();
            btnApply.Click += (_, __) => LoadHistory();
            btnClose.Click += (_, __) => Close();
        }

        private void LoadHistory()
        {
            try
            {
                var fromDate = DateOnly.FromDateTime(dtFrom.Value.Date);
                var toDate = DateOnly.FromDateTime(dtTo.Value.Date);

                using var db = new CleanPlanetContext();

                var rows = (from h in db.partner_service_history
                            join s in db.services on h.service_id equals s.id
                            where h.partner_id == _partnerId
                                  && h.performed_at >= fromDate
                                  && h.performed_at <= toDate
                            orderby h.performed_at, h.id
                            select new
                            {
                                Дата = h.performed_at,
                                Услуга = s.name,
                                Количество = h.qty
                            })
                           .ToList();

                dgvHistory.DataSource = rows;

                if (dgvHistory.Rows.Count > 0)
                {
                    dgvHistory.ClearSelection();
                    dgvHistory.Rows[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    "Ошибка загрузки истории:\r\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

    }
}
