// Class name is always file base name with "Class" appended:
//
/// <summary>
/// VTK test class
/// </summary>
public class vtkViewSelectionChangedEvtTestClass
{
  private static int SelectionChangedCallCount;

  /// <summary>
  /// Handler for any vtkObject event:
  /// </summary>
  public static void SelectionChanged(Kitware.VTK.vtkObject sender, Kitware.VTK.vtkObjectEventArgs e)
  {
    ++SelectionChangedCallCount;

    System.Console.Error.WriteLine("//----------------------------------------------------------------------------");
    System.Console.Error.WriteLine("SelectionChanged");

    System.Console.Error.WriteLine("sender:");
    System.Console.Error.WriteLine(sender.ToString());

    System.Console.Error.WriteLine("e.Caller:");
    System.Console.Error.WriteLine(e.Caller.ToString());

    System.Console.Error.WriteLine("e.EventId:");
    System.Console.Error.WriteLine(e.EventId.ToString());

    System.Console.Error.WriteLine("e.CallData:");
    System.Console.Error.WriteLine(e.CallData.ToString());

    System.Console.Error.WriteLine("");
  }

  // Static void method with same signature as "Main" is always
  // file base name:
  //
  /// <summary>
  /// VTK test Main method
  /// </summary>
  public static void vtkViewSelectionChangedEvtTest(string[] args)
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

    SelectionChangedCallCount = 0;

    // Create an object to send an event:
    //
    Kitware.VTK.vtkGraphLayoutView view = Kitware.VTK.vtkGraphLayoutView.New();
    Kitware.VTK.vtkRandomGraphSource g = Kitware.VTK.vtkRandomGraphSource.New();
    Kitware.VTK.vtkVertexDegree f = Kitware.VTK.vtkVertexDegree.New();

    view.SetLayoutStrategyToSimple2D();
    f.SetInputConnection(g.GetOutputPort());
    view.AddRepresentationFromInputConnection(f.GetOutputPort());
    view.SetVertexLabelArrayName("VertexDegree");
    view.VertexLabelVisibilityOn();
    view.SetVertexColorArrayName("VertexDegree");
    view.ColorVerticesOn();
    view.GetRenderer().ResetCamera();
    view.Update();

    // Connect a new handler for the SelectionChanged event:
    //
    Kitware.VTK.vtkObject.vtkObjectEventHandler handler =
      new Kitware.VTK.vtkObject.vtkObjectEventHandler(SelectionChanged);

    view.SelectionChangedEvt += handler;

    if (interactive)
    {
      view.GetInteractor().Start();
    }

    // Disconnect them:
    //
    view.SelectionChangedEvt -= handler;

    // Dispose for proper RenderWindow clean up:
    //
    f.Dispose();
    g.Dispose();
    view.Dispose();

//    if (2 != SelectionChangedCallCount)
//    {
//      throw new System.Exception(System.String.Format(
//        "error: SelectionChanged was called {0} times. Expected exactly 2 calls.",
//        SelectionChangedCallCount));
//    }
  }
}
