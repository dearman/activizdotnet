using Kitware.VTK;

// Class name is always file base name with "Class" appended:
//
/// <summary>
/// VTK test class
/// </summary>
public class vtkConeSourceTestClass
{
  // Static void method with same signature as "Main" is always
  // file base name:
  //
  /// <summary>
  /// VTK test Main method
  /// </summary>
  public static void vtkConeSourceTest(string[] args)
  {
    bool interactive = false;
    foreach (string s in args)
    {
      // -I means "interactive" test -- do not automatically quit:
      //
      if (s == "-I")
      {
        interactive = true;
      }
    }

    vtkConeSource source = new vtkConeSource();

    vtkMapper mapper = vtkPolyDataMapper.New();
    mapper.SetInputConnection(source.GetOutputPort());

    vtkActor actor = new vtkActor();
    actor.SetMapper(mapper);

    vtkRenderer ren1 = vtkRenderer.New();
    ren1.AddActor(actor);
    ren1.SetBackground(0.1, 0.2, 0.4);

    vtkRenderWindow renWin = vtkRenderWindow.New();
    renWin.AddRenderer(ren1);
    renWin.SetSize(400, 300);

    vtkRenderWindowInteractor iren = vtkRenderWindowInteractor.New();
    iren.SetRenderWindow(renWin);
    iren.Initialize();

    Kitware.mummy.Runtime.Methods.Print(false);
    Kitware.mummy.Runtime.Methods.PrintWrappedObjectsTable();

    if (interactive)
    {
      iren.Start();
    }

    ren1.SetRenderWindow(null);
    iren.SetRenderWindow(null);

    renWin.Dispose();
  }
}
