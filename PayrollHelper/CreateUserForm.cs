using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows.Forms;
using System.Drawing;

namespace PayrollHelper
{
    public partial class CreateUserForm : Form
    {
        private const string UsersFilePath = "users.json";

        public CreateUserForm()
        {
            InitializeComponent();

            // Инициализация списка ролей на русском
            cmbRole.Items.Add("Администратор");
            cmbRole.Items.Add("Пользователь");
            cmbRole.SelectedIndex = 1;

            btnCreate.Click += btnCreate_Click;
            
            // Снятие фокуса только при клике на саму форму
            this.MouseClick += Form_MouseClick;

            // Привязка событий валидации
            txtUsername.Enter += txtUsername_Enter;
            txtUsername.Leave += txtUsername_Leave;
            txtPassword.Enter += txtPassword_Enter;
            txtPassword.Leave += txtPassword_Leave;
        }

        private void HighlightInvalidField(Control control, bool isValid)
        {
            control.BackColor = isValid ? Color.White : Color.LightPink;
        }

        private bool ValidateUsername()
        {
            string username = txtUsername.Text.Trim();
            bool isValid = !string.IsNullOrWhiteSpace(username) && username.Length >= 3;
            HighlightInvalidField(txtUsername, isValid);
            return isValid;
        }

        private bool ValidatePassword()
        {
            string password = txtPassword.Text.Trim();
            bool isValid = !string.IsNullOrWhiteSpace(password) && password.Length >= 4;
            HighlightInvalidField(txtPassword, isValid);
            return isValid;
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            HighlightInvalidField(txtUsername, true);
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            ValidateUsername();
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            HighlightInvalidField(txtPassword, true);
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            ValidatePassword();
        }

        private void Form_MouseClick(object sender, MouseEventArgs e)
        {
            // Снимаем фокус только если кликнули по форме, а не по контролам
            if (e.Button == MouseButtons.Left)
            {
                this.ActiveControl = null;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Text.Trim();

                // Проверки с конкретными сообщениями через методы валидации
                if (!ValidateUsername())
                {
                    if (string.IsNullOrWhiteSpace(username))
                        MessageBox.Show("Логин не может быть пустым", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("Логин должен содержать не менее 3 символов", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidatePassword())
                {
                    if (string.IsNullOrWhiteSpace(password))
                        MessageBox.Show("Пароль не может быть пустым", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        MessageBox.Show("Пароль должен содержать не менее 4 символов", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Маппинг роли: Русское -> Английское (для JSON)
                string role = cmbRole.SelectedItem?.ToString() == "Администратор" ? "Administrator" : "User";

                UserRoot userRoot;
                if (File.Exists(UsersFilePath))
                {
                    string json = File.ReadAllText(UsersFilePath);
                    userRoot = JsonSerializer.Deserialize<UserRoot>(json) ?? new UserRoot();
                }
                else
                {
                    userRoot = new UserRoot();
                }

                if (userRoot.Users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
                {
                    HighlightInvalidField(txtUsername, false);
                    MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var newUser = new UserEntry
                {
                    Username = username,
                    PasswordHash = LoginForm.GetHash(password),
                    Role = role
                };

                userRoot.Users.Add(newUser);

                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };

                File.WriteAllText(UsersFilePath, JsonSerializer.Serialize(userRoot, options));

                MessageBox.Show($"Пользователь '{username}' успешно создан!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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