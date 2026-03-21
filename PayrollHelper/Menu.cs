using System;
using System.Windows.Forms;

namespace PayrollHelper
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += Menu_KeyDown;
            this.MouseClick += Form_MouseClick;

            if (LoginForm.admin == true)
            {
                buttonEditDatabase.Enabled = true;
                buttonEditDatabase.Visible = true;
            }
            else
            {
                buttonEditDatabase.Enabled = false;
                buttonEditDatabase.Visible = false;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        /*
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            // Если мы не скрыли форму для возврата к логину, выходим из приложения
            if (this.Visible)
            {
                Application.Exit();
            }
        }
        */

        private void Form_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.ActiveControl = null;
            }
        }

        private void Menu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void buttonDashBoard_Click(object sender, EventArgs e)
        {
            this.Hide();
            DashBoardForm dashboardForm = new DashBoardForm();
            dashboardForm.FormClosed += (s, args) => this.Show();
            dashboardForm.ShowDialog();
        }

        private void buttonManageTaxes_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageTaxationForm taxationForm = new ManageTaxationForm();
            taxationForm.FormClosed += (s, args) => this.Show();
            taxationForm.ShowDialog();
        }

        private void buttonManagePayments_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManagePaymentsForm paymentForm = new ManagePaymentsForm();
            paymentForm.FormClosed += (s, args) => this.Show();
            paymentForm.ShowDialog();
        }

        private void buttonEditDatabase_Click(object sender, EventArgs e)
        {
            this.Hide();
            EditDatasBaseForm editDatabaseForm = new EditDatasBaseForm();
            editDatabaseForm.FormClosed += (s, args) => this.Show();
            editDatabaseForm.ShowDialog();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReportsForm reportsForm = new ReportsForm();
            reportsForm.FormClosed += (s, args) => this.Show();
            reportsForm.ShowDialog();
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}