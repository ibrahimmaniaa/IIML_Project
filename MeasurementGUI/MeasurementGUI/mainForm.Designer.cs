namespace MeasurementGUI
{
    partial class mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.uploadImgBtn = new System.Windows.Forms.Button();
            this.startBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.zoomBtn = new System.Windows.Forms.Button();
            this.homeBtn = new System.Windows.Forms.Button();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uploadImgBtn
            // 
            this.uploadImgBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uploadImgBtn.Location = new System.Drawing.Point(822, 37);
            this.uploadImgBtn.Name = "uploadImgBtn";
            this.uploadImgBtn.Size = new System.Drawing.Size(191, 46);
            this.uploadImgBtn.TabIndex = 5;
            this.uploadImgBtn.Text = "upload image";
            this.uploadImgBtn.UseVisualStyleBackColor = true;
            this.uploadImgBtn.Click += new System.EventHandler(this.uploadImgBtn_Click);
            // 
            // startBtn
            // 
            this.startBtn.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.startBtn.Location = new System.Drawing.Point(822, 606);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(218, 147);
            this.startBtn.TabIndex = 4;
            this.startBtn.Text = "START";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Enabled = false;
            this.saveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.saveBtn.Image = ((System.Drawing.Image)(resources.GetObject("saveBtn.Image")));
            this.saveBtn.Location = new System.Drawing.Point(167, 786);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(50, 50);
            this.saveBtn.TabIndex = 3;
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // zoomBtn
            // 
            this.zoomBtn.Enabled = false;
            this.zoomBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.zoomBtn.Image = ((System.Drawing.Image)(resources.GetObject("zoomBtn.Image")));
            this.zoomBtn.Location = new System.Drawing.Point(89, 786);
            this.zoomBtn.Margin = new System.Windows.Forms.Padding(0);
            this.zoomBtn.Name = "zoomBtn";
            this.zoomBtn.Size = new System.Drawing.Size(50, 50);
            this.zoomBtn.TabIndex = 2;
            this.zoomBtn.UseVisualStyleBackColor = true;
            this.zoomBtn.Click += new System.EventHandler(this.zoomBtn_Click);
            // 
            // homeBtn
            // 
            this.homeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.homeBtn.Image = ((System.Drawing.Image)(resources.GetObject("homeBtn.Image")));
            this.homeBtn.Location = new System.Drawing.Point(11, 786);
            this.homeBtn.Margin = new System.Windows.Forms.Padding(0);
            this.homeBtn.Name = "homeBtn";
            this.homeBtn.Size = new System.Drawing.Size(50, 50);
            this.homeBtn.TabIndex = 1;
            this.homeBtn.UseVisualStyleBackColor = true;
            this.homeBtn.Click += new System.EventHandler(this.homeBtn_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(768, 768);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.pictureBox_DragDrop);
            this.pictureBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.pictureBox_DragEnter);
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseClick);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(768, 768);
            this.panel1.TabIndex = 6;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 843);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.homeBtn);
            this.Controls.Add(this.zoomBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.startBtn);
            this.Controls.Add(this.uploadImgBtn);
            this.Font = new System.Drawing.Font("Symbol", 8.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button uploadImgBtn;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button zoomBtn;
        private System.Windows.Forms.Button homeBtn;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Panel panel1;
    }
}

