using System.IO;
using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\extractRectGrid.tcl
// output file is AVextractRectGrid.cs
/// <summary>
/// The testing class derived from AVextractRectGrid
/// </summary>
public class AVextractRectGridClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVextractRectGrid(String [] argv)
  {
  //Prefix Content is: ""
  
  // create pipeline - rectilinear grid[]
  //[]
  rgridReader = new vtkRectilinearGridReader();
  rgridReader.SetFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/RectGrid2.vtk");
  outline = new vtkOutlineFilter();
  outline.SetInputConnection((vtkAlgorithmOutput)rgridReader.GetOutputPort());
  mapper = vtkPolyDataMapper.New();
  mapper.SetInputConnection((vtkAlgorithmOutput)outline.GetOutputPort());
  actor = new vtkActor();
  actor.SetMapper((vtkMapper)mapper);
  rgridReader.Update();
  extract1 = new vtkExtractRectilinearGrid();
  extract1.SetInputConnection((vtkAlgorithmOutput)rgridReader.GetOutputPort());
  //extract1 SetVOI 0 46 0 32 0 10[]
  extract1.SetVOI((int)23,(int)40,(int)16,(int)30,(int)9,(int)9);
  extract1.SetSampleRate((int)2,(int)2,(int)1);
  extract1.IncludeBoundaryOn();
  extract1.Update();
  surf1 = new vtkDataSetSurfaceFilter();
  surf1.SetInputConnection((vtkAlgorithmOutput)extract1.GetOutputPort());
  tris = new vtkTriangleFilter();
  tris.SetInputConnection((vtkAlgorithmOutput)surf1.GetOutputPort());
  mapper1 = vtkPolyDataMapper.New();
  mapper1.SetInputConnection((vtkAlgorithmOutput)tris.GetOutputPort());
  mapper1.SetScalarRange((double)((vtkDataSet)extract1.GetOutput()).GetScalarRange()[0], (double)((vtkDataSet)extract1.GetOutput()).GetScalarRange()[1]);
  actor1 = new vtkActor();
  actor1.SetMapper((vtkMapper)mapper1);
  // write out a rect grid[]
  // write to the temp directory if possible, otherwise use .[]
  dir = ".";
      dir = TclToCsScriptTestDriver.GetTempDirectory();

  
  // make sure the directory is writeable first[]
  try
  {
     channel = new StreamWriter("" + (dir.ToString()) + "/test.tmp");
      tryCatchError = "NOERROR";
  }
  catch(Exception)
  {tryCatchError = "ERROR";}
  
if(tryCatchError.Equals("NOERROR"))
  {
      channel.Close();
      File.Delete("" + (dir.ToString()) + "/test.tmp");
      rectWriter = new vtkRectilinearGridWriter();
      rectWriter.SetInputConnection((vtkAlgorithmOutput)extract1.GetOutputPort());
      rectWriter.SetFileName((string)"" + (dir.ToString()) + "/rect.tmp");
      rectWriter.Write();
      // delete the file[]
      File.Delete("" + (dir.ToString()) + "/rect.tmp");
    }

  
  // Create the RenderWindow, Renderer and both Actors[]
  //[]
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  //ren1 AddActor actor[]
  ren1.AddActor((vtkProp)actor1);
  renWin.SetSize((int)340,(int)400);
  iren.Initialize();
  // render the image[]
  //[]
  // prevent the tk window from showing up then start the event loop[]
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkRectilinearGridReader rgridReader;
static vtkOutlineFilter outline;
static vtkPolyDataMapper mapper;
static vtkActor actor;
static vtkExtractRectilinearGrid extract1;
static vtkDataSetSurfaceFilter surf1;
static vtkTriangleFilter tris;
static vtkPolyDataMapper mapper1;
static vtkActor actor1;
static string dir;
static string tryCatchError;
static StreamWriter channel;
static vtkRectilinearGridWriter rectWriter;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;


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
        public static vtkRectilinearGridReader GetrgridReader()
        {
            return rgridReader;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetrgridReader(vtkRectilinearGridReader toSet)
        {
            rgridReader = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkOutlineFilter Getoutline()
        {
            return outline;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setoutline(vtkOutlineFilter toSet)
        {
            outline = toSet;
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
        public static vtkExtractRectilinearGrid Getextract1()
        {
            return extract1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setextract1(vtkExtractRectilinearGrid toSet)
        {
            extract1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetSurfaceFilter Getsurf1()
        {
            return surf1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setsurf1(vtkDataSetSurfaceFilter toSet)
        {
            surf1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTriangleFilter Gettris()
        {
            return tris;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settris(vtkTriangleFilter toSet)
        {
            tris = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Getmapper1()
        {
            return mapper1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setmapper1(vtkPolyDataMapper toSet)
        {
            mapper1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getactor1()
        {
            return actor1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setactor1(vtkActor toSet)
        {
            actor1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static string Getdir()
        {
            return dir;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setdir(string toSet)
        {
            dir = toSet;
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
        public static vtkRectilinearGridWriter GetrectWriter()
        {
            return rectWriter;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetrectWriter(vtkRectilinearGridWriter toSet)
        {
            rectWriter = toSet;
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
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(rgridReader!= null){rgridReader.Dispose();}
  	if(outline!= null){outline.Dispose();}
  	if(mapper!= null){mapper.Dispose();}
  	if(actor!= null){actor.Dispose();}
  	if(extract1!= null){extract1.Dispose();}
  	if(surf1!= null){surf1.Dispose();}
  	if(tris!= null){tris.Dispose();}
  	if(mapper1!= null){mapper1.Dispose();}
  	if(actor1!= null){actor1.Dispose();}
  	if(rectWriter!= null){rectWriter.Dispose();}
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  }

}
//--- end of script --//

