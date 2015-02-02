using Kitware.VTK;
using System;
// input file is C:\VTK\Parallel\Testing\Tcl\TestExtractCTHPart.tcl
// output file is AVTestExtractCTHPart.cs
/// <summary>
/// The testing class derived from AVTestExtractCTHPart
/// </summary>
public class AVTestExtractCTHPartClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVTestExtractCTHPart(String [] argv)
  {
  //Prefix Content is: ""
  
  // we need to use composite data pipeline with multiblock datasets[]
  alg = new vtkAlgorithm();
  pip = new vtkCompositeDataPipeline();
  vtkAlgorithm.SetDefaultExecutivePrototype((vtkExecutive)pip);
  //skipping Delete pip
  // Create the RenderWindow, Renderer and both Actors[]
  //[]
  Ren1 = vtkRenderer.New();
  Ren1.SetBackground((double)0.33,(double)0.35,(double)0.43);
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)Ren1);
  renWin.SetSize((int)300,(int)300);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  pvTemp59 = new vtkXMLRectilinearGridReader();
  pvTemp59.SetFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/cth.vtr");
  pvTemp59.UpdateInformation();
  pvTemp59.SetCellArrayStatus((string)"X Velocity",(int)0);
  pvTemp59.SetCellArrayStatus((string)"Y Velocity",(int)0);
  pvTemp59.SetCellArrayStatus((string)"Z Velocity",(int)0);
  pvTemp59.SetCellArrayStatus((string)"Mass for Armor Plate",(int)0);
  pvTemp59.SetCellArrayStatus((string)"Mass for Body, Nose",(int)0);
  pvTemp79 = new vtkExtractCTHPart();
  pvTemp79.SetInputConnection((vtkAlgorithmOutput)pvTemp59.GetOutputPort());
  pvTemp79.AddVolumeArrayName((string)"Volume Fraction for Armor Plate");
  pvTemp79.AddVolumeArrayName((string)"Volume Fraction for Body, Nose");
  pvTemp79.SetClipPlane(null);
  pvTemp104 = new vtkLookupTable();
  pvTemp104.SetNumberOfTableValues((int)256);
  pvTemp104.SetHueRange((double)0.6667,(double)0);
  pvTemp104.SetSaturationRange((double)1,(double)1);
  pvTemp104.SetValueRange((double)1,(double)1);
  pvTemp104.SetTableRange((double)0,(double)1);
  pvTemp104.SetVectorComponent((int)0);
  pvTemp104.Build();
  pvTemp87 = new vtkCompositePolyDataMapper();
  pvTemp87.SetInputConnection((vtkAlgorithmOutput)pvTemp79.GetOutputPort());
  pvTemp87.SetImmediateModeRendering((int)1);
  pvTemp87.SetScalarRange((double)0,(double)1);
  pvTemp87.UseLookupTableScalarRangeOn();
  pvTemp87.SetScalarVisibility((int)1);
  pvTemp87.SetScalarModeToUsePointFieldData();
  pvTemp87.SelectColorArray((string)"Part Index");
  pvTemp87.SetLookupTable((vtkScalarsToColors)pvTemp104);
  pvTemp88 = new vtkActor();
  pvTemp88.SetMapper((vtkMapper)pvTemp87);
  pvTemp88.GetProperty().SetRepresentationToSurface();
  pvTemp88.GetProperty().SetInterpolationToGouraud();
  pvTemp88.GetProperty().SetAmbient((double)0);
  pvTemp88.GetProperty().SetDiffuse((double)1);
  pvTemp88.GetProperty().SetSpecular((double)0);
  pvTemp88.GetProperty().SetSpecularPower((double)1);
  pvTemp88.GetProperty().SetSpecularColor((double)1,(double)1,(double)1);
  Ren1.AddActor((vtkProp)pvTemp88);
  renWin.Render();
  vtkAlgorithm.SetDefaultExecutivePrototype(null);
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkAlgorithm alg;
static vtkCompositeDataPipeline pip;
static vtkRenderer Ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkXMLRectilinearGridReader pvTemp59;
static vtkExtractCTHPart pvTemp79;
static vtkLookupTable pvTemp104;
static vtkCompositePolyDataMapper pvTemp87;
static vtkActor pvTemp88;


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
        public static vtkXMLRectilinearGridReader GetpvTemp59()
        {
            return pvTemp59;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetpvTemp59(vtkXMLRectilinearGridReader toSet)
        {
            pvTemp59 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkExtractCTHPart GetpvTemp79()
        {
            return pvTemp79;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetpvTemp79(vtkExtractCTHPart toSet)
        {
            pvTemp79 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkLookupTable GetpvTemp104()
        {
            return pvTemp104;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetpvTemp104(vtkLookupTable toSet)
        {
            pvTemp104 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCompositePolyDataMapper GetpvTemp87()
        {
            return pvTemp87;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetpvTemp87(vtkCompositePolyDataMapper toSet)
        {
            pvTemp87 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetpvTemp88()
        {
            return pvTemp88;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetpvTemp88(vtkActor toSet)
        {
            pvTemp88 = toSet;
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
  	if(pvTemp59!= null){pvTemp59.Dispose();}
  	if(pvTemp79!= null){pvTemp79.Dispose();}
  	if(pvTemp104!= null){pvTemp104.Dispose();}
  	if(pvTemp87!= null){pvTemp87.Dispose();}
  	if(pvTemp88!= null){pvTemp88.Dispose();}
  }

}
//--- end of script --//

