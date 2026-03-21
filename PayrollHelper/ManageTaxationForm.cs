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
    public partial class ManageTaxationForm : Form
    {
        public ManageTaxationForm()
        {
            InitializeComponent();

            // Настройки формы
            this.KeyPreview = true;
            this.AcceptButton = btnAddTax;
            this.KeyDown += ManageTaxationForm_KeyDown;
            this.MouseClick += Form_MouseClick;

            // Снятие фокуса при клике на пустые области
            foreach (Control c in this.Controls) c.MouseClick += Form_MouseClick;
            grpAddTax.MouseClick += Form_MouseClick;
            grpTaxList.MouseClick += Form_MouseClick;

            // Привязка событий валидации НАЗВАНИЯ
            txtTaxName.KeyPress += txtTaxName_KeyPress;
            txtTaxName.TextChanged += txtTaxName_TextChanged;
            txtTaxName.Leave += (s, e) => HighlightInvalidField(txtTaxName, IsValidTaxName(txtTaxName.Text));
            txtTaxName.Enter += (s, e) => HighlightInvalidField(txtTaxName, true);

            // Привязка событий валидации СТАВКИ
            txtTaxRate.KeyPress += txtTaxRate_KeyPress;
            txtTaxRate.TextChanged += txtTaxRate_TextChanged;
            txtTaxRate.Leave += (s, e) => HighlightInvalidField(txtTaxRate, IsValidTaxRate(txtTaxRate.Text));
            txtTaxRate.Enter += (s, e) => HighlightInvalidField(txtTaxRate, true);

            txtTaxDescription.Enter += (s, e) => HighlightInvalidField(txtTaxDescription, true);

            btnAddTax.Click += btnAddTax_Click;

            // Начальная загрузка
            LoadTaxes();
        }

        private void ManageTaxationForm_KeyDown(object? sender, KeyEventArgs e)
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

        private bool IsValidTaxName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            name = name.Trim();
            // Минимум 3 символа, только буквы, пробелы и дефисы
            return name.Length >= 3 && name.Length <= 50 && Regex.IsMatch(name, @"^[a-zA-Zа-яА-ЯёЁ\s-]+$");
        }

        private bool IsValidTaxRate(string rateStr)
        {
            if (string.IsNullOrWhiteSpace(rateStr)) return false;
            string normalized = rateStr.Replace(',', '.');
            if (double.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out double rate))
            {
                return rate > 0 && rate <= 100;
            }
            return false;
        }

        // Обработка ввода НАЗВАНИЯ (блокировка цифр и спецсимволов)
        private void txtTaxName_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            // Разрешить только буквы, пробел и дефис
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        // Фильтрация НАЗВАНИЯ при вставке (Ctrl+V)
        private void txtTaxName_TextChanged(object? sender, EventArgs e)
        {
            string input = txtTaxName.Text;
            string filtered = Regex.Replace(input, @"[^a-zA-Zа-яА-ЯёЁ\s-]", "");
            if (input != filtered)
            {
                int cursor = txtTaxName.SelectionStart;
                txtTaxName.Text = filtered;
                txtTaxName.SelectionStart = Math.Min(cursor, filtered.Length);
            }
        }

        // Обработка ввода СТАВКИ (только числа и один разделитель)
        private void txtTaxRate_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
                return;
            }
            if ((e.KeyChar == '.' || e.KeyChar == ',') && (txtTaxRate.Text.Contains(".") || txtTaxRate.Text.Contains(",")))
            {
                e.Handled = true;
            }
        }

        // Фильтрация СТАВКИ при вставке
        private void txtTaxRate_TextChanged(object? sender, EventArgs e)
        {
            string input = txtTaxRate.Text;
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
                int cursor = txtTaxRate.SelectionStart;
                txtTaxRate.Text = filtered;
                txtTaxRate.SelectionStart = Math.Min(cursor, filtered.Length);
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
                var taxes = Program.dbContext.Taxations.OrderBy(t => t.TaxType).ToList();
                lstTaxes.Items.Clear();
                foreach (var tax in taxes)
                {
                    string rateStr = tax.TaxRate.ToString("0.##").Replace('.', ',');
                    lstTaxes.Items.Add($"{tax.TaxType} — {rateStr}%");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки налогов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddTax_Click(object? sender, EventArgs e)
        {
            try
            {
                string name = txtTaxName.Text.Trim();
                string rateStr = txtTaxRate.Text.Trim();
                string desc = txtTaxDescription.Text.Trim();

                bool nameOk = IsValidTaxName(name);
                bool rateOk = IsValidTaxRate(rateStr);

                HighlightInvalidField(txtTaxName, nameOk);
                HighlightInvalidField(txtTaxRate, rateOk);

                if (!nameOk || !rateOk)
                {
                    MessageBox.Show("Пожалуйста, проверьте правильность заполнения полей:\n- Название: от 3 символов, без цифр\n- Ставка: число от 0.01 до 100",
                                    "Ошибка валидации", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (Program.dbContext.Taxations.Any(t => t.TaxType.ToLower() == name.ToLower()))
                {
                    HighlightInvalidField(txtTaxName, false);
                    MessageBox.Show($"Налог с названием '{name}' уже существует.", "Дубликат", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal rate = Convert.ToDecimal(rateStr.Replace(',', '.'), CultureInfo.InvariantCulture);

                var newTax = new Taxation
                {
                    TaxType = name,
                    TaxRate = rate,
                    Description = string.IsNullOrEmpty(desc) ? null : desc
                };

                Program.dbContext.Taxations.Add(newTax);
                Program.dbContext.SaveChanges();

                MessageBox.Show($"Налог '{name}' успешно добавлен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtTaxName.Clear();
                txtTaxRate.Clear();
                txtTaxDescription.Clear();
                this.ActiveControl = null;

                LoadTaxes();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (ex.InnerException != null) msg += "\n\nПодробности: " + ex.InnerException.Message;
                MessageBox.Show($"Ошибка при сохранении налога: {msg}", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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