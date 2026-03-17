namespace PayrollHelper
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            usernameTextBox = new TextBox();
            passwordTextBox = new TextBox();
            loginButton = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // usernameTextBox
            // 
            usernameTextBox.Location = new Point(132, 71);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(169, 23);
            usernameTextBox.TabIndex = 0;
            usernameTextBox.Text = "Введите имя пользователя";
            usernameTextBox.Enter += usernameTextBox_Enter;
            usernameTextBox.Leave += usernameTextBox_Leave;
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(132, 103);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(174, 23);
            passwordTextBox.TabIndex = 1;
            passwordTextBox.Text = "Введите пароль";
            passwordTextBox.Enter += passwordTextBox_Enter;
            passwordTextBox.Leave += passwordTextBox_Leave;
            // 
            // loginButton
            // 
            loginButton.BackColor = Color.LightBlue;
            loginButton.Cursor = Cursors.Hand;
            loginButton.FlatStyle = FlatStyle.Flat;
            loginButton.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            loginButton.Location = new Point(125, 150);
            loginButton.Name = "loginButton";
            loginButton.Size = new Size(100, 30);
            loginButton.TabIndex = 2;
            loginButton.Text = "Войти";
            loginButton.UseVisualStyleBackColor = false;
            loginButton.Click += loginButton_Click;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(100, 20);
            label1.Name = "label1";
            label1.Size = new Size(150, 30);
            label1.TabIndex = 3;
            label1.Text = "Вход в систему";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Location = new Point(50, 70);
            label2.Name = "label2";
            label2.Size = new Size(70, 23);
            label2.TabIndex = 4;
            label2.Text = "Логин:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.Location = new Point(71, 103);
            label3.Name = "label3";
            label3.Size = new Size(49, 23);
            label3.TabIndex = 5;
            label3.Text = "Пароль";
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(334, 211);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(loginButton);
            Controls.Add(passwordTextBox);
            Controls.Add(usernameTextBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LoginForm";
            RightToLeft = RightToLeft.No;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Авторизация";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Button loginButton;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}
