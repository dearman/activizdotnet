// Class name is always file base name with "Class" appended:
//
/// <summary>
/// VTK test class
/// </summary>
public class vtkObjectTestClass
{
  // Static void method with same signature as "Main" is always
  // file base name:
  //
  /// <summary>
  /// VTK test Main method
  /// </summary>
  public static void vtkObjectTest(string[] args)
  {
    // Reference a method in the Kitware.VTK assembly:
    //   (forces the Kitware.VTK assembly to load...)
    //
    string version = Kitware.VTK.vtkVersion.GetVTKSourceVersion();

    string assemblyname = "Kitware.VTK";
    bool listAllAssemblies = false;
    bool listAllClasses = false;
    bool collectingClassNames = false;
    System.Collections.Hashtable classnames =
      new System.Collections.Hashtable();
    System.Collections.Hashtable classtypes =
      new System.Collections.Hashtable();

    for (int i = 0; i < args.Length; ++i)
      {
      string s = args[i];

      if (s == "--assemblyname")
        {
        collectingClassNames = false;

        if (i < args.Length - 1)
          {
          assemblyname = args[i + 1];
          }
        else
          {
          throw new System.Exception("--assemblyname used, but no name given");
          }
        }
      else if (s == "--classname")
        {
        collectingClassNames = false;

        if (i < args.Length - 1)
          {
          classnames.Add(args[i + 1], args[i + 1]);
          }
        else
          {
          throw new System.Exception("--classname used, but no name given");
          }
        }
      else if (s == "--classnames")
        {
        collectingClassNames = true;
        }
      else if (s == "--classnames-file")
        {
        if (i < args.Length - 1)
          {
          ReadClassNamesFromFile(classnames, args[i + 1]);
          }
        else
          {
          throw new System.Exception("--classnames-file used, but no filename given");
          }
        }
      else if (s == "--list-all-classes")
        {
        collectingClassNames = false;
        listAllClasses = true;
        }
      else if (s == "--list-all-assemblies")
        {
        collectingClassNames = false;
        listAllAssemblies = true;
        }
      else if (collectingClassNames)
        {
        classnames.Add(s, s);
        }
      }

    if (0 == classnames.Count)
      {
      classnames.Add("vtkObject", "vtkObject");
      }

    // Find the assembly containing the class to instantiate:
    //
    System.Reflection.Assembly[] assemblies =
      System.AppDomain.CurrentDomain.GetAssemblies();
    System.Reflection.Assembly assembly = null;

    foreach (System.Reflection.Assembly a in assemblies)
      {
      System.Reflection.AssemblyName aname = a.GetName();

      if (aname.FullName == assemblyname || aname.Name == assemblyname)
        {
        assembly = a;
        }

      if (listAllAssemblies)
        {
        //System.Console.Error.WriteLine(aname.Name);
        System.Console.Error.WriteLine(aname.FullName);
        }
      }
    if (listAllAssemblies)
      {
      return;
      }

    // Find the type of the class to instantiate:
    //
    if (assembly != null)
      {
      foreach (System.Type et in assembly.GetExportedTypes())
        {
        if (listAllClasses)
          {
          System.Console.Error.WriteLine(et.Name);
          System.Console.Error.WriteLine(et.AssemblyQualifiedName);
          }

        if (classnames.ContainsKey(et.Name))
          {
          classtypes.Add(et.Name, et);
          }
        if (classnames.ContainsKey(et.AssemblyQualifiedName))
          {
          classtypes.Add(et.AssemblyQualifiedName, et);
          }
        }
      if (listAllClasses)
        {
        return;
        }
      }

    if (0 == classtypes.Count)
      {
      throw new System.ArgumentException(System.String.Format(
        "error: did not find any Type objects... Typo in command line args?"));
      }

    // Instantiate and print each type:
    //
    string classname = "";
    System.Type classtype = null;

    System.Console.Error.WriteLine("");
    System.Console.Error.WriteLine("CTEST_FULL_OUTPUT (Avoid ctest truncation of output)");
    System.Console.Error.WriteLine("");
    System.Console.Error.WriteLine(System.String.Format("classtypes.Count: {0}", classtypes.Count));
    System.Console.Error.WriteLine("");
    System.Console.Error.WriteLine(System.String.Format("GetVTKSourceVersion(): '{0}'", version));
    System.Console.Error.WriteLine("");

    foreach (System.Collections.DictionaryEntry entry in classtypes)
      {
      classname = (string) entry.Key;
      classtype = (System.Type) entry.Value;

      // Instantiate via "New" method:
      //
      System.Console.Error.WriteLine("");
      System.Console.Error.WriteLine(
        "==============================================================================");
      System.Console.Error.WriteLine(System.String.Format(
        "Instantiating and printing class '{0}'", classname));
      System.Console.Error.WriteLine("");

      // Look for a New method that takes no parameters:
      //
      System.Reflection.MethodInfo mi = classtype.GetMethod("New", System.Type.EmptyTypes);
      if (null == mi)
        {
        if (classtype.IsAbstract)
          {
          System.Console.Error.WriteLine(System.String.Format(
            "No 'New' method in abstract class '{0}'. Test passes without instantiating or printing an object.",
            classname));
          }
        else
          {
          System.Console.Error.WriteLine(System.String.Format(
            "No 'New' method in concrete class '{0}'. Test passes without instantiating or printing an object.",
            classname));
          //throw new System.ArgumentException(System.String.Format(
          //  "error: could not find 'New' method for concrete class '{0}'",
          //  classname));
          }
        }

      if (null != mi)
        {
        // Assumption: any object we create via a 'New' method will implement
        // the 'IDisposable' interface...
        //
        // 'using' forces an 'o.Dispose' call at the closing curly brace:
        //
        using (System.IDisposable o = (System.IDisposable) mi.Invoke(null, null))
          {
          System.Console.Error.WriteLine(o.ToString());
          }
        }

      System.Console.Error.WriteLine("");

      // Instantiate via public default constructor:
      //
      //System.Type [] ca = new System.Type[0];
      //System.Reflection.ConstructorInfo ci = classtype.GetConstructor(ca);
      //if (null == ci)
      //  {
      //  throw new System.ArgumentException(System.String.Format(
      //    "error: could not find public default constructor for '{0}'",
      //    classname));
      //  }
      //
      //o = ci.Invoke(null);
      //System.Console.Error.WriteLine(o.ToString());
      }
  }

  /// <summary>
  /// Helper method to read in class names from a file.
  /// </summary>
  public static void ReadClassNamesFromFile(System.Collections.Hashtable classnames, string filename)
  {
    System.IO.StreamReader file = new System.IO.StreamReader(filename);
    string line;

    while ((line = file.ReadLine()) != null)
      {
      classnames.Add(line, line);
      }

    file.Close();
  }
}
