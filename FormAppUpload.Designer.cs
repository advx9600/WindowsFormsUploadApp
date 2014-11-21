namespace WindowsFormsUploadApp
{
    partial class FormAppUpload
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
            this.labelBoard = new System.Windows.Forms.Label();
            this.labelPath = new System.Windows.Forms.Label();
            this.textNote = new System.Windows.Forms.RichTextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.textBoxTitile = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // labelBoard
            // 
            this.labelBoard.AutoSize = true;
            this.labelBoard.Location = new System.Drawing.Point(128, 22);
            this.labelBoard.Name = "labelBoard";
            this.labelBoard.Size = new System.Drawing.Size(41, 12);
            this.labelBoard.TabIndex = 0;
            this.labelBoard.Text = "label1";
            // 
            // labelPath
            // 
            this.labelPath.Location = new System.Drawing.Point(44, 58);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(296, 44);
            this.labelPath.TabIndex = 1;
            this.labelPath.Text = "label2";
            // 
            // textNote
            // 
            this.textNote.Location = new System.Drawing.Point(35, 143);
            this.textNote.Name = "textNote";
            this.textNote.Size = new System.Drawing.Size(322, 106);
            this.textNote.TabIndex = 2;
            this.textNote.Text = "";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(282, 267);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Submit";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.Location = new System.Drawing.Point(46, 9);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(39, 36);
            this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxIcon.TabIndex = 5;
            this.pictureBoxIcon.TabStop = false;
            // 
            // textBoxTitile
            // 
            this.textBoxTitile.Location = new System.Drawing.Point(35, 106);
            this.textBoxTitile.Name = "textBoxTitile";
            this.textBoxTitile.Size = new System.Drawing.Size(322, 21);
            this.textBoxTitile.TabIndex = 6;
            // 
            // FormAppUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 302);
            this.Controls.Add(this.textBoxTitile);
            this.Controls.Add(this.pictureBoxIcon);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.textNote);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.labelBoard);
            this.Name = "FormAppUpload";
            this.Text = "FormAppUpload";
            this.Load += new System.EventHandler(this.FormAppUpload_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBoard;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.RichTextBox textNote;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.TextBox textBoxTitile;
    }
}