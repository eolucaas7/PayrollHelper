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

            this.KeyPreview = true;
            this.KeyDown += LoginForm_KeyDown;
            this.MouseClick += Form_MouseClick;

            grpReportParams.MouseClick += Form_MouseClick;
            grpSave.MouseClick += Form_MouseClick;
            clbPaymentTypes.MouseClick += Form_MouseClick;
            if (lblPaymentTypes != null) lblPaymentTypes.MouseClick += Form_MouseClick;

            // Настройка lblCurrentPath для корректного отображения длинных путей
            lblCurrentPath.AutoSize = false;
            lblCurrentPath.Width = 350;
            lblCurrentPath.TextAlign = ContentAlignment.MiddleLeft;

            // Настройка выбора периода (только месяц и год)
            dtpPeriod.Format = DateTimePickerFormat.Custom;
            dtpPeriod.CustomFormat = "MMMM yyyy";
            dtpPeriod.ShowUpDown = true;

            // Настройка типов отчетов
            reportTypeComboBox.Items.Clear();
            reportTypeComboBox.Items.Add("Зарплатная ведомость");
            reportTypeComboBox.Items.Add("Премиальные выплаты");
            reportTypeComboBox.Items.Add("Особые суммы начисления");
            reportTypeComboBox.SelectedIndex = -1; // Чтобы сработало событие при установке индекса ниже
            reportTypeComboBox.SelectedIndexChanged += reportTypeComboBox_SelectedIndexChanged;

            // Настройка форматов
            cmbFormat.Items.Clear();
            cmbFormat.Items.Add("Текстовый (.txt)");
            cmbFormat.Items.Add("CSV (.csv)");
            cmbFormat.SelectedIndex = 0;

            // Загрузка типов выплат
            LoadPaymentTypes();

            // Установка начального типа отчета
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

        private void LoadPaymentTypes()
        {
            try
            {
                var types = Program.dbContext.SalaryAndBonuses
                    .Select(s => s.PaymentType)
                    .Distinct()
                    .OrderBy(t => t)
                    .ToList();

                clbPaymentTypes.Items.Clear();
                foreach (var t in types)
                {
                    clbPaymentTypes.Items.Add(t);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке типов выплат: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reportTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (reportTypeComboBox.SelectedIndex < 0) return;

            string selectedReport = reportTypeComboBox.SelectedItem.ToString();

            // Снимаем все отметки
            for (int i = 0; i < clbPaymentTypes.Items.Count; i++)
            {
                clbPaymentTypes.SetItemChecked(i, false);
            }

            // Автоматическая отметка на основе типа отчета
            for (int i = 0; i < clbPaymentTypes.Items.Count; i++)
            {
                string itemText = clbPaymentTypes.Items[i].ToString();

                if (selectedReport == "Зарплатная ведомость")
                {
                    if (itemText.Contains("Оклад") || itemText.Contains("Зарплата"))
                        clbPaymentTypes.SetItemChecked(i, true);
                }
                else if (selectedReport == "Премиальные выплаты")
                {
                    if (itemText.Contains("Премия"))
                        clbPaymentTypes.SetItemChecked(i, true);
                }
                else if (selectedReport == "Особые суммы начисления")
                {
                    if (itemText.Contains("Специальная сумма"))
                        clbPaymentTypes.SetItemChecked(i, true);
                }
            }
        }

        private void Form_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.ActiveControl = null;
            }
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void UpdatePathLabel(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                lblCurrentPath.Text = path;
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
                MessageBox.Show("Не выбран тип отчета!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (clbPaymentTypes.CheckedItems.Count == 0)
            {
                MessageBox.Show("Не выбраны типы выплат для отчета", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GetEmployeeData();

            if (employees.Count == 0)
            {
                return;
            }

            string reportType = reportTypeComboBox.SelectedItem.ToString();
            string selectedFormat = cmbFormat.SelectedItem.ToString();
            string extension = selectedFormat.Contains(".csv") ? ".csv" : ".txt";
            
            string reportContent = extension == ".csv" ? GetReportCsv() : GetReportText(reportType);
            string reportFolderPath = Settings.Default.ReportFolderPath;

            SaveReport(reportContent, reportFolderPath, reportType, extension);
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
                        UpdatePathLabel(folderDialog.SelectedPath);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при выборе папки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveReport(string reportContent, string folderPath, string reportType, string extension)
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

                // Формируем имя файла: ТипОтчета dd_MM_yyyy.ext
                string fileName = $"{reportType} {DateTime.Now:dd_MM_yyyy}{extension}";
                string fullPath = Path.Combine(folderPath, fileName);

                File.WriteAllText(fullPath, reportContent, Encoding.UTF8);
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
                int year = dtpPeriod.Value.Year;
                int month = dtpPeriod.Value.Month;

                // Получаем выбранные типы выплат
                var selectedTypes = clbPaymentTypes.CheckedItems.Cast<string>().ToList();

                var query = Program.dbContext.Payments
                    .Include(p => p.Employee)
                        .ThenInclude(e => e.PostNumberNavigation)
                    .Where(p => p.PaymentDate.Year == year && p.PaymentDate.Month == month)
                    .Where(p => selectedTypes.Contains(p.PaymentType))
                    .AsQueryable();

                var results = query.Select(p => new
                {
                    EmployeeName = p.Employee != null ? p.Employee.EmployeeName : "Неизвестный сотрудник",
                    Position = (p.Employee != null && p.Employee.PostNumberNavigation != null) ? p.Employee.PostNumberNavigation.Name : "Должность не найдена",
                    Amount = (double)p.PaymentAmount,
                    Type = p.PaymentType
                }).ToList();

                if (results.Count == 0)
                {
                    string period = dtpPeriod.Value.ToString("MMMM yyyy");
                    MessageBox.Show($"Выплат за период {period} по выбранным типам не найдено.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show($"Ошибка при получении данных: {ex.Message}{inner}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetReportText(string reportType)
        {
            StringBuilder reportText = new StringBuilder();
            string period = dtpPeriod.Value.ToString("MMMM yyyy");

            reportText.AppendLine(reportType);
            reportText.AppendLine($"Период: {period}");
            reportText.AppendLine("------------------------------------------------------------");
            reportText.AppendLine("Данные по сотрудникам:");
            reportText.AppendLine("------------------------------------------------------------");

            foreach (var employee in employees)
            {
                reportText.AppendLine($"ФИО: {employee.EmployeeName}, Должность: {employee.Position}, Сумма выплаты: {employee.PaymentAmount:F2} рублей ({employee.PaymentType})");
            }

            return reportText.ToString();
        }

        private string GetReportCsv()
        {
            StringBuilder csv = new StringBuilder();
            csv.AppendLine("ФИО;Должность;Сумма;Тип");

            foreach (var employee in employees)
            {
                csv.AppendLine($"{employee.EmployeeName};{employee.Position};{employee.PaymentAmount:F2};{employee.PaymentType}");
            }

            return csv.ToString();
        }
    }
}