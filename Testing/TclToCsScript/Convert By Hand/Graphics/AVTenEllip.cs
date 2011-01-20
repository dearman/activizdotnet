using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\TenEllip.tcl
// output file is AVTenEllip.cs
/// <summary>
/// The testing class derived from AVTenEllip
/// </summary>
public class AVTenEllipClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVTenEllip(String [] argv)
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
  //[]
  // Create tensor ellipsoids[]
  //[]
  // generate tensors[]
  ptLoad = new vtkPointLoad();
  ptLoad.SetLoadValue((double)100.0);
  ptLoad.SetSampleDimensions((int)6,(int)6,(int)6);
  ptLoad.ComputeEffectiveStressOn();
  ptLoad.SetModelBounds((double)-10,(double)10,(double)-10,(double)10,(double)-10,(double)10);
  // extract plane of data[]
  plane = new vtkImageDataGeometryFilter();
  plane.SetInputConnection((vtkAlgorithmOutput)ptLoad.GetOutputPort());
  plane.SetExtent((int)2,(int)2,(int)0,(int)99,(int)0,(int)99);
  // Generate ellipsoids[]
  sphere = new vtkSphereSource();
  sphere.SetThetaResolution((int)8);
  sphere.SetPhiResolution((int)8);
  ellipsoids = new vtkTensorGlyph();
  ellipsoids.SetInputConnection((vtkAlgorithmOutput)ptLoad.GetOutputPort());
  ellipsoids.SetSourceConnection((vtkAlgorithmOutput)sphere.GetOutputPort());
  ellipsoids.SetScaleFactor((double)10);
  ellipsoids.ClampScalingOn();
  ellipNormals = new vtkPolyDataNormals();
  ellipNormals.SetInputConnection((vtkAlgorithmOutput)ellipsoids.GetOutputPort());
  // Map contour[]
  lut = new vtkLogLookupTable();
  lut.SetHueRange((double).6667,(double)0.0);
  ellipMapper = vtkPolyDataMapper.New();
  ellipMapper.SetInputConnection((vtkAlgorithmOutput)ellipNormals.GetOutputPort());
  ellipMapper.SetLookupTable((vtkScalarsToColors)lut);
  plane.Update();
  //force update for scalar range[]
  ellipMapper.SetScalarRange((double)((vtkDataSet)plane.GetOutput()).GetScalarRange()[0],(double)((vtkDataSet)plane.GetOutput()).GetScalarRange()[1]);
  ellipActor = new vtkActor();
  ellipActor.SetMapper((vtkMapper)ellipMapper);
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
  ren1.AddActor((vtkProp)ellipActor);
  ren1.AddActor((vtkProp)outlineActor);
  ren1.AddActor((vtkProp)coneActor);
  ren1.SetBackground((double)1.0,(double)1.0,(double)1.0);
  ren1.SetActiveCamera((vtkCamera)camera);
  renWin.SetSize((int)400,(int)400);
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
static vtkImageDataGeometryFilter plane;
static vtkSphereSource sphere;
static vtkTensorGlyph ellipsoids;
static vtkPolyDataNormals ellipNormals;
static vtkLogLookupTable lut;
static vtkPolyDataMapper ellipMapper;
static vtkActor ellipActor;
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
        public static vtkImageDataGeometryFilter Getplane()
        {
            return plane;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setplane(vtkImageDataGeometryFilter toSet)
        {
            plane = toSet;
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
        public static vtkTensorGlyph Getellipsoids()
        {
            return ellipsoids;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setellipsoids(vtkTensorGlyph toSet)
        {
            ellipsoids = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataNormals GetellipNormals()
        {
            return ellipNormals;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetellipNormals(vtkPolyDataNormals toSet)
        {
            ellipNormals = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkLogLookupTable Getlut()
        {
            return lut;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setlut(vtkLogLookupTable toSet)
        {
            lut = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetellipMapper()
        {
            return ellipMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetellipMapper(vtkPolyDataMapper toSet)
        {
            ellipMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetellipActor()
        {
            return ellipActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetellipActor(vtkActor toSet)
        {
            ellipActor = toSet;
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
  	if(plane!= null){plane.Dispose();}
  	if(sphere!= null){sphere.Dispose();}
  	if(ellipsoids!= null){ellipsoids.Dispose();}
  	if(ellipNormals!= null){ellipNormals.Dispose();}
  	if(lut!= null){lut.Dispose();}
  	if(ellipMapper!= null){ellipMapper.Dispose();}
  	if(ellipActor!= null){ellipActor.Dispose();}
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

