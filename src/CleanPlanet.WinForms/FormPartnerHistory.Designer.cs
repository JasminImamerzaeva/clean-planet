using System.Drawing;
using System.Windows.Forms;

namespace CleanPlanet.WinForms
{
    partial class FormPartnerHistory
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelTop;
        private Label lblPartnerCaption;
        private Label lblPartner;
        private Label lblFrom;
        private DateTimePicker dtFrom;
        private Label lblTo;
        private DateTimePicker dtTo;
        private Button btnApply;
        private Button btnClose;

        private DataGridView dgvHistory;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            panelTop = new Panel();
            lblPartnerCaption = new Label();
            lblPartner = new Label();
            lblFrom = new Label();
            dtFrom = new DateTimePicker();
            lblTo = new Label();
            dtTo = new DateTimePicker();
            btnApply = new Button();
            btnClose = new Button();

            dgvHistory = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)(dgvHistory)).BeginInit();
            SuspendLayout();

            // --- Form ---
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 560);
            Name = "FormPartnerHistory";
            Text = "История услуг партнёра";
            StartPosition = FormStartPosition.CenterParent;

            // --- panelTop ---
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 56;
            panelTop.Padding = new Padding(10, 8, 10, 8);

            // --- lblPartnerCaption ---
            lblPartnerCaption.AutoSize = true;
            lblPartnerCaption.Text = "Партнёр:";
            lblPartnerCaption.Left = 12;
            lblPartnerCaption.Top = 18;

            // --- lblPartner ---
            lblPartner.AutoSize = true;
            lblPartner.Text = "-";
            lblPartner.Left = lblPartnerCaption.Left + 70;
            lblPartner.Top = 18;
            lblPartner.Font = new Font("Segoe UI", 9F, FontStyle.Bold);

            // --- lblFrom ---
            lblFrom.AutoSize = true;
            lblFrom.Text = "С:";
            lblFrom.Left = 380;
            lblFrom.Top = 18;

            // --- dtFrom ---
            dtFrom.Name = "dtFrom";
            dtFrom.Format = DateTimePickerFormat.Short;
            dtFrom.Left = lblFrom.Left + 20;
            dtFrom.Top = 14;
            dtFrom.Width = 110;

            // --- lblTo ---
            lblTo.AutoSize = true;
            lblTo.Text = "По:";
            lblTo.Left = dtFrom.Left + dtFrom.Width + 15;
            lblTo.Top = 18;

            // --- dtTo ---
            dtTo.Name = "dtTo";
            dtTo.Format = DateTimePickerFormat.Short;
            dtTo.Left = lblTo.Left + 25;
            dtTo.Top = 14;
            dtTo.Width = 110;

            // --- btnApply ---
            btnApply.Name = "btnApply";
            btnApply.Text = "Применить";
            btnApply.AutoSize = true;
            btnApply.Left = dtTo.Left + dtTo.Width + 20;
            btnApply.Top = 12;

            // --- btnClose ---
            btnClose.Name = "btnClose";
            btnClose.Text = "Закрыть";
            btnClose.AutoSize = true;
            btnClose.Left = btnApply.Left + 110;
            btnClose.Top = 12;

            // add to panelTop
            panelTop.Controls.Add(lblPartnerCaption);
            panelTop.Controls.Add(lblPartner);
            panelTop.Controls.Add(lblFrom);
            panelTop.Controls.Add(dtFrom);
            panelTop.Controls.Add(lblTo);
            panelTop.Controls.Add(dtTo);
            panelTop.Controls.Add(btnApply);
            panelTop.Controls.Add(btnClose);

            // --- dgvHistory ---
            dgvHistory.Dock = DockStyle.Fill;
            dgvHistory.ReadOnly = true;
            dgvHistory.AllowUserToAddRows = false;
            dgvHistory.AllowUserToDeleteRows = false;
            dgvHistory.AllowUserToResizeRows = false;
            dgvHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistory.MultiSelect = false;
            dgvHistory.RowHeadersVisible = false;
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistory.Name = "dgvHistory";

            // --- add controls ---
            Controls.Add(dgvHistory);
            Controls.Add(panelTop);

            ((System.ComponentModel.ISupportInitialize)(dgvHistory)).EndInit();
            ResumeLayout(false);
        }
    }
}
