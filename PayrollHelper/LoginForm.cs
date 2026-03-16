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

            usernameTextBox.Enter += usernameTextBox_Enter;
            usernameTextBox.Leave += usernameTextBox_Leave;
            passwordTextBox.Enter += passwordTextBox_Enter;
            passwordTextBox.Leave += passwordTextBox_Leave;
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
                        new UserEntry { Username = "user", PasswordHash = GetHash("user"), Role = "User" },
                        new UserEntry { Username = "Васильев Артем Сергеевич", PasswordHash = GetHash("vasartem123"), Role = "User" }
                    }
                };

                // Настройки для красивого вывода и поддержки кириллицы (без экранирования)
                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(defaultUsers, options);
                File.WriteAllText(UsersFilePath, json);
            }
        }

        private string GetHash(string input)
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
                string username = usernameTextBox.Text;
                string password = passwordTextBox.Text;

                if (username == "Введите имя пользователя" || string.IsNullOrWhiteSpace(password) || password == "Введите пароль")
                {
                    MessageBox.Show("Введите логин и пароль!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!File.Exists(UsersFilePath))
                {
                    MessageBox.Show("Файл пользователей не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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
                    menu.FormClosed += (s, args) => this.Close();
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
                MessageBox.Show($"Произошла ошибка при входе: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void usernameTextBox_Enter(object sender, EventArgs e)
        {
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
        }

        private void passwordTextBox_Enter(object sender, EventArgs e)
        {
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
