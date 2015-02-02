using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\TestGridSynchronizedTemplates3D.tcl
// output file is AVTestGridSynchronizedTemplates3D.cs
/// <summary>
/// The testing class derived from AVTestGridSynchronizedTemplates3D
/// </summary>
public class AVTestGridSynchronizedTemplates3DClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVTestGridSynchronizedTemplates3D(String [] argv)
  {
  //Prefix Content is: ""
  
  // cut data[]
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
  //vtkGridSynchronizedTemplates3D cf[]
  cf = new vtkContourFilter();
  cf.SetInputData((vtkDataSet)pl3d.GetOutput().GetBlock(0));
  cf.SetValue((int)0,(double)value);
  //cf ComputeNormalsOff[]
  cfMapper = vtkPolyDataMapper.New();
  cfMapper.ImmediateModeRenderingOn();
  cfMapper.SetInputConnection((vtkAlgorithmOutput)cf.GetOutputPort());
  cfMapper.SetScalarRange((double)((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetPointData().GetScalars().GetRange()[0], (double)((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetPointData().GetScalars().GetRange()[1]);
  cfActor = new vtkActor();
  cfActor.SetMapper((vtkMapper)cfMapper);
  //outline[]
  outline = new vtkStructuredGridOutlineFilter();
  outline.SetInputData((vtkDataSet)pl3d.GetOutput().GetBlock(0));
  outlineMapper = vtkPolyDataMapper.New();
  outlineMapper.SetInputConnection((vtkAlgorithmOutput)outline.GetOutputPort());
  outlineActor = new vtkActor();
  outlineActor.SetMapper((vtkMapper)outlineMapper);
  outlineActor.GetProperty().SetColor((double)0,(double)0,(double)0);
  //# Graphics stuff[]
  // Create the RenderWindow, Renderer and both Actors[]
  //[]
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.SetMultiSamples(0);
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  // Add the actors to the renderer, set the background and size[]
  //[]
  ren1.AddActor((vtkProp)outlineActor);
  ren1.AddActor((vtkProp)cfActor);
  ren1.SetBackground((double)1,(double)1,(double)1);
  renWin.SetSize((int)400,(int)400);
  cam1 = ren1.GetActiveCamera();
  cam1.SetClippingRange((double)3.95297,(double)50);
  cam1.SetFocalPoint((double)9.71821,(double)0.458166,(double)29.3999);
  cam1.SetPosition((double)2.7439,(double)-37.3196,(double)38.7167);
  cam1.SetViewUp((double)-0.16123,(double)0.264271,(double)0.950876);
  iren.Initialize();
  // render the image[]
  //[]
  // loop over surfaces[]
  i = 0;
  while((i) < 17)
    {
      cf.SetValue((int)0,(double)min+(i/16.0)*(max-min));
      renWin.Render();
      i = i + 1;
    }

  cf.SetValue((int)0,(double)min+(0.2)*(max-min));
  renWin.Render();
  // prevent the tk window from showing up then start the event loop[]
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkMultiBlockPLOT3DReader pl3d;
static double[] range;
static double min;
static double max;
static double value;
static vtkContourFilter cf;
static vtkPolyDataMapper cfMapper;
static vtkActor cfActor;
static vtkStructuredGridOutlineFilter outline;
static vtkPolyDataMapper outlineMapper;
static vtkActor outlineActor;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkCamera cam1;
static int i;


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
        public static vtkPolyDataMapper GetcfMapper()
        {
            return cfMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetcfMapper(vtkPolyDataMapper toSet)
        {
            cfMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetcfActor()
        {
            return cfActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetcfActor(vtkActor toSet)
        {
            cfActor = toSet;
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
        public static int Geti()
        {
            return i;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Seti(int toSet)
        {
            i = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(pl3d!= null){pl3d.Dispose();}
  	if(cf!= null){cf.Dispose();}
  	if(cfMapper!= null){cfMapper.Dispose();}
  	if(cfActor!= null){cfActor.Dispose();}
  	if(outline!= null){outline.Dispose();}
  	if(outlineMapper!= null){outlineMapper.Dispose();}
  	if(outlineActor!= null){outlineActor.Dispose();}
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(cam1!= null){cam1.Dispose();}
  }

}
//--- end of script --//

