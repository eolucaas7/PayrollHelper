namespace PayrollHelper
{
    partial class EditDatasBaseForm
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
            saveButton = new Button();
            tableSelectorComboBox = new ComboBox();
            comboBoxEmployeeName = new ComboBox();
            dataGridView1 = new DataGridView();
            deleteButton = new Button();
            refreshButton = new Button();
            buttonShowEmployeeInfo = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // saveButton
            // 
            saveButton.FlatStyle = FlatStyle.Popup;
            saveButton.Location = new Point(12, 79);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(151, 23);
            saveButton.TabIndex = 0;
            saveButton.Text = "Сохранить изменения";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            // 
            // tableSelectorComboBox
            // 
            tableSelectorComboBox.FormattingEnabled = true;
            tableSelectorComboBox.Location = new Point(12, 24);
            tableSelectorComboBox.Name = "tableSelectorComboBox";
            tableSelectorComboBox.Size = new Size(236, 23);
            tableSelectorComboBox.TabIndex = 4;
            tableSelectorComboBox.Text = "Выберите таблицу для редактирования";
            tableSelectorComboBox.SelectedIndexChanged += tableSelectorComboBox_SelectedIndexChanged;
            // 
            // comboBoxEmployeeName
            // 
            comboBoxEmployeeName.FormattingEnabled = true;
            comboBoxEmployeeName.Location = new Point(655, 12);
            comboBoxEmployeeName.Name = "comboBoxEmployeeName";
            comboBoxEmployeeName.Size = new Size(228, 23);
            comboBoxEmployeeName.TabIndex = 5;
            comboBoxEmployeeName.Text = "Выберите сотрудника";
            comboBoxEmployeeName.SelectedIndexChanged += comboBoxEmployeeName_SelectedIndexChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 142);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(871, 230);
            dataGridView1.TabIndex = 6;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // deleteButton
            // 
            deleteButton.FlatStyle = FlatStyle.Popup;
            deleteButton.Location = new Point(203, 79);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(146, 23);
            deleteButton.TabIndex = 7;
            deleteButton.Text = "Удалить строку";
            deleteButton.UseVisualStyleBackColor = true;
            deleteButton.Click += deleteButton_Click;
            // 
            // refreshButton
            // 
            refreshButton.FlatStyle = FlatStyle.Popup;
            refreshButton.Location = new Point(387, 79);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(149, 23);
            refreshButton.TabIndex = 8;
            refreshButton.Text = "Обновить данные";
            refreshButton.UseVisualStyleBackColor = true;
            refreshButton.Click += refreshButton_Click;
            // 
            // buttonShowEmployeeInfo
            // 
            buttonShowEmployeeInfo.FlatStyle = FlatStyle.Popup;
            buttonShowEmployeeInfo.Location = new Point(655, 55);
            buttonShowEmployeeInfo.Name = "buttonShowEmployeeInfo";
            buttonShowEmployeeInfo.Size = new Size(228, 23);
            buttonShowEmployeeInfo.TabIndex = 9;
            buttonShowEmployeeInfo.Text = "Показать информацию о сотруднике";
            buttonShowEmployeeInfo.UseVisualStyleBackColor = true;
            buttonShowEmployeeInfo.Click += buttonShowEmployeeInfo_Click;
            // 
            // EditDatasBaseForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(895, 400);
            Controls.Add(buttonShowEmployeeInfo);
            Controls.Add(refreshButton);
            Controls.Add(deleteButton);
            Controls.Add(dataGridView1);
            Controls.Add(comboBoxEmployeeName);
            Controls.Add(tableSelectorComboBox);
            Controls.Add(saveButton);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "EditDatasBaseForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Редактирование/Просмотр Базы Данных";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button saveButton;
        private ComboBox tableSelectorComboBox;
        private ComboBox comboBoxEmployeeName;
        private DataGridView dataGridView1;
        private Button deleteButton;
        private Button refreshButton;
        private Button buttonShowEmployeeInfo;
    }
}