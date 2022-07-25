namespace GUI
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.btnGo = new System.Windows.Forms.Button();
            this.lblAlgorithm = new System.Windows.Forms.Label();
            this.ButtonFileUpload = new System.Windows.Forms.Button();
            this.textBoxNodes = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(92, 422);
            this.btnGo.Margin = new System.Windows.Forms.Padding(2);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(55, 24);
            this.btnGo.TabIndex = 31;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.BtnGo_Click);
            // 
            // lblAlgorithm
            // 
            this.lblAlgorithm.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlgorithm.Location = new System.Drawing.Point(269, 9);
            this.lblAlgorithm.Name = "lblAlgorithm";
            this.lblAlgorithm.Size = new System.Drawing.Size(319, 24);
            this.lblAlgorithm.TabIndex = 0;
            this.lblAlgorithm.Text = "longest Route";
            this.lblAlgorithm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ButtonUploadFileData
            // 
            this.ButtonFileUpload.Location = new System.Drawing.Point(12, 422);
            this.ButtonFileUpload.Name = "ButtonUploadFileData";
            this.ButtonFileUpload.Size = new System.Drawing.Size(75, 24);
            this.ButtonFileUpload.TabIndex = 32;
            this.ButtonFileUpload.Text = "Upload ";
            this.ButtonFileUpload.UseVisualStyleBackColor = true;
            this.ButtonFileUpload.Click += new System.EventHandler(this.ButtonFileUpload_Click);
            // 
            // textBoxNodes
            // 
            this.textBoxNodes.Location = new System.Drawing.Point(12, 45);
            this.textBoxNodes.Multiline = true;
            this.textBoxNodes.Name = "textBoxNodes";
            this.textBoxNodes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxNodes.Size = new System.Drawing.Size(878, 371);
            this.textBoxNodes.TabIndex = 33;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(902, 472);
            this.Controls.Add(this.textBoxNodes);
            this.Controls.Add(this.ButtonFileUpload);
            this.Controls.Add(this.lblAlgorithm);
            this.Controls.Add(this.btnGo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Main";
            this.Text = "Path Finding";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label lblAlgorithm;
        private System.Windows.Forms.Button ButtonFileUpload;
        private System.Windows.Forms.TextBox textBoxNodes;
    }
}

