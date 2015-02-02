using Kitware.VTK;
using System;
// input file is C:\VTK\Parallel\Testing\Tcl\TestExtractVOI.tcl
// output file is AVTestExtractVOI.cs
/// <summary>
/// The testing class derived from AVTestExtractVOI
/// </summary>
public class AVTestExtractVOIClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVTestExtractVOI(String [] argv)
  {
  //Prefix Content is: ""
  
  // to mark the origin[]
  sphere = new vtkSphereSource();
  sphere.SetRadius((double)2.0);
  sphereMapper = vtkPolyDataMapper.New();
  sphereMapper.SetInputConnection((vtkAlgorithmOutput)sphere.GetOutputPort());
  sphereMapper.ImmediateModeRenderingOn();
  sphereActor = new vtkActor();
  sphereActor.SetMapper((vtkMapper)sphereMapper);
  rt = new vtkRTAnalyticSource();
  rt.SetWholeExtent((int)-50,(int)50,(int)-50,(int)50,(int)0,(int)0);
  voi = new vtkExtractVOI();
  voi.SetInputConnection((vtkAlgorithmOutput)rt.GetOutputPort());
  voi.SetVOI((int)-11,(int)39,(int)5,(int)45,(int)0,(int)0);
  voi.SetSampleRate((int)5,(int)5,(int)1);
  // Get rid ambiguous triagulation issues.[]
  surf = new vtkDataSetSurfaceFilter();
  surf.SetInputConnection((vtkAlgorithmOutput)voi.GetOutputPort());
  tris = new vtkTriangleFilter();
  tris.SetInputConnection((vtkAlgorithmOutput)surf.GetOutputPort());
  mapper = vtkPolyDataMapper.New();
  mapper.SetInputConnection((vtkAlgorithmOutput)tris.GetOutputPort());
  mapper.ImmediateModeRenderingOn();
  mapper.SetScalarRange((double)130,(double)280);
  actor = new vtkActor();
  actor.SetMapper((vtkMapper)mapper);
  ren = vtkRenderer.New();
  ren.AddActor((vtkProp)actor);
  ren.AddActor((vtkProp)sphereActor);
  ren.ResetCamera();
  camera = ren.GetActiveCamera();
  //$camera SetPosition 68.1939 -23.4323 12.6465[]
  //$camera SetViewUp 0.46563 0.882375 0.0678508  []
  //$camera SetFocalPoint 3.65707 11.4552 1.83509 []
  //$camera SetClippingRange 59.2626 101.825 []
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  iren.Initialize();
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkSphereSource sphere;
static vtkPolyDataMapper sphereMapper;
static vtkActor sphereActor;
static vtkRTAnalyticSource rt;
static vtkExtractVOI voi;
static vtkDataSetSurfaceFilter surf;
static vtkTriangleFilter tris;
static vtkPolyDataMapper mapper;
static vtkActor actor;
static vtkRenderer ren;
static vtkCamera camera;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;


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
        public static vtkPolyDataMapper GetsphereMapper()
        {
            return sphereMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetsphereMapper(vtkPolyDataMapper toSet)
        {
            sphereMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetsphereActor()
        {
            return sphereActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetsphereActor(vtkActor toSet)
        {
            sphereActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRTAnalyticSource Getrt()
        {
            return rt;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setrt(vtkRTAnalyticSource toSet)
        {
            rt = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkExtractVOI Getvoi()
        {
            return voi;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setvoi(vtkExtractVOI toSet)
        {
            voi = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetSurfaceFilter Getsurf()
        {
            return surf;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setsurf(vtkDataSetSurfaceFilter toSet)
        {
            surf = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTriangleFilter Gettris()
        {
            return tris;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settris(vtkTriangleFilter toSet)
        {
            tris = toSet;
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
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRenderer Getren()
        {
            return ren;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setren(vtkRenderer toSet)
        {
            ren = toSet;
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
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(sphere!= null){sphere.Dispose();}
  	if(sphereMapper!= null){sphereMapper.Dispose();}
  	if(sphereActor!= null){sphereActor.Dispose();}
  	if(rt!= null){rt.Dispose();}
  	if(voi!= null){voi.Dispose();}
  	if(surf!= null){surf.Dispose();}
  	if(tris!= null){tris.Dispose();}
  	if(mapper!= null){mapper.Dispose();}
  	if(actor!= null){actor.Dispose();}
  	if(ren!= null){ren.Dispose();}
  	if(camera!= null){camera.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  }

}
//--- end of script --//

