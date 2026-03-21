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
        private List<Taxation> _allTaxes = new List<Taxation>();

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
            grpTaxSelection.MouseClick += Form_MouseClick;

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
            lstPayments.SelectedIndexChanged += lstPayments_SelectedIndexChanged;

            // Настройка списка налогов
            clbTaxes.CheckOnClick = true;

            // Начальная загрузка
            LoadTaxes();
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

        private void txtPaymentName_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

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

        private void LoadTaxes()
        {
            try
            {
                if (Program.dbContext == null) return;
                _allTaxes = Program.dbContext.Taxations.OrderBy(t => t.TaxType).ToList();
                clbTaxes.Items.Clear();
                foreach (var tax in _allTaxes)
                {
                    clbTaxes.Items.Add($"{tax.TaxType} — {tax.TaxRate}%");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки налогов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPayments()
        {
            try
            {
                if (Program.dbContext == null) return;
                var payments = Program.dbContext.SalaryAndBonuses.OrderBy(p => p.PaymentType).ToList();
                lstPayments.Items.Clear();
                foreach (var p in payments)
                {
                    // Формат: "Название — сумма" (с запятой, без лишних нулей)
                    string amountStr = p.DefaultAmount?.ToString("G29", new CultureInfo("ru-RU")) ?? "0";
                    lstPayments.Items.Add($"{p.PaymentType} — {amountStr}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки выплат: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lstPayments_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (lstPayments.SelectedItem == null) return;

            try
            {
                string selectedText = lstPayments.SelectedItem.ToString() ?? "";
                string paymentName = selectedText.Split(" — ")[0];

                var payment = Program.dbContext.SalaryAndBonuses
                    .Include(p => p.Taxations)
                    .FirstOrDefault(p => p.PaymentType == paymentName);

                if (payment != null)
                {
                    txtPaymentName.Text = payment.PaymentType;
                    txtDefaultAmount.Text = payment.DefaultAmount?.ToString(new CultureInfo("ru-RU")) ?? "";
                    txtPaymentDescription.Text = payment.Description ?? "";

                    // Снимаем все галочки
                    for (int i = 0; i < clbTaxes.Items.Count; i++)
                        clbTaxes.SetItemChecked(i, false);

                    // Отмечаем привязанные налоги
                    foreach (var tax in payment.Taxations)
                    {
                        int index = _allTaxes.FindIndex(t => t.Id == tax.Id);
                        if (index != -1)
                        {
                            clbTaxes.SetItemChecked(index, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выборе выплаты: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddPayment_Click(object? sender, EventArgs e)
        {
            using var transaction = Program.dbContext.Database.BeginTransaction();
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

                // Проверка уникальности
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
                    Amount = amount,
                    DefaultAmount = amount,
                    Description = string.IsNullOrEmpty(desc) ? null : desc
                };

                // Добавление связей с налогами
                for (int i = 0; i < clbTaxes.Items.Count; i++)
                {
                    if (clbTaxes.GetItemChecked(i))
                    {
                        var tax = _allTaxes[i];
                        newPayment.Taxations.Add(tax);
                    }
                }

                Program.dbContext.SalaryAndBonuses.Add(newPayment);
                Program.dbContext.SaveChanges();
                transaction.Commit();

                MessageBox.Show($"Выплата '{name}' успешно добавлена!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Очистка полей
                txtPaymentName.Clear();
                txtDefaultAmount.Clear();
                txtPaymentDescription.Clear();
                for (int i = 0; i < clbTaxes.Items.Count; i++)
                    clbTaxes.SetItemChecked(i, false);
                
                this.ActiveControl = null;

                LoadPayments();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                string msg = ex.Message;
                if (ex.InnerException != null) msg += "\n\nПодробности: " + ex.InnerException.Message;
                MessageBox.Show($"Ошибка при сохранении выплаты: {msg}", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*
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
        */
    }
}
