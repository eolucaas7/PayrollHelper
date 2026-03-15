using Npgsql;
using System;
using PayrollHelper.Properties;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayrollHelper
{
    public partial class ReportsForm : Form
    {
        private List<(string EmployeeName, string Position, double PaymentAmount, string PaymentType)> employees = new List<(string, string, double, string)>();
        private static string filter = "";
        public ReportsForm()
        {
            InitializeComponent();
            string savedPath = Properties.Settings.Default.ReportFolderPath;
            lblCurrentPath.Text = !string.IsNullOrEmpty(savedPath)
                ? $"Текущий путь к отчетам: {savedPath}"
                : "Папка для хранения отчетов не выбрана";
        }

        private void generateAndExportButton_Click(object sender, EventArgs e)
        {
            if (reportTypeComboBox.SelectedIndex == 0) filter = "зарплата";
            else if (reportTypeComboBox.SelectedIndex == 1) filter = "премия";
            else MessageBox.Show("Не выбран тип выплаты!");

            GetEmployeeData();

            string reportType = reportTypeComboBox.SelectedItem.ToString().ToLower();
            string reportText = GetReportText(reportType);
            string reportFolderPath = Properties.Settings.Default.ReportFolderPath;

            SaveReport(reportText, reportFolderPath, reportType);
        }

        private void btnSelectReportFolder_Click(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = "Выберите папку для хранения отчетов";

                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        Properties.Settings.Default.ReportFolderPath = folderDialog.SelectedPath;
                        Properties.Settings.Default.Save();
                        lblCurrentPath.Text = $"Текущий путь к отчетам: {folderDialog.SelectedPath}";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при выборе папки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveReport(string reportText, string folderPath, string reportType)
        {
            string fileName = $"{reportType}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.txt";

            try
            {
                File.WriteAllText(Path.Combine(folderPath, fileName), reportText);
                MessageBox.Show("Отчет успешно создан.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании отчета: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetEmployeeData()
        {
            employees.Clear();
            using (var conn = new NpgsqlConnection(LoadDB.Connection_String))
            {
                try
                {
                    conn.Open();
                    string query = "";
                    if (includeBonusesCheckBox.Checked)
                    {
                        query = $@"
    SELECT e.employee_name, p.name AS position, pay.payment_amount, pay.payment_type  
    FROM employees e
    JOIN positions p ON e.post_number = p.id
    JOIN payments pay ON e.employee_id = pay.employee_id
    WHERE pay.payment_type ILIKE '%{filter}%' OR pay.payment_type ILIKE '%сумма'
";
                    }
                    else
                    {
                        query = $@"
    SELECT e.employee_name, p.name AS position, pay.payment_amount, pay.payment_type  
    FROM employees e
    JOIN positions p ON e.post_number = p.id
    JOIN payments pay ON e.employee_id = pay.employee_id
    WHERE pay.payment_type ILIKE '%{filter}%'
";

                    }


                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string employeeName = reader.GetString(0);
                                string position = reader.GetString(1);
                                double paymentAmount = reader.GetDouble(2);
                                string paymentType = reader.GetString(3);

                                employees.Add((employeeName, position, paymentAmount, paymentType));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при получении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string GetReportText(string reportType)
        {
            StringBuilder reportText = new StringBuilder();

            switch (reportType)
            {
                case "отчет по зарплате":
                    reportText.AppendLine("Отчет по заработной плате сотрудников");
                    reportText.AppendLine("------------------------------------------------------------");
                    break;

                case "отчет по премии":
                    reportText.AppendLine("Отчет по премиям сотрудников");
                    reportText.AppendLine("------------------------------------------------------------");
                    break;
            }

            reportText.AppendLine("Данные по сотрудникам:");
            reportText.AppendLine("------------------------------------------------------------");

            foreach (var employee in employees)
            {
                reportText.AppendLine($"ФИО: {employee.EmployeeName}, Должность: {employee.Position}, Сумма выплаты: {employee.PaymentAmount:F2} рублей ({employee.PaymentType})");
            }

            return reportText.ToString();
        }

    }
}
