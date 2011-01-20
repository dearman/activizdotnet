using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\probeComb.tcl
// output file is AVprobeComb.cs
/// <summary>
/// The testing class derived from AVprobeComb
/// </summary>
public class AVprobeCombClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVprobeComb(String [] argv)
  {
  //Prefix Content is: ""
  
  // create planes[]
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
  plane = new vtkPlaneSource();
  plane.SetResolution((int)50,(int)50);
  transP1 = new vtkTransform();
  transP1.Translate((double)3.7,(double)0.0,(double)28.37);
  transP1.Scale((double)5,(double)5,(double)5);
  transP1.RotateY((double)90);
  tpd1 = new vtkTransformPolyDataFilter();
  tpd1.SetInputConnection((vtkAlgorithmOutput)plane.GetOutputPort());
  tpd1.SetTransform((vtkAbstractTransform)transP1);
  outTpd1 = new vtkOutlineFilter();
  outTpd1.SetInputConnection((vtkAlgorithmOutput)tpd1.GetOutputPort());
  mapTpd1 = vtkPolyDataMapper.New();
  mapTpd1.SetInputConnection((vtkAlgorithmOutput)outTpd1.GetOutputPort());
  tpd1Actor = new vtkActor();
  tpd1Actor.SetMapper((vtkMapper)mapTpd1);
  tpd1Actor.GetProperty().SetColor((double)0,(double)0,(double)0);
  transP2 = new vtkTransform();
  transP2.Translate((double)9.2,(double)0.0,(double)31.20);
  transP2.Scale((double)5,(double)5,(double)5);
  transP2.RotateY((double)90);
  tpd2 = new vtkTransformPolyDataFilter();
  tpd2.SetInputConnection((vtkAlgorithmOutput)plane.GetOutputPort());
  tpd2.SetTransform((vtkAbstractTransform)transP2);
  outTpd2 = new vtkOutlineFilter();
  outTpd2.SetInputConnection((vtkAlgorithmOutput)tpd2.GetOutputPort());
  mapTpd2 = vtkPolyDataMapper.New();
  mapTpd2.SetInputConnection((vtkAlgorithmOutput)outTpd2.GetOutputPort());
  tpd2Actor = new vtkActor();
  tpd2Actor.SetMapper((vtkMapper)mapTpd2);
  tpd2Actor.GetProperty().SetColor((double)0,(double)0,(double)0);
  transP3 = new vtkTransform();
  transP3.Translate((double)13.27,(double)0.0,(double)33.30);
  transP3.Scale((double)5,(double)5,(double)5);
  transP3.RotateY((double)90);
  tpd3 = new vtkTransformPolyDataFilter();
  tpd3.SetInputConnection((vtkAlgorithmOutput)plane.GetOutputPort());
  tpd3.SetTransform((vtkAbstractTransform)transP3);
  outTpd3 = new vtkOutlineFilter();
  outTpd3.SetInputConnection((vtkAlgorithmOutput)tpd3.GetOutputPort());
  mapTpd3 = vtkPolyDataMapper.New();
  mapTpd3.SetInputConnection((vtkAlgorithmOutput)outTpd3.GetOutputPort());
  tpd3Actor = new vtkActor();
  tpd3Actor.SetMapper((vtkMapper)mapTpd3);
  tpd3Actor.GetProperty().SetColor((double)0,(double)0,(double)0);
  appendF = new vtkAppendPolyData();
  appendF.AddInput((vtkPolyData)tpd1.GetOutput());
  appendF.AddInput((vtkPolyData)tpd2.GetOutput());
  appendF.AddInput((vtkPolyData)tpd3.GetOutput());
  probe = new vtkProbeFilter();
  probe.SetInputConnection((vtkAlgorithmOutput)appendF.GetOutputPort());
  probe.SetSource((vtkDataObject)pl3d.GetOutput());
  contour = new vtkContourFilter();
  contour.SetInputConnection((vtkAlgorithmOutput)probe.GetOutputPort());
  contour.GenerateValues((int)50,(double)((vtkDataSet)pl3d.GetOutput()).GetScalarRange()[0],
      (double)((vtkDataSet)pl3d.GetOutput()).GetScalarRange()[1]);
  contourMapper = vtkPolyDataMapper.New();
  contourMapper.SetInputConnection((vtkAlgorithmOutput)contour.GetOutputPort());
  contourMapper.SetScalarRange((double)((vtkDataSet)pl3d.GetOutput()).GetScalarRange()[0],
      (double)((vtkDataSet)pl3d.GetOutput()).GetScalarRange()[1]);
  planeActor = new vtkActor();
  planeActor.SetMapper((vtkMapper)contourMapper);
  outline = new vtkStructuredGridOutlineFilter();
  outline.SetInputConnection((vtkAlgorithmOutput)pl3d.GetOutputPort());
  outlineMapper = vtkPolyDataMapper.New();
  outlineMapper.SetInputConnection((vtkAlgorithmOutput)outline.GetOutputPort());
  outlineActor = new vtkActor();
  outlineActor.SetMapper((vtkMapper)outlineMapper);
  outlineActor.GetProperty().SetColor((double)0,(double)0,(double)0);
  ren1.AddActor((vtkProp)outlineActor);
  ren1.AddActor((vtkProp)planeActor);
  ren1.AddActor((vtkProp)tpd1Actor);
  ren1.AddActor((vtkProp)tpd2Actor);
  ren1.AddActor((vtkProp)tpd3Actor);
  ren1.SetBackground((double)1,(double)1,(double)1);
  renWin.SetSize((int)400,(int)400);
  cam1 = ren1.GetActiveCamera();
  cam1.SetClippingRange((double)3.95297,(double)50);
  cam1.SetFocalPoint((double)8.88908,(double)0.595038,(double)29.3342);
  cam1.SetPosition((double)-12.3332,(double)31.7479,(double)41.2387);
  cam1.SetViewUp((double)0.060772,(double)-0.319905,(double)0.945498);
  iren.Initialize();
  // prevent the tk window from showing up then start the event loop[]
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkPLOT3DReader pl3d;
static vtkPlaneSource plane;
static vtkTransform transP1;
static vtkTransformPolyDataFilter tpd1;
static vtkOutlineFilter outTpd1;
static vtkPolyDataMapper mapTpd1;
static vtkActor tpd1Actor;
static vtkTransform transP2;
static vtkTransformPolyDataFilter tpd2;
static vtkOutlineFilter outTpd2;
static vtkPolyDataMapper mapTpd2;
static vtkActor tpd2Actor;
static vtkTransform transP3;
static vtkTransformPolyDataFilter tpd3;
static vtkOutlineFilter outTpd3;
static vtkPolyDataMapper mapTpd3;
static vtkActor tpd3Actor;
static vtkAppendPolyData appendF;
static vtkProbeFilter probe;
static vtkContourFilter contour;
static vtkPolyDataMapper contourMapper;
static vtkActor planeActor;
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
        public static vtkPlaneSource Getplane()
        {
            return plane;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setplane(vtkPlaneSource toSet)
        {
            plane = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTransform GettransP1()
        {
            return transP1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SettransP1(vtkTransform toSet)
        {
            transP1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTransformPolyDataFilter Gettpd1()
        {
            return tpd1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settpd1(vtkTransformPolyDataFilter toSet)
        {
            tpd1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkOutlineFilter GetoutTpd1()
        {
            return outTpd1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetoutTpd1(vtkOutlineFilter toSet)
        {
            outTpd1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapTpd1()
        {
            return mapTpd1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapTpd1(vtkPolyDataMapper toSet)
        {
            mapTpd1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Gettpd1Actor()
        {
            return tpd1Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settpd1Actor(vtkActor toSet)
        {
            tpd1Actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTransform GettransP2()
        {
            return transP2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SettransP2(vtkTransform toSet)
        {
            transP2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTransformPolyDataFilter Gettpd2()
        {
            return tpd2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settpd2(vtkTransformPolyDataFilter toSet)
        {
            tpd2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkOutlineFilter GetoutTpd2()
        {
            return outTpd2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetoutTpd2(vtkOutlineFilter toSet)
        {
            outTpd2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapTpd2()
        {
            return mapTpd2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapTpd2(vtkPolyDataMapper toSet)
        {
            mapTpd2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Gettpd2Actor()
        {
            return tpd2Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settpd2Actor(vtkActor toSet)
        {
            tpd2Actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTransform GettransP3()
        {
            return transP3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SettransP3(vtkTransform toSet)
        {
            transP3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTransformPolyDataFilter Gettpd3()
        {
            return tpd3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settpd3(vtkTransformPolyDataFilter toSet)
        {
            tpd3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkOutlineFilter GetoutTpd3()
        {
            return outTpd3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetoutTpd3(vtkOutlineFilter toSet)
        {
            outTpd3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapTpd3()
        {
            return mapTpd3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapTpd3(vtkPolyDataMapper toSet)
        {
            mapTpd3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Gettpd3Actor()
        {
            return tpd3Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settpd3Actor(vtkActor toSet)
        {
            tpd3Actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkAppendPolyData GetappendF()
        {
            return appendF;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetappendF(vtkAppendPolyData toSet)
        {
            appendF = toSet;
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
        public static vtkPolyDataMapper GetcontourMapper()
        {
            return contourMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetcontourMapper(vtkPolyDataMapper toSet)
        {
            contourMapper = toSet;
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
  	if(transP1!= null){transP1.Dispose();}
  	if(tpd1!= null){tpd1.Dispose();}
  	if(outTpd1!= null){outTpd1.Dispose();}
  	if(mapTpd1!= null){mapTpd1.Dispose();}
  	if(tpd1Actor!= null){tpd1Actor.Dispose();}
  	if(transP2!= null){transP2.Dispose();}
  	if(tpd2!= null){tpd2.Dispose();}
  	if(outTpd2!= null){outTpd2.Dispose();}
  	if(mapTpd2!= null){mapTpd2.Dispose();}
  	if(tpd2Actor!= null){tpd2Actor.Dispose();}
  	if(transP3!= null){transP3.Dispose();}
  	if(tpd3!= null){tpd3.Dispose();}
  	if(outTpd3!= null){outTpd3.Dispose();}
  	if(mapTpd3!= null){mapTpd3.Dispose();}
  	if(tpd3Actor!= null){tpd3Actor.Dispose();}
  	if(appendF!= null){appendF.Dispose();}
  	if(probe!= null){probe.Dispose();}
  	if(contour!= null){contour.Dispose();}
  	if(contourMapper!= null){contourMapper.Dispose();}
  	if(planeActor!= null){planeActor.Dispose();}
  	if(outline!= null){outline.Dispose();}
  	if(outlineMapper!= null){outlineMapper.Dispose();}
  	if(outlineActor!= null){outlineActor.Dispose();}
  	if(cam1!= null){cam1.Dispose();}
  }

}
//--- end of script --//

