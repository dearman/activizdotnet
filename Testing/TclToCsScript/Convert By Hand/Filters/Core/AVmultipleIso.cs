using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\multipleIso.tcl
// output file is AVmultipleIso.cs
/// <summary>
/// The testing class derived from AVmultipleIso
/// </summary>
public class AVmultipleIsoClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVmultipleIso(String [] argv)
  {
  //Prefix Content is: ""
  
  // get the interactor ui[]
  //# Graphics stuff[]
  // Create the RenderWindow, Renderer and both Actors[]
  //[]
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  pl3d = new vtkMultiBlockPLOT3DReader();
  pl3d.SetXYZFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/combxyz.bin");
  pl3d.SetQFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/combq.bin");
  pl3d.SetScalarFunctionNumber((int)100);
  pl3d.SetVectorFunctionNumber((int)202);
  pl3d.Update();
  range = ((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetPointData().GetScalars().GetRange();
  min = (double)(lindex(range,0));
  max = (double)(lindex(range,1));
  value = (min+max)/2.0;
        
  cf = new vtkContourFilter();
  cf.SetInputData((vtkDataSet)pl3d.GetOutput().GetBlock(0));
  cf.SetValue((int)0,(double)value);
  cf.UseScalarTreeOn();
  numberOfContours = 5;
  epsilon = (double)(max-min)/(double)(numberOfContours*10);
  min = min+epsilon;
  max = max-epsilon;
  i = 1;
  while((i) <= numberOfContours)
    {
      cf.SetValue((int)0,(double)min+((i-1)/(double)(numberOfContours-1))*(max-min));
      cf.Update();
      pd[i] = new vtkPolyData();
      pd[i].CopyStructure((vtkDataSet)cf.GetOutput());
      pd[i].GetPointData().DeepCopy((vtkFieldData)cf.GetOutput().GetPointData());
      mapper[i] = vtkPolyDataMapper.New();
      mapper[i].SetInputData((vtkPolyData)pd[i]);
      mapper[i].SetScalarRange((double)((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetPointData().GetScalars().GetRange()[0],
          (double)((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetPointData().GetScalars().GetRange()[1]);
      actor[i] = new vtkActor();
      actor[i].AddPosition((double)0,(double)i*12,(double)0);
      actor[i].SetMapper((vtkMapper)mapper[i]);
      ren1.AddActor((vtkProp)actor[i]);
      i = i + 1;
    }

  // Add the actors to the renderer, set the background and size[]
  //[]
  ren1.SetBackground((double).3,(double).3,(double).3);
  renWin.SetSize((int)450,(int)150);
  cam1 = ren1.GetActiveCamera();
  ren1.GetActiveCamera().SetPosition((double)-36.3762,(double)32.3855,(double)51.3652);
  ren1.GetActiveCamera().SetFocalPoint((double)8.255,(double)33.3861,(double)29.7687);
  ren1.GetActiveCamera().SetViewAngle((double)30);
  ren1.GetActiveCamera().SetViewUp((double)0,(double)0,(double)1);
  ren1.ResetCameraClippingRange();
  iren.Initialize();
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
static vtkMultiBlockPLOT3DReader pl3d;
static double[] range;
static double min;
static double max;
static double value;
static vtkContourFilter cf;
static int numberOfContours;
static double epsilon;
static int i;
static vtkPolyData[] pd = new vtkPolyData[100];
static vtkPolyDataMapper[] mapper = new vtkPolyDataMapper[100];
static vtkActor[] actor = new vtkActor[100];
static vtkCamera cam1;


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
        public static vtkMultiBlockPLOT3DReader Getpl3d()
        {
            return pl3d;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpl3d(vtkMultiBlockPLOT3DReader toSet)
        {
            pl3d = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double[] Getrange()
        {
            return range;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setrange(double[] toSet)
        {
            range = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double Getmin()
        {
            return min;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setmin(double toSet)
        {
            min = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double Getmax()
        {
            return max;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setmax(double toSet)
        {
            max = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double Getvalue()
        {
            return value;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setvalue(double toSet)
        {
            value = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkContourFilter Getcf()
        {
            return cf;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcf(vtkContourFilter toSet)
        {
            cf = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int GetnumberOfContours()
        {
            return numberOfContours;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetnumberOfContours(int toSet)
        {
            numberOfContours = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double Getepsilon()
        {
            return epsilon;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setepsilon(double toSet)
        {
            epsilon = toSet;
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
        public static vtkPolyData[] Getpd()
        {
            return pd;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpd(vtkPolyData[] toSet)
        {
            pd = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper[] Getmapper()
        {
            return mapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setmapper(vtkPolyDataMapper[] toSet)
        {
            mapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor[] Getactor()
        {
            return actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setactor(vtkActor[] toSet)
        {
            actor = toSet;
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
  	if(cf!= null){cf.Dispose();}
  	if(cam1!= null){cam1.Dispose();}
  }

}
//--- end of script --//

