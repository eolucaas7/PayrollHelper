namespace PayrollHelper
{
    partial class EditDatasBaseForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            grpTable = new GroupBox();
            lblSelectTable = new Label();
            tableSelectorComboBox = new ComboBox();
            refreshButton = new Button();
            deleteButton = new Button();
            saveButton = new Button();
            grpEmployee = new GroupBox();
            lblSelectEmployee = new Label();
            comboBoxEmployeeName = new ComboBox();
            buttonShowEmployeeInfo = new Button();
            dgvTables = new DataGridView();
            statusStrip1 = new StatusStrip();
            lblRecordCount = new ToolStripStatusLabel();
            separator = new ToolStripStatusLabel();
            lblCurrentTable = new ToolStripStatusLabel();
            grpTable.SuspendLayout();
            grpEmployee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTables).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // grpTable
            // 
            grpTable.BackColor = Color.Transparent;
            grpTable.Controls.Add(saveButton);
            grpTable.Controls.Add(tableSelectorComboBox);
            grpTable.Controls.Add(deleteButton);
            grpTable.Controls.Add(lblSelectTable);
            grpTable.Controls.Add(refreshButton);
            grpTable.FlatStyle = FlatStyle.Flat;
            grpTable.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpTable.ForeColor = SystemColors.WindowText;
            grpTable.Location = new Point(12, 12);
            grpTable.Name = "grpTable";
            grpTable.Size = new Size(700, 100);
            grpTable.TabIndex = 0;
            grpTable.TabStop = false;
            grpTable.Text = "Работа с таблицей";
            // 
            // lblSelectTable
            // 
            lblSelectTable.ForeColor = SystemColors.WindowText;
            lblSelectTable.Location = new Point(10, 25);
            lblSelectTable.Name = "lblSelectTable";
            lblSelectTable.Size = new Size(120, 23);
            lblSelectTable.TabIndex = 0;
            lblSelectTable.Text = "Выберите таблицу:";
            lblSelectTable.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tableSelectorComboBox
            // 
            tableSelectorComboBox.BackColor = Color.White;
            tableSelectorComboBox.Cursor = Cursors.Hand;
            tableSelectorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            tableSelectorComboBox.FlatStyle = FlatStyle.Flat;
            tableSelectorComboBox.FormattingEnabled = true;
            tableSelectorComboBox.Location = new Point(150, 26);
            tableSelectorComboBox.Name = "tableSelectorComboBox";
            tableSelectorComboBox.Size = new Size(205, 23);
            tableSelectorComboBox.TabIndex = 1;
            // 
            // refreshButton
            // 
            refreshButton.BackColor = Color.LightBlue;
            refreshButton.Cursor = Cursors.Hand;
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.ForeColor = Color.Black;
            refreshButton.Location = new Point(10, 64);
            refreshButton.Name = "refreshButton";
            refreshButton.Size = new Size(100, 30);
            refreshButton.TabIndex = 1;
            refreshButton.Text = "⟳ Обновить";
            refreshButton.UseVisualStyleBackColor = false;
            // 
            // deleteButton
            // 
            deleteButton.BackColor = Color.LightCoral;
            deleteButton.Cursor = Cursors.Hand;
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.ForeColor = Color.Black;
            deleteButton.Location = new Point(325, 64);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(120, 30);
            deleteButton.TabIndex = 3;
            deleteButton.Text = "🗑 Удалить строку";
            deleteButton.UseVisualStyleBackColor = false;
            // 
            // saveButton
            // 
            saveButton.BackColor = Color.LightGreen;
            saveButton.Cursor = Cursors.Hand;
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.ForeColor = Color.Black;
            saveButton.Location = new Point(141, 64);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(166, 30);
            saveButton.TabIndex = 4;
            saveButton.Text = "💾 Сохранить изменения";
            saveButton.UseVisualStyleBackColor = false;
            // 
            // grpEmployee
            // 
            grpEmployee.BackColor = Color.Transparent;
            grpEmployee.Controls.Add(buttonShowEmployeeInfo);
            grpEmployee.Controls.Add(comboBoxEmployeeName);
            grpEmployee.Controls.Add(lblSelectEmployee);
            grpEmployee.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpEmployee.ForeColor = SystemColors.WindowText;
            grpEmployee.Location = new Point(12, 120);
            grpEmployee.Name = "grpEmployee";
            grpEmployee.Size = new Size(760, 80);
            grpEmployee.TabIndex = 1;
            grpEmployee.TabStop = false;
            grpEmployee.Text = "Информация о сотруднике";
            // 
            // lblSelectEmployee
            // 
            lblSelectEmployee.ForeColor = SystemColors.WindowText;
            lblSelectEmployee.Location = new Point(10, 25);
            lblSelectEmployee.Name = "lblSelectEmployee";
            lblSelectEmployee.Size = new Size(80, 23);
            lblSelectEmployee.TabIndex = 0;
            lblSelectEmployee.Text = "Сотрудник:";
            lblSelectEmployee.TextAlign = ContentAlignment.MiddleRight;
            // 
            // comboBoxEmployeeName
            // 
            comboBoxEmployeeName.BackColor = Color.White;
            comboBoxEmployeeName.Cursor = Cursors.Hand;
            comboBoxEmployeeName.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEmployeeName.FlatStyle = FlatStyle.Flat;
            comboBoxEmployeeName.FormattingEnabled = true;
            comboBoxEmployeeName.Location = new Point(107, 26);
            comboBoxEmployeeName.Name = "comboBoxEmployeeName";
            comboBoxEmployeeName.Size = new Size(300, 23);
            comboBoxEmployeeName.TabIndex = 1;
            // 
            // buttonShowEmployeeInfo
            // 
            buttonShowEmployeeInfo.BackColor = Color.LightYellow;
            buttonShowEmployeeInfo.Cursor = Cursors.Hand;
            buttonShowEmployeeInfo.FlatStyle = FlatStyle.Flat;
            buttonShowEmployeeInfo.ForeColor = Color.Black;
            buttonShowEmployeeInfo.Location = new Point(410, 20);
            buttonShowEmployeeInfo.Name = "buttonShowEmployeeInfo";
            buttonShowEmployeeInfo.Size = new Size(200, 30);
            buttonShowEmployeeInfo.TabIndex = 2;
            buttonShowEmployeeInfo.Text = "👤 Показать информацию";
            buttonShowEmployeeInfo.UseVisualStyleBackColor = false;
            // 
            // dgvTables
            // 
            dgvTables.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTables.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTables.BackgroundColor = Color.White;
            dgvTables.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTables.GridColor = Color.LightGray;
            dgvTables.Location = new Point(12, 210);
            dgvTables.MultiSelect = false;
            dgvTables.Name = "dgvTables";
            dgvTables.RowTemplate.Height = 25;
            dgvTables.Size = new Size(760, 289);
            dgvTables.TabIndex = 2;
            // 
            // statusStrip1
            // 
            statusStrip1.AutoSize = false;
            statusStrip1.BackColor = SystemColors.ControlLight;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblRecordCount, separator, lblCurrentTable });
            statusStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            statusStrip1.Location = new Point(0, 486);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.ShowItemToolTips = true;
            statusStrip1.Size = new Size(784, 25);
            statusStrip1.SizingGrip = false;
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";            
            // lblRecordCount
            // 
            lblRecordCount.Name = "lblRecordCount";
            lblRecordCount.Size = new Size(65, 20);
            lblRecordCount.Text = "Записей: 0";
            lblRecordCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // separator
            // 
            separator.Name = "separator";
            separator.Size = new Size(28, 20);
            separator.Text = "   |   ";
            // 
            // lblCurrentTable
            // 
            lblCurrentTable.Name = "lblCurrentTable";
            lblCurrentTable.Size = new Size(59, 20);
            lblCurrentTable.Spring = true;
            lblCurrentTable.Text = "Таблица: ";
            lblCurrentTable.TextAlign = ContentAlignment.MiddleRight;
            // 
            // EditDatasBaseForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 511);
            Controls.Add(statusStrip1);
            Controls.Add(dgvTables);
            Controls.Add(grpEmployee);
            Controls.Add(grpTable);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "EditDatasBaseForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Редактирование базы данных";
            grpTable.ResumeLayout(false);
            grpEmployee.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTables).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpTable;
        private ComboBox tableSelectorComboBox;
        private Label lblSelectTable;
        private Button refreshButton;
        private Button saveButton;
        private Button deleteButton;
        private GroupBox grpEmployee;
        private Button buttonShowEmployeeInfo;
        private ComboBox comboBoxEmployeeName;
        private Label lblSelectEmployee;
        private DataGridView dgvTables;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblRecordCount;
        private ToolStripStatusLabel separator;
        private ToolStripStatusLabel lblCurrentTable;
    }
}
