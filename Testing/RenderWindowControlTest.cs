using Kitware.VTK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

// Class name is always file base name with "Class" appended:
//
/// <summary>
/// VTK test class
/// </summary>
public class RenderWindowControlTestClass
{
  // Static void method with same signature as "Main" is always
  // file base name:
  //
  /// <summary>
  /// VTK test Main method
  /// </summary>
  public static void RenderWindowControlTest(string[] args)
  {
    foreach(string s in args)
    {
      // -I means "interactive" test -- do not automatically quit:
      //
      if (s == "-I")
      {
        Form1.AutoQuitTicks = 0;
      }
    }

    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    Application.Run(new Form1());
  }

  /// <summary>
  /// Primary test class instantiated from the test Main method
  /// </summary>
  public class Form1 : Form
  {
    /// <summary>
    /// Constructor
    /// </summary>
    public Form1()
    {
      InitializeComponent();
    }

    private void AddConeSourceToRenderWindow(Kitware.VTK.vtkRenderWindow renWin)
    {
      Kitware.VTK.vtkConeSource source = new Kitware.VTK.vtkConeSource();

      Kitware.VTK.vtkMapper mapper = new Kitware.VTK.vtkOpenGLPolyDataMapper();
      mapper.SetInputConnection(source.GetOutputPort());

      Kitware.VTK.vtkActor actor = new Kitware.VTK.vtkActor();
      actor.SetMapper(mapper);

      Kitware.VTK.vtkRenderer ren = null; //new Kitware.VTK.vtkOpenGLRenderer();
      ren = renWin.GetRenderers().GetFirstRenderer();
      ren.AddActor(actor);

      Kitware.VTK.vtkTextActor textActor = new Kitware.VTK.vtkTextActor();
      textActor.SetInput(Kitware.VTK.vtkVersion.GetVTKSourceVersion());
      ren.AddActor(textActor);

      //int n = renWin.GetRenderers().GetNumberOfItems();
      //System.Diagnostics.Debug.WriteLine(n);
      //System.Diagnostics.Debug.WriteLine(
      //  Kitware.mummy.Runtime.Methods.Print(false)
      //  );
    }

    private Kitware.VTK.vtkRenderer SecondRenderer;
    private Kitware.VTK.vtkRenderWindow SecondRenderWindow;
    private Kitware.VTK.vtkRenderWindowInteractor SecondRenderWindowInteractor;

    private Kitware.VTK.vtkRenderWindow AddConeSourceToSecondRenderWindow()
    {
      this.SecondRenderer = Kitware.VTK.vtkRenderer.New();
      this.SecondRenderWindow = Kitware.VTK.vtkRenderWindow.New();
      this.SecondRenderWindowInteractor = Kitware.VTK.vtkRenderWindowInteractor.New();

      this.SecondRenderWindow.AddRenderer(this.SecondRenderer);
      this.SecondRenderWindow.SetSize(400, 300);
      this.SecondRenderWindow.SetInteractor(this.SecondRenderWindowInteractor);

      AddConeSourceToRenderWindow(this.SecondRenderWindow);

      this.SecondRenderWindow.Render();

      return this.SecondRenderWindow;
    }

    private void DisposeSecondRenderWindow()
    {
      if (this.SecondRenderer != null)
      {
        this.SecondRenderer.SetRenderWindow(null);
      }

      if (this.SecondRenderWindowInteractor != null)
      {
        this.SecondRenderWindowInteractor.Dispose();
        this.SecondRenderWindowInteractor = null;
      }

      if (this.SecondRenderWindow != null)
      {
        this.SecondRenderWindow.Dispose();
        this.SecondRenderWindow = null;
      }

      if (this.SecondRenderer != null)
      {
        this.SecondRenderer.Dispose();
        this.SecondRenderer = null;
      }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      AddConeSourceToRenderWindow(this.renderWindowControl1.RenderWindow);
      AddConeSourceToSecondRenderWindow();

      Kitware.VTK.RenderWindowControl ctl2 = new Kitware.VTK.RenderWindowControl();
      ctl2.Dispose();
    }

    /// <summary>
    /// Number of clock ticks after the first timer Tick event that
    /// Exit is called automatically. If 0, Exit is never called
    /// automatically. Use -I (for interactive) on the command line
    /// to set AutoQuitTicks to 0.
    /// </summary>
    static public long AutoQuitTicks = 25000000;
    static System.DateTime FirstTimer = System.DateTime.MinValue;

    private void timer1_Tick(object sender, EventArgs e)
    {
      if (FirstTimer == System.DateTime.MinValue)
      {
        FirstTimer = System.DateTime.Now;
      }

      if (AutoQuitTicks > 0 &&
        System.DateTime.Now.Ticks - FirstTimer.Ticks > AutoQuitTicks)
      {
        Application.Exit();
      }
    }

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
      if (disposing && (this.SecondRenderWindow != null))
      {
        this.DisposeSecondRenderWindow();
      }
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
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.SuspendLayout();
      // 
      // renderWindowControl1
      // 
      this.renderWindowControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.renderWindowControl1.Location = new System.Drawing.Point(0, 0);
      this.renderWindowControl1.Margin = new System.Windows.Forms.Padding(0);
      this.renderWindowControl1.Name = "renderWindowControl1";
      this.renderWindowControl1.Size = new System.Drawing.Size(400, 300);
      this.renderWindowControl1.TabIndex = 0;
      // 
      // timer1
      // 
      this.timer1.Enabled = true;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(400, 300);
      this.Controls.Add(this.renderWindowControl1);
      this.Name = "Form1";
      this.Text = "Form1";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.ResumeLayout(false);
    }

    #endregion

    private Kitware.VTK.RenderWindowControl renderWindowControl1;
    private System.Windows.Forms.Timer timer1;
  }
}
