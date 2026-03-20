namespace PayrollHelper
{
    partial class ManageTaxationForm
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
            lblTaxName = new Label();
            lblTaxRate = new Label();
            lblTaxDescription = new Label();
            txtTaxName = new TextBox();
            txtTaxRate = new TextBox();
            txtTaxDescription = new TextBox();
            btnAddTax = new Button();
            lstTaxes = new ListBox();
            grpAddTax = new GroupBox();
            grpTaxList = new GroupBox();
            grpAddTax.SuspendLayout();
            grpTaxList.SuspendLayout();
            SuspendLayout();
            // 
            // lblTaxName
            // 
            lblTaxName.Location = new Point(6, 34);
            lblTaxName.Name = "lblTaxName";
            lblTaxName.Size = new Size(109, 23);
            lblTaxName.TabIndex = 0;
            lblTaxName.Text = "Название налога:";
            lblTaxName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTaxRate
            // 
            lblTaxRate.Location = new Point(6, 75);
            lblTaxRate.Name = "lblTaxRate";
            lblTaxRate.Size = new Size(100, 23);
            lblTaxRate.TabIndex = 1;
            lblTaxRate.Text = "Ставка (%):";
            lblTaxRate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTaxDescription
            // 
            lblTaxDescription.Location = new Point(6, 113);
            lblTaxDescription.Name = "lblTaxDescription";
            lblTaxDescription.Size = new Size(100, 23);
            lblTaxDescription.TabIndex = 2;
            lblTaxDescription.Text = "Описание:";
            lblTaxDescription.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtTaxName
            // 
            txtTaxName.Location = new Point(121, 35);
            txtTaxName.MaxLength = 50;
            txtTaxName.Name = "txtTaxName";
            txtTaxName.Size = new Size(250, 23);
            txtTaxName.TabIndex = 3;
            // 
            // txtTaxRate
            // 
            txtTaxRate.Location = new Point(121, 84);
            txtTaxRate.Name = "txtTaxRate";
            txtTaxRate.Size = new Size(100, 23);
            txtTaxRate.TabIndex = 4;
            txtTaxRate.TextAlign = HorizontalAlignment.Right;
            // 
            // txtTaxDescription
            // 
            txtTaxDescription.AcceptsReturn = true;
            txtTaxDescription.Location = new Point(6, 151);
            txtTaxDescription.MaxLength = 255;
            txtTaxDescription.Multiline = true;
            txtTaxDescription.Name = "txtTaxDescription";
            txtTaxDescription.ScrollBars = ScrollBars.Vertical;
            txtTaxDescription.Size = new Size(250, 60);
            txtTaxDescription.TabIndex = 5;
            // 
            // btnAddTax
            // 
            btnAddTax.BackColor = Color.Orange;
            btnAddTax.Cursor = Cursors.Hand;
            btnAddTax.FlatStyle = FlatStyle.Flat;
            btnAddTax.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            btnAddTax.ForeColor = Color.Black;
            btnAddTax.Location = new Point(6, 233);
            btnAddTax.Name = "btnAddTax";
            btnAddTax.Size = new Size(180, 35);
            btnAddTax.TabIndex = 6;
            btnAddTax.Text = "➕ Добавить налог";
            btnAddTax.UseVisualStyleBackColor = false;
            // 
            // lstTaxes
            // 
            lstTaxes.BorderStyle = BorderStyle.FixedSingle;
            lstTaxes.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lstTaxes.FormattingEnabled = true;
            lstTaxes.IntegralHeight = false;
            lstTaxes.ItemHeight = 15;
            lstTaxes.Location = new Point(6, 22);
            lstTaxes.Name = "lstTaxes";
            lstTaxes.ScrollAlwaysVisible = true;
            lstTaxes.Size = new Size(230, 292);
            lstTaxes.TabIndex = 8;
            // 
            // grpAddTax
            // 
            grpAddTax.Controls.Add(txtTaxName);
            grpAddTax.Controls.Add(lblTaxName);
            grpAddTax.Controls.Add(lblTaxRate);
            grpAddTax.Controls.Add(txtTaxRate);
            grpAddTax.Controls.Add(btnAddTax);
            grpAddTax.Controls.Add(lblTaxDescription);
            grpAddTax.Controls.Add(txtTaxDescription);
            grpAddTax.FlatStyle = FlatStyle.Flat;
            grpAddTax.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpAddTax.Location = new Point(12, 12);
            grpAddTax.Name = "grpAddTax";
            grpAddTax.Size = new Size(389, 320);
            grpAddTax.TabIndex = 9;
            grpAddTax.TabStop = false;
            grpAddTax.Text = "Добавление налога";
            // 
            // grpTaxList
            // 
            grpTaxList.Controls.Add(lstTaxes);
            grpTaxList.FlatStyle = FlatStyle.Flat;
            grpTaxList.Location = new Point(456, 14);
            grpTaxList.Name = "grpTaxList";
            grpTaxList.Size = new Size(2502, 320);
            grpTaxList.TabIndex = 10;
            grpTaxList.TabStop = false;
            grpTaxList.Text = "Существующие налоги";
            // 
            // ManageTaxationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(707, 346);
            Controls.Add(grpTaxList);
            Controls.Add(grpAddTax);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            Name = "ManageTaxationForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Управление налогами";
            grpAddTax.ResumeLayout(false);
            grpAddTax.PerformLayout();
            grpTaxList.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblTaxName;
        private Label lblTaxRate;
        private Label lblTaxDescription;
        private TextBox txtTaxName;
        private TextBox txtTaxRate;
        private TextBox txtTaxDescription;
        private Button btnAddTax;
        private ListBox lstTaxes;
        private GroupBox grpAddTax;
        private GroupBox grpTaxList;
    }
}