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
using System.Globalization;

namespace PayrollHelper
{
    public partial class ManagePaymentsForm : Form
    {
        public ManagePaymentsForm()
        {
            InitializeComponent();

            // Настройки формы
            this.KeyPreview = true;
            this.AcceptButton = btnAddPayment;
            this.KeyDown += ManagePaymentsForm_KeyDown;
            this.MouseClick += Form_MouseClick;

            // Снятие фокуса при клике на пустые области
            foreach (Control c in this.Controls) c.MouseClick += Form_MouseClick;
            grpAddPayment.MouseClick += Form_MouseClick;
            grpPaymentList.MouseClick += Form_MouseClick;

            // Привязка событий валидации НАЗВАНИЯ
            txtPaymentName.KeyPress += txtPaymentName_KeyPress;
            txtPaymentName.TextChanged += txtPaymentName_TextChanged;
            txtPaymentName.Leave += (s, e) => HighlightInvalidField(txtPaymentName, IsValidPaymentName(txtPaymentName.Text));
            txtPaymentName.Enter += (s, e) => HighlightInvalidField(txtPaymentName, true);

            // Привязка событий валидации СУММЫ
            txtDefaultAmount.KeyPress += txtDefaultAmount_KeyPress;
            txtDefaultAmount.TextChanged += txtDefaultAmount_TextChanged;
            txtDefaultAmount.Leave += (s, e) => HighlightInvalidField(txtDefaultAmount, IsValidAmount(txtDefaultAmount.Text));
            txtDefaultAmount.Enter += (s, e) => HighlightInvalidField(txtDefaultAmount, true);

            txtPaymentDescription.Enter += (s, e) => HighlightInvalidField(txtPaymentDescription, true);

            btnAddPayment.Click += btnAddPayment_Click;

            // Начальная загрузка
            LoadPayments();
        }

        private void ManagePaymentsForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void Form_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) this.ActiveControl = null;
        }

        // ==========================================================
        // ВАЛИДАЦИЯ И ПОДСВЕТКА
        // ==========================================================

        private void HighlightInvalidField(Control control, bool isValid)
        {
            control.BackColor = isValid ? Color.White : Color.LightPink;
        }

        private bool IsValidPaymentName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            name = name.Trim();
            // Минимум 3 символа, только буквы, пробелы и дефисы
            return name.Length >= 3 && name.Length <= 50 && Regex.IsMatch(name, @"^[a-zA-Zа-яА-ЯёЁ\s-]+$");
        }

        private bool IsValidAmount(string amountStr)
        {
            if (string.IsNullOrWhiteSpace(amountStr)) return false;
            string normalized = amountStr.Replace(',', '.');
            if (double.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out double amount))
            {
                return amount > 0;
            }
            return false;
        }

        // Обработка ввода НАЗВАНИЯ (блокировка цифр и спецсимволов)
        private void txtPaymentName_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            // Разрешить только буквы, пробел и дефис
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        // Фильтрация НАЗВАНИЯ при вставке (Ctrl+V)
        private void txtPaymentName_TextChanged(object? sender, EventArgs e)
        {
            string input = txtPaymentName.Text;
            string filtered = Regex.Replace(input, @"[^a-zA-Zа-яА-ЯёЁ\s-]", "");
            if (input != filtered)
            {
                int cursor = txtPaymentName.SelectionStart;
                txtPaymentName.Text = filtered;
                txtPaymentName.SelectionStart = Math.Min(cursor, filtered.Length);
            }
        }

        // Обработка ввода СУММЫ (только числа и один разделитель)
        private void txtDefaultAmount_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
                return;
            }
            if ((e.KeyChar == '.' || e.KeyChar == ',') && (txtDefaultAmount.Text.Contains(".") || txtDefaultAmount.Text.Contains(",")))
            {
                e.Handled = true;
            }
        }

        // Фильтрация СУММЫ при вставке
        private void txtDefaultAmount_TextChanged(object? sender, EventArgs e)
        {
            string input = txtDefaultAmount.Text;
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
                int cursor = txtDefaultAmount.SelectionStart;
                txtDefaultAmount.Text = filtered;
                txtDefaultAmount.SelectionStart = Math.Min(cursor, filtered.Length);
            }
        }

        // ==========================================================
        // ЛОГИКА РАБОТЫ С ДАННЫМИ
        // ==========================================================

        private void LoadPayments()
        {
            try
            {
                if (Program.dbContext == null) return;
                var payments = Program.dbContext.SalaryAndBonuses.OrderBy(p => p.PaymentType).ToList();
                lstPayments.Items.Clear();
                foreach (var p in payments)
                {
                    string amountStr = p.DefaultAmount?.ToString("0.##").Replace('.', ',') ?? "0";
                    lstPayments.Items.Add($"{p.PaymentType} — {amountStr}"); // Этой строки не хватало!
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки выплат: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddPayment_Click(object? sender, EventArgs e)
        {
            try
            {
                string name = txtPaymentName.Text.Trim();
                string amountStr = txtDefaultAmount.Text.Trim();
                string desc = txtPaymentDescription.Text.Trim();

                bool nameOk = IsValidPaymentName(name);
                bool amountOk = IsValidAmount(amountStr);

                HighlightInvalidField(txtPaymentName, nameOk);
                HighlightInvalidField(txtDefaultAmount, amountOk);

                if (!nameOk || !amountOk)
                {
                    MessageBox.Show("Пожалуйста, проверьте правильность заполнения полей:\n- Название: от 3 символов, без цифр\n- Сумма: положительное число",
                                    "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (Program.dbContext.SalaryAndBonuses.Any(p => p.PaymentType.ToLower() == name.ToLower()))
                {
                    HighlightInvalidField(txtPaymentName, false);
                    MessageBox.Show($"Выплата с названием '{name}' уже существует.", "Дубликат", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal amount = Convert.ToDecimal(amountStr.Replace(',', '.'), CultureInfo.InvariantCulture);

                var newPayment = new SalaryAndBonus
                {
                    PaymentType = name,
                    Amount = amount, // В модели это поле обязательное
                    DefaultAmount = amount, // Сумма по умолчанию
                    Description = string.IsNullOrEmpty(desc) ? null : desc
                };

                Program.dbContext.SalaryAndBonuses.Add(newPayment);
                Program.dbContext.SaveChanges();

                MessageBox.Show($"Выплата '{name}' успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtPaymentName.Clear();
                txtDefaultAmount.Clear();
                txtPaymentDescription.Clear();
                this.ActiveControl = null;

                LoadPayments();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (ex.InnerException != null) msg += "\n\nПодробности: " + ex.InnerException.Message;
                MessageBox.Show($"Ошибка при сохранении выплаты: {msg}", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
