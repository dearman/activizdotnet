using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\probe.tcl
// output file is AVprobe.cs
/// <summary>
/// The testing class derived from AVprobe
/// </summary>
public class AVprobeClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVprobe(String [] argv)
  {
  //Prefix Content is: ""
  
  // Create the RenderWindow, Renderer and both Actors[]
  //[]
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  // cut data[]
  pl3d = new vtkPLOT3DReader();
  pl3d.SetXYZFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/combxyz.bin");
  pl3d.SetQFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/combq.bin");
  pl3d.SetScalarFunctionNumber((int)100);
  pl3d.SetVectorFunctionNumber((int)202);
  pl3d.Update();
  plane = new vtkPlane();
  plane.SetOrigin(pl3d.GetOutput().GetCenter()[0],pl3d.GetOutput().GetCenter()[1],pl3d.GetOutput().GetCenter()[2]);
  plane.SetNormal((double)-0.287,(double)0,(double)0.9579);
  planeCut = new vtkCutter();
  planeCut.SetInputConnection((vtkAlgorithmOutput)pl3d.GetOutputPort());
  planeCut.SetCutFunction((vtkImplicitFunction)plane);
  probe = new vtkProbeFilter();
  probe.SetInputConnection((vtkAlgorithmOutput)planeCut.GetOutputPort());
  probe.SetSourceConnection((vtkAlgorithmOutput)pl3d.GetOutputPort());
  cutMapper = new vtkDataSetMapper();
  cutMapper.SetInputConnection((vtkAlgorithmOutput)probe.GetOutputPort());
  cutMapper.SetScalarRange((double)((vtkStructuredGrid)pl3d.GetOutput()).GetPointData().GetScalars().GetRange()[0],
      (double)((vtkStructuredGrid)pl3d.GetOutput()).GetPointData().GetScalars().GetRange()[1]);
  cutActor = new vtkActor();
  cutActor.SetMapper((vtkMapper)cutMapper);
  //extract plane[]
  compPlane = new vtkStructuredGridGeometryFilter();
  compPlane.SetInputConnection((vtkAlgorithmOutput)pl3d.GetOutputPort());
  compPlane.SetExtent((int)0,(int)100,(int)0,(int)100,(int)9,(int)9);
  planeMapper = vtkPolyDataMapper.New();
  planeMapper.SetInputConnection((vtkAlgorithmOutput)compPlane.GetOutputPort());
  planeMapper.ScalarVisibilityOff();
  planeActor = new vtkActor();
  planeActor.SetMapper((vtkMapper)planeMapper);
  planeActor.GetProperty().SetRepresentationToWireframe();
  planeActor.GetProperty().SetColor((double)0,(double)0,(double)0);
  //outline[]
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
  ren1.AddActor((vtkProp)planeActor);
  ren1.AddActor((vtkProp)cutActor);
  ren1.SetBackground((double)1,(double)1,(double)1);
  renWin.SetSize((int)400,(int)300);
  cam1 = ren1.GetActiveCamera();
  cam1.SetClippingRange((double)11.1034,(double)59.5328);
  cam1.SetFocalPoint((double)9.71821,(double)0.458166,(double)29.3999);
  cam1.SetPosition((double)-2.95748,(double)-26.7271,(double)44.5309);
  cam1.SetViewUp((double)0.0184785,(double)0.479657,(double)0.877262);
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
static vtkPlane plane;
static vtkCutter planeCut;
static vtkProbeFilter probe;
static vtkDataSetMapper cutMapper;
static vtkActor cutActor;
static vtkStructuredGridGeometryFilter compPlane;
static vtkPolyDataMapper planeMapper;
static vtkActor planeActor;
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
        public static vtkPlane Getplane()
        {
            return plane;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setplane(vtkPlane toSet)
        {
            plane = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCutter GetplaneCut()
        {
            return planeCut;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetplaneCut(vtkCutter toSet)
        {
            planeCut = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkProbeFilter Getprobe()
        {
            return probe;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setprobe(vtkProbeFilter toSet)
        {
            probe = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetcutMapper()
        {
            return cutMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetcutMapper(vtkDataSetMapper toSet)
        {
            cutMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetcutActor()
        {
            return cutActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetcutActor(vtkActor toSet)
        {
            cutActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter GetcompPlane()
        {
            return compPlane;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetcompPlane(vtkStructuredGridGeometryFilter toSet)
        {
            compPlane = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetplaneMapper()
        {
            return planeMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetplaneMapper(vtkPolyDataMapper toSet)
        {
            planeMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetplaneActor()
        {
            return planeActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetplaneActor(vtkActor toSet)
        {
            planeActor = toSet;
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
  	if(plane!= null){plane.Dispose();}
  	if(planeCut!= null){planeCut.Dispose();}
  	if(probe!= null){probe.Dispose();}
  	if(cutMapper!= null){cutMapper.Dispose();}
  	if(cutActor!= null){cutActor.Dispose();}
  	if(compPlane!= null){compPlane.Dispose();}
  	if(planeMapper!= null){planeMapper.Dispose();}
  	if(planeActor!= null){planeActor.Dispose();}
  	if(outline!= null){outline.Dispose();}
  	if(outlineMapper!= null){outlineMapper.Dispose();}
  	if(outlineActor!= null){outlineActor.Dispose();}
  	if(outlineProp!= null){outlineProp.Dispose();}
  	if(cam1!= null){cam1.Dispose();}
  }

}
//--- end of script --//

