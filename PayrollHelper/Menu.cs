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
            DashBoardForm dashboardForm = new DashBoardForm();
            dashboardForm.ShowDialog();
        }

        private void buttonEditDatabase_Click(object sender, EventArgs e)
        {
            EditDatasBaseForm editDatabaseForm = new EditDatasBaseForm();
            editDatabaseForm.ShowDialog();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            ReportsForm reportsForm = new ReportsForm();
            reportsForm.ShowDialog();
        }
    }
}
