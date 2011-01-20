using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\EnSightOfficeASCII.tcl
// output file is AVEnSightOfficeASCII.cs
/// <summary>
/// The testing class derived from AVEnSightOfficeASCII
/// </summary>
public class AVEnSightOfficeASCIIClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVEnSightOfficeASCII(String [] argv)
  {
  //Prefix Content is: ""
  
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  // read data[]
  //[]
  reader = new vtkGenericEnSightReader();
  // Make sure all algorithms use the composite data pipeline[]
  cdp = new vtkCompositeDataPipeline();
  vtkGenericEnSightReader.SetDefaultExecutivePrototype((vtkExecutive)cdp);
  reader.SetCaseFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/EnSight/office_ascii.case");
  reader.Update();
  // to add coverage for vtkOnePieceExtentTranslator[]
  translator = new vtkOnePieceExtentTranslator();
  reader.GetOutput().SetExtentTranslator((vtkExtentTranslator)translator);
  outline = new vtkStructuredGridOutlineFilter();
  //    outline SetInputConnection [reader GetOutputPort][]
  outline.SetInput((vtkDataObject)reader.GetOutput().GetBlock((uint)0));
  mapOutline = vtkPolyDataMapper.New();
  mapOutline.SetInputConnection((vtkAlgorithmOutput)outline.GetOutputPort());
  outlineActor = new vtkActor();
  outlineActor.SetMapper((vtkMapper)mapOutline);
  outlineActor.GetProperty().SetColor((double)0,(double)0,(double)0);
  // Create source for streamtubes[]
  streamer = new vtkStreamPoints();
  //    streamer SetInputConnection [reader GetOutputPort][]
  streamer.SetInput((vtkDataObject)reader.GetOutput().GetBlock((uint)0));
  streamer.SetStartPosition((double)0.1,(double)2.1,(double)0.5);
  streamer.SetMaximumPropagationTime((double)500);
  streamer.SetTimeIncrement((double)0.5);
  streamer.SetIntegrationDirectionToForward();
  cone = new vtkConeSource();
  cone.SetResolution((int)8);
  cones = new vtkGlyph3D();
  cones.SetInputConnection((vtkAlgorithmOutput)streamer.GetOutputPort());
  cones.SetSource((vtkPolyData)cone.GetOutput());
  cones.SetScaleFactor((double)0.9);
  cones.SetScaleModeToScaleByVector();
  mapCones = vtkPolyDataMapper.New();
  mapCones.SetInputConnection((vtkAlgorithmOutput)cones.GetOutputPort());
  //    eval mapCones SetScalarRange [[reader GetOutput] GetScalarRange][]
  mapCones.SetScalarRange((double)((vtkDataSet)reader.GetOutput().GetBlock((uint)0)).GetScalarRange()[0],(double)((vtkDataSet)reader.GetOutput().GetBlock((uint)0)).GetScalarRange()[1]);
  conesActor = new vtkActor();
  conesActor.SetMapper((vtkMapper)mapCones);
  ren1.AddActor((vtkProp)outlineActor);
  ren1.AddActor((vtkProp)conesActor);
  ren1.SetBackground((double)0.4,(double)0.4,(double)0.5);
  renWin.SetSize((int)300,(int)300);
  iren.Initialize();
  // interact with data[]
  vtkGenericEnSightReader.SetDefaultExecutivePrototype(null);
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkGenericEnSightReader reader;
static vtkCompositeDataPipeline cdp;
static vtkOnePieceExtentTranslator translator;
static vtkStructuredGridOutlineFilter outline;
static vtkPolyDataMapper mapOutline;
static vtkActor outlineActor;
static vtkStreamPoints streamer;
static vtkConeSource cone;
static vtkGlyph3D cones;
static vtkPolyDataMapper mapCones;
static vtkActor conesActor;


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
        public static vtkGenericEnSightReader Getreader()
        {
            return reader;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setreader(vtkGenericEnSightReader toSet)
        {
            reader = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCompositeDataPipeline Getcdp()
        {
            return cdp;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcdp(vtkCompositeDataPipeline toSet)
        {
            cdp = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkOnePieceExtentTranslator Gettranslator()
        {
            return translator;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settranslator(vtkOnePieceExtentTranslator toSet)
        {
            translator = toSet;
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
        public static vtkStreamPoints Getstreamer()
        {
            return streamer;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setstreamer(vtkStreamPoints toSet)
        {
            streamer = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkConeSource Getcone()
        {
            return cone;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcone(vtkConeSource toSet)
        {
            cone = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkGlyph3D Getcones()
        {
            return cones;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcones(vtkGlyph3D toSet)
        {
            cones = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapCones()
        {
            return mapCones;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapCones(vtkPolyDataMapper toSet)
        {
            mapCones = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetconesActor()
        {
            return conesActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetconesActor(vtkActor toSet)
        {
            conesActor = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(reader!= null){reader.Dispose();}
  	if(cdp!= null){cdp.Dispose();}
  	if(translator!= null){translator.Dispose();}
  	if(outline!= null){outline.Dispose();}
  	if(mapOutline!= null){mapOutline.Dispose();}
  	if(outlineActor!= null){outlineActor.Dispose();}
  	if(streamer!= null){streamer.Dispose();}
  	if(cone!= null){cone.Dispose();}
  	if(cones!= null){cones.Dispose();}
  	if(mapCones!= null){mapCones.Dispose();}
  	if(conesActor!= null){conesActor.Dispose();}
  }

}
//--- end of script --//

