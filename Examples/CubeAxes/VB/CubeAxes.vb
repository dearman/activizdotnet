Imports Kitware.vtk
Module CubeAxes
    Dim fohe As vtkBYUReader
    Dim normals As vtkPolyDataNormals
    Dim foheMapper As vtkPolyDataMapper
    Dim foheActor As vtkLODActor
    Dim outline As vtkOutlineFilter
    Dim mapOutline As vtkPolyDataMapper
    Dim outlineActor As vtkActor
    Dim camera As vtkCamera
    Dim light As vtkLight
    Dim ren1 As vtkRenderer
    Dim ren2 As vtkRenderer
    Dim renWin As vtkRenderWindow
    Dim iren As vtkRenderWindowInteractor
    Dim tprop As vtkTextProperty
    Dim axes As vtkCubeAxesActor2D
    Dim axes2 As vtkCubeAxesActor2D
    Dim foo As Integer
  
    ' <summary>
    ' An Example that does not use a Windows Form
    ' </summary>
    ' <param name="argv"></param>
    Sub Main()
        ' This example demonstrates the use of vtkCubeAxesActor2D to indicate the
        ' position in space that the camera is currently viewing.
        ' The vtkCubeAxesActor2D draws axes on the bounding box of the data set and
        ' labels the axes with x-y-z coordinates.
        '
        ' First we include the VTK Tcl packages which will make available
        ' all of the vtk commands to Tcl
        '
        ' Create a vtkBYUReader and read in a data set.
        '
        fohe = vtkBYUReader.[New]
        fohe.SetGeometryFileName("../../../teapot.g")

        ' Create a vtkPolyDataNormals filter to calculate the normals of the data set.
        normals = vtkPolyDataNormals.[New]
        normals.SetInputConnection(fohe.GetOutputPort())

        ' Set up the associated mapper and actor.
        foheMapper = vtkPolyDataMapper.[New]
        foheMapper.SetInputConnection(normals.GetOutputPort())

        foheActor = vtkLODActor.[New]
        foheActor.SetMapper(foheMapper)

        ' Create a vtkOutlineFilter to draw the bounding box of the data set.  Also
        ' create the associated mapper and actor.
        outline = vtkOutlineFilter.[New]
        outline.SetInputConnection(normals.GetOutputPort())

        mapOutline = vtkPolyDataMapper.[New]
        mapOutline.SetInputConnection(outline.GetOutputPort())

        outlineActor = vtkActor.[New]
        outlineActor.SetMapper(mapOutline)
        outlineActor.GetProperty().SetColor(0, 0, 0)

        ' Create a vtkCamera, and set the camera parameters.
        camera = vtkCamera.[New]
        camera.SetClippingRange(1.60187, 20.0842)
        camera.SetFocalPoint(0.21406, 1.5, 0)
        camera.SetPosition(8.3761, 4.94858, 4.12505)
        camera.SetViewUp(0.180325, 0.549245, -0.815974)

        ' Create a vtkLight, and set the light parameters.
        light = vtkLight.[New]
        light.SetFocalPoint(0.21406, 1.5, 0)
        light.SetPosition(8.3761, 4.94858, 4.12505)

        ' Create the Renderers.  Assign them the appropriate viewport coordinates,
        ' active camera, and light.
        ren1 = vtkRenderer.[New]
        ren1.SetViewport(0, 0, 0.5, 1.0)
        ren1.SetActiveCamera(camera)
        ren1.AddLight(light)

        ren2 = vtkRenderer.[New]
        ren2.SetViewport(0.5, 0, 1.0, 1.0)
        ren2.SetActiveCamera(camera)
        ren2.AddLight(light)

        ' Create the RenderWindow and RenderWindowInteractor.
        renWin = vtkRenderWindow.[New]
        renWin.AddRenderer(ren1)
        renWin.AddRenderer(ren2)
        renWin.SetWindowName("VTK - Cube Axes")
        renWin.SetSize(600, 300)
        iren = vtkRenderWindowInteractor.[New]
        iren.SetRenderWindow(renWin)

        ' Add the actors to the renderer, and set the background.
        ren1.AddViewProp(foheActor)
        ren1.AddViewProp(outlineActor)
        ren2.AddViewProp(foheActor)
        ren2.AddViewProp(outlineActor)
        ren1.SetBackground(0.1, 0.2, 0.4)
        ren2.SetBackground(0.1, 0.2, 0.4)

        ' Create a text property for both cube axes
        tprop = vtkTextProperty.[New]
        tprop.SetColor(1, 1, 1)
        tprop.ShadowOn()

        ' Create a vtkCubeAxesActor2D.  Use the outer edges of the bounding box to
        ' draw the axes.  Add the actor to the renderer.
        axes = vtkCubeAxesActor2D.[New]
        axes.SetInput(normals.GetOutput())
        axes.SetCamera(ren1.GetActiveCamera())
        axes.SetLabelFormat("%6.4g")
        axes.SetFlyModeToOuterEdges()
        axes.SetFontFactor(0.8)
        axes.SetAxisTitleTextProperty(tprop)
        axes.SetAxisLabelTextProperty(tprop)
        ren1.AddViewProp(axes)

        ' Create a vtkCubeAxesActor2D.  Use the closest vertex to the camera to
        ' determine where to draw the axes.  Add the actor to the renderer.
        axes2 = vtkCubeAxesActor2D.[New]
        axes2.SetViewProp(foheActor)
        axes2.SetCamera(ren2.GetActiveCamera())
        axes2.SetLabelFormat("%6.4g")
        axes2.SetFlyModeToClosestTriad()
        axes2.SetFontFactor(0.8)
        axes2.ScalingOff()
        axes2.SetAxisTitleTextProperty(tprop)
        axes2.SetAxisLabelTextProperty(tprop)
        ren2.AddViewProp(axes2)

        ' Render
        renWin.Render()

        ' Set the user method (bound to key 'u')
        iren.Initialize()
        iren.Start()

        ' Set up a check for aborting rendering
        AddHandler renWin.AbortCheckEvt, AddressOf TkCheckAbort

        ' Clean Up
        deleteAllVTKObjects()
    End Sub

    ' <summary>
    ' Callback function for renWin.AbortCheckEvt
    ' </summary>
    Public Sub TkCheckAbort(ByVal sender As vtkObject, ByVal e As vtkObjectEventArgs)
        foo = renWin.GetEventPending()
        If Not (foo = 0) Then
            renWin.SetAbortRender(1)
        End If
    End Sub

    '<summary>Deletes all Dim Asobjects created</summary>
    Public Sub deleteAllVTKObjects()
        'clean up vtk objects
        If fohe IsNot Nothing Then
            fohe.Dispose()
        End If
        If normals IsNot Nothing Then
            normals.Dispose()
        End If
        If foheMapper IsNot Nothing Then
            foheMapper.Dispose()
        End If
        If foheActor IsNot Nothing Then
            foheActor.Dispose()
        End If
        If outline IsNot Nothing Then
            outline.Dispose()
        End If
        If mapOutline IsNot Nothing Then
            mapOutline.Dispose()
        End If
        If outlineActor IsNot Nothing Then
            outlineActor.Dispose()
        End If
        If camera IsNot Nothing Then
            camera.Dispose()
        End If
        If light IsNot Nothing Then
            light.Dispose()
        End If
        If ren1 IsNot Nothing Then
            ren1.Dispose()
        End If
        If ren2 IsNot Nothing Then
            ren2.Dispose()
        End If
        If renWin IsNot Nothing Then
            renWin.Dispose()
        End If
        If iren IsNot Nothing Then
            iren.Dispose()
        End If
        If tprop IsNot Nothing Then
            tprop.Dispose()
        End If
        If axes IsNot Nothing Then
            axes.Dispose()
        End If
        If axes2 IsNot Nothing Then
            axes2.Dispose()
        End If
    End Sub
End Module
