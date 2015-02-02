using Kitware.VTK;
using System;
// input file is C:\VTK\Parallel\Testing\Tcl\RectOutline.tcl
// output file is AVRectOutline.cs
/// <summary>
/// The testing class derived from AVRectOutline
/// </summary>
public class AVRectOutlineClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVRectOutline(String [] argv)
  {
  //Prefix Content is: ""
  
  // create pipeline[]
  //[]
  reader = new vtkDataSetReader();
  reader.SetFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/RectGrid2.vtk");
  reader.Update();
  // here to force exact extent[]
  elev = new vtkElevationFilter();
  elev.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  outline = new vtkRectilinearGridOutlineFilter();
  outline.SetInput((vtkDataObject)elev.GetRectilinearGridOutput());
  outlineMapper = vtkPolyDataMapper.New();
  outlineMapper.SetInputConnection((vtkAlgorithmOutput)outline.GetOutputPort());
  outlineMapper.SetNumberOfPieces((int)2);
  outlineMapper.SetPiece((int)1);
  outlineActor = new vtkActor();
  outlineActor.SetMapper((vtkMapper)outlineMapper);
  outlineActor.GetProperty().SetColor((double) 0.0000, 0.0000, 0.0000 );
  // Graphics stuff[]
  // Create the RenderWindow, Renderer and both Actors[]
  //[]
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  // Add the actors to the renderer, set the background and size[]
  //[]
  ren1.AddActor((vtkProp)outlineActor);
  ren1.SetBackground((double)1,(double)1,(double)1);
  renWin.SetSize((int)400,(int)400);
  cam1 = ren1.GetActiveCamera();
  cam1.SetClippingRange((double)3.76213,(double)10.712);
  cam1.SetFocalPoint((double)-0.0842503,(double)-0.136905,(double)0.610234);
  cam1.SetPosition((double)2.53813,(double)2.2678,(double)-5.22172);
  cam1.SetViewUp((double)-0.241047,(double)0.930635,(double)0.275343);
  iren.Initialize();
  // render the image[]
  //[]
  // prevent the tk window from showing up then start the event loop[]
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkDataSetReader reader;
static vtkElevationFilter elev;
static vtkRectilinearGridOutlineFilter outline;
static vtkPolyDataMapper outlineMapper;
static vtkActor outlineActor;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
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
        public static vtkDataSetReader Getreader()
        {
            return reader;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setreader(vtkDataSetReader toSet)
        {
            reader = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkElevationFilter Getelev()
        {
            return elev;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setelev(vtkElevationFilter toSet)
        {
            elev = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRectilinearGridOutlineFilter Getoutline()
        {
            return outline;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setoutline(vtkRectilinearGridOutlineFilter toSet)
        {
            outline = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetoutlineMapper()
        {
            return outlineMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetoutlineMapper(vtkPolyDataMapper toSet)
        {
            outlineMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetoutlineActor()
        {
            return outlineActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetoutlineActor(vtkActor toSet)
        {
            outlineActor = toSet;
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
  	if(reader!= null){reader.Dispose();}
  	if(elev!= null){elev.Dispose();}
  	if(outline!= null){outline.Dispose();}
  	if(outlineMapper!= null){outlineMapper.Dispose();}
  	if(outlineActor!= null){outlineActor.Dispose();}
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(cam1!= null){cam1.Dispose();}
  }

}
//--- end of script --//

