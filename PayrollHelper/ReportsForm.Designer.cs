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
            label1 = new Label();
            label2 = new Label();
            lblCurrentPath = new Label();
            reportTypeComboBox = new ComboBox();
            dateTimePicker1 = new DateTimePicker();
            btnSelectReportFolder = new Button();
            generateAndExportButton = new Button();
            includeBonusesCheckBox = new CheckBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(69, 15);
            label1.TabIndex = 0;
            label1.Text = "Тип отчета:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 57);
            label2.Name = "label2";
            label2.Size = new Size(100, 15);
            label2.TabIndex = 1;
            label2.Text = "Дата отчетности:";
            // 
            // lblCurrentPath
            // 
            lblCurrentPath.AutoSize = true;
            lblCurrentPath.Location = new Point(12, 129);
            lblCurrentPath.Name = "lblCurrentPath";
            lblCurrentPath.Size = new Size(189, 15);
            lblCurrentPath.TabIndex = 2;
            lblCurrentPath.Text = "Текущие расположение отчетов:";
            // 
            // reportTypeComboBox
            // 
            reportTypeComboBox.FormattingEnabled = true;
            reportTypeComboBox.Location = new Point(113, 9);
            reportTypeComboBox.Name = "reportTypeComboBox";
            reportTypeComboBox.Size = new Size(262, 23);
            reportTypeComboBox.TabIndex = 3;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(129, 51);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 4;
            // 
            // btnSelectReportFolder
            // 
            btnSelectReportFolder.FlatStyle = FlatStyle.Popup;
            btnSelectReportFolder.Location = new Point(12, 158);
            btnSelectReportFolder.Name = "btnSelectReportFolder";
            btnSelectReportFolder.Size = new Size(241, 23);
            btnSelectReportFolder.TabIndex = 5;
            btnSelectReportFolder.Text = "Выбрать путь для сохранения отчетов";
            btnSelectReportFolder.UseVisualStyleBackColor = true;
            btnSelectReportFolder.Click += btnSelectReportFolder_Click;
            // 
            // generateAndExportButton
            // 
            generateAndExportButton.FlatStyle = FlatStyle.Popup;
            generateAndExportButton.Location = new Point(12, 201);
            generateAndExportButton.Name = "generateAndExportButton";
            generateAndExportButton.Size = new Size(241, 23);
            generateAndExportButton.TabIndex = 6;
            generateAndExportButton.Text = "Генерировать и экспортировать отчет";
            generateAndExportButton.UseVisualStyleBackColor = true;
            generateAndExportButton.Click += generateAndExportButton_Click;
            // 
            // includeBonusesCheckBox
            // 
            includeBonusesCheckBox.AutoSize = true;
            includeBonusesCheckBox.Location = new Point(12, 93);
            includeBonusesCheckBox.Name = "includeBonusesCheckBox";
            includeBonusesCheckBox.Size = new Size(167, 19);
            includeBonusesCheckBox.TabIndex = 7;
            includeBonusesCheckBox.Text = "Включать бонусы в отчет";
            includeBonusesCheckBox.UseVisualStyleBackColor = true;
            // 
            // ReportsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(387, 244);
            Controls.Add(includeBonusesCheckBox);
            Controls.Add(generateAndExportButton);
            Controls.Add(btnSelectReportFolder);
            Controls.Add(dateTimePicker1);
            Controls.Add(reportTypeComboBox);
            Controls.Add(lblCurrentPath);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "ReportsForm";
            Text = "ReportsForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label lblCurrentPath;
        private ComboBox reportTypeComboBox;
        private DateTimePicker dateTimePicker1;
        private Button btnSelectReportFolder;
        private Button generateAndExportButton;
        private CheckBox includeBonusesCheckBox;
    }
}