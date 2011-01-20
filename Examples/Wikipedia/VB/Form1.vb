Imports Kitware.VTK

Public Class Form1
    'The graph and the view for the graph
    Dim view As vtkGraphLayoutView
    Dim g As vtkMutableDirectedGraph

    'The loading graphic's actor and renderer.
    Dim logoRenderer As vtkRenderer
    Dim logoActor As vtkImageActor

    'An array list that only gets filled with
    'links when the graph is expanding.
    Dim arrListSmall As System.Collections.ArrayList

    ' <summary>
    ' Application Constructor
    ' </summary>
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        'Initialize the loading graphic
        Dim ass As System.Reflection.Assembly
        Dim stream As System.IO.Stream
        ass = System.Reflection.Assembly.GetExecutingAssembly()
        stream = ass.GetManifestResourceStream(ass.GetName().Name + ".logo.png")
        'Read in the logo as a stream
        Dim img As System.Drawing.Image
        img = Image.FromStream(stream)
        'Convert the System.Drawing.Image to Kitware.VTK.ImageData
        Dim idata As vtkImageData

        idata = vtkImageData.FromImage(img)
        logoActor = vtkImageActor.[New]
        logoActor.SetInput(idata)
        logoRenderer = vtkRenderer.[New]
        logoRenderer.AddActor(logoActor)
        'Make sure the camera will look at the point the actor will spin around
        logoActor.SetOrigin((idata.GetDimensions()(0) / 2), (idata.GetDimensions()(1) / 2), 0)
        Dim cam As vtkCamera
        cam = logoRenderer.GetActiveCamera()
        cam.SetFocalPoint(logoActor.GetCenter()(0), logoActor.GetCenter()(1), logoActor.GetCenter()(2))
        cam.SetPosition(logoActor.GetCenter()(0), logoActor.GetCenter()(1), 900)
    End Sub

    ' <summary>
    ' Load the vtkRenderWindowControl
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub RenderWindowControl1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenderWindowControl1.Load
        'Set up the view and make it fire a SelectionChangedEvt
        view = vtkGraphLayoutView.[New]
        AddHandler view.SelectionChangedEvt, AddressOf view_SelectionChangedEvt
        view.SetupRenderWindow(RenderWindowControl1.RenderWindow)
    End Sub

    ' <summary>
    ' Event fired when the graph selection is changed
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Public Sub view_SelectionChangedEvt(ByVal sender As vtkObject, ByVal e As vtkObjectEventArgs)
        Dim arr1 As vtkIdTypeArray
        arr1 = view.GetRepresentation(0).GetSelectionLink().GetSelection().GetSelectionList()
        Dim arr2 As vtkStringArray
        arr2 = g.GetVertexData().GetAbstractArray("label")
        'get a psudo random name if more than one are selected
        Dim name As String
        name = arr2.GetValue(arr1.GetValue(System.DateTime.Now.Millisecond Mod arr1.GetSize()))
        'reset the small arrayList 
        arrListSmall = New System.Collections.ArrayList()
        Dim hops As Integer
        Try
            hops = System.Convert.ToInt32(ToolStripTextBox3.Text)
        Catch ex As Exception
            hops = 1
        End Try
        'Start the loading graphic and switch renderers
        Dim win As vtkRenderWindow
        win = Me.RenderWindowControl1.RenderWindow
        win.AddRenderer(logoRenderer)
        win.Render()
        Me.WebBrowser1.Url = New Uri("http://en.wikipedia.org/wiki/" + name.Replace(" ", "_"))

        'Start the work
        addLinks(g, name, hops)
        'Go back to the graph view after the work is done
        win.RemoveRenderer(logoRenderer)
    End Sub

    ' <summary>
    ' Rotates the logo 
    ' </summary>
    Public Sub rotateLogo()
        'rotate the logo a number of degrees that does not 
        'evenly go into 90
        logoActor.RotateY(13.583495783485784)
        logoRenderer.ResetCameraClippingRange()
        RenderWindowControl1.RenderWindow.Render()
    End Sub

    ' <summary>
    ' Clears the graph and makes a new one
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        Dim lookupValue As String
        lookupValue = ToolStripTextBox1.Text
        'clean up any old graph views in the renderer
        Dim g_temp As vtkMutableDirectedGraph
        g_temp = g
        g = vtkMutableDirectedGraph.[New]
        If g_temp IsNot Nothing Then
            g_temp.Dispose()
        End If
        'reset the small arrayList
        arrListSmall = New System.Collections.ArrayList()
        Dim Label As vtkStringArray
        Label = vtkStringArray.[New]
        Label.SetName("label")
        'give the graph a starting point
        g.GetVertexData().AddArray(Label)
        g.AddVertex()
        Label.InsertNextValue(lookupValue)
        arrListSmall.Add(lookupValue)
        Dim hops As Integer
        Try
            hops = System.Convert.ToInt32(ToolStripTextBox3.Text)

        Catch ex As Exception
            hops = 1
        End Try
        'Start the loading graphic and switch renderers
        Dim win As vtkRenderWindow
        win = Me.RenderWindowControl1.RenderWindow
        win.AddRenderer(logoRenderer)
        win.Render()

        Me.WebBrowser1.Url = New Uri("http://en.wikipedia.org/wiki/" + lookupValue.Replace(" ", "_"))
        'Start the work
        addLinks(g, lookupValue, hops)
        'Go back to the graph view after the work is done
        win.RemoveRenderer(logoRenderer)

        'Setup the view properties
        view.SetupRenderWindow(win)
        view.SetLayoutStrategyToSimple2D()
        view.AddRepresentationFromInput(g)
        view.SetVertexLabelArrayName("label")
        view.VertexLabelVisibilityOn()
        view.SetVertexColorArrayName("VertexDegree")
        view.ColorVerticesOn()
        view.GetRenderer().ResetCamera()
        view.Update()
    End Sub

    ' <summary>
    ' Recursive function that finds and 
    ' graphs wiipedia links
    ' </summary>
    ' <param name="g">The graph</param>
    ' <param name="lookupValue">Name of orgin article</param>
    ' <param name="hops">How many degrees of separation from the original article</param>
    Private Sub addLinks(ByRef g As Kitware.VTK.vtkMutableDirectedGraph, ByVal lookupValue As String, ByVal hops As Integer)
        Dim label As vtkStringArray
        label = g.GetVertexData().GetAbstractArray("label")
        Dim Parent As Integer
        Parent = label.LookupValue(lookupValue)
        'if lookupValue is not in the graph add it
        If Parent < 0 Then
            rotateLogo()
            Parent = g.AddVertex()
            label.InsertNextValue(lookupValue)
            arrListSmall.Add(lookupValue)
        End If
        'Parse Wikipedia for the lookupValue
        Dim underscores As String
        underscores = lookupValue.Replace(" ", "_")
        Dim webRequest As System.Net.HttpWebRequest
        webRequest = System.Net.WebRequest.Create("http://en.wikipedia.org/wiki/Special:Export/" + underscores)
        webRequest.Credentials = System.Net.CredentialCache.DefaultCredentials
        webRequest.Accept = "text/xml"
        Try
            Dim webResponse As System.Net.HttpWebResponse
            webResponse = webRequest.GetResponse()
            Dim responseStream As System.IO.Stream
            responseStream = webResponse.GetResponseStream()
            Dim reader As System.Xml.XmlReader
            reader = New System.Xml.XmlTextReader(responseStream)
            Dim NS As String
            NS = "http://www.mediawiki.org/xml/export-0.4/"
            Dim doc As System.Xml.XPath.XPathDocument
            doc = New System.Xml.XPath.XPathDocument(reader)
            reader.Close()
            webResponse.Close()
            Dim myXPahtNavigator As System.Xml.XPath.XPathNavigator
            myXPahtNavigator = doc.CreateNavigator()
            Dim nodesText As System.Xml.XPath.XPathNodeIterator
            nodesText = myXPahtNavigator.SelectDescendants("text", NS, False)
            Dim fullText As String

            fullText = ""
            'Parse the wiki page for links
            While nodesText.MoveNext()
                fullText += nodesText.Current.InnerXml + " "
            End While
            Dim m As System.Text.RegularExpressions.Match
            m = System.Text.RegularExpressions.Regex.Match(fullText, "\[\[.*?\]\]")
            Dim max As Integer
            Try
                max = System.Convert.ToInt32(ToolStripTextBox2.Text)
            Catch ex As Exception
                max = -1
            End Try
            Dim count As Integer
            count = 0
            While (m.Success And ((count < max) Or (max < 0)))
                Dim s As String
                s = m.ToString()
                Dim index As Integer
                index = s.IndexOf("|")
                Dim substring As String
                substring = ""
                If (index > 0) Then
                    substring = s.Substring(2, index - 2)
                Else
                    substring = s.Substring(2, s.Length - 4)
                End If
                'if the new substring is not already there add it
                Dim v As Integer
                v = label.LookupValue(substring)
                If (v < 0) Then
                    rotateLogo()
                    v = g.AddVertex()
                    label.InsertNextValue(substring)
                    arrListSmall.Add(substring)
                    If (hops > 1) Then
                        addLinks(g, substring, hops - 1)
                    End If
                ElseIf (arrListSmall.IndexOf(substring) < 0) Then
                    arrListSmall.Add(substring)
                    If (hops > 1) Then
                        addLinks(g, substring, hops - 1)
                    End If
                End If
                'Make sure nothing is linked to twice by expanding the graph
                Dim avi As vtkAdjacentVertexIterator
                avi = vtkAdjacentVertexIterator.[New]
                g.GetAdjacentVertices(Parent, avi)
                m = m.NextMatch()
                count = count + 1
                While avi.HasNext()
                    Dim id As Integer
                    id = avi.Next()
                    If (id = v) Then
                        Return
                    End If
                End While
                rotateLogo()
                g.AddGraphEdge(Parent, v)
            End While
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    ' <summary>
    ' Clean Up
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If (view IsNot Nothing) Then
            view.Dispose()
        End If
        If (logoRenderer IsNot Nothing) Then
            logoRenderer.Dispose()
        End If
        If (logoActor IsNot Nothing) Then
            logoActor.Dispose()
        End If
        view = Nothing
        System.GC.Collect()
    End Sub

    ' <summary>
    ' Listen for the enter key
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub ToolStripTextBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ToolStripTextBox2.KeyPress
        ToolStripTextBox1_KeyPress(sender, e)
    End Sub

    ' <summary>
    ' Listen for the enter key
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub ToolStripTextBox3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ToolStripTextBox3.KeyPress
        ToolStripTextBox1_KeyPress(sender, e)
    End Sub

    ' <summary>
    ' Listen for the enter key
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub ToolStripTextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ToolStripTextBox1.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(System.Windows.Forms.Keys.Enter) Then
            'if enter was pressed on a text box simulate a click on the Go button
            ToolStripButton1_Click(sender, e)
        End If
    End Sub
End Class
