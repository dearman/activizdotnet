using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\streamTracer.tcl
// output file is AVstreamTracer.cs
/// <summary>
/// The testing class derived from AVstreamTracer
/// </summary>
public class AVstreamTracerClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVstreamTracer(String [] argv)
  {
  //Prefix Content is: ""
  
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();

  // this test has some wireframe geometry
  // Make sure multisampling is disabled to avoid generating multiple
  // regression images
  // renWin SetMultiSamples 0

  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  // read data[]
  //[]
  reader = new vtkStructuredGridReader();
  reader.SetFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/office.binary.vtk");
  reader.Update();
  //force a read to occur[]
  outline = new vtkStructuredGridOutlineFilter();
  outline.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  mapOutline = vtkPolyDataMapper.New();
  mapOutline.SetInputConnection((vtkAlgorithmOutput)outline.GetOutputPort());
  outlineActor = new vtkActor();
  outlineActor.SetMapper((vtkMapper)mapOutline);
  outlineActor.GetProperty().SetColor((double)0,(double)0,(double)0);
  rk = new vtkRungeKutta45();
  // Create source for streamtubes[]
  streamer = new vtkStreamTracer();
  streamer.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  streamer.SetStartPosition((double)0.1,(double)2.1,(double)0.5);
  streamer.SetMaximumPropagation((double)500);
  streamer.SetIntegrationStepUnit(2);
  streamer.SetMinimumIntegrationStep((double)0.1);
  streamer.SetMaximumIntegrationStep((double)1.0);
  streamer.SetInitialIntegrationStep((double)0.2);
  streamer.SetIntegrationDirection((int)0);
  streamer.SetIntegrator((vtkInitialValueProblemSolver)rk);
  streamer.SetRotationScale((double)0.5);
  streamer.SetMaximumError((double)1.0e-8);
  aa = new vtkAssignAttribute();
  aa.SetInputConnection((vtkAlgorithmOutput)streamer.GetOutputPort());
  aa.Assign((string)"Normals",(string)"NORMALS",(string)"POINT_DATA");
  rf1 = new vtkRibbonFilter();
  rf1.SetInputConnection((vtkAlgorithmOutput)aa.GetOutputPort());
  rf1.SetWidth((double)0.1);
  rf1.VaryWidthOff();
  mapStream = vtkPolyDataMapper.New();
  mapStream.SetInputConnection((vtkAlgorithmOutput)rf1.GetOutputPort());
  mapStream.SetScalarRange((double)((vtkDataSet)reader.GetOutput()).GetScalarRange()[0],(double)((vtkDataSet)reader.GetOutput()).GetScalarRange()[1]);
  streamActor = new vtkActor();
  streamActor.SetMapper((vtkMapper)mapStream);
  ren1.AddActor((vtkProp)outlineActor);
  ren1.AddActor((vtkProp)streamActor);
  ren1.SetBackground((double)0.4,(double)0.4,(double)0.5);
  cam = ren1.GetActiveCamera();
  cam.SetPosition((double)-2.35599,(double)-3.35001,(double)4.59236);
  cam.SetFocalPoint((double)2.255,(double)2.255,(double)1.28413);
  cam.SetViewUp((double)0.311311,(double)0.279912,(double)0.908149);
  cam.SetClippingRange((double)1.12294,(double)16.6226);
  renWin.SetSize((int)300,(int)200);
  iren.Initialize();
  // interact with data[]
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkStructuredGridReader reader;
static vtkStructuredGridOutlineFilter outline;
static vtkPolyDataMapper mapOutline;
static vtkActor outlineActor;
static vtkRungeKutta45 rk;
static vtkStreamTracer streamer;
static vtkAssignAttribute aa;
static vtkRibbonFilter rf1;
static vtkPolyDataMapper mapStream;
static vtkActor streamActor;
static vtkCamera cam;


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
        public static vtkStructuredGridReader Getreader()
        {
            return reader;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setreader(vtkStructuredGridReader toSet)
        {
            reader = toSet;
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
        public static vtkPolyDataMapper GetmapOutline()
        {
            return mapOutline;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapOutline(vtkPolyDataMapper toSet)
        {
            mapOutline = toSet;
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
        public static vtkRungeKutta45 Getrk()
        {
            return rk;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setrk(vtkRungeKutta45 toSet)
        {
            rk = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStreamTracer Getstreamer()
        {
            return streamer;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setstreamer(vtkStreamTracer toSet)
        {
            streamer = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkAssignAttribute Getaa()
        {
            return aa;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setaa(vtkAssignAttribute toSet)
        {
            aa = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRibbonFilter Getrf1()
        {
            return rf1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setrf1(vtkRibbonFilter toSet)
        {
            rf1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapStream()
        {
            return mapStream;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapStream(vtkPolyDataMapper toSet)
        {
            mapStream = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetstreamActor()
        {
            return streamActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetstreamActor(vtkActor toSet)
        {
            streamActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCamera Getcam()
        {
            return cam;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcam(vtkCamera toSet)
        {
            cam = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(reader!= null){reader.Dispose();}
  	if(outline!= null){outline.Dispose();}
  	if(mapOutline!= null){mapOutline.Dispose();}
  	if(outlineActor!= null){outlineActor.Dispose();}
  	if(rk!= null){rk.Dispose();}
  	if(streamer!= null){streamer.Dispose();}
  	if(aa!= null){aa.Dispose();}
  	if(rf1!= null){rf1.Dispose();}
  	if(mapStream!= null){mapStream.Dispose();}
  	if(streamActor!= null){streamActor.Dispose();}
  	if(cam!= null){cam.Dispose();}
  }

}
//--- end of script --//

