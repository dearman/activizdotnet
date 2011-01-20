// Class name is always file base name with "Class" appended:
//
/// <summary>
/// VTK test class
/// </summary>
public class vtkVersionTestClass
{
  // Static void method with same signature as "Main" is always
  // file base name:
  //
  /// <summary>
  /// VTK test Main method
  /// </summary>
  public static void vtkVersionTest(string[] args)
  {
    System.Console.Error.WriteLine(System.String.Format("Kitware.VTK.vtkVersion.GetVTKSourceVersion(): {0}", Kitware.VTK.vtkVersion.GetVTKSourceVersion()));
    System.Console.Error.WriteLine("");
    System.Console.Error.WriteLine(System.String.Format("Kitware.VTK.vtkVersion.GetVTKVersion(): {0}", Kitware.VTK.vtkVersion.GetVTKVersion()));
    System.Console.Error.WriteLine(System.String.Format("Kitware.VTK.vtkVersion.GetVTKMajorVersion(): {0}", Kitware.VTK.vtkVersion.GetVTKMajorVersion()));
    System.Console.Error.WriteLine(System.String.Format("Kitware.VTK.vtkVersion.GetVTKMinorVersion(): {0}", Kitware.VTK.vtkVersion.GetVTKMinorVersion()));
    System.Console.Error.WriteLine(System.String.Format("Kitware.VTK.vtkVersion.GetVTKBuildVersion(): {0}", Kitware.VTK.vtkVersion.GetVTKBuildVersion()));
    System.Console.Error.WriteLine("");

    Kitware.VTK.vtkVersion v = new Kitware.VTK.vtkVersion();
    System.Console.Error.WriteLine(v.ToString());
  }
}
