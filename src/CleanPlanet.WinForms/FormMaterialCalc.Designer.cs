using System.Drawing;
using System.Windows.Forms;

namespace CleanPlanet.WinForms
{
    partial class FormMaterialCalc
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelTop;
        private Label lblService;
        private ComboBox cbService;
        private Label lblQty;
        private NumericUpDown nudQty;
        private Button btnCalc;
        private Button btnClose;

        private DataGridView dgvResult;
        private Label lblHint;

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
            lblService = new Label();
            cbService = new ComboBox();
            lblQty = new Label();
            nudQty = new NumericUpDown();
            btnCalc = new Button();
            btnClose = new Button();

            dgvResult = new DataGridView();
            lblHint = new Label();

            ((System.ComponentModel.ISupportInitialize)(nudQty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(dgvResult)).BeginInit();
            SuspendLayout();

            // --- Form ---
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(900, 540);
            Name = "FormMaterialCalc";
            Text = "Расчёт требуемых материалов";
            StartPosition = FormStartPosition.CenterParent;

            // --- panelTop ---
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 64;
            panelTop.Padding = new Padding(10, 10, 10, 10);

            // --- lblService ---
            lblService.AutoSize = true;
            lblService.Text = "Услуга:";
            lblService.Left = 12;
            lblService.Top = 18;

            // --- cbService ---
            cbService.Name = "cbService";
            cbService.Left = lblService.Left + 60;
            cbService.Top = 14;
            cbService.Width = 460;
            cbService.DropDownStyle = ComboBoxStyle.DropDownList;

            // --- lblQty ---
            lblQty.AutoSize = true;
            lblQty.Text = "Количество:";
            lblQty.Left = cbService.Left + cbService.Width + 16;
            lblQty.Top = 18;

            // --- nudQty ---
            nudQty.Name = "nudQty";
            nudQty.Left = lblQty.Left + 90;
            nudQty.Top = 14;
            nudQty.Width = 80;
            nudQty.Minimum = 1;
            nudQty.Maximum = 100000;
            nudQty.Value = 1;

            // --- btnCalc ---
            btnCalc.Name = "btnCalc";
            btnCalc.Text = "Рассчитать";
            btnCalc.AutoSize = true;
            btnCalc.Left = nudQty.Left + nudQty.Width + 16;
            btnCalc.Top = 12;

            // --- btnClose ---
            btnClose.Name = "btnClose";
            btnClose.Text = "Закрыть";
            btnClose.AutoSize = true;
            btnClose.Left = btnCalc.Left + 110;
            btnClose.Top = 12;

            // add to panelTop
            panelTop.Controls.Add(lblService);
            panelTop.Controls.Add(cbService);
            panelTop.Controls.Add(lblQty);
            panelTop.Controls.Add(nudQty);
            panelTop.Controls.Add(btnCalc);
            panelTop.Controls.Add(btnClose);

            // --- dgvResult ---
            dgvResult.Name = "dgvResult";
            dgvResult.Dock = DockStyle.Fill;
            dgvResult.ReadOnly = true;
            dgvResult.AllowUserToAddRows = false;
            dgvResult.AllowUserToDeleteRows = false;
            dgvResult.AllowUserToResizeRows = false;
            dgvResult.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResult.MultiSelect = false;
            dgvResult.RowHeadersVisible = false;
            dgvResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // --- lblHint ---
            lblHint.Dock = DockStyle.Bottom;
            lblHint.Height = 28;
            lblHint.TextAlign = ContentAlignment.MiddleLeft;
            lblHint.Text = "Подсказка: расчёт учитывает нормы на услугу и процент перерасхода по типу материала.";

            // --- add controls ---
            Controls.Add(dgvResult);
            Controls.Add(lblHint);
            Controls.Add(panelTop);

            ((System.ComponentModel.ISupportInitialize)(nudQty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(dgvResult)).EndInit();
            ResumeLayout(false);
        }
    }
}
