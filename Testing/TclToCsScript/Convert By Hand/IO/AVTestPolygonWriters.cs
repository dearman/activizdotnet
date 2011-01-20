using System.IO;
using Kitware.VTK;
using System;
// input file is C:\VTK\IO\Testing\Tcl\TestPolygonWriters.tcl
// output file is AVTestPolygonWriters.cs
/// <summary>
/// The testing class derived from AVTestPolygonWriters
/// </summary>
public class AVTestPolygonWritersClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVTestPolygonWriters(String [] argv)
  {
  //Prefix Content is: ""
  
  // Create the RenderWindow, Renderer and both Actors[]
  //[]
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  // read data[]
  //[]
  input = new vtkPolyDataReader();
  input.SetFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/brainImageSmooth.vtk");
  //[]
  // generate vectors[]
  clean = new vtkCleanPolyData();
  clean.SetInputConnection((vtkAlgorithmOutput)input.GetOutputPort());
  smooth = new vtkWindowedSincPolyDataFilter();
  smooth.SetInputConnection((vtkAlgorithmOutput)clean.GetOutputPort());
  smooth.GenerateErrorVectorsOn();
  smooth.GenerateErrorScalarsOn();
  smooth.Update();
  mapper = vtkPolyDataMapper.New();
  mapper.SetInputConnection((vtkAlgorithmOutput)smooth.GetOutputPort());
  mapper.SetScalarRange((double)((vtkDataSet)smooth.GetOutput()).GetScalarRange()[0],
      (double)((vtkDataSet)smooth.GetOutput()).GetScalarRange()[1]);
  brain = new vtkActor();
  brain.SetMapper((vtkMapper)mapper);
  // Add the actors to the renderer, set the background and size[]
  //[]
  ren1.AddActor((vtkProp)brain);
  renWin.SetSize((int)320,(int)240);
  ren1.GetActiveCamera().SetPosition((double)149.653,(double)-65.3464,(double)96.0401);
  ren1.GetActiveCamera().SetFocalPoint((double)146.003,(double)22.3839,(double)0.260541);
  ren1.GetActiveCamera().SetViewAngle((double)30);
  ren1.GetActiveCamera().SetViewUp((double)-0.255578,(double)-0.717754,(double)-0.647695);
  ren1.GetActiveCamera().SetClippingRange((double)79.2526,(double)194.052);
  iren.Initialize();
  renWin.Render();
  // render the image[]
  //[]
  // prevent the tk window from showing up then start the event loop[]
  //[]
  // If the current directory is writable, then test the witers[]
  //[]
  try
  {
     channel = new StreamWriter("test.tmp");
      tryCatchError = "NOERROR";
  }
  catch(Exception)
  {tryCatchError = "ERROR";}
  
if(tryCatchError.Equals("NOERROR"))
  {
      channel.Close();
      File.Delete("test.tmp");
      //[]
      //[]
      // test the writers[]
      dsw = new vtkDataSetWriter();
      dsw.SetInputConnection((vtkAlgorithmOutput)smooth.GetOutputPort());
      dsw.SetFileName((string)"brain.dsw");
      dsw.Write();
      File.Delete("brain.dsw");
      pdw = new vtkPolyDataWriter();
      pdw.SetInputConnection((vtkAlgorithmOutput)smooth.GetOutputPort());
      pdw.SetFileName((string)"brain.pdw");
      pdw.Write();
      File.Delete("brain.pdw");
          iv = new vtkIVWriter();
          iv.SetInputConnection((vtkAlgorithmOutput)smooth.GetOutputPort());
          iv.SetFileName((string)"brain.iv");
          iv.Write();
          File.Delete("brain.iv");

      
      //[]
      // the next writers only handle triangles[]
      triangles = new vtkTriangleFilter();
      triangles.SetInputConnection((vtkAlgorithmOutput)smooth.GetOutputPort());
          iv2 = new vtkIVWriter();
          iv2.SetInputConnection((vtkAlgorithmOutput)triangles.GetOutputPort());
          iv2.SetFileName((string)"brain2.iv");
          iv2.Write();
          File.Delete("brain2.iv");


          edges = new vtkExtractEdges();
          edges.SetInputConnection((vtkAlgorithmOutput)triangles.GetOutputPort());
          iv3 = new vtkIVWriter();
          iv3.SetInputConnection((vtkAlgorithmOutput)edges.GetOutputPort());
          iv3.SetFileName((string)"brain3.iv");
          iv3.Write();
          File.Delete("brain3.iv");

      
      byu = new vtkBYUWriter();
      byu.SetGeometryFileName((string)"brain.g");
      byu.SetScalarFileName((string)"brain.s");
      byu.SetDisplacementFileName((string)"brain.d");
      byu.SetInputConnection((vtkAlgorithmOutput)triangles.GetOutputPort());
      byu.Write();
      File.Delete("brain.g");
      File.Delete("brain.s");
      File.Delete("brain.d");
      mcubes = new vtkMCubesWriter();
      mcubes.SetInputConnection((vtkAlgorithmOutput)triangles.GetOutputPort());
      mcubes.SetFileName((string)"brain.tri");
      mcubes.SetLimitsFileName((string)"brain.lim");
      mcubes.Write();
      File.Delete("brain.lim");
      File.Delete("brain.tri");
      stl = new vtkSTLWriter();
      stl.SetInputConnection((vtkAlgorithmOutput)triangles.GetOutputPort());
      stl.SetFileName((string)"brain.stl");
      stl.Write();
      File.Delete("brain.stl");
      stlBinary = new vtkSTLWriter();
      stlBinary.SetInputConnection((vtkAlgorithmOutput)triangles.GetOutputPort());
      stlBinary.SetFileName((string)"brainBinary.stl");
      stlBinary.SetFileType((int)2);
      stlBinary.Write();
      File.Delete("brainBinary.stl");
      cgm = new vtkCGMWriter();
      cgm.SetInputConnection((vtkAlgorithmOutput)triangles.GetOutputPort());
      cgm.SetFileName((string)"brain.cgm");
      cgm.Write();
      File.Delete("brain.cgm");
    }

  
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkPolyDataReader input;
static vtkCleanPolyData clean;
static vtkWindowedSincPolyDataFilter smooth;
static vtkPolyDataMapper mapper;
static vtkActor brain;
static string tryCatchError;
static StreamWriter channel;
static vtkDataSetWriter dsw;
static vtkPolyDataWriter pdw;
static vtkIVWriter iv;
static vtkTriangleFilter triangles;
static vtkIVWriter iv2;
static vtkExtractEdges edges;
static vtkIVWriter iv3;
static vtkBYUWriter byu;
static vtkMCubesWriter mcubes;
static vtkSTLWriter stl;
static vtkSTLWriter stlBinary;
static vtkCGMWriter cgm;


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
        public static vtkPolyDataReader Getinput()
        {
            return input;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setinput(vtkPolyDataReader toSet)
        {
            input = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCleanPolyData Getclean()
        {
            return clean;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setclean(vtkCleanPolyData toSet)
        {
            clean = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkWindowedSincPolyDataFilter Getsmooth()
        {
            return smooth;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setsmooth(vtkWindowedSincPolyDataFilter toSet)
        {
            smooth = toSet;
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
        public static vtkActor Getbrain()
        {
            return brain;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbrain(vtkActor toSet)
        {
            brain = toSet;
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
        public static vtkDataSetWriter Getdsw()
        {
            return dsw;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setdsw(vtkDataSetWriter toSet)
        {
            dsw = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataWriter Getpdw()
        {
            return pdw;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpdw(vtkPolyDataWriter toSet)
        {
            pdw = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkIVWriter Getiv()
        {
            return iv;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setiv(vtkIVWriter toSet)
        {
            iv = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTriangleFilter Gettriangles()
        {
            return triangles;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settriangles(vtkTriangleFilter toSet)
        {
            triangles = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkIVWriter Getiv2()
        {
            return iv2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setiv2(vtkIVWriter toSet)
        {
            iv2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkExtractEdges Getedges()
        {
            return edges;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setedges(vtkExtractEdges toSet)
        {
            edges = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkIVWriter Getiv3()
        {
            return iv3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setiv3(vtkIVWriter toSet)
        {
            iv3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkBYUWriter Getbyu()
        {
            return byu;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbyu(vtkBYUWriter toSet)
        {
            byu = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkMCubesWriter Getmcubes()
        {
            return mcubes;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setmcubes(vtkMCubesWriter toSet)
        {
            mcubes = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkSTLWriter Getstl()
        {
            return stl;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setstl(vtkSTLWriter toSet)
        {
            stl = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkSTLWriter GetstlBinary()
        {
            return stlBinary;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetstlBinary(vtkSTLWriter toSet)
        {
            stlBinary = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCGMWriter Getcgm()
        {
            return cgm;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcgm(vtkCGMWriter toSet)
        {
            cgm = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(input!= null){input.Dispose();}
  	if(clean!= null){clean.Dispose();}
  	if(smooth!= null){smooth.Dispose();}
  	if(mapper!= null){mapper.Dispose();}
  	if(brain!= null){brain.Dispose();}
  	if(dsw!= null){dsw.Dispose();}
  	if(pdw!= null){pdw.Dispose();}
  	if(iv!= null){iv.Dispose();}
  	if(triangles!= null){triangles.Dispose();}
  	if(iv2!= null){iv2.Dispose();}
  	if(edges!= null){edges.Dispose();}
  	if(iv3!= null){iv3.Dispose();}
  	if(byu!= null){byu.Dispose();}
  	if(mcubes!= null){mcubes.Dispose();}
  	if(stl!= null){stl.Dispose();}
  	if(stlBinary!= null){stlBinary.Dispose();}
  	if(cgm!= null){cgm.Dispose();}
  }

}
//--- end of script --//

