Imports Kitware.VTK
Imports System.Text.RegularExpressions
Imports System.IO

Public Class Form1

    ' The view for rendering the graph.
    Dim view As vtkTreeMapView
    Dim g As vtkMutableDirectedGraph
    Dim initialized As Boolean
    Dim SelectedPath As String

    ' <summary>
    ' Set up the view in the render window
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub RenderWindowControl1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenderWindowControl1.Load
        view = vtkTreeMapView.[New]
    End Sub

    ' <summary>
    ' Open a folder browser and graph the 
    ' selected folder
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        If (Me.FolderBrowserDialog1.ShowDialog().Equals(DialogResult.OK)) Then
            'Add the view to the render window
            If (Not initialized) Then
                initialized = True
                view.SetupRenderWindow(Me.RenderWindowControl1.RenderWindow)
                AddHandler view.SelectionChangedEvt, AddressOf view_SelectionChangedEvt
            End If
            g = vtkMutableDirectedGraph.[New]
            Dim name As vtkStringArray
            name = vtkStringArray.[New]
            name.SetName("name")
            g.GetVertexData().AddArray(name)
            Dim path As vtkStringArray
            path = vtkStringArray.[New]
            path.SetName("path")
            g.GetVertexData().SetPedigreeIds(path)
            Dim size As vtkLongLongArray
            size = vtkLongLongArray.[New]
            size.SetName("size")
            g.GetVertexData().AddArray(size)
            Dim cur As String
            cur = Directory.GetCurrentDirectory()

            buildTree(g, -1, Me.FolderBrowserDialog1.SelectedPath)

            Me.Label1.Text = "Viewing " + Me.FolderBrowserDialog1.SelectedPath

            Dim t As vtkTree
            t = vtkTree.[New]
            t.ShallowCopy(g)

            Dim win As vtkRenderWindow
            win = Me.RenderWindowControl1.RenderWindow()
            view.SetupRenderWindow(win)
            view.SetLayoutStrategyToSliceAndDice()
            view.SetLayoutStrategyToSquarify()
            'view.SetBorderPercentage(0.1)
            view.AddRepresentationFromInput(t)
            view.SetAreaLabelArrayName("name")
            view.SetAreaHoverArrayName("path")
            view.SetAreaColorArrayName("level")
            view.SetAreaSizeArrayName("size")
            view.GetRenderer().ResetCamera()
            view.Update()
        End If
    End Sub

    ' <summary>
    ' Open the explorer window of the selected file
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Public Sub view_SelectionChangedEvt(ByVal sender As vtkObject, ByVal e As vtkObjectEventArgs)
        'Get the selection
        Dim arr1 As vtkStringArray
        arr1 = Nothing
        Dim node As vtkSelectionNode
        node = view.GetRepresentation(0).GetSelectionLink().GetSelection().GetNode(0)
        If (Not node Is Nothing) Then
            arr1 = node.GetSelectionList()
        End If

        Dim path As String
        path = ""

        'If it is a diretory open it, if not cut off the end and open the directory
        If (Not arr1 Is Nothing) Then
            If (System.IO.Directory.Exists(arr1.GetValue(0))) Then
                path = arr1.GetValue(0)
            Else
                path = Regex.Replace(arr1.GetValue(0), "\\[^\\]+\.[^\\]*$", "")
            End If
        End If

        If (path <> SelectedPath) Then
            SelectedPath = path

            System.Diagnostics.Debug.WriteLine(SelectedPath)

            If ("" <> SelectedPath) Then
                'Create the process
                Dim psi As System.Diagnostics.ProcessStartInfo
                psi = New System.Diagnostics.ProcessStartInfo("cmd.exe", "/C explorer " + SelectedPath)
                psi.CreateNoWindow = True
                psi.UseShellExecute = False
                Dim p As System.Diagnostics.Process
                p = System.Diagnostics.Process.Start(psi)
            End If
        End If
    End Sub

    ' <summary>
    ' Recursive function that creates a level of the file tree
    ' </summary>
    ' <param name="g"></param>
    ' <param name="parent"></param>
    ' <param name="path"></param>
    Private Sub buildTree(ByRef g As vtkMutableDirectedGraph, ByVal parent As Integer, ByVal path As String)
        Dim name As vtkStringArray
        Dim size As vtkLongLongArray
        Dim fullpath As vtkStringArray

        name = g.GetVertexData().GetAbstractArray("name")
        size = g.GetVertexData().GetAbstractArray("size")
        fullpath = g.GetVertexData().GetAbstractArray("path")

        If (System.IO.Directory.Exists(path)) Then
            Dim v As Integer
            v = 0
            If (parent = -1) Then
                v = g.AddVertex()
            Else
                v = g.AddChild(parent)
            End If
            Dim pathparts As String()
            pathparts = path.Split("\\")
            Dim ipaths As Integer
            ipaths = pathparts.GetUpperBound(0)
            fullpath.InsertNextValue(path)
            name.InsertNextValue(pathparts(ipaths))
            size.InsertNextValue(1024)
            Me.Label1.Text = "Processing " + path
            Me.Update()
            Try
                For Each f As String In Directory.GetFiles(path)
                    Console.Out.WriteLine(f)
                    buildTree(g, v, f)
                Next f
                For Each d As String In Directory.GetDirectories(path)
                    Console.Out.WriteLine(d)
                    buildTree(g, v, d)
                Next d
            Catch excpt As Exception
                Console.Error.WriteLine(excpt.Message)
            End Try
        ElseIf File.Exists(path) Then
            Dim info As FileInfo
            info = New FileInfo(path)
            'Do not graph files smaller than 1K
            If (info.Length > 1024) Then
                g.AddChild(parent)
                fullpath.InsertNextValue(path)
                name.InsertNextValue(System.IO.Path.GetFileName(path))
                size.InsertNextValue(info.Length)
            End If
        End If
    End Sub

    ' <summary>
    ' Clean up
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        System.GC.Collect()
    End Sub
End Class
