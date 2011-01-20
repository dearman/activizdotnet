<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.RenderWindowControl1 = New Kitware.VTK.RenderWindowControl
        Me.SuspendLayout()
        '
        'RenderWindowControl1
        '
        Me.RenderWindowControl1.AddTestActors = False
        Me.RenderWindowControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RenderWindowControl1.Location = New System.Drawing.Point(12, 12)
        Me.RenderWindowControl1.Name = "RenderWindowControl1"
        Me.RenderWindowControl1.Size = New System.Drawing.Size(562, 346)
        Me.RenderWindowControl1.TabIndex = 0
        Me.RenderWindowControl1.TestText = Nothing
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(586, 370)
        Me.Controls.Add(Me.RenderWindowControl1)
        Me.Name = "Form1"
        Me.Text = "Hello VTK"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents RenderWindowControl1 As Kitware.VTK.RenderWindowControl

End Class
