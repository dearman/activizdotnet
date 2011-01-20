// Class name is always file base name with "Class" appended:
//
/// <summary>
/// VTK test class
/// </summary>
public class vtkPointsTestClass
{
  // Static void method with same signature as "Main" is always
  // file base name:
  //
  /// <summary>
  /// VTK test Main method
  /// </summary>
  public static void vtkPointsTest(string[] args)
  {
    // From Common/vtkType.h:
    //
    //   #define VTK_FLOAT    10
    //   #define VTK_DOUBLE   11

    using (Kitware.VTK.vtkPoints p1 = Kitware.VTK.vtkPoints.New(10))
    {
      System.Console.Error.WriteLine(p1.ToString());
    }

    using (Kitware.VTK.vtkPoints p2 = Kitware.VTK.vtkPoints.New(11))
    {
      System.Console.Error.WriteLine(p2.ToString());
    }
  }
}
