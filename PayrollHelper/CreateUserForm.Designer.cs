namespace PayrollHelper
{
    partial class CreateUserForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblUsername = new Label();
            lblPassword = new Label();
            lblRole = new Label();
            lblHint = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            cmbRole = new ComboBox();
            btnCreate = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lblUsername
            // 
            lblUsername.Location = new Point(30, 25);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(63, 23);
            lblUsername.TabIndex = 0;
            lblUsername.Text = "Логин:";
            // 
            // lblPassword
            // 
            lblPassword.Location = new Point(30, 59);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(63, 23);
            lblPassword.TabIndex = 1;
            lblPassword.Text = "Пароль:";
            // 
            // lblRole
            // 
            lblRole.Location = new Point(30, 102);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(53, 23);
            lblRole.TabIndex = 2;
            lblRole.Text = "Роль:";
            // 
            // lblHint
            // 
            lblHint.ForeColor = Color.Gray;
            lblHint.Location = new Point(30, 190);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(280, 22);
            lblHint.TabIndex = 3;
            lblHint.Text = "Пароль будет сохранён в безопасном виде (хэш)";
            lblHint.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txtUsername
            // 
            txtUsername.BackColor = Color.White;
            txtUsername.Location = new Point(117, 22);
            txtUsername.MaxLength = 50;
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(100, 23);
            txtUsername.TabIndex = 4;
            // 
            // txtPassword
            // 
            txtPassword.BackColor = Color.White;
            txtPassword.Location = new Point(117, 59);
            txtPassword.MaxLength = 50;
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(100, 23);
            txtPassword.TabIndex = 5;
            // 
            // cmbRole
            // 
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.FlatStyle = FlatStyle.System;
            cmbRole.FormattingEnabled = true;
            cmbRole.Location = new Point(117, 102);
            cmbRole.Name = "cmbRole";
            cmbRole.Size = new Size(205, 23);
            cmbRole.TabIndex = 6;
            // 
            // btnCreate
            // 
            btnCreate.BackColor = Color.CornflowerBlue;
            btnCreate.Cursor = Cursors.Hand;
            btnCreate.FlatStyle = FlatStyle.Flat;
            btnCreate.Location = new Point(115, 140);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(90, 30);
            btnCreate.TabIndex = 7;
            btnCreate.Text = "Создать";
            btnCreate.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.LightGray;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Location = new Point(215, 140);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 30);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Отмена";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // CreateUserForm
            // 
            AcceptButton = btnCreate;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(334, 241);
            Controls.Add(btnCancel);
            Controls.Add(btnCreate);
            Controls.Add(cmbRole);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(lblHint);
            Controls.Add(lblRole);
            Controls.Add(lblPassword);
            Controls.Add(lblUsername);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CreateUserForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Создание нового пользователя";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblUsername;
        private Label lblPassword;
        private Label lblRole;
        private Label lblHint;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private ComboBox cmbRole;
        private Button btnCreate;
        private Button btnCancel;
    }
}