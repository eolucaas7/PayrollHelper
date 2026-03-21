using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows.Forms;

namespace PayrollHelper
{
    public partial class LoginForm : Form
    {
        public static bool admin;
        private const string UsersFilePath = "users.json";

        public LoginForm()
        {
            InitializeComponent();
            InitializeUsersFile();

            this.KeyPreview = true;
            this.KeyDown += LoginForm_KeyDown;
            this.AcceptButton = this.loginButton;
            
            // Снятие фокуса только при клике на саму форму
            this.MouseClick += Form_MouseClick;

            llCreateUser.LinkClicked += llCreateUser_LinkClicked;
        }

        private void HighlightInvalidField(Control control, bool isValid)
        {
            control.BackColor = isValid ? Color.White : Color.LightPink;
        }

        private bool ValidateLoginField()
        {
            bool isValid = !string.IsNullOrWhiteSpace(usernameTextBox.Text) && usernameTextBox.Text != "Введите имя пользователя";
            HighlightInvalidField(usernameTextBox, isValid);
            return isValid;
        }

        private bool ValidatePasswordField()
        {
            bool isValid = !string.IsNullOrWhiteSpace(passwordTextBox.Text) && passwordTextBox.Text != "Введите пароль";
            HighlightInvalidField(passwordTextBox, isValid);
            return isValid;
        }

        private void llCreateUser_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var form = new CreateUserForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Обновление не требуется, так как данные в JSON
                }
            }
        }

        private void Form_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.ActiveControl = null;
                // Принудительно снимаем фокус с поля пароля
                if (passwordTextBox.Focused)
                {
                    passwordTextBox.Enabled = false;
                    passwordTextBox.Enabled = true;
                }
            }
        }

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        private void InitializeUsersFile()
        {
            if (!File.Exists(UsersFilePath))
            {
                var defaultUsers = new UserRoot
                {
                    Users = new List<UserEntry>
                    {
                        new UserEntry { Username = "admin", PasswordHash = GetHash("admin"), Role = "Administrator" },
                        new UserEntry { Username = "CEO", PasswordHash = GetHash("CEO123"), Role = "Administrator" },
                        new UserEntry { Username = "user", PasswordHash = GetHash("user"), Role = "User" }
                    }
                };

                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(defaultUsers, options);
                File.WriteAllText(UsersFilePath, json);
            }
        }

        public static string GetHash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateLoginField())
                {
                    MessageBox.Show("Введите логин", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidatePasswordField())
                {
                    MessageBox.Show("Введите пароль", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string username = usernameTextBox.Text;
                string password = passwordTextBox.Text;

                if (!File.Exists(UsersFilePath)) return;

                string json = File.ReadAllText(UsersFilePath);
                var userRoot = JsonSerializer.Deserialize<UserRoot>(json);
                string enteredHash = GetHash(password);

                var user = userRoot?.Users?.FirstOrDefault(u => u.Username == username && u.PasswordHash == enteredHash);

                if (user != null)
                {
                    admin = user.Role == "Administrator";
                    string roleText = admin ? "с правами администратора" : "как обычный сотрудник";

                    MessageBox.Show($"Вы вошли {roleText}.", "Добро пожаловать", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Menu menu = new Menu();
                    menu.FormClosed += (s, args) => { if (!menu.Visible && Application.OpenForms.Count == 0) Application.Exit(); else if (Application.OpenForms.Count == 1 && Application.OpenForms[0] is LoginForm) return; else Application.Exit(); };
                    menu.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void usernameTextBox_Enter(object sender, EventArgs e)
        {
            HighlightInvalidField(usernameTextBox, true);
            if (usernameTextBox.Text == "Введите имя пользователя")
            {
                usernameTextBox.Text = "";
                usernameTextBox.ForeColor = SystemColors.WindowText;
            }
        }

        private void usernameTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(usernameTextBox.Text))
            {
                usernameTextBox.Text = "Введите имя пользователя";
                usernameTextBox.ForeColor = SystemColors.GrayText;
            }
            ValidateLoginField();
        }

        private void passwordTextBox_Enter(object sender, EventArgs e)
        {
            HighlightInvalidField(passwordTextBox, true);
            if (passwordTextBox.Text == "Введите пароль")
            {
                passwordTextBox.Text = "";
                passwordTextBox.UseSystemPasswordChar = true;
                passwordTextBox.ForeColor = SystemColors.WindowText;
            }
        }

        private void passwordTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                passwordTextBox.UseSystemPasswordChar = false;
                passwordTextBox.Text = "Введите пароль";
                passwordTextBox.ForeColor = SystemColors.GrayText;
            }
            ValidatePasswordField();
        }
    }

    // Классы для десериализации JSON
    public class UserEntry
    {
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = null!;
    }

    public class UserRoot
    {
        public List<UserEntry> Users { get; set; } = new List<UserEntry>();
    }
}
