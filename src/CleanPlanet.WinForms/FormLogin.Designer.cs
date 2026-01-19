using System.Drawing;
using System.Windows.Forms;

namespace CleanPlanet.WinForms
{
    partial class FormLogin
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblServer;
        private Label lblDatabase;
        private TextBox tbServer;
        private TextBox tbDatabase;

        private CheckBox cbTrusted;
        private Label lblUser;
        private Label lblPassword;
        private TextBox tbUser;
        private TextBox tbPassword;

        private Button btnConnect;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            lblServer = new Label();
            lblDatabase = new Label();
            tbServer = new TextBox();
            tbDatabase = new TextBox();

            cbTrusted = new CheckBox();
            lblUser = new Label();
            lblPassword = new Label();
            tbUser = new TextBox();
            tbPassword = new TextBox();

            btnConnect = new Button();
            btnCancel = new Button();

            SuspendLayout();

            // --- FormLogin ---
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(560, 280);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Подключение к базе данных";
            Padding = new Padding(16);

            // --- Labels ---
            lblServer.AutoSize = true;
            lblServer.Text = "Сервер:";
            lblServer.Left = 20;
            lblServer.Top = 24;

            lblDatabase.AutoSize = true;
            lblDatabase.Text = "База данных:";
            lblDatabase.Left = 20;
            lblDatabase.Top = 64;

            // --- TextBoxes (server, database) ---
            tbServer.Name = "tbServer";
            tbServer.Left = 160;
            tbServer.Top = 20;
            tbServer.Width = 360;
            tbServer.PlaceholderText = @"DESKTOP-XXX\SQLEXPRESS";

            tbDatabase.Name = "tbDatabase";
            tbDatabase.Left = 160;
            tbDatabase.Top = 60;
            tbDatabase.Width = 360;
            tbDatabase.PlaceholderText = "CleanPlanet";

            // --- Trusted connection ---
            cbTrusted.Name = "cbTrusted";
            cbTrusted.Left = 160;
            cbTrusted.Top = 100;
            cbTrusted.Width = 360;
            cbTrusted.Text = "Использовать Windows-аутентификацию (Trusted_Connection)";
            cbTrusted.Checked = true; // по умолчанию используем Trusted_Connection

            // --- User/Password labels ---
            lblUser.AutoSize = true;
            lblUser.Text = "Пользователь:";
            lblUser.Left = 20;
            lblUser.Top = 140;

            lblPassword.AutoSize = true;
            lblPassword.Text = "Пароль:";
            lblPassword.Left = 20;
            lblPassword.Top = 176;

            // --- User/Password textboxes ---
            tbUser.Name = "tbUser";
            tbUser.Left = 160;
            tbUser.Top = 136;
            tbUser.Width = 240;
            tbUser.PlaceholderText = "sa / username";
            tbUser.Enabled = false; // включим в коде при снятии галки Trusted

            tbPassword.Name = "tbPassword";
            tbPassword.Left = 160;
            tbPassword.Top = 172;
            tbPassword.Width = 240;
            tbPassword.PasswordChar = '●';
            tbPassword.Enabled = false;

            // --- Buttons ---
            btnConnect.Name = "btnConnect";
            btnConnect.Text = "Подключиться";
            btnConnect.AutoSize = true;
            btnConnect.Left = 160;
            btnConnect.Top = 220;

            btnCancel.Name = "btnCancel";
            btnCancel.Text = "Отмена";
            btnCancel.AutoSize = true;
            btnCancel.Left = btnConnect.Left + 150;
            btnCancel.Top = 220;
            btnCancel.DialogResult = DialogResult.Cancel;

            // --- Accept/Cancel ---
            AcceptButton = btnConnect;
            CancelButton = btnCancel;

            // --- Add controls ---
            Controls.Add(lblServer);
            Controls.Add(tbServer);
            Controls.Add(lblDatabase);
            Controls.Add(tbDatabase);

            Controls.Add(cbTrusted);
            Controls.Add(lblUser);
            Controls.Add(tbUser);
            Controls.Add(lblPassword);
            Controls.Add(tbPassword);

            Controls.Add(btnConnect);
            Controls.Add(btnCancel);

            ResumeLayout(false);
        }
    }
}
