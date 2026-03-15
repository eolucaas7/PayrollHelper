using System;
using Npgsql;
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
    public partial class DashBoardForm : Form
    {
        public DashBoardForm()
        {
            InitializeComponent();
            LoadPostInComboBox();
            LoadEmployees();
            LoadBonusTypes();
        }

        private void LoadPostInComboBox()
        {
            try
            {
                using (var conn = new NpgsqlConnection(LoadDB.Connection_String))
                {
                    conn.Open();
                    string query = $"SELECT name FROM positions";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            comboPosition.Items.Clear();
                            while (reader.Read())
                            {
                                comboPosition.Items.Add(reader["name"].ToString());
                            }
                        }


                    }

                }

                if (comboPosition.Items.Count > 0)
                {
                    comboPosition.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке списка таблиц: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAddEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new NpgsqlConnection(LoadDB.Connection_String))
                {
                    conn.Open();
                    string query = $"SELECT id FROM positions WHERE name='{comboPosition.SelectedItem.ToString()}'";
                    string fd = "";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                fd = reader["id"].ToString();
                            }
                        }
                    }
                    query = $"INSERT into employees (employee_name, post_number, insurance, phone_number, address) values ('{textFullName.Text}', '{fd}', '{checkInsurance.Checked}','{textPhoneNumber.Text}', '{textAddress.Text}')";

                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Сотрудник успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            comboEmployee.Items.Clear();

                            while (reader.Read())
                            {
                                string employeeName = reader["employee_name"].ToString();
                                comboEmployee.Items.Add(employeeName);
                            }
                        }
                    }
                }

                if (comboEmployee.Items.Count > 0)
                {
                    comboEmployee.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке сотрудников: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBonusTypes()
        {
            try
            {
                using (var conn = new NpgsqlConnection(LoadDB.Connection_String))
                {
                    conn.Open();
                    string query = "SELECT payment_type FROM salary_and_bonuses WHERE payment_type ILIKE 'Премия%'";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            comboBonusType.Items.Clear();
                            while (reader.Read())
                            {
                                comboBonusType.Items.Add(reader["payment_type"].ToString());
                            }
                        }
                    }
                }

                if (comboBonusType.Items.Count > 0)
                {
                    comboBonusType.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке видов премий: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAddPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkSpecialAmount.Checked)
                {
                    string FIO = comboEmployee.Text;
                    double Special_Payment = Convert.ToDouble(textSpecialAmount.Text);
                    double tax_rate = 0;

                    DateTime paymentDate = DateTime.Now;

                    string query = $"SELECT employee_id FROM employees WHERE employee_name='{FIO}'";
                    string employeeId = "";

                    using (var conn = new NpgsqlConnection(LoadDB.Connection_String))
                    {
                        conn.Open();

                        using (var cmd = new NpgsqlCommand(query, conn))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    employeeId = reader["employee_id"].ToString();
                                }
                                else
                                {
                                    MessageBox.Show("Сотрудник не найден.");
                                    return;
                                }
                            }
                        }

                        string taxQuery = "SELECT tax_rate FROM taxation WHERE tax_type='НДФЛ'";
                        using (var cmd = new NpgsqlCommand(taxQuery, conn))
                        {
                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    tax_rate = Convert.ToDouble(reader["tax_rate"]);
                                }
                            }
                        }

                        Special_Payment = Math.Round(Special_Payment - (Special_Payment * tax_rate / 100), 2);

                        query = $"INSERT INTO payments (payment_type, payment_amount, payment_date, employee_id) VALUES ('Специальная сумма', {Special_Payment}, '{paymentDate}', '{employeeId}')";

                        using (var cmd = new NpgsqlCommand(query, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show("Специальная сумма успешно добавлена.");
                    }
                }
                else
                {
                    if (comboPaymentType.Text == "Зарплата")
                    {
                        string employeeId = "";
                        bool hasInsurance = false;
                        string post_number = "";
                        string post_name = "";
                        double default_amount = 0;
                        string taxQuery = "";
                        string insuranceTaxQuery = "";
                        double tax_rate = 0;
                        double insuranceTaxRate = 0;
                        double totalTaxRate = 0;
                        double finalAmount = 0;

                        string query = $"SELECT employee_id, post_number, insurance FROM employees WHERE employee_name='{comboEmployee.Text}'";
                        string FIO = comboEmployee.Text;

                        using (var conn = new NpgsqlConnection(LoadDB.Connection_String))
                        {
                            conn.Open();

                            using (var cmd = new NpgsqlCommand(query, conn))
                            {
                                using (var reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        employeeId = reader["employee_id"].ToString();
                                        hasInsurance = reader["insurance"] != DBNull.Value && (bool)reader["insurance"];
                                        post_number = reader["post_number"].ToString();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Сотрудник не найден.");
                                        return;
                                    }
                                }
                            }

                            query = $"SELECT name FROM positions WHERE id='{post_number}'";
                            using (var cmd = new NpgsqlCommand(query, conn))
                            {
                                using (var reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        post_name = reader["name"].ToString();
                                    }
                                }
                            }

                            query = $"SELECT default_amount FROM salary_and_bonuses WHERE payment_type ILIKE '%{post_name}%'";
                            using (var cmd = new NpgsqlCommand(query, conn))
                            {
                                using (var reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        default_amount = Convert.ToDouble(reader["default_amount"]);
                                    }
                                }
                            }

                            if (hasInsurance)
                            {
                                taxQuery = "SELECT tax_rate FROM taxation WHERE tax_type='НДФЛ'";
                                using (var cmd = new NpgsqlCommand(taxQuery, conn))
                                {
                                    using (var reader = cmd.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            tax_rate = Convert.ToDouble(reader["tax_rate"]);
                                        }
                                    }
                                }

                                insuranceTaxQuery = "SELECT tax_rate FROM taxation WHERE tax_type='страховка'";
                                using (var cmd = new NpgsqlCommand(insuranceTaxQuery, conn))
                                {
                                    using (var reader = cmd.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            insuranceTaxRate = Convert.ToDouble(reader["tax_rate"]);
                                        }
                                    }
                                }

                                totalTaxRate = tax_rate + insuranceTaxRate;
                                finalAmount = Math.Round(default_amount - (default_amount * totalTaxRate / 100), 2);
                            }
                            else
                            {
                                taxQuery = "SELECT tax_rate FROM taxation WHERE tax_type='НДФЛ'";
                                using (var cmd = new NpgsqlCommand(taxQuery, conn))
                                {
                                    using (var reader = cmd.ExecuteReader())
                                    {
                                        if (reader.Read())
                                        {
                                            tax_rate = Convert.ToDouble(reader["tax_rate"]);
                                        }
                                    }
                                }

                                finalAmount = Math.Round(default_amount - (default_amount * tax_rate / 100), 2);
                            }
                        }

                        DateTime paymentDate = DateTime.Now;
                        query = $"INSERT INTO payments (payment_type, payment_amount, payment_date, employee_id) VALUES ('{comboPaymentType.Text}', '{finalAmount}', '{paymentDate}', '{employeeId}')";
                        using (var conn = new NpgsqlConnection(LoadDB.Connection_String))
                        {
                            conn.Open();
                            using (var cmd = new NpgsqlCommand(query, conn))
                            {
                                cmd.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Выплата зарплаты успешно добавлена.");
                    }
                    else if (comboPaymentType.Text == "Премия")
                    {
                        string FIO = comboEmployee.Text;
                        string Bonus_Type = comboBonusType.Text;
                        string employeeId = "";
                        double bonusAmount = 0;

                        string query = $"SELECT employee_id FROM employees WHERE employee_name='{FIO}'";
                        using (var conn = new NpgsqlConnection(LoadDB.Connection_String))
                        {
                            conn.Open();

                            using (var cmd = new NpgsqlCommand(query, conn))
                            {
                                using (var reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        employeeId = reader["employee_id"].ToString();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Сотрудник не найден.");
                                        return;
                                    }
                                }
                            }

                            query = $"SELECT default_amount FROM salary_and_bonuses WHERE payment_type ILIKE '%{Bonus_Type}%'";
                            using (var cmd = new NpgsqlCommand(query, conn))
                            {
                                using (var reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        bonusAmount = Convert.ToDouble(reader["default_amount"]);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Вид премии не найден.");
                                        return;
                                    }
                                }
                            }

                            DateTime paymentDate = DateTime.Now;
                            query = $"INSERT INTO payments (payment_type, payment_amount, payment_date, employee_id) VALUES ('Премия', {bonusAmount}, '{paymentDate}', '{employeeId}')";
                            using (var cmd = new NpgsqlCommand(query, conn))
                            {
                                cmd.ExecuteNonQuery();
                            }

                            MessageBox.Show("Премия успешно добавлена.");
                        }
                    }
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Ошибка: Убедитесь, что сумма введена в правильном формате.\n" + ex.Message);
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Ошибка базы данных: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }
            finally
            {
                textSpecialAmount.Clear();
                checkSpecialAmount.Checked = false;
                GC.Collect();
                MessageBox.Show("Операция завершена.");
            }
        }

        private void checkSpecialAmount_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkSpecialAmount.Checked)
                {
                    textSpecialAmount.Visible = true;
                    textSpecialAmount.Enabled = true;
                    comboPaymentType.Visible = false;
                    comboPaymentType.Enabled = false;
                    label1.Visible = false;
                }
                else
                {
                    textSpecialAmount.Visible = false;
                    textSpecialAmount.Enabled = false;
                    comboPaymentType.Visible = true;
                    comboPaymentType.Enabled = true;
                    label1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboPaymentType.SelectedIndex == 0)
                {
                    comboBonusType.Visible = false;
                    comboBonusType.Enabled = false;
                    label7.Visible = false;
                }
                else if (comboPaymentType.SelectedIndex == 1)
                {
                    comboBonusType.Visible = true;
                    comboBonusType.Enabled = true;
                    label7.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при изменении типа оплаты: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPostInComboBox();
            LoadEmployees();
            LoadBonusTypes();
        }
    }
}
