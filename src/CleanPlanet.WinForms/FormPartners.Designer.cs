using System.Windows.Forms;

namespace CleanPlanet.WinForms
{
    partial class FormPartners
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelTop;
        private TextBox tbSearch;
        private Button btnSearch;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnRefresh;
        private DataGridView dgvPartners;
        private Button btnHistory;
        private Button btnMaterials;


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
            tbSearch = new TextBox();
            btnSearch = new Button();
            btnAdd = new Button();
            btnEdit = new Button();
            btnRefresh = new Button();
            dgvPartners = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)(dgvPartners)).BeginInit();
            SuspendLayout();

            // panelTop
            panelTop.Name = "panelTop";
            panelTop.Dock = DockStyle.Top;
            panelTop.Height = 44;
            panelTop.Padding = new Padding(10, 8, 10, 8);

            // tbSearch
            tbSearch.Name = "tbSearch";
            tbSearch.PlaceholderText = "Поиск (наименование, email, телефон)…";
            tbSearch.Width = 320;
            tbSearch.Left = 10;
            tbSearch.Top = 8;

            // btnSearch
            btnSearch.Name = "btnSearch";
            btnSearch.Text = "Найти";
            btnSearch.AutoSize = true;
            btnSearch.Left = tbSearch.Left + tbSearch.Width + 10;
            btnSearch.Top = 7;

            // btnRefresh
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Text = "Обновить";
            btnRefresh.AutoSize = true;
            btnRefresh.Left = btnSearch.Left + 90;
            btnRefresh.Top = 7;

            // btnAdd
            btnAdd.Name = "btnAdd";
            btnAdd.Text = "Добавить";
            btnAdd.AutoSize = true;
            btnAdd.Left = btnRefresh.Left + 100;
            btnAdd.Top = 7;

            // btnEdit
            btnEdit.Name = "btnEdit";
            btnEdit.Text = "Редактировать";
            btnEdit.AutoSize = true;
            btnEdit.Left = btnAdd.Left + 110;
            btnEdit.Top = 7;

            // btnHistory
            btnHistory = new Button();
            btnHistory.Name = "btnHistory";
            btnHistory.Text = "История…";
            btnHistory.AutoSize = true;
            btnHistory.Left = btnEdit.Left + 130; // рядом после "Редактировать"
            btnHistory.Top = 7;

            // btnMaterials
            btnMaterials = new Button();
            btnMaterials.Name = "btnMaterials";
            btnMaterials.Text = "Материалы…";
            btnMaterials.AutoSize = true;
            btnMaterials.Left = btnHistory.Left + 120; // после кнопки "История…"
            btnMaterials.Top = 7;

            // add controls to panelTop
            panelTop.Controls.Add(tbSearch);
            panelTop.Controls.Add(btnSearch);
            panelTop.Controls.Add(btnRefresh);
            panelTop.Controls.Add(btnAdd);
            panelTop.Controls.Add(btnEdit);
            panelTop.Controls.Add(btnHistory);
            panelTop.Controls.Add(btnMaterials);

            // dgvPartners
            dgvPartners.Name = "dgvPartners";
            dgvPartners.Dock = DockStyle.Fill;
            dgvPartners.ReadOnly = true;
            dgvPartners.AllowUserToAddRows = false;
            dgvPartners.AllowUserToDeleteRows = false;
            dgvPartners.AllowUserToResizeRows = false;
            dgvPartners.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPartners.MultiSelect = false;
            dgvPartners.RowHeadersVisible = false;
            dgvPartners.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // FormPartners
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1024, 600);
            Controls.Add(dgvPartners);
            Controls.Add(panelTop);
            Name = "FormPartners";
            Text = "Партнёры";
            StartPosition = FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)(dgvPartners)).EndInit();
            ResumeLayout(false);
        }
    }
}
