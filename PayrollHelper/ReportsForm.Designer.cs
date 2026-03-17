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
            lblReportType = new Label();
            lblSelectDate = new Label();
            lblCurrentPath = new Label();
            reportTypeComboBox = new ComboBox();
            dtpReportDate = new DateTimePicker();
            btnSelectReportFolder = new Button();
            generateAndExportButton = new Button();
            includeBonusesCheckBox = new CheckBox();
            lblDirectory = new Label();
            SuspendLayout();
            // 
            // lblReportType
            // 
            lblReportType.FlatStyle = FlatStyle.Flat;
            lblReportType.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblReportType.Location = new Point(12, 9);
            lblReportType.Name = "lblReportType";
            lblReportType.Size = new Size(86, 15);
            lblReportType.TabIndex = 0;
            lblReportType.Text = "Вид отчета:";
            lblReportType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblSelectDate
            // 
            lblSelectDate.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblSelectDate.Location = new Point(8, 50);
            lblSelectDate.Name = "lblSelectDate";
            lblSelectDate.Size = new Size(108, 23);
            lblSelectDate.TabIndex = 1;
            lblSelectDate.Text = "Выберите дату:";
            lblSelectDate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblCurrentPath
            // 
            lblCurrentPath.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblCurrentPath.Location = new Point(104, 116);
            lblCurrentPath.Name = "lblCurrentPath";
            lblCurrentPath.Size = new Size(178, 25);
            lblCurrentPath.TabIndex = 2;
            lblCurrentPath.Text = "Текущий путь к отчетам:";
            lblCurrentPath.TextAlign = ContentAlignment.MiddleRight;
            // 
            // reportTypeComboBox
            // 
            reportTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            reportTypeComboBox.FormattingEnabled = true;
            reportTypeComboBox.Location = new Point(122, 12);
            reportTypeComboBox.Name = "reportTypeComboBox";
            reportTypeComboBox.Size = new Size(200, 23);
            reportTypeComboBox.TabIndex = 3;
            // 
            // dtpReportDate
            // 
            dtpReportDate.Format = DateTimePickerFormat.Short;
            dtpReportDate.Location = new Point(122, 49);
            dtpReportDate.Name = "dtpReportDate";
            dtpReportDate.Size = new Size(200, 23);
            dtpReportDate.TabIndex = 4;
            // 
            // btnSelectReportFolder
            // 
            btnSelectReportFolder.BackColor = Color.LightGray;
            btnSelectReportFolder.Cursor = Cursors.Hand;
            btnSelectReportFolder.FlatStyle = FlatStyle.Flat;
            btnSelectReportFolder.Location = new Point(12, 158);
            btnSelectReportFolder.Name = "btnSelectReportFolder";
            btnSelectReportFolder.Size = new Size(150, 30);
            btnSelectReportFolder.TabIndex = 5;
            btnSelectReportFolder.Text = "Указать расположение";
            btnSelectReportFolder.UseVisualStyleBackColor = false;
            btnSelectReportFolder.Click += btnSelectReportFolder_Click;
            // 
            // generateAndExportButton
            // 
            generateAndExportButton.BackColor = Color.LightBlue;
            generateAndExportButton.Cursor = Cursors.Hand;
            generateAndExportButton.FlatStyle = FlatStyle.Flat;
            generateAndExportButton.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            generateAndExportButton.Location = new Point(183, 158);
            generateAndExportButton.Name = "generateAndExportButton";
            generateAndExportButton.Size = new Size(167, 30);
            generateAndExportButton.TabIndex = 6;
            generateAndExportButton.Text = "Сформировать отчет";
            generateAndExportButton.UseVisualStyleBackColor = false;
            generateAndExportButton.Click += generateAndExportButton_Click;
            // 
            // includeBonusesCheckBox
            // 
            includeBonusesCheckBox.FlatStyle = FlatStyle.Flat;
            includeBonusesCheckBox.Location = new Point(12, 88);
            includeBonusesCheckBox.Name = "includeBonusesCheckBox";
            includeBonusesCheckBox.Size = new Size(241, 25);
            includeBonusesCheckBox.TabIndex = 7;
            includeBonusesCheckBox.Text = "Включить дополнительные выплаты";
            includeBonusesCheckBox.UseVisualStyleBackColor = true;
            // 
            // lblDirectory
            // 
            lblDirectory.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblDirectory.Location = new Point(8, 116);
            lblDirectory.Name = "lblDirectory";
            lblDirectory.Size = new Size(90, 25);
            lblDirectory.TabIndex = 8;
            lblDirectory.Text = "Директория";
            lblDirectory.TextAlign = ContentAlignment.MiddleRight;
            // 
            // ReportsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(378, 214);
            Controls.Add(lblDirectory);
            Controls.Add(includeBonusesCheckBox);
            Controls.Add(generateAndExportButton);
            Controls.Add(btnSelectReportFolder);
            Controls.Add(dtpReportDate);
            Controls.Add(reportTypeComboBox);
            Controls.Add(lblCurrentPath);
            Controls.Add(lblSelectDate);
            Controls.Add(lblReportType);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ReportsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Генерация отчетов";
            ResumeLayout(false);
        }

        #endregion

        private Label lblReportType;
        private Label lblSelectDate;
        private Label lblCurrentPath;
        private ComboBox reportTypeComboBox;
        private DateTimePicker dtpReportDate;
        private Button btnSelectReportFolder;
        private Button generateAndExportButton;
        private CheckBox includeBonusesCheckBox;
        private Label lblDirectory;
    }
}