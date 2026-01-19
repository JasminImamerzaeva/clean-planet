using System;
using System.Windows.Forms;
using CleanPlanet.WinForms.Data; 

namespace CleanPlanet.WinForms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();

            tbServer.Text = @"LAPTOP-CB1HSA8I";
            tbDatabase.Text = @"CleanPlanet";

            // События
            cbTrusted.CheckedChanged += (_, __) =>
            {
                var useTrusted = cbTrusted.Checked;
                tbUser.Enabled = !useTrusted;
                tbPassword.Enabled = !useTrusted;
                if (useTrusted)
                {
                    tbUser.Clear();
                    tbPassword.Clear();
                }
            };

            btnConnect.Click += (_, __) => ConnectAndClose();
        }

        private void ConnectAndClose()
        {
            var server = tbServer.Text.Trim();
            var db = tbDatabase.Text.Trim();

            if (string.IsNullOrWhiteSpace(server))
            {
                MessageBox.Show(this, "Укажите имя сервера SQL.", "Проверка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbServer.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(db))
            {
                MessageBox.Show(this, "Укажите имя базы данных.", "Проверка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbDatabase.Focus();
                return;
            }

            string conn;
            if (cbTrusted.Checked)
            {
                conn = $"Server={server};Database={db};Trusted_Connection=True;TrustServerCertificate=True";
            }
            else
            {
                var user = tbUser.Text.Trim();
                var pwd = tbPassword.Text; 
                if (string.IsNullOrWhiteSpace(user))
                {
                    MessageBox.Show(this, "Укажите имя пользователя.", "Проверка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbUser.Focus();
                    return;
                }
                conn = $"Server={server};Database={db};User Id={user};Password={pwd};TrustServerCertificate=True";
            }

            try
            {

                AppConfig.ConnectionString = conn;


                using var ctx = new CleanPlanetContext();
                var can = ctx.Database.CanConnect();
                if (!can)
                    throw new Exception("Не удалось подключиться к БД с указанными параметрами.");

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    "Ошибка подключения:\r\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
