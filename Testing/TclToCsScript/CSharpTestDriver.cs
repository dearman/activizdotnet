using Kitware.VTK;
using System;

// input file is +C:\VTK\Common\Testing\Tcl\rtImageTest.tcl
// output file is +rtImageTest1.cs

/// <summary>
/// The testing class derived from rtImageTest1
/// </summary>
public class TclToCsScriptTestDriver
{
  static vtkTesting rtTester;
  static vtkRenderWindow tempRenderWindow;
  static vtkRenderWindowInteractor tempRenderWindowInteractor;
  static vtkObject tempViewer;
  static vtkWindowToImageFilter tempw2i;
  static string VTK_DATA_ROOT;
  static string test;
  static int threshold;
  static int rtResult;

  ///<summary>
  ///Executes a static method using reflection
  ///</summary>
  public static object executeMethod(System.Type t, string toExecute, object[] commands)
  {
      System.Reflection.MethodInfo methodInfo = t.GetMethod(toExecute);
      if (methodInfo == null)
      {
          return null;
      }
      return methodInfo.Invoke(null, commands);
  }

  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void Main(String[] argv)
  {
    try
    {
      bool fail_on_image_diff = false;

      //Prefix Content is: ""
      int argc = 0;
      if (argv != null)
      {
        argc = argv.Length;
      }

      // setup some common things for testing[]
      vtkMath.RandomSeed(6);
      // create the testing class to do the work[]
      rtTester = new vtkTesting();
      for (int i = 1; i < argc; i++)
      {
        rtTester.AddArgument(argv[i]);

        if (argv[i] == "--fail-on-image-diff")
        {
          fail_on_image_diff = true;
        }
      }

      // string auto_path = "";
      //
      VTK_DATA_ROOT = rtTester.GetDataRoot();

      // load in the script[]
      if (0 == argv.Length)
      {
        test = GetTestNameInteractively();
      }
      else
      {
        test = argv[0];
      }

      //The class that we are about to execute the test in
      System.Type t = System.Type.GetType(test + "Class");
      if (null == t)
      {
        throw new System.ArgumentException(System.String.Format(
              "error: could not create a Type object for '{0}'...\n\n{1}\n{2}\n{3}\n{4}\n\n{5}\n\n",
              test + "Class",
              "Typo?",
              "Did you follow the C# test driver naming convention?",
              "Did you add the test to the CMakeLists.txt file?",
              "Did you reconfigure/rebuild after adding the test?",
              "Test 'method' name should equal 'file name without extension'... Test 'public class' name should be the same but with 'Class' appended..."
              ));
      }


      // set the default threshold, the Tcl script may change this[]
      threshold = -1;

      executeMethod(t, "Setthreshold", new object[] { threshold });
      executeMethod(t, "SetVTK_DATA_ROOT", new object[] { VTK_DATA_ROOT });

      //run the test
      executeMethod(t, test, new object[] { argv });

      tempRenderWindowInteractor = (vtkRenderWindowInteractor)executeMethod(t, "Getiren", new object[] { });
      tempRenderWindow = (vtkRenderWindow)executeMethod(t, "GetrenWin", new object[] { });
      tempViewer = (vtkObject)executeMethod(t, "Getviewer", new object[] { });
      tempw2i = (vtkWindowToImageFilter)executeMethod(t, "Getw2i", new object[] { });

      //update the threshold from what the test made it
      threshold = (int)executeMethod(t, "Getthreshold", new object[] { });
      if (tempRenderWindowInteractor != null)
      {
        tempRenderWindow.Render();
      }

      // run the event loop quickly to map any tkwidget windows[]
      // current directory[]
      rtResult = 0;
      if (fail_on_image_diff && rtTester.IsValidImageSpecified() != 0)
      {
        // look for a renderWindow ImageWindow or ImageViewer[]
        // first check for some common names[]
        if (tempRenderWindow != null)
        {
          rtTester.SetRenderWindow(tempRenderWindow);
          if ((threshold) == -1)
          {
            threshold = 10;
          }
        }
        else
        {
          if ((threshold) == -1)
          {
            threshold = 5;
          }
          if (tempViewer != null)
          {
            if (tempViewer.IsA("vtkImageViewer") != 0)
            {
              tempRenderWindow = ((vtkImageViewer)tempViewer).GetRenderWindow();
            }
            else if (tempViewer.IsA("vtkImageViewer2") != 0)
            {
              tempRenderWindow = ((vtkImageViewer2)tempViewer).GetRenderWindow();
            }
            else
            {
              throw new System.Exception("");
            }
            rtTester.SetRenderWindow(tempRenderWindow);

            if (tempViewer.IsA("vtkImageViewer") != 0)
            {
              ((vtkImageViewer)tempViewer).Render();
            }
            else if (tempViewer.IsA("vtkImageViewer2") != 0)
            {
              ((vtkImageViewer2)tempViewer).Render();
            }
          }
          else
          {
            tempRenderWindow = (vtkRenderWindow)executeMethod(t, "GetimgWin", new object[] { });
            if (tempRenderWindow != null)
            {
              rtTester.SetRenderWindow(tempRenderWindow);
              tempRenderWindow.Render();
            }
          }
        }

        if (tempRenderWindow == null)
        {
          throw new System.Exception("tempRenderWindow cannot be null for IsValidImageSpecified case...");
        }

        rtResult = rtTester.RegressionTest(threshold);
      }

      if (rtTester.IsInteractiveModeSpecified() != 0)
      {
        if (tempRenderWindowInteractor != null)
        {
          tempRenderWindowInteractor.Start();
        }
      }

      // Force other objects that may have holds on the render window
      // to let go:
      //
      rtTester.SetRenderWindow(null);
      if (null != tempw2i)
      {
        tempw2i.SetInput(null);
      }

      executeMethod(t, "deleteAllVTKObjects", new object[] { });
      deleteAllVTKObjects();

      // Force a garbage collection prior to exiting the test
      // so that any memory leaks reported are likely to be
      // *actual* leaks of some sort rather than false reports:
      //
      System.GC.Collect();
      System.GC.WaitForPendingFinalizers();

      // Fail the tests that have image diffs if fail_on_image_diff is on:
      //
      if (fail_on_image_diff && 0 == rtResult)
      {
        throw new System.Exception("error: image RegressionTest failed");
      }

      // Test finished without throwing any exceptions...
      // Therefore, it passed. Exit with a zero ExitCode.
      //
      System.Environment.ExitCode = 0;
    }
    catch (System.Exception exc)
    {
      // Catch anything, spit it out to the console so it can be captured
      // by ctest. Exit with a non-zero ExitCode.
      //
      System.Console.Error.WriteLine("================================================================================");
      System.Console.Error.WriteLine("");
      System.Console.Error.WriteLine("TclToCsScript C# test driver caught System.Exception:");
      System.Console.Error.WriteLine("");
      System.Console.Error.WriteLine("{0}", exc.ToString());
      System.Console.Error.WriteLine("");
      System.Console.Error.WriteLine("================================================================================");
      System.Console.Error.WriteLine("");
      System.Environment.ExitCode = 2345;
    }
  }

  /// <summary>
  /// Returns the variable in the index [index] of the stringarray [arr]
  /// </summary>
  /// <param name="arr"></param>     
  /// <param name="index"></param>
  public static string lindex(string[] arr, int index)
  {
    return arr[index];
  }

  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
    //clean up vtk objects
    rtTester.Dispose();
    if (tempViewer != null)
    {
      tempViewer.Dispose();
    }
    if (tempRenderWindow != null)
    {
      tempRenderWindow.Dispose();
    }
    if (tempRenderWindowInteractor != null)
    {
      tempRenderWindowInteractor.Dispose();
    }
    if (tempw2i != null)
    {
      tempw2i.Dispose();
    }
  }

  /// <summary>
  /// Gets the tests that currently compile
  /// </summary>
  public static string[] GetAvailableTests()
  {
    System.Collections.ArrayList testList = new System.Collections.ArrayList();
    System.Reflection.Assembly assy = System.Reflection.Assembly.GetExecutingAssembly();

    foreach (System.Type et in assy.GetExportedTypes())
    {
      if (et.IsClass)
      {
        foreach (System.Reflection.MethodInfo mInfo in et.GetMethods())
        {
          if (et.Name == mInfo.Name + "Class")
          {
            testList.Add(mInfo.Name);
          }
        }
      }
    }

    return (string[])testList.ToArray(System.Type.GetType("System.String"));
  }

  /// <summary>
  /// Returns the temp directory
  /// </summary>
  public static string GetTempDirectory()
  {
    return rtTester.GetTempDirectory();
  }

  /// <summary>
  /// get the names of the tests
  /// </summary>
  public static string GetTestNameInteractively()
  {
    string s = "Available tests:\n";

    string[] tests = GetAvailableTests();

    int i = 0;
    foreach (string xyz in tests)
    {
      s = System.String.Format("{0}  {1}: {2}\n", s, i, xyz);
      ++i;
    }

    s = System.String.Format("{0}To run a test, enter the test number: ", s);

    System.Console.Write(s);

    string choice = System.Console.ReadLine();

    int choiceNumber = -1;
    try
    {
      choiceNumber = System.Convert.ToInt32(choice);

      if (choiceNumber < 0 || choiceNumber >= tests.Length)
      {
        throw new System.ArgumentOutOfRangeException(System.String.Format(
           "'{0}' is an invalid test number.\nExiting without running a test.\n\n",
           choice));
      }
    }
    catch (System.Exception)
    {
      System.Console.Error.Write(System.String.Format(
         "'{0}' is an invalid test number.\nExiting without running a test.\n\n",
         choice));

      throw;
    }

    return tests[choiceNumber];
  }
}
