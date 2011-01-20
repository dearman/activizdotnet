Imports Kitware.VTK
Public Class Form1
    ' <summary>
    ' Application to load and display a volume
    ' </summary>

    Dim MouseDown1 As Boolean
    Dim Interactor As vtkRenderWindowInteractor
    Dim RenderWindow As vtkRenderWindow
    Dim Renderer As vtkRenderer
    Dim ImageActor As vtkImageActor
    Dim Clip As vtkImageClip
    Dim fileName As String

    ' <summary>
    ' Tell the application when the mouse is being dragged
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub iren_LeftButtonReleaseEvt(ByVal sender As vtkObject, ByVal e As vtkObjectEventArgs)
        Me.MouseDown1 = False
    End Sub

    ' <summary>
    ' Tell the application when the mouse is being dragged
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub iren_LeftButtonPressEvt(ByVal sender As vtkObject, ByVal e As vtkObjectEventArgs)
        Me.MouseDown1 = True
    End Sub

    ' <summary>
    ' Display the render window with the 3D Volume in it
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub RenderWindowControl2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenderWindowControl2.Load
        'Create all the objects for the pipeline
        Dim spwf As vtkPiecewiseFunction
        Dim gpwf As vtkPiecewiseFunction
        Dim ctf As vtkColorTransferFunction
        Dim vol As vtkVolume
        Dim texMapper As vtkFixedPointVolumeRayCastMapper
        Dim reader As vtkXMLReader
        Dim renderer As vtkRenderer

        renderer = RenderWindowControl2.RenderWindow.GetRenderers().GetFirstRenderer()
        reader = vtkXMLImageDataReader.[New]
        texMapper = vtkFixedPointVolumeRayCastMapper.[New]
        vol = vtkVolume.[New]
        ctf = vtkColorTransferFunction.[New]
        spwf = vtkPiecewiseFunction.[New]
        gpwf = vtkPiecewiseFunction.[New]

        'Read in the file
        reader.SetFileName(fileName)
        reader.Update()

        'Go through the visulizatin pipeline
        texMapper.SetInputConnection(reader.GetOutputPort())

        'Set the color curve for the volume
        ctf.AddHSVPoint(0, 0.67, 0.07, 1)
        ctf.AddHSVPoint(94, 0.67, 0.07, 1)
        ctf.AddHSVPoint(139, 0, 0, 0)
        ctf.AddHSVPoint(160, 0.28, 0.047, 1)
        ctf.AddHSVPoint(254, 0.38, 0.013, 1)

        'Set the opacity curve for the volume
        spwf.AddPoint(84, 0)
        spwf.AddPoint(151, 0.1)
        spwf.AddPoint(255, 1)

        'Set the gradient curve for the volume
        gpwf.AddPoint(0, 0.2)
        gpwf.AddPoint(10, 0.2)
        gpwf.AddPoint(25, 1)

        vol.GetProperty().SetColor(ctf)
        vol.GetProperty().SetScalarOpacity(spwf)
        vol.GetProperty().SetGradientOpacity(gpwf)

        vol.SetMapper(texMapper)

        'Go through the Graphics Pipeline
        renderer.AddVolume(vol)
    End Sub



    ' <summary>
    ' Display the render window with the slice in it
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub RenderWindowControl1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenderWindowControl1.Load
        'Get the name of the Unsigned Char volume that you want to load
        fileName = "../../../head.vti"

        'Create all the objects for the pipeline
        Dim reader As vtkXMLImageDataReader
        Dim iactor As vtkImageActor
        Dim contour As vtkContourFilter
        Dim mapper As vtkPolyDataMapper
        Dim actor As vtkActor
        Dim style As vtkInteractorStyle
        Dim clip As vtkImageClip
        Dim renderer As vtkRenderer
        Dim iren As vtkRenderWindowInteractor

        reader = vtkXMLImageDataReader.[New]
        iactor = vtkImageActor.[New]
        clip = vtkImageClip.[New]
        contour = vtkContourFilter.[New]
        mapper = vtkPolyDataMapper.[New]
        actor = vtkActor.[New]
        style = vtkInteractorStyleImage.[New]

        renderer = RenderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer()

        'Read the Image
        reader.SetFileName(fileName)

        'Go through the visulization pipeline
        iactor.SetInput(reader.GetOutput())
        renderer.AddActor(iactor)
        reader.Update()
        Dim extent(6) As Integer
        extent = reader.GetOutput().GetWholeExtent()
        iactor.SetDisplayExtent(extent(0), extent(1), extent(2), extent(3), (extent(4) + extent(5)) / 2, (extent(4) + extent(5)) / 2)

        clip.SetInputConnection(reader.GetOutputPort())
        clip.SetOutputWholeExtent(extent(0), extent(1), extent(2), extent(3), (extent(4) + extent(5)) / 2, (extent(4) + extent(5)) / 2)

        contour.SetInputConnection(Clip.GetOutputPort())
        contour.SetValue(0, 100)

        mapper.SetInputConnection(contour.GetOutputPort())
        mapper.SetScalarVisibility(1)

        'Go through the graphics pipeline
        actor.SetMapper(mapper)
        actor.GetProperty().SetColor(0, 1, 0)

        renderer.AddActor(actor)

        'Give a new style to the interactor
        iren = RenderWindowControl1.RenderWindow.GetInteractor()
        iren.SetInteractorStyle(style)

        'Add new events to the interactor style

        AddHandler style.LeftButtonPressEvt, New vtkObject.vtkObjectEventHandler(AddressOf iren_LeftButtonPressEvt)
        AddHandler style.LeftButtonReleaseEvt, New vtkObject.vtkObjectEventHandler(AddressOf iren_LeftButtonReleaseEvt)
        AddHandler style.MouseMoveEvt, New vtkObject.vtkObjectEventHandler(AddressOf iren_MouseMoveEvt)

        'Update global variables
        Me.TrackBar1.Maximum = extent(5)
        Me.TrackBar1.Minimum = extent(4)
        Me.Interactor = iren
        Me.RenderWindow = RenderWindowControl1.RenderWindow
        Me.Renderer = renderer
        Me.Clip = clip
        Me.ImageActor = iactor
    End Sub

    ' <summary>
    ' Move the slice when the trackbar is moved
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll
        If (ImageActor IsNot Nothing) Then

            Dim dim1(3) As Integer
            Dim lastPos(2) As Integer
            Dim size(2) As Integer

            lastPos = Me.Interactor.GetLastEventPosition()
            Size = Me.RenderWindow.GetSize()
            dim1 = Me.ImageActor.GetInput().GetDimensions()

            Dim newSlice As Integer
            newSlice = TrackBar1.Value

            If newSlice >= 0 And newSlice < dim1(2) Then
                Me.Clip.SetOutputWholeExtent(0, dim1(0) - 1, 0, dim1(1) - 1, newSlice, newSlice)
                Me.ImageActor.SetDisplayExtent(0, dim1(0) - 1, 0, dim1(1) - 1, newSlice, newSlice)
                Me.Renderer.ResetCameraClippingRange()
                Me.RenderWindow.Render()
            End If
        End If
    End Sub

    ' <summary>
    ' Move the slice and the trackbar when the mouse is dragged on the render window
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub iren_MouseMoveEvt(ByVal sender As vtkObject, ByVal e As vtkObjectEventArgs)
        If Me.MouseDown1 And Me.ImageActor IsNot Nothing Then
            Dim dim1(3) As Integer
            Dim lastPos(2) As Integer
            Dim size(2) As Integer

            lastPos = Me.Interactor.GetLastEventPosition()
            size = Me.RenderWindow.GetSize()
            dim1 = Me.ImageActor.GetInput().GetDimensions()

            Dim newSlice As Integer
            newSlice = CType((CType(dim1(2) - 1.0, Double) * CType(lastPos(1), Double) / CType(size(1), Double)), Integer)
            If newSlice >= 0 And newSlice < dim1(2) Then
                Me.TrackBar1.Value = newSlice
                Me.Clip.SetOutputWholeExtent(0, dim1(0) - 1, 0, dim1(1) - 1, newSlice, newSlice)
                Me.ImageActor.SetDisplayExtent(0, dim1(0) - 1, 0, dim1(1) - 1, newSlice, newSlice)
                Me.Renderer.ResetCameraClippingRange()
                Me.RenderWindow.Render()
            End If
        End If
    End Sub
    ' <summary>
    ' Clean Up any global variables that might still be around
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If (Me.Interactor IsNot Nothing) Then
            Me.Interactor.Dispose()
        End If
        If (Me.ImageActor IsNot Nothing) Then
            Me.ImageActor.Dispose()
        End If

        If (Me.Clip IsNot Nothing) Then
            Me.Clip.Dispose()
        End If
    End Sub
End Class
