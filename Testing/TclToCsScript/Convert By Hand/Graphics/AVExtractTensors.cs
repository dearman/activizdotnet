using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\ExtractTensors.tcl
// output file is AVExtractTensors.cs
/// <summary>
/// The testing class derived from AVExtractTensors
/// </summary>
public class AVExtractTensorsClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVExtractTensors(String [] argv)
  {
  //Prefix Content is: ""
  
  // create tensor ellipsoids[]
  // Create the RenderWindow, Renderer and interactive renderer[]
  //[]
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  ptLoad = new vtkPointLoad();
  ptLoad.SetLoadValue((double)100.0);
  ptLoad.SetSampleDimensions((int)30,(int)30,(int)30);
  ptLoad.ComputeEffectiveStressOn();
  ptLoad.SetModelBounds((double)-10,(double)10,(double)-10,(double)10,(double)-10,(double)10);
  extractTensor = new vtkExtractTensorComponents();
  extractTensor.SetInputConnection((vtkAlgorithmOutput)ptLoad.GetOutputPort());
  extractTensor.ScalarIsEffectiveStress();
  extractTensor.ScalarIsComponent();
  extractTensor.ExtractScalarsOn();
  extractTensor.ExtractVectorsOn();
  extractTensor.ExtractNormalsOff();
  extractTensor.ExtractTCoordsOn();
  contour = new vtkContourFilter();
  contour.SetInputConnection((vtkAlgorithmOutput)extractTensor.GetOutputPort());
  contour.SetValue((int)0,(double)0);
  probe = new vtkProbeFilter();
  probe.SetInputConnection((vtkAlgorithmOutput)contour.GetOutputPort());
  probe.SetSource((vtkDataObject)ptLoad.GetOutput());
  su = new vtkLoopSubdivisionFilter();
  su.SetInputConnection((vtkAlgorithmOutput)probe.GetOutputPort());
  su.SetNumberOfSubdivisions((int)1);
  s1Mapper = vtkPolyDataMapper.New();
  s1Mapper.SetInputConnection((vtkAlgorithmOutput)probe.GetOutputPort());
  //    s1Mapper SetInputConnection [su GetOutputPort][]
  s1Actor = new vtkActor();
  s1Actor.SetMapper((vtkMapper)s1Mapper);
  //[]
  // plane for context[]
  //[]
  g = new vtkImageDataGeometryFilter();
  g.SetInputConnection((vtkAlgorithmOutput)ptLoad.GetOutputPort());
  g.SetExtent((int)0,(int)100,(int)0,(int)100,(int)0,(int)0);
  g.Update();
  //for scalar range[]
  gm = vtkPolyDataMapper.New();
  gm.SetInputConnection((vtkAlgorithmOutput)g.GetOutputPort());
  gm.SetScalarRange((double)((vtkDataSet)g.GetOutput()).GetScalarRange()[0],(double)((vtkDataSet)g.GetOutput()).GetScalarRange()[1]);
  ga = new vtkActor();
  ga.SetMapper((vtkMapper)gm);
  s1Mapper.SetScalarRange((double)((vtkDataSet)g.GetOutput()).GetScalarRange()[0],(double)((vtkDataSet)g.GetOutput()).GetScalarRange()[1]);
  //[]
  // Create outline around data[]
  //[]
  outline = new vtkOutlineFilter();
  outline.SetInputConnection((vtkAlgorithmOutput)ptLoad.GetOutputPort());
  outlineMapper = vtkPolyDataMapper.New();
  outlineMapper.SetInputConnection((vtkAlgorithmOutput)outline.GetOutputPort());
  outlineActor = new vtkActor();
  outlineActor.SetMapper((vtkMapper)outlineMapper);
  outlineActor.GetProperty().SetColor((double)0,(double)0,(double)0);
  //[]
  // Create cone indicating application of load[]
  //[]
  coneSrc = new vtkConeSource();
  coneSrc.SetRadius((double).5);
  coneSrc.SetHeight((double)2);
  coneMap = vtkPolyDataMapper.New();
  coneMap.SetInputConnection((vtkAlgorithmOutput)coneSrc.GetOutputPort());
  coneActor = new vtkActor();
  coneActor.SetMapper((vtkMapper)coneMap);
  coneActor.SetPosition((double)0,(double)0,(double)11);
  coneActor.RotateY((double)90);
  coneActor.GetProperty().SetColor((double)1,(double)0,(double)0);
  camera = new vtkCamera();
  camera.SetFocalPoint((double)0.113766,(double)-1.13665,(double)-1.01919);
  camera.SetPosition((double)-29.4886,(double)-63.1488,(double)26.5807);
  camera.SetViewAngle((double)24.4617);
  camera.SetViewUp((double)0.17138,(double)0.331163,(double)0.927879);
  camera.SetClippingRange((double)1,(double)100);
  ren1.AddActor((vtkProp)s1Actor);
  ren1.AddActor((vtkProp)outlineActor);
  ren1.AddActor((vtkProp)coneActor);
  ren1.AddActor((vtkProp)ga);
  ren1.SetBackground((double)1.0,(double)1.0,(double)1.0);
  ren1.SetActiveCamera((vtkCamera)camera);
  renWin.SetSize((int)300,(int)300);
  renWin.Render();
  // prevent the tk window from showing up then start the event loop[]
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkPointLoad ptLoad;
static vtkExtractTensorComponents extractTensor;
static vtkContourFilter contour;
static vtkProbeFilter probe;
static vtkLoopSubdivisionFilter su;
static vtkPolyDataMapper s1Mapper;
static vtkActor s1Actor;
static vtkImageDataGeometryFilter g;
static vtkPolyDataMapper gm;
static vtkActor ga;
static vtkOutlineFilter outline;
static vtkPolyDataMapper outlineMapper;
static vtkActor outlineActor;
static vtkConeSource coneSrc;
static vtkPolyDataMapper coneMap;
static vtkActor coneActor;
static vtkCamera camera;


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
        public static vtkPointLoad GetptLoad()
        {
            return ptLoad;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetptLoad(vtkPointLoad toSet)
        {
            ptLoad = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkExtractTensorComponents GetextractTensor()
        {
            return extractTensor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetextractTensor(vtkExtractTensorComponents toSet)
        {
            extractTensor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkContourFilter Getcontour()
        {
            return contour;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcontour(vtkContourFilter toSet)
        {
            contour = toSet;
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
        public static vtkLoopSubdivisionFilter Getsu()
        {
            return su;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setsu(vtkLoopSubdivisionFilter toSet)
        {
            su = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Gets1Mapper()
        {
            return s1Mapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sets1Mapper(vtkPolyDataMapper toSet)
        {
            s1Mapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Gets1Actor()
        {
            return s1Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sets1Actor(vtkActor toSet)
        {
            s1Actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageDataGeometryFilter Getg()
        {
            return g;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setg(vtkImageDataGeometryFilter toSet)
        {
            g = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Getgm()
        {
            return gm;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setgm(vtkPolyDataMapper toSet)
        {
            gm = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getga()
        {
            return ga;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setga(vtkActor toSet)
        {
            ga = toSet;
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
        public static vtkConeSource GetconeSrc()
        {
            return coneSrc;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetconeSrc(vtkConeSource toSet)
        {
            coneSrc = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetconeMap()
        {
            return coneMap;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetconeMap(vtkPolyDataMapper toSet)
        {
            coneMap = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetconeActor()
        {
            return coneActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetconeActor(vtkActor toSet)
        {
            coneActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCamera Getcamera()
        {
            return camera;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcamera(vtkCamera toSet)
        {
            camera = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(ptLoad!= null){ptLoad.Dispose();}
  	if(extractTensor!= null){extractTensor.Dispose();}
  	if(contour!= null){contour.Dispose();}
  	if(probe!= null){probe.Dispose();}
  	if(su!= null){su.Dispose();}
  	if(s1Mapper!= null){s1Mapper.Dispose();}
  	if(s1Actor!= null){s1Actor.Dispose();}
  	if(g!= null){g.Dispose();}
  	if(gm!= null){gm.Dispose();}
  	if(ga!= null){ga.Dispose();}
  	if(outline!= null){outline.Dispose();}
  	if(outlineMapper!= null){outlineMapper.Dispose();}
  	if(outlineActor!= null){outlineActor.Dispose();}
  	if(coneSrc!= null){coneSrc.Dispose();}
  	if(coneMap!= null){coneMap.Dispose();}
  	if(coneActor!= null){coneActor.Dispose();}
  	if(camera!= null){camera.Dispose();}
  }

}
//--- end of script --//

