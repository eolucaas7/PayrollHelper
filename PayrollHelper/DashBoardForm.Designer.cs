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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            buttonAddPayment = new Button();
            textSpecialAmount = new TextBox();
            checkSpecialAmount = new CheckBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            comboEmployee = new ComboBox();
            comboBonusType = new ComboBox();
            comboPaymentType = new ComboBox();
            tabPage2 = new TabPage();
            buttonAddEmployee = new Button();
            comboPosition = new ComboBox();
            checkInsurance = new CheckBox();
            textPhoneNumber = new TextBox();
            textAddress = new TextBox();
            textFullName = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(340, 241);
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(buttonAddPayment);
            tabPage1.Controls.Add(textSpecialAmount);
            tabPage1.Controls.Add(checkSpecialAmount);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(comboEmployee);
            tabPage1.Controls.Add(comboBonusType);
            tabPage1.Controls.Add(comboPaymentType);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(332, 213);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Выплаты";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonAddPayment
            // 
            buttonAddPayment.FlatStyle = FlatStyle.Popup;
            buttonAddPayment.Location = new Point(13, 164);
            buttonAddPayment.Name = "buttonAddPayment";
            buttonAddPayment.Size = new Size(159, 23);
            buttonAddPayment.TabIndex = 8;
            buttonAddPayment.Text = "Добавить запись";
            buttonAddPayment.UseVisualStyleBackColor = true;
            buttonAddPayment.Click += buttonAddPayment_Click;
            // 
            // textSpecialAmount
            // 
            textSpecialAmount.Location = new Point(193, 125);
            textSpecialAmount.Name = "textSpecialAmount";
            textSpecialAmount.Size = new Size(100, 23);
            textSpecialAmount.TabIndex = 7;
            // 
            // checkSpecialAmount
            // 
            checkSpecialAmount.AutoSize = true;
            checkSpecialAmount.Location = new Point(6, 125);
            checkSpecialAmount.Name = "checkSpecialAmount";
            checkSpecialAmount.Size = new Size(166, 19);
            checkSpecialAmount.TabIndex = 6;
            checkSpecialAmount.Text = "Особый размер выплаты";
            checkSpecialAmount.UseVisualStyleBackColor = true;
            checkSpecialAmount.CheckedChanged += checkSpecialAmount_CheckedChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 87);
            label3.Name = "label3";
            label3.Size = new Size(66, 15);
            label3.TabIndex = 5;
            label3.Text = "Сотрудник";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 49);
            label2.Name = "label2";
            label2.Size = new Size(73, 15);
            label2.TabIndex = 4;
            label2.Text = "Вид премии";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 9);
            label1.Name = "label1";
            label1.Size = new Size(82, 15);
            label1.TabIndex = 3;
            label1.Text = "Вид выплаты:";
            // 
            // comboEmployee
            // 
            comboEmployee.FormattingEnabled = true;
            comboEmployee.Location = new Point(109, 87);
            comboEmployee.Name = "comboEmployee";
            comboEmployee.Size = new Size(205, 23);
            comboEmployee.TabIndex = 2;
            // 
            // comboBonusType
            // 
            comboBonusType.FormattingEnabled = true;
            comboBonusType.Location = new Point(109, 49);
            comboBonusType.Name = "comboBonusType";
            comboBonusType.Size = new Size(205, 23);
            comboBonusType.TabIndex = 1;
            // 
            // comboPaymentType
            // 
            comboPaymentType.FormattingEnabled = true;
            comboPaymentType.Location = new Point(109, 9);
            comboPaymentType.Name = "comboPaymentType";
            comboPaymentType.Size = new Size(205, 23);
            comboPaymentType.TabIndex = 0;
            comboPaymentType.SelectedIndexChanged += comboPaymentType_SelectedIndexChanged;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(buttonAddEmployee);
            tabPage2.Controls.Add(comboPosition);
            tabPage2.Controls.Add(checkInsurance);
            tabPage2.Controls.Add(textPhoneNumber);
            tabPage2.Controls.Add(textAddress);
            tabPage2.Controls.Add(textFullName);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(label4);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(332, 213);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Новый сотрудник";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonAddEmployee
            // 
            buttonAddEmployee.FlatStyle = FlatStyle.Popup;
            buttonAddEmployee.Location = new Point(156, 177);
            buttonAddEmployee.Name = "buttonAddEmployee";
            buttonAddEmployee.Size = new Size(134, 23);
            buttonAddEmployee.TabIndex = 9;
            buttonAddEmployee.Text = "Добавить сотрудника";
            buttonAddEmployee.UseVisualStyleBackColor = true;
            buttonAddEmployee.Click += buttonAddEmployee_Click;
            // 
            // comboPosition
            // 
            comboPosition.FormattingEnabled = true;
            comboPosition.Location = new Point(91, 56);
            comboPosition.Name = "comboPosition";
            comboPosition.Size = new Size(199, 23);
            comboPosition.TabIndex = 8;
            // 
            // checkInsurance
            // 
            checkInsurance.AutoSize = true;
            checkInsurance.Location = new Point(6, 177);
            checkInsurance.Name = "checkInsurance";
            checkInsurance.Size = new Size(134, 19);
            checkInsurance.TabIndex = 7;
            checkInsurance.Text = "Наличие страховки";
            checkInsurance.UseVisualStyleBackColor = true;
            // 
            // textPhoneNumber
            // 
            textPhoneNumber.Location = new Point(125, 101);
            textPhoneNumber.Name = "textPhoneNumber";
            textPhoneNumber.Size = new Size(165, 23);
            textPhoneNumber.TabIndex = 6;
            // 
            // textAddress
            // 
            textAddress.Location = new Point(125, 140);
            textAddress.Name = "textAddress";
            textAddress.Size = new Size(165, 23);
            textAddress.TabIndex = 5;
            // 
            // textFullName
            // 
            textFullName.Location = new Point(76, 15);
            textFullName.Name = "textFullName";
            textFullName.Size = new Size(214, 23);
            textFullName.TabIndex = 4;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 143);
            label7.Name = "label7";
            label7.Size = new Size(112, 15);
            label7.TabIndex = 3;
            label7.Text = "Адрес проживания";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 104);
            label6.Name = "label6";
            label6.Size = new Size(101, 15);
            label6.TabIndex = 2;
            label6.Text = "Номер телефона";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 59);
            label5.Name = "label5";
            label5.Size = new Size(69, 15);
            label5.TabIndex = 1;
            label5.Text = "Должность";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 18);
            label4.Name = "label4";
            label4.Size = new Size(34, 15);
            label4.TabIndex = 0;
            label4.Text = "ФИО";
            // 
            // DashBoardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(364, 265);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "DashBoardForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Рабочее пространство";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private Button buttonAddPayment;
        private TextBox textSpecialAmount;
        private CheckBox checkSpecialAmount;
        private Label label3;
        private Label label2;
        private Label label1;
        private ComboBox comboEmployee;
        private ComboBox comboBonusType;
        private ComboBox comboPaymentType;
        private TabPage tabPage2;
        private TextBox textPhoneNumber;
        private TextBox textAddress;
        private TextBox textFullName;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Button buttonAddEmployee;
        private ComboBox comboPosition;
        private CheckBox checkInsurance;
    }
}