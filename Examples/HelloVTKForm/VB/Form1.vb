Imports Kitware.VTK

Public Class Form1

    Private Sub RenderWindowControl1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenderWindowControl1.Load
        ' Create a simple sphere. A pipeline is created.
        Dim sphere As vtkSphereSource
        sphere = vtkSphereSource.[New]
        sphere.SetThetaResolution(8)
        sphere.SetPhiResolution(16)

        Dim shrink As vtkShrinkPolyData
        shrink = vtkShrinkPolyData.[New]
        shrink.SetInputConnection(sphere.GetOutputPort())
        shrink.SetShrinkFactor(0.9)

        Dim mapper As vtkPolyDataMapper
        mapper = vtkPolyDataMapper.[New]
        mapper.SetInputConnection(shrink.GetOutputPort())

        ' The actor links the data pipeline to the rendering subsystem
        Dim actor As vtkActor
        actor = vtkActor.[New]
        actor.SetMapper(mapper)
        actor.GetProperty().SetColor(1, 0, 0)

        ' Create components of the rendering subsystem
        '
        Dim ren1 As vtkRenderer
        Dim renWin As vtkRenderWindow
        ren1 = RenderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer()
        renWin = RenderWindowControl1.RenderWindow

        ' Add the actors to the renderer, set the window size
        '
        ren1.AddViewProp(actor)
        renWin.SetSize(250, 250)
        renWin.Render()
        Dim camera As vtkCamera
        camera = ren1.GetActiveCamera()
        camera.Zoom(1.5)
    End Sub
End Class
