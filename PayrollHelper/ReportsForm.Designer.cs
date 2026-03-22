namespace PayrollHelper
{
    partial class ReportsForm
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
            dtpPeriod = new DateTimePicker();
            btnSelectReportFolder = new Button();
            generateAndExportButton = new Button();
            reportTypeComboBox = new ComboBox();
            lblReportType = new Label();
            lblCurrentPath = new Label();
            lblDirectory = new Label();
            grpReportParams = new GroupBox();
            lblPaymentTypes = new Label();
            clbPaymentTypes = new CheckedListBox();
            cmbFormat = new ComboBox();
            lblFormat = new Label();
            lblPeriod = new Label();
            grpSave = new GroupBox();
            grpReportParams.SuspendLayout();
            grpSave.SuspendLayout();
            SuspendLayout();
            // 
            // dtpPeriod
            // 
            dtpPeriod.CustomFormat = "MMMM yyyy";
            dtpPeriod.Format = DateTimePickerFormat.Custom;
            dtpPeriod.Location = new Point(106, 65);
            dtpPeriod.Name = "dtpPeriod";
            dtpPeriod.ShowUpDown = true;
            dtpPeriod.Size = new Size(150, 23);
            dtpPeriod.TabIndex = 4;
            // 
            // btnSelectReportFolder
            // 
            btnSelectReportFolder.BackColor = Color.Khaki;
            btnSelectReportFolder.Cursor = Cursors.Hand;
            btnSelectReportFolder.FlatStyle = FlatStyle.Flat;
            btnSelectReportFolder.Location = new Point(78, 64);
            btnSelectReportFolder.Name = "btnSelectReportFolder";
            btnSelectReportFolder.Size = new Size(150, 30);
            btnSelectReportFolder.TabIndex = 5;
            btnSelectReportFolder.Text = "📁 Выбрать папку";
            btnSelectReportFolder.UseVisualStyleBackColor = false;
            btnSelectReportFolder.Click += btnSelectReportFolder_Click;
            // 
            // generateAndExportButton
            // 
            generateAndExportButton.BackColor = Color.White;
            generateAndExportButton.Cursor = Cursors.Hand;
            generateAndExportButton.FlatStyle = FlatStyle.Flat;
            generateAndExportButton.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            generateAndExportButton.Location = new Point(160, 499);
            generateAndExportButton.Name = "generateAndExportButton";
            generateAndExportButton.Size = new Size(180, 30);
            generateAndExportButton.TabIndex = 6;
            generateAndExportButton.Text = "📄 Сформировать отчет";
            generateAndExportButton.UseVisualStyleBackColor = false;
            generateAndExportButton.Click += generateAndExportButton_Click;
            // 
            // reportTypeComboBox
            // 
            reportTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            reportTypeComboBox.FlatStyle = FlatStyle.Flat;
            reportTypeComboBox.FormattingEnabled = true;
            reportTypeComboBox.Items.AddRange(new object[] { "Зарплатная ведомость ", "Премиальные выплаты ", "Особые суммы начисления" });
            reportTypeComboBox.Location = new Point(106, 22);
            reportTypeComboBox.Name = "reportTypeComboBox";
            reportTypeComboBox.Size = new Size(250, 23);
            reportTypeComboBox.TabIndex = 3;
            // 
            // lblReportType
            // 
            lblReportType.FlatStyle = FlatStyle.Flat;
            lblReportType.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblReportType.Location = new Point(10, 19);
            lblReportType.Name = "lblReportType";
            lblReportType.Size = new Size(80, 23);
            lblReportType.TabIndex = 0;
            lblReportType.Text = "Тип отчета:";
            lblReportType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblCurrentPath
            // 
            lblCurrentPath.BackColor = Color.White;
            lblCurrentPath.BorderStyle = BorderStyle.FixedSingle;
            lblCurrentPath.FlatStyle = FlatStyle.Flat;
            lblCurrentPath.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblCurrentPath.Location = new Point(78, 19);
            lblCurrentPath.Name = "lblCurrentPath";
            lblCurrentPath.Size = new Size(380, 25);
            lblCurrentPath.TabIndex = 2;
            lblCurrentPath.Text = "Текущий путь к отчетам:";
            lblCurrentPath.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblDirectory
            // 
            lblDirectory.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblDirectory.Location = new Point(6, 19);
            lblDirectory.Name = "lblDirectory";
            lblDirectory.Size = new Size(62, 25);
            lblDirectory.TabIndex = 8;
            lblDirectory.Text = "Папка:";
            lblDirectory.TextAlign = ContentAlignment.MiddleRight;
            // 
            // grpReportParams
            // 
            grpReportParams.Controls.Add(lblPaymentTypes);
            grpReportParams.Controls.Add(clbPaymentTypes);
            grpReportParams.Controls.Add(cmbFormat);
            grpReportParams.Controls.Add(lblFormat);
            grpReportParams.Controls.Add(lblPeriod);
            grpReportParams.Controls.Add(lblReportType);
            grpReportParams.Controls.Add(reportTypeComboBox);
            grpReportParams.Controls.Add(dtpPeriod);
            grpReportParams.FlatStyle = FlatStyle.Flat;
            grpReportParams.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpReportParams.Location = new Point(12, 12);
            grpReportParams.Name = "grpReportParams";
            grpReportParams.Size = new Size(520, 311);
            grpReportParams.TabIndex = 9;
            grpReportParams.TabStop = false;
            grpReportParams.Text = "Параметры отчета";
            // 
            // lblPaymentTypes
            // 
            lblPaymentTypes.FlatStyle = FlatStyle.Flat;
            lblPaymentTypes.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblPaymentTypes.Location = new Point(10, 152);
            lblPaymentTypes.Name = "lblPaymentTypes";
            lblPaymentTypes.Size = new Size(164, 23);
            lblPaymentTypes.TabIndex = 10;
            lblPaymentTypes.Text = "Включать типы выплат:";
            lblPaymentTypes.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // clbPaymentTypes
            // 
            clbPaymentTypes.BackColor = Color.White;
            clbPaymentTypes.BorderStyle = BorderStyle.FixedSingle;
            clbPaymentTypes.CheckOnClick = true;
            clbPaymentTypes.FormattingEnabled = true;
            clbPaymentTypes.IntegralHeight = false;
            clbPaymentTypes.Location = new Point(6, 184);
            clbPaymentTypes.Name = "clbPaymentTypes";
            clbPaymentTypes.ScrollAlwaysVisible = true;
            clbPaymentTypes.Size = new Size(504, 120);
            clbPaymentTypes.TabIndex = 9;
            // 
            // cmbFormat
            // 
            cmbFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFormat.FlatStyle = FlatStyle.Flat;
            cmbFormat.FormattingEnabled = true;
            cmbFormat.Items.AddRange(new object[] { "Текстовый (.txt)", "CSV (.csv)" });
            cmbFormat.Location = new Point(121, 107);
            cmbFormat.Name = "cmbFormat";
            cmbFormat.Size = new Size(150, 23);
            cmbFormat.TabIndex = 6;
            // 
            // lblFormat
            // 
            lblFormat.FlatStyle = FlatStyle.Flat;
            lblFormat.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblFormat.Location = new Point(10, 105);
            lblFormat.Name = "lblFormat";
            lblFormat.Size = new Size(105, 23);
            lblFormat.TabIndex = 5;
            lblFormat.Text = "Формат файла:";
            lblFormat.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblPeriod
            // 
            lblPeriod.FlatStyle = FlatStyle.Flat;
            lblPeriod.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblPeriod.Location = new Point(10, 56);
            lblPeriod.Name = "lblPeriod";
            lblPeriod.Size = new Size(80, 23);
            lblPeriod.TabIndex = 4;
            lblPeriod.Text = "Период:";
            lblPeriod.TextAlign = ContentAlignment.MiddleRight;
            // 
            // grpSave
            // 
            grpSave.Controls.Add(lblDirectory);
            grpSave.Controls.Add(lblCurrentPath);
            grpSave.Controls.Add(btnSelectReportFolder);
            grpSave.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpSave.Location = new Point(12, 342);
            grpSave.Name = "grpSave";
            grpSave.Size = new Size(520, 151);
            grpSave.TabIndex = 10;
            grpSave.TabStop = false;
            grpSave.Text = "Сохранение";
            // 
            // ReportsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 541);
            Controls.Add(grpSave);
            Controls.Add(grpReportParams);
            Controls.Add(generateAndExportButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            KeyPreview = true;
            MaximizeBox = false;
            Name = "ReportsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Формирование отчетов";
            grpReportParams.ResumeLayout(false);
            grpSave.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private DateTimePicker dtpPeriod;
        private Button btnSelectReportFolder;
        private Button generateAndExportButton;
        private ComboBox reportTypeComboBox;
        private Label lblReportType;
        private Label lblCurrentPath;
        private Label lblDirectory;
        private GroupBox grpReportParams;
        private Label lblPeriod;
        private ComboBox cmbFormat;
        private Label lblFormat;
        private GroupBox grpSave;
        private Label lblPaymentTypes;
        private CheckedListBox clbPaymentTypes;
    }
}