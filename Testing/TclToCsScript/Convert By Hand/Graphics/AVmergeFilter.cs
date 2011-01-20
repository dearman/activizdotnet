using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\mergeFilter.tcl
// output file is AVmergeFilter.cs
/// <summary>
/// The testing class derived from AVmergeFilter
/// </summary>
public class AVmergeFilterClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVmergeFilter(String [] argv)
  {
  //Prefix Content is: ""
  
  // Create the RenderWindow, Renderer and both Actors[]
  //[]
  ren1 = vtkRenderer.New();
  ren2 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  renWin.AddRenderer((vtkRenderer)ren2);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  // create pipeline[]
  //[]
  pl3d = new vtkPLOT3DReader();
  pl3d.SetXYZFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/combxyz.bin");
  pl3d.SetQFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/combq.bin");
  pl3d.SetScalarFunctionNumber((int)110);
  pl3d.SetVectorFunctionNumber((int)202);
  pl3d.Update();
  probeLine = new vtkLineSource();
  probeLine.SetPoint1((double)1,(double)1,(double)29);
  probeLine.SetPoint2((double)16.5,(double)5,(double)31.7693);
  probeLine.SetResolution((int)500);
  probe = new vtkProbeFilter();
  probe.SetInputConnection((vtkAlgorithmOutput)probeLine.GetOutputPort());
  probe.SetSource((vtkDataObject)pl3d.GetOutput());
  probeTube = new vtkTubeFilter();
  probeTube.SetInput((vtkDataObject)probe.GetPolyDataOutput());
  probeTube.SetNumberOfSides((int)5);
  probeTube.SetRadius((double).05);
  probeMapper = vtkPolyDataMapper.New();
  probeMapper.SetInputConnection((vtkAlgorithmOutput)probeTube.GetOutputPort());
  probeMapper.SetScalarRange((double)((vtkDataSet)pl3d.GetOutput()).GetScalarRange()[0],
      (double)((vtkDataSet)pl3d.GetOutput()).GetScalarRange()[1]);
  probeActor = new vtkActor();
  probeActor.SetMapper((vtkMapper)probeMapper);
  displayLine = new vtkLineSource();
  displayLine.SetPoint1((double)0,(double)0,(double)0);
  displayLine.SetPoint2((double)1,(double)0,(double)0);
  displayLine.SetResolution((int)probeLine.GetResolution());
  displayMerge = new vtkMergeFilter();
  displayMerge.SetGeometry((vtkDataSet)displayLine.GetOutput());
  displayMerge.SetScalars((vtkDataSet)probe.GetPolyDataOutput());
  displayWarp = new vtkWarpScalar();
  displayWarp.SetInput((vtkDataObject)displayMerge.GetPolyDataOutput());
  displayWarp.SetNormal((double)0,(double)1,(double)0);
  displayWarp.SetScaleFactor((double).000001);
  displayMapper = vtkPolyDataMapper.New();
  displayMapper.SetInput((vtkPolyData)displayWarp.GetPolyDataOutput());
  displayMapper.SetScalarRange((double)((vtkDataSet)pl3d.GetOutput()).GetScalarRange()[0],
      (double)((vtkDataSet)pl3d.GetOutput()).GetScalarRange()[1]);
  displayActor = new vtkActor();
  displayActor.SetMapper((vtkMapper)displayMapper);
  outline = new vtkStructuredGridOutlineFilter();
  outline.SetInputConnection((vtkAlgorithmOutput)pl3d.GetOutputPort());
  outlineMapper = vtkPolyDataMapper.New();
  outlineMapper.SetInputConnection((vtkAlgorithmOutput)outline.GetOutputPort());
  outlineActor = new vtkActor();
  outlineActor.SetMapper((vtkMapper)outlineMapper);
  outlineActor.GetProperty().SetColor((double)0,(double)0,(double)0);
  ren1.AddActor((vtkProp)outlineActor);
  ren1.AddActor((vtkProp)probeActor);
  ren1.SetBackground((double)1,(double)1,(double)1);
  ren1.SetViewport((double)0,(double).25,(double)1,(double)1);
  ren2.AddActor((vtkProp)displayActor);
  ren2.SetBackground((double)0,(double)0,(double)0);
  ren2.SetViewport((double)0,(double)0,(double)1,(double).25);
  renWin.SetSize((int)300,(int)300);
  ren1.ResetCamera();
  cam1 = ren1.GetActiveCamera();
  cam1.SetClippingRange((double)3.95297,(double)50);
  cam1.SetFocalPoint((double)8.88908,(double)0.595038,(double)29.3342);
  cam1.SetPosition((double)9.9,(double)-26,(double)41);
  cam1.SetViewUp((double)0.060772,(double)-0.319905,(double)0.945498);
  ren2.ResetCamera();
  cam2 = ren2.GetActiveCamera();
  cam2.ParallelProjectionOn();
  cam2.SetParallelScale((double).15);
  iren.Initialize();
  // render the image[]
  //[]
  // prevent the tk window from showing up then start the event loop[]
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkRenderer ren1;
static vtkRenderer ren2;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkPLOT3DReader pl3d;
static vtkLineSource probeLine;
static vtkProbeFilter probe;
static vtkTubeFilter probeTube;
static vtkPolyDataMapper probeMapper;
static vtkActor probeActor;
static vtkLineSource displayLine;
static vtkMergeFilter displayMerge;
static vtkWarpScalar displayWarp;
static vtkPolyDataMapper displayMapper;
static vtkActor displayActor;
static vtkStructuredGridOutlineFilter outline;
static vtkPolyDataMapper outlineMapper;
static vtkActor outlineActor;
static vtkCamera cam1;
static vtkCamera cam2;


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
        public static vtkRenderer Getren2()
        {
            return ren2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setren2(vtkRenderer toSet)
        {
            ren2 = toSet;
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
        public static vtkLineSource GetprobeLine()
        {
            return probeLine;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetprobeLine(vtkLineSource toSet)
        {
            probeLine = toSet;
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
        public static vtkTubeFilter GetprobeTube()
        {
            return probeTube;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetprobeTube(vtkTubeFilter toSet)
        {
            probeTube = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetprobeMapper()
        {
            return probeMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetprobeMapper(vtkPolyDataMapper toSet)
        {
            probeMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetprobeActor()
        {
            return probeActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetprobeActor(vtkActor toSet)
        {
            probeActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkLineSource GetdisplayLine()
        {
            return displayLine;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetdisplayLine(vtkLineSource toSet)
        {
            displayLine = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkMergeFilter GetdisplayMerge()
        {
            return displayMerge;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetdisplayMerge(vtkMergeFilter toSet)
        {
            displayMerge = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkWarpScalar GetdisplayWarp()
        {
            return displayWarp;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetdisplayWarp(vtkWarpScalar toSet)
        {
            displayWarp = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetdisplayMapper()
        {
            return displayMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetdisplayMapper(vtkPolyDataMapper toSet)
        {
            displayMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetdisplayActor()
        {
            return displayActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetdisplayActor(vtkActor toSet)
        {
            displayActor = toSet;
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
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCamera Getcam2()
        {
            return cam2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcam2(vtkCamera toSet)
        {
            cam2 = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(ren1!= null){ren1.Dispose();}
  	if(ren2!= null){ren2.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(pl3d!= null){pl3d.Dispose();}
  	if(probeLine!= null){probeLine.Dispose();}
  	if(probe!= null){probe.Dispose();}
  	if(probeTube!= null){probeTube.Dispose();}
  	if(probeMapper!= null){probeMapper.Dispose();}
  	if(probeActor!= null){probeActor.Dispose();}
  	if(displayLine!= null){displayLine.Dispose();}
  	if(displayMerge!= null){displayMerge.Dispose();}
  	if(displayWarp!= null){displayWarp.Dispose();}
  	if(displayMapper!= null){displayMapper.Dispose();}
  	if(displayActor!= null){displayActor.Dispose();}
  	if(outline!= null){outline.Dispose();}
  	if(outlineMapper!= null){outlineMapper.Dispose();}
  	if(outlineActor!= null){outlineActor.Dispose();}
  	if(cam1!= null){cam1.Dispose();}
  	if(cam2!= null){cam2.Dispose();}
  }

}
//--- end of script --//

