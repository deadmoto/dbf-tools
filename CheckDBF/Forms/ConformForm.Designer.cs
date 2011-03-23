namespace CheckDBF.Forms
{
    partial class ConformForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConformForm));
            this.SearchPanel = new System.Windows.Forms.Panel();
            this.KOD_NTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.KOD_TTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.VIDTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.PREDTextBox = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.GridView = new System.Windows.Forms.DataGridView();
            this.PRED = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KOD_T = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TARIF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KOD_N = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VOL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SearchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).BeginInit();
            this.SuspendLayout();
            // 
            // SearchPanel
            // 
            this.SearchPanel.Controls.Add(this.KOD_NTextBox);
            this.SearchPanel.Controls.Add(this.label4);
            this.SearchPanel.Controls.Add(this.KOD_TTextBox);
            this.SearchPanel.Controls.Add(this.label3);
            this.SearchPanel.Controls.Add(this.VIDTextBox);
            this.SearchPanel.Controls.Add(this.label2);
            this.SearchPanel.Controls.Add(this.label1);
            this.SearchPanel.Controls.Add(this.SearchButton);
            this.SearchPanel.Controls.Add(this.PREDTextBox);
            this.SearchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.SearchPanel.Location = new System.Drawing.Point(0, 0);
            this.SearchPanel.Name = "SearchPanel";
            this.SearchPanel.Size = new System.Drawing.Size(624, 48);
            this.SearchPanel.TabIndex = 0;
            // 
            // KOD_NTextBox
            // 
            this.KOD_NTextBox.Location = new System.Drawing.Point(297, 14);
            this.KOD_NTextBox.Name = "KOD_NTextBox";
            this.KOD_NTextBox.Size = new System.Drawing.Size(31, 20);
            this.KOD_NTextBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(247, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "KOD_N";
            // 
            // KOD_TTextBox
            // 
            this.KOD_TTextBox.Location = new System.Drawing.Point(209, 14);
            this.KOD_TTextBox.Name = "KOD_TTextBox";
            this.KOD_TTextBox.Size = new System.Drawing.Size(31, 20);
            this.KOD_TTextBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(160, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "KOD_T";
            // 
            // VIDTextBox
            // 
            this.VIDTextBox.Location = new System.Drawing.Point(123, 14);
            this.VIDTextBox.Name = "VIDTextBox";
            this.VIDTextBox.Size = new System.Drawing.Size(31, 20);
            this.VIDTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(92, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "VID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "PRED";
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(537, 12);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 4;
            this.SearchButton.Text = "Поиск";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButtonClick);
            // 
            // PREDTextBox
            // 
            this.PREDTextBox.Location = new System.Drawing.Point(55, 14);
            this.PREDTextBox.Name = "PREDTextBox";
            this.PREDTextBox.Size = new System.Drawing.Size(31, 20);
            this.PREDTextBox.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBox1.Location = new System.Drawing.Point(0, 345);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(624, 97);
            this.textBox1.TabIndex = 2;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // GridView
            // 
            this.GridView.AllowUserToAddRows = false;
            this.GridView.AllowUserToDeleteRows = false;
            this.GridView.AllowUserToResizeColumns = false;
            this.GridView.AllowUserToResizeRows = false;
            this.GridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.GridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PRED,
            this.VID,
            this.KOD_T,
            this.TARIF,
            this.KOD_N,
            this.VOL,
            this.NAME});
            this.GridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridView.Location = new System.Drawing.Point(0, 48);
            this.GridView.MultiSelect = false;
            this.GridView.Name = "GridView";
            this.GridView.ReadOnly = true;
            this.GridView.RowHeadersVisible = false;
            this.GridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GridView.Size = new System.Drawing.Size(624, 297);
            this.GridView.TabIndex = 1;
            // 
            // PRED
            // 
            this.PRED.HeaderText = "PRED";
            this.PRED.Name = "PRED";
            this.PRED.ReadOnly = true;
            this.PRED.Width = 62;
            // 
            // VID
            // 
            this.VID.HeaderText = "VID";
            this.VID.Name = "VID";
            this.VID.ReadOnly = true;
            this.VID.Width = 50;
            // 
            // KOD_T
            // 
            this.KOD_T.HeaderText = "KOD_T";
            this.KOD_T.Name = "KOD_T";
            this.KOD_T.ReadOnly = true;
            this.KOD_T.Width = 68;
            // 
            // TARIF
            // 
            this.TARIF.HeaderText = "TARIF";
            this.TARIF.Name = "TARIF";
            this.TARIF.ReadOnly = true;
            this.TARIF.Width = 63;
            // 
            // KOD_N
            // 
            this.KOD_N.HeaderText = "KOD_N";
            this.KOD_N.Name = "KOD_N";
            this.KOD_N.ReadOnly = true;
            this.KOD_N.Width = 69;
            // 
            // VOL
            // 
            this.VOL.HeaderText = "VOL";
            this.VOL.Name = "VOL";
            this.VOL.ReadOnly = true;
            this.VOL.Width = 53;
            // 
            // NAME
            // 
            this.NAME.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NAME.HeaderText = "NAME";
            this.NAME.Name = "NAME";
            this.NAME.ReadOnly = true;
            // 
            // ConformForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.GridView);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.SearchPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ConformForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Справочник соответствия";
            this.SearchPanel.ResumeLayout(false);
            this.SearchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel SearchPanel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox KOD_NTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox KOD_TTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox VIDTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TextBox PREDTextBox;
        private System.Windows.Forms.DataGridView GridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRED;
        private System.Windows.Forms.DataGridViewTextBoxColumn VID;
        private System.Windows.Forms.DataGridViewTextBoxColumn KOD_T;
        private System.Windows.Forms.DataGridViewTextBoxColumn TARIF;
        private System.Windows.Forms.DataGridViewTextBoxColumn KOD_N;
        private System.Windows.Forms.DataGridViewTextBoxColumn VOL;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME;
    }
}