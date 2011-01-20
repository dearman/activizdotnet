// Class name is always file base name with "Class" appended:
//
/// <summary>
/// VTK test class
/// </summary>
public class vtkProgrammableAttributeDataFilterTestClass
{
  private static void TestCallback(System.IntPtr p)
  {
    System.Console.Error.WriteLine("From TestCallback");
    System.Console.Error.WriteLine(System.String.Format("p: {0}", p.ToString()));
  }

  // Static void method with same signature as "Main" is always
  // file base name:
  //
  /// <summary>
  /// VTK test Main method
  /// </summary>
  public static void vtkProgrammableAttributeDataFilterTest(string[] args)
  {
    Kitware.VTK.vtkProgrammableAttributeDataFilter f =
      new Kitware.VTK.vtkProgrammableAttributeDataFilter();

    Kitware.VTK.vtkProgrammableAttributeDataFilter f2 =
      new Kitware.VTK.vtkProgrammableAttributeDataFilter();

    Kitware.VTK.vtkProgrammableAttributeDataFilter.ExecuteMethodHandler h =
      new Kitware.VTK.vtkProgrammableAttributeDataFilter.ExecuteMethodHandler(TestCallback);

    f.SetExecuteMethod(h);
    f.SetExecuteMethod(h, (System.IntPtr) 4444);

    System.Console.Error.WriteLine("From vtkProgrammableAttributeDataFilterTest");
    System.Console.Error.WriteLine(System.String.Format("f: {0}", f.ToString()));
    System.Console.Error.WriteLine(System.String.Format("f2: {0}", f2.ToString()));

    f.Dispose();
    f2.Dispose();
  }
}
