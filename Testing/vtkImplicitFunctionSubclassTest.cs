// Class name is always file base name with "Class" appended:
//
/// <summary>
/// VTK test class
/// </summary>
public class vtkImplicitFunctionSubclassTestClass
{
  /// <summary>
  /// Subclass of vtkImplicitFunction
  /// </summary>
  public class Sine : Kitware.VTK.vtkImplicitFunction
  {
    /// <summary>
    /// Constructor
    /// </summary>
    public Sine()
    {
    }
  }

  // Static void method with same signature as "Main" is always
  // file base name:
  //
  /// <summary>
  /// VTK test Main method
  /// </summary>
  public static void vtkImplicitFunctionSubclassTest(string[] args)
  {
    using (Sine s1 = new Sine())
    {
      System.Console.Error.WriteLine(s1.ToString());
    }
  }
}
