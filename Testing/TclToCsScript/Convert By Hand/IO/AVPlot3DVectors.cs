using Kitware.VTK;
using System;
// input file is C:\VTK\IO\Testing\Tcl\Plot3DVectors.tcl
// output file is AVPlot3DVectors.cs
/// <summary>
/// The testing class derived from AVPlot3DVectors
/// </summary>
public class AVPlot3DVectorsClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVPlot3DVectors(String [] argv)
  {
  //Prefix Content is: ""
  
  //[]
  // All Plot3D vector functions[]
  //[]
  // Create the RenderWindow, Renderer and both Actors[]
  //[]
  renWin = vtkRenderWindow.New();
  ren1 = vtkRenderer.New();
  ren1.SetBackground((double).8,(double).8,(double).2);
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  vectorLabels = "Velocity Vorticity Momentum Pressure_Gradient";
  vectorFunctions = "200 201 202 210";
  camera = new vtkCamera();
  light = new vtkLight();
  // All text actors will share the same text prop[]
  textProp = new vtkTextProperty();
  textProp.SetFontSize((int)10);
  textProp.SetFontFamilyToArial();
  textProp.SetColor((double).3,(double)1,(double)1);
  i = 0;
  foreach (string vectorFunction in vectorFunctions.Split(new char[]{' '}))
  {
      pl3d[getArrayIndex(vectorFunction)] = new vtkPLOT3DReader();
      pl3d[getArrayIndex(vectorFunction)].SetXYZFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/bluntfinxyz.bin");
      pl3d[getArrayIndex(vectorFunction)].SetQFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/bluntfinq.bin");
      pl3d[getArrayIndex(vectorFunction)].SetVectorFunctionNumber((int)(int)(getArrayIndex(vectorFunction)));
      pl3d[getArrayIndex(vectorFunction)].Update();
      plane[getArrayIndex(vectorFunction)] = new vtkStructuredGridGeometryFilter();
      plane[getArrayIndex(vectorFunction)].SetInputConnection((vtkAlgorithmOutput)pl3d[getArrayIndex(vectorFunction)].GetOutputPort());
      plane[getArrayIndex(vectorFunction)].SetExtent((int)25,(int)25,(int)0,(int)100,(int)0,(int)100);
      hog[getArrayIndex(vectorFunction)] = new vtkHedgeHog();
      hog[getArrayIndex(vectorFunction)].SetInputConnection((vtkAlgorithmOutput)plane[getArrayIndex(vectorFunction)].GetOutputPort());
      maxnorm = pl3d[getArrayIndex(vectorFunction)].GetOutput().GetPointData().GetVectors().GetMaxNorm();
      hog[getArrayIndex(vectorFunction)].SetScaleFactor((double)1.0/maxnorm);
      mapper[getArrayIndex(vectorFunction)] = vtkPolyDataMapper.New();
      mapper[getArrayIndex(vectorFunction)].SetInputConnection((vtkAlgorithmOutput)hog[getArrayIndex(vectorFunction)].GetOutputPort());
      actor[getArrayIndex(vectorFunction)] = new vtkActor();
      actor[getArrayIndex(vectorFunction)].SetMapper((vtkMapper)mapper[getArrayIndex(vectorFunction)]);
      ren[getArrayIndex(vectorFunction)] = vtkRenderer.New();
      ren[getArrayIndex(vectorFunction)].SetBackground((double)0.5,(double).5,(double).5);
      ren[getArrayIndex(vectorFunction)].SetActiveCamera((vtkCamera)camera);
      ren[getArrayIndex(vectorFunction)].AddLight((vtkLight)light);
      renWin.AddRenderer(ren[getArrayIndex(vectorFunction)]);
      ren[getArrayIndex(vectorFunction)].AddActor((vtkProp)actor[getArrayIndex(vectorFunction)]);
      textMapper[getArrayIndex(vectorFunction)] = new vtkTextMapper();
      textMapper[getArrayIndex(vectorFunction)].SetInput(vectorLabels.Split(new char[] { ' ' })[i]);
      textMapper[getArrayIndex(vectorFunction)].SetTextProperty((vtkTextProperty)textProp);
      text[getArrayIndex(vectorFunction)] = new vtkActor2D();
      text[getArrayIndex(vectorFunction)].SetMapper((vtkMapper2D)textMapper[getArrayIndex(vectorFunction)]);
      text[getArrayIndex(vectorFunction)].SetPosition((double)2,(double)5);

          ren[getArrayIndex(vectorFunction)].AddActor2D((vtkProp)text[getArrayIndex(vectorFunction)]);

      
      i = i + 1;

      }
  //[]
  // now layout renderers[]
  column = 1;
  row = 1;
  deltaX = 1.0/2.0;
  deltaY = 1.0/2.0;
  foreach (string vectorFunction in vectorFunctions.Split(new char[]{' '}))
  {
      ren[getArrayIndex(vectorFunction)].SetViewport((double)(column - 1) * deltaX + (deltaX * .05), (double)(row - 1) * deltaY + (deltaY * .05), (double)column * deltaX - (deltaX * .05), (double)row * deltaY - (deltaY * .05));
      column = column + 1;
      if ((column) > 2)
        {
          column = 1;
          row = row + 1;
        }

      

      }
  camera.SetViewUp((double)1,(double)0,(double)0);
  camera.SetFocalPoint((double)0,(double)0,(double)0);
  camera.SetPosition((double).4,(double)-.5,(double)-.75);
  ren[200].ResetCamera();
  camera.Dolly((double)1.25);
  ren[200].ResetCameraClippingRange();
  ren[201].ResetCameraClippingRange();
  ren[202].ResetCameraClippingRange();
  ren[210].ResetCameraClippingRange();
  light.SetPosition(camera.GetPosition()[0],camera.GetPosition()[1],camera.GetPosition()[2]);
  light.SetFocalPoint(camera.GetFocalPoint()[0],camera.GetFocalPoint()[1],camera.GetFocalPoint()[2]);
  renWin.SetSize(350,350);
  renWin.Render();
  iren.Initialize();
  // render the image[]
  //[]
  // prevent the tk window from showing up then start the event loop[]
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkRenderWindow renWin;
static vtkRenderer ren1;
static vtkRenderWindowInteractor iren;
static string vectorLabels;
static string vectorFunctions;
static vtkCamera camera;
static vtkLight light;
static vtkTextProperty textProp;
static int i;
static vtkPLOT3DReader[] pl3d = new vtkPLOT3DReader[230];
static vtkStructuredGridGeometryFilter[] plane = new vtkStructuredGridGeometryFilter[230];
static vtkHedgeHog[] hog = new vtkHedgeHog[230];
static double maxnorm;
static vtkPolyDataMapper[] mapper = new vtkPolyDataMapper[230];
static vtkActor[] actor = new vtkActor[230];
static vtkRenderer[] ren = new vtkRenderer[230];
static vtkTextMapper[] textMapper = new vtkTextMapper[230];
static vtkActor2D[] text = new vtkActor2D[230];
static int column;
static int row;
static double deltaX;
static double deltaY;

/// <summary>
/// hack to make this test work from tcl
/// </summary>
public static int getArrayIndex(string str)
{
    string[] arrFunc = vectorFunctions.Split(new char[] { ' ' });
    string[] arrLabel = vectorLabels.Split(new char[] { ' ' });
    for (int i = 0; i < arrLabel.Length; i++)
    {
        if (arrLabel[i].Equals(str))
        {
            return i;
        }
    }
    for (int i = 0; i < arrFunc.Length; i++)
    {
        if (arrFunc[i].Equals(str))
        {
            return System.Convert.ToInt32(str);
        }
    }
    return -1;
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
        public static string GetvectorLabels()
        {
            return vectorLabels;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetvectorLabels(string toSet)
        {
            vectorLabels = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static string GetvectorFunctions()
        {
            return vectorFunctions;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetvectorFunctions(string toSet)
        {
            vectorFunctions = toSet;
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
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkLight Getlight()
        {
            return light;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setlight(vtkLight toSet)
        {
            light = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTextProperty GettextProp()
        {
            return textProp;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SettextProp(vtkTextProperty toSet)
        {
            textProp = toSet;
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
        public static vtkPLOT3DReader[] Getpl3d()
        {
            return pl3d;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpl3d(vtkPLOT3DReader[] toSet)
        {
            pl3d = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter[] Getplane()
        {
            return plane;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setplane(vtkStructuredGridGeometryFilter[] toSet)
        {
            plane = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkHedgeHog[] Gethog()
        {
            return hog;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sethog(vtkHedgeHog[] toSet)
        {
            hog = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double Getmaxnorm()
        {
            return maxnorm;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setmaxnorm(double toSet)
        {
            maxnorm = toSet;
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
        public static vtkRenderer[] Getren()
        {
            return ren;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setren(vtkRenderer[] toSet)
        {
            ren = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTextMapper[] GettextMapper()
        {
            return textMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SettextMapper(vtkTextMapper[] toSet)
        {
            textMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor2D[] Gettext()
        {
            return text;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settext(vtkActor2D[] toSet)
        {
            text = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int Getcolumn()
        {
            return column;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcolumn(int toSet)
        {
            column = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int Getrow()
        {
            return row;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setrow(int toSet)
        {
            row = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double GetdeltaX()
        {
            return deltaX;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetdeltaX(double toSet)
        {
            deltaX = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double GetdeltaY()
        {
            return deltaY;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetdeltaY(double toSet)
        {
            deltaY = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(renWin!= null){renWin.Dispose();}
  	if(ren1!= null){ren1.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(camera!= null){camera.Dispose();}
  	if(light!= null){light.Dispose();}
  	if(textProp!= null){textProp.Dispose();}
  }

}
//--- end of script --//

