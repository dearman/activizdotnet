// Class name is always file base name with "Class" appended:
//
/// <summary>
/// VTK test class
/// </summary>
public class vtkWin32OpenGLRenderWindowTestClass
{
  // Static void method with same signature as "Main" is always
  // file base name:
  //
  /// <summary>
  /// VTK test Main method
  /// </summary>
  public static void vtkWin32OpenGLRenderWindowTest(string[] args)
  {
    Kitware.VTK.vtkRenderWindow v = Kitware.VTK.vtkRenderWindow.New();
    System.Console.WriteLine(v.ToString());

    v.Render();
    v.Dispose();
  }
}
