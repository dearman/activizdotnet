// Class name is always file base name with "Class" appended:
//
/// <summary>
/// VTK test class
/// </summary>
public class ExhaustMemoryQuicklyTestClass
{
  private static int reportCount;
  private static string reportMessage1;


  private static void ThrowIfNonZeroReportCount()
  {
    if (reportCount != 0)
    {
      throw new System.Exception(reportMessage1);
    }
  }


  /// <summary>
  /// ReportHookProc is used in conjunction with PInvoke for
  /// _CrtSetReportHook.
  /// </summary>
  /// <param name="a"></param>
  /// <param name="message"></param>
  /// <param name="b"></param>
  /// <returns></returns>
  public delegate int ReportHookProc(int a, string message, System.IntPtr b);

  private static int CSharpReportHook(int a, string message, System.IntPtr /* int* */ b)
  {
    ++reportCount;
    reportMessage1 = message;

    System.Console.Error.WriteLine(System.String.Format(
      "reportCount: {0}", reportCount));
    System.Console.Error.WriteLine(System.String.Format(
      "a: {0}", a));
    System.Console.Error.WriteLine(System.String.Format(
      "reportMessage1: {0}", reportMessage1));
    System.Console.Error.WriteLine(System.String.Format(
      "b: {0}", b));

    ThrowIfNonZeroReportCount();

    // return 1 means "we handled this report completely"
    return 1;

    // return 0 would mean "didn't handle it -- you handle it after I return"
  }


  // Can't figure out how to get this to work without using the full path
  // name of "msvcr80d.dll" -- very annoying!!
  //
  const string msvcr80d_dll = @"C:\WINDOWS\WinSxS\x86_Microsoft.VC80.DebugCRT_1fc8b3b9a1e18e3b_8.0.50727.762_x-ww_5490cd9f\msvcr80d.dll";

  [System.Runtime.InteropServices.DllImport(
    msvcr80d_dll, EntryPoint = "_CrtSetReportHook")]
  static extern ReportHookProc _CrtSetReportHook(ReportHookProc hook);

  const string msvcr80d_amd64_dll = @"C:\WINDOWS\WinSxS\amd64_Microsoft.VC80.DebugCRT_1fc8b3b9a1e18e3b_8.0.50727.762_x-ww_869ab37f\msvcr80d.dll";

  [System.Runtime.InteropServices.DllImport(
    msvcr80d_amd64_dll, EntryPoint = "_CrtSetReportHook")]
  static extern ReportHookProc _CrtSetReportHook_amd64(ReportHookProc hook);


  private static void SetupReportHook()
  {
    reportCount = 0;

    // Build hooks into *both* 32-bit and 64-bit unmanaged msvcr80d layer.
    //
    // Only call the one appropriate for the runtime we are actually
    // executing on:
    //
    if (System.IntPtr.Size > 4)
    {
      // 64-bit Windows:
      //
      if (System.IO.File.Exists(msvcr80d_amd64_dll))
      {
        _CrtSetReportHook_amd64(new ReportHookProc(CSharpReportHook));
      }
      else
      {
        System.Console.Error.WriteLine(System.String.Format(
          "warning: file '{0}' does not exist: skipping _CrtSetReportHook_amd64 call",
          msvcr80d_amd64_dll));
      }
    }
    else
    {
      // 32-bit Windows:
      //
      if (System.IO.File.Exists(msvcr80d_dll))
      {
        _CrtSetReportHook(new ReportHookProc(CSharpReportHook));
      }
      else
      {
        System.Console.Error.WriteLine(System.String.Format(
          "warning: file '{0}' does not exist: skipping _CrtSetReportHook call",
          msvcr80d_dll));
      }
    }
  }


  private static void ErrorEvtHandler(Kitware.VTK.vtkObject sender, Kitware.VTK.vtkObjectEventArgs e)
  {
    ++reportCount;
    reportMessage1 = "ErrorEvtHandler";

    System.Console.Error.WriteLine(System.String.Format(
      "sender: {0}", sender));
    System.Console.Error.WriteLine(System.String.Format(
      "e: {0}", e));

    ThrowIfNonZeroReportCount();
  }


  private static void MethodThatUsesUpMemory()
  {
    // Intercept vtkOutputWindow error events:
    //
    Kitware.VTK.vtkOutputWindow owi =
      Kitware.VTK.vtkOutputWindow.GetInstance();
    owi.ErrorEvt += new Kitware.VTK.vtkObject.vtkObjectEventHandler(
      ErrorEvtHandler);

    // Intercept abort calls in builds that support
    // _CrtSetReportHook (Debug builds):
    //
    SetupReportHook();

    // Now.... do some stuff to use up memory pronto:
    //
    System.Random r = new System.Random();
    Kitware.VTK.vtkPoints pts1 = new Kitware.VTK.vtkPoints();
    Kitware.VTK.vtkPoints pts2 = new Kitware.VTK.vtkPoints();

    long index = 256;

    long max = System.Int64.MaxValue;
    if (System.IntPtr.Size < 8)
    {
      max = System.Int32.MaxValue;
    }

    while (index < max)
    {
      System.Console.Error.WriteLine(System.String.Format(
        "Inserting at index: {0}", index));

      pts1.InsertPoint((int) index, r.NextDouble(), r.NextDouble(), r.NextDouble());
      ThrowIfNonZeroReportCount();

      pts2.InsertPoint((int)index, r.NextDouble(), r.NextDouble(), r.NextDouble());
      ThrowIfNonZeroReportCount();

      index = 2 * index;
    }

    Kitware.VTK.vtkPolyData p1 = new Kitware.VTK.vtkPolyData();
    ThrowIfNonZeroReportCount();
    p1.SetPoints(pts1);
    ThrowIfNonZeroReportCount();

    Kitware.VTK.vtkPolyData p2 = new Kitware.VTK.vtkPolyData();
    ThrowIfNonZeroReportCount();
    p2.SetPoints(pts2);
    ThrowIfNonZeroReportCount();
  }

  // Static void method with same signature as "Main" is always
  // file base name:
  //
  /// <summary>
  /// VTK test Main method
  /// </summary>
  public static void ExhaustMemoryQuicklyTest(string[] args)
  {
    try
    {
      MethodThatUsesUpMemory();
    }
    catch(System.Exception exc)
    {
      System.Console.Error.WriteLine(System.String.Format(
        "exc: {0}", exc));
    }
  }
}
