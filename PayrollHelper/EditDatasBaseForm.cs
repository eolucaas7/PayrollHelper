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
            LoadTablesIntoComboBox();
            LoadEmployees();
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
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();

                switch (selectedTable)
                {
                    case "employees":
                        Program.dbContext.Employees.Load();
                        dataGridView1.DataSource = Program.dbContext.Employees.Local.ToBindingList();
                        break;
                    case "positions":
                        Program.dbContext.Positions.Load();
                        dataGridView1.DataSource = Program.dbContext.Positions.Local.ToBindingList();
                        break;
                    case "payments":
                        Program.dbContext.Payments.Load();
                        dataGridView1.DataSource = Program.dbContext.Payments.Local.ToBindingList();
                        break;
                    case "salary_and_bonuses":
                        Program.dbContext.SalaryAndBonuses.Load();
                        dataGridView1.DataSource = Program.dbContext.SalaryAndBonuses.Local.ToBindingList();
                        break;
                    case "taxation":
                        Program.dbContext.Taxations.Load();
                        dataGridView1.DataSource = Program.dbContext.Taxations.Local.ToBindingList();
                        break;
                    default:
                        MessageBox.Show("Выбрана недопустимая таблица.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                }

                // Скрываем навигационные свойства, чтобы они не отображались в GridView
                HideNavigationProperties();
                
                // Настраиваем названия колонок для красоты (опционально)
                SetReadableHeaders();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HideNavigationProperties()
        {
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                // Скрываем колонки, которые являются коллекциями или другими сущностями
                if (col.Name.EndsWith("Navigation") || 
                    col.Name == "Payments" || 
                    col.Name == "Employees" || 
                    col.Name == "Taxations" || 
                    col.Name == "SalaryAndBonuses" || 
                    col.Name == "Employee")
                {
                    col.Visible = false;
                }
                
                // ID колонки делаем только для чтения
                if (col.Name.ToLower().Contains("id"))
                {
                    col.ReadOnly = true;
                }
            }
        }

        private void SetReadableHeaders()
        {
            // Здесь можно переименовать заголовки колонок, если нужно
            // Например: if (dataGridView1.Columns.Contains("EmployeeName")) dataGridView1.Columns["EmployeeName"].HeaderText = "ФИО";
        }

        private void LoadTablesIntoComboBox()
        {
            tableSelectorComboBox.Items.Clear();
            tableSelectorComboBox.Items.Add("employees");
            tableSelectorComboBox.Items.Add("payments");
            tableSelectorComboBox.Items.Add("positions");
            tableSelectorComboBox.Items.Add("salary_and_bonuses");
            tableSelectorComboBox.Items.Add("taxation");

            if (tableSelectorComboBox.Items.Count > 0)
            {
                tableSelectorComboBox.SelectedIndex = 0;
            }
        }

        private void LoadEmployees()
        {
            try
            {
                comboBoxEmployeeName.Items.Clear();
                var employees = Program.dbContext.Employees.Select(e => e.EmployeeName).ToList();
                foreach (var name in employees)
                {
                    comboBoxEmployeeName.Items.Add(name);
                }

                if (comboBoxEmployeeName.Items.Count > 0)
                {
                    comboBoxEmployeeName.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке сотрудников: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // В EF Core версии этот метод можно оставить пустым или удалить, 
            // так как мы установили ReadOnly для ID колонок в HideNavigationProperties
        }

        private void tableSelectorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataIntoGridView();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                Program.dbContext.SaveChanges();
                MessageBox.Show("Изменения успешно сохранены в базе данных!");
                LoadDataIntoGridView();
                LoadEmployees(); // На случай, если имена сотрудников изменились
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // В случае ошибки лучше перезагрузить данные, чтобы синхронизироваться с БД
                LoadDataIntoGridView();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    var item = dataGridView1.SelectedRows[0].DataBoundItem;
                    if (item == null) return;

                    var confirmResult = MessageBox.Show("Вы уверены, что хотите удалить выбранную строку?", 
                                                       "Подтверждение удаления", 
                                                       MessageBoxButtons.YesNo, 
                                                       MessageBoxIcon.Question);
                    
                    if (confirmResult == DialogResult.Yes)
                    {
                        string selectedTable = tableSelectorComboBox.Text.Trim();

                        if (selectedTable == "employees")
                        {
                            var employee = (Employee)item;
                            // Ручное удаление связанных выплат, так как в БД настроено ClientSetNull
                            var payments = Program.dbContext.Payments.Where(p => p.EmployeeId == employee.EmployeeId).ToList();
                            Program.dbContext.Payments.RemoveRange(payments);
                            Program.dbContext.Employees.Remove(employee);
                        }
                        else if (selectedTable == "positions")
                        {
                            Program.dbContext.Positions.Remove((Position)item);
                        }
                        else if (selectedTable == "payments")
                        {
                            Program.dbContext.Payments.Remove((Payment)item);
                        }
                        else if (selectedTable == "salary_and_bonuses")
                        {
                            Program.dbContext.SalaryAndBonuses.Remove((SalaryAndBonus)item);
                        }
                        else if (selectedTable == "taxation")
                        {
                            Program.dbContext.Taxations.Remove((Taxation)item);
                        }

                        Program.dbContext.SaveChanges();
                        MessageBox.Show("Строка успешно удалена!");
                        
                        if (selectedTable == "employees")
                            LoadEmployees();
                        
                        LoadDataIntoGridView();
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите всю строку (выделите строку слева), чтобы удалить её.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadDataIntoGridView();
            }
        }

        private void buttonShowEmployeeInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxEmployeeName.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, выберите сотрудника.");
                    return;
                }

                string employeeName = comboBoxEmployeeName.SelectedItem.ToString();
                var employee = Program.dbContext.Employees.FirstOrDefault(e => e.EmployeeName == employeeName);

                if (employee != null)
                {
                    string message = $"Информация о сотруднике:\n" +
                                     $"ID: {employee.EmployeeId}\n" +
                                     $"Имя: {employee.EmployeeName}\n" +
                                     $"Телефон: {employee.PhoneNumber}\n" +
                                     $"Адрес: {employee.Address}\n" +
                                     $"Страхование: {(employee.Insurance == true ? "Есть" : "Нет")}\n" +
                                     $"Номер должности: {employee.PostNumber}";

                    MessageBox.Show(message, "Информация о сотруднике");
                }
                else
                {
                    MessageBox.Show("Сотрудник не найден.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении информации о сотруднике: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Можно добавить логику, если нужно что-то делать при смене сотрудника
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            // Для полного обновления сбрасываем локальные изменения и загружаем заново
            // В простом случае просто вызываем LoadDataIntoGridView
            LoadDataIntoGridView();
            LoadEmployees();
            MessageBox.Show("Данные обновлены.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
