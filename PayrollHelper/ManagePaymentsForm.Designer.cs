namespace PayrollHelper
{
    partial class ManagePaymentsForm
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
            lblPaymentName = new Label();
            lblDefaultAmount = new Label();
            lblPaymentDescription = new Label();
            txtPaymentName = new TextBox();
            txtDefaultAmount = new TextBox();
            txtPaymentDescription = new TextBox();
            btnAddPayment = new Button();
            lstPayments = new ListBox();
            grpAddPayment = new GroupBox();
            grpPaymentList = new GroupBox();
            grpTaxSelection = new GroupBox();
            clbTaxes = new CheckedListBox();
            grpAddPayment.SuspendLayout();
            grpPaymentList.SuspendLayout();
            grpTaxSelection.SuspendLayout();
            SuspendLayout();
            // 
            // lblPaymentName
            // 
            lblPaymentName.Location = new Point(6, 28);
            lblPaymentName.Name = "lblPaymentName";
            lblPaymentName.Size = new Size(120, 23);
            lblPaymentName.TabIndex = 0;
            lblPaymentName.Text = "Название выплаты:";
            lblPaymentName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblDefaultAmount
            // 
            lblDefaultAmount.Location = new Point(-2, 65);
            lblDefaultAmount.Name = "lblDefaultAmount";
            lblDefaultAmount.Size = new Size(141, 23);
            lblDefaultAmount.TabIndex = 1;
            lblDefaultAmount.Text = "Сумма по умолчанию:";
            lblDefaultAmount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblPaymentDescription
            // 
            lblPaymentDescription.Location = new Point(6, 111);
            lblPaymentDescription.Name = "lblPaymentDescription";
            lblPaymentDescription.Size = new Size(120, 23);
            lblPaymentDescription.TabIndex = 2;
            lblPaymentDescription.Text = "Описание:";
            lblPaymentDescription.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPaymentName
            // 
            txtPaymentName.Location = new Point(145, 29);
            txtPaymentName.MaxLength = 50;
            txtPaymentName.Name = "txtPaymentName";
            txtPaymentName.Size = new Size(250, 23);
            txtPaymentName.TabIndex = 5;
            // 
            // txtDefaultAmount
            // 
            txtDefaultAmount.Location = new Point(145, 66);
            txtDefaultAmount.MaxLength = 10;
            txtDefaultAmount.Name = "txtDefaultAmount";
            txtDefaultAmount.Size = new Size(120, 23);
            txtDefaultAmount.TabIndex = 6;
            txtDefaultAmount.TextAlign = HorizontalAlignment.Right;
            // 
            // txtPaymentDescription
            // 
            txtPaymentDescription.AcceptsReturn = true;
            txtPaymentDescription.Location = new Point(6, 147);
            txtPaymentDescription.MaxLength = 255;
            txtPaymentDescription.Multiline = true;
            txtPaymentDescription.Name = "txtPaymentDescription";
            txtPaymentDescription.ScrollBars = ScrollBars.Vertical;
            txtPaymentDescription.Size = new Size(250, 60);
            txtPaymentDescription.TabIndex = 7;
            // 
            // btnAddPayment
            // 
            btnAddPayment.BackColor = Color.LightGreen;
            btnAddPayment.Cursor = Cursors.Hand;
            btnAddPayment.FlatStyle = FlatStyle.Flat;
            btnAddPayment.Location = new Point(6, 225);
            btnAddPayment.Name = "btnAddPayment";
            btnAddPayment.Size = new Size(189, 35);
            btnAddPayment.TabIndex = 11;
            btnAddPayment.Text = "➕ Добавить выплату";
            btnAddPayment.UseVisualStyleBackColor = false;
            // 
            // lstPayments
            // 
            lstPayments.BorderStyle = BorderStyle.FixedSingle;
            lstPayments.FormattingEnabled = true;
            lstPayments.IntegralHeight = false;
            lstPayments.ItemHeight = 15;
            lstPayments.Location = new Point(6, 28);
            lstPayments.Name = "lstPayments";
            lstPayments.ScrollAlwaysVisible = true;
            lstPayments.Size = new Size(230, 286);
            lstPayments.TabIndex = 12;
            // 
            // grpAddPayment
            // 
            grpAddPayment.Controls.Add(lblPaymentName);
            grpAddPayment.Controls.Add(txtPaymentName);
            grpAddPayment.Controls.Add(btnAddPayment);
            grpAddPayment.Controls.Add(lblDefaultAmount);
            grpAddPayment.Controls.Add(txtPaymentDescription);
            grpAddPayment.Controls.Add(txtDefaultAmount);
            grpAddPayment.Controls.Add(lblPaymentDescription);
            grpAddPayment.FlatStyle = FlatStyle.Flat;
            grpAddPayment.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpAddPayment.Location = new Point(12, 12);
            grpAddPayment.Name = "grpAddPayment";
            grpAddPayment.Size = new Size(413, 320);
            grpAddPayment.TabIndex = 13;
            grpAddPayment.TabStop = false;
            grpAddPayment.Text = "Добавление выплаты";
            // 
            // grpPaymentList
            // 
            grpPaymentList.Controls.Add(lstPayments);
            grpPaymentList.FlatStyle = FlatStyle.Flat;
            grpPaymentList.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpPaymentList.Location = new Point(470, 12);
            grpPaymentList.Name = "grpPaymentList";
            grpPaymentList.Size = new Size(250, 320);
            grpPaymentList.TabIndex = 14;
            grpPaymentList.TabStop = false;
            grpPaymentList.Text = "Существующие выплаты";
            // 
            // grpTaxSelection
            // 
            grpTaxSelection.Controls.Add(clbTaxes);
            grpTaxSelection.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpTaxSelection.Location = new Point(751, 12);
            grpTaxSelection.Name = "grpTaxSelection";
            grpTaxSelection.Size = new Size(248, 320);
            grpTaxSelection.TabIndex = 15;
            grpTaxSelection.TabStop = false;
            grpTaxSelection.Text = "Применяемые налоги";
            // 
            // clbTaxes
            // 
            clbTaxes.BackColor = Color.White;
            clbTaxes.BorderStyle = BorderStyle.FixedSingle;
            clbTaxes.CheckOnClick = true;
            clbTaxes.FormattingEnabled = true;
            clbTaxes.IntegralHeight = false;
            clbTaxes.Location = new Point(6, 22);
            clbTaxes.Name = "clbTaxes";
            clbTaxes.ScrollAlwaysVisible = true;
            clbTaxes.Size = new Size(230, 292);
            clbTaxes.TabIndex = 0;
            // 
            // ManagePaymentsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1011, 358);
            Controls.Add(grpTaxSelection);
            Controls.Add(grpPaymentList);
            Controls.Add(grpAddPayment);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            Name = "ManagePaymentsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Управление выплатами";
            grpAddPayment.ResumeLayout(false);
            grpAddPayment.PerformLayout();
            grpPaymentList.ResumeLayout(false);
            grpTaxSelection.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblPaymentName;
        private Label lblDefaultAmount;
        private Label lblPaymentDescription;
        private TextBox txtPaymentName;
        private TextBox txtDefaultAmount;
        private TextBox txtPaymentDescription;
        private Button btnAddPayment;
        private ListBox lstPayments;
        private GroupBox grpAddPayment;
        private GroupBox grpPaymentList;
        private GroupBox grpTaxSelection;
        private CheckedListBox clbTaxes;
    }
}