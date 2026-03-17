namespace PayrollHelper
{
    partial class Menu
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
            buttonDashBoard = new Button();
            buttonEditDatabase = new Button();
            buttonReport = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // buttonDashBoard
            // 
            buttonDashBoard.BackColor = Color.LightBlue;
            buttonDashBoard.Cursor = Cursors.Hand;
            buttonDashBoard.FlatStyle = FlatStyle.Flat;
            buttonDashBoard.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            buttonDashBoard.Location = new Point(100, 70);
            buttonDashBoard.Name = "buttonDashBoard";
            buttonDashBoard.Size = new Size(200, 40);
            buttonDashBoard.TabIndex = 0;
            buttonDashBoard.Text = "Управление персоналом";
            buttonDashBoard.UseVisualStyleBackColor = false;
            buttonDashBoard.Click += buttonDashBoard_Click;
            // 
            // buttonEditDatabase
            // 
            buttonEditDatabase.BackColor = Color.LightGreen;
            buttonEditDatabase.Cursor = Cursors.Hand;
            buttonEditDatabase.FlatStyle = FlatStyle.Flat;
            buttonEditDatabase.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            buttonEditDatabase.Location = new Point(100, 120);
            buttonEditDatabase.Name = "buttonEditDatabase";
            buttonEditDatabase.Size = new Size(200, 40);
            buttonEditDatabase.TabIndex = 1;
            buttonEditDatabase.Text = "Редактирование БД";
            buttonEditDatabase.UseVisualStyleBackColor = false;
            buttonEditDatabase.Click += buttonEditDatabase_Click;
            // 
            // buttonReport
            // 
            buttonReport.BackColor = Color.LightYellow;
            buttonReport.Cursor = Cursors.Hand;
            buttonReport.FlatStyle = FlatStyle.Flat;
            buttonReport.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            buttonReport.Location = new Point(100, 166);
            buttonReport.Name = "buttonReport";
            buttonReport.Size = new Size(200, 40);
            buttonReport.TabIndex = 2;
            buttonReport.Text = "Формирование отчетов";
            buttonReport.UseVisualStyleBackColor = false;
            buttonReport.Click += buttonReport_Click;
            // 
            // label1
            // 
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.DarkBlue;
            label1.Location = new Point(100, 20);
            label1.Name = "label1";
            label1.Size = new Size(200, 30);
            label1.TabIndex = 3;
            label1.Text = "PayrollHelper";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(384, 261);
            Controls.Add(label1);
            Controls.Add(buttonReport);
            Controls.Add(buttonEditDatabase);
            Controls.Add(buttonDashBoard);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Menu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Главное меню";
            ResumeLayout(false);
        }

        #endregion

        private Button buttonDashBoard;
        private Button buttonEditDatabase;
        private Button buttonReport;
        private Label label1;
    }
}