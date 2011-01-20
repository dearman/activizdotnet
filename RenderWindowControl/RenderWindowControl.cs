using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Kitware.VTK
{
  /// <summary>
  /// UserControl derived implementation of vtkRenderWindow for use
  /// in Windows Forms applications.
  /// The client area of this UserControl is completely filled with
  /// an instance of a vtkRenderWindow.
  /// </summary>
  [System.Runtime.InteropServices.ComVisible(true), System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.AutoDual)]
  public partial class RenderWindowControl : UserControl
  {
    private Kitware.VTK.vtkRenderer m_Renderer;
    private Kitware.VTK.vtkRenderWindow m_RenderWindow;
    private Kitware.VTK.vtkRenderWindowInteractor m_RenderWindowInteractor;

    /// <summary>
    /// UserControl derived implementation of vtkRenderWindow for use
    /// in Windows Forms applications.
    /// The client area of this UserControl is completely filled with
    /// an instance of a vtkRenderWindow.
    /// </summary>
    public RenderWindowControl()
    {
      InitializeComponent();
    }

    /// <summary>
    /// TestAddActorsToRenderWindow.
    /// </summary>
    private void TestAddActorsToRenderWindow(Kitware.VTK.vtkRenderWindow renWin)
    {
      Kitware.VTK.vtkConeSource source = new Kitware.VTK.vtkConeSource();

      Kitware.VTK.vtkMapper mapper = Kitware.VTK.vtkPolyDataMapper.New();
      mapper.SetInputConnection(source.GetOutputPort());

      Kitware.VTK.vtkActor actor = new Kitware.VTK.vtkActor();
      actor.SetMapper(mapper);

      Kitware.VTK.vtkRenderer ren = null;
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

    #region Public Properties
    /// <summary>
    /// This property gives you access to the vtkRenderWindow that
    /// fills the client area.
    /// </summary>
    public Kitware.VTK.vtkRenderWindow RenderWindow
    {
      get
      {
        return this.m_RenderWindow;
      }
    }

    private string m_TestText;

    /// <summary>
    /// Text property for test purposes.
    /// </summary>
    public string TestText
    {
      get
      {
        return m_TestText;
      }

      set
      {
        m_TestText = value;
      }
    }

    private bool m_AddTestActors = false;
    private bool m_AddedTestActors = false;

    /// <summary>
    /// Bool property for test purposes. If true, VTK actors will be added to
    /// the render window in OnHandleCreated.
    /// </summary>
    public bool AddTestActors
    {
      get
      {
        return m_AddTestActors;
      }

      set
      {
        m_AddTestActors = value;
      }
    }
    #endregion
  }
}
