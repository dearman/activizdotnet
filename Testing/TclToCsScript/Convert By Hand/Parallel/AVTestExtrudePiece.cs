using Kitware.VTK;
using System;
// input file is C:\VTK\Parallel\Testing\Tcl\TestExtrudePiece.tcl
// output file is AVTestExtrudePiece.cs
/// <summary>
/// The testing class derived from AVTestExtrudePiece
/// </summary>
public class AVTestExtrudePieceClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVTestExtrudePiece(String [] argv)
  {
  //Prefix Content is: ""
  
  disk = new vtkDiskSource();
  disk.SetRadialResolution((int)2);
  disk.SetCircumferentialResolution((int)9);
  clean = new vtkCleanPolyData();
  clean.SetInputConnection((vtkAlgorithmOutput)disk.GetOutputPort());
  clean.SetTolerance((double)0.01);
  piece = new vtkExtractPolyDataPiece();
  piece.SetInputConnection((vtkAlgorithmOutput)clean.GetOutputPort());
  extrude = new vtkPLinearExtrusionFilter();
  extrude.SetInputConnection((vtkAlgorithmOutput)piece.GetOutputPort());
  extrude.PieceInvariantOn();
  // Create the RenderWindow, Renderer and both Actors[]
  //[]
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  mapper = vtkPolyDataMapper.New();
  mapper.SetInputConnection((vtkAlgorithmOutput)extrude.GetOutputPort());
  mapper.SetNumberOfPieces((int)2);
  mapper.SetPiece((int)1);
  bf = new vtkProperty();
  bf.SetColor((double)1,(double)0,(double)0);
  actor = new vtkActor();
  actor.SetMapper((vtkMapper)mapper);
  actor.GetProperty().SetColor((double)1,(double)1,(double)0.8);
  actor.SetBackfaceProperty((vtkProperty)bf);
  // Add the actors to the renderer, set the background and size[]
  //[]
  ren1.AddActor((vtkProp)actor);
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
static vtkDiskSource disk;
static vtkCleanPolyData clean;
static vtkExtractPolyDataPiece piece;
static vtkPLinearExtrusionFilter extrude;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkPolyDataMapper mapper;
static vtkProperty bf;
static vtkActor actor;
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
        public static vtkDiskSource Getdisk()
        {
            return disk;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setdisk(vtkDiskSource toSet)
        {
            disk = toSet;
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
        public static vtkExtractPolyDataPiece Getpiece()
        {
            return piece;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpiece(vtkExtractPolyDataPiece toSet)
        {
            piece = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPLinearExtrusionFilter Getextrude()
        {
            return extrude;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setextrude(vtkPLinearExtrusionFilter toSet)
        {
            extrude = toSet;
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
        public static vtkProperty Getbf()
        {
            return bf;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbf(vtkProperty toSet)
        {
            bf = toSet;
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
  	if(disk!= null){disk.Dispose();}
  	if(clean!= null){clean.Dispose();}
  	if(piece!= null){piece.Dispose();}
  	if(extrude!= null){extrude.Dispose();}
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(mapper!= null){mapper.Dispose();}
  	if(bf!= null){bf.Dispose();}
  	if(actor!= null){actor.Dispose();}
  	if(cam1!= null){cam1.Dispose();}
  }

}
//--- end of script --//

