using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\combStreamers2.tcl
// output file is AVcombStreamers2.cs
/// <summary>
/// The testing class derived from AVcombStreamers2
/// </summary>
public class AVcombStreamers2Class
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVcombStreamers2(String [] argv)
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
  pl3d = new vtkPLOT3DReader();
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
  streamer = new vtkDashedStreamLine();
  streamer.SetInputConnection((vtkAlgorithmOutput)pl3d.GetOutputPort());
  streamer.SetSource((vtkDataSet)ps.GetOutput());
  streamer.SetMaximumPropagationTime((double)100);
  streamer.SetIntegrationStepLength((double).2);
  streamer.SetStepLength((double).001);
  streamer.SetNumberOfThreads((int)1);
  streamer.SetIntegrationDirectionToForward();
  streamMapper = vtkPolyDataMapper.New();
  streamMapper.SetInputConnection((vtkAlgorithmOutput)streamer.GetOutputPort());
  streamMapper.SetScalarRange(
      (double)((vtkDataSet)pl3d.GetOutput()).GetScalarRange()[0],
      (double)((vtkDataSet)pl3d.GetOutput()).GetScalarRange()[1]);
  streamline = new vtkActor();
  streamline.SetMapper((vtkMapper)streamMapper);
  outline = new vtkStructuredGridOutlineFilter();
  outline.SetInputConnection((vtkAlgorithmOutput)pl3d.GetOutputPort());
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
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkPLOT3DReader pl3d;
static vtkPlaneSource ps;
static vtkPolyDataMapper psMapper;
static vtkActor psActor;
static vtkDashedStreamLine streamer;
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
        public static vtkDashedStreamLine Getstreamer()
        {
            return streamer;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setstreamer(vtkDashedStreamLine toSet)
        {
            streamer = toSet;
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
  	if(streamer!= null){streamer.Dispose();}
  	if(streamMapper!= null){streamMapper.Dispose();}
  	if(streamline!= null){streamline.Dispose();}
  	if(outline!= null){outline.Dispose();}
  	if(outlineMapper!= null){outlineMapper.Dispose();}
  	if(outlineActor!= null){outlineActor.Dispose();}
  	if(cam1!= null){cam1.Dispose();}
  }

}
//--- end of script --//

