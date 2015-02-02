using System.IO;
using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\Hyper.tcl
// output file is AVHyper.cs
/// <summary>
/// The testing class derived from AVHyper
/// </summary>
public class AVHyperClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVHyper(String [] argv)
  {
  //Prefix Content is: ""
  
  // Create the RenderWindow, Renderer and interactive renderer[]
  //[]
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  VTK_INTEGRATE_BOTH_DIRECTIONS = 2;
  //[]
  // generate tensors[]
  ptLoad = new vtkPointLoad();
  ptLoad.SetLoadValue((double)100.0);
  ptLoad.SetSampleDimensions((int)20,(int)20,(int)20);
  ptLoad.ComputeEffectiveStressOn();
  ptLoad.SetModelBounds((double)-10,(double)10,(double)-10,(double)10,(double)-10,(double)10);
  //[]
  // If the current directory is writable, then test the witers[]
  //[]
  try
  {
     channel = new StreamWriter("test.tmp");
      tryCatchError = "NOERROR";
  }
  catch(Exception)
  {tryCatchError = "ERROR";}
  
if(tryCatchError.Equals("NOERROR"))
  {
      channel.Close();
      File.Delete("test.tmp");
      wSP = new vtkDataSetWriter();
      wSP.SetInputConnection((vtkAlgorithmOutput)ptLoad.GetOutputPort());
      wSP.SetFileName((string)"wSP.vtk");
      wSP.SetTensorsName((string)"pointload");
      wSP.SetScalarsName((string)"effective_stress");
      wSP.Write();
      rSP = new vtkDataSetReader();
      rSP.SetFileName((string)"wSP.vtk");
      rSP.SetTensorsName((string)"pointload");
      rSP.SetScalarsName((string)"effective_stress");
      rSP.Update();
      input = rSP.GetOutput();
      File.Delete("wSP.vtk");
    }
  else
    {
      input = ptLoad.GetOutput();
    }
  
  // Generate hyperstreamlines[]
  s1 = new vtkHyperStreamline();
  s1.SetInputData((vtkDataObject)input);
  s1.SetStartPosition((double)9,(double)9,(double)-9);
  s1.IntegrateMinorEigenvector();
  s1.SetMaximumPropagationDistance((double)18.0);
  s1.SetIntegrationStepLength((double)0.1);
  s1.SetStepLength((double)0.01);
  s1.SetRadius((double)0.25);
  s1.SetNumberOfSides((int)18);
  s1.SetIntegrationDirection((int)VTK_INTEGRATE_BOTH_DIRECTIONS);
  s1.Update();
  // Map hyperstreamlines[]
  lut = new vtkLogLookupTable();
  lut.SetHueRange((double).6667,(double)0.0);
  s1Mapper = vtkPolyDataMapper.New();
  s1Mapper.SetInputConnection((vtkAlgorithmOutput)s1.GetOutputPort());
  s1Mapper.SetLookupTable((vtkScalarsToColors)lut);
  ptLoad.Update();
  //force update for scalar range[]
  s1Mapper.SetScalarRange((double)((vtkDataSet)ptLoad.GetOutput()).GetScalarRange()[0],(double)((vtkDataSet)ptLoad.GetOutput()).GetScalarRange()[1]);
  s1Actor = new vtkActor();
  s1Actor.SetMapper((vtkMapper)s1Mapper);
  s2 = new vtkHyperStreamline();
  s2.SetInputData((vtkDataObject)input);
  s2.SetStartPosition((double)-9,(double)-9,(double)-9);
  s2.IntegrateMinorEigenvector();
  s2.SetMaximumPropagationDistance((double)18.0);
  s2.SetIntegrationStepLength((double)0.1);
  s2.SetStepLength((double)0.01);
  s2.SetRadius((double)0.25);
  s2.SetNumberOfSides((int)18);
  s2.SetIntegrationDirection((int)VTK_INTEGRATE_BOTH_DIRECTIONS);
  s2.Update();
  s2Mapper = vtkPolyDataMapper.New();
  s2Mapper.SetInputConnection((vtkAlgorithmOutput)s2.GetOutputPort());
  s2Mapper.SetLookupTable((vtkScalarsToColors)lut);
  s2Mapper.SetScalarRange((double)((vtkDataSet)input).GetScalarRange()[0],(double)((vtkDataSet)input).GetScalarRange()[1]);
  s2Actor = new vtkActor();
  s2Actor.SetMapper((vtkMapper)s2Mapper);
  s3 = new vtkHyperStreamline();
  s3.SetInputData((vtkDataObject)input);
  s3.SetStartPosition((double)9,(double)-9,(double)-9);
  s3.IntegrateMinorEigenvector();
  s3.SetMaximumPropagationDistance((double)18.0);
  s3.SetIntegrationStepLength((double)0.1);
  s3.SetStepLength((double)0.01);
  s3.SetRadius((double)0.25);
  s3.SetNumberOfSides((int)18);
  s3.SetIntegrationDirection((int)VTK_INTEGRATE_BOTH_DIRECTIONS);
  s3.Update();
  s3Mapper = vtkPolyDataMapper.New();
  s3Mapper.SetInputConnection((vtkAlgorithmOutput)s3.GetOutputPort());
  s3Mapper.SetLookupTable((vtkScalarsToColors)lut);
  s3Mapper.SetScalarRange((double)((vtkDataSet)input).GetScalarRange()[0],
      (double)((vtkDataSet)input).GetScalarRange()[1]);
  s3Actor = new vtkActor();
  s3Actor.SetMapper((vtkMapper)s3Mapper);
  s4 = new vtkHyperStreamline();
  s4.SetInputData((vtkDataObject)input);
  s4.SetStartPosition((double)-9,(double)9,(double)-9);
  s4.IntegrateMinorEigenvector();
  s4.SetMaximumPropagationDistance((double)18.0);
  s4.SetIntegrationStepLength((double)0.1);
  s4.SetStepLength((double)0.01);
  s4.SetRadius((double)0.25);
  s4.SetNumberOfSides((int)18);
  s4.SetIntegrationDirection((int)VTK_INTEGRATE_BOTH_DIRECTIONS);
  s4.Update();
  s4Mapper = vtkPolyDataMapper.New();
  s4Mapper.SetInputConnection((vtkAlgorithmOutput)s4.GetOutputPort());
  s4Mapper.SetLookupTable((vtkScalarsToColors)lut);
  s4Mapper.SetScalarRange((double)((vtkDataSet)input).GetScalarRange()[0],(double)((vtkDataSet)input).GetScalarRange()[1]);
  s4Actor = new vtkActor();
  s4Actor.SetMapper((vtkMapper)s4Mapper);
  // plane for context[]
  //[]
  g = new vtkImageDataGeometryFilter();
  g.SetInputData((vtkDataObject)input);
  g.SetExtent((int)0,(int)100,(int)0,(int)100,(int)0,(int)0);
  g.Update();
  //for scalar range[]
  gm = vtkPolyDataMapper.New();
  gm.SetInputConnection((vtkAlgorithmOutput)g.GetOutputPort());
  gm.SetScalarRange((double)((vtkDataSet)g.GetOutput()).GetScalarRange()[0],(double)((vtkDataSet)g.GetOutput()).GetScalarRange()[1]);
  ga = new vtkActor();
  ga.SetMapper((vtkMapper)gm);
  // Create outline around data[]
  //[]
  outline = new vtkOutlineFilter();
  outline.SetInputData((vtkDataObject)input);
  outlineMapper = vtkPolyDataMapper.New();
  outlineMapper.SetInputConnection((vtkAlgorithmOutput)outline.GetOutputPort());
  outlineActor = new vtkActor();
  outlineActor.SetMapper((vtkMapper)outlineMapper);
  outlineActor.GetProperty().SetColor((double)0,(double)0,(double)0);
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
  ren1.AddActor((vtkProp)s2Actor);
  ren1.AddActor((vtkProp)s3Actor);
  ren1.AddActor((vtkProp)s4Actor);
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
static int VTK_INTEGRATE_BOTH_DIRECTIONS;
static vtkPointLoad ptLoad;
static string tryCatchError;
static StreamWriter channel;
static vtkDataSetWriter wSP;
static vtkDataSetReader rSP;
static vtkDataSet input;
static vtkHyperStreamline s1;
static vtkLogLookupTable lut;
static vtkPolyDataMapper s1Mapper;
static vtkActor s1Actor;
static vtkHyperStreamline s2;
static vtkPolyDataMapper s2Mapper;
static vtkActor s2Actor;
static vtkHyperStreamline s3;
static vtkPolyDataMapper s3Mapper;
static vtkActor s3Actor;
static vtkHyperStreamline s4;
static vtkPolyDataMapper s4Mapper;
static vtkActor s4Actor;
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
        public static int GetVTK_INTEGRATE_BOTH_DIRECTIONS()
        {
            return VTK_INTEGRATE_BOTH_DIRECTIONS;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetVTK_INTEGRATE_BOTH_DIRECTIONS(int toSet)
        {
            VTK_INTEGRATE_BOTH_DIRECTIONS = toSet;
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
        public static string GettryCatchError()
        {
            return tryCatchError;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SettryCatchError(string toSet)
        {
            tryCatchError = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static StreamWriter Getchannel()
        {
            return channel;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setchannel(StreamWriter toSet)
        {
            channel = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetWriter GetwSP()
        {
            return wSP;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetwSP(vtkDataSetWriter toSet)
        {
            wSP = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetReader GetrSP()
        {
            return rSP;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetrSP(vtkDataSetReader toSet)
        {
            rSP = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSet Getinput()
        {
            return input;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setinput(vtkDataSet toSet)
        {
            input = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkHyperStreamline Gets1()
        {
            return s1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sets1(vtkHyperStreamline toSet)
        {
            s1 = toSet;
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
        public static vtkHyperStreamline Gets2()
        {
            return s2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sets2(vtkHyperStreamline toSet)
        {
            s2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Gets2Mapper()
        {
            return s2Mapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sets2Mapper(vtkPolyDataMapper toSet)
        {
            s2Mapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Gets2Actor()
        {
            return s2Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sets2Actor(vtkActor toSet)
        {
            s2Actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkHyperStreamline Gets3()
        {
            return s3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sets3(vtkHyperStreamline toSet)
        {
            s3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Gets3Mapper()
        {
            return s3Mapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sets3Mapper(vtkPolyDataMapper toSet)
        {
            s3Mapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Gets3Actor()
        {
            return s3Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sets3Actor(vtkActor toSet)
        {
            s3Actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkHyperStreamline Gets4()
        {
            return s4;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sets4(vtkHyperStreamline toSet)
        {
            s4 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Gets4Mapper()
        {
            return s4Mapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sets4Mapper(vtkPolyDataMapper toSet)
        {
            s4Mapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Gets4Actor()
        {
            return s4Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sets4Actor(vtkActor toSet)
        {
            s4Actor = toSet;
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
  	if(wSP!= null){wSP.Dispose();}
  	if(rSP!= null){rSP.Dispose();}
  	if(input!= null){input.Dispose();}
  	if(s1!= null){s1.Dispose();}
  	if(lut!= null){lut.Dispose();}
  	if(s1Mapper!= null){s1Mapper.Dispose();}
  	if(s1Actor!= null){s1Actor.Dispose();}
  	if(s2!= null){s2.Dispose();}
  	if(s2Mapper!= null){s2Mapper.Dispose();}
  	if(s2Actor!= null){s2Actor.Dispose();}
  	if(s3!= null){s3.Dispose();}
  	if(s3Mapper!= null){s3Mapper.Dispose();}
  	if(s3Actor!= null){s3Actor.Dispose();}
  	if(s4!= null){s4.Dispose();}
  	if(s4Mapper!= null){s4Mapper.Dispose();}
  	if(s4Actor!= null){s4Actor.Dispose();}
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

