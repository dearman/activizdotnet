Imports Kitware.VTK
Public Class Form1

    Dim puzzle As vtkSpherePuzzle
    Dim mapper As vtkPolyDataMapper
    Dim actor As vtkActor
    Dim arrows As vtkSpherePuzzleArrows
    Dim mapper2 As vtkPolyDataMapper
    Dim actor2 As vtkActor
    Dim once As Boolean

    Dim in_piece_rotation As Boolean
    Dim LastVal As Double
    Dim LastValExists As Boolean


    ' <summary>
    ' Reset
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        puzzle.Reset()
        RenderWindowControl1.RenderWindow.Render()
    End Sub

    ' <summary>
    ' Quit
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    ' <summary>
    ' Set up the dialog
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub RenderWindowControl1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenderWindowControl1.Load
        'Setup the variables and the background
        Dim ren1 As vtkRenderer
        ren1 = RenderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer()
        mapper.SetInputConnection(puzzle.GetOutputPort())
        mapper2.SetInputConnection(arrows.GetOutputPort())
        actor.SetMapper(mapper)
        actor2.SetMapper(mapper2)
        ren1.AddActor(actor)
        ren1.AddActor(actor2)
        ren1.SetBackground(0.1, 0.2, 0.4)

        'Set up the camera
        ren1.ResetCamera()
        Dim cam As vtkCamera
        cam = ren1.GetActiveCamera()
        cam.Elevation(-40)
        RenderWindowControl1.RenderWindow.Render()

        'Change the style to a trackball style
        'Equivalent of pressing 't'
        Dim iren As vtkRenderWindowInteractor
        Dim istyle As vtkInteractorStyleSwitch
        iren = RenderWindowControl1.RenderWindow.GetInteractor()
        istyle = vtkInteractorStyleSwitch.[New]
        iren.SetInteractorStyle(istyle)
        istyle.SetCurrentStyleToTrackballCamera()

        'Add events to the iren instead of Observers
        AddHandler iren.MouseMoveEvt, AddressOf MotionCallback
        AddHandler iren.CharEvt, AddressOf CharCallback
    End Sub

    ' <summary>
    ' Clean up globals
    ' </summary>
    Sub disposeAllVTKObjects()
        puzzle.Dispose()
        mapper.Dispose()
        actor.Dispose()
        arrows.Dispose()
        mapper2.Dispose()
        actor2.Dispose()
    End Sub


    ' <summary>
    ' Highlights pieces
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Sub MotionCallback(ByVal sender As vtkObject, ByVal e As vtkObjectEventArgs)
        'Make sure the piece isn't in an animation
        'durring a click or bad things happen
        If in_piece_rotation = False Then
            Dim iren As vtkRenderWindowInteractor
            Dim istyle As vtkInteractorStyleSwitch
            iren = RenderWindowControl1.RenderWindow.GetInteractor()
            istyle = iren.GetInteractorStyle()

            'return if the user is performing interaction
            If istyle.GetState() <> 0 Then
                Return
            End If
            Dim pos() As Integer
            Dim x As Integer
            Dim y As Integer
            pos = iren.GetEventPosition()
            x = pos(0)
            y = pos(1)
            Dim ren1 As vtkRenderer
            ren1 = RenderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer()
            ren1.SetDisplayPoint(x, y, ren1.GetZ(x, y))
            ren1.DisplayToWorld()
            Dim pt() As Double
            Dim val As Double
            pt = ren1.GetWorldPoint()
            val = puzzle.SetPoint(pt(0), pt(1), pt(2))
            If (LastValExists = False Or val <> LastVal) Then
                RenderWindowControl1.RenderWindow.Render()
                LastVal = val
                LastValExists = True
            End If
        End If
    End Sub

    ' <summary>
    ' Called when a key is pressed
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Sub CharCallback(byval sender as vtkObject, byval e as vtkObjectEventArgs)
        Dim iren As vtkRenderWindowInteractor
        Dim keycode As SByte
        iren = RenderWindowControl1.RenderWindow.GetInteractor()
        keycode = iren.GetKeyCode()
        'if the keycode is not M
        If (keycode <> 109 And keycode <> 77) Then

            Return
        End If
        Dim pos() As Integer
        pos = iren.GetEventPosition()
        ButtonCallback(pos(0), pos(1))
    End Sub

    ' <summary>
    ' Moves the sphere when the mouse is clicked in
    ' position (x,y)
    ' </summary>
    ' <param name="x"></param>
    ' <param name="y"></param>
    Sub ButtonCallback(ByVal x As Double, ByVal y As Double)
        If (in_piece_rotation = False) Then
            in_piece_rotation = True
            Dim ren1 As vtkRenderer
            ren1 = RenderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer()
            ren1.SetDisplayPoint(x, y, ren1.GetZ(x, y))
            ren1.DisplayToWorld()
            Dim pt() As Double
            pt = ren1.GetWorldPoint()
            x = pt(0)
            y = pt(1)
            Dim z As Double
            z = pt(2)
            Dim i As Integer
            i = 0
            While i <= 100
                puzzle.SetPoint(x, y, z)
                puzzle.MovePoint(i)
                RenderWindowControl1.RenderWindow.Render()
                Me.Update()
                i = i + 10
            End While
            in_piece_rotation = False
        End If
    End Sub

    ' <summary>
    ' Scrambles the puzzle when the form first becomes
    ' visible
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub Form1_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Me.Timer1.Start()
    End Sub
    ' <summary>
    ' Clean up
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        disposeAllVTKObjects()
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        puzzle = vtkSpherePuzzle.[New]
        mapper = vtkPolyDataMapper.[New]
        actor = vtkActor.[New]
        arrows = vtkSpherePuzzleArrows.[New]
        mapper2 = vtkPolyDataMapper.[New]
        actor2 = vtkActor.[New]
        once = True

        in_piece_rotation = False
        LastVal = 0
        LastValExists = False
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If (once = True) Then
            ButtonCallback(218, 195)
            ButtonCallback(261, 128)
            ButtonCallback(213, 107)
            ButtonCallback(203, 162)
            ButtonCallback(134, 186)
            once = False
        End If
    End Sub
End Class
