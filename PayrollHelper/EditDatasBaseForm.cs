using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using PayrollHelper.Models;

namespace PayrollHelper
{
    public partial class EditDatasBaseForm : Form
    {
        public EditDatasBaseForm()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += LoginForm_KeyDown;
            this.MouseClick += Form_MouseClick;

            // Привязка событий вручную
            tableSelectorComboBox.SelectedIndexChanged += tableSelectorComboBox_SelectedIndexChanged;
            saveButton.Click += saveButton_Click;
            deleteButton.Click += deleteButton_Click;
            buttonShowEmployeeInfo.Click += buttonShowEmployeeInfo_Click;
            dgvTables.DataError += DgvTables_DataError;
            dgvTables.CellFormatting += DgvTables_CellFormatting;

            LoadTablesIntoComboBox();
            LoadEmployees();
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

        private void DgvTables_DataError(object? sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Ошибка ввода данных. Проверьте правильность формата (числа, даты и т.д.).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            e.ThrowException = false;
        }

        // Форматирование отображения логических значений (Страховка: Есть/Нет)
        private void DgvTables_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (tableSelectorComboBox.Text == "employees" && dgvTables.Columns[e.ColumnIndex].Name == "Insurance")
            {
                if (e.Value is bool val)
                {
                    e.Value = val ? "Есть" : "Нет";
                    e.FormattingApplied = true;
                }
                else if (e.Value == null)
                {
                    e.Value = "Нет";
                    e.FormattingApplied = true;
                }
            }
        }

        private void LoadDataIntoGridView()
        {
            try
            {
                string selectedTable = tableSelectorComboBox.Text.Trim();

                if (string.IsNullOrEmpty(selectedTable) || selectedTable == "Выберите таблицу для редактирования")
                {
                    return;
                }

                dgvTables.DataSource = null;

                switch (selectedTable)
                {
                    case "employees":
                        Program.dbContext.Employees.Load();
                        dgvTables.DataSource = Program.dbContext.Employees.Local.ToBindingList();
                        break;
                    case "positions":
                        Program.dbContext.Positions.Load();
                        dgvTables.DataSource = Program.dbContext.Positions.Local.ToBindingList();
                        break;
                    case "payments":
                        Program.dbContext.Payments.Load();
                        dgvTables.DataSource = Program.dbContext.Payments.Local.ToBindingList();
                        break;
                    case "salary_and_bonuses":
                        Program.dbContext.SalaryAndBonuses.Load();
                        dgvTables.DataSource = Program.dbContext.SalaryAndBonuses.Local.ToBindingList();
                        break;
                    case "taxation":
                        Program.dbContext.Taxations.Load();
                        dgvTables.DataSource = Program.dbContext.Taxations.Local.ToBindingList();
                        break;
                    default:
                        return;
                }

                UpdateStatusInfo(selectedTable);
                HideNavigationProperties();
                SetReadableHeaders();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatusInfo(string tableName)
        {
            lblCurrentTable.Text = $"Таблица: {tableName}";
            int count = dgvTables.Rows.Count;
            if (dgvTables.AllowUserToAddRows && count > 0) count--;
            lblRecordCount.Text = $"Записей: {count}";
        }

        private void HideNavigationProperties()
        {
            foreach (DataGridViewColumn col in dgvTables.Columns)
            {
                // Скрываем навигационные свойства EF Core
                if (col.Name.EndsWith("Navigation") ||
                    col.Name == "Payments" ||
                    col.Name == "Employees" ||
                    col.Name == "Taxations" ||
                    col.Name == "SalaryAndBonuses" ||
                    col.Name == "Employee" ||
                    col.Name == "Position" ||
                    col.Name == "SalaryAndBonusesTaxations")
                {
                    col.Visible = false;
                }

                // ID колонки делаем только для чтения
                if (col.Name.ToLower().EndsWith("id") || col.Name == "Id")
                {
                    col.ReadOnly = true;
                }
            }
        }

        private void SetReadableHeaders()
        {
            string selectedTable = tableSelectorComboBox.Text.Trim();

            // Создаем словарь БЕЗ дубликатов ключей
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "EmployeeId", "ID сотрудника" },
                { "EmployeeName", "ФИО" },
                { "PostNumber", "Номер должности" },
                { "Insurance", "Страховка" },
                { "PhoneNumber", "Телефон" },
                { "Address", "Адрес" },
                { "Name", "Название" },
                { "Description", "Описание" },
                { "Status", "Статус" },
                { "CreatedAt", "Создано" },
                { "UpdatedAt", "Обновлено" },
                { "PaymentId", "ID выплаты" },
                { "PaymentType", "Тип выплаты" },
                { "PaymentAmount", "Сумма" },
                { "PaymentDate", "Дата" },
                { "Amount", "Сумма" },
                { "DefaultAmount", "По умолчанию" },
                { "TaxType", "Тип налога" },
                { "TaxRate", "Ставка (%)" }
            };

            foreach (DataGridViewColumn col in dgvTables.Columns)
            {
                if (headers.ContainsKey(col.Name))
                {
                    col.HeaderText = headers[col.Name];
                }
                
                // Обработка универсального поля "Id" в зависимости от контекста
                if (col.Name == "Id")
                {
                    col.HeaderText = (selectedTable == "positions") ? "ID должности" : "ID";
                }
            }
        }

        private void LoadTablesIntoComboBox()
        {
            tableSelectorComboBox.Items.Clear();
            tableSelectorComboBox.Items.AddRange(new string[] 
            { 
                "employees", 
                "positions", 
                "payments", 
                "salary_and_bonuses", 
                "taxation"
            });

            if (tableSelectorComboBox.Items.Count > 0)
            {
                tableSelectorComboBox.SelectedIndex = 0;
            }
        }

        private void LoadEmployees()
        {
            try
            {
                var currentSelection = comboBoxEmployeeName.SelectedItem?.ToString();
                comboBoxEmployeeName.Items.Clear();
                var employees = Program.dbContext.Employees.OrderBy(e => e.EmployeeName).Select(e => e.EmployeeName).ToList();
                comboBoxEmployeeName.Items.AddRange(employees.ToArray());

                if (!string.IsNullOrEmpty(currentSelection) && comboBoxEmployeeName.Items.Contains(currentSelection))
                    comboBoxEmployeeName.SelectedItem = currentSelection;
                else if (comboBoxEmployeeName.Items.Count > 0)
                    comboBoxEmployeeName.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке сотрудников: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tableSelectorComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LoadDataIntoGridView();
        }

        private void saveButton_Click(object? sender, EventArgs e)
        {
            try
            {
                dgvTables.EndEdit();
                Program.dbContext.SaveChanges();
                MessageBox.Show("Изменения успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataIntoGridView();
                LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}\nВнутренняя ошибка: {ex.InnerException?.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadDataIntoGridView();
            }
        }

        private async void deleteButton_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgvTables.CurrentRow != null && !dgvTables.CurrentRow.IsNewRow)
                {
                    var item = dgvTables.CurrentRow.DataBoundItem;
                    if (item == null) return;

                    string selectedTable = tableSelectorComboBox.Text.Trim();

                    // --- УДАЛЕНИЕ СОТРУДНИКА ---
                    if (selectedTable == "employees")
                    {
                        var employee = (Employee)item;
                        var paymentCount = await Program.dbContext.Payments.CountAsync(p => p.EmployeeId == employee.EmployeeId);

                        string confirmMsg = $"Вы действительно хотите удалить сотрудника {employee.EmployeeName}?";
                        if (paymentCount > 0)
                        {
                            confirmMsg = $"У сотрудника {employee.EmployeeName} найдено {paymentCount} связанных выплат. " +
                                         "\n\nВНИМАНИЕ: Все выплаты будут удалены вместе с сотрудником. Продолжить?";
                        }

                        if (MessageBox.Show(confirmMsg, "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                            return;

                        using var transaction = await Program.dbContext.Database.BeginTransactionAsync();
                        try
                        {
                            var payments = await Program.dbContext.Payments.Where(p => p.EmployeeId == employee.EmployeeId).ToListAsync();
                            if (payments.Any())
                            {
                                Program.dbContext.Payments.RemoveRange(payments);
                                await Program.dbContext.SaveChangesAsync();
                            }

                            Program.dbContext.Employees.Remove(employee);
                            await Program.dbContext.SaveChangesAsync();

                            await transaction.CommitAsync();
                            MessageBox.Show($"Сотрудник {employee.EmployeeName} успешно удален.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadEmployees();
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            throw ex;
                        }
                    }

                    // --- УДАЛЕНИЕ ДОЛЖНОСТИ ---
                    else if (selectedTable == "positions")
                    {
                        var position = (Position)item;
                        bool hasEmployees = await Program.dbContext.Employees.AnyAsync(e => e.PostNumber == position.Id);

                        if (hasEmployees)
                        {
                            MessageBox.Show($"Невозможно удалить должность '{position.Name}', так как она закреплена за сотрудниками.", 
                                            "Защита данных", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        if (MessageBox.Show($"Удалить должность '{position.Name}'?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Program.dbContext.Positions.Remove(position);
                            await Program.dbContext.SaveChangesAsync();
                            MessageBox.Show("Должность удалена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    // --- УДАЛЕНИЕ ТИПА ВЫПЛАТЫ ---
                    else if (selectedTable == "salary_and_bonuses")
                    {
                        var sb = (SalaryAndBonus)item;
                        bool isUsedInPayments = await Program.dbContext.Payments.AnyAsync(p => p.PaymentType == sb.PaymentType);

                        if (isUsedInPayments)
                        {
                            MessageBox.Show($"Невозможно удалить тип выплаты '{sb.PaymentType}', так как он уже использован в истории выплат.", 
                                            "Защита данных", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        if (MessageBox.Show($"Удалить тип выплаты '{sb.PaymentType}'? Связи с налогами будут также удалены.", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                            return;

                        using var transaction = await Program.dbContext.Database.BeginTransactionAsync();
                        try
                        {
                            await Program.dbContext.Entry(sb).Collection(s => s.Taxations).LoadAsync();
                            sb.Taxations.Clear();
                            await Program.dbContext.SaveChangesAsync();

                            Program.dbContext.SalaryAndBonuses.Remove(sb);
                            await Program.dbContext.SaveChangesAsync();

                            await transaction.CommitAsync();
                            MessageBox.Show("Тип выплаты успешно удален.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            throw ex;
                        }
                    }

                    // --- УДАЛЕНИЕ НАЛОГА ---
                    else if (selectedTable == "taxation")
                    {
                        var tax = (Taxation)item;
                        
                        await Program.dbContext.Entry(tax).Collection(t => t.SalaryAndBonuses).LoadAsync();
                        int usageCount = tax.SalaryAndBonuses.Count;

                        string confirmMsg = usageCount > 0 
                            ? $"Налог '{tax.TaxType}' привязан к типам выплат. При удалении эти связи будут разорваны. Продолжить?"
                            : $"Удалить налог '{tax.TaxType}'?";

                        if (MessageBox.Show(confirmMsg, "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                            return;

                        using var transaction = await Program.dbContext.Database.BeginTransactionAsync();
                        try
                        {
                            tax.SalaryAndBonuses.Clear();
                            await Program.dbContext.SaveChangesAsync();

                            Program.dbContext.Taxations.Remove(tax);
                            await Program.dbContext.SaveChangesAsync();

                            await transaction.CommitAsync();
                            MessageBox.Show("Налог успешно удален.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            throw ex;
                        }
                    }

                    // --- УДАЛЕНИЕ ВЫПЛАТЫ ---
                    else if (selectedTable == "payments")
                    {
                        var payment = (Payment)item;
                        if (MessageBox.Show($"Удалить запись о выплате №{payment.PaymentId}?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Program.dbContext.Payments.Remove(payment);
                            await Program.dbContext.SaveChangesAsync();
                            MessageBox.Show("Выплата удалена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                    LoadDataIntoGridView();
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выделите строку для удаления.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                string errorMsg = $"Произошла ошибка при удалении:\n{ex.Message}";
                if (ex.InnerException != null)
                    errorMsg += $"\nПодробности: {ex.InnerException.Message}";
                
                MessageBox.Show(errorMsg, "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadDataIntoGridView();
            }
        }

        private void buttonShowEmployeeInfo_Click(object? sender, EventArgs e)
        {
            try
            {
                if (comboBoxEmployeeName.SelectedItem == null) return;

                string name = comboBoxEmployeeName.SelectedItem.ToString();
                var emp = Program.dbContext.Employees
                                    .Include(e => e.PostNumberNavigation)
                                    .FirstOrDefault(e => e.EmployeeName == name);

                if (emp != null)
                {
                    string positionName = emp.PostNumberNavigation?.Name ?? "Не указана";

                    MessageBox.Show($"ФИО: {emp.EmployeeName}\n" +
                                    $"Должность: {positionName}\n" +
                                    $"Телефон: {emp.PhoneNumber}\n" +
                                    $"Адрес: {emp.Address}\n" +
                                    $"Страховка: {(emp.Insurance == true ? "Есть" : "Нет")}", 
                                    "Информация о сотруднике");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                bool otherVisibleForms = false;
                foreach (Form f in Application.OpenForms)
                {
                    if (f != this && f.Visible)
                    {
                        otherVisibleForms = true;
                        break;
                    }
                }
                if (!otherVisibleForms)
                {
                    Application.Exit();
                }
            }
        }
    }
}
