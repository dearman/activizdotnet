// Class name is always file base name with "Class" appended:
//
/// <summary>
/// VTK test class
/// </summary>
public class vtkCommandTestClass
{
  /// <summary>
  /// Test override of vtkCommand::Execute and AddObserver
  /// </summary>
  public class TestObserver : Kitware.VTK.vtkCommand
  {
     /// <summary>
     /// Override vtkCommand::Execute
     /// </summary>
     public override void Execute(
      [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.CustomMarshaler,
        MarshalTypeRef = typeof(Kitware.VTK.vtkObjectMarshaler))]
      Kitware.VTK.vtkObject caller, uint eventId, System.IntPtr data)
    {
      System.Console.WriteLine("");
      System.Console.WriteLine("TestObserver.Execute called...");
      System.Console.WriteLine(System.String.Format("  caller='{0}'", caller));
      System.Console.WriteLine(System.String.Format("  eventId='{0}'", eventId));
      System.Console.WriteLine(System.String.Format("  data='{0}'", data));

      Kitware.VTK.vtkObject ob = caller;
      if (ob.GetDebug() != 0)
      {
        System.Console.WriteLine("  caller Debug is ON");
      }
      else
      {
        System.Console.WriteLine("  caller Debug is OFF");
      }
      System.Console.WriteLine(System.String.Format("  caller MTime is {0}\n", ob.GetMTime()));

      System.Console.WriteLine("");
    }
  }

  // Static void method with same signature as "Main" is always
  // file base name:
  //
  /// <summary>
  /// VTK test Main method
  /// </summary>
  public static void vtkCommandTest(string[] args)
  {
    // Create an object to send an event:
    //
    Kitware.VTK.vtkObject sender = new Kitware.VTK.vtkObject();

    // Create an event observer:
    //
    TestObserver observer = new TestObserver();

    // Connect them:
    //
    uint observerModifiedEventTag = sender.AddObserver(
      (uint) Kitware.VTK.vtkCommand.EventIds.ModifiedEvent, observer, 0.69f);

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
    sender.RemoveObserver(observerModifiedEventTag);
  }
}
