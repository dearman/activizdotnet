using Kitware.VTK;
using System;
// input file is C:\VTK\Rendering\Testing\Tcl\labeledContours.tcl
// output file is AVlabeledContours.cs
/// <summary>
/// The testing class derived from AVlabeledContours
/// </summary>
public class AVlabeledContoursClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVlabeledContours(String [] argv)
  {
  //Prefix Content is: ""
  
  // demonstrate labeling of contour with scalar value[]
  // Create the RenderWindow, Renderer and both Actors[]
  //[]
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  // Read a slice and contour it[]
  v16 = new vtkVolume16Reader();
  v16.SetDataDimensions((int)64,(int)64);
  v16.GetOutput().SetOrigin((double)0.0,(double)0.0,(double)0.0);
  v16.SetDataByteOrderToLittleEndian();
  v16.SetFilePrefix((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/headsq/quarter");
  v16.SetImageRange((int)45,(int)45);
  v16.SetDataSpacing((double)3.2,(double)3.2,(double)1.5);
  iso = new vtkContourFilter();
  iso.SetInputConnection((vtkAlgorithmOutput)v16.GetOutputPort());
  iso.GenerateValues((int)6,(double)500,(double)1150);
  iso.Update();
  numPts = iso.GetOutput().GetNumberOfPoints();
  isoMapper = vtkPolyDataMapper.New();
  isoMapper.SetInputConnection((vtkAlgorithmOutput)iso.GetOutputPort());
  isoMapper.ScalarVisibilityOn();
  isoMapper.SetScalarRange((double)((vtkDataSet)iso.GetOutput()).GetScalarRange()[0],(double)((vtkDataSet)iso.GetOutput()).GetScalarRange()[1]);
  isoActor = new vtkActor();
  isoActor.SetMapper((vtkMapper)isoMapper);
  // Subsample the points and label them[]
  mask = new vtkMaskPoints();
  mask.SetInputConnection((vtkAlgorithmOutput)iso.GetOutputPort());
  mask.SetOnRatio((int)(numPts/50));
  mask.SetMaximumNumberOfPoints((int)50);
  mask.RandomModeOn();
  // Create labels for points - only show visible points[]
  visPts = new vtkSelectVisiblePoints();
  visPts.SetInputConnection((vtkAlgorithmOutput)mask.GetOutputPort());
  visPts.SetRenderer((vtkRenderer)ren1);
  ldm = new vtkLabeledDataMapper();
  ldm.SetInputConnection((vtkAlgorithmOutput)mask.GetOutputPort());
  //    ldm SetLabelFormat "%g"[]
  ldm.SetLabelModeToLabelScalars();
  tprop = ldm.GetLabelTextProperty();
  tprop.SetFontFamilyToArial();
  tprop.SetFontSize((int)10);
  tprop.SetColor((double)1,(double)0,(double)0);
  contourLabels = new vtkActor2D();
  contourLabels.SetMapper((vtkMapper2D)ldm);
  // Add the actors to the renderer, set the background and size[]
  //[]
  ren1.AddActor2D((vtkProp)isoActor);
  ren1.AddActor2D((vtkProp)contourLabels);
  ren1.SetBackground((double)1,(double)1,(double)1);
  renWin.SetSize((int)500,(int)500);
  renWin.Render();
  ren1.GetActiveCamera().Zoom((double)1.5);
  // render the image[]
  //[]
  // prevent the tk window from showing up then start the event loop[]
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkVolume16Reader v16;
static vtkContourFilter iso;
static long numPts;
static vtkPolyDataMapper isoMapper;
static vtkActor isoActor;
static vtkMaskPoints mask;
static vtkSelectVisiblePoints visPts;
static vtkLabeledDataMapper ldm;
static vtkTextProperty tprop;
static vtkActor2D contourLabels;


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
        public static vtkVolume16Reader Getv16()
        {
            return v16;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setv16(vtkVolume16Reader toSet)
        {
            v16 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkContourFilter Getiso()
        {
            return iso;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setiso(vtkContourFilter toSet)
        {
            iso = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static long GetnumPts()
        {
            return numPts;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetnumPts(long toSet)
        {
            numPts = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetisoMapper()
        {
            return isoMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetisoMapper(vtkPolyDataMapper toSet)
        {
            isoMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetisoActor()
        {
            return isoActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetisoActor(vtkActor toSet)
        {
            isoActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkMaskPoints Getmask()
        {
            return mask;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setmask(vtkMaskPoints toSet)
        {
            mask = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkSelectVisiblePoints GetvisPts()
        {
            return visPts;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetvisPts(vtkSelectVisiblePoints toSet)
        {
            visPts = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkLabeledDataMapper Getldm()
        {
            return ldm;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setldm(vtkLabeledDataMapper toSet)
        {
            ldm = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTextProperty Gettprop()
        {
            return tprop;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settprop(vtkTextProperty toSet)
        {
            tprop = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor2D GetcontourLabels()
        {
            return contourLabels;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetcontourLabels(vtkActor2D toSet)
        {
            contourLabels = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(v16!= null){v16.Dispose();}
  	if(iso!= null){iso.Dispose();}
  	if(isoMapper!= null){isoMapper.Dispose();}
  	if(isoActor!= null){isoActor.Dispose();}
  	if(mask!= null){mask.Dispose();}
  	if(visPts!= null){visPts.Dispose();}
  	if(ldm!= null){ldm.Dispose();}
  	if(tprop!= null){tprop.Dispose();}
  	if(contourLabels!= null){contourLabels.Dispose();}
  }

}
//--- end of script --//

