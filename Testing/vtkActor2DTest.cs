// Class name is always file base name with "Class" appended:
//
/// <summary>
/// VTK test class
/// </summary>
public class vtkActor2DTestClass
{
  // Static void method with same signature as "Main" is always
  // file base name:
  //
  /// <summary>
  /// VTK test Main method
  /// </summary>
  public static void vtkActor2DTest(string[] args)
  {
    Kitware.VTK.vtkActor2D a = new Kitware.VTK.vtkActor2D();
    System.Console.Error.WriteLine(a.ToString());

    double [] da = a.GetBounds();
    if (null == da)
    {
      System.Console.Error.WriteLine("da is null");
    }
    else
    {
      System.Console.Error.WriteLine(da.ToString());
    }
  }
}
