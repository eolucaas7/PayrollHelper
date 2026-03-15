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
            SuspendLayout();
            // 
            // buttonDashBoard
            // 
            buttonDashBoard.FlatStyle = FlatStyle.Popup;
            buttonDashBoard.Location = new Point(31, 12);
            buttonDashBoard.Name = "buttonDashBoard";
            buttonDashBoard.Size = new Size(241, 23);
            buttonDashBoard.TabIndex = 0;
            buttonDashBoard.Text = "Перейти в рабочиее пространство";
            buttonDashBoard.UseVisualStyleBackColor = true;
            buttonDashBoard.Click += buttonDashBoard_Click;
            // 
            // buttonEditDatabase
            // 
            buttonEditDatabase.FlatStyle = FlatStyle.Popup;
            buttonEditDatabase.Location = new Point(31, 57);
            buttonEditDatabase.Name = "buttonEditDatabase";
            buttonEditDatabase.Size = new Size(241, 23);
            buttonEditDatabase.TabIndex = 1;
            buttonEditDatabase.Text = "Перейти к редактированию/просмотру БД";
            buttonEditDatabase.UseVisualStyleBackColor = true;
            buttonEditDatabase.Click += buttonEditDatabase_Click;
            // 
            // buttonReport
            // 
            buttonReport.FlatStyle = FlatStyle.Popup;
            buttonReport.Location = new Point(31, 100);
            buttonReport.Name = "buttonReport";
            buttonReport.Size = new Size(241, 23);
            buttonReport.TabIndex = 2;
            buttonReport.Text = "Перейти к формированию отчетов";
            buttonReport.UseVisualStyleBackColor = true;
            buttonReport.Click += buttonReport_Click;
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(294, 146);
            Controls.Add(buttonReport);
            Controls.Add(buttonEditDatabase);
            Controls.Add(buttonDashBoard);
            Name = "Menu";
            Text = "Меню";
            ResumeLayout(false);
        }

        #endregion

        private Button buttonDashBoard;
        private Button buttonEditDatabase;
        private Button buttonReport;
    }
}