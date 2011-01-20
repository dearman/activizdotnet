using Kitware.VTK;
using System;
// input file is C:\VTK\Parallel\Testing\Tcl\TestCutMaterial.tcl
// output file is AVTestCutMaterial.cs
/// <summary>
/// The testing class derived from AVTestCutMaterial
/// </summary>
public class AVTestCutMaterialClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVTestCutMaterial(String [] argv)
  {
  //Prefix Content is: ""
  
  // Lets create a data set.[]
  data = new vtkImageData();
  data.SetExtent((int)0,(int)31,(int)0,(int)31,(int)0,(int)31);
  data.SetScalarTypeToFloat();
  // First the data array:[]
  gauss = new vtkImageGaussianSource();
  gauss.SetWholeExtent((int)0,(int)30,(int)0,(int)30,(int)0,(int)30);
  gauss.SetCenter((double)18,(double)12,(double)20);
  gauss.SetMaximum((double)1.0);
  gauss.SetStandardDeviation((double)10.0);
  gauss.Update();
  a = gauss.GetOutput().GetPointData().GetScalars();
  a.SetName((string)"Gauss");
  data.GetCellData().SetScalars((vtkDataArray)a);
  //skipping Delete gauss
  // Now the material array:[]
  ellipse = new vtkImageEllipsoidSource();
  ellipse.SetWholeExtent((int)0,(int)30,(int)0,(int)30,(int)0,(int)30);
  ellipse.SetCenter((double)11,(double)12,(double)13);
  ellipse.SetRadius((double)5,(double)9,(double)13);
  ellipse.SetInValue((double)1);
  ellipse.SetOutValue((double)0);
  ellipse.SetOutputScalarTypeToInt();
  ellipse.Update();
  m = ellipse.GetOutput().GetPointData().GetScalars();
  m.SetName((string)"Material");
  data.GetCellData().AddArray((vtkAbstractArray)m);
  //skipping Delete ellipse
  cut = new vtkCutMaterial();
  cut.SetInput((vtkDataObject)data);
  cut.SetMaterialArrayName((string)"Material");
  cut.SetMaterial((int)1);
  cut.SetArrayName((string)"Gauss");
  cut.SetUpVector((double)1,(double)0,(double)0);
  cut.Update();
  mapper2 = vtkPolyDataMapper.New();
  mapper2.SetInputConnection((vtkAlgorithmOutput)cut.GetOutputPort());
  mapper2.SetScalarRange((double)0,(double)1);
  //apper2 SetScalarModeToUseCellFieldData[]
  //apper2 SetColorModeToMapScalars []
  //apper2 ColorByArrayComponent "vtkGhostLevels" 0[]
  actor2 = new vtkActor();
  actor2.SetMapper((vtkMapper)mapper2);
  actor2.SetPosition((double)1.5,(double)0,(double)0);
  ren = vtkRenderer.New();
  ren.AddActor((vtkProp)actor2);
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren);
  p = cut.GetCenterPoint();
  n = cut.GetNormal();
  cam = ren.GetActiveCamera();
  cam.SetFocalPoint(p[0],p[1],p[2]);
  cam.SetViewUp(cut.GetUpVector()[0], cut.GetUpVector()[1], cut.GetUpVector()[2]);
  cam.SetPosition(
      (double)(lindex(n,0))+(double)(lindex(p,0)),
      (double)(lindex(n,1))+(double)(lindex(p,1)),
      (double)(lindex(n,2))+(double)(lindex(p,2)));
  ren.ResetCamera();
  iren = vtkRenderWindowInteractor.New();
  iren.SetRenderWindow(renWin);
  iren.Initialize();
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkImageData data;
static vtkImageGaussianSource gauss;
static vtkDataArray a;
static vtkImageEllipsoidSource ellipse;
static vtkDataArray m;
static vtkCutMaterial cut;
static vtkPolyDataMapper mapper2;
static vtkActor actor2;
static vtkRenderer ren;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static double[] p;
static double[] n;
static vtkCamera cam;


        /// <summary>
        /// Returns the variable in the index [i] of the System.Array [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="i"></param>   
        public static Object lindex(System.Array arr, int i)
        {
            return arr.GetValue(i);
        }

        /// <summary>
        /// Returns the variable in the index [index] of the array [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="index"></param>   
        public static double lindex(IntPtr arr, int index)
        {
            double[] destination = new double[index + 1];
            System.Runtime.InteropServices.Marshal.Copy(arr, destination, 0, index + 1);
            return destination[index];
        }

        /// <summary>
        /// Returns the variable in the index [index] of the vtkLookupTable [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="index"></param>
        public static long lindex(vtkLookupTable arr, double index)
        {
            return arr.GetIndex(index);
        }

        /// <summary>
        /// Returns the substring ([index], [index]+1) in the string [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="index"></param>
        public static int lindex(String arr, int index)
        {
           string[] str = arr.Split(new char[]{' '});      
           return System.Int32.Parse(str[index]);
        }

        /// <summary>
        /// Returns the index [index] in the int array [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="index"></param>
        public static int lindex(int[] arr, int index)
        {
          return arr[index];
        }

        /// <summary>
        /// Returns the index [index] in the float array [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="index"></param>
        public static float lindex(float[] arr, int index)
        {
          return arr[index];
        }

        /// <summary>
        /// Returns the index [index] in the double array [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="index"></param>
        public static double lindex(double[] arr, int index)
        {
          return arr[index];
        }

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
        public static vtkImageData Getdata()
        {
            return data;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setdata(vtkImageData toSet)
        {
            data = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageGaussianSource Getgauss()
        {
            return gauss;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setgauss(vtkImageGaussianSource toSet)
        {
            gauss = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataArray Geta()
        {
            return a;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Seta(vtkDataArray toSet)
        {
            a = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageEllipsoidSource Getellipse()
        {
            return ellipse;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setellipse(vtkImageEllipsoidSource toSet)
        {
            ellipse = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataArray Getm()
        {
            return m;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setm(vtkDataArray toSet)
        {
            m = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCutMaterial Getcut()
        {
            return cut;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcut(vtkCutMaterial toSet)
        {
            cut = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Getmapper2()
        {
            return mapper2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setmapper2(vtkPolyDataMapper toSet)
        {
            mapper2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getactor2()
        {
            return actor2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setactor2(vtkActor toSet)
        {
            actor2 = toSet;
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
        public static double[] Getp()
        {
            return p;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setp(double[] toSet)
        {
            p = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double[] Getn()
        {
            return n;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setn(double[] toSet)
        {
            n = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCamera Getcam()
        {
            return cam;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcam(vtkCamera toSet)
        {
            cam = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(data!= null){data.Dispose();}
  	if(gauss!= null){gauss.Dispose();}
  	if(a!= null){a.Dispose();}
  	if(ellipse!= null){ellipse.Dispose();}
  	if(m!= null){m.Dispose();}
  	if(cut!= null){cut.Dispose();}
  	if(mapper2!= null){mapper2.Dispose();}
  	if(actor2!= null){actor2.Dispose();}
  	if(ren!= null){ren.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
    if (iren != null) { iren.Dispose(); }
  }

}
//--- end of script --//

