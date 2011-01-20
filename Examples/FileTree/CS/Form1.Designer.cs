namespace FileTree
{
    partial class Form1
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
          System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
          this.setupView = new System.Windows.Forms.ToolStripButton();
          this.toolStrip1 = new System.Windows.Forms.ToolStrip();
          this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
          this.label1 = new System.Windows.Forms.Label();
          this.renWinCTRL = new Kitware.VTK.RenderWindowControl();
          this.toolStrip1.SuspendLayout();
          this.SuspendLayout();
          // 
          // setupView
          // 
          this.setupView.ImageTransparentColor = System.Drawing.Color.Magenta;
          this.setupView.Name = "setupView";
          this.setupView.Size = new System.Drawing.Size(72, 22);
          this.setupView.Text = "View Folder";
          this.setupView.Click += new System.EventHandler(this.setupView_Click);
          // 
          // toolStrip1
          // 
          this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setupView});
          this.toolStrip1.Location = new System.Drawing.Point(0, 0);
          this.toolStrip1.Name = "toolStrip1";
          this.toolStrip1.Size = new System.Drawing.Size(632, 25);
          this.toolStrip1.TabIndex = 1;
          this.toolStrip1.Text = "toolStrip1";
          // 
          // label1
          // 
          this.label1.AutoSize = true;
          this.label1.Location = new System.Drawing.Point(109, 5);
          this.label1.Name = "label1";
          this.label1.Size = new System.Drawing.Size(73, 13);
          this.label1.TabIndex = 0;
          this.label1.Text = "Viewing None";
          // 
          // renWinCTRL
          // 
          this.renWinCTRL.AddTestActors = false;
          this.renWinCTRL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                      | System.Windows.Forms.AnchorStyles.Left)
                      | System.Windows.Forms.AnchorStyles.Right)));
          this.renWinCTRL.Location = new System.Drawing.Point(12, 39);
          this.renWinCTRL.Name = "renWinCTRL";
          this.renWinCTRL.Size = new System.Drawing.Size(608, 555);
          this.renWinCTRL.TabIndex = 0;
          this.renWinCTRL.TestText = null;
          this.renWinCTRL.Load += new System.EventHandler(this.renWinCTRL_Load);
          this.renWinCTRL.Resize += new System.EventHandler(this.renWinCTRL_Resize);
          // 
          // Form1
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(632, 606);
          this.Controls.Add(this.label1);
          this.Controls.Add(this.toolStrip1);
          this.Controls.Add(this.renWinCTRL);
          this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
          this.Name = "Form1";
          this.Text = "File Tree";
          this.toolStrip1.ResumeLayout(false);
          this.toolStrip1.PerformLayout();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton setupView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private Kitware.VTK.RenderWindowControl renWinCTRL;
    }
}

