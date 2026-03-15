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
using Microsoft.EntityFrameworkCore;

namespace PayrollHelper
{
    public partial class ReportsForm : Form
    {
        private List<(string EmployeeName, string Position, double PaymentAmount, string PaymentType)> employees = new List<(string, string, double, string)>();
        
        public ReportsForm()
        {
            InitializeComponent();

            reportTypeComboBox.Items.Clear();
            reportTypeComboBox.Items.Add("Отчет по зарплате");
            reportTypeComboBox.Items.Add("Отчет по премии");
            reportTypeComboBox.SelectedIndex = 0; // Выбрать первый по умолчанию

            string savedPath = Properties.Settings.Default.ReportFolderPath;
            lblCurrentPath.Text = !string.IsNullOrEmpty(savedPath)
                ? $"Текущий путь к отчетам: {savedPath}"
                : "Папка для хранения отчетов не выбрана";
        }

        private void generateAndExportButton_Click(object sender, EventArgs e)
        {
            if (reportTypeComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Не выбран тип выплаты!");
                return;
            }

            GetEmployeeData();

            if (employees.Count == 0)
            {
                // Сообщение уже выводится внутри GetEmployeeData в случае ошибки фильтрации
                return;
            }

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
            try
            {
                // Начинаем построение запроса (БЕЗ ToList() ДО ФИЛЬТРАЦИИ)
                var query = Program.dbContext.Payments
                    .Include(p => p.Employee)
                        .ThenInclude(e => e.PostNumberNavigation)
                    .AsQueryable();

                // Фильтры в Unicode (чтобы избежать проблем в самом коде)
                string salaryFilter = "\u041e\u043a\u043b\u0430\u0434"; // "Оклад"
                string bonusFilter = "\u041f\u0440\u0435\u043c\u0438\u044f"; // "Премия"
                string sumFilter = "\u0441\u0443\u043c\u043c\u0430"; // "сумма"

                int selectedIndex = reportTypeComboBox.SelectedIndex;

                if (selectedIndex == 0) // Отчет по зарплате
                {
                    // Ищем Оклад (ILike для поиска на стороне БД с учетом регистра PostgreSQL)
                    if (includeBonusesCheckBox.Checked)
                    {
                        query = query.Where(p => EF.Functions.ILike(p.PaymentType, $"%{salaryFilter}%") 
                                              || EF.Functions.ILike(p.PaymentType, $"%{sumFilter}%"));
                    }
                    else
                    {
                        query = query.Where(p => EF.Functions.ILike(p.PaymentType, $"%{salaryFilter}%"));
                    }
                }
                else // Отчет по премии
                {
                    query = query.Where(p => EF.Functions.ILike(p.PaymentType, $"%{bonusFilter}%"));
                }

                // Выполнение запроса и маппинг
                var results = query.Select(p => new
                {
                    EmployeeName = p.Employee != null ? p.Employee.EmployeeName : "Неизвестный сотрудник",
                    Position = (p.Employee != null && p.Employee.PostNumberNavigation != null) ? p.Employee.PostNumberNavigation.Name : "Должность не найдена",
                    Amount = (double)p.PaymentAmount,
                    Type = p.PaymentType
                }).ToList();

                if (results.Count == 0)
                {
                    MessageBox.Show("Записи в БД найдены, но по выбранному фильтру (" + (selectedIndex == 0 ? "Зарплата" : "Премия") + ") ничего нет.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                foreach (var item in results)
                {
                    employees.Add((item.EmployeeName, item.Position, item.Amount, item.Type ?? "Н/Д"));
                }
            }
            catch (Exception ex)
            {
                // Выводим детальную ошибку, если она останется
                string inner = ex.InnerException != null ? $"\nПодробности: {ex.InnerException.Message}" : "";
                MessageBox.Show($"Ошибка при получении данных: {ex.Message}{inner}", "Ошибка EF Core", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
