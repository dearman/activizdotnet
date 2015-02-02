using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\TestMultiBlockStreamer.tcl
// output file is AVTestMultiBlockStreamer.cs
/// <summary>
/// The testing class derived from AVTestMultiBlockStreamer
/// </summary>
public class AVTestMultiBlockStreamerClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVTestMultiBlockStreamer(String [] argv)
  {
  //Prefix Content is: ""
  
  // we need to use composite data pipeline with multiblock datasets[]
  alg = new vtkAlgorithm();
  pip = new vtkCompositeDataPipeline();
  vtkAlgorithm.SetDefaultExecutivePrototype((vtkExecutive)pip);
  //skipping Delete pip
  Ren1 = vtkRenderer.New();
  Ren1.SetBackground((double)0.33,(double)0.35,(double)0.43);

  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)Ren1);

  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);

  Plot3D0 = new vtkMultiBlockPLOT3DReader();
  Plot3D0.SetFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/combxyz.bin");
  Plot3D0.SetQFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/combq.bin");
  Plot3D0.SetBinaryFile((int)1);
  Plot3D0.SetMultiGrid((int)0);
  Plot3D0.SetHasByteCount((int)0);
  Plot3D0.SetIBlanking((int)0);
  Plot3D0.SetTwoDimensionalGeometry((int)0);
  Plot3D0.SetForceRead((int)0);
  Plot3D0.SetByteOrder((int)0);
  Plot3D0.Update();
 
  Geometry5 = new vtkStructuredGridOutlineFilter();
  Geometry5.SetInputData((vtkDataSet)Plot3D0.GetOutput().GetBlock(0));

  Mapper5 = vtkPolyDataMapper.New();
  Mapper5.SetInputConnection((vtkAlgorithmOutput)Geometry5.GetOutputPort());
  Mapper5.SetImmediateModeRendering((int)1);
  Mapper5.UseLookupTableScalarRangeOn();
  Mapper5.SetScalarVisibility((int)0);
  Mapper5.SetScalarModeToDefault();

  Actor5 = new vtkActor();
  Actor5.SetMapper((vtkMapper)Mapper5);
  Actor5.GetProperty().SetRepresentationToSurface();
  Actor5.GetProperty().SetInterpolationToGouraud();
  Actor5.GetProperty().SetAmbient((double)0.15);
  Actor5.GetProperty().SetDiffuse((double)0.85);
  Actor5.GetProperty().SetSpecular((double)0.1);
  Actor5.GetProperty().SetSpecularPower((double)100);
  Actor5.GetProperty().SetSpecularColor((double)1,(double)1,(double)1);

  Actor5.GetProperty().SetColor((double)1,(double)1,(double)1);
  Ren1.AddActor((vtkProp)Actor5);

  ExtractGrid[0] = new vtkExtractGrid();
  ExtractGrid[0].SetInputData((vtkDataSet)Plot3D0.GetOutput().GetBlock(0));
  ExtractGrid[0].SetVOI((int)0,(int)14,(int)0,(int)32,(int)0,(int)24);
  ExtractGrid[0].SetSampleRate((int)1,(int)1,(int)1);
  ExtractGrid[0].SetIncludeBoundary((int)0);

  ExtractGrid[1] = new vtkExtractGrid();
  ExtractGrid[1].SetInputData((vtkDataSet)Plot3D0.GetOutput().GetBlock(0));
  ExtractGrid[1].SetVOI((int)14,(int)29,(int)0,(int)32,(int)0,(int)24);
  ExtractGrid[1].SetSampleRate((int)1,(int)1,(int)1);
  ExtractGrid[1].SetIncludeBoundary((int)0);

  ExtractGrid[2] = new vtkExtractGrid();
  ExtractGrid[2].SetInputData((vtkDataSet)Plot3D0.GetOutput().GetBlock(0));
  ExtractGrid[2].SetVOI((int)29,(int)56,(int)0,(int)32,(int)0,(int)24);
  ExtractGrid[2].SetSampleRate((int)1,(int)1,(int)1);
  ExtractGrid[2].SetIncludeBoundary((int)0);

  LineSourceWidget0 = new vtkLineSource();
  LineSourceWidget0.SetPoint1((double)3.05638,(double)-3.00497,(double)28.2211);
  LineSourceWidget0.SetPoint2((double)3.05638,(double)3.95916,(double)28.2211);
  LineSourceWidget0.SetResolution((int)20);

  mbds = new vtkMultiBlockDataSet();
  mbds.SetNumberOfBlocks((uint)3);
  i = 0;
  while((i) < 3)
    {
      ExtractGrid[i].Update();
      sg[i] = vtkStructuredGrid.New();
      sg[i].ShallowCopy(ExtractGrid[i].GetOutput());
      mbds.SetBlock((uint)i, sg[i]);
      //skipping Delete sg[i]
      i = i + 1;
    }

  Stream0 = new vtkStreamTracer();
  Stream0.SetInputData((vtkDataObject)mbds);
  Stream0.SetSourceConnection(LineSourceWidget0.GetOutputPort());
  Stream0.SetIntegrationStepUnit(2);
  Stream0.SetMaximumPropagation((double)20);
  Stream0.SetInitialIntegrationStep((double)0.5);
  Stream0.SetIntegrationDirection((int)0);
  Stream0.SetIntegratorType((int)0);
  Stream0.SetMaximumNumberOfSteps((int)2000);
  Stream0.SetTerminalSpeed((double)1e-12);

  //skipping Delete mbds

  aa = new vtkAssignAttribute();
  aa.SetInputConnection((vtkAlgorithmOutput)Stream0.GetOutputPort());
  aa.Assign((string)"Normals",(string)"NORMALS",(string)"POINT_DATA");

  Ribbon0 = new vtkRibbonFilter();
  Ribbon0.SetInputConnection((vtkAlgorithmOutput)aa.GetOutputPort());
  Ribbon0.SetWidth((double)0.1);
  Ribbon0.SetAngle((double)0);
  Ribbon0.SetDefaultNormal((double)0,(double)0,(double)1);
  Ribbon0.SetVaryWidth((int)0);

  LookupTable1 = new vtkLookupTable();
  LookupTable1.SetNumberOfTableValues((int)256);
  LookupTable1.SetHueRange((double)0,(double)0.66667);
  LookupTable1.SetSaturationRange((double)1,(double)1);
  LookupTable1.SetValueRange((double)1,(double)1);
  LookupTable1.SetTableRange((double)0.197813,(double)0.710419);
  LookupTable1.SetVectorComponent((int)0);
  LookupTable1.Build();

  Mapper10 = vtkPolyDataMapper.New();
  Mapper10.SetInputConnection((vtkAlgorithmOutput)Ribbon0.GetOutputPort());
  Mapper10.SetImmediateModeRendering((int)1);
  Mapper10.UseLookupTableScalarRangeOn();
  Mapper10.SetScalarVisibility((int)1);
  Mapper10.SetScalarModeToUsePointFieldData();
  Mapper10.SelectColorArray((string)"Density");
  Mapper10.SetLookupTable((vtkScalarsToColors)LookupTable1);

  Actor10 = new vtkActor();
  Actor10.SetMapper((vtkMapper)Mapper10);
  Actor10.GetProperty().SetRepresentationToSurface();
  Actor10.GetProperty().SetInterpolationToGouraud();
  Actor10.GetProperty().SetAmbient((double)0.15);
  Actor10.GetProperty().SetDiffuse((double)0.85);
  Actor10.GetProperty().SetSpecular((double)0);
  Actor10.GetProperty().SetSpecularPower((double)1);
  Actor10.GetProperty().SetSpecularColor((double)1,(double)1,(double)1);
  Ren1.AddActor((vtkProp)Actor10);

  // enable user interface interactor[]
  iren.Initialize();
  // prevent the tk window from showing up then start the event loop[]
  vtkAlgorithm.SetDefaultExecutivePrototype(null);
  //skipping Delete alg
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkAlgorithm alg;
static vtkCompositeDataPipeline pip;
static vtkRenderer Ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkMultiBlockPLOT3DReader Plot3D0;
static vtkStructuredGridOutlineFilter Geometry5;
static vtkPolyDataMapper Mapper5;
static vtkActor Actor5;
static vtkExtractGrid[] ExtractGrid = new vtkExtractGrid[100];
static vtkLineSource LineSourceWidget0;
static vtkMultiBlockDataSet mbds;
static int i;
static vtkStructuredGrid[] sg = new vtkStructuredGrid[100];
static vtkStreamTracer Stream0;
static vtkAssignAttribute aa;
static vtkRibbonFilter Ribbon0;
static vtkLookupTable LookupTable1;
static vtkPolyDataMapper Mapper10;
static vtkActor Actor10;


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
        public static vtkAlgorithm Getalg()
        {
            return alg;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setalg(vtkAlgorithm toSet)
        {
            alg = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCompositeDataPipeline Getpip()
        {
            return pip;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpip(vtkCompositeDataPipeline toSet)
        {
            pip = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRenderer GetRen1()
        {
            return Ren1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetRen1(vtkRenderer toSet)
        {
            Ren1 = toSet;
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
        public static vtkMultiBlockPLOT3DReader GetPlot3D0()
        {
            return Plot3D0;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetPlot3D0(vtkMultiBlockPLOT3DReader toSet)
        {
            Plot3D0 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridOutlineFilter GetGeometry5()
        {
            return Geometry5;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetGeometry5(vtkStructuredGridOutlineFilter toSet)
        {
            Geometry5 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetMapper5()
        {
            return Mapper5;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetMapper5(vtkPolyDataMapper toSet)
        {
            Mapper5 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetActor5()
        {
            return Actor5;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetActor5(vtkActor toSet)
        {
            Actor5 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkExtractGrid GetExtractGrid0()
        {
            return ExtractGrid[0];
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetExtractGrid0(vtkExtractGrid toSet)
        {
            ExtractGrid[0] = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkExtractGrid GetExtractGrid1()
        {
            return ExtractGrid[1];
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetExtractGrid1(vtkExtractGrid toSet)
        {
            ExtractGrid[1] = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkExtractGrid GetExtractGrid2()
        {
            return ExtractGrid[2];
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetExtractGrid2(vtkExtractGrid toSet)
        {
            ExtractGrid[2] = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkLineSource GetLineSourceWidget0()
        {
            return LineSourceWidget0;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetLineSourceWidget0(vtkLineSource toSet)
        {
            LineSourceWidget0 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkMultiBlockDataSet Getmbds()
        {
            return mbds;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setmbds(vtkMultiBlockDataSet toSet)
        {
            mbds = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int Geti()
        {
            return i;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Seti(int toSet)
        {
            i = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGrid[] Getsg()
        {
            return sg;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setsg(vtkStructuredGrid[] toSet)
        {
            sg = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStreamTracer GetStream0()
        {
            return Stream0;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetStream0(vtkStreamTracer toSet)
        {
            Stream0 = toSet;
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
        public static vtkRibbonFilter GetRibbon0()
        {
            return Ribbon0;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetRibbon0(vtkRibbonFilter toSet)
        {
            Ribbon0 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkLookupTable GetLookupTable1()
        {
            return LookupTable1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetLookupTable1(vtkLookupTable toSet)
        {
            LookupTable1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetMapper10()
        {
            return Mapper10;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetMapper10(vtkPolyDataMapper toSet)
        {
            Mapper10 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetActor10()
        {
            return Actor10;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetActor10(vtkActor toSet)
        {
            Actor10 = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(alg!= null){alg.Dispose();}
  	if(pip!= null){pip.Dispose();}
  	if(Ren1!= null){Ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(Plot3D0!= null){Plot3D0.Dispose();}
  	if(Geometry5!= null){Geometry5.Dispose();}
  	if(Mapper5!= null){Mapper5.Dispose();}
  	if(Actor5!= null){Actor5.Dispose();}
  	if(ExtractGrid[0]!= null){ExtractGrid[0].Dispose();}
  	if(ExtractGrid[1]!= null){ExtractGrid[1].Dispose();}
  	if(ExtractGrid[2]!= null){ExtractGrid[2].Dispose();}
  	if(LineSourceWidget0!= null){LineSourceWidget0.Dispose();}
  	if(mbds!= null){mbds.Dispose();}
  	if(Stream0!= null){Stream0.Dispose();}
  	if(aa!= null){aa.Dispose();}
  	if(Ribbon0!= null){Ribbon0.Dispose();}
  	if(LookupTable1!= null){LookupTable1.Dispose();}
  	if(Mapper10!= null){Mapper10.Dispose();}
  	if(Actor10!= null){Actor10.Dispose();}
  }

}
//--- end of script --//

