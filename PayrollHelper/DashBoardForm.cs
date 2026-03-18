using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using PayrollHelper.Models;

namespace PayrollHelper
{
    public partial class DashBoardForm : Form
    {
        public DashBoardForm()
        {
            InitializeComponent();

            this.MouseClick += DashboardForm_MouseClick;

            tabPayments.Text = "Выплаты сотрудникам";
            tabNewEmployee.Text = "Добавление сотрудника";
            if (tabPositions != null) tabPositions.Text = "Должности";

            this.KeyPreview = true;
            this.KeyDown += LoginForm_KeyDown;

            // Защита от ошибок Дизайнера Visual Studio
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            tabControl.MouseClick += DashboardForm_MouseClick;
            tabPayments.MouseClick += DashboardForm_MouseClick;
            tabNewEmployee.MouseClick += DashboardForm_MouseClick;
            tabPositions.MouseClick += DashboardForm_MouseClick;

            // ИНИЦИАЛИЗАЦИЯ ВЫПЛАТ
            comboPaymentType.Items.Clear();
            comboPaymentType.Items.Add("Зарплата");
            comboPaymentType.Items.Add("Премия");
            comboPaymentType.SelectedIndex = 0;

            comboBonusType.Visible = false;
            lblBonusType.Visible = false; 
            textSpecialAmount.Visible = false;

            // ПРИВЯЗКА СОБЫТИЙ ВАЛИДАЦИИ (ВЫПЛАТЫ И СОТРУДНИКИ)
            textSpecialAmount.KeyPress += textSpecialAmount_KeyPress;
            textSpecialAmount.TextChanged += textSpecialAmount_TextChanged;
            textSpecialAmount.Leave += (s, e) => { if (checkSpecialAmount.Checked) HighlightInvalidField(textSpecialAmount, IsValidSpecialAmount(textSpecialAmount.Text)); };
            textSpecialAmount.Enter += (s, e) => HighlightInvalidField(textSpecialAmount, true);

            textFullName.KeyPress += textFullName_KeyPress;
            textFullName.TextChanged += textFullName_TextChanged;
            textFullName.Leave += (s, e) => HighlightInvalidField(textFullName, IsValidFullName(textFullName.Text));
            textFullName.Enter += (s, e) => HighlightInvalidField(textFullName, true);

            textAddress.Leave += (s, e) => HighlightInvalidField(textAddress, IsValidAddress(textAddress.Text));
            textAddress.Enter += (s, e) => HighlightInvalidField(textAddress, true);

            maskedPhoneNumber.Leave += (s, e) => HighlightInvalidField(maskedPhoneNumber, IsValidPhone(maskedPhoneNumber));
            maskedPhoneNumber.Enter += (s, e) => HighlightInvalidField(maskedPhoneNumber, true);

            // ПРИВЯЗКА СОБЫТИЙ ВКЛАДКИ ДОЛЖНОСТЕЙ
            if (btnAddPosition != null) btnAddPosition.Click += btnAddPosition_Click;
            if (txtPositionName != null)
            {
                txtPositionName.Leave += (s, e) => HighlightInvalidField(txtPositionName, txtPositionName.Text.Trim().Length >= 3);
                txtPositionName.Enter += (s, e) => HighlightInvalidField(txtPositionName, true);
            }
            if (txtPositionDescription != null)
            {
                txtPositionDescription.Leave += (s, e) => HighlightInvalidField(txtPositionDescription, txtPositionDescription.Text.Trim().Length >= 5);
                txtPositionDescription.Enter += (s, e) => HighlightInvalidField(txtPositionDescription, true);
            }

            try
            {
                LoadInitialData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при инициализации: {ex.Message}");
            }
        }

        private void DashboardForm_MouseClick(object sender, MouseEventArgs e)
        {
            // Проверяем, что клик был левой кнопкой мыши
            if (e.Button == MouseButtons.Left)
            {
                // Снимаем фокус со всех элементов
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

        private void LoadInitialData()
        {
            if (Program.dbContext == null) return;
            LoadEmployees();
            LoadBonusTypes();
            LoadPostInComboBox();
        }

        // ==========================================================
        // ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ (ВАЛИДАЦИЯ И ПОДСВЕТКА)
        // ==========================================================

        private void HighlightInvalidField(Control control, bool isValid)
        {
            control.BackColor = isValid ? Color.White : Color.LightPink;
        }

        private void ClearHighlights()
        {
            HighlightInvalidField(textFullName, true);
            HighlightInvalidField(maskedPhoneNumber, true);
            HighlightInvalidField(textAddress, true);
            HighlightInvalidField(textSpecialAmount, true);
            if (txtPositionName != null) HighlightInvalidField(txtPositionName, true);
            if (txtPositionDescription != null) HighlightInvalidField(txtPositionDescription, true);
        }

        private bool IsValidFullName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            name = name.Trim();
            return name.Length >= 5 && name.Length <= 100 && Regex.IsMatch(name, @"^[a-zA-Zа-яА-ЯёЁ\s-]+$");
        }

        private bool IsValidPhone(MaskedTextBox mtb)
        {
            return mtb.MaskFull;
        }

        private bool IsValidAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address)) return false;
            address = address.Trim();
            return address.Length >= 10 && address.Length <= 150;
        }

        private bool IsValidSpecialAmount(string amount)
        {
            if (string.IsNullOrWhiteSpace(amount)) return false;
            string normalized = amount.Replace(',', '.');
            return double.TryParse(normalized, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _);
        }

        private void textFullName_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void textFullName_TextChanged(object? sender, EventArgs e)
        {
            string input = textFullName.Text;
            string filtered = Regex.Replace(input, @"[^a-zA-Zа-яА-ЯёЁ\s-]", "");
            if (input != filtered)
            {
                int cursor = textFullName.SelectionStart;
                textFullName.Text = filtered;
                textFullName.SelectionStart = Math.Min(cursor, filtered.Length);
            }
        }

        private void textSpecialAmount_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
                return;
            }
            if ((e.KeyChar == '.' || e.KeyChar == ',') && (textSpecialAmount.Text.Contains(".") || textSpecialAmount.Text.Contains(",")))
            {
                e.Handled = true;
            }
        }

        private void textSpecialAmount_TextChanged(object? sender, EventArgs e)
        {
            string input = textSpecialAmount.Text;
            string filtered = Regex.Replace(input, @"[^0-9.,]", "");
            int firstSep = filtered.IndexOfAny(new char[] { '.', ',' });
            if (firstSep != -1)
            {
                string head = filtered.Substring(0, firstSep + 1);
                string tail = filtered.Substring(firstSep + 1).Replace(".", "").Replace(",", "");
                filtered = head + tail;
            }
            if (input != filtered)
            {
                int cursor = textSpecialAmount.SelectionStart;
                textSpecialAmount.Text = filtered;
                textSpecialAmount.SelectionStart = Math.Min(cursor, filtered.Length);
            }
        }

        // ==========================================================
        // Вкладка "ДОЛЖНОСТИ" (tabPositions)
        // ==========================================================

        private void LoadPositionsToList()
        {
            try
            {
                if (lstPositions == null) return;
                var positions = Program.dbContext.Positions.OrderBy(p => p.Name).ToList();
                lstPositions.Items.Clear();
                foreach (var pos in positions)
                {
                    lstPositions.Items.Add($"{pos.Name} ({(pos.Status == "active" ? "Активна" : "Неактивна")})");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки списка должностей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddPosition_Click(object? sender, EventArgs e)
        {
            try
            {
                ClearHighlights();
                bool hasErrors = false;
                StringBuilder errors = new StringBuilder("Пожалуйста, исправьте следующие ошибки:\n\n");

                string posName = txtPositionName.Text.Trim();
                string posDesc = txtPositionDescription.Text.Trim();

                if (posName.Length < 3)
                {
                    HighlightInvalidField(txtPositionName, false);
                    errors.AppendLine("- Название должности: минимум 3 символа.");
                    hasErrors = true;
                }

                if (posDesc.Length < 5)
                {
                    HighlightInvalidField(txtPositionDescription, false);
                    errors.AppendLine("- Описание: минимум 5 символов.");
                    hasErrors = true;
                }

                if (hasErrors)
                {
                    MessageBox.Show(errors.ToString(), "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Проверка на уникальность названия должности
                if (Program.dbContext.Positions.Any(p => p.Name.ToLower() == posName.ToLower()))
                {
                    HighlightInvalidField(txtPositionName, false);
                    MessageBox.Show($"Должность '{posName}' уже существует в базе данных.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var newPos = new Position
                {
                    Name = posName,
                    Description = posDesc,
                    Status = chkPositionActive.Checked ? "active" : "inactive",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                Program.dbContext.Positions.Add(newPos);
                Program.dbContext.SaveChanges();

                MessageBox.Show($"Должность '{posName}' успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Очистка полей
                txtPositionName.Clear();
                txtPositionDescription.Clear();
                chkPositionActive.Checked = true;

                // Обновляем списки
                LoadPositionsToList();
                LoadPostInComboBox(); 
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (ex.InnerException != null) msg += "\n\nПодробности: " + ex.InnerException.Message;
                MessageBox.Show($"Ошибка при добавлении должности: {msg}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================================
        // Вкладка "НОВЫЙ СОТРУДНИК" (TabPage2)
        // ==========================================================

        private void LoadPostInComboBox()
        {
            try
            {
                // Показываем только активные должности для новых сотрудников
                var positions = Program.dbContext.Positions
                    .Where(p => p.Status == "active")
                    .OrderBy(p => p.Name)
                    .Select(p => p.Name).ToList();

                comboPosition.Items.Clear();
                foreach (var name in positions)
                {
                    if (!string.IsNullOrEmpty(name)) comboPosition.Items.Add(name);
                }
                if (comboPosition.Items.Count > 0) comboPosition.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки должностей: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAddEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                ClearHighlights();
                bool hasErrors = false;
                StringBuilder errorMsg = new StringBuilder("Пожалуйста, исправьте ошибки в следующих полях:\n\n");

                if (!IsValidFullName(textFullName.Text))
                {
                    HighlightInvalidField(textFullName, false);
                    errorMsg.AppendLine("- ФИО: от 5 до 100 символов, только буквы.");
                    hasErrors = true;
                }

                if (!IsValidPhone(maskedPhoneNumber))
                {
                    HighlightInvalidField(maskedPhoneNumber, false);
                    errorMsg.AppendLine("- Телефон: номер должен быть заполнен полностью.");
                    hasErrors = true;
                }

                if (!IsValidAddress(textAddress.Text))
                {
                    HighlightInvalidField(textAddress, false);
                    errorMsg.AppendLine("- Адрес: от 10 до 150 символов.");
                    hasErrors = true;
                }

                if (hasErrors)
                {
                    MessageBox.Show(errorMsg.ToString(), "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ПРАВИЛЬНОЕ ИЗВЛЕЧЕНИЕ НОМЕРА ТЕЛЕФОНА
                string allDigits = Regex.Replace(maskedPhoneNumber.Text, @"\D", "");
                string cleanDigits = allDigits;
                if (allDigits.Length > 10 && allDigits.StartsWith("7"))
                {
                    cleanDigits = allDigits.Substring(1);
                }
                string formattedPhone = "+7" + cleanDigits;

                // Проверка на уникальность номера телефона
                bool exists = Program.dbContext.Employees.Any(e => e.PhoneNumber == formattedPhone);
                if (exists)
                {
                    HighlightInvalidField(maskedPhoneNumber, false);
                    MessageBox.Show($"Сотрудник с номером {formattedPhone} уже существует в базе данных.", "Ошибка уникальности", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string selectedPosName = comboPosition.SelectedItem?.ToString();
                var position = Program.dbContext.Positions.FirstOrDefault(p => p.Name == selectedPosName);
                if (position == null)
                {
                    MessageBox.Show("Выбранная должность не найдена.");
                    return;
                }

                var newEmployee = new Employee
                {
                    EmployeeName = textFullName.Text.Trim(),
                    PostNumber = position.Id,
                    Insurance = checkInsurance.Checked,
                    PhoneNumber = formattedPhone,
                    Address = textAddress.Text.Trim()
                };

                Program.dbContext.Employees.Add(newEmployee);
                Program.dbContext.SaveChanges();

                MessageBox.Show("Сотрудник успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                textFullName.Clear();
                maskedPhoneNumber.Clear();
                textAddress.Clear();
                checkInsurance.Checked = false;

                LoadEmployees();
            }
            catch (Exception ex)
            {
                if (ex.InnerException?.Message.Contains("23505") == true || ex.Message.Contains("23505"))
                {
                    HighlightInvalidField(maskedPhoneNumber, false);
                    MessageBox.Show("Сотрудник с таким номером телефона уже существует.", "Ошибка уникальности", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string msg = ex.Message;
                    if (ex.InnerException != null) msg += "\n\nПодробности: " + ex.InnerException.Message;
                    MessageBox.Show($"Ошибка при добавлении сотрудника: {msg}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ==========================================================
        // Вкладка "ВЫПЛАТЫ" (TabPage1)
        // ==========================================================

        private void LoadEmployees()
        {
            try
            {
                var employees = Program.dbContext.Employees.Select(e => e.EmployeeName).ToList();
                comboEmployee.Items.Clear();
                foreach (var name in employees)
                {
                    if (!string.IsNullOrEmpty(name)) comboEmployee.Items.Add(name);
                }
                if (comboEmployee.Items.Count > 0) comboEmployee.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки сотрудников: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBonusTypes()
        {
            try
            {
                var bonuses = Program.dbContext.SalaryAndBonuses
                    .Where(s => EF.Functions.ILike(s.PaymentType, "Премия%"))
                    .Select(s => s.PaymentType).ToList();

                comboBonusType.Items.Clear();
                foreach (var type in bonuses)
                {
                    if (!string.IsNullOrEmpty(type)) comboBonusType.Items.Add(type);
                }
                if (comboBonusType.Items.Count > 0) comboBonusType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки премий: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAddPayment_Click(object sender, EventArgs e)
        {
            try
            {
                string empName = comboEmployee.Text;
                var employee = Program.dbContext.Employees
                    .Include(e => e.PostNumberNavigation)
                    .FirstOrDefault(e => e.EmployeeName == empName);

                if (employee == null)
                {
                    MessageBox.Show("Сотрудник не выбран.");
                    return;
                }

                DateOnly paymentDate = DateOnly.FromDateTime(DateTime.Now);

                if (checkSpecialAmount.Checked)
                {
                    if (!IsValidSpecialAmount(textSpecialAmount.Text))
                    {
                        HighlightInvalidField(textSpecialAmount, false);
                        MessageBox.Show("Введите корректную числовую сумму (например: 1500,50).", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    double.TryParse(textSpecialAmount.Text.Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double specialAmount);

                    var ndflTax = Program.dbContext.Taxations.FirstOrDefault(t => EF.Functions.ILike(t.TaxType, "НДФЛ"));
                    double taxRate = (double)(ndflTax?.TaxRate ?? 0);
                    double finalAmount = Math.Round(specialAmount - (specialAmount * taxRate / 100), 2);

                    var p = new Payment { 
                        PaymentType = "Специальная сумма", 
                        PaymentAmount = (decimal)finalAmount, 
                        PaymentDate = paymentDate, 
                        EmployeeId = employee.EmployeeId 
                    };
                    Program.dbContext.Payments.Add(p);
                    Program.dbContext.SaveChanges();
                    MessageBox.Show($"Выплата специальной суммы ({finalAmount} руб.) успешно проведена.");
                }
                else
                {
                    if (comboPaymentType.Text == "Зарплата")
                    {
                        string postName = employee.PostNumberNavigation?.Name;
                        var salaryInfo = Program.dbContext.SalaryAndBonuses
                            .FirstOrDefault(s => EF.Functions.ILike(s.PaymentType, $"%{postName}%"))
                            ?? Program.dbContext.SalaryAndBonuses.FirstOrDefault(s => EF.Functions.ILike(s.PaymentType, "Оклад"));

                        if (salaryInfo == null)
                        {
                            MessageBox.Show("Ставка оклада не найдена в базе данных.");
                            return;
                        }

                        var ndflTax = Program.dbContext.Taxations.FirstOrDefault(t => EF.Functions.ILike(t.TaxType, "НДФЛ"));
                        double totalTaxRate = (double)(ndflTax?.TaxRate ?? 0);

                        if (employee.Insurance == true)
                        {
                            var insTax = Program.dbContext.Taxations.FirstOrDefault(t => EF.Functions.ILike(t.TaxType, "%страхов%"));
                            totalTaxRate += (double)(insTax?.TaxRate ?? 0);
                        }

                        double baseAmount = (double)(salaryInfo.DefaultAmount ?? 0);
                        double finalAmount = Math.Round(baseAmount - (baseAmount * totalTaxRate / 100), 2);

                        var p = new Payment { 
                            PaymentType = "Зарплата", 
                            PaymentAmount = (decimal)finalAmount, 
                            PaymentDate = paymentDate, 
                            EmployeeId = employee.EmployeeId 
                        };
                        Program.dbContext.Payments.Add(p);
                        Program.dbContext.SaveChanges();
                        MessageBox.Show($"Зарплата ({finalAmount} руб.) начислена сотруднику {empName}.");
                    }
                    else if (comboPaymentType.Text == "Премия")
                    {
                        var bonusInfo = Program.dbContext.SalaryAndBonuses.FirstOrDefault(s => s.PaymentType == comboBonusType.Text);
                        if (bonusInfo == null)
                        {
                            MessageBox.Show("Вид премии не найден.");
                            return;
                        }

                        var p = new Payment { 
                            PaymentType = "Премия", 
                            PaymentAmount = bonusInfo.DefaultAmount ?? 0, 
                            PaymentDate = paymentDate, 
                            EmployeeId = employee.EmployeeId 
                        };
                        Program.dbContext.Payments.Add(p);
                        Program.dbContext.SaveChanges();
                        MessageBox.Show($"Премия ({bonusInfo.DefaultAmount ?? 0} руб.) успешно начислена.");
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (ex.InnerException != null) msg += "\n\nПодробности: " + ex.InnerException.Message;
                MessageBox.Show($"Ошибка при выполнении выплаты: {msg}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                textSpecialAmount.Clear();
                checkSpecialAmount.Checked = false;
            }
        }

        // ==========================================================
        // УПРАВЛЕНИЕ ИНТЕРФЕЙСОМ
        // ==========================================================

        private void checkSpecialAmount_CheckedChanged(object sender, EventArgs e)
        {
            bool isSpecial = checkSpecialAmount.Checked;
            textSpecialAmount.Visible = isSpecial;
            textSpecialAmount.Enabled = isSpecial;
            comboPaymentType.Visible = !isSpecial;
            comboPaymentType.Enabled = !isSpecial;
            lblPaymentType.Visible = !isSpecial;

            if (isSpecial)
            {
                comboBonusType.Visible = false;
                lblBonusType.Visible = false;
            }
            else
            {
                comboPaymentType_SelectedIndexChanged(null, null);
            }
        }

        private void comboPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isBonus = comboPaymentType.Text == "Премия";
            comboBonusType.Visible = isBonus;
            comboBonusType.Enabled = isBonus;
            lblBonusType.Visible = isBonus;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearHighlights();
            if (tabControl.SelectedIndex == 0) // Выплаты
            {
                LoadEmployees();
                LoadBonusTypes();
            }
            else if (tabControl.SelectedIndex == 1) // Новый сотрудник
            {
                LoadPostInComboBox();
            }
            else if (tabControl.SelectedIndex == 2) // Должности
            {
                LoadPositionsToList();
            }
        }
    }
}
