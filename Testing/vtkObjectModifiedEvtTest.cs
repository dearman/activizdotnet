// Class name is always file base name with "Class" appended:
//
/// <summary>
/// VTK test class
/// </summary>
public class vtkObjectModifiedEvtTestClass
{
  private static int ObjectModifiedCallCount;

  /// <summary>
  /// Handler for any vtkObject event:
  /// </summary>
  public static void ObjectModified(Kitware.VTK.vtkObject sender, Kitware.VTK.vtkObjectEventArgs e)
  {
    ++ObjectModifiedCallCount;

    System.Console.Error.WriteLine("//----------------------------------------------------------------------------");
    System.Console.Error.WriteLine("ObjectModified");

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
  public static void vtkObjectModifiedEvtTest(string[] args)
  {
    ObjectModifiedCallCount = 0;

    // Create an object to send an event:
    //
    Kitware.VTK.vtkActor2D sender = new Kitware.VTK.vtkActor2D();

    // Connect a new handler for the Modified event:
    //
    Kitware.VTK.vtkObject.vtkObjectEventHandler handler =
      new Kitware.VTK.vtkObject.vtkObjectEventHandler(ObjectModified);

    sender.ModifiedEvt += handler;

    // Trigger the event:
    //
    sender.Modified();

    // Toggle Debug, trigger event again, then turn off Debug before objects are destroyed...
    //
    sender.SetDebug((byte)((0 == sender.GetDebug()) ? 1 : 0));
    sender.Modified();
    sender.DebugOff();

    // Disconnect them:
    //
    sender.ModifiedEvt -= handler;

    if (2 != ObjectModifiedCallCount)
    {
      throw new System.Exception(System.String.Format(
        "error: ObjectModified was called {0} times. Expected exactly 2 calls.",
        ObjectModifiedCallCount));
    }
  }
}
