using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\polyConn.tcl
// output file is AVpolyConn.cs
/// <summary>
/// The testing class derived from AVpolyConn
/// </summary>
public class AVpolyConnClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVpolyConn(String [] argv)
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
  pl3d = new vtkPLOT3DReader();
  pl3d.SetXYZFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/combxyz.bin");
  pl3d.SetQFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/combq.bin");
  pl3d.SetScalarFunctionNumber((int)100);
  pl3d.SetVectorFunctionNumber((int)202);
  pl3d.Update();
  // planes to connect[]
  plane1 = new vtkStructuredGridGeometryFilter();
  plane1.SetInputConnection((vtkAlgorithmOutput)pl3d.GetOutputPort());
  plane1.SetExtent((int)20,(int)20,(int)0,(int)100,(int)0,(int)100);
  conn = new vtkPolyDataConnectivityFilter();
  conn.SetInputConnection((vtkAlgorithmOutput)plane1.GetOutputPort());
  conn.ScalarConnectivityOn();
  conn.SetScalarRange((double)0.19,(double)0.25);
  plane1Map = vtkPolyDataMapper.New();
  plane1Map.SetInputConnection((vtkAlgorithmOutput)conn.GetOutputPort());
  plane1Map.SetScalarRange((double)((vtkDataSet)pl3d.GetOutput()).GetScalarRange()[0],
      (double)((vtkDataSet)pl3d.GetOutput()).GetScalarRange()[1]);
  plane1Actor = new vtkActor();
  plane1Actor.SetMapper((vtkMapper)plane1Map);
  plane1Actor.GetProperty().SetOpacity((double)0.999);
  // outline[]
  outline = new vtkStructuredGridOutlineFilter();
  outline.SetInputConnection((vtkAlgorithmOutput)pl3d.GetOutputPort());
  outlineMapper = vtkPolyDataMapper.New();
  outlineMapper.SetInputConnection((vtkAlgorithmOutput)outline.GetOutputPort());
  outlineActor = new vtkActor();
  outlineActor.SetMapper((vtkMapper)outlineMapper);
  outlineProp = outlineActor.GetProperty();
  outlineProp.SetColor((double)0,(double)0,(double)0);
  // Add the actors to the renderer, set the background and size[]
  //[]
  ren1.AddActor((vtkProp)outlineActor);
  ren1.AddActor((vtkProp)plane1Actor);
  ren1.SetBackground((double)1,(double)1,(double)1);
  renWin.SetSize((int)300,(int)300);
  cam1 = new vtkCamera();
  cam1.SetClippingRange((double)14.29,(double)63.53);
  cam1.SetFocalPoint((double)8.58522,(double)1.58266,(double)30.6486);
  cam1.SetPosition((double)37.6808,(double)-20.1298,(double)35.4016);
  cam1.SetViewAngle((double)30);
  cam1.SetViewUp((double)-0.0566235,(double)0.140504,(double)0.98846);
  ren1.SetActiveCamera((vtkCamera)cam1);
  iren.Initialize();
  // render the image[]
  //[]
  // prevent the tk window from showing up then start the event loop[]
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkPLOT3DReader pl3d;
static vtkStructuredGridGeometryFilter plane1;
static vtkPolyDataConnectivityFilter conn;
static vtkPolyDataMapper plane1Map;
static vtkActor plane1Actor;
static vtkStructuredGridOutlineFilter outline;
static vtkPolyDataMapper outlineMapper;
static vtkActor outlineActor;
static vtkProperty outlineProp;
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
        public static vtkPLOT3DReader Getpl3d()
        {
            return pl3d;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpl3d(vtkPLOT3DReader toSet)
        {
            pl3d = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getplane1()
        {
            return plane1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setplane1(vtkStructuredGridGeometryFilter toSet)
        {
            plane1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataConnectivityFilter Getconn()
        {
            return conn;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setconn(vtkPolyDataConnectivityFilter toSet)
        {
            conn = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Getplane1Map()
        {
            return plane1Map;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setplane1Map(vtkPolyDataMapper toSet)
        {
            plane1Map = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getplane1Actor()
        {
            return plane1Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setplane1Actor(vtkActor toSet)
        {
            plane1Actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridOutlineFilter Getoutline()
        {
            return outline;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setoutline(vtkStructuredGridOutlineFilter toSet)
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
        public static vtkProperty GetoutlineProp()
        {
            return outlineProp;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetoutlineProp(vtkProperty toSet)
        {
            outlineProp = toSet;
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
  	if(pl3d!= null){pl3d.Dispose();}
  	if(plane1!= null){plane1.Dispose();}
  	if(conn!= null){conn.Dispose();}
  	if(plane1Map!= null){plane1Map.Dispose();}
  	if(plane1Actor!= null){plane1Actor.Dispose();}
  	if(outline!= null){outline.Dispose();}
  	if(outlineMapper!= null){outlineMapper.Dispose();}
  	if(outlineActor!= null){outlineActor.Dispose();}
  	if(outlineProp!= null){outlineProp.Dispose();}
  	if(cam1!= null){cam1.Dispose();}
  }

}
//--- end of script --//

