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

            // Привязка событий вручную, так как они могут отсутствовать в дизайнере
            tableSelectorComboBox.SelectedIndexChanged += tableSelectorComboBox_SelectedIndexChanged;
            saveButton.Click += saveButton_Click;
            deleteButton.Click += deleteButton_Click;
            refreshButton.Click += refreshButton_Click;
            buttonShowEmployeeInfo.Click += buttonShowEmployeeInfo_Click;
            dgvTables.DataError += DgvTables_DataError;

            LoadTablesIntoComboBox();
            LoadEmployees();
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

        private void LoadDataIntoGridView()
        {
            try
            {
                string selectedTable = tableSelectorComboBox.Text.Trim();

                if (string.IsNullOrEmpty(selectedTable) || selectedTable == "Выберите таблицу для редактирования")
                {
                    return;
                }

                // Очистка привязки перед новой загрузкой
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

                if (col.Name.ToLower().EndsWith("id"))
                {
                    col.ReadOnly = true;
                }
            }
        }

        private void SetReadableHeaders()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "EmployeeId", "ID Сотрудника" },
                { "EmployeeName", "ФИО" },
                { "PostNumber", "№ Должности" },
                { "Insurance", "Страховка" },
                { "PhoneNumber", "Телефон" },
                { "Address", "Адрес" },
                { "PositionId", "ID Должности" },
                { "PositionName", "Должность" },
                { "BaseSalary", "Оклад" },
                { "PaymentId", "ID Выплаты" },
                { "PaymentDate", "Дата" },
                { "Amount", "Сумма" },
                { "Bonus", "Премия" },
                { "TaxId", "ID Налога" },
                { "TaxName", "Название налога" },
                { "Rate", "Ставка %" }
            };

            foreach (DataGridViewColumn col in dgvTables.Columns)
            {
                if (headers.ContainsKey(col.Name))
                {
                    col.HeaderText = headers[col.Name];
                }
            }
        }

        private void LoadTablesIntoComboBox()
        {
            tableSelectorComboBox.Items.Clear();
            tableSelectorComboBox.Items.AddRange(new string[] { "employees", "positions", "payments", "salary_and_bonuses", "taxation" });

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
                dgvTables.EndEdit(); // Завершаем редактирование текущей ячейки
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

        private void deleteButton_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgvTables.CurrentRow != null && !dgvTables.CurrentRow.IsNewRow)
                {
                    var item = dgvTables.CurrentRow.DataBoundItem;
                    if (item == null) return;

                    var confirmResult = MessageBox.Show("Вы уверены, что хотите удалить выбранную запись?",
                                                       "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmResult == DialogResult.Yes)
                    {
                        string selectedTable = tableSelectorComboBox.Text.Trim();

                        if (selectedTable == "employees")
                            Program.dbContext.Employees.Remove((Employee)item);
                        else if (selectedTable == "positions")
                            Program.dbContext.Positions.Remove((Position)item);
                        else if (selectedTable == "payments")
                            Program.dbContext.Payments.Remove((Payment)item);
                        else if (selectedTable == "salary_and_bonuses")
                            Program.dbContext.SalaryAndBonuses.Remove((SalaryAndBonus)item);
                        else if (selectedTable == "taxation")
                            Program.dbContext.Taxations.Remove((Taxation)item);

                        Program.dbContext.SaveChanges();
                        LoadDataIntoGridView();
                        if (selectedTable == "employees") LoadEmployees();
                    }
                }
                else
                {
                    MessageBox.Show("Выберите запись для удаления.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadDataIntoGridView();
            }
        }

        private void buttonShowEmployeeInfo_Click(object? sender, EventArgs e)
        {
            try
            {
                if (comboBoxEmployeeName.SelectedItem == null) return;

                string name = comboBoxEmployeeName.SelectedItem.ToString();
                // Загружаем сотрудника вместе с навигационным свойством должности
                var emp = Program.dbContext.Employees
                                    .Include(e => e.PostNumberNavigation)
                                    .FirstOrDefault(e => e.EmployeeName == name);

                if (emp != null)
                {
                    // Получаем название должности (если навигационное свойство не пустое)
                    string positionName = emp.PostNumberNavigation?.Name ?? "Не указана";

                    MessageBox.Show($"ФИО: {emp.EmployeeName}\n" +
                                    $"Должность: {positionName}\n" +
                                    $"Телефон: {emp.PhoneNumber}\n" +
                                    $"Адрес: {emp.Address}\n" +
                                    $"Страховка: {(emp.Insurance == true ? "Есть" : "Нет")}", 
                                    "Информация о сотруднике");
                }
                else
                {
                    MessageBox.Show("Сотрудник не найден.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void refreshButton_Click(object? sender, EventArgs e)
        {
            LoadDataIntoGridView();
            LoadEmployees();
        }
    }
}
