namespace PayrollHelper
{
    partial class DashBoardForm
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
            tabControl = new TabControl();
            tabPayments = new TabPage();
            buttonAddPayment = new Button();
            textSpecialAmount = new TextBox();
            checkSpecialAmount = new CheckBox();
            lblEmployee = new Label();
            lblBonusType = new Label();
            lblPaymentType = new Label();
            comboEmployee = new ComboBox();
            comboBonusType = new ComboBox();
            comboPaymentType = new ComboBox();
            tabNewEmployee = new TabPage();
            buttonAddEmployee = new Button();
            comboPosition = new ComboBox();
            checkInsurance = new CheckBox();
            textPhoneNumber = new TextBox();
            textAddress = new TextBox();
            textFullName = new TextBox();
            lblAddress = new Label();
            lblPhone = new Label();
            lblPosition = new Label();
            lblFullName = new Label();
            tabControl.SuspendLayout();
            tabPayments.SuspendLayout();
            tabNewEmployee.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabPayments);
            tabControl.Controls.Add(tabNewEmployee);
            tabControl.Location = new Point(12, 12);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(520, 246);
            tabControl.TabIndex = 0;
            tabControl.TabStop = false;
            tabControl.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabPayments
            // 
            tabPayments.Controls.Add(buttonAddPayment);
            tabPayments.Controls.Add(textSpecialAmount);
            tabPayments.Controls.Add(checkSpecialAmount);
            tabPayments.Controls.Add(lblEmployee);
            tabPayments.Controls.Add(lblBonusType);
            tabPayments.Controls.Add(lblPaymentType);
            tabPayments.Controls.Add(comboEmployee);
            tabPayments.Controls.Add(comboBonusType);
            tabPayments.Controls.Add(comboPaymentType);
            tabPayments.Location = new Point(4, 24);
            tabPayments.Name = "tabPayments";
            tabPayments.Padding = new Padding(3);
            tabPayments.Size = new Size(512, 218);
            tabPayments.TabIndex = 0;
            tabPayments.Text = "Выплаты сотрудникам";
            tabPayments.UseVisualStyleBackColor = true;
            // 
            // buttonAddPayment
            // 
            buttonAddPayment.BackColor = Color.LightGreen;
            buttonAddPayment.FlatStyle = FlatStyle.Flat;
            buttonAddPayment.Location = new Point(323, 59);
            buttonAddPayment.Name = "buttonAddPayment";
            buttonAddPayment.Size = new Size(160, 30);
            buttonAddPayment.TabIndex = 8;
            buttonAddPayment.Text = "Начислить выплату";
            buttonAddPayment.UseVisualStyleBackColor = false;
            buttonAddPayment.Click += buttonAddPayment_Click;
            // 
            // textSpecialAmount
            // 
            textSpecialAmount.Location = new Point(393, 20);
            textSpecialAmount.Name = "textSpecialAmount";
            textSpecialAmount.Size = new Size(100, 23);
            textSpecialAmount.TabIndex = 7;
            textSpecialAmount.TextAlign = HorizontalAlignment.Right;
            textSpecialAmount.Visible = false;
            // 
            // checkSpecialAmount
            // 
            checkSpecialAmount.AutoSize = true;
            checkSpecialAmount.FlatStyle = FlatStyle.Flat;
            checkSpecialAmount.Location = new Point(271, 20);
            checkSpecialAmount.Name = "checkSpecialAmount";
            checkSpecialAmount.Size = new Size(103, 19);
            checkSpecialAmount.TabIndex = 6;
            checkSpecialAmount.Text = "Особая сумма";
            checkSpecialAmount.UseVisualStyleBackColor = true;
            checkSpecialAmount.CheckedChanged += checkSpecialAmount_CheckedChanged;
            // 
            // lblEmployee
            // 
            lblEmployee.AutoSize = true;
            lblEmployee.Location = new Point(15, 59);
            lblEmployee.Name = "lblEmployee";
            lblEmployee.Size = new Size(66, 15);
            lblEmployee.TabIndex = 5;
            lblEmployee.Text = "Сотрудник";
            lblEmployee.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblBonusType
            // 
            lblBonusType.AutoSize = true;
            lblBonusType.Location = new Point(15, 97);
            lblBonusType.Name = "lblBonusType";
            lblBonusType.Size = new Size(73, 15);
            lblBonusType.TabIndex = 4;
            lblBonusType.Text = "Вид премии";
            lblBonusType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblPaymentType
            // 
            lblPaymentType.AutoSize = true;
            lblPaymentType.FlatStyle = FlatStyle.Popup;
            lblPaymentType.Location = new Point(15, 20);
            lblPaymentType.Name = "lblPaymentType";
            lblPaymentType.Size = new Size(82, 15);
            lblPaymentType.TabIndex = 3;
            lblPaymentType.Text = "Вид выплаты:";
            lblPaymentType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // comboEmployee
            // 
            comboEmployee.DropDownStyle = ComboBoxStyle.DropDownList;
            comboEmployee.FormattingEnabled = true;
            comboEmployee.Location = new Point(109, 59);
            comboEmployee.Name = "comboEmployee";
            comboEmployee.Size = new Size(180, 23);
            comboEmployee.TabIndex = 2;
            // 
            // comboBonusType
            // 
            comboBonusType.FormattingEnabled = true;
            comboBonusType.Location = new Point(109, 97);
            comboBonusType.Name = "comboBonusType";
            comboBonusType.Size = new Size(150, 23);
            comboBonusType.TabIndex = 1;
            // 
            // comboPaymentType
            // 
            comboPaymentType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPaymentType.FormattingEnabled = true;
            comboPaymentType.Location = new Point(109, 17);
            comboPaymentType.Name = "comboPaymentType";
            comboPaymentType.Size = new Size(150, 23);
            comboPaymentType.TabIndex = 0;
            comboPaymentType.SelectedIndexChanged += comboPaymentType_SelectedIndexChanged;
            // 
            // tabNewEmployee
            // 
            tabNewEmployee.Controls.Add(buttonAddEmployee);
            tabNewEmployee.Controls.Add(comboPosition);
            tabNewEmployee.Controls.Add(checkInsurance);
            tabNewEmployee.Controls.Add(textPhoneNumber);
            tabNewEmployee.Controls.Add(textAddress);
            tabNewEmployee.Controls.Add(textFullName);
            tabNewEmployee.Controls.Add(lblAddress);
            tabNewEmployee.Controls.Add(lblPhone);
            tabNewEmployee.Controls.Add(lblPosition);
            tabNewEmployee.Controls.Add(lblFullName);
            tabNewEmployee.Location = new Point(4, 24);
            tabNewEmployee.Name = "tabNewEmployee";
            tabNewEmployee.Padding = new Padding(3);
            tabNewEmployee.Size = new Size(512, 218);
            tabNewEmployee.TabIndex = 1;
            tabNewEmployee.Text = "Добавление сотрудника";
            tabNewEmployee.UseVisualStyleBackColor = true;
            // 
            // buttonAddEmployee
            // 
            buttonAddEmployee.BackColor = Color.LightBlue;
            buttonAddEmployee.FlatStyle = FlatStyle.Flat;
            buttonAddEmployee.Location = new Point(15, 160);
            buttonAddEmployee.Name = "buttonAddEmployee";
            buttonAddEmployee.Size = new Size(184, 37);
            buttonAddEmployee.TabIndex = 9;
            buttonAddEmployee.Text = "Зарегистрировать сотрудника";
            buttonAddEmployee.TextAlign = ContentAlignment.MiddleLeft;
            buttonAddEmployee.UseVisualStyleBackColor = false;
            buttonAddEmployee.Click += buttonAddEmployee_Click;
            // 
            // comboPosition
            // 
            comboPosition.DropDownStyle = ComboBoxStyle.DropDownList;
            comboPosition.FormattingEnabled = true;
            comboPosition.Location = new Point(91, 52);
            comboPosition.Name = "comboPosition";
            comboPosition.Size = new Size(150, 23);
            comboPosition.TabIndex = 8;
            // 
            // checkInsurance
            // 
            checkInsurance.AutoSize = true;
            checkInsurance.FlatStyle = FlatStyle.Flat;
            checkInsurance.Location = new Point(224, 169);
            checkInsurance.Name = "checkInsurance";
            checkInsurance.Size = new Size(80, 19);
            checkInsurance.TabIndex = 7;
            checkInsurance.Text = "Страховка";
            checkInsurance.UseVisualStyleBackColor = true;
            // 
            // textPhoneNumber
            // 
            textPhoneNumber.Location = new Point(120, 88);
            textPhoneNumber.Name = "textPhoneNumber";
            textPhoneNumber.Size = new Size(250, 23);
            textPhoneNumber.TabIndex = 6;
            // 
            // textAddress
            // 
            textAddress.Location = new Point(133, 122);
            textAddress.Name = "textAddress";
            textAddress.Size = new Size(100, 23);
            textAddress.TabIndex = 5;
            // 
            // textFullName
            // 
            textFullName.Location = new Point(66, 17);
            textFullName.Name = "textFullName";
            textFullName.Size = new Size(250, 23);
            textFullName.TabIndex = 4;
            // 
            // lblAddress
            // 
            lblAddress.AutoSize = true;
            lblAddress.Location = new Point(15, 125);
            lblAddress.Name = "lblAddress";
            lblAddress.Size = new Size(112, 15);
            lblAddress.TabIndex = 3;
            lblAddress.Text = "Адрес проживания";
            lblAddress.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(15, 90);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(101, 15);
            lblPhone.TabIndex = 2;
            lblPhone.Text = "Номер телефона";
            lblPhone.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblPosition
            // 
            lblPosition.AutoSize = true;
            lblPosition.Location = new Point(15, 55);
            lblPosition.Name = "lblPosition";
            lblPosition.Size = new Size(69, 15);
            lblPosition.TabIndex = 1;
            lblPosition.Text = "Должность";
            lblPosition.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(15, 20);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(34, 15);
            lblFullName.TabIndex = 0;
            lblFullName.Text = "ФИО";
            lblFullName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // DashBoardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 269);
            Controls.Add(tabControl);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "DashBoardForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Управление персоналом";
            tabControl.ResumeLayout(false);
            tabPayments.ResumeLayout(false);
            tabPayments.PerformLayout();
            tabNewEmployee.ResumeLayout(false);
            tabNewEmployee.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl;
        private TabPage tabPayments;
        private Button buttonAddPayment;
        private TextBox textSpecialAmount;
        private CheckBox checkSpecialAmount;
        private Label lblEmployee;
        private Label lblBonusType;
        private Label lblPaymentType;
        private ComboBox comboEmployee;
        private ComboBox comboBonusType;
        private ComboBox comboPaymentType;
        private TabPage tabNewEmployee;
        private TextBox textPhoneNumber;
        private TextBox textAddress;
        private TextBox textFullName;
        private Label lblAddress;
        private Label lblPhone;
        private Label lblPosition;
        private Label lblFullName;
        private Button buttonAddEmployee;
        private ComboBox comboPosition;
        private CheckBox checkInsurance;
    }
}