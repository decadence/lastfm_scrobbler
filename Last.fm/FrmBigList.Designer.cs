namespace Last.fm
{
    partial class FrmBigList
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
            this.tbBig = new System.Windows.Forms.TextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbBig
            // 
            this.tbBig.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbBig.Location = new System.Drawing.Point(0, 0);
            this.tbBig.Multiline = true;
            this.tbBig.Name = "tbBig";
            this.tbBig.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbBig.Size = new System.Drawing.Size(290, 377);
            this.tbBig.TabIndex = 0;
            this.tbBig.Text = "Example Artist - Example Track - Example Album\r\nDisturbed - The Curse - Indestruc" +
                "tible \r\nLinkin Park - In The End - Hybrid Theory\r\n";
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(293, 242);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(204, 78);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Одна песня на одну строку.\r\nСтроки формата: \r\nИсполнитель - Название песни - \r\nНа" +
                "звание альбома (Необязательно)\r\nПри несоответствии могут возникнуть\r\nошибки во в" +
                "ремя добавления.\r\n";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(295, 342);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(399, 342);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // FrmBigList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 377);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.tbBig);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmBigList";
            this.Text = "Добавление списка треков";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbBig;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}