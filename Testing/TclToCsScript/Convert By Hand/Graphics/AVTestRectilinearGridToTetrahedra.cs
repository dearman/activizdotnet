using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\TestRectilinearGridToTetrahedra.tcl
// output file is AVTestRectilinearGridToTetrahedra.cs
/// <summary>
/// The testing class derived from AVTestRectilinearGridToTetrahedra
/// </summary>
public class AVTestRectilinearGridToTetrahedraClass
{
    /// <summary>
    /// The main entry method called by the CSharp driver
    /// </summary>
    /// <param name="argv"></param>
    public static void AVTestRectilinearGridToTetrahedra(String[] argv)
    {
        //Prefix Content is: ""

        //## SetUp the pipeline[]
        FormMesh = new vtkRectilinearGridToTetrahedra();
        FormMesh.SetInput((double)4, (double)2, (double)2, (double)1, (double)1, (double)1, (double)0.001);
        FormMesh.RememberVoxelIdOn();
        TetraEdges = new vtkExtractEdges();
        TetraEdges.SetInputConnection((vtkAlgorithmOutput)FormMesh.GetOutputPort());
        tubes = new vtkTubeFilter();
        tubes.SetInputConnection((vtkAlgorithmOutput)TetraEdges.GetOutputPort());
        tubes.SetRadius((double)0.05);
        tubes.SetNumberOfSides((int)6);
        //## Run the pipeline 3 times, with different conversions to TetMesh[]
        Tubes[0] = new vtkPolyData();
        FormMesh.SetTetraPerCellTo5();
        tubes.Update();
        Tubes[0].DeepCopy((vtkDataObject)tubes.GetOutput());
        Tubes[1] = new vtkPolyData();
        FormMesh.SetTetraPerCellTo6();
        tubes.Update();
        Tubes[1].DeepCopy((vtkDataObject)tubes.GetOutput());
        Tubes[2] = new vtkPolyData();
        FormMesh.SetTetraPerCellTo12();
        tubes.Update();
        Tubes[2].DeepCopy((vtkDataObject)tubes.GetOutput());
        //## Run the pipeline once more, this time converting some cells to[]
        //## 5 and some data to 12 TetMesh[]
        //## Determine which cells are which[]
        DivTypes = new vtkIntArray();
        numCell = (long)((vtkDataSet)FormMesh.GetInput()).GetNumberOfCells();
        DivTypes.SetNumberOfValues((int)numCell);
        i = 0;
        while ((i) < numCell)
        {
            DivTypes.SetValue((int)i, (int)5 + (7 * (i % 4)));
            i = i + 1;
        }

        //## Finish this pipeline[]
        Tubes[3] = new vtkPolyData();
        FormMesh.SetTetraPerCellTo5And12();
        ((vtkRectilinearGrid)FormMesh.GetInput()).GetCellData().SetScalars(DivTypes);
        tubes.Update();
        Tubes[3].DeepCopy((vtkDataObject)tubes.GetOutput());
        //## Finish the 4 pipelines[]
        i = 1;
        while ((i) < 5)
        {
            mapEdges[i] = vtkPolyDataMapper.New();
            mapEdges[i].SetInput((vtkPolyData)Tubes[i - 1]);
            edgeActor[i] = new vtkActor();
            edgeActor[i].SetMapper((vtkMapper)mapEdges[i]);
            edgeActor[i].GetProperty().SetColor((double)0.2000, 0.6300, 0.7900);
            edgeActor[i].GetProperty().SetSpecularColor((double)1, (double)1, (double)1);
            edgeActor[i].GetProperty().SetSpecular((double)0.3);
            edgeActor[i].GetProperty().SetSpecularPower((double)20);
            edgeActor[i].GetProperty().SetAmbient((double)0.2);
            edgeActor[i].GetProperty().SetDiffuse((double)0.8);
            ren[i] = vtkRenderer.New();
            ren[i].AddActor((vtkProp)edgeActor[i]);
            ren[i].SetBackground((double)0, (double)0, (double)0);
            ren[i].ResetCamera();
            ren[i].GetActiveCamera().Zoom((double)1);
            ren[i].GetActiveCamera().SetPosition((double)1.73906, (double)12.7987, (double)-0.257808);
            ren[i].GetActiveCamera().SetViewUp((double)0.992444, (double)0.00890284, (double)-0.122379);
            ren[i].GetActiveCamera().SetClippingRange((double)9.36398, (double)15.0496);
            i = i + 1;
        }

        // Create graphics objects[]
        // Create the rendering window, renderer, and interactive renderer[]
        renWin = vtkRenderWindow.New();
        renWin.AddRenderer(ren[1]);
        renWin.AddRenderer(ren[2]);
        renWin.AddRenderer(ren[3]);
        renWin.AddRenderer(ren[4]);
        renWin.SetSize(600, 300);
        iren = new vtkRenderWindowInteractor();
        iren.SetRenderWindow((vtkRenderWindow)renWin);
        // Add the actors to the renderer, set the background and size[]
        ren[1].SetViewport((double).75, (double)0, (double)1, (double)1);
        ren[2].SetViewport((double).50, (double)0, (double).75, (double)1);
        ren[3].SetViewport((double).25, (double)0, (double).50, (double)1);
        ren[4].SetViewport((double)0, (double)0, (double).25, (double)1);
        // render the image[]
        //[]
        iren.Initialize();
        // prevent the tk window from showing up then start the event loop[]

        //deleteAllVTKObjects();
    }
    static string VTK_DATA_ROOT;
    static int threshold;
    static vtkRectilinearGridToTetrahedra FormMesh;
    static vtkExtractEdges TetraEdges;
    static vtkTubeFilter tubes;
    static vtkPolyData[] Tubes = new vtkPolyData[4];
    static vtkIntArray DivTypes;
    static long numCell;
    static int i;
    static vtkPolyDataMapper[] mapEdges = new vtkPolyDataMapper[100];
    static vtkActor[] edgeActor = new vtkActor[100];
    static vtkRenderer[] ren = new vtkRenderer[100];
    static vtkRenderWindow renWin;
    static vtkRenderWindowInteractor iren;

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
    public static vtkRectilinearGridToTetrahedra GetFormMesh()
    {
        return FormMesh;
    }

    ///<summary> A Set Method for Static Variables </summary>
    public static void SetFormMesh(vtkRectilinearGridToTetrahedra toSet)
    {
        FormMesh = toSet;
    }

    ///<summary> A Get Method for Static Variables </summary>
    public static vtkExtractEdges GetTetraEdges()
    {
        return TetraEdges;
    }

    ///<summary> A Set Method for Static Variables </summary>
    public static void SetTetraEdges(vtkExtractEdges toSet)
    {
        TetraEdges = toSet;
    }

    ///<summary> A Get Method for Static Variables </summary>
    public static vtkTubeFilter Gettubes()
    {
        return tubes;
    }

    ///<summary> A Set Method for Static Variables </summary>
    public static void Settubes(vtkTubeFilter toSet)
    {
        tubes = toSet;
    }

    ///<summary> A Get Method for Static Variables </summary>
    public static vtkPolyData[] GetTubes()
    {
        return Tubes;
    }

    ///<summary> A Set Method for Static Variables </summary>
    public static void SetTubes(vtkPolyData[] toSet)
    {
        Tubes = toSet;
    }

    ///<summary> A Get Method for Static Variables </summary>
    public static vtkIntArray GetDivTypes()
    {
        return DivTypes;
    }

    ///<summary> A Set Method for Static Variables </summary>
    public static void SetDivTypes(vtkIntArray toSet)
    {
        DivTypes = toSet;
    }

    ///<summary> A Get Method for Static Variables </summary>
    public static long GetnumCell()
    {
        return numCell;
    }

    ///<summary> A Set Method for Static Variables </summary>
    public static void SetnumCell(int toSet)
    {
        numCell = toSet;
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
    public static vtkPolyDataMapper[] GetmapEdges()
    {
        return mapEdges;
    }

    ///<summary> A Set Method for Static Variables </summary>
    public static void SetmapEdges(vtkPolyDataMapper[] toSet)
    {
        mapEdges = toSet;
    }

    ///<summary> A Get Method for Static Variables </summary>
    public static vtkActor[] GetedgeActor()
    {
        return edgeActor;
    }

    ///<summary> A Set Method for Static Variables </summary>
    public static void SetedgeActor(vtkActor[] toSet)
    {
        edgeActor = toSet;
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
    public static vtkRenderWindow GetrenWin()
    {
      return renWin;
    }

    ///<summary> A Get Method for Static Variables </summary>
    public static vtkRenderWindowInteractor Getiren()
    {
      return iren;
    }

    ///<summary>Deletes all static objects created</summary>
    public static void deleteAllVTKObjects()
    {
        //clean up vtk objects
        if (renWin != null) { renWin.Dispose(); }
        if (iren != null) { iren.Dispose(); }
        if (FormMesh != null) { FormMesh.Dispose(); }
        if (TetraEdges != null) { TetraEdges.Dispose(); }
        if (tubes != null) { tubes.Dispose(); }
        if (Tubes[0] != null) { Tubes[0].Dispose(); }
        if (Tubes[1] != null) { Tubes[1].Dispose(); }
        if (Tubes[2] != null) { Tubes[2].Dispose(); }
        if (Tubes[3] != null) { Tubes[3].Dispose(); }
        if (DivTypes != null) { DivTypes.Dispose(); }
        for (int i = 0; i < 100; i++)
        {
            if (mapEdges[i] != null)
            {
                mapEdges[i].Dispose();
            }
            if (edgeActor[i] != null)
            {
                edgeActor[i].Dispose();
            }
            if (ren[i] != null)
            {
                ren[i].Dispose();
            }
        }

    }

}
//--- end of script --//

