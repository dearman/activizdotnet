using System.IO;
using Kitware.VTK;
using System;
// input file is C:\VTK\Parallel\Testing\Tcl\TestPDataSetReaderGrid.tcl
// output file is AVTestPDataSetReaderGrid.cs
/// <summary>
/// The testing class derived from AVTestPDataSetReaderGrid
/// </summary>
public class AVTestPDataSetReaderGridClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVTestPDataSetReaderGrid(String [] argv)
    {
    //Prefix Content is: ""

    // Create the RenderWindow, Renderer and both Actors[]
    //[]
    ren1 = vtkRenderer.New();
    renWin = vtkRenderWindow.New();
    renWin.AddRenderer((vtkRenderer)ren1);
    iren = new vtkRenderWindowInteractor();
    iren.SetRenderWindow((vtkRenderWindow)renWin);
    //[]
    // If the current directory is writable, then test the witers[]
    //[]
    try
      {
      channel = new StreamWriter("test.tmp");
      tryCatchError = "NOERROR";
      }
    catch(Exception)
      {
      tryCatchError = "ERROR";
      }
    if(tryCatchError.Equals("NOERROR"))
      {
      channel.Close();
      File.Delete("test.tmp");
      // ====== Structured Grid ======[]
      // First save out a grid in parallel form.[]
      reader = new vtkMultiBlockPLOT3DReader();
      reader.SetXYZFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/combxyz.bin");
      reader.SetQFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/combq.bin");
      writer = new vtkPDataSetWriter();
      writer.SetFileName((string)"comb.pvtk");
      writer.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
      writer.SetNumberOfPieces((int)4);
      writer.Write();
      pReader = new vtkPDataSetReader();
      pReader.SetFileName((string)"comb.pvtk");
      surface = new vtkDataSetSurfaceFilter();
      surface.SetInputConnection((vtkAlgorithmOutput)pReader.GetOutputPort());
      mapper = vtkPolyDataMapper.New();
      mapper.SetInputConnection((vtkAlgorithmOutput)surface.GetOutputPort());
      mapper.SetNumberOfPieces((int)2);
      mapper.SetPiece((int)0);
      mapper.SetGhostLevel((int)1);
      mapper.Update();
      File.Delete("comb.pvtk");
      File.Delete("comb.0.vtk");
      File.Delete("comb.1.vtk");
      File.Delete("comb.2.vtk");
      File.Delete("comb.3.vtk");
      actor = new vtkActor();
      actor.SetMapper((vtkMapper)mapper);
      actor.SetPosition((double)-5,(double)0,(double)-29);
      // Add the actors to the renderer, set the background and size[]
      //[]
      ren1.AddActor((vtkProp)actor);
      // ====== ImageData ======[]
      // First save out a grid in parallel form.[]
      fractal = new vtkImageMandelbrotSource();
      fractal.SetWholeExtent((int)0,(int)9,(int)0,(int)9,(int)0,(int)9);
      fractal.SetSampleCX((double)0.1,(double)0.1,(double)0.1,(double)0.1);
      fractal.SetMaximumNumberOfIterations((ushort)10);
      writer2 = new vtkPDataSetWriter();
      writer.SetFileName((string)"fractal.pvtk");
      writer.SetInputConnection((vtkAlgorithmOutput)fractal.GetOutputPort());
      writer.SetNumberOfPieces((int)4);
      writer.Write();
      pReader2 = new vtkPDataSetReader();
      pReader2.SetFileName((string)"fractal.pvtk");
      iso = new vtkContourFilter();
      iso.SetInputConnection((vtkAlgorithmOutput)pReader2.GetOutputPort());
      iso.SetValue((int)0,(double)4);
      mapper2 = vtkPolyDataMapper.New();
      mapper2.SetInputConnection((vtkAlgorithmOutput)iso.GetOutputPort());
      mapper2.SetNumberOfPieces((int)3);
      mapper2.SetPiece((int)0);
      mapper2.SetGhostLevel((int)0);
      mapper2.Update();
      File.Delete("fractal.pvtk");
      File.Delete("fractal.0.vtk");
      File.Delete("fractal.1.vtk");
      File.Delete("fractal.2.vtk");
      File.Delete("fractal.3.vtk");
      actor2 = new vtkActor();
      actor2.SetMapper((vtkMapper)mapper2);
      actor2.SetScale((double)5,(double)5,(double)5);
      actor2.SetPosition((double)6,(double)6,(double)6);
      // Add the actors to the renderer, set the background and size[]
      //[]
      ren1.AddActor((vtkProp)actor2);
      // ====== PolyData ======[]
      // First save out a grid in parallel form.[]
      sphere = new vtkSphereSource();
      sphere.SetRadius((double)2);
      writer3 = new vtkPDataSetWriter();
      writer3.SetFileName((string)"sphere.pvtk");
      writer3.SetInputConnection((vtkAlgorithmOutput)sphere.GetOutputPort());
      writer3.SetNumberOfPieces((int)4);
      writer3.Write();
      pReader3 = new vtkPDataSetReader();
      pReader3.SetFileName((string)"sphere.pvtk");
      mapper3 = vtkPolyDataMapper.New();
      mapper3.SetInputConnection((vtkAlgorithmOutput)pReader3.GetOutputPort());
      mapper3.SetNumberOfPieces((int)2);
      mapper3.SetPiece((int)0);
      mapper3.SetGhostLevel((int)1);
      mapper3.Update();
      File.Delete("sphere.pvtk");
      File.Delete("sphere.0.vtk");
      File.Delete("sphere.1.vtk");
      File.Delete("sphere.2.vtk");
      File.Delete("sphere.3.vtk");
      actor3 = new vtkActor();
      actor3.SetMapper((vtkMapper)mapper3);
      actor3.SetPosition((double)6,(double)6,(double)6);
      // Add the actors to the renderer, set the background and size[]
      //[]
      ren1.AddActor((vtkProp)actor3);
      }


    ren1.SetBackground((double)0.1,(double)0.2,(double)0.4);
    renWin.SetSize((int)300,(int)300);
    // render the image[]
    //[]
    cam1 = ren1.GetActiveCamera();
    cam1.Azimuth((double)20);
    cam1.Elevation((double)40);
    ren1.ResetCamera();
    cam1.Zoom((double)1.2);
    iren.Initialize();
    // prevent the tk window from showing up then start the event loop[]

    //deleteAllVTKObjects();
    }
  static string VTK_DATA_ROOT;
  static int threshold;
  static vtkRenderer ren1;
  static vtkRenderWindow renWin;
  static vtkRenderWindowInteractor iren;
  static string tryCatchError;
  static StreamWriter channel;
  static vtkMultiBlockPLOT3DReader reader;
  static vtkPDataSetWriter writer;
  static vtkPDataSetReader pReader;
  static vtkDataSetSurfaceFilter surface;
  static vtkPolyDataMapper mapper;
  static vtkActor actor;
  static vtkImageMandelbrotSource fractal;
  static vtkPDataSetWriter writer2;
  static vtkPDataSetReader pReader2;
  static vtkContourFilter iso;
  static vtkPolyDataMapper mapper2;
  static vtkActor actor2;
  static vtkSphereSource sphere;
  static vtkPDataSetWriter writer3;
  static vtkPDataSetReader pReader3;
  static vtkPolyDataMapper mapper3;
  static vtkActor actor3;
  static vtkCamera cam1;


  ///<summary> A Get Method for Static Variables </summary>
  public static string GetVTK_DATA_ROOT()
    {
    return VTK_DATA_ROOT;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void SetVTK_DATA_ROOT(string toSet)
    {
    VTK_DATA_ROOT = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static int Getthreshold()
    {
    return threshold;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void Setthreshold(int toSet)
    {
    threshold = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkRenderer Getren1()
    {
    return ren1;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void Setren1(vtkRenderer toSet)
    {
    ren1 = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkRenderWindow GetrenWin()
    {
    return renWin;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void SetrenWin(vtkRenderWindow toSet)
    {
    renWin = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkRenderWindowInteractor Getiren()
    {
    return iren;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void Setiren(vtkRenderWindowInteractor toSet)
    {
    iren = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static string GettryCatchError()
    {
    return tryCatchError;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void SettryCatchError(string toSet)
    {
    tryCatchError = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static StreamWriter Getchannel()
    {
    return channel;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void Setchannel(StreamWriter toSet)
    {
    channel = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkMultiBlockPLOT3DReader Getreader()
    {
    return reader;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void Setreader(vtkMultiBlockPLOT3DReader toSet)
    {
    reader = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkPDataSetWriter Getwriter()
    {
    return writer;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void Setwriter(vtkPDataSetWriter toSet)
    {
    writer = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkPDataSetReader GetpReader()
    {
    return pReader;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void SetpReader(vtkPDataSetReader toSet)
    {
    pReader = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkDataSetSurfaceFilter Getsurface()
    {
    return surface;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void Setsurface(vtkDataSetSurfaceFilter toSet)
    {
    surface = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkPolyDataMapper Getmapper()
    {
    return mapper;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void Setmapper(vtkPolyDataMapper toSet)
    {
    mapper = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkActor Getactor()
    {
    return actor;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void Setactor(vtkActor toSet)
    {
    actor = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkImageMandelbrotSource Getfractal()
    {
    return fractal;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void Setfractal(vtkImageMandelbrotSource toSet)
    {
    fractal = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkPDataSetWriter Getwriter2()
    {
    return writer2;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void Setwriter2(vtkPDataSetWriter toSet)
    {
    writer2 = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkPDataSetReader GetpReader2()
    {
    return pReader2;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void SetpReader2(vtkPDataSetReader toSet)
    {
    pReader2 = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkContourFilter Getiso()
    {
    return iso;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void Setiso(vtkContourFilter toSet)
    {
    iso = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkPolyDataMapper Getmapper2()
    {
    return mapper2;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void Setmapper2(vtkPolyDataMapper toSet)
    {
    mapper2 = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkActor Getactor2()
    {
    return actor2;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void Setactor2(vtkActor toSet)
    {
    actor2 = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkSphereSource Getsphere()
    {
    return sphere;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void Setsphere(vtkSphereSource toSet)
    {
    sphere = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkPDataSetWriter Getwriter3()
    {
    return writer3;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void Setwriter3(vtkPDataSetWriter toSet)
    {
    writer3 = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkPDataSetReader GetpReader3()
    {
    return pReader3;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void SetpReader3(vtkPDataSetReader toSet)
    {
    pReader3 = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkPolyDataMapper Getmapper3()
    {
    return mapper3;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void Setmapper3(vtkPolyDataMapper toSet)
    {
    mapper3 = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkActor Getactor3()
    {
    return actor3;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void Setactor3(vtkActor toSet)
    {
    actor3 = toSet;
    }

  ///<summary> A Get Method for Static Variables </summary>
  public static vtkCamera Getcam1()
    {
    return cam1;
    }

  ///<summary> A Set Method for Static Variables </summary>
  public static void Setcam1(vtkCamera toSet)
    {
    cam1 = toSet;
    }

  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
    {
    //clean up vtk objects
    if(ren1!= null){ren1.Dispose();}
    if(renWin!= null){renWin.Dispose();}
    if(iren!= null){iren.Dispose();}
    if(reader!= null){reader.Dispose();}
    if(writer!= null){writer.Dispose();}
    if(pReader!= null){pReader.Dispose();}
    if(surface!= null){surface.Dispose();}
    if(mapper!= null){mapper.Dispose();}
    if(actor!= null){actor.Dispose();}
    if(fractal!= null){fractal.Dispose();}
    if(writer2!= null){writer2.Dispose();}
    if(pReader2!= null){pReader2.Dispose();}
    if(iso!= null){iso.Dispose();}
    if(mapper2!= null){mapper2.Dispose();}
    if(actor2!= null){actor2.Dispose();}
    if(sphere!= null){sphere.Dispose();}
    if(writer3!= null){writer3.Dispose();}
    if(pReader3!= null){pReader3.Dispose();}
    if(mapper3!= null){mapper3.Dispose();}
    if(actor3!= null){actor3.Dispose();}
    if(cam1!= null){cam1.Dispose();}
    }

}
//--- end of script --//

