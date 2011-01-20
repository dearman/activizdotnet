namespace Kitware.VTK
{
  partial class RenderWindowControl
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
      try
      {
        if (disposing)
        {
          if (components != null)
          {
            components.Dispose();
          }
        }
      }
      finally
      {
        base.Dispose(disposing);
      }
    }

    /// <summary>
    /// Called to set the vtkRenderWindow size according to this control's
    /// Size property.
    /// </summary>
    internal void SyncRenderWindowSize()
    {
      if (this.m_RenderWindow != null)
      {
        this.m_RenderWindow.SetSize(this.Size.Width, this.Size.Height);
      }
    }

    private System.IntPtr XDisplay;

    /// <summary>
    /// Retrieve the X11 Display* to pass to VTK's vtkRenderWindow::SetDisplayId
    /// </summary>
    private System.IntPtr GetXDisplay()
    {
      System.Type xplatui = System.Type.GetType("System.Windows.Forms.XplatUIX11, System.Windows.Forms");
      if (xplatui != null)
      {
        System.IntPtr DisplayHandle = (System.IntPtr)xplatui.
          GetField("DisplayHandle", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).
          GetValue(null);
        XDisplay = DisplayHandle;

        // Also, may need possibly:
        // Setup correct X visual and colormap so that VTK OpenGL stuff
        // works properly on mono/X11. Cache the display value so that we
        // can use it to set the RenderWindowId in OnCreated.

        //System.IntPtr RootWindow = (System.IntPtr)xplatui.GetField("RootWindow", System.Reflection.BindingFlags.Static |
        //System.Reflection.BindingFlags.NonPublic).GetValue(null);
        //int ScreenNo = (int)xplatui.GetField("ScreenNo", System.Reflection.BindingFlags.Static | 
        //System.Reflection.BindingFlags.NonPublic).GetValue(null);
        //int[] dblBuf = new int[] { 5, (int)GLXFlags.GLX_RGBA, (int)GLXFlags.GLX_RED_SIZE, 1, (int)GLXFlags.GLX_GREEN_SIZE, 
        //1, (int)GLXFlags.GLX_BLUE_SIZE, 1, (int)GLXFlags.GLX_DEPTH_SIZE, 1, 0 };
        //GLXVisualInfo = glXChooseVisual(DisplayHandle, ScreenNo, dblBuf);
        //XVisualInfo xVisualInfo = (XVisualInfo)Marshal.PtrToStructure(GLXVisualInfo, typeof(XVisualInfo));
        //System.IntPtr visual = System.IntPtr.Zero; // xVisualInfo.visual;
        //System.IntPtr colormap = XCreateColormap(DisplayHandle, RootWindow, visual, 0/*AllocNone*/);
        //xplatui.GetField("CustomVisual", System.Reflection.BindingFlags.Static | 
        //System.Reflection.BindingFlags.NonPublic).SetValue(null, visual);
        //xplatui.GetField("CustomColormap", System.Reflection.BindingFlags.Static | 
        //System.Reflection.BindingFlags.NonPublic).SetValue(null, colormap);
      }

      return XDisplay;
    }

    private bool AttachedInteractor;

    private void AttachInteractor()
    {
      if (!this.AttachedInteractor)
      {
        this.AttachedInteractor = true;
        this.m_RenderWindow.SetInteractor(this.m_RenderWindowInteractor);
      }
    }

    /// <summary>
    /// OnHandleCreated.
    /// </summary>
    protected override void OnHandleCreated(System.EventArgs e)
    {
      if (!this.DesignMode)
      {
        Kitware.VTK.vtkLogoWidget w = new Kitware.VTK.vtkLogoWidget();

        this.m_Renderer = Kitware.VTK.vtkRenderer.New();
        this.m_RenderWindow = Kitware.VTK.vtkRenderWindow.New();

        System.IntPtr xdisplay = GetXDisplay();
        bool isX11 = false;
        if (System.IntPtr.Zero != xdisplay)
        {
          isX11 = true;
        }

        if (isX11)
        {
          // If running an X11-based build, then we must use a
          // vtkGenericRenderWindowInteractor:
          //
          this.m_RenderWindowInteractor = Kitware.VTK.vtkGenericRenderWindowInteractor.New();
          this.m_RenderWindow.SetDisplayId(xdisplay);
        }
        else
        {
          // Allow the native factory method to produce the natively expected subclass
          // of vtkRenderWindowInteractor:
          //
          this.m_RenderWindowInteractor = Kitware.VTK.vtkRenderWindowInteractor.New();
        }

        Kitware.VTK.vtkInteractorStyleSwitch style = this.m_RenderWindowInteractor.GetInteractorStyle()
          as Kitware.VTK.vtkInteractorStyleSwitch;
        if (null != style)
        {
          style.SetCurrentStyleToTrackballCamera();
        }

        this.m_RenderWindow.SetParentId(this.Handle);
        this.m_RenderWindow.AddRenderer(this.m_Renderer);

        if (!isX11)
        {
          this.AttachInteractor();
        }

        w.Dispose();
      }

      base.OnHandleCreated(e);
    }

    /// <summary>
    /// OnHandleDestroyed.
    /// </summary>
    protected override void OnHandleDestroyed(System.EventArgs e)
    {
      if (this.m_Renderer != null)
      {
        this.m_Renderer.SetRenderWindow(null);
      }

      if (this.m_RenderWindowInteractor != null)
      {
        this.m_RenderWindowInteractor.Dispose();
        this.m_RenderWindowInteractor = null;
      }

      if (this.m_RenderWindow != null)
      {
        this.m_RenderWindow.Dispose();
        this.m_RenderWindow = null;
      }

      if (this.m_Renderer != null)
      {
        this.m_Renderer.Dispose();
        this.m_Renderer = null;
      }

      base.OnHandleDestroyed(e);
    }

    /// <summary>
    /// OnMouseDown.
    /// </summary>
    protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
    {
      Kitware.VTK.vtkGenericRenderWindowInteractor grwi = this.m_RenderWindowInteractor as Kitware.VTK.vtkGenericRenderWindowInteractor;
      if (null != grwi)
      {
        grwi.SetEventInformationFlipY(e.X, e.Y, 0, 0, 0, e.Clicks, null);

        switch (e.Button)
        {
          case System.Windows.Forms.MouseButtons.Left:
            grwi.LeftButtonPressEvent();
          break;

          case System.Windows.Forms.MouseButtons.Middle:
            grwi.MiddleButtonPressEvent();
          break;

          case System.Windows.Forms.MouseButtons.Right:
            grwi.RightButtonPressEvent();
          break;
        }
      }
    }

    /// <summary>
    /// OnMouseMove.
    /// </summary>
    protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
    {
      Kitware.VTK.vtkGenericRenderWindowInteractor grwi = this.m_RenderWindowInteractor as Kitware.VTK.vtkGenericRenderWindowInteractor;
      if (null != grwi)
      {
        grwi.SetEventInformationFlipY(e.X, e.Y, 0, 0, 0, e.Clicks, null);

        grwi.MouseMoveEvent();
      }
    }

    /// <summary>
    /// OnMouseUp.
    /// </summary>
    protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
    {
      Kitware.VTK.vtkGenericRenderWindowInteractor grwi = this.m_RenderWindowInteractor as Kitware.VTK.vtkGenericRenderWindowInteractor;
      if (null != grwi)
      {
        grwi.SetEventInformationFlipY(e.X, e.Y, 0, 0, 0, e.Clicks, null);

        switch (e.Button)
        {
          case System.Windows.Forms.MouseButtons.Left:
            grwi.LeftButtonReleaseEvent();
          break;

          case System.Windows.Forms.MouseButtons.Middle:
            grwi.MiddleButtonReleaseEvent();
          break;

          case System.Windows.Forms.MouseButtons.Right:
            grwi.RightButtonReleaseEvent();
          break;
        }
      }
    }

    /// <summary>
    /// OnMouseWheel.
    /// </summary>
    protected override void OnMouseWheel(System.Windows.Forms.MouseEventArgs e)
    {
      Kitware.VTK.vtkGenericRenderWindowInteractor grwi = this.m_RenderWindowInteractor as Kitware.VTK.vtkGenericRenderWindowInteractor;
      if (null != grwi)
      {
        grwi.SetEventInformationFlipY(e.X, e.Y, 0, 0, 0, e.Clicks, null);

        if (e.Delta > 0)
        {
            grwi.MouseWheelForwardEvent();
        }
        else
        {
            grwi.MouseWheelBackwardEvent();
        }
      }
    }


    //string s = "e.KeyCode.ToString(): " + e.KeyCode.ToString();
    //System.Windows.Forms.MessageBox.Show(s, "KeyDown1");
    //System.Windows.Forms.KeysConverter kc = new System.Windows.Forms.KeysConverter();
    //s = "kc.ConvertToString(e.KeyCode): " + kc.ConvertToString(e.KeyCode);
    //System.Windows.Forms.MessageBox.Show(s, "KeyDown2");


    /// <summary>
    /// OnKeyDown.
    /// </summary>
    protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
    {
      Kitware.VTK.vtkGenericRenderWindowInteractor grwi = this.m_RenderWindowInteractor as Kitware.VTK.vtkGenericRenderWindowInteractor;
      if (null != grwi)
      {
        grwi.SetKeyEventInformation(e.Control ? 1 : 0, e.Shift ? 1 : 0, (sbyte) e.KeyCode, 1, null);

        grwi.KeyPressEvent();
      }
    }

    /// <summary>
    /// OnKeyPress.
    /// </summary>
    protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
    {
      Kitware.VTK.vtkGenericRenderWindowInteractor grwi = this.m_RenderWindowInteractor as Kitware.VTK.vtkGenericRenderWindowInteractor;
      if (null != grwi)
      {
        grwi.SetKeyEventInformation(0, 0, (sbyte) e.KeyChar, 1, e.KeyChar.ToString());

        grwi.CharEvent();
      }
    }

    /// <summary>
    /// OnKeyUp.
    /// </summary>
    protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs e)
    {
      Kitware.VTK.vtkGenericRenderWindowInteractor grwi = this.m_RenderWindowInteractor as Kitware.VTK.vtkGenericRenderWindowInteractor;
      if (null != grwi)
      {
        grwi.SetKeyEventInformation(e.Control ? 1 : 0, e.Shift ? 1 : 0, (sbyte) e.KeyCode, 1, null);

        grwi.KeyReleaseEvent();
      }
    }

    /// <summary>
    /// OnSizeChanged fires after the Size property has changed value.
    /// </summary>
    protected override void OnSizeChanged(System.EventArgs e)
    {
      this.SyncRenderWindowSize();

      Kitware.VTK.vtkGenericRenderWindowInteractor grwi = this.m_RenderWindowInteractor as Kitware.VTK.vtkGenericRenderWindowInteractor;
      if (null != grwi)
      {
        grwi.ConfigureEvent();
      }

      base.OnSizeChanged(e);

      this.Invalidate();
    }

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    internal static extern System.IntPtr SetFocus(System.IntPtr hWnd);

    /// <summary>
    /// OnGotFocus fires after Windows keyboard focus enters the control.
    /// </summary>
    protected override void OnGotFocus(System.EventArgs e)
    {
      if (this.m_RenderWindow != null)
      {
        System.IntPtr hWnd = (System.IntPtr) this.m_RenderWindow.GetGenericWindowId();
        if (System.IntPtr.Zero != hWnd)
        {
          try
          {
            // TODO: X-Windows equivalent to Win32 SetFocus?
            //
            // (For now, the try/catch block handles trying to call the Win32
            // function even on mono/Linux/Mac, but it would be nice to have
            // the correct behavior on all platforms...)
            //
            SetFocus(hWnd);
          }
          catch
          {
          }
        }
      }

      base.OnGotFocus(e);
    }


    /// <summary>
    /// Override to do "last minute cram" of child control...
    /// </summary>
    protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
    {
      if (this.m_RenderWindow != null)
      {
        if (this.Visible)
        {
          this.SyncRenderWindowSize();

          if (this.m_RenderWindow.GetInteractor() != this.m_RenderWindowInteractor)
          {
            // On X11, the SetInteractor method cannot be called until the parent
            // window is already visible/mapped/painting... Since SetInteractor
            // is the method that does this in VTK, we avoid calling it until now:
            // when our parent has definitely shown us and we are in the process
            // of painting.
            //
            this.AttachInteractor();
            this.m_RenderWindow.Render();
          }

          if (this.AddTestActors && !this.m_AddedTestActors)
          {
            this.m_AddedTestActors = true;
            this.TestAddActorsToRenderWindow(this.m_RenderWindow);
          }

          this.m_RenderWindow.Render();
        }
      }

      base.OnPaint(e);
    }


    /// <summary>
    /// OnVisibleChanged fires after the Visible property has changed value.
    /// </summary>
    protected override void OnVisibleChanged(System.EventArgs e)
    {
      base.OnVisibleChanged(e);
    }

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.SuspendLayout();
      // 
      // RenderWindowControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Name = "RenderWindowControl";
      this.Size = new System.Drawing.Size(400, 300);
      this.ResumeLayout(false);
    }

    #endregion
  }
}
