using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\combStreamers.tcl
// output file is AVcombStreamers.cs
/// <summary>
/// The testing class derived from AVcombStreamers
/// </summary>
public class AVcombStreamersClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVcombStreamers(String [] argv)
  {
  //Prefix Content is: ""
  
  // Create the RenderWindow, Renderer and both Actors[]
  //[]
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  // create pipeline[]
  //[]
  pl3d = new vtkMultiBlockPLOT3DReader();
  pl3d.SetXYZFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/combxyz.bin");
  pl3d.SetQFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/combq.bin");
  pl3d.SetScalarFunctionNumber((int)100);
  pl3d.SetVectorFunctionNumber((int)202);
  pl3d.Update();
  ps = new vtkPlaneSource();
  ps.SetXResolution((int)4);
  ps.SetYResolution((int)4);
  ps.SetOrigin((double)2,(double)-2,(double)26);
  ps.SetPoint1((double)2,(double)2,(double)26);
  ps.SetPoint2((double)2,(double)-2,(double)32);
  psMapper = vtkPolyDataMapper.New();
  psMapper.SetInputConnection((vtkAlgorithmOutput)ps.GetOutputPort());
  psActor = new vtkActor();
  psActor.SetMapper((vtkMapper)psMapper);
  psActor.GetProperty().SetRepresentationToWireframe();
  rk4 = new vtkRungeKutta4();
  streamer = new vtkStreamLine();
  streamer.SetInputData((vtkDataSet)pl3d.GetOutput().GetBlock(0));
  streamer.SetSourceConnection(ps.GetOutputPort());
  streamer.SetMaximumPropagationTime((double)100);
  streamer.SetIntegrationStepLength((double).2);
  streamer.SetStepLength((double).001);
  streamer.SetNumberOfThreads((int)1);
  streamer.SetIntegrationDirectionToForward();
  streamer.VorticityOn();
  streamer.SetIntegrator((vtkInitialValueProblemSolver)rk4);
  rf = new vtkRibbonFilter();
  rf.SetInputConnection((vtkAlgorithmOutput)streamer.GetOutputPort());
  rf.SetWidth((double)0.1);
  rf.SetWidthFactor((double)5);
  streamMapper = vtkPolyDataMapper.New();
  streamMapper.SetInputConnection((vtkAlgorithmOutput)rf.GetOutputPort());
  streamMapper.SetScalarRange(
      (double)((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetScalarRange()[0],
      (double)((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetScalarRange()[1]);
  streamline = new vtkActor();
  streamline.SetMapper((vtkMapper)streamMapper);
  outline = new vtkStructuredGridOutlineFilter();
  outline.SetInputData((vtkDataSet)pl3d.GetOutput().GetBlock(0));
  outlineMapper = vtkPolyDataMapper.New();
  outlineMapper.SetInputConnection((vtkAlgorithmOutput)outline.GetOutputPort());
  outlineActor = new vtkActor();
  outlineActor.SetMapper((vtkMapper)outlineMapper);
  // Add the actors to the renderer, set the background and size[]
  //[]
  ren1.AddActor((vtkProp)psActor);
  ren1.AddActor((vtkProp)outlineActor);
  ren1.AddActor((vtkProp)streamline);
  ren1.SetBackground((double)1,(double)1,(double)1);
  renWin.SetSize((int)300,(int)300);
  ren1.SetBackground((double)0.1,(double)0.2,(double)0.4);
  cam1 = ren1.GetActiveCamera();
  cam1.SetClippingRange((double)3.95297,(double)50);
  cam1.SetFocalPoint((double)9.71821,(double)0.458166,(double)29.3999);
  cam1.SetPosition((double)2.7439,(double)-37.3196,(double)38.7167);
  cam1.SetViewUp((double)-0.16123,(double)0.264271,(double)0.950876);
  // render the image[]
  //[]
  renWin.Render();
  // prevent the tk window from showing up then start the event loop[]
  // for testing[]
  threshold = 15;
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkMultiBlockPLOT3DReader pl3d;
static vtkPlaneSource ps;
static vtkPolyDataMapper psMapper;
static vtkActor psActor;
static vtkRungeKutta4 rk4;
static vtkStreamLine streamer;
static vtkRibbonFilter rf;
static vtkPolyDataMapper streamMapper;
static vtkActor streamline;
static vtkStructuredGridOutlineFilter outline;
static vtkPolyDataMapper outlineMapper;
static vtkActor outlineActor;
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
        public static vtkMultiBlockPLOT3DReader Getpl3d()
        {
            return pl3d;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpl3d(vtkMultiBlockPLOT3DReader toSet)
        {
            pl3d = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPlaneSource Getps()
        {
            return ps;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setps(vtkPlaneSource toSet)
        {
            ps = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetpsMapper()
        {
            return psMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetpsMapper(vtkPolyDataMapper toSet)
        {
            psMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetpsActor()
        {
            return psActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetpsActor(vtkActor toSet)
        {
            psActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRungeKutta4 Getrk4()
        {
            return rk4;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setrk4(vtkRungeKutta4 toSet)
        {
            rk4 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStreamLine Getstreamer()
        {
            return streamer;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setstreamer(vtkStreamLine toSet)
        {
            streamer = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRibbonFilter Getrf()
        {
            return rf;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setrf(vtkRibbonFilter toSet)
        {
            rf = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetstreamMapper()
        {
            return streamMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetstreamMapper(vtkPolyDataMapper toSet)
        {
            streamMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getstreamline()
        {
            return streamline;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setstreamline(vtkActor toSet)
        {
            streamline = toSet;
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
  	if(ps!= null){ps.Dispose();}
  	if(psMapper!= null){psMapper.Dispose();}
  	if(psActor!= null){psActor.Dispose();}
  	if(rk4!= null){rk4.Dispose();}
  	if(streamer!= null){streamer.Dispose();}
  	if(rf!= null){rf.Dispose();}
  	if(streamMapper!= null){streamMapper.Dispose();}
  	if(streamline!= null){streamline.Dispose();}
  	if(outline!= null){outline.Dispose();}
  	if(outlineMapper!= null){outlineMapper.Dispose();}
  	if(outlineActor!= null){outlineActor.Dispose();}
  	if(cam1!= null){cam1.Dispose();}
  }

}
//--- end of script --//

