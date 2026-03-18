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
    public partial class DashBoardForm : Form
    {
        public DashBoardForm()
        {
            InitializeComponent();

            tabPayments.Text = "Выплаты сотрудникам";
            tabNewEmployee.Text = "Добавление сотрудника";

            this.KeyPreview = true;
            this.KeyDown += LoginForm_KeyDown;

            // Защита от ошибок Дизайнера Visual Studio
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;

            // ИНИЦИАЛИЗАЦИЯ КОМБОБОКСА ТИПОВ ВЫПЛАТ
            comboPaymentType.Items.Clear();
            comboPaymentType.Items.Add("Зарплата");
            comboPaymentType.Items.Add("Премия");
            comboPaymentType.SelectedIndex = 0;

            // Настройка начальной видимости элементов на вкладке "Выплаты"
            comboBonusType.Visible = false;
            lblBonusType.Visible = false; // label2 - это "Вид премии", а не label7!
            textSpecialAmount.Visible = false;

            try
            {
                // Первичная загрузка данных
                LoadInitialData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при инициализации: {ex.Message}");
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
        // Вкладка "НОВЫЙ СОТРУДНИК" (TabPage2)
        // ==========================================================

        private void LoadPostInComboBox()
        {
            try
            {
                var positions = Program.dbContext.Positions
                    .Select(p => p.Name)
                    .ToList();

                comboPosition.Items.Clear();
                foreach (var name in positions)
                {
                    if (!string.IsNullOrEmpty(name))
                        comboPosition.Items.Add(name);
                }

                if (comboPosition.Items.Count > 0)
                    comboPosition.SelectedIndex = 0;
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
                // Валидация
                if (string.IsNullOrWhiteSpace(textFullName.Text))
                {
                    MessageBox.Show("Введите ФИО сотрудника.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string selectedPosName = comboPosition.SelectedItem?.ToString();
                var position = Program.dbContext.Positions.FirstOrDefault(p => p.Name == selectedPosName);
                if (position == null)
                {
                    MessageBox.Show("Выбранная должность не найдена.");
                    return;
                }

                // Создание через EF Core
                var newEmployee = new Employee
                {
                    EmployeeName = textFullName.Text,
                    PostNumber = position.Id,
                    Insurance = checkInsurance.Checked,
                    PhoneNumber = textPhoneNumber.Text,
                    Address = textAddress.Text
                };

                Program.dbContext.Employees.Add(newEmployee);
                Program.dbContext.SaveChanges();

                MessageBox.Show("Сотрудник успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Очистка полей
                textFullName.Clear();
                textPhoneNumber.Clear();
                textAddress.Clear();
                checkInsurance.Checked = false;

                // Обновляем список сотрудников на вкладке выплат
                LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении сотрудника: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================================
        // Вкладка "ВЫПЛАТЫ" (TabPage1)
        // ==========================================================

        private void LoadEmployees()
        {
            try
            {
                var employees = Program.dbContext.Employees
                    .Select(e => e.EmployeeName)
                    .ToList();

                comboEmployee.Items.Clear();
                foreach (var name in employees)
                {
                    if (!string.IsNullOrEmpty(name))
                        comboEmployee.Items.Add(name);
                }

                if (comboEmployee.Items.Count > 0)
                    comboEmployee.SelectedIndex = 0;
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
                    .Select(s => s.PaymentType)
                    .ToList();

                comboBonusType.Items.Clear();
                foreach (var type in bonuses)
                {
                    if (!string.IsNullOrEmpty(type))
                        comboBonusType.Items.Add(type);
                }

                if (comboBonusType.Items.Count > 0)
                    comboBonusType.SelectedIndex = 0;
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
                    // СЦЕНАРИЙ: ОСОБАЯ СУММА
                    if (!double.TryParse(textSpecialAmount.Text, out double specialAmount))
                    {
                        MessageBox.Show("Введите корректную числовую сумму.");
                        return;
                    }

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
                        // СЦЕНАРИЙ: ЗАРПЛАТА
                        string postName = employee.PostNumberNavigation?.Name;
                        var salaryInfo = Program.dbContext.SalaryAndBonuses
                            .FirstOrDefault(s => EF.Functions.ILike(s.PaymentType, $"%{postName}%"))
                            ?? Program.dbContext.SalaryAndBonuses.FirstOrDefault(s => EF.Functions.ILike(s.PaymentType, "Оклад"));

                        if (salaryInfo == null)
                        {
                            MessageBox.Show("Ставка оклада не найдена в базе данных.");
                            return;
                        }

                        // Налоги
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
                        // СЦЕНАРИЙ: ПРЕМИЯ
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
                MessageBox.Show($"Ошибка при выполнении выплаты: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            
            // Скрываем/показываем обычный выбор типа оплаты
            comboPaymentType.Visible = !isSpecial;
            comboPaymentType.Enabled = !isSpecial;
            lblPaymentType.Visible = !isSpecial;

            if (isSpecial)
            {
                comboBonusType.Visible = false;
                lblBonusType.Visible = false; // Используем label2 (Вид премии)
            }
            else
            {
                // Восстанавливаем видимость премии, если она была выбрана до этого
                comboPaymentType_SelectedIndexChanged(null, null);
            }
        }

        private void comboPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Показываем выбор вида премии только если выбрана "Премия"
            bool isBonus = comboPaymentType.Text == "Премия";
            comboBonusType.Visible = isBonus;
            comboBonusType.Enabled = isBonus;
            lblBonusType.Visible = isBonus; // Используем label2 (Вид премии)
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Обновляем данные в зависимости от активной вкладки
            if (tabControl.SelectedIndex == 0) // Выплаты
            {
                LoadEmployees();
                LoadBonusTypes();
            }
            else if (tabControl.SelectedIndex == 1) // Новый сотрудник
            {
                LoadPostInComboBox();
            }
        }
    }
}
