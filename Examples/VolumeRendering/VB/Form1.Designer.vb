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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.RenderWindowControl1 = New Kitware.VTK.RenderWindowControl
        Me.RenderWindowControl2 = New Kitware.VTK.RenderWindowControl
        Me.TrackBar1 = New System.Windows.Forms.TrackBar
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TrackBar1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.RenderWindowControl1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.RenderWindowControl2)
        Me.SplitContainer1.Size = New System.Drawing.Size(512, 275)
        Me.SplitContainer1.SplitterDistance = 265
        Me.SplitContainer1.TabIndex = 0
        '
        'RenderWindowControl1
        '
        Me.RenderWindowControl1.AddTestActors = False
        Me.RenderWindowControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RenderWindowControl1.Location = New System.Drawing.Point(12, 12)
        Me.RenderWindowControl1.Name = "RenderWindowControl1"
        Me.RenderWindowControl1.Size = New System.Drawing.Size(249, 200)
        Me.RenderWindowControl1.TabIndex = 0
        Me.RenderWindowControl1.TestText = Nothing
        '
        'RenderWindowControl2
        '
        Me.RenderWindowControl2.AddTestActors = False
        Me.RenderWindowControl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RenderWindowControl2.Location = New System.Drawing.Point(3, 12)
        Me.RenderWindowControl2.Name = "RenderWindowControl2"
        Me.RenderWindowControl2.Size = New System.Drawing.Size(228, 251)
        Me.RenderWindowControl2.TabIndex = 0
        Me.RenderWindowControl2.TestText = Nothing
        '
        'TrackBar1
        '
        Me.TrackBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TrackBar1.Location = New System.Drawing.Point(0, 218)
        Me.TrackBar1.Maximum = 100
        Me.TrackBar1.Name = "TrackBar1"
        Me.TrackBar1.Size = New System.Drawing.Size(261, 45)
        Me.TrackBar1.TabIndex = 2
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(512, 275)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "Form1"
        Me.Text = "Volume Rendering"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.TrackBar1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TrackBar1 As System.Windows.Forms.TrackBar
    Friend WithEvents RenderWindowControl1 As Kitware.VTK.RenderWindowControl
    Friend WithEvents RenderWindowControl2 As Kitware.VTK.RenderWindowControl

End Class
