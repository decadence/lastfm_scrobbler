namespace Last.fm
{
    partial class FrmExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExport));
            this.btnChoosePath = new System.Windows.Forms.Button();
            this.chBFilterByArtist = new System.Windows.Forms.CheckBox();
            this.tbFilterByArtist = new System.Windows.Forms.TextBox();
            this.chBFilterByPages = new System.Windows.Forms.CheckBox();
            this.nUPFilterByPages = new System.Windows.Forms.NumericUpDown();
            this.tbName = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.mainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cbMode = new System.Windows.Forms.ComboBox();
            this.chbAutoOpen = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tmrMain = new System.Windows.Forms.Timer(this.components);
            this.chBSeparate = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nUPFilterByPages)).BeginInit();
            this.SuspendLayout();
            // 
            // btnChoosePath
            // 
            this.btnChoosePath.Location = new System.Drawing.Point(55, 3);
            this.btnChoosePath.Name = "btnChoosePath";
            this.btnChoosePath.Size = new System.Drawing.Size(93, 23);
            this.btnChoosePath.TabIndex = 0;
            this.btnChoosePath.Text = "Выбрать файл";
            this.mainToolTip.SetToolTip(this.btnChoosePath, "Выбрать файл, куда будут записаны данные или данные будут записаны в файл default" +
                    ".txt в папке программы");
            this.btnChoosePath.UseVisualStyleBackColor = true;
            this.btnChoosePath.Click += new System.EventHandler(this.btnChoosePath_Click);
            // 
            // chBFilterByArtist
            // 
            this.chBFilterByArtist.AutoSize = true;
            this.chBFilterByArtist.Location = new System.Drawing.Point(26, 82);
            this.chBFilterByArtist.Name = "chBFilterByArtist";
            this.chBFilterByArtist.Size = new System.Drawing.Size(151, 17);
            this.chBFilterByArtist.TabIndex = 1;
            this.chBFilterByArtist.Text = "Фильтр по исполнителю";
            this.mainToolTip.SetToolTip(this.chBFilterByArtist, "Будут выведены только песни этого исполнителя");
            this.chBFilterByArtist.UseVisualStyleBackColor = true;
            this.chBFilterByArtist.CheckedChanged += new System.EventHandler(this.chBFilterByArtist_CheckedChanged);
            // 
            // tbFilterByArtist
            // 
            this.tbFilterByArtist.Enabled = false;
            this.tbFilterByArtist.Location = new System.Drawing.Point(51, 58);
            this.tbFilterByArtist.Name = "tbFilterByArtist";
            this.tbFilterByArtist.Size = new System.Drawing.Size(100, 20);
            this.tbFilterByArtist.TabIndex = 2;
            // 
            // chBFilterByPages
            // 
            this.chBFilterByPages.AutoSize = true;
            this.chBFilterByPages.Location = new System.Drawing.Point(9, 131);
            this.chBFilterByPages.Name = "chBFilterByPages";
            this.chBFilterByPages.Size = new System.Drawing.Size(185, 17);
            this.chBFilterByPages.TabIndex = 3;
            this.chBFilterByPages.Text = "Фильтр по количеству страниц";
            this.chBFilterByPages.UseVisualStyleBackColor = true;
            this.chBFilterByPages.CheckedChanged += new System.EventHandler(this.chBFilterByPages_CheckedChanged);
            // 
            // nUPFilterByPages
            // 
            this.nUPFilterByPages.Enabled = false;
            this.nUPFilterByPages.Location = new System.Drawing.Point(52, 105);
            this.nUPFilterByPages.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nUPFilterByPages.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUPFilterByPages.Name = "nUPFilterByPages";
            this.nUPFilterByPages.Size = new System.Drawing.Size(99, 20);
            this.nUPFilterByPages.TabIndex = 4;
            this.mainToolTip.SetToolTip(this.nUPFilterByPages, "На странице по 50 треков");
            this.nUPFilterByPages.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(51, 32);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(100, 20);
            this.tbName.TabIndex = 5;
            this.tbName.Text = "Ваш логин";
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(64, 217);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 6;
            this.btnGo.Text = "Go!";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // mainToolTip
            // 
            this.mainToolTip.AutomaticDelay = 200;
            this.mainToolTip.AutoPopDelay = 20000;
            this.mainToolTip.InitialDelay = 200;
            this.mainToolTip.ReshowDelay = 40;
            this.mainToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.mainToolTip.ToolTipTitle = "Информация";
            // 
            // cbMode
            // 
            this.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMode.FormattingEnabled = true;
            this.cbMode.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cbMode.Location = new System.Drawing.Point(83, 153);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(37, 21);
            this.cbMode.TabIndex = 7;
            this.mainToolTip.SetToolTip(this.cbMode, resources.GetString("cbMode.ToolTip"));
            // 
            // chbAutoOpen
            // 
            this.chbAutoOpen.AutoSize = true;
            this.chbAutoOpen.Location = new System.Drawing.Point(26, 198);
            this.chbAutoOpen.Name = "chbAutoOpen";
            this.chbAutoOpen.Size = new System.Drawing.Size(152, 17);
            this.chbAutoOpen.TabIndex = 8;
            this.chbAutoOpen.Text = "Открыть по завершению";
            this.chbAutoOpen.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(9, 245);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(185, 13);
            this.progressBar1.TabIndex = 9;
            this.progressBar1.Visible = false;
            // 
            // tmrMain
            // 
            this.tmrMain.Interval = 1000;
            this.tmrMain.Tick += new System.EventHandler(this.tmrMain_Tick);
            // 
            // chBSeparate
            // 
            this.chBSeparate.AutoSize = true;
            this.chBSeparate.Location = new System.Drawing.Point(26, 175);
            this.chBSeparate.Name = "chBSeparate";
            this.chBSeparate.Size = new System.Drawing.Size(153, 17);
            this.chBSeparate.TabIndex = 10;
            this.chBSeparate.Text = "Разбивать по страницам";
            this.chBSeparate.UseVisualStyleBackColor = true;
            // 
            // FrmExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(202, 262);
            this.Controls.Add(this.chBSeparate);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.chbAutoOpen);
            this.Controls.Add(this.cbMode);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.nUPFilterByPages);
            this.Controls.Add(this.chBFilterByPages);
            this.Controls.Add(this.tbFilterByArtist);
            this.Controls.Add(this.chBFilterByArtist);
            this.Controls.Add(this.btnChoosePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmExport";
            this.Text = "Экспорт прослушанных треков";
            ((System.ComponentModel.ISupportInitialize)(this.nUPFilterByPages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChoosePath;
        private System.Windows.Forms.CheckBox chBFilterByArtist;
        private System.Windows.Forms.TextBox tbFilterByArtist;
        private System.Windows.Forms.CheckBox chBFilterByPages;
        private System.Windows.Forms.NumericUpDown nUPFilterByPages;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.ToolTip mainToolTip;
        private System.Windows.Forms.ComboBox cbMode;
        private System.Windows.Forms.CheckBox chbAutoOpen;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer tmrMain;
        private System.Windows.Forms.CheckBox chBSeparate;
    }
}