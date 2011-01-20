Imports Kitware.VTK
Module DelMesh


    Dim math As vtkMath
    Dim points As vtkPoints
    Dim i As Integer
    Dim profile As vtkPolyData
    Dim del As vtkDelaunay2D
    Dim mapMesh As vtkPolyDataMapper
    Dim meshActor As vtkActor
    Dim extract As vtkExtractEdges
    Dim tubes As vtkTubeFilter
    Dim mapEdges As vtkPolyDataMapper
    Dim edgeActor As vtkActor
    Dim ball As vtkSphereSource
    Dim balls As vtkGlyph3D
    Dim mapBalls As vtkPolyDataMapper
    Dim ballActor As vtkActor
    Dim ren1 As vtkRenderer
    Dim renWin As vtkRenderWindow
    Dim iren As vtkRenderWindowInteractor

    ' <summary>
    ' Entry Point
    ' </summary>
    ' <param name="argv"></param>
    Sub Main()
        ' This example demonstrates how to use 2D Delaunay triangulation.
        ' We create a fancy image of a 2D Delaunay triangulation. Points are 
        ' randomly generated.
        ' first we load in the standard vtk packages into tcl
        ' Generate some random points
        math = vtkMath.[New]
        points = vtkPoints.[New]
        i = 0
        While (i < 50)
            points.InsertPoint(i, vtkMath.Random(0, 1), vtkMath.Random(0, 1), 0.0)
            i = i + 1
        End While

        ' Create a polydata with the points we just created.
        profile = vtkPolyData.[New]
        profile.SetPoints(points)

        ' Perform a 2D Delaunay triangulation on them.
        del = vtkDelaunay2D.[New]
        del.SetInput(profile)
        del.SetTolerance(0.001)

        mapMesh = vtkPolyDataMapper.[New]
        mapMesh.SetInputConnection(del.GetOutputPort())

        meshActor = vtkActor.[New]
        meshActor.SetMapper(mapMesh)
        meshActor.GetProperty().SetColor(0.1, 0.2, 0.4)

        ' We will now create a nice looking mesh by wrapping the edges in tubes,
        ' and putting fat spheres at the points.
        extract = vtkExtractEdges.[New]
        extract.SetInputConnection(del.GetOutputPort())

        tubes = vtkTubeFilter.[New]
        tubes.SetInputConnection(extract.GetOutputPort())
        tubes.SetRadius(0.01)
        tubes.SetNumberOfSides(6)

        mapEdges = vtkPolyDataMapper.[New]
        mapEdges.SetInputConnection(tubes.GetOutputPort())

        edgeActor = vtkActor.[New]
        edgeActor.SetMapper(mapEdges)
        edgeActor.GetProperty().SetColor(0.2, 0.63, 0.79)
        edgeActor.GetProperty().SetSpecularColor(1, 1, 1)
        edgeActor.GetProperty().SetSpecular(0.3)
        edgeActor.GetProperty().SetSpecularPower(20)
        edgeActor.GetProperty().SetAmbient(0.2)
        edgeActor.GetProperty().SetDiffuse(0.8)

        ball = vtkSphereSource.[New]
        ball.SetRadius(0.025)
        ball.SetThetaResolution(12)
        ball.SetPhiResolution(12)

        balls = vtkGlyph3D.[New]
        balls.SetInputConnection(del.GetOutputPort())
        balls.SetSourceConnection(ball.GetOutputPort())
        mapBalls = vtkPolyDataMapper.[New]

        mapBalls.SetInputConnection(balls.GetOutputPort())
        ballActor = vtkActor.[New]

        ballActor.SetMapper(mapBalls)
        ballActor.GetProperty().SetColor(1.0, 0.4118, 0.7059)
        ballActor.GetProperty().SetSpecularColor(1, 1, 1)
        ballActor.GetProperty().SetSpecular(0.3)
        ballActor.GetProperty().SetSpecularPower(20)
        ballActor.GetProperty().SetAmbient(0.2)
        ballActor.GetProperty().SetDiffuse(0.8)

        ' Create graphics objects
        ' Create the rendering window, renderer, and interactive renderer
        ren1 = vtkRenderer.[New]
        renWin = vtkRenderWindow.[New]
        renWin.AddRenderer(ren1)
        iren = vtkRenderWindowInteractor.[New]
        iren.SetRenderWindow(renWin)

        ' Add the actors to the renderer, set the background and size
        ren1.AddActor(ballActor)
        ren1.AddActor(edgeActor)
        ren1.SetBackground(1, 1, 1)
        renWin.SetSize(150, 150)

        ' render the image
        ren1.ResetCamera()
        ren1.GetActiveCamera().Zoom(1.5)
        iren.Initialize()
        iren.Start()

        ' Clean Up
        deleteAllVTKObjects()
    End Sub


    '<summary>Deletes all objects</summary>
    Sub deleteAllVTKObjects()
        'clean up vtk objects
        If math IsNot Nothing Then
            math.Dispose()
        End If
        If points IsNot Nothing Then
            points.Dispose()
        End If
        If profile IsNot Nothing Then
            profile.Dispose()
        End If
        If del IsNot Nothing Then
            del.Dispose()
        End If
        If mapMesh IsNot Nothing Then
            mapMesh.Dispose()
        End If
        If meshActor IsNot Nothing Then
            meshActor.Dispose()
        End If
        If extract IsNot Nothing Then
            extract.Dispose()
        End If
        If tubes IsNot Nothing Then
            tubes.Dispose()
        End If
        If mapEdges IsNot Nothing Then
            mapEdges.Dispose()
        End If
        If edgeActor IsNot Nothing Then
            edgeActor.Dispose()
        End If
        If ball IsNot Nothing Then
            ball.Dispose()
        End If
        If balls IsNot Nothing Then
            balls.Dispose()
        End If
        If mapBalls IsNot Nothing Then
            mapBalls.Dispose()
        End If
        If ballActor IsNot Nothing Then
            ballActor.Dispose()
        End If
        If ren1 IsNot Nothing Then
            ren1.Dispose()
        End If
        If renWin IsNot Nothing Then
            renWin.Dispose()
        End If
        If iren IsNot Nothing Then
            iren.Dispose()
        End If
    End Sub
End Module


