using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\StreamPolyData.tcl
// output file is AVStreamPolyData.cs
/// <summary>
/// The testing class derived from AVStreamPolyData
/// </summary>
public class AVStreamPolyDataClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVStreamPolyData(String [] argv)
  {
  //Prefix Content is: ""
  
  NUMBER_OF_PIECES = 5;
  // Generate implicit model of a sphere[]
  //[]
  // Create renderer stuff[]
  //[]
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  // create pipeline that handles ghost cells[]
  sphere = new vtkSphereSource();
  sphere.SetRadius((double)3);
  sphere.SetPhiResolution((int)100);
  sphere.SetThetaResolution((int)150);
  // sphere AddObserver StartEvent {tk_messageBox -message "Executing with piece [[sphere GetOutput] GetUpdatePiece]"}[]
  // Just playing with an alternative that is not currently used.[]
  //method moved
  // Just playing with an alternative that is not currently used.[]
  deci = new vtkDecimatePro();
  deci.SetInputConnection((vtkAlgorithmOutput)sphere.GetOutputPort());
  // this did not remove seams as I thought it would[]
  deci.BoundaryVertexDeletionOff();
  //deci PreserveTopologyOn[]
  // Since quadric Clustering does not handle borders properly yet,[]
  // the pieces will have dramatic "eams"[]
  q = new vtkQuadricClustering();
  q.SetInputConnection((vtkAlgorithmOutput)sphere.GetOutputPort());
  q.SetNumberOfXDivisions((int)5);
  q.SetNumberOfYDivisions((int)5);
  q.SetNumberOfZDivisions((int)10);
  q.UseInputPointsOn();
  streamer = new vtkPolyDataStreamer();
  //streamer SetInputConnection [deci GetOutputPort][]
  streamer.SetInputConnection((vtkAlgorithmOutput)q.GetOutputPort());
  //streamer SetInputConnection [pdn GetOutputPort][]
  streamer.SetNumberOfStreamDivisions((int)NUMBER_OF_PIECES);
  mapper = vtkPolyDataMapper.New();
  mapper.SetInputConnection((vtkAlgorithmOutput)streamer.GetOutputPort());
  mapper.ScalarVisibilityOff();
  mapper.SetPiece((int)0);
  mapper.SetNumberOfPieces((int)2);
  mapper.ImmediateModeRenderingOn();
  actor = new vtkActor();
  actor.SetMapper((vtkMapper)mapper);
  actor.GetProperty().SetColor((double) 0.8300, 0.2400, 0.1000 );
  // Add the actors to the renderer, set the background and size[]
  //[]
  ren1.GetActiveCamera().SetPosition((double)5,(double)5,(double)10);
  ren1.GetActiveCamera().SetFocalPoint((double)0,(double)0,(double)0);
  ren1.AddActor((vtkProp)actor);
  ren1.SetBackground((double)1,(double)1,(double)1);
  renWin.SetSize((int)300,(int)300);
  iren.Initialize();
  // render the image[]
  //[]
  // prevent the tk window from showing up then start the event loop[]
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static int NUMBER_OF_PIECES;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkSphereSource sphere;
static vtkExtractPolyDataPiece piece;
static vtkPolyDataNormals pdn;
static vtkDecimatePro deci;
static vtkQuadricClustering q;
static vtkPolyDataStreamer streamer;
static vtkPolyDataMapper mapper;
static vtkActor actor;


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
        public static int GetNUMBER_OF_PIECES()
        {
            return NUMBER_OF_PIECES;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetNUMBER_OF_PIECES(int toSet)
        {
            NUMBER_OF_PIECES = toSet;
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
        public static vtkPolyDataNormals Getpdn()
        {
            return pdn;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpdn(vtkPolyDataNormals toSet)
        {
            pdn = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDecimatePro Getdeci()
        {
            return deci;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setdeci(vtkDecimatePro toSet)
        {
            deci = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkQuadricClustering Getq()
        {
            return q;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setq(vtkQuadricClustering toSet)
        {
            q = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataStreamer Getstreamer()
        {
            return streamer;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setstreamer(vtkPolyDataStreamer toSet)
        {
            streamer = toSet;
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
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(sphere!= null){sphere.Dispose();}
  	if(piece!= null){piece.Dispose();}
  	if(pdn!= null){pdn.Dispose();}
  	if(deci!= null){deci.Dispose();}
  	if(q!= null){q.Dispose();}
  	if(streamer!= null){streamer.Dispose();}
  	if(mapper!= null){mapper.Dispose();}
  	if(actor!= null){actor.Dispose();}
  }

}
//--- end of script --//

