using Kitware.VTK;
using System;
// input file is C:\VTK\IO\Testing\Tcl\Plot3DScalars.tcl
// output file is AVPlot3DScalars.cs
/// <summary>
/// The testing class derived from AVPlot3DScalars
/// </summary>
public class AVPlot3DScalarsClass
{
    /// <summary>
    /// The main entry method called by the CSharp driver
    /// </summary>
    /// <param name="argv"></param>
    public static void AVPlot3DScalars(String[] argv)
    {
        //Prefix Content is: ""

        //[]
        // All Plot3D scalar functions[]
        //[]
        // Create the RenderWindow, Renderer and both Actors[]
        //[]
        renWin = vtkRenderWindow.New();
        iren = new vtkRenderWindowInteractor();
        iren.SetRenderWindow((vtkRenderWindow)renWin);
        scalarLabels = "Density Pressure Temperature Enthalpy Internal_Energy Kinetic_Energy Velocity_Magnitude Stagnation_Energy Entropy Swirl";
        scalarFunctions = "100 110 120 130 140 144 153 163 170 184";
        camera = new vtkCamera();
        light = new vtkLight();
        math = new vtkMath();
        // All text actors will share the same text prop[]
        textProp = new vtkTextProperty();
        textProp.SetFontSize((int)10);
        textProp.SetFontFamilyToArial();
        textProp.SetColor((double)0, (double)0, (double)0);
        i = 0;
        foreach (string scalarFunction in scalarFunctions.Split(new char[] { ' ' }))
        {
            pl3d[getArrayIndex(scalarFunction)] = new vtkPLOT3DReader();
            pl3d[getArrayIndex(scalarFunction)].SetXYZFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/bluntfinxyz.bin");
            pl3d[getArrayIndex(scalarFunction)].SetQFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/bluntfinq.bin");
            pl3d[getArrayIndex(scalarFunction)].SetScalarFunctionNumber((int)(int)(Int32.Parse(scalarFunction)));
            pl3d[getArrayIndex(scalarFunction)].Update();

            plane[getArrayIndex(scalarFunction)] = new vtkStructuredGridGeometryFilter();
            plane[getArrayIndex(scalarFunction)].SetInputConnection((vtkAlgorithmOutput)pl3d[getArrayIndex(scalarFunction)].GetOutputPort());
            plane[getArrayIndex(scalarFunction)].SetExtent((int)25, (int)25, (int)0, (int)100, (int)0, (int)100);

            mapper[getArrayIndex(scalarFunction)] = vtkPolyDataMapper.New();
            mapper[getArrayIndex(scalarFunction)].SetInputConnection((vtkAlgorithmOutput)plane[getArrayIndex(scalarFunction)].GetOutputPort());
            mapper[getArrayIndex(scalarFunction)].SetScalarRange((double)((vtkDataSet)pl3d[getArrayIndex(scalarFunction)].GetOutput()).GetPointData().GetScalars().GetRange()[0],
                (double)((vtkDataSet)pl3d[getArrayIndex(scalarFunction)].GetOutput()).GetPointData().GetScalars().GetRange()[1]);

            actor[getArrayIndex(scalarFunction)] = new vtkActor();
            actor[getArrayIndex(scalarFunction)].SetMapper((vtkMapper)mapper[getArrayIndex(scalarFunction)]);
            ren[getArrayIndex(scalarFunction)] = vtkRenderer.New();
            ren[getArrayIndex(scalarFunction)].SetBackground((double)0, (double)0, (double).5);
            ren[getArrayIndex(scalarFunction)].SetActiveCamera((vtkCamera)camera);
            ren[getArrayIndex(scalarFunction)].AddLight((vtkLight)light);
            renWin.AddRenderer(ren[getArrayIndex(scalarFunction)]);
            ren[getArrayIndex(scalarFunction)].SetBackground((double)vtkMath.Random((double).5, (double)1), (double)vtkMath.Random((double).5, (double)1), (double)vtkMath.Random((double).5, (double)1));
            ren[getArrayIndex(scalarFunction)].AddActor((vtkProp)actor[getArrayIndex(scalarFunction)]);

            textMapper[getArrayIndex(scalarFunction)] = new vtkTextMapper();
            textMapper[getArrayIndex(scalarFunction)].SetInput(scalarLabels.Split(new char[] { ' ' })[i]);
            textMapper[getArrayIndex(scalarFunction)].SetTextProperty((vtkTextProperty)textProp);

            text[getArrayIndex(scalarFunction)] = new vtkActor2D();
            text[getArrayIndex(scalarFunction)].SetMapper((vtkMapper2D)textMapper[getArrayIndex(scalarFunction)]);
            text[getArrayIndex(scalarFunction)].SetPosition((double)2, (double)3);

            ren[getArrayIndex(scalarFunction)].AddActor2D(text[getArrayIndex(scalarFunction)]);


            i = i + 1;

        }
        //[]
        // now layout renderers[]
        column = 1;
        row = 1;
        deltaX = 1.0 / 5.0;
        deltaY = 1.0 / 2.0;
        foreach (string scalarFunction in scalarFunctions.Split(new char[] { ' ' }))
        {
            ren[getArrayIndex(scalarFunction)].SetViewport((double)(column - 1) * deltaX, (double)(row - 1) * deltaY, (double)column * deltaX, (double)row * deltaY);
            column = column + 1;
            if ((column) > 5)
            {
                column = 1;
                row = row + 1;
            }
        }
        camera.SetViewUp((double)0, (double)1, (double)0);
        camera.SetFocalPoint((double)0, (double)0, (double)0);
        camera.SetPosition((double)1, (double)0, (double)0);
        ren[100].ResetCamera();
        camera.Dolly((double)1.25);
        ren[100].ResetCameraClippingRange();
        ren[110].ResetCameraClippingRange();
        ren[120].ResetCameraClippingRange();
        ren[130].ResetCameraClippingRange();
        ren[140].ResetCameraClippingRange();
        ren[144].ResetCameraClippingRange();
        ren[153].ResetCameraClippingRange();
        ren[163].ResetCameraClippingRange();
        ren[170].ResetCameraClippingRange();
        ren[184].ResetCameraClippingRange();

        light.SetPosition(camera.GetPosition()[0], camera.GetPosition()[1], camera.GetPosition()[2]);
        light.SetFocalPoint(camera.GetFocalPoint()[0], camera.GetFocalPoint()[1], camera.GetFocalPoint()[2]);

        renWin.SetSize(600, 180);
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
    static vtkRenderWindowInteractor iren;
    static string scalarLabels;
    static string scalarFunctions;
    static vtkCamera camera;
    static vtkLight light;
    static vtkMath math;
    static vtkTextProperty textProp;
    static int i;
    static vtkPLOT3DReader[] pl3d = new vtkPLOT3DReader[200];
    static vtkStructuredGridGeometryFilter[] plane = new vtkStructuredGridGeometryFilter[200];
    static vtkPolyDataMapper[] mapper = new vtkPolyDataMapper[200];
    static vtkActor[] actor = new vtkActor[200];
    static vtkRenderer[] ren = new vtkRenderer[200];
    static vtkTextMapper[] textMapper = new vtkTextMapper[200];
    static vtkActor2D[] text = new vtkActor2D[200];
    static int column;
    static int row;
    static double deltaX;
    static double deltaY;

    /// <summary>
    /// hack to make this test work from tcl
    /// </summary>
    public static int getArrayIndex(string str)
    {
        string[] arrFunc = scalarFunctions.Split(new char[] { ' ' });
        string[] arrLabel = scalarLabels.Split(new char[] { ' ' });
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
        string[] str = arr.Split(new char[] { ' ' });
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
    public static string GetscalarLabels()
    {
        return scalarLabels;
    }

    ///<summary> A Set Method for Static Variables </summary>
    public static void SetscalarLabels(string toSet)
    {
        scalarLabels = toSet;
    }

    ///<summary> A Get Method for Static Variables </summary>
    public static string GetscalarFunctions()
    {
        return scalarFunctions;
    }

    ///<summary> A Set Method for Static Variables </summary>
    public static void SetscalarFunctions(string toSet)
    {
        scalarFunctions = toSet;
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
    public static vtkMath Getmath()
    {
        return math;
    }

    ///<summary> A Set Method for Static Variables </summary>
    public static void Setmath(vtkMath toSet)
    {
        math = toSet;
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
        if (renWin != null) { renWin.Dispose(); }
        if (iren != null) { iren.Dispose(); }
        if (camera != null) { camera.Dispose(); }
        if (light != null) { light.Dispose(); }
        if (math != null) { math.Dispose(); }
        if (textProp != null) { textProp.Dispose(); }
        for (int i = 0; i < 200; i++)
        {
            /*if (pl3d[i] != null)
            {
                pl3d[i].Dispose();
            }
            if (lane[i] != null)
            {
                lane[i].Dispose();
            }*/
            if (mapper[i] != null)
            {
                mapper[i].Dispose();
            }
            if (actor[i] != null)
            {
                actor[i].Dispose();
            }
            if (ren[i] != null)
            {
                ren[i].Dispose();
            }
            if (textMapper[i] != null)
            {
                textMapper[i].Dispose();
            }
            if (text[i] != null)
            {
                text[i].Dispose();
            }
        }
    }

}
//--- end of script --//

