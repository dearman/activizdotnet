using Kitware.VTK;
using System;

/// <summary>
/// Class with a Main Method
/// </summary>
public class DelMeshClass
{
    /// <summary>
    /// Entry Point
    /// </summary>
    /// <param name="argv"></param>
    public static void Main(String[] argv)
    {
        // This example demonstrates how to use 2D Delaunay triangulation.
        // We create a fancy image of a 2D Delaunay triangulation. Points are 
        // randomly generated.
        // first we load in the standard vtk packages into tcl
        // Generate some random points
        math = vtkMath.New();
        points = vtkPoints.New();
        for(int i = 0; i < 50; i++)
        {
            points.InsertPoint(i, vtkMath.Random(0, 1), vtkMath.Random(0, 1), 0.0);
        }

        // Create a polydata with the points we just created.
        profile = vtkPolyData.New();
        profile.SetPoints(points);

        // Perform a 2D Delaunay triangulation on them.
        del = vtkDelaunay2D.New();
        del.SetInput(profile);
        del.SetTolerance(0.001);

        mapMesh = vtkPolyDataMapper.New();
        mapMesh.SetInputConnection(del.GetOutputPort());

        meshActor = vtkActor.New();
        meshActor.SetMapper(mapMesh);
        meshActor.GetProperty().SetColor(.1, .2, .4);

        // We will now create a nice looking mesh by wrapping the edges in tubes,
        // and putting fat spheres at the points.
        extract = vtkExtractEdges.New();
        extract.SetInputConnection(del.GetOutputPort());

        tubes = vtkTubeFilter.New();
        tubes.SetInputConnection(extract.GetOutputPort());
        tubes.SetRadius(0.01);
        tubes.SetNumberOfSides(6);

        mapEdges = vtkPolyDataMapper.New();
        mapEdges.SetInputConnection(tubes.GetOutputPort());

        edgeActor = vtkActor.New();
        edgeActor.SetMapper(mapEdges);
        edgeActor.GetProperty().SetColor(0.2000, 0.6300, 0.7900);
        edgeActor.GetProperty().SetSpecularColor(1, 1, 1);
        edgeActor.GetProperty().SetSpecular(0.3);
        edgeActor.GetProperty().SetSpecularPower(20);
        edgeActor.GetProperty().SetAmbient(0.2);
        edgeActor.GetProperty().SetDiffuse(0.8);

        ball = vtkSphereSource.New();
        ball.SetRadius(0.025);
        ball.SetThetaResolution(12);
        ball.SetPhiResolution(12);

        balls = vtkGlyph3D.New();
        balls.SetInputConnection(del.GetOutputPort());
        balls.SetSourceConnection(ball.GetOutputPort());

        mapBalls = vtkPolyDataMapper.New();
        mapBalls.SetInputConnection(balls.GetOutputPort());

        ballActor = vtkActor.New();
        ballActor.SetMapper(mapBalls);
        ballActor.GetProperty().SetColor(1.0000, 0.4118, 0.7059);
        ballActor.GetProperty().SetSpecularColor(1, 1, 1);
        ballActor.GetProperty().SetSpecular(0.3);
        ballActor.GetProperty().SetSpecularPower(20);
        ballActor.GetProperty().SetAmbient(0.2);
        ballActor.GetProperty().SetDiffuse(0.8);

        // Create graphics objects
        // Create the rendering window, renderer, and interactive renderer
        ren1 = vtkRenderer.New();
        renWin = vtkRenderWindow.New();
        renWin.AddRenderer(ren1);
        iren = vtkRenderWindowInteractor.New();
        iren.SetRenderWindow(renWin);

        // Add the actors to the renderer, set the background and size
        ren1.AddActor(ballActor);
        ren1.AddActor(edgeActor);
        ren1.SetBackground(1, 1, 1);
        renWin.SetSize(150, 150);

        // render the image
        ren1.ResetCamera();
        ren1.GetActiveCamera().Zoom(1.5);
        iren.Initialize();
        iren.Start();

        // Clean Up
        deleteAllVTKObjects();
    }
    static vtkMath math;
    static vtkPoints points;
    static vtkPolyData profile;
    static vtkDelaunay2D del;
    static vtkPolyDataMapper mapMesh;
    static vtkActor meshActor;
    static vtkExtractEdges extract;
    static vtkTubeFilter tubes;
    static vtkPolyDataMapper mapEdges;
    static vtkActor edgeActor;
    static vtkSphereSource ball;
    static vtkGlyph3D balls;
    static vtkPolyDataMapper mapBalls;
    static vtkActor ballActor;
    static vtkRenderer ren1;
    static vtkRenderWindow renWin;
    static vtkRenderWindowInteractor iren;

    ///<summary>Deletes all static objects created</summary>
    public static void deleteAllVTKObjects()
    {
        //clean up vtk objects
        if (math != null) { math.Dispose(); }
        if (points != null) { points.Dispose(); }
        if (profile != null) { profile.Dispose(); }
        if (del != null) { del.Dispose(); }
        if (mapMesh != null) { mapMesh.Dispose(); }
        if (meshActor != null) { meshActor.Dispose(); }
        if (extract != null) { extract.Dispose(); }
        if (tubes != null) { tubes.Dispose(); }
        if (mapEdges != null) { mapEdges.Dispose(); }
        if (edgeActor != null) { edgeActor.Dispose(); }
        if (ball != null) { ball.Dispose(); }
        if (balls != null) { balls.Dispose(); }
        if (mapBalls != null) { mapBalls.Dispose(); }
        if (ballActor != null) { ballActor.Dispose(); }
        if (ren1 != null) { ren1.Dispose(); }
        if (renWin != null) { renWin.Dispose(); }
        if (iren != null) { iren.Dispose(); }
    }
}


