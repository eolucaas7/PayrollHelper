using System;
using System.Windows.Forms;

namespace PayrollHelper
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();

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

        private void buttonDashBoard_Click(object sender, EventArgs e)
        {
            this.Hide();
            DashBoardForm dashboardForm = new DashBoardForm();
            dashboardForm.FormClosed += (s, args) => this.Show();
            dashboardForm.ShowDialog();
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
    }
}
