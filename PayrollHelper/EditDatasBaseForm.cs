using Npgsql;
using System;
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

                if (string.IsNullOrEmpty(selectedTable))
                {
                    MessageBox.Show("Пожалуйста, выберите таблицу для отображения.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<string> allowedTables = new List<string> { "positions", "employees", "payments", "salary_and_bonuses", "taxation" };

                if (!allowedTables.Contains(selectedTable))
                {
                    MessageBox.Show("Выбрана недопустимая таблица.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string query = $"SELECT * FROM {selectedTable}";

                using (var conn = new NpgsqlConnection(LoadDB.Connection_String))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dataGridView1.DataSource = null;
                        dataGridView1.Rows.Clear();
                        dataGridView1.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных в таблицу: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadTablesIntoComboBox()
        {
            try
            {
                string[] tables = new string[] { "positions", "employees", "payments", "salary_and_bonuses", "taxation" };

                using (var conn = new NpgsqlConnection(LoadDB.Connection_String))
                {
                    conn.Open();

                    foreach (var table in tables)
                    {
                        string query = $"SELECT '{table}' as table_name";
                        using (var cmd = new NpgsqlCommand(query, conn))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string tableName = reader["table_name"].ToString();
                                    tableSelectorComboBox.Items.Add(tableName);
                                }
                            }
                        }
                    }
                }

                if (tableSelectorComboBox.Items.Count > 0)
                {
                    tableSelectorComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке списка таблиц: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEmployees()
        {
            try
            {
                string query = "SELECT employee_name FROM employees";

                using (var conn = new NpgsqlConnection(LoadDB.Connection_String))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            comboBoxEmployeeName.Items.Clear();

                            while (reader.Read())
                            {
                                string employeeName = reader["employee_name"].ToString();
                                comboBoxEmployeeName.Items.Add(employeeName);
                            }
                        }
                    }
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
            try
            {
                int column = dataGridView1.SelectedCells[0].ColumnIndex;
                int row = dataGridView1.SelectedCells[0].RowIndex;

                if (dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText.ToString() == "payment_id" ||
                    dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText.ToString() == "id")

                {
                    MessageBox.Show("Столбец нельзя редактировать!");
                    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[dataGridView1.CurrentCell.ColumnIndex + 1].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при редактировании ячейки: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tableSelectorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataIntoGridView();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                string query = $"SELECT * FROM {tableSelectorComboBox.Text}";

                using (var conn = new NpgsqlConnection(LoadDB.Connection_String))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                        NpgsqlCommandBuilder builder = new NpgsqlCommandBuilder(da);

                        DataTable dt = (DataTable)dataGridView1.DataSource;

                        foreach (DataRow row in dt.Rows)
                        {
                            if (row.RowState == DataRowState.Added)
                            {
                                if (row.Table.Columns.Contains("id"))
                                    row["id"] = DBNull.Value;

                                if (row.Table.Columns.Contains("payment_id"))
                                    row["payment_id"] = DBNull.Value;

                                if (row.Table.Columns.Contains("salary_and_bonuses_id"))
                                    row["salary_and_bonuses_id"] = DBNull.Value;

                                if (row.Table.Columns.Contains("taxation_id"))
                                    row["taxation_id"] = DBNull.Value;
                            }
                        }

                        da.Update(dt);
                    }
                }

                MessageBox.Show("Изменения сохранены в базе данных!");

                LoadDataIntoGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка ввода данных: {ex.Message}");
                LoadDataIntoGridView();
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    string tableName = tableSelectorComboBox.Text;
                    string idColumn = GetIdColumnForTable(tableName);

                    if (idColumn == null)
                    {
                        MessageBox.Show("Не удалось определить ключевую колонку для таблицы.");
                        return;
                    }

                    int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[idColumn].Value);

                    using (var conn = new NpgsqlConnection(LoadDB.Connection_String))
                    {
                        conn.Open();

                        if (tableName == "employees")
                        {
                            DeleteEmployee(id);
                            comboBoxEmployeeName.Items.Clear();
                            LoadEmployees();
                        }
                        else if (tableName == "positions")
                        {
                            DeletePosition(id);
                        }
                        else if (tableName == "payments")
                        {
                            DeletePayment(id);
                        }
                        else if (tableName == "salary_and_bonuses")
                        {
                            DeleteSalaryAndBonus(id);
                        }
                        else if (tableName == "taxation")
                        {
                            DeleteTaxation(id);
                        }
                        else
                        {
                            MessageBox.Show("Неизвестная таблица для удаления.");
                            return;
                        }
                    }

                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                    MessageBox.Show("Строка успешно удалена!");
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите строку для удаления");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                MessageBox.Show($"Произошла ошибка при удалении: {ex.Message}");
            }
        }

        private void DeleteEmployee(int employeeId)
        {
            try
            {
                using (var conn = new NpgsqlConnection(LoadDB.Connection_String))
                {
                    conn.Open();

                    string deletePaymentsQuery = "DELETE FROM payments WHERE employee_id = @employee_id";
                    using (var cmd = new NpgsqlCommand(deletePaymentsQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@employee_id", employeeId);
                        cmd.ExecuteNonQuery();
                    }

                    string deleteEmployeeQuery = "DELETE FROM employees WHERE employee_id = @employee_id";
                    using (var cmd = new NpgsqlCommand(deleteEmployeeQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@employee_id", employeeId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении сотрудника: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeletePosition(int positionId)
        {
            using (var conn = new NpgsqlConnection(LoadDB.Connection_String))
            {
                conn.Open();
                string deletePositionQuery = "DELETE FROM positions WHERE id = @id";
                using (var cmd = new NpgsqlCommand(deletePositionQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@id", positionId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void DeletePayment(int paymentId)
        {
            using (var conn = new NpgsqlConnection(LoadDB.Connection_String))
            {
                conn.Open();
                string deletePaymentQuery = "DELETE FROM payments WHERE payment_id = @payment_id";
                using (var cmd = new NpgsqlCommand(deletePaymentQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@payment_id", paymentId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void DeleteSalaryAndBonus(int id)
        {
            using (var conn = new NpgsqlConnection(LoadDB.Connection_String))
            {
                conn.Open();
                string deleteSalaryAndBonusQuery = "DELETE FROM salary_and_bonuses WHERE id = @id";
                using (var cmd = new NpgsqlCommand(deleteSalaryAndBonusQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void DeleteTaxation(int id)
        {
            using (var conn = new NpgsqlConnection(LoadDB.Connection_String))
            {
                conn.Open();
                string deleteTaxationQuery = "DELETE FROM taxation WHERE id = @id";
                using (var cmd = new NpgsqlCommand(deleteTaxationQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string GetIdColumnForTable(string tableName)
        {
            switch (tableName)
            {
                case "employees":
                    return "employee_id";
                case "positions":
                    return "id";
                case "payments":
                    return "payment_id";
                case "salary_and_bonuses":
                    return "id";
                case "taxation":
                    return "id";
                default:
                    return null;
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

                string query = "SELECT * FROM employees WHERE employee_name = @employee_name";

                using (var conn = new NpgsqlConnection(LoadDB.Connection_String))
                {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@employee_name", employeeName);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string employeeId = reader["employee_id"].ToString();
                                string phoneNumber = reader["phone_number"].ToString();
                                string address = reader["address"].ToString();
                                bool insurance = Convert.ToBoolean(reader["insurance"]);
                                int postNumber = Convert.ToInt32(reader["post_number"]);

                                string message = $"Информация о сотруднике:\n" +
                                                 $"ID: {employeeId}\n" +
                                                 $"Имя: {employeeName}\n" +
                                                 $"Телефон: {phoneNumber}\n" +
                                                 $"Адрес: {address}\n" +
                                                 $"Страхование: {(insurance ? "Есть" : "Нет")}\n" +
                                                 $"Номер должности: {postNumber}";

                                MessageBox.Show(message, "Информация о сотруднике");
                            }
                            else
                            {
                                MessageBox.Show("Сотрудник не найден.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении информации о сотруднике: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBoxEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string employeeName = comboBoxEmployeeName.Text;

                if (string.IsNullOrEmpty(employeeName))
                {
                    MessageBox.Show("Выберите сотрудника.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке информации о сотруднике: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedTable = tableSelectorComboBox.Text.Trim();

                if (string.IsNullOrEmpty(selectedTable))
                {
                    MessageBox.Show("Пожалуйста, выберите таблицу для отображения.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<string> allowedTables = new List<string> { "positions", "employees", "payments", "salary_and_bonuses", "taxation" };

                if (!allowedTables.Contains(selectedTable))
                {
                    MessageBox.Show("Выбрана недопустимая таблица.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                LoadDataIntoGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
