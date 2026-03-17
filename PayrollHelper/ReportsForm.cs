using System;
using PayrollHelper.Properties;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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

            // Настройка lblCurrentPath для корректного отображения длинных путей
            lblCurrentPath.AutoSize = false;
            lblCurrentPath.Width = 350;
            lblCurrentPath.TextAlign = ContentAlignment.MiddleLeft;

            reportTypeComboBox.Items.Clear();
            reportTypeComboBox.Items.Add("Отчет по зарплате");
            reportTypeComboBox.Items.Add("Отчет по премии");
            reportTypeComboBox.SelectedIndex = 0;

            // Загрузка и проверка пути
            string savedPath = Settings.Default.ReportFolderPath;
            
            if (string.IsNullOrEmpty(savedPath) || !Directory.Exists(savedPath))
            {
                try
                {
                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string defaultReportsFolder = Path.Combine(desktopPath, "Reports");
                    
                    if (!Directory.Exists(defaultReportsFolder))
                    {
                        Directory.CreateDirectory(defaultReportsFolder);
                    }
                    
                    Settings.Default.ReportFolderPath = defaultReportsFolder;
                    Settings.Default.Save();
                    
                    savedPath = defaultReportsFolder;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось настроить путь по умолчанию: {ex.Message}", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            // Отображаем путь в лейбле
            UpdatePathLabel(savedPath);
        }

        private void UpdatePathLabel(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                lblCurrentPath.Text = path;
                // Подсказка при наведении, если путь очень длинный
                ToolTip toolTip = new ToolTip();
                toolTip.SetToolTip(lblCurrentPath, path);
            }
            else
            {
                lblCurrentPath.Text = "Папка не выбрана";
            }
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
                return;
            }

            string reportType = reportTypeComboBox.SelectedItem.ToString().ToLower();
            string reportText = GetReportText(reportType);
            string reportFolderPath = Settings.Default.ReportFolderPath;

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
                        Settings.Default.ReportFolderPath = folderDialog.SelectedPath;
                        Settings.Default.Save();
                        
                        // Сразу обновляем текст в лейбле на форме
                        UpdatePathLabel(folderDialog.SelectedPath);
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
            try
            {
                if (string.IsNullOrEmpty(folderPath))
                {
                    MessageBox.Show("Путь для сохранения не указан!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = $"{reportType}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string fullPath = Path.Combine(folderPath, fileName);

                File.WriteAllText(fullPath, reportText);
                MessageBox.Show($"Отчет успешно сохранен в:\n{fullPath}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении отчета: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetEmployeeData()
        {
            employees.Clear();
            try
            {
                var query = Program.dbContext.Payments
                    .Include(p => p.Employee)
                        .ThenInclude(e => e.PostNumberNavigation)
                    .AsQueryable();

                string salaryFilter = "\u041e\u043a\u043b\u0430\u0434"; // "Оклад"
                string bonusFilter = "\u041f\u0440\u0435\u043c\u0438\u044f"; // "Премия"
                string sumFilter = "\u0441\u0443\u043c\u043c\u0430"; // "сумма"

                int selectedIndex = reportTypeComboBox.SelectedIndex;

                if (selectedIndex == 0) // Отчет по зарплате
                {
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
