using System;
using System.Windows.Forms;

namespace PayrollHelper
{
    public partial class LoginForm : Form
    {
        public static bool admin;
        public LoginForm()
        {
            InitializeComponent();

            usernameTextBox.Enter += usernameTextBox_Enter;
            usernameTextBox.Leave += usernameTextBox_Leave;
            passwordTextBox.Enter += passwordTextBox_Enter;
            passwordTextBox.Leave += passwordTextBox_Leave;
        }
        

        private void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                string username = usernameTextBox.Text;
                string password = passwordTextBox.Text;

                if (username == "admin" && password == "admin")
                {
                    admin = true;
                    MessageBox.Show("Вы вошли с правами администратора.", "Добро пожаловать", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Menu menu = new Menu();
                    menu.FormClosed += (s, args) => this.Close();
                    menu.Show();
                    this.Hide();
                }
                else if (username == "user" && password == "user")
                {
                    admin = false;
                    MessageBox.Show("Вы вошли как обычный сотрудник.", "Добро пожаловать", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (passwordTextBox.Text == "Введите пароль");
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
}
