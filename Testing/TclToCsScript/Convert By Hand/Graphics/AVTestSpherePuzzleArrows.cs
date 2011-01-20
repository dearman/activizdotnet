using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\TestSpherePuzzleArrows.tcl
// output file is AVTestSpherePuzzleArrows.cs
/// <summary>
/// The testing class derived from AVTestSpherePuzzleArrows
/// </summary>
public class AVTestSpherePuzzleArrowsClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVTestSpherePuzzleArrows(String [] argv)
  {
  //Prefix Content is: ""
  
  // prevent the tk window from showing up then start the event loop[]
  renWin = vtkRenderWindow.New();
  // create a rendering window and renderer[]
  ren1 = vtkRenderer.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  renWin.SetSize((int)400,(int)400);
  puzzle = new vtkSpherePuzzle();
  mapper = vtkPolyDataMapper.New();
  mapper.SetInputConnection((vtkAlgorithmOutput)puzzle.GetOutputPort());
  actor = new vtkActor();
  actor.SetMapper((vtkMapper)mapper);
  arrows = new vtkSpherePuzzleArrows();
  mapper2 = vtkPolyDataMapper.New();
  mapper2.SetInputConnection((vtkAlgorithmOutput)arrows.GetOutputPort());
  actor2 = new vtkActor();
  actor2.SetMapper((vtkMapper)mapper2);
  // Add the actors to the renderer, set the background and size[]
  //[]
  ren1.AddActor((vtkProp)actor);
  ren1.AddActor((vtkProp)actor2);
  ren1.SetBackground((double)0.1,(double)0.2,(double)0.4);
  LastVal = -1;
  //method moved
  //method moved
  renWin.Render();
  cam = ren1.GetActiveCamera();
  cam.Elevation((double)-40);
  ButtonCallback(261,272);
  arrows.SetPermutation((vtkSpherePuzzle)puzzle);
  renWin.Render();
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkRenderWindow renWin;
static vtkRenderer ren1;
static vtkSpherePuzzle puzzle;
static vtkPolyDataMapper mapper;
static vtkActor actor;
static vtkSpherePuzzleArrows arrows;
static vtkPolyDataMapper mapper2;
static vtkActor actor2;
static double LastVal;
static int WindowY;
static double y;
static double z;
static double[] pt;
static double x;
static double val;
static int i;
static vtkCamera cam;


  /// <summary>
  ///A process translated from the tcl scripts
  /// </summary>
  public static void MotionCallback(double x, double y)
    {
      //Global Variable Declaration Skipped
      // Compute display point from Tk display point.[]
      WindowY = 400;
      y = WindowY-y;
      z = ren1.GetZ((int)x,(int)y);
      ren1.SetDisplayPoint((double)x,(double)y,(double)z);
      ren1.DisplayToWorld();
      pt = ren1.GetWorldPoint();
      //tk_messageBox -message "$pt"[]
      x = (double)(lindex(pt,0));
      y = (double)(lindex(pt,1));
      z = (double)(lindex(pt,2));
      val = puzzle.SetPoint((double)x,(double)y,(double)z);
      if ((val) != LastVal)
        {
          renWin.Render();
          LastVal = val;
        }

      
    }

  /// <summary>
  ///A process translated from the tcl scripts
  /// </summary>
  public static void ButtonCallback(double x, double y)
    {
      // Compute display point from Tk display point.[]
      WindowY = 400;
      y = WindowY-y;
      z = ren1.GetZ((int)x,(int)y);
      ren1.SetDisplayPoint((double)x,(double)y,(double)z);
      ren1.DisplayToWorld();
      pt = ren1.GetWorldPoint();
      //tk_messageBox -message "$pt"[]
      x = (double)(lindex(pt,0));
      y = (double)(lindex(pt,1));
      z = (double)(lindex(pt,2));
      i = 0;
      while((i) <= 100)
        {
          puzzle.SetPoint((double)x,(double)y,(double)z);
          puzzle.MovePoint((int)i);
          renWin.Render();
          i = i+5;
        }

    }

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
            return (long)arr.GetIndex(index);
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
        public static vtkSpherePuzzle Getpuzzle()
        {
            return puzzle;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpuzzle(vtkSpherePuzzle toSet)
        {
            puzzle = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Getmapper()
        {
            return mapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setmapper(vtkPolyDataMapper toSet)
        {
            mapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getactor()
        {
            return actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setactor(vtkActor toSet)
        {
            actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkSpherePuzzleArrows Getarrows()
        {
            return arrows;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setarrows(vtkSpherePuzzleArrows toSet)
        {
            arrows = toSet;
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
        public static double GetLastVal()
        {
            return LastVal;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetLastVal(double toSet)
        {
            LastVal = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int GetWindowY()
        {
            return WindowY;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetWindowY(int toSet)
        {
            WindowY = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double Gety()
        {
            return y;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sety(double toSet)
        {
            y = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double Getz()
        {
            return z;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setz(double toSet)
        {
            z = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double[] Getpt()
        {
            return pt;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpt(double[] toSet)
        {
            pt = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double Getx()
        {
            return x;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setx(double toSet)
        {
            x = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double Getval()
        {
            return val;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setval(double toSet)
        {
            val = toSet;
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
  	if(renWin!= null){renWin.Dispose();}
  	if(ren1!= null){ren1.Dispose();}
  	if(puzzle!= null){puzzle.Dispose();}
  	if(mapper!= null){mapper.Dispose();}
  	if(actor!= null){actor.Dispose();}
  	if(arrows!= null){arrows.Dispose();}
  	if(mapper2!= null){mapper2.Dispose();}
  	if(actor2!= null){actor2.Dispose();}
  	if(cam!= null){cam.Dispose();}
  }

}
//--- end of script --//

