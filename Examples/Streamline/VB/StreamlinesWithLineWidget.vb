Imports Kitware.VTK

' <summary>
' Class Containing Main Method
' </summary
Module StreamlinesWithLineWidget

    Dim pl3d As vtkMultiBlockPLOT3DReader
    Dim lineWidget As vtkLineWidget
    Dim seeds As vtkPolyData
    Dim rk4 As vtkRungeKutta4
    Dim streamer As vtkStreamLine
    Dim rf As vtkRibbonFilter
    Dim streamMapper As vtkPolyDataMapper
    Dim streamline As vtkActor
    Dim lineWidget2 As vtkLineWidget
    Dim seeds2 As vtkPolyData
    Dim streamer2 As vtkStreamLine
    Dim rf2 As vtkRibbonFilter
    Dim streamMapper2 As vtkPolyDataMapper
    Dim streamline2 As vtkActor
    Dim outline As vtkStructuredGridOutlineFilter
    Dim outlineMapper As vtkPolyDataMapper
    Dim outlineActor As vtkActor
    Dim ren1 As vtkRenderer
    Dim renWin As vtkRenderWindow
    Dim iren As vtkRenderWindowInteractor
    Dim cam1 As vtkCamera


    ' <summary>
    ' Entry Point
    ' </summary>
    ' <param name="argv"></param
    Sub Main()
        'Prefix Content is: ""
        ' This example demonstrates how to use the vtkLineWidget to seed
        ' and manipulate streamlines. Two line widgets are created. One is
        ' invoked by pressing 'W', the other by pressing 'L'. Both can exist
        ' together.
        ' Start by loading some data.
        pl3d = vtkMultiBlockPLOT3DReader.[New]
        pl3d.SetXYZFileName("../../../combxyz.bin")
        pl3d.SetQFileName("../../../combq.bin")
        pl3d.SetScalarFunctionNumber(100)
        pl3d.SetVectorFunctionNumber(202)
        pl3d.Update()

        ' The line widget is used seed the streamlines.
        lineWidget = vtkLineWidget.[New]
        seeds = vtkPolyData.[New]
        lineWidget.SetInput(pl3d.GetOutput())
        lineWidget.SetAlignToYAxis()
        lineWidget.PlaceWidget()
        lineWidget.GetPolyData(seeds)
        lineWidget.ClampToBoundsOn()

        rk4 = vtkRungeKutta4.[New]
        streamer = vtkStreamLine.[New]
        streamer.SetInputConnection(pl3d.GetOutputPort())
        streamer.SetSource(seeds)
        streamer.SetMaximumPropagationTime(100)
        streamer.SetIntegrationStepLength(0.2)
        streamer.SetStepLength(0.001)
        streamer.SetNumberOfThreads(1)
        streamer.SetIntegrationDirectionToForward()
        streamer.VorticityOn()
        streamer.SetIntegrator(rk4)

        rf = vtkRibbonFilter.[New]
        rf.SetInputConnection(streamer.GetOutputPort())
        rf.SetWidth(0.1)
        rf.SetWidthFactor(5)

        streamMapper = vtkPolyDataMapper.[New]
        streamMapper.SetInputConnection(rf.GetOutputPort())
        streamMapper.SetScalarRange(pl3d.GetOutput().GetScalarRange()(0), pl3d.GetOutput().GetScalarRange()(1))

        streamline = vtkActor.[New]
        streamline.SetMapper(streamMapper)
        streamline.VisibilityOff()

        ' The second line widget is used seed more streamlines.
        lineWidget2 = vtkLineWidget.[New]
        seeds2 = vtkPolyData.[New]
        lineWidget2.SetInput(pl3d.GetOutput())
        lineWidget2.PlaceWidget()
        lineWidget2.GetPolyData(seeds2)
        lineWidget2.SetKeyPressActivationValue(108)

        streamer2 = vtkStreamLine.[New]
        streamer2.SetInputConnection(pl3d.GetOutputPort())
        streamer2.SetSource(seeds2)
        streamer2.SetMaximumPropagationTime(100)
        streamer2.SetIntegrationStepLength(0.2)
        streamer2.SetStepLength(0.001)
        streamer2.SetNumberOfThreads(1)
        streamer2.SetIntegrationDirectionToForward()
        streamer2.VorticityOn()
        streamer2.SetIntegrator(rk4)

        rf2 = vtkRibbonFilter.[New]
        rf2.SetInputConnection(streamer2.GetOutputPort())
        rf2.SetWidth(0.1)
        rf2.SetWidthFactor(5)

        streamMapper2 = vtkPolyDataMapper.[New]
        streamMapper2.SetInputConnection(rf2.GetOutputPort())
        streamMapper2.SetScalarRange(pl3d.GetOutput().GetScalarRange()(0), pl3d.GetOutput().GetScalarRange()(1))

        streamline2 = vtkActor.[New]
        streamline2.SetMapper(streamMapper2)
        streamline2.VisibilityOff()

        outline = vtkStructuredGridOutlineFilter.[New]
        outline.SetInputConnection(pl3d.GetOutputPort())

        outlineMapper = vtkPolyDataMapper.[New]
        outlineMapper.SetInputConnection(outline.GetOutputPort())
        outlineActor = vtkActor.[New]
        outlineActor.SetMapper(outlineMapper)

        ' Create the RenderWindow, Renderer and both Actors
        ren1 = vtkRenderer.[New]
        renWin = vtkRenderWindow.[New]
        renWin.AddRenderer(ren1)
        iren = vtkRenderWindowInteractor.[New]
        iren.SetRenderWindow(renWin)

        ' Associate the line widget with the interactor
        lineWidget.SetInteractor(iren)
        AddHandler lineWidget.StartInteractionEvt, AddressOf BeginInteraction
        AddHandler lineWidget.InteractionEvt, AddressOf GenerateStreamlines
        lineWidget2.SetInteractor(iren)
        AddHandler lineWidget2.StartInteractionEvt, AddressOf BeginInteraction2
        AddHandler lineWidget2.EndInteractionEvt, AddressOf GenerateStreamlines2

        ' Add the actors to the renderer, set the background and size
        ren1.AddActor(outlineActor)
        ren1.AddActor(streamline)
        ren1.AddActor(streamline2)

        ren1.SetBackground(1, 1, 1)
        renWin.SetSize(300, 300)
        ren1.SetBackground(0.1, 0.2, 0.4)

        cam1 = ren1.GetActiveCamera()
        cam1.SetClippingRange(3.95297, 50)
        cam1.SetFocalPoint(9.71821, 0.458166, 29.3999)
        cam1.SetPosition(2.7439, -37.3196, 38.7167)
        cam1.SetViewUp(-0.16123, 0.264271, 0.950876)

        ' render the image
        renWin.Render()
        lineWidget2.On()
        iren.Initialize()
        iren.Start()

        ' Clean Up
        deleteAllVTKObjects()
    End Sub

    '<summary>Deletes all Dim Asobjects created</summary>
    Public Sub deleteAllVTKObjects()
        'clean up vtk objects
        If (pl3d IsNot Nothing) Then
            pl3d.Dispose()
        End If
        If (lineWidget IsNot Nothing) Then
            lineWidget.Dispose()
        End If
        If (seeds IsNot Nothing) Then
            seeds.Dispose()
        End If
        If (rk4 IsNot Nothing) Then
            rk4.Dispose()
        End If
        If (streamer IsNot Nothing) Then
            streamer.Dispose()
        End If
        If (rf IsNot Nothing) Then
            rf.Dispose()
        End If
        If (streamMapper IsNot Nothing) Then
            streamMapper.Dispose()
        End If
        If (streamline IsNot Nothing) Then
            streamline.Dispose()
        End If
        If (lineWidget2 IsNot Nothing) Then
            lineWidget2.Dispose()
        End If
        If (seeds2 IsNot Nothing) Then
            seeds2.Dispose()
        End If
        If (streamer2 IsNot Nothing) Then
            streamer2.Dispose()
        End If
        If (rf2 IsNot Nothing) Then
            rf2.Dispose()
        End If
        If (streamMapper2 IsNot Nothing) Then
            streamMapper2.Dispose()
        End If
        If (streamline2 IsNot Nothing) Then
            streamline2.Dispose()
        End If
        If (outline IsNot Nothing) Then
            outline.Dispose()
        End If
        If (outlineMapper IsNot Nothing) Then
            outlineMapper.Dispose()
        End If
        If (outlineActor IsNot Nothing) Then
            outlineActor.Dispose()
        End If
        If (ren1 IsNot Nothing) Then
            ren1.Dispose()
        End If
        If (renWin IsNot Nothing) Then
            renWin.Dispose()
        End If
        If (iren IsNot Nothing) Then
            iren.Dispose()
        End If
        If (cam1 IsNot Nothing) Then
            cam1.Dispose()
        End If
    End Sub

    ' <summary>
    ' Callback function for lineWidget.StartInteractionEvt
    ' </summary>
    Sub BeginInteraction(ByVal sender As vtkObject, ByVal e As vtkObjectEventArgs)
        streamline.VisibilityOn()
    End Sub

    ' <summary>
    ' Callback function for lineWidget.InteractionEvt
    ' </summary>
    Sub GenerateStreamlines(ByVal sender As vtkObject, ByVal e As vtkObjectEventArgs)
        lineWidget.GetPolyData(seeds)
    End Sub

    ' <summary>
    ' Callback function for lineWidget2.StartInteractionEvt
    ' </summary>
    Sub BeginInteraction2(ByVal sender As vtkObject, ByVal e As vtkObjectEventArgs)
        streamline2.VisibilityOn()
    End Sub

    ' <summary>
    ' Callback function for lineWidget2.InteractionEvt
    ' </summary>
    Sub GenerateStreamlines2(ByVal sender As vtkObject, ByVal e As vtkObjectEventArgs)
        lineWidget2.GetPolyData(seeds2)
        renWin.Render()
    End Sub


End Module


