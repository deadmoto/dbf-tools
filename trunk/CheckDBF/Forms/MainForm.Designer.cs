namespace CheckDBF.Forms
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckLSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckKDOMVLMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckROPLMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowMessagesMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowLogMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ReplaceMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConformMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFileGroupBox = new System.Windows.Forms.GroupBox();
            this.ProcessTextBox = new System.Windows.Forms.TextBox();
            this.ProcessBar = new System.Windows.Forms.ProgressBar();
            this.ProcessButton = new System.Windows.Forms.Button();
            this.PaymentFileButton = new System.Windows.Forms.Button();
            this.PaymentFileTextBox = new System.Windows.Forms.TextBox();
            this.SupplierFileButton = new System.Windows.Forms.Button();
            this.SupplierFileTextBox = new System.Windows.Forms.TextBox();
            this.FileInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.RecordsTextBox = new System.Windows.Forms.TextBox();
            this.RecordsLabel = new System.Windows.Forms.Label();
            this.MonthTextBox = new System.Windows.Forms.TextBox();
            this.MonthLabel = new System.Windows.Forms.Label();
            this.CodeLabel = new System.Windows.Forms.Label();
            this.CodeTextBox = new System.Windows.Forms.TextBox();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.LogTextBox = new System.Windows.Forms.TextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenu.SuspendLayout();
            this.OpenFileGroupBox.SuspendLayout();
            this.FileInfoGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.EditMenu,
            this.ViewMenu,
            this.ToolsMenu});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(474, 24);
            this.MainMenu.TabIndex = 12;
            this.MainMenu.Text = "menuStrip1";
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitMenuItem});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(48, 20);
            this.FileMenu.Text = "Файл";
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(108, 22);
            this.ExitMenuItem.Text = "Выход";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItemClick);
            // 
            // EditMenu
            // 
            this.EditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CheckLSMenuItem,
            this.CheckKDOMVLMenuItem,
            this.CheckROPLMenuItem,
            this.toolStripSeparator1,
            this.ShowMessagesMenuItem});
            this.EditMenu.Name = "EditMenu";
            this.EditMenu.Size = new System.Drawing.Size(59, 20);
            this.EditMenu.Text = "Правка";
            // 
            // CheckLSMenuItem
            // 
            this.CheckLSMenuItem.Checked = true;
            this.CheckLSMenuItem.CheckOnClick = true;
            this.CheckLSMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckLSMenuItem.Name = "CheckLSMenuItem";
            this.CheckLSMenuItem.Size = new System.Drawing.Size(238, 22);
            this.CheckLSMenuItem.Text = "Проверять лицевые счета";
            // 
            // CheckKDOMVLMenuItem
            // 
            this.CheckKDOMVLMenuItem.Checked = true;
            this.CheckKDOMVLMenuItem.CheckOnClick = true;
            this.CheckKDOMVLMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckKDOMVLMenuItem.Name = "CheckKDOMVLMenuItem";
            this.CheckKDOMVLMenuItem.Size = new System.Drawing.Size(238, 22);
            this.CheckKDOMVLMenuItem.Text = "Проверять вид жилого фонда";
            // 
            // CheckROPLMenuItem
            // 
            this.CheckROPLMenuItem.Checked = true;
            this.CheckROPLMenuItem.CheckOnClick = true;
            this.CheckROPLMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckROPLMenuItem.Name = "CheckROPLMenuItem";
            this.CheckROPLMenuItem.Size = new System.Drawing.Size(238, 22);
            this.CheckROPLMenuItem.Text = "Проверять жилую площадь";
            // 
            // ShowMessagesMenuItem
            // 
            this.ShowMessagesMenuItem.Checked = true;
            this.ShowMessagesMenuItem.CheckOnClick = true;
            this.ShowMessagesMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowMessagesMenuItem.Name = "ShowMessagesMenuItem";
            this.ShowMessagesMenuItem.Size = new System.Drawing.Size(238, 22);
            this.ShowMessagesMenuItem.Text = "Сообщения об ошибках";
            // 
            // ViewMenu
            // 
            this.ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowLogMenuItem});
            this.ViewMenu.Name = "ViewMenu";
            this.ViewMenu.Size = new System.Drawing.Size(39, 20);
            this.ViewMenu.Text = "Вид";
            // 
            // ShowLogMenuItem
            // 
            this.ShowLogMenuItem.CheckOnClick = true;
            this.ShowLogMenuItem.Name = "ShowLogMenuItem";
            this.ShowLogMenuItem.Size = new System.Drawing.Size(118, 22);
            this.ShowLogMenuItem.Text = "Журнал";
            this.ShowLogMenuItem.Click += new System.EventHandler(this.ShowLogMenuItemClick);
            // 
            // ToolsMenu
            // 
            this.ToolsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReplaceMenuItem,
            this.ConformMenuItem});
            this.ToolsMenu.Name = "ToolsMenu";
            this.ToolsMenu.Size = new System.Drawing.Size(59, 20);
            this.ToolsMenu.Text = "Сервис";
            // 
            // ReplaceMenuItem
            // 
            this.ReplaceMenuItem.Name = "ReplaceMenuItem";
            this.ReplaceMenuItem.Size = new System.Drawing.Size(217, 22);
            this.ReplaceMenuItem.Text = "Справочник замены";
            this.ReplaceMenuItem.Click += new System.EventHandler(this.справочникЗаменыToolStripMenuItem_Click);
            // 
            // ConformMenuItem
            // 
            this.ConformMenuItem.Enabled = false;
            this.ConformMenuItem.Name = "ConformMenuItem";
            this.ConformMenuItem.Size = new System.Drawing.Size(217, 22);
            this.ConformMenuItem.Text = "Справочник соответствия";
            // 
            // OpenFileGroupBox
            // 
            this.OpenFileGroupBox.Controls.Add(this.ProcessTextBox);
            this.OpenFileGroupBox.Controls.Add(this.ProcessBar);
            this.OpenFileGroupBox.Controls.Add(this.ProcessButton);
            this.OpenFileGroupBox.Controls.Add(this.PaymentFileButton);
            this.OpenFileGroupBox.Controls.Add(this.PaymentFileTextBox);
            this.OpenFileGroupBox.Controls.Add(this.SupplierFileButton);
            this.OpenFileGroupBox.Controls.Add(this.SupplierFileTextBox);
            this.OpenFileGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.OpenFileGroupBox.Location = new System.Drawing.Point(0, 24);
            this.OpenFileGroupBox.Name = "OpenFileGroupBox";
            this.OpenFileGroupBox.Size = new System.Drawing.Size(474, 93);
            this.OpenFileGroupBox.TabIndex = 13;
            this.OpenFileGroupBox.TabStop = false;
            // 
            // ProcessTextBox
            // 
            this.ProcessTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ProcessTextBox.Location = new System.Drawing.Point(6, 66);
            this.ProcessTextBox.Name = "ProcessTextBox";
            this.ProcessTextBox.ReadOnly = true;
            this.ProcessTextBox.Size = new System.Drawing.Size(53, 20);
            this.ProcessTextBox.TabIndex = 11;
            this.ProcessTextBox.Visible = false;
            // 
            // ProcessBar
            // 
            this.ProcessBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcessBar.Location = new System.Drawing.Point(65, 66);
            this.ProcessBar.Maximum = 19;
            this.ProcessBar.Name = "ProcessBar";
            this.ProcessBar.Size = new System.Drawing.Size(282, 20);
            this.ProcessBar.Step = 1;
            this.ProcessBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.ProcessBar.TabIndex = 10;
            this.ProcessBar.Visible = false;
            // 
            // ProcessButton
            // 
            this.ProcessButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcessButton.Location = new System.Drawing.Point(353, 64);
            this.ProcessButton.Name = "ProcessButton";
            this.ProcessButton.Size = new System.Drawing.Size(115, 23);
            this.ProcessButton.TabIndex = 9;
            this.ProcessButton.Text = "Сравнить";
            this.ProcessButton.UseVisualStyleBackColor = true;
            this.ProcessButton.Click += new System.EventHandler(this.ProcessButtonClick);
            // 
            // PaymentFileButton
            // 
            this.PaymentFileButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PaymentFileButton.Location = new System.Drawing.Point(353, 37);
            this.PaymentFileButton.Name = "PaymentFileButton";
            this.PaymentFileButton.Size = new System.Drawing.Size(115, 23);
            this.PaymentFileButton.TabIndex = 8;
            this.PaymentFileButton.Text = "Файл БУ";
            this.PaymentFileButton.UseVisualStyleBackColor = true;
            this.PaymentFileButton.Click += new System.EventHandler(this.OpenPaymentFileClick);
            // 
            // PaymentFileTextBox
            // 
            this.PaymentFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.PaymentFileTextBox.Location = new System.Drawing.Point(6, 39);
            this.PaymentFileTextBox.Name = "PaymentFileTextBox";
            this.PaymentFileTextBox.ReadOnly = true;
            this.PaymentFileTextBox.Size = new System.Drawing.Size(341, 20);
            this.PaymentFileTextBox.TabIndex = 7;
            // 
            // SupplierFileButton
            // 
            this.SupplierFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SupplierFileButton.Location = new System.Drawing.Point(353, 11);
            this.SupplierFileButton.Name = "SupplierFileButton";
            this.SupplierFileButton.Size = new System.Drawing.Size(115, 23);
            this.SupplierFileButton.TabIndex = 6;
            this.SupplierFileButton.Text = "Файл поставщика";
            this.SupplierFileButton.UseVisualStyleBackColor = true;
            this.SupplierFileButton.Click += new System.EventHandler(this.OpenSupplierFileClick);
            // 
            // SupplierFileTextBox
            // 
            this.SupplierFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.SupplierFileTextBox.Location = new System.Drawing.Point(6, 13);
            this.SupplierFileTextBox.Name = "SupplierFileTextBox";
            this.SupplierFileTextBox.ReadOnly = true;
            this.SupplierFileTextBox.Size = new System.Drawing.Size(341, 20);
            this.SupplierFileTextBox.TabIndex = 4;
            // 
            // FileInfoGroupBox
            // 
            this.FileInfoGroupBox.Controls.Add(this.RecordsTextBox);
            this.FileInfoGroupBox.Controls.Add(this.RecordsLabel);
            this.FileInfoGroupBox.Controls.Add(this.MonthTextBox);
            this.FileInfoGroupBox.Controls.Add(this.MonthLabel);
            this.FileInfoGroupBox.Controls.Add(this.CodeLabel);
            this.FileInfoGroupBox.Controls.Add(this.CodeTextBox);
            this.FileInfoGroupBox.Controls.Add(this.NameTextBox);
            this.FileInfoGroupBox.Controls.Add(this.NameLabel);
            this.FileInfoGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.FileInfoGroupBox.Location = new System.Drawing.Point(0, 117);
            this.FileInfoGroupBox.Name = "FileInfoGroupBox";
            this.FileInfoGroupBox.Size = new System.Drawing.Size(474, 60);
            this.FileInfoGroupBox.TabIndex = 14;
            this.FileInfoGroupBox.TabStop = false;
            // 
            // RecordsTextBox
            // 
            this.RecordsTextBox.Location = new System.Drawing.Point(65, 34);
            this.RecordsTextBox.Name = "RecordsTextBox";
            this.RecordsTextBox.ReadOnly = true;
            this.RecordsTextBox.Size = new System.Drawing.Size(42, 20);
            this.RecordsTextBox.TabIndex = 11;
            // 
            // RecordsLabel
            // 
            this.RecordsLabel.AutoSize = true;
            this.RecordsLabel.Location = new System.Drawing.Point(6, 37);
            this.RecordsLabel.Name = "RecordsLabel";
            this.RecordsLabel.Size = new System.Drawing.Size(53, 13);
            this.RecordsLabel.TabIndex = 10;
            this.RecordsLabel.Text = "Записей:";
            // 
            // MonthTextBox
            // 
            this.MonthTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.MonthTextBox.Location = new System.Drawing.Point(205, 34);
            this.MonthTextBox.Name = "MonthTextBox";
            this.MonthTextBox.ReadOnly = true;
            this.MonthTextBox.Size = new System.Drawing.Size(263, 20);
            this.MonthTextBox.TabIndex = 9;
            // 
            // MonthLabel
            // 
            this.MonthLabel.AutoSize = true;
            this.MonthLabel.Location = new System.Drawing.Point(113, 37);
            this.MonthLabel.Name = "MonthLabel";
            this.MonthLabel.Size = new System.Drawing.Size(43, 13);
            this.MonthLabel.TabIndex = 8;
            this.MonthLabel.Text = "Месяц:";
            // 
            // CodeLabel
            // 
            this.CodeLabel.AutoSize = true;
            this.CodeLabel.Location = new System.Drawing.Point(6, 13);
            this.CodeLabel.Name = "CodeLabel";
            this.CodeLabel.Size = new System.Drawing.Size(29, 13);
            this.CodeLabel.TabIndex = 7;
            this.CodeLabel.Text = "Код:";
            // 
            // CodeTextBox
            // 
            this.CodeTextBox.Location = new System.Drawing.Point(65, 10);
            this.CodeTextBox.Name = "CodeTextBox";
            this.CodeTextBox.ReadOnly = true;
            this.CodeTextBox.Size = new System.Drawing.Size(42, 20);
            this.CodeTextBox.TabIndex = 6;
            // 
            // NameTextBox
            // 
            this.NameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.NameTextBox.Location = new System.Drawing.Point(205, 10);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.ReadOnly = true;
            this.NameTextBox.Size = new System.Drawing.Size(263, 20);
            this.NameTextBox.TabIndex = 3;
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(113, 13);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(86, 13);
            this.NameLabel.TabIndex = 2;
            this.NameLabel.Text = "Наименование:";
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.Text = "Process...";
            this.NotifyIcon.Visible = true;
            // 
            // LogTextBox
            // 
            this.LogTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogTextBox.Location = new System.Drawing.Point(0, 177);
            this.LogTextBox.Multiline = true;
            this.LogTextBox.Name = "LogTextBox";
            this.LogTextBox.ReadOnly = true;
            this.LogTextBox.Size = new System.Drawing.Size(474, 278);
            this.LogTextBox.TabIndex = 22;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(235, 6);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(474, 455);
            this.Controls.Add(this.LogTextBox);
            this.Controls.Add(this.FileInfoGroupBox);
            this.Controls.Add(this.OpenFileGroupBox);
            this.Controls.Add(this.MainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.MainMenu;
            this.MinimumSize = new System.Drawing.Size(480, 28);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CheckDBF";
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.OpenFileGroupBox.ResumeLayout(false);
            this.OpenFileGroupBox.PerformLayout();
            this.FileInfoGroupBox.ResumeLayout(false);
            this.FileInfoGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem FileMenu;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditMenu;
        private System.Windows.Forms.ToolStripMenuItem ViewMenu;
        private System.Windows.Forms.ToolStripMenuItem ShowLogMenuItem;
        private System.Windows.Forms.GroupBox OpenFileGroupBox;
        private System.Windows.Forms.Button ProcessButton;
        private System.Windows.Forms.Button PaymentFileButton;
        private System.Windows.Forms.TextBox PaymentFileTextBox;
        private System.Windows.Forms.Button SupplierFileButton;
        private System.Windows.Forms.TextBox SupplierFileTextBox;
        private System.Windows.Forms.GroupBox FileInfoGroupBox;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.TextBox RecordsTextBox;
        private System.Windows.Forms.Label RecordsLabel;
        private System.Windows.Forms.TextBox MonthTextBox;
        private System.Windows.Forms.Label MonthLabel;
        private System.Windows.Forms.Label CodeLabel;
        private System.Windows.Forms.TextBox CodeTextBox;
        private System.Windows.Forms.ProgressBar ProcessBar;
        private System.Windows.Forms.ToolStripMenuItem CheckLSMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CheckKDOMVLMenuItem;
        private System.Windows.Forms.TextBox ProcessTextBox;
        private System.Windows.Forms.ToolStripMenuItem CheckROPLMenuItem;
        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem ShowMessagesMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolsMenu;
        private System.Windows.Forms.ToolStripMenuItem ReplaceMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ConformMenuItem;
        private System.Windows.Forms.TextBox LogTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

