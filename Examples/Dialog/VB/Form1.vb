Imports Kitware.VTK

Public Class Form1

    Dim imgProp As vtkProp3D
    ' <summary>
    ' Clean up
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        If Me.imgProp IsNot Nothing Then
            Me.imgProp.Dispose()
        End If

        If Me.RenderWindowControl1 IsNot Nothing Then
            Me.RenderWindowControl1.Dispose()
        End If
        System.GC.Collect()
    End Sub

    ' <summary>
    ' Load an image with an open dialog
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click

        If Me.OpenFileDialog1.ShowDialog().Equals(DialogResult.OK) Then
            ' Get the name of the file you want to open from the dialog 
            Dim fileName As String
            Dim ren As vtkRenderer

            fileName = OpenFileDialog1.FileName
            ren = Me.RenderWindowControl1.RenderWindow.GetRenderers().GetItemAsObject(0)

            'Get rid of any props already there
            If imgProp IsNot Nothing Then

                ren.RemoveActor(imgProp)
                imgProp.Dispose()
                imgProp = Nothing
            End If


            'Look at known file types to see if they are readable
            If fileName.Contains(".png") Or fileName.Contains(".jpg") Or fileName.Contains(".jpeg") Or fileName.Contains(".tif") Or fileName.Contains(".slc") Or fileName.Contains(".dicom") Or fileName.Contains(".minc") Or fileName.Contains(".bmp") Or fileName.Contains(".pmn") Then
                Dim rdr As vtkImageReader2
                rdr = Kitware.VTK.vtkImageReader2Factory.CreateImageReader2(fileName)
                rdr.SetFileName(fileName)
                rdr.Update()
                imgProp = vtkImageActor.[New]
                CType(imgProp, vtkImageActor).SetInput(rdr.GetOutput())
                rdr.Dispose()


                '.vtk files need a DataSetReader instead of a ImageReader2
                'some .vtk files need a different kind of reader, but this
                'will read most and serve our purposes
            ElseIf fileName.Contains(".vtk") Then

                Dim dataReader As vtkDataSetReader
                Dim dataMapper As vtkDataSetMapper

                dataReader = vtkDataSetReader.[New]
                dataMapper = vtkDataSetMapper.[New]
                imgProp = vtkActor.[New]
                dataReader.SetFileName(fileName)
                dataReader.Update()
                dataMapper.SetInput(dataReader.GetOutput())
                CType(imgProp, vtkActor).SetMapper(dataMapper)
                dataMapper.Dispose()
                dataMapper = Nothing
                dataReader.Dispose()
                dataReader = Nothing
            Else
                Return
            End If

            ren.AddActor(imgProp)
            'Reset the camera to show the image
            'Equivilant of pressing 'r'
            ren.ResetCamera()
            'Rerender the screen
            'NOTE: sometimes you have to drag the mouse
            'a little before the image shows up
            RenderWindowControl1.RenderWindow.Render()
            ren.Render()

        End If
    End Sub
End Class
