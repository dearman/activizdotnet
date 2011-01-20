Imports Kitware.vtk
Module BoxWidget
    Dim sphere As vtkSphereSource
    Dim cone As vtkConeSource
    Dim glyph As vtkGlyph3D
    Dim apd As vtkAppendPolyData
    Dim maceMapper As vtkPolyDataMapper
    Dim maceActor As vtkLODActor
    Dim planes As vtkPlanes
    Dim clipper As vtkClipPolyData
    Dim selectMapper As vtkPolyDataMapper
    Dim selectActor As vtkLODActor
    Dim ren1 As vtkRenderer
    Dim renWin As vtkRenderWindow
    Dim iren As vtkRenderWindowInteractor
    Dim boxWidget As vtkBoxWidget
    
    ' <summary>
    ' An example converted from a vtk Tcl example
    ' Note: a vtkRenderWindow does not have to be 
    ' embeded in a CSharp Dialog
    ' </summary>
    ' <param name="argv"></param>
    Sub Main()
        ' Demonstrate how to use the vtkBoxWidget 3D widget,
        ' Me script uses a 3D box widget to define a "clipping box" to clip some
        ' simple geometry (a mace). Make sure that you hit the "W" key to activate the widget.
        ' Create a mace out of filters.
        sphere = vtkSphereSource.[New]
        cone = vtkConeSource.[New]
        glyph = vtkGlyph3D.[New]

        glyph.SetInputConnection(sphere.GetOutputPort())
        glyph.SetSource(cone.GetOutput())
        glyph.SetVectorModeToUseNormal()
        glyph.SetScaleModeToScaleByVector()
        glyph.SetScaleFactor(0.25)

        ' The sphere and spikes are appended into a single polydata. Me just makes things
        ' simpler to manage.
        apd = vtkAppendPolyData.[New]
        apd.AddInput(glyph.GetOutput())
        apd.AddInput(sphere.GetOutput())

        maceMapper = vtkPolyDataMapper.[New]
        maceMapper.SetInputConnection(apd.GetOutputPort())

        maceActor = vtkLODActor.[New]
        maceActor.SetMapper(maceMapper)
        maceActor.VisibilityOn()

        ' Me portion of the code clips the mace with the vtkPlanes implicit function.
        ' The clipped region is colored green.
        planes = vtkPlanes.[New]
        clipper = vtkClipPolyData.[New]

        clipper.SetInputConnection(apd.GetOutputPort())
        clipper.SetClipFunction(planes)
        clipper.InsideOutOn()

        selectMapper = vtkPolyDataMapper.[New]
        selectMapper.SetInputConnection(clipper.GetOutputPort())

        selectActor = vtkLODActor.[New]
        selectActor.SetMapper(selectMapper)
        selectActor.GetProperty().SetColor(0, 1, 0)
        selectActor.VisibilityOff()
        selectActor.SetScale(1.01, 1.01, 1.01)

        ' Create the RenderWindow, Renderer and both Actors
        ren1 = vtkRenderer.[New]
        renWin = vtkRenderWindow.[New]
        renWin.AddRenderer(ren1)
        iren = vtkRenderWindowInteractor.[New]
        iren.SetRenderWindow(renWin)

        ' The SetInteractor method is how 3D widgets are associated with the render
        ' window interactor. Internally, SetInteractor sets up a bunch of callbacks
        ' using the Command/Observer mechanism (AddObserver()).
        boxWidget = vtkBoxWidget.[New]
        boxWidget.SetInteractor(iren)
        boxWidget.SetPlaceFactor(1.25)
        ren1.AddActor(maceActor)
        ren1.AddActor(selectActor)

        ' Add the actors to the renderer, set the background and size
        ren1.SetBackground(0.1, 0.2, 0.4)
        renWin.SetSize(300, 300)

        ' Place the interactor initially. The input to a 3D widget is used to 
        ' initially position and scale the widget. The EndInteractionEvent is
        ' observed which invokes the SelectPolygons callback.
        boxWidget.SetInput(glyph.GetOutput())
        boxWidget.PlaceWidget()
        AddHandler boxWidget.EndInteractionEvt, AddressOf SelectPolygons

        ' render the image
        iren.Initialize()
        iren.Start()
        'Clean Up
        deleteAllVTKObjects()
    End Sub

    ' <summary>
    ' Callback function for boxWidget.EndInteractionEvt
    ' </summary>
    Public Sub SelectPolygons(ByVal sender As vtkObject, ByVal e As vtkObjectEventArgs)
        boxWidget.GetPlanes(planes)
        selectActor.VisibilityOn()
    End Sub

    '<summary>Deletes all static objects created</summary>
    Public Sub deleteAllVTKObjects()
        'clean up vtk objects
        If sphere IsNot Nothing Then
            sphere.Dispose()
        End If
        If cone IsNot Nothing Then
            cone.Dispose()
        End If
        If glyph IsNot Nothing Then
            glyph.Dispose()
        End If
        If apd IsNot Nothing Then
            apd.Dispose()
        End If
        If maceMapper IsNot Nothing Then
            maceMapper.Dispose()
        End If
        If maceActor IsNot Nothing Then
            maceActor.Dispose()
        End If
        If planes IsNot Nothing Then
            planes.Dispose()
        End If
        If clipper IsNot Nothing Then
            clipper.Dispose()
        End If
        If selectMapper IsNot Nothing Then
            selectMapper.Dispose()
        End If
        If selectActor IsNot Nothing Then
            selectActor.Dispose()
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
        If boxWidget IsNot Nothing Then
            boxWidget.Dispose()
        End If
    End Sub
End Module
