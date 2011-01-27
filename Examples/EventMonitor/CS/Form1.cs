using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EventMonitor
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    Kitware.VTK.vtkRenderWindowInteractor Interactor = null;
    Kitware.VTK.vtkObject.vtkObjectEventHandler InteractorHandler = null;
    Kitware.VTK.vtkInteractorStyleUser UserStyle = null;
    Kitware.VTK.vtkObject.vtkObjectEventHandler UserHandler = null;

    public void HookEvents()
    {
      this.Interactor = this.renderWindowControl1.RenderWindow.GetInteractor();
      this.InteractorHandler = new Kitware.VTK.vtkObject.vtkObjectEventHandler(Interactor_AnyEventHandler);
      this.Interactor.AnyEvt += this.InteractorHandler;

      // Give our own style a higher priority than the built-in one
      // so that we see the events first:
      //
      float builtInPriority = this.Interactor.GetInteractorStyle().GetPriority();

      this.UserStyle = Kitware.VTK.vtkInteractorStyleUser.New();
      this.UserStyle.SetPriority(0.5f);
      this.UserStyle.SetInteractor(this.Interactor);

      this.UserHandler = new Kitware.VTK.vtkObject.vtkObjectEventHandler(UserStyle_MultipleEventHandler);

      // Keyboard events:
      this.UserStyle.KeyPressEvt += this.UserHandler;
      this.UserStyle.CharEvt += this.UserHandler;
      this.UserStyle.KeyReleaseEvt += this.UserHandler;
    }

    public void UnhookEvents()
    {
      this.UserStyle.KeyPressEvt -= this.UserHandler;
      this.UserStyle.CharEvt -= this.UserHandler;
      this.UserStyle.KeyReleaseEvt -= this.UserHandler;

      this.Interactor.AnyEvt -= this.InteractorHandler;

      this.UserHandler = null;
      this.UserStyle = null;
      this.InteractorHandler = null;
      this.Interactor = null;
    }

    void PrintEvent(Kitware.VTK.vtkObject sender, Kitware.VTK.vtkObjectEventArgs e)
    {
      int[] pos = this.Interactor.GetEventPosition();
      string keysym = this.Interactor.GetKeySym();
      sbyte keycode = this.Interactor.GetKeyCode();

      string line = String.Format("{0} ({1},{2}) ('{3}',{4}) {5} data='0x{6:x8}'{7}",
        Kitware.VTK.vtkCommand.GetStringFromEventId(e.EventId),
        pos[0], pos[1],
        keysym, keycode,
        e.Caller.GetClassName(), e.CallData.ToInt32(), System.Environment.NewLine);

      System.Diagnostics.Debug.Write(line);
      this.textEvents.AppendText(line);
    }

    void UserStyle_MultipleEventHandler(Kitware.VTK.vtkObject sender, Kitware.VTK.vtkObjectEventArgs e)
    {
      string keysym = this.Interactor.GetKeySym();

      Kitware.VTK.vtkCommand.EventIds eid = (Kitware.VTK.vtkCommand.EventIds) e.EventId;

      switch (eid)
      {
        case Kitware.VTK.vtkCommand.EventIds.KeyPressEvent:
        case Kitware.VTK.vtkCommand.EventIds.CharEvent:
        case Kitware.VTK.vtkCommand.EventIds.KeyReleaseEvent:
          if (keysym == "f")
          {
            // Temporarily disable the interactor, so that the built-in 'f'
            // handler does not get called:
            //
            this.Interactor.Disable();

            // Turn on the timer, so we can re-enable the interactor
            // after the processing of this event is over (one tenth
            // of a second later...)
            //
            this.timer1.Enabled = true;
          }
        break;
      }

      this.PrintEvent(sender, e);
    }

    void Interactor_AnyEventHandler(Kitware.VTK.vtkObject sender, Kitware.VTK.vtkObjectEventArgs e)
    {
      this.PrintEvent(sender, e);
    }

    public void AddActors()
    {
      Kitware.VTK.vtkConeSource source = new Kitware.VTK.vtkConeSource();
      source.SetResolution(30);

      Kitware.VTK.vtkMapper mapper = Kitware.VTK.vtkPolyDataMapper.New();
      mapper.SetInputConnection(source.GetOutputPort());

      Kitware.VTK.vtkActor actor = new Kitware.VTK.vtkActor();
      actor.SetMapper(mapper);

      Kitware.VTK.vtkTextActor textActor = new Kitware.VTK.vtkTextActor();
      textActor.SetInput(Kitware.VTK.vtkVersion.GetVTKSourceVersion());

      Kitware.VTK.vtkRenderer ren =
        this.renderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer();
      ren.AddActor(actor);
      ren.AddActor(textActor);

      ren.ResetCamera();
      this.renderWindowControl1.RenderWindow.Render();
    }

    public void RemoveActors()
    {
      Kitware.VTK.vtkRenderer ren =
        this.renderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer();
      ren.RemoveAllViewProps();

      ren.ResetCamera();
      this.renderWindowControl1.RenderWindow.Render();
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      this.AddActors();
    }

    private void btnRemove_Click(object sender, EventArgs e)
    {
      this.RemoveActors();
    }

    private void btnEvents_Click(object sender, EventArgs e)
    {
      if (this.Width < 391)
      {
        this.Width = 790;
      }
      else
      {
        this.Width = 390;
      }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      this.HookEvents();
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.UnhookEvents();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      // Re-enable the interactor:
      //
      this.Interactor.Enable();

      // Disable the timer, so it's not continually firing:
      //
      this.timer1.Enabled = false;
    }
  }
}
