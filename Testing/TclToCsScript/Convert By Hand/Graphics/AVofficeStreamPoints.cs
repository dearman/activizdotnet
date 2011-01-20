using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\officeStreamPoints.tcl
// output file is AVofficeStreamPoints.cs
/// <summary>
/// The testing class derived from AVofficeStreamPoints
/// </summary>
public class AVofficeStreamPointsClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVofficeStreamPoints(String [] argv)
  {
  //Prefix Content is: ""
  
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  // read data[]
  //[]
  reader = new vtkStructuredGridReader();
  reader.SetFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/office.binary.vtk");
  reader.Update();
  //force a read to occur[]
  // to add coverage for vtkOnePieceExtentTranslator[]
  translator = new vtkOnePieceExtentTranslator();
  reader.GetOutput().SetExtentTranslator((vtkExtentTranslator)translator);
  length = reader.GetOutput().GetLength();
  maxVelocity = reader.GetOutput().GetPointData().GetVectors().GetMaxNorm();
  maxTime = 35.0*length/maxVelocity;
  table1 = new vtkStructuredGridGeometryFilter();
  table1.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  table1.SetExtent((int)11,(int)15,(int)7,(int)9,(int)8,(int)8);
  mapTable1 = vtkPolyDataMapper.New();
  mapTable1.SetInputConnection((vtkAlgorithmOutput)table1.GetOutputPort());
  mapTable1.ScalarVisibilityOff();
  table1Actor = new vtkActor();
  table1Actor.SetMapper((vtkMapper)mapTable1);
  table1Actor.GetProperty().SetColor((double).59,(double).427,(double).392);
  table2 = new vtkStructuredGridGeometryFilter();
  table2.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  table2.SetExtent((int)11,(int)15,(int)10,(int)12,(int)8,(int)8);
  mapTable2 = vtkPolyDataMapper.New();
  mapTable2.SetInputConnection((vtkAlgorithmOutput)table2.GetOutputPort());
  mapTable2.ScalarVisibilityOff();
  table2Actor = new vtkActor();
  table2Actor.SetMapper((vtkMapper)mapTable2);
  table2Actor.GetProperty().SetColor((double).59,(double).427,(double).392);
  FilingCabinet1 = new vtkStructuredGridGeometryFilter();
  FilingCabinet1.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  FilingCabinet1.SetExtent((int)15,(int)15,(int)7,(int)9,(int)0,(int)8);
  mapFilingCabinet1 = vtkPolyDataMapper.New();
  mapFilingCabinet1.SetInputConnection((vtkAlgorithmOutput)FilingCabinet1.GetOutputPort());
  mapFilingCabinet1.ScalarVisibilityOff();
  FilingCabinet1Actor = new vtkActor();
  FilingCabinet1Actor.SetMapper((vtkMapper)mapFilingCabinet1);
  FilingCabinet1Actor.GetProperty().SetColor((double).8,(double).8,(double).6);
  FilingCabinet2 = new vtkStructuredGridGeometryFilter();
  FilingCabinet2.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  FilingCabinet2.SetExtent((int)15,(int)15,(int)10,(int)12,(int)0,(int)8);
  mapFilingCabinet2 = vtkPolyDataMapper.New();
  mapFilingCabinet2.SetInputConnection((vtkAlgorithmOutput)FilingCabinet2.GetOutputPort());
  mapFilingCabinet2.ScalarVisibilityOff();
  FilingCabinet2Actor = new vtkActor();
  FilingCabinet2Actor.SetMapper((vtkMapper)mapFilingCabinet2);
  FilingCabinet2Actor.GetProperty().SetColor((double).8,(double).8,(double).6);
  bookshelf1Top = new vtkStructuredGridGeometryFilter();
  bookshelf1Top.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  bookshelf1Top.SetExtent((int)13,(int)13,(int)0,(int)4,(int)0,(int)11);
  mapBookshelf1Top = vtkPolyDataMapper.New();
  mapBookshelf1Top.SetInputConnection((vtkAlgorithmOutput)bookshelf1Top.GetOutputPort());
  mapBookshelf1Top.ScalarVisibilityOff();
  bookshelf1TopActor = new vtkActor();
  bookshelf1TopActor.SetMapper((vtkMapper)mapBookshelf1Top);
  bookshelf1TopActor.GetProperty().SetColor((double).8,(double).8,(double).6);
  bookshelf1Bottom = new vtkStructuredGridGeometryFilter();
  bookshelf1Bottom.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  bookshelf1Bottom.SetExtent((int)20,(int)20,(int)0,(int)4,(int)0,(int)11);
  mapBookshelf1Bottom = vtkPolyDataMapper.New();
  mapBookshelf1Bottom.SetInputConnection((vtkAlgorithmOutput)bookshelf1Bottom.GetOutputPort());
  mapBookshelf1Bottom.ScalarVisibilityOff();
  bookshelf1BottomActor = new vtkActor();
  bookshelf1BottomActor.SetMapper((vtkMapper)mapBookshelf1Bottom);
  bookshelf1BottomActor.GetProperty().SetColor((double).8,(double).8,(double).6);
  bookshelf1Front = new vtkStructuredGridGeometryFilter();
  bookshelf1Front.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  bookshelf1Front.SetExtent((int)13,(int)20,(int)0,(int)0,(int)0,(int)11);
  mapBookshelf1Front = vtkPolyDataMapper.New();
  mapBookshelf1Front.SetInputConnection((vtkAlgorithmOutput)bookshelf1Front.GetOutputPort());
  mapBookshelf1Front.ScalarVisibilityOff();
  bookshelf1FrontActor = new vtkActor();
  bookshelf1FrontActor.SetMapper((vtkMapper)mapBookshelf1Front);
  bookshelf1FrontActor.GetProperty().SetColor((double).8,(double).8,(double).6);
  bookshelf1Back = new vtkStructuredGridGeometryFilter();
  bookshelf1Back.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  bookshelf1Back.SetExtent((int)13,(int)20,(int)4,(int)4,(int)0,(int)11);
  mapBookshelf1Back = vtkPolyDataMapper.New();
  mapBookshelf1Back.SetInputConnection((vtkAlgorithmOutput)bookshelf1Back.GetOutputPort());
  mapBookshelf1Back.ScalarVisibilityOff();
  bookshelf1BackActor = new vtkActor();
  bookshelf1BackActor.SetMapper((vtkMapper)mapBookshelf1Back);
  bookshelf1BackActor.GetProperty().SetColor((double).8,(double).8,(double).6);
  bookshelf1LHS = new vtkStructuredGridGeometryFilter();
  bookshelf1LHS.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  bookshelf1LHS.SetExtent((int)13,(int)20,(int)0,(int)4,(int)0,(int)0);
  mapBookshelf1LHS = vtkPolyDataMapper.New();
  mapBookshelf1LHS.SetInputConnection((vtkAlgorithmOutput)bookshelf1LHS.GetOutputPort());
  mapBookshelf1LHS.ScalarVisibilityOff();
  bookshelf1LHSActor = new vtkActor();
  bookshelf1LHSActor.SetMapper((vtkMapper)mapBookshelf1LHS);
  bookshelf1LHSActor.GetProperty().SetColor((double).8,(double).8,(double).6);
  bookshelf1RHS = new vtkStructuredGridGeometryFilter();
  bookshelf1RHS.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  bookshelf1RHS.SetExtent((int)13,(int)20,(int)0,(int)4,(int)11,(int)11);
  mapBookshelf1RHS = vtkPolyDataMapper.New();
  mapBookshelf1RHS.SetInputConnection((vtkAlgorithmOutput)bookshelf1RHS.GetOutputPort());
  mapBookshelf1RHS.ScalarVisibilityOff();
  bookshelf1RHSActor = new vtkActor();
  bookshelf1RHSActor.SetMapper((vtkMapper)mapBookshelf1RHS);
  bookshelf1RHSActor.GetProperty().SetColor((double).8,(double).8,(double).6);
  bookshelf2Top = new vtkStructuredGridGeometryFilter();
  bookshelf2Top.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  bookshelf2Top.SetExtent((int)13,(int)13,(int)15,(int)19,(int)0,(int)11);
  mapBookshelf2Top = vtkPolyDataMapper.New();
  mapBookshelf2Top.SetInputConnection((vtkAlgorithmOutput)bookshelf2Top.GetOutputPort());
  mapBookshelf2Top.ScalarVisibilityOff();
  bookshelf2TopActor = new vtkActor();
  bookshelf2TopActor.SetMapper((vtkMapper)mapBookshelf2Top);
  bookshelf2TopActor.GetProperty().SetColor((double).8,(double).8,(double).6);
  bookshelf2Bottom = new vtkStructuredGridGeometryFilter();
  bookshelf2Bottom.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  bookshelf2Bottom.SetExtent((int)20,(int)20,(int)15,(int)19,(int)0,(int)11);
  mapBookshelf2Bottom = vtkPolyDataMapper.New();
  mapBookshelf2Bottom.SetInputConnection((vtkAlgorithmOutput)bookshelf2Bottom.GetOutputPort());
  mapBookshelf2Bottom.ScalarVisibilityOff();
  bookshelf2BottomActor = new vtkActor();
  bookshelf2BottomActor.SetMapper((vtkMapper)mapBookshelf2Bottom);
  bookshelf2BottomActor.GetProperty().SetColor((double).8,(double).8,(double).6);
  bookshelf2Front = new vtkStructuredGridGeometryFilter();
  bookshelf2Front.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  bookshelf2Front.SetExtent((int)13,(int)20,(int)15,(int)15,(int)0,(int)11);
  mapBookshelf2Front = vtkPolyDataMapper.New();
  mapBookshelf2Front.SetInputConnection((vtkAlgorithmOutput)bookshelf2Front.GetOutputPort());
  mapBookshelf2Front.ScalarVisibilityOff();
  bookshelf2FrontActor = new vtkActor();
  bookshelf2FrontActor.SetMapper((vtkMapper)mapBookshelf2Front);
  bookshelf2FrontActor.GetProperty().SetColor((double).8,(double).8,(double).6);
  bookshelf2Back = new vtkStructuredGridGeometryFilter();
  bookshelf2Back.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  bookshelf2Back.SetExtent((int)13,(int)20,(int)19,(int)19,(int)0,(int)11);
  mapBookshelf2Back = vtkPolyDataMapper.New();
  mapBookshelf2Back.SetInputConnection((vtkAlgorithmOutput)bookshelf2Back.GetOutputPort());
  mapBookshelf2Back.ScalarVisibilityOff();
  bookshelf2BackActor = new vtkActor();
  bookshelf2BackActor.SetMapper((vtkMapper)mapBookshelf2Back);
  bookshelf2BackActor.GetProperty().SetColor((double).8,(double).8,(double).6);
  bookshelf2LHS = new vtkStructuredGridGeometryFilter();
  bookshelf2LHS.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  bookshelf2LHS.SetExtent((int)13,(int)20,(int)15,(int)19,(int)0,(int)0);
  mapBookshelf2LHS = vtkPolyDataMapper.New();
  mapBookshelf2LHS.SetInputConnection((vtkAlgorithmOutput)bookshelf2LHS.GetOutputPort());
  mapBookshelf2LHS.ScalarVisibilityOff();
  bookshelf2LHSActor = new vtkActor();
  bookshelf2LHSActor.SetMapper((vtkMapper)mapBookshelf2LHS);
  bookshelf2LHSActor.GetProperty().SetColor((double).8,(double).8,(double).6);
  bookshelf2RHS = new vtkStructuredGridGeometryFilter();
  bookshelf2RHS.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  bookshelf2RHS.SetExtent((int)13,(int)20,(int)15,(int)19,(int)11,(int)11);
  mapBookshelf2RHS = vtkPolyDataMapper.New();
  mapBookshelf2RHS.SetInputConnection((vtkAlgorithmOutput)bookshelf2RHS.GetOutputPort());
  mapBookshelf2RHS.ScalarVisibilityOff();
  bookshelf2RHSActor = new vtkActor();
  bookshelf2RHSActor.SetMapper((vtkMapper)mapBookshelf2RHS);
  bookshelf2RHSActor.GetProperty().SetColor((double).8,(double).8,(double).6);
  window = new vtkStructuredGridGeometryFilter();
  window.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  window.SetExtent((int)20,(int)20,(int)6,(int)13,(int)10,(int)13);
  mapWindow = vtkPolyDataMapper.New();
  mapWindow.SetInputConnection((vtkAlgorithmOutput)window.GetOutputPort());
  mapWindow.ScalarVisibilityOff();
  windowActor = new vtkActor();
  windowActor.SetMapper((vtkMapper)mapWindow);
  windowActor.GetProperty().SetColor((double).3,(double).3,(double).5);
  outlet = new vtkStructuredGridGeometryFilter();
  outlet.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  outlet.SetExtent((int)0,(int)0,(int)9,(int)10,(int)14,(int)16);
  mapOutlet = vtkPolyDataMapper.New();
  mapOutlet.SetInputConnection((vtkAlgorithmOutput)outlet.GetOutputPort());
  mapOutlet.ScalarVisibilityOff();
  outletActor = new vtkActor();
  outletActor.SetMapper((vtkMapper)mapOutlet);
  outletActor.GetProperty().SetColor((double)0,(double)0,(double)0);
  inlet = new vtkStructuredGridGeometryFilter();
  inlet.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  inlet.SetExtent((int)0,(int)0,(int)9,(int)10,(int)0,(int)6);
  mapInlet = vtkPolyDataMapper.New();
  mapInlet.SetInputConnection((vtkAlgorithmOutput)inlet.GetOutputPort());
  mapInlet.ScalarVisibilityOff();
  inletActor = new vtkActor();
  inletActor.SetMapper((vtkMapper)mapInlet);
  inletActor.GetProperty().SetColor((double)0,(double)0,(double)0);
  outline = new vtkStructuredGridOutlineFilter();
  outline.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  mapOutline = vtkPolyDataMapper.New();
  mapOutline.SetInputConnection((vtkAlgorithmOutput)outline.GetOutputPort());
  outlineActor = new vtkActor();
  outlineActor.SetMapper((vtkMapper)mapOutline);
  outlineActor.GetProperty().SetColor((double)0,(double)0,(double)0);
  // Create source for streamtubes[]
  streamer = new vtkStreamPoints();
  streamer.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  streamer.SetStartPosition((double)0.1,(double)2.1,(double)0.5);
  streamer.SetMaximumPropagationTime((double)500);
  streamer.SetTimeIncrement((double)0.5);
  streamer.SetIntegrationDirectionToForward();
  cone = new vtkConeSource();
  cone.SetResolution((int)8);
  cones = new vtkGlyph3D();
  cones.SetInputConnection((vtkAlgorithmOutput)streamer.GetOutputPort());
  cones.SetSource((vtkPolyData)cone.GetOutput());
  cones.SetScaleFactor((double)0.5);
  cones.SetScaleModeToScaleByVector();
  mapCones = vtkPolyDataMapper.New();
  mapCones.SetInputConnection((vtkAlgorithmOutput)cones.GetOutputPort());
  mapCones.SetScalarRange((double)((vtkDataSet)reader.GetOutput()).GetScalarRange()[0],(double)((vtkDataSet)reader.GetOutput()).GetScalarRange()[1]);
  conesActor = new vtkActor();
  conesActor.SetMapper((vtkMapper)mapCones);
  ren1.AddActor((vtkProp)table1Actor);
  ren1.AddActor((vtkProp)table2Actor);
  ren1.AddActor((vtkProp)FilingCabinet1Actor);
  ren1.AddActor((vtkProp)FilingCabinet2Actor);
  ren1.AddActor((vtkProp)bookshelf1TopActor);
  ren1.AddActor((vtkProp)bookshelf1BottomActor);
  ren1.AddActor((vtkProp)bookshelf1FrontActor);
  ren1.AddActor((vtkProp)bookshelf1BackActor);
  ren1.AddActor((vtkProp)bookshelf1LHSActor);
  ren1.AddActor((vtkProp)bookshelf1RHSActor);
  ren1.AddActor((vtkProp)bookshelf2TopActor);
  ren1.AddActor((vtkProp)bookshelf2BottomActor);
  ren1.AddActor((vtkProp)bookshelf2FrontActor);
  ren1.AddActor((vtkProp)bookshelf2BackActor);
  ren1.AddActor((vtkProp)bookshelf2LHSActor);
  ren1.AddActor((vtkProp)bookshelf2RHSActor);
  ren1.AddActor((vtkProp)windowActor);
  ren1.AddActor((vtkProp)outletActor);
  ren1.AddActor((vtkProp)inletActor);
  ren1.AddActor((vtkProp)outlineActor);
  ren1.AddActor((vtkProp)conesActor);
  ren1.SetBackground((double)0.4,(double)0.4,(double)0.5);
  aCamera = new vtkCamera();
  aCamera.SetClippingRange((double)0.7724,(double)39);
  aCamera.SetFocalPoint((double)1.14798,(double)3.08416,(double)2.47187);
  aCamera.SetPosition((double)-2.64683,(double)-3.55525,(double)3.55848);
  aCamera.SetViewUp((double)0.0511273,(double)0.132773,(double)0.989827);
  aCamera.SetViewAngle((double)15.5033);
  ren1.SetActiveCamera((vtkCamera)aCamera);
  renWin.SetSize((int)500,(int)300);
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
static vtkOnePieceExtentTranslator translator;
static double length;
static double maxVelocity;
static double maxTime;
static vtkStructuredGridGeometryFilter table1;
static vtkPolyDataMapper mapTable1;
static vtkActor table1Actor;
static vtkStructuredGridGeometryFilter table2;
static vtkPolyDataMapper mapTable2;
static vtkActor table2Actor;
static vtkStructuredGridGeometryFilter FilingCabinet1;
static vtkPolyDataMapper mapFilingCabinet1;
static vtkActor FilingCabinet1Actor;
static vtkStructuredGridGeometryFilter FilingCabinet2;
static vtkPolyDataMapper mapFilingCabinet2;
static vtkActor FilingCabinet2Actor;
static vtkStructuredGridGeometryFilter bookshelf1Top;
static vtkPolyDataMapper mapBookshelf1Top;
static vtkActor bookshelf1TopActor;
static vtkStructuredGridGeometryFilter bookshelf1Bottom;
static vtkPolyDataMapper mapBookshelf1Bottom;
static vtkActor bookshelf1BottomActor;
static vtkStructuredGridGeometryFilter bookshelf1Front;
static vtkPolyDataMapper mapBookshelf1Front;
static vtkActor bookshelf1FrontActor;
static vtkStructuredGridGeometryFilter bookshelf1Back;
static vtkPolyDataMapper mapBookshelf1Back;
static vtkActor bookshelf1BackActor;
static vtkStructuredGridGeometryFilter bookshelf1LHS;
static vtkPolyDataMapper mapBookshelf1LHS;
static vtkActor bookshelf1LHSActor;
static vtkStructuredGridGeometryFilter bookshelf1RHS;
static vtkPolyDataMapper mapBookshelf1RHS;
static vtkActor bookshelf1RHSActor;
static vtkStructuredGridGeometryFilter bookshelf2Top;
static vtkPolyDataMapper mapBookshelf2Top;
static vtkActor bookshelf2TopActor;
static vtkStructuredGridGeometryFilter bookshelf2Bottom;
static vtkPolyDataMapper mapBookshelf2Bottom;
static vtkActor bookshelf2BottomActor;
static vtkStructuredGridGeometryFilter bookshelf2Front;
static vtkPolyDataMapper mapBookshelf2Front;
static vtkActor bookshelf2FrontActor;
static vtkStructuredGridGeometryFilter bookshelf2Back;
static vtkPolyDataMapper mapBookshelf2Back;
static vtkActor bookshelf2BackActor;
static vtkStructuredGridGeometryFilter bookshelf2LHS;
static vtkPolyDataMapper mapBookshelf2LHS;
static vtkActor bookshelf2LHSActor;
static vtkStructuredGridGeometryFilter bookshelf2RHS;
static vtkPolyDataMapper mapBookshelf2RHS;
static vtkActor bookshelf2RHSActor;
static vtkStructuredGridGeometryFilter window;
static vtkPolyDataMapper mapWindow;
static vtkActor windowActor;
static vtkStructuredGridGeometryFilter outlet;
static vtkPolyDataMapper mapOutlet;
static vtkActor outletActor;
static vtkStructuredGridGeometryFilter inlet;
static vtkPolyDataMapper mapInlet;
static vtkActor inletActor;
static vtkStructuredGridOutlineFilter outline;
static vtkPolyDataMapper mapOutline;
static vtkActor outlineActor;
static vtkStreamPoints streamer;
static vtkConeSource cone;
static vtkGlyph3D cones;
static vtkPolyDataMapper mapCones;
static vtkActor conesActor;
static vtkCamera aCamera;


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
        public static double Getlength()
        {
            return length;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setlength(double toSet)
        {
            length = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double GetmaxVelocity()
        {
            return maxVelocity;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmaxVelocity(double toSet)
        {
            maxVelocity = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double GetmaxTime()
        {
            return maxTime;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmaxTime(double toSet)
        {
            maxTime = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Gettable1()
        {
            return table1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settable1(vtkStructuredGridGeometryFilter toSet)
        {
            table1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapTable1()
        {
            return mapTable1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapTable1(vtkPolyDataMapper toSet)
        {
            mapTable1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Gettable1Actor()
        {
            return table1Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settable1Actor(vtkActor toSet)
        {
            table1Actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Gettable2()
        {
            return table2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settable2(vtkStructuredGridGeometryFilter toSet)
        {
            table2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapTable2()
        {
            return mapTable2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapTable2(vtkPolyDataMapper toSet)
        {
            mapTable2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Gettable2Actor()
        {
            return table2Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settable2Actor(vtkActor toSet)
        {
            table2Actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter GetFilingCabinet1()
        {
            return FilingCabinet1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetFilingCabinet1(vtkStructuredGridGeometryFilter toSet)
        {
            FilingCabinet1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapFilingCabinet1()
        {
            return mapFilingCabinet1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapFilingCabinet1(vtkPolyDataMapper toSet)
        {
            mapFilingCabinet1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetFilingCabinet1Actor()
        {
            return FilingCabinet1Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetFilingCabinet1Actor(vtkActor toSet)
        {
            FilingCabinet1Actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter GetFilingCabinet2()
        {
            return FilingCabinet2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetFilingCabinet2(vtkStructuredGridGeometryFilter toSet)
        {
            FilingCabinet2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapFilingCabinet2()
        {
            return mapFilingCabinet2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapFilingCabinet2(vtkPolyDataMapper toSet)
        {
            mapFilingCabinet2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetFilingCabinet2Actor()
        {
            return FilingCabinet2Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetFilingCabinet2Actor(vtkActor toSet)
        {
            FilingCabinet2Actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getbookshelf1Top()
        {
            return bookshelf1Top;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf1Top(vtkStructuredGridGeometryFilter toSet)
        {
            bookshelf1Top = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapBookshelf1Top()
        {
            return mapBookshelf1Top;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapBookshelf1Top(vtkPolyDataMapper toSet)
        {
            mapBookshelf1Top = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getbookshelf1TopActor()
        {
            return bookshelf1TopActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf1TopActor(vtkActor toSet)
        {
            bookshelf1TopActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getbookshelf1Bottom()
        {
            return bookshelf1Bottom;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf1Bottom(vtkStructuredGridGeometryFilter toSet)
        {
            bookshelf1Bottom = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapBookshelf1Bottom()
        {
            return mapBookshelf1Bottom;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapBookshelf1Bottom(vtkPolyDataMapper toSet)
        {
            mapBookshelf1Bottom = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getbookshelf1BottomActor()
        {
            return bookshelf1BottomActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf1BottomActor(vtkActor toSet)
        {
            bookshelf1BottomActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getbookshelf1Front()
        {
            return bookshelf1Front;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf1Front(vtkStructuredGridGeometryFilter toSet)
        {
            bookshelf1Front = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapBookshelf1Front()
        {
            return mapBookshelf1Front;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapBookshelf1Front(vtkPolyDataMapper toSet)
        {
            mapBookshelf1Front = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getbookshelf1FrontActor()
        {
            return bookshelf1FrontActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf1FrontActor(vtkActor toSet)
        {
            bookshelf1FrontActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getbookshelf1Back()
        {
            return bookshelf1Back;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf1Back(vtkStructuredGridGeometryFilter toSet)
        {
            bookshelf1Back = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapBookshelf1Back()
        {
            return mapBookshelf1Back;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapBookshelf1Back(vtkPolyDataMapper toSet)
        {
            mapBookshelf1Back = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getbookshelf1BackActor()
        {
            return bookshelf1BackActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf1BackActor(vtkActor toSet)
        {
            bookshelf1BackActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getbookshelf1LHS()
        {
            return bookshelf1LHS;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf1LHS(vtkStructuredGridGeometryFilter toSet)
        {
            bookshelf1LHS = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapBookshelf1LHS()
        {
            return mapBookshelf1LHS;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapBookshelf1LHS(vtkPolyDataMapper toSet)
        {
            mapBookshelf1LHS = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getbookshelf1LHSActor()
        {
            return bookshelf1LHSActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf1LHSActor(vtkActor toSet)
        {
            bookshelf1LHSActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getbookshelf1RHS()
        {
            return bookshelf1RHS;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf1RHS(vtkStructuredGridGeometryFilter toSet)
        {
            bookshelf1RHS = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapBookshelf1RHS()
        {
            return mapBookshelf1RHS;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapBookshelf1RHS(vtkPolyDataMapper toSet)
        {
            mapBookshelf1RHS = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getbookshelf1RHSActor()
        {
            return bookshelf1RHSActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf1RHSActor(vtkActor toSet)
        {
            bookshelf1RHSActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getbookshelf2Top()
        {
            return bookshelf2Top;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf2Top(vtkStructuredGridGeometryFilter toSet)
        {
            bookshelf2Top = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapBookshelf2Top()
        {
            return mapBookshelf2Top;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapBookshelf2Top(vtkPolyDataMapper toSet)
        {
            mapBookshelf2Top = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getbookshelf2TopActor()
        {
            return bookshelf2TopActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf2TopActor(vtkActor toSet)
        {
            bookshelf2TopActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getbookshelf2Bottom()
        {
            return bookshelf2Bottom;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf2Bottom(vtkStructuredGridGeometryFilter toSet)
        {
            bookshelf2Bottom = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapBookshelf2Bottom()
        {
            return mapBookshelf2Bottom;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapBookshelf2Bottom(vtkPolyDataMapper toSet)
        {
            mapBookshelf2Bottom = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getbookshelf2BottomActor()
        {
            return bookshelf2BottomActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf2BottomActor(vtkActor toSet)
        {
            bookshelf2BottomActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getbookshelf2Front()
        {
            return bookshelf2Front;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf2Front(vtkStructuredGridGeometryFilter toSet)
        {
            bookshelf2Front = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapBookshelf2Front()
        {
            return mapBookshelf2Front;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapBookshelf2Front(vtkPolyDataMapper toSet)
        {
            mapBookshelf2Front = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getbookshelf2FrontActor()
        {
            return bookshelf2FrontActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf2FrontActor(vtkActor toSet)
        {
            bookshelf2FrontActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getbookshelf2Back()
        {
            return bookshelf2Back;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf2Back(vtkStructuredGridGeometryFilter toSet)
        {
            bookshelf2Back = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapBookshelf2Back()
        {
            return mapBookshelf2Back;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapBookshelf2Back(vtkPolyDataMapper toSet)
        {
            mapBookshelf2Back = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getbookshelf2BackActor()
        {
            return bookshelf2BackActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf2BackActor(vtkActor toSet)
        {
            bookshelf2BackActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getbookshelf2LHS()
        {
            return bookshelf2LHS;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf2LHS(vtkStructuredGridGeometryFilter toSet)
        {
            bookshelf2LHS = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapBookshelf2LHS()
        {
            return mapBookshelf2LHS;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapBookshelf2LHS(vtkPolyDataMapper toSet)
        {
            mapBookshelf2LHS = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getbookshelf2LHSActor()
        {
            return bookshelf2LHSActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf2LHSActor(vtkActor toSet)
        {
            bookshelf2LHSActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getbookshelf2RHS()
        {
            return bookshelf2RHS;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf2RHS(vtkStructuredGridGeometryFilter toSet)
        {
            bookshelf2RHS = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapBookshelf2RHS()
        {
            return mapBookshelf2RHS;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapBookshelf2RHS(vtkPolyDataMapper toSet)
        {
            mapBookshelf2RHS = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getbookshelf2RHSActor()
        {
            return bookshelf2RHSActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbookshelf2RHSActor(vtkActor toSet)
        {
            bookshelf2RHSActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getwindow()
        {
            return window;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setwindow(vtkStructuredGridGeometryFilter toSet)
        {
            window = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapWindow()
        {
            return mapWindow;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapWindow(vtkPolyDataMapper toSet)
        {
            mapWindow = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetwindowActor()
        {
            return windowActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetwindowActor(vtkActor toSet)
        {
            windowActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getoutlet()
        {
            return outlet;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setoutlet(vtkStructuredGridGeometryFilter toSet)
        {
            outlet = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapOutlet()
        {
            return mapOutlet;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapOutlet(vtkPolyDataMapper toSet)
        {
            mapOutlet = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetoutletActor()
        {
            return outletActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetoutletActor(vtkActor toSet)
        {
            outletActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getinlet()
        {
            return inlet;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setinlet(vtkStructuredGridGeometryFilter toSet)
        {
            inlet = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapInlet()
        {
            return mapInlet;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapInlet(vtkPolyDataMapper toSet)
        {
            mapInlet = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetinletActor()
        {
            return inletActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetinletActor(vtkActor toSet)
        {
            inletActor = toSet;
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
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCamera GetaCamera()
        {
            return aCamera;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaCamera(vtkCamera toSet)
        {
            aCamera = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(reader!= null){reader.Dispose();}
  	if(translator!= null){translator.Dispose();}
  	if(table1!= null){table1.Dispose();}
  	if(mapTable1!= null){mapTable1.Dispose();}
  	if(table1Actor!= null){table1Actor.Dispose();}
  	if(table2!= null){table2.Dispose();}
  	if(mapTable2!= null){mapTable2.Dispose();}
  	if(table2Actor!= null){table2Actor.Dispose();}
  	if(FilingCabinet1!= null){FilingCabinet1.Dispose();}
  	if(mapFilingCabinet1!= null){mapFilingCabinet1.Dispose();}
  	if(FilingCabinet1Actor!= null){FilingCabinet1Actor.Dispose();}
  	if(FilingCabinet2!= null){FilingCabinet2.Dispose();}
  	if(mapFilingCabinet2!= null){mapFilingCabinet2.Dispose();}
  	if(FilingCabinet2Actor!= null){FilingCabinet2Actor.Dispose();}
  	if(bookshelf1Top!= null){bookshelf1Top.Dispose();}
  	if(mapBookshelf1Top!= null){mapBookshelf1Top.Dispose();}
  	if(bookshelf1TopActor!= null){bookshelf1TopActor.Dispose();}
  	if(bookshelf1Bottom!= null){bookshelf1Bottom.Dispose();}
  	if(mapBookshelf1Bottom!= null){mapBookshelf1Bottom.Dispose();}
  	if(bookshelf1BottomActor!= null){bookshelf1BottomActor.Dispose();}
  	if(bookshelf1Front!= null){bookshelf1Front.Dispose();}
  	if(mapBookshelf1Front!= null){mapBookshelf1Front.Dispose();}
  	if(bookshelf1FrontActor!= null){bookshelf1FrontActor.Dispose();}
  	if(bookshelf1Back!= null){bookshelf1Back.Dispose();}
  	if(mapBookshelf1Back!= null){mapBookshelf1Back.Dispose();}
  	if(bookshelf1BackActor!= null){bookshelf1BackActor.Dispose();}
  	if(bookshelf1LHS!= null){bookshelf1LHS.Dispose();}
  	if(mapBookshelf1LHS!= null){mapBookshelf1LHS.Dispose();}
  	if(bookshelf1LHSActor!= null){bookshelf1LHSActor.Dispose();}
  	if(bookshelf1RHS!= null){bookshelf1RHS.Dispose();}
  	if(mapBookshelf1RHS!= null){mapBookshelf1RHS.Dispose();}
  	if(bookshelf1RHSActor!= null){bookshelf1RHSActor.Dispose();}
  	if(bookshelf2Top!= null){bookshelf2Top.Dispose();}
  	if(mapBookshelf2Top!= null){mapBookshelf2Top.Dispose();}
  	if(bookshelf2TopActor!= null){bookshelf2TopActor.Dispose();}
  	if(bookshelf2Bottom!= null){bookshelf2Bottom.Dispose();}
  	if(mapBookshelf2Bottom!= null){mapBookshelf2Bottom.Dispose();}
  	if(bookshelf2BottomActor!= null){bookshelf2BottomActor.Dispose();}
  	if(bookshelf2Front!= null){bookshelf2Front.Dispose();}
  	if(mapBookshelf2Front!= null){mapBookshelf2Front.Dispose();}
  	if(bookshelf2FrontActor!= null){bookshelf2FrontActor.Dispose();}
  	if(bookshelf2Back!= null){bookshelf2Back.Dispose();}
  	if(mapBookshelf2Back!= null){mapBookshelf2Back.Dispose();}
  	if(bookshelf2BackActor!= null){bookshelf2BackActor.Dispose();}
  	if(bookshelf2LHS!= null){bookshelf2LHS.Dispose();}
  	if(mapBookshelf2LHS!= null){mapBookshelf2LHS.Dispose();}
  	if(bookshelf2LHSActor!= null){bookshelf2LHSActor.Dispose();}
  	if(bookshelf2RHS!= null){bookshelf2RHS.Dispose();}
  	if(mapBookshelf2RHS!= null){mapBookshelf2RHS.Dispose();}
  	if(bookshelf2RHSActor!= null){bookshelf2RHSActor.Dispose();}
  	if(window!= null){window.Dispose();}
  	if(mapWindow!= null){mapWindow.Dispose();}
  	if(windowActor!= null){windowActor.Dispose();}
  	if(outlet!= null){outlet.Dispose();}
  	if(mapOutlet!= null){mapOutlet.Dispose();}
  	if(outletActor!= null){outletActor.Dispose();}
  	if(inlet!= null){inlet.Dispose();}
  	if(mapInlet!= null){mapInlet.Dispose();}
  	if(inletActor!= null){inletActor.Dispose();}
  	if(outline!= null){outline.Dispose();}
  	if(mapOutline!= null){mapOutline.Dispose();}
  	if(outlineActor!= null){outlineActor.Dispose();}
  	if(streamer!= null){streamer.Dispose();}
  	if(cone!= null){cone.Dispose();}
  	if(cones!= null){cones.Dispose();}
  	if(mapCones!= null){mapCones.Dispose();}
  	if(conesActor!= null){conesActor.Dispose();}
  	if(aCamera!= null){aCamera.Dispose();}
  }

}
//--- end of script --//

