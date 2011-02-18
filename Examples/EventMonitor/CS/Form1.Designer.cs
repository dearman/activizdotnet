namespace EventMonitor
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
      this.components = new System.ComponentModel.Container();
      this.renderWindowControl1 = new Kitware.VTK.RenderWindowControl();
      this.btnAdd = new System.Windows.Forms.Button();
      this.btnRemove = new System.Windows.Forms.Button();
      this.btnEvents = new System.Windows.Forms.Button();
      this.textEvents = new System.Windows.Forms.TextBox();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.btnSave = new System.Windows.Forms.Button();
      this.btnRestore = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // renderWindowControl1
      // 
      this.renderWindowControl1.AddTestActors = false;
      this.renderWindowControl1.Location = new System.Drawing.Point(12, 12);
      this.renderWindowControl1.Name = "renderWindowControl1";
      this.renderWindowControl1.Size = new System.Drawing.Size(360, 309);
      this.renderWindowControl1.TabIndex = 0;
      this.renderWindowControl1.TestText = null;
      // 
      // btnAdd
      // 
      this.btnAdd.Location = new System.Drawing.Point(13, 327);
      this.btnAdd.Name = "btnAdd";
      this.btnAdd.Size = new System.Drawing.Size(75, 23);
      this.btnAdd.TabIndex = 1;
      this.btnAdd.Text = "Add";
      this.btnAdd.UseVisualStyleBackColor = true;
      this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
      // 
      // btnRemove
      // 
      this.btnRemove.Location = new System.Drawing.Point(95, 327);
      this.btnRemove.Name = "btnRemove";
      this.btnRemove.Size = new System.Drawing.Size(75, 23);
      this.btnRemove.TabIndex = 2;
      this.btnRemove.Text = "Remove";
      this.btnRemove.UseVisualStyleBackColor = true;
      this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
      // 
      // btnEvents
      // 
      this.btnEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.btnEvents.Location = new System.Drawing.Point(697, 327);
      this.btnEvents.Name = "btnEvents";
      this.btnEvents.Size = new System.Drawing.Size(75, 23);
      this.btnEvents.TabIndex = 3;
      this.btnEvents.Text = "Events";
      this.btnEvents.UseVisualStyleBackColor = true;
      this.btnEvents.Click += new System.EventHandler(this.btnEvents_Click);
      // 
      // textEvents
      // 
      this.textEvents.Location = new System.Drawing.Point(400, 12);
      this.textEvents.Multiline = true;
      this.textEvents.Name = "textEvents";
      this.textEvents.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.textEvents.Size = new System.Drawing.Size(372, 309);
      this.textEvents.TabIndex = 4;
      this.textEvents.WordWrap = false;
      // 
      // timer1
      // 
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // btnSave
      // 
      this.btnSave.Location = new System.Drawing.Point(211, 327);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(75, 23);
      this.btnSave.TabIndex = 1;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // btnRestore
      // 
      this.btnRestore.Location = new System.Drawing.Point(292, 327);
      this.btnRestore.Name = "btnRestore";
      this.btnRestore.Size = new System.Drawing.Size(75, 23);
      this.btnRestore.TabIndex = 1;
      this.btnRestore.Text = "Restore";
      this.btnRestore.UseVisualStyleBackColor = true;
      this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(784, 362);
      this.Controls.Add(this.textEvents);
      this.Controls.Add(this.btnEvents);
      this.Controls.Add(this.btnRemove);
      this.Controls.Add(this.btnRestore);
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.btnAdd);
      this.Controls.Add(this.renderWindowControl1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximumSize = new System.Drawing.Size(790, 390);
      this.Name = "Form1";
      this.Text = "ActiViz .NET Event Monitor";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Kitware.VTK.RenderWindowControl renderWindowControl1;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Button btnRemove;
    private System.Windows.Forms.Button btnEvents;
    private System.Windows.Forms.TextBox textEvents;
    private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnRestore;
  }
}

