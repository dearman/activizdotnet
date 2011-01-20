Imports Kitware.VTK

Module HelloVTK

    Dim sphere As vtkSphereSource
    Dim shrink As vtkShrinkPolyData
    Dim mapper As vtkPolyDataMapper
    Dim actor As vtkActor
    Dim ren1 As vtkRenderer
    Dim renWin As vtkRenderWindow
    Dim iren As vtkRenderWindowInteractor
    Dim camera As vtkCamera

    '<summary>Entry Point</summary>
    Sub Main()
        ' Create a simple sphere. A pipeline is created.
        sphere = vtkSphereSource.[New]
        sphere.SetThetaResolution(8)
        sphere.SetPhiResolution(16)

        shrink = vtkShrinkPolyData.[New]
        shrink.SetInputConnection(sphere.GetOutputPort())
        shrink.SetShrinkFactor(0.9)

        mapper = vtkPolyDataMapper.[New]
        mapper.SetInputConnection(shrink.GetOutputPort())

        ' The actor links the data pipeline to the rendering subsystem
        actor = vtkActor.[New]
        actor.SetMapper(mapper)
        actor.GetProperty().SetColor(1, 0, 0)

        ' Create components of the rendering subsystem
        '
        ren1 = vtkRenderer.[New]
        renWin = vtkRenderWindow.[New]
        renWin.AddRenderer(ren1)
        iren = vtkRenderWindowInteractor.[New]
        iren.SetRenderWindow(renWin)

        ' Add the actors to the renderer, set the window size
        '
        ren1.AddViewProp(actor)
        renWin.SetSize(250, 250)
        renWin.Render()
        camera = ren1.GetActiveCamera()
        camera.Zoom(1.5)

        ' render the image and start the event loop
        '
        renWin.Render()

        iren.Initialize()
        iren.Start()

        deleteAllVTKObjects()
    End Sub

    '<summary>Deletes all static objects created</summary>
    Public Sub deleteAllVTKObjects()
        'clean up vtk objects
        If sphere IsNot Nothing Then
            sphere.Dispose()
        End If
        If mapper IsNot Nothing Then
            mapper.Dispose()
        End If
        If actor IsNot Nothing Then
            actor.Dispose()
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
        If camera IsNot Nothing Then
            camera.Dispose()
        End If
    End Sub

End Module
