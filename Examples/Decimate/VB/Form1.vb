Imports Kitware.VTK

' <summary>
' Me is an example that shows decimation mainly.
' It also shows how to map textures using predefined
' UV coordinates in vtk file, make your own texture
' coordinates using vtkTextureMapToSphere, and smooth
' polydata.
' </summary>
Public Class Form1

    'Polydata files for the models
    Dim animalData As vtkAlgorithmOutput
    Dim eyeData1 As vtkAlgorithmOutput
    Dim eyeData2 As vtkAlgorithmOutput

    'textures for the models
    Dim animalColorTexture As vtkTexture
    Dim eyeColorTexture1 As vtkTexture
    Dim eyeColorTexture2 As vtkTexture

    Dim deciAnimalColorTexture As vtkTexture
    Dim deciEyeColorTexture1 As vtkTexture
    Dim deciEyeColorTexture2 As vtkTexture

    'full polygon actors 
    Dim animalActor As vtkActor
    Dim eyeActor1 As vtkActor
    Dim eyeActor2 As vtkActor

    'decimated actors
    Dim deciAnimalActor As vtkActor
    Dim deciEyeActor1 As vtkActor
    Dim deciEyeActor2 As vtkActor

    'text showing number of polygons in each window
    Dim textBefore As vtkTextActor
    Dim textAfter As vtkTextActor

    'decimated mappers
    Dim deciAnimalMapper As vtkDataSetMapper
    Dim deciEyeMapper1 As vtkDataSetMapper
    Dim deciEyeMapper2 As vtkDataSetMapper

    'full poly mappers
    Dim animalMapper As vtkDataSetMapper
    Dim eyeMapper1 As vtkDataSetMapper
    Dim eyeMapper2 As vtkDataSetMapper

    'filters for the body model
    Dim triangleAnimal As vtkTriangleFilter
    Dim decimateAnimal As vtkDecimatePro
    Dim cleanAnimal As vtkCleanPolyData
    Dim smoothAnimal As vtkWindowedSincPolyDataFilter
    Dim normalsAnimal As vtkPolyDataNormals

    'filters for the eye models
    Dim triangles As vtkTriangleFilter
    Dim decimate As vtkDecimatePro
    Dim clean As vtkCleanPolyData
    Dim smooth As vtkWindowedSincPolyDataFilter
    Dim normals As vtkPolyDataNormals
    Dim sphereTexture As vtkTextureMapToSphere

    'position of the left eye
    Dim eyeX As Double
    Dim eyeY As Double
    Dim eyeZ As Double

    'makes sure the readers only read once
    Dim rabbitLoaded As Boolean
    Dim squirrelLoaded As Boolean
    Dim flyingSquirrelLoaded As Boolean
    Dim chinchillaLoaded As Boolean

    'Don't let Camera_Modified trigger itself
    Dim ModifyingCamera As Boolean

    'Readers for the models and the textures
    Dim rabbitReader As vtkDataSetReader
    Dim eyeReader As vtkDataSetReader
    Dim rabbitColorReader As vtkPNGReader
    Dim eyeColorReader As vtkPNGReader
    Dim squirrelReader As vtkDataSetReader
    Dim squirrelEyeReader As vtkDataSetReader
    Dim squirrelEyeReader2 As vtkDataSetReader
    Dim squirrelColorReader As vtkPNGReader
    Dim squirrelEyeColorReader As vtkPNGReader
    Dim squirrelEyeColorReader2 As vtkPNGReader
    Dim flyingSquirrelReader As vtkDataSetReader
    Dim flyingSquirreleyeReader As vtkDataSetReader
    Dim flyingSquirrelColorReader As vtkPNGReader
    Dim flyingSquirrelEyeColorReader As vtkPNGReader
    Dim chinchillaReader As vtkDataSetReader
    Dim chinchillaEyeReader As vtkDataSetReader
    Dim chinchillaColorReader As vtkPNGReader
    Dim chinchillaEyeColorReader As vtkPNGReader

    ' <summary>
    ' Constructor
    ' </summary>
    Sub New()
        animalActor = vtkActor.[New]
        eyeActor1 = vtkActor.[New]
        eyeActor2 = vtkActor.[New]

        deciAnimalActor = vtkActor.[New]
        deciEyeActor1 = vtkActor.[New]
        deciEyeActor2 = vtkActor.[New]

        textBefore = vtkTextActor.[New]
        textAfter = vtkTextActor.[New]

        deciAnimalMapper = vtkDataSetMapper.[New]
        deciEyeMapper1 = vtkDataSetMapper.[New]
        deciEyeMapper2 = vtkDataSetMapper.[New]

        animalMapper = vtkDataSetMapper.[New]
        eyeMapper1 = vtkDataSetMapper.[New]
        eyeMapper2 = vtkDataSetMapper.[New]

        triangleAnimal = vtkTriangleFilter.[New]
        decimateAnimal = vtkDecimatePro.[New]
        cleanAnimal = vtkCleanPolyData.[New]
        smoothAnimal = vtkWindowedSincPolyDataFilter.[New]
        normalsAnimal = vtkPolyDataNormals.[New]

        triangles = vtkTriangleFilter.[New]
        decimate = vtkDecimatePro.[New]
        clean = vtkCleanPolyData.[New]
        smooth = vtkWindowedSincPolyDataFilter.[New]
        normals = vtkPolyDataNormals.[New]
        sphereTexture = vtkTextureMapToSphere.[New]

        animalColorTexture = vtkTexture.[New]
        eyeColorTexture1 = vtkTexture.[New]
        eyeColorTexture2 = vtkTexture.[New]

        deciAnimalColorTexture = vtkTexture.[New]
        deciEyeColorTexture1 = vtkTexture.[New]
        deciEyeColorTexture2 = vtkTexture.[New]

        eyeX = 0
        eyeY = 0
        eyeZ = 0

        rabbitLoaded = False
        squirrelLoaded = False
        flyingSquirrelLoaded = False
        chinchillaLoaded = False

        ModifyingCamera = False

        rabbitReader = vtkDataSetReader.[New]
        eyeReader = vtkDataSetReader.[New]
        rabbitColorReader = vtkPNGReader.[New]
        eyeColorReader = vtkPNGReader.[New]
        squirrelReader = vtkDataSetReader.[New]
        squirrelEyeReader = vtkDataSetReader.[New]
        squirrelEyeReader2 = vtkDataSetReader.[New]
        squirrelColorReader = vtkPNGReader.[New]
        squirrelEyeColorReader = vtkPNGReader.[New]
        squirrelEyeColorReader2 = vtkPNGReader.[New]
        flyingSquirrelReader = vtkDataSetReader.[New]
        flyingSquirreleyeReader = vtkDataSetReader.[New]
        flyingSquirrelColorReader = vtkPNGReader.[New]
        flyingSquirrelEyeColorReader = vtkPNGReader.[New]
        chinchillaReader = vtkDataSetReader.[New]
        chinchillaEyeReader = vtkDataSetReader.[New]
        chinchillaColorReader = vtkPNGReader.[New]
        chinchillaEyeColorReader = vtkPNGReader.[New]

        InitializeComponent()
    End Sub

    ' <summary>
    ' Clean up the global variables
    ' </summary>
    Sub DeleteAllVTKObjects()
        animalActor.Dispose()
        deciAnimalActor.Dispose()
        eyeActor1.Dispose()
        eyeActor2.Dispose()
        deciEyeActor1.Dispose()
        deciEyeActor2.Dispose()
        textAfter.Dispose()
        textBefore.Dispose()

        deciAnimalMapper.Dispose()
        deciEyeMapper1.Dispose()
        deciEyeMapper2.Dispose()
        cleanAnimal.Dispose()
        smoothAnimal.Dispose()
        normalsAnimal.Dispose()
        triangleAnimal.Dispose()
        decimateAnimal.Dispose()

        animalMapper.Dispose()
        eyeMapper1.Dispose()
        eyeMapper2.Dispose()
        clean.Dispose()
        smooth.Dispose()
        normals.Dispose()
        triangles.Dispose()
        decimate.Dispose()

        rabbitReader.Dispose()
        eyeReader.Dispose()
        rabbitColorReader.Dispose()
        eyeColorReader.Dispose()
        squirrelReader.Dispose()
        squirrelEyeReader.Dispose()
        squirrelEyeReader2.Dispose()
        squirrelColorReader.Dispose()
        squirrelEyeColorReader.Dispose()
        squirrelEyeColorReader2.Dispose()
        flyingSquirrelReader.Dispose()
        flyingSquirreleyeReader.Dispose()
        flyingSquirrelColorReader.Dispose()
        flyingSquirrelEyeColorReader.Dispose()
        chinchillaReader.Dispose()
        chinchillaEyeReader.Dispose()
        chinchillaColorReader.Dispose()
        chinchillaEyeColorReader.Dispose()
    End Sub

    ' <summary>
    ' Loads the Rabbit model and textures
    ' into the algorithms and textures
    ' </summary>
    Sub loadRabbit()
        'Set a predefined position for the eyes
        'that matches the .blend file
        eyeX = 0.057
        eyeY = -0.311
        eyeZ = 1.879

        'load the rabbit model and textures if 
        'they are not already loaded
        If (False = rabbitLoaded) Then
            rabbitReader.SetFileName("../../../models/rabbit.vtk")
            rabbitReader.Update()
            eyeReader.SetFileName("../../../models/rabbit_eye.vtk")
            eyeReader.Update()
            rabbitColorReader.SetFileName("../../../textures/rabbit_skin_col.png")
            rabbitColorReader.Update()
            eyeColorReader.SetFileName("../../../textures/rabbit_eye.png")
            eyeColorReader.Update()
            rabbitLoaded = True
        End If
        'Set the algorithms and textures to the
        'ouput of the readers
        animalData = rabbitReader.GetOutputPort()

        eyeData1 = eyeReader.GetOutputPort()
        eyeData2 = eyeData1

        animalColorTexture.InterpolateOn()
        animalColorTexture.SetInput(rabbitColorReader.GetOutput())

        eyeColorTexture1.InterpolateOn()
        eyeColorTexture1.SetInput(eyeColorReader.GetOutput())

        eyeColorTexture2.InterpolateOn()
        eyeColorTexture2.SetInput(eyeColorReader.GetOutput())

        deciAnimalColorTexture.InterpolateOn()
        deciAnimalColorTexture.SetInput(rabbitColorReader.GetOutput())

        deciEyeColorTexture1.InterpolateOn()
        deciEyeColorTexture1.SetInput(eyeColorReader.GetOutput())

        deciEyeColorTexture2.InterpolateOn()
        deciEyeColorTexture2.SetInput(eyeColorReader.GetOutput())
    End Sub

    ' <summary>
    ' Loads the Squirrel model and textures
    ' into the algorithms and textures
    ' </summary>
    Sub loadSquirrel()
        'Set a predefined position for the eyes
        'that matches the .blend file
        eyeX = 0.076
        eyeY = -0.178
        eyeZ = 0.675

        'load the squirrel model and textures if 
        'they are not already loaded
        If (False = squirrelLoaded) Then
            squirrelReader.SetFileName("../../../models/squirrel.vtk")
            squirrelReader.Update()
            squirrelEyeReader.SetFileName("../../../models/squirrel_eyeR.vtk")
            squirrelEyeReader.Update()
            squirrelEyeReader2.SetFileName("../../../models/squirrel_eyeL.vtk")
            squirrelEyeReader2.Update()
            squirrelColorReader.SetFileName("../../../textures/squirrel_skin_col.png")
            squirrelColorReader.Update()
            squirrelEyeColorReader.SetFileName("../../../textures/squirrel_eyeR.png")
            squirrelEyeColorReader.Update()
            squirrelEyeColorReader2.SetFileName("../../../textures/squirrel_eyeL.png")
            squirrelEyeColorReader2.Update()
            squirrelLoaded = True
        End If
        'Set the algorithms and textures to the
        'ouput of the readers
        eyeColorTexture1.InterpolateOn()
        eyeColorTexture1.SetInput(squirrelEyeColorReader.GetOutput())

        deciEyeColorTexture1.InterpolateOn()
        deciEyeColorTexture1.SetInput(squirrelEyeColorReader.GetOutput())

        eyeColorTexture2.InterpolateOn()
        eyeColorTexture2.SetInput(squirrelEyeColorReader2.GetOutput())

        deciEyeColorTexture2.InterpolateOn()
        deciEyeColorTexture2.SetInput(squirrelEyeColorReader2.GetOutput())

        animalColorTexture.InterpolateOn()
        animalColorTexture.SetInput(squirrelColorReader.GetOutput())

        deciAnimalColorTexture.InterpolateOn()
        deciAnimalColorTexture.SetInput(squirrelColorReader.GetOutput())

        eyeData2 = squirrelEyeReader2.GetOutputPort()
        eyeData1 = squirrelEyeReader.GetOutputPort()

        animalData = squirrelReader.GetOutputPort()
    End Sub
    ' <summary>
    ' Loads the Flying Squirrel model and textures
    ' into the algorithms and textures
    ' </summary>
    Sub loadFlyingSquirrel()
        'Set a predefined position for the eyes
        'that matches the .blend file
        eyeX = 0.054
        eyeY = -0.189
        eyeZ = 0.427

        'load the flyingsquirrel model and textures if 
        'they are not already loaded
        If (False = flyingSquirrelLoaded) Then
            flyingSquirrelReader.SetFileName("../../../models/flyingsquirrel.vtk")
            flyingSquirrelReader.Update()
            flyingSquirreleyeReader.SetFileName("../../../models/flyingsquirrel_eye.vtk")
            flyingSquirreleyeReader.Update()
            flyingSquirrelColorReader.SetFileName("../../../textures/flyingsquirrel_skin_col.png")
            flyingSquirrelColorReader.Update()
            flyingSquirrelEyeColorReader.SetFileName("../../../textures/flyingsquirrel_eye.png")
            flyingSquirrelEyeColorReader.Update()
            flyingSquirrelLoaded = True
        End If
        'Set the algorithms and textures to the
        'ouput of the readers
        animalData = flyingSquirrelReader.GetOutputPort()

        eyeData1 = flyingSquirreleyeReader.GetOutputPort()
        eyeData2 = eyeData1

        animalColorTexture.InterpolateOn()
        animalColorTexture.SetInput(flyingSquirrelColorReader.GetOutput())

        deciAnimalColorTexture.InterpolateOn()
        deciAnimalColorTexture.SetInput(flyingSquirrelColorReader.GetOutput())

        eyeColorTexture1.InterpolateOn()
        eyeColorTexture1.SetInput(flyingSquirrelEyeColorReader.GetOutput())

        deciEyeColorTexture1.InterpolateOn()
        deciEyeColorTexture1.SetInput(flyingSquirrelEyeColorReader.GetOutput())

        eyeColorTexture2.InterpolateOn()
        eyeColorTexture2.SetInput(flyingSquirrelEyeColorReader.GetOutput())

        deciEyeColorTexture2.InterpolateOn()
        deciEyeColorTexture2.SetInput(flyingSquirrelEyeColorReader.GetOutput())
    End Sub
    ' <summary>
    ' Loads the Chinchilla model and textures
    ' into the algorithms and textures
    ' </summary>
    Sub loadChinchilla()
        'Set a predefined position for the eyes
        'that matches the .blend file
        eyeX = 0.052
        eyeY = -0.144
        eyeZ = 0.424

        'load the chinchilla model and textures if 
        'they are not already loaded
        If (False = chinchillaLoaded) Then
            chinchillaReader.SetFileName("../../../models/chinchilla.vtk")
            chinchillaReader.Update()
            chinchillaEyeReader.SetFileName("../../../models/chinchilla_eye.vtk")
            chinchillaEyeReader.Update()
            chinchillaColorReader.SetFileName("../../../textures/chinchilla_skin_col.png")
            chinchillaColorReader.Update()
            chinchillaEyeColorReader.SetFileName("../../../textures/chinchilla_eye.png")
            chinchillaEyeColorReader.Update()
            chinchillaLoaded = True
        End If

        'Set the algorithms and textures to the
        'ouput of the readers
        animalData = chinchillaReader.GetOutputPort()

        eyeData1 = chinchillaEyeReader.GetOutputPort()
        eyeData2 = eyeData1

        animalColorTexture.InterpolateOn()
        animalColorTexture.SetInput(chinchillaColorReader.GetOutput())

        deciAnimalColorTexture.InterpolateOn()
        deciAnimalColorTexture.SetInput(chinchillaColorReader.GetOutput())

        eyeColorTexture1.InterpolateOn()
        eyeColorTexture1.SetInput(chinchillaEyeColorReader.GetOutput())

        deciEyeColorTexture1.InterpolateOn()
        deciEyeColorTexture1.SetInput(chinchillaEyeColorReader.GetOutput())

        eyeColorTexture2.InterpolateOn()
        eyeColorTexture2.SetInput(chinchillaEyeColorReader.GetOutput())

        deciEyeColorTexture2.InterpolateOn()
        deciEyeColorTexture2.SetInput(chinchillaEyeColorReader.GetOutput())
    End Sub

    ' <summary>
    ' Changes the actors to whatever the 
    ' animal currently loaded is
    ' </summary>
    Sub updateAnimal()
        '----Go through the pipeline for the animal body

        'Convert the polydata to triangles (in the default files they are rectangles)
        triangleAnimal.SetInputConnection(animalData)
        If (Me.CheckBox1.Checked) Then
            'smooth the polydata
            cleanAnimal.SetInputConnection(triangleAnimal.GetOutputPort())
            smoothAnimal.SetInputConnection(cleanAnimal.GetOutputPort())
            normalsAnimal.SetInputConnection(smoothAnimal.GetOutputPort())
            'connect the smoothed data to a mapper
            animalMapper.SetInputConnection(normalsAnimal.GetOutputPort())
            'decimate the smoothed polydata
            decimateAnimal.SetInputConnection(normalsAnimal.GetOutputPort())
        Else
            'connect the triangle polydata to a mapper before decimation
            animalMapper.SetInputConnection(triangleAnimal.GetOutputPort())
            'decimate the triangled data
            decimateAnimal.SetInputConnection(triangleAnimal.GetOutputPort())
        End If
        decimateAnimal.SetTargetReduction(System.Convert.ToDouble(ToolStripTextBox1.Text))
        decimateAnimal.SetPreserveTopology(0)
        'connect the decimated polydata a mapper
        deciAnimalMapper.SetInputConnection(decimateAnimal.GetOutputPort())

        '----Go through the pipeline for the first eye
        'Convert the polydata to triangles (in the default files they are rectangles)
        triangles.SetInputConnection(eyeData1)
        If (Me.CheckBox1.Checked) Then
            'smooth the polydata
            clean.SetInputConnection(triangles.GetOutputPort())
            smooth.SetInputConnection(clean.GetOutputPort())
            normals.SetInputConnection(smooth.GetOutputPort())
            'connect the smoothed data to a mapper
            sphereTexture.SetInputConnection(normals.GetOutputPort())
            'decimate the smoothed polydata
            eyeMapper1.SetInputConnection(sphereTexture.GetOutputPort())
        Else
            sphereTexture.SetInputConnection(triangles.GetOutputPort())
            'connect the triangle polydata to a mapper before decimation
            eyeMapper1.SetInputConnection(sphereTexture.GetOutputPort())
        End If
        decimate.SetInputConnection(sphereTexture.GetOutputPort())
        decimate.SetTargetReduction(System.Convert.ToDouble(ToolStripTextBox1.Text))
        decimate.SetPreserveTopology(0)
        'connect the decimated polydata a mapper
        deciEyeMapper1.SetInputConnection(decimate.GetOutputPort())

        '----Go through the pipeline for the second eye
        'Convert the polydata to triangles (in the default files they are rectangles)
        triangles.SetInputConnection(eyeData1)
        If (Me.CheckBox1.Checked) Then
            'smooth the polydata
            clean.SetInputConnection(triangles.GetOutputPort())
            smooth.SetInputConnection(clean.GetOutputPort())
            normals.SetInputConnection(smooth.GetOutputPort())
            'connect the smoothed data to a mapper
            sphereTexture.SetInputConnection(normals.GetOutputPort())
            'decimate the smoothed polydata
            eyeMapper2.SetInputConnection(sphereTexture.GetOutputPort())
        Else
            sphereTexture.SetInputConnection(triangles.GetOutputPort())
            'connect the triangle polydata to a mapper before decimation
            eyeMapper2.SetInputConnection(sphereTexture.GetOutputPort())
        End If
        decimate.SetInputConnection(sphereTexture.GetOutputPort())
        decimate.SetTargetReduction(System.Convert.ToDouble(ToolStripTextBox1.Text))
        decimate.SetPreserveTopology(0)
        'connect the decimated polydata a mapper
        deciEyeMapper2.SetInputConnection(decimate.GetOutputPort())

        '----Set the textures and position of the decimated model
        deciAnimalActor.SetMapper(deciAnimalMapper)
        If (Me.CheckBox2.Checked) Then
            deciAnimalActor.SetTexture(deciAnimalColorTexture)
        Else
            deciAnimalActor.SetTexture(Nothing)
        End If
        deciEyeActor1.SetMapper(deciEyeMapper1)
        If (Me.CheckBox2.Checked) Then
            deciEyeActor1.SetTexture(eyeColorTexture1)
            deciEyeActor1.SetTexture(deciEyeColorTexture1)
        Else
            deciEyeActor1.SetTexture(Nothing)
        End If
        deciEyeActor1.SetPosition(eyeX, eyeY, eyeZ)
        deciEyeActor2.SetMapper(deciEyeMapper2)
        If (Me.CheckBox2.Checked) Then
            deciEyeActor2.SetTexture(eyeColorTexture2)
            deciEyeActor2.SetTexture(deciEyeColorTexture2)
        Else
            deciEyeActor2.SetTexture(Nothing)
        End If
        deciEyeActor2.SetPosition(-eyeX, eyeY, eyeZ)

        '----Set the text to the decimated poly count
        'Update the mappers to get the number of polygons
        deciAnimalMapper.Update()
        deciEyeMapper1.Update()
        deciEyeMapper2.Update()
        textAfter.SetInput(((CType(deciAnimalMapper.GetInput(), vtkPolyData).GetNumberOfPolys() + CType(deciEyeMapper1.GetInput(), vtkPolyData).GetNumberOfPolys() + CType(deciEyeMapper2.GetInput(), vtkPolyData).GetNumberOfPolys())).ToString() + " Polygons")
        textAfter.SetDisplayPosition(10, 10)
        '----Set the textures and position of the decimated model
        animalActor.SetMapper(animalMapper)
        If (Me.CheckBox2.Checked) Then
            animalActor.SetTexture(animalColorTexture)
        Else
            animalActor.SetTexture(Nothing)
        End If
        eyeActor1.SetMapper(eyeMapper1)
        If (Me.CheckBox2.Checked) Then
            eyeActor1.SetTexture(eyeColorTexture1)
        Else
            eyeActor1.SetTexture(Nothing)
        End If
        eyeActor1.SetPosition(eyeX, eyeY, eyeZ)
        eyeActor2.SetMapper(eyeMapper2)
        If (Me.CheckBox2.Checked) Then
            eyeActor2.SetTexture(eyeColorTexture2)
        Else
            eyeActor2.SetTexture(Nothing)
        End If
        eyeActor2.SetPosition(-eyeX, eyeY, eyeZ)

        'Update the pipeline to get the number of polygons
        animalMapper.Update()
        eyeMapper1.Update()
        eyeMapper2.Update()
        '----Set the text to the full poly count
        textBefore.SetInput((CType(animalMapper.GetInput(), vtkPolyData).GetNumberOfPolys() + CType(eyeMapper1.GetInput(), vtkPolyData).GetNumberOfPolys() + CType(eyeMapper2.GetInput(), vtkPolyData).GetNumberOfPolys()).ToString() + " Polygons")
        textBefore.SetDisplayPosition(10, 10)
    End Sub
    ' <summary>
    ' Make the camera look at the currently
    ' loaded animal's eye
    ' </summary>
    ' <param name="ren"></param>
    Sub updateCamera(ByRef ren As vtkRenderer)
        'The models are loaded on their bellys so set Z to be up
        ren.GetActiveCamera().SetViewUp(0, 0, 1)
        'look at the center of the animal's head
        ren.GetActiveCamera().SetFocalPoint(0, eyeY, eyeZ)
        'dolly the camera out from the animal's head
        ren.GetActiveCamera().SetPosition(0, eyeY - 3, eyeZ)
        ren.Render()
    End Sub
    ' <summary>
    ' Smooth or unsmooth the animal
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Sub checkBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        'rerun through the pipeline
        updateAnimal()
        'Rerender the window
        RenderWindowControl1.RenderWindow.Render()
        RenderWindowControl2.RenderWindow.Render()
    End Sub
    ' <summary>
    ' Texture or untexture the animal
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Sub checkBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        'update the decimated animal
        updateAnimal()
        'Rerender the window
        RenderWindowControl1.RenderWindow.Render()
        RenderWindowControl2.RenderWindow.Render()
    End Sub

    ' <summary>
    ' Clean up
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Sub Form1_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        DeleteAllVTKObjects()
    End Sub

    ' <summary>
    ' Redecimate the animal
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub ToolStripButton1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton1.Click
        'update the decimated animal
        updateAnimal()
        'Rerender the second window
        RenderWindowControl2.RenderWindow.Render()
    End Sub

    ' <summary>
    ' Show bigbuckbunny.org
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub ToolStripButton2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripButton2.Click
        Dim f As Form2
        f = New Form2
        f.Show()
    End Sub

    ' <summary>
    ' Initialize the render windows
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub Form1_Activated1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        'do this once
        If (rabbitLoaded = False) Then
            'load the model
            loadRabbit()
            'Create the pipeline
            updateAnimal()

            Dim ren As vtkRenderer
            'get the left renderer
            ren = RenderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer()

            'add full poly actors and text
            ren.AddActor(textBefore)
            ren.AddActor(eyeActor1)
            ren.AddActor(eyeActor2)
            ren.AddActor(animalActor)
            'look at the head of the rabbit
            updateCamera(ren)

            'get the right renderer
            ren = RenderWindowControl2.RenderWindow.GetRenderers().GetFirstRenderer()
            'add decimated actors and text
            ren.AddActor2D(textAfter)
            ren.AddActor(deciEyeActor1)
            ren.AddActor(deciEyeActor2)
            ren.AddActor(deciAnimalActor)
            'look at the head of the rabbit
            updateCamera(ren)

            'Add Handlers
            AddHandler RenderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer().GetActiveCamera().ModifiedEvt, AddressOf Camera1_Modified
            AddHandler RenderWindowControl2.RenderWindow.GetRenderers().GetFirstRenderer().GetActiveCamera().ModifiedEvt, AddressOf Camera2_Modified

        End If
    End Sub

    ' <summary>
    ' Change the loaded model
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Private Sub ToolStripComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripComboBox1.SelectedIndexChanged
        If Me.RenderWindowControl1.RenderWindow IsNot Nothing Then
            If (ToolStripComboBox1.Text = "Bunny") Then
                'Load data data into memory
                loadRabbit()
                'Create the pipeline on the loaded data
                updateAnimal()
                'Set up the camera
                updateCamera(RenderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer())
                updateCamera(RenderWindowControl2.RenderWindow.GetRenderers().GetFirstRenderer())
            End If
            If (ToolStripComboBox1.Text = "Chinchilla") Then
                'Load data data into memory
                loadChinchilla()
                'Create the pipeline on the loaded data
                updateAnimal()
                'Set up the camera
                updateCamera(RenderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer())
                updateCamera(RenderWindowControl2.RenderWindow.GetRenderers().GetFirstRenderer())
            End If
            If (ToolStripComboBox1.Text = "Squirrel") Then
                'Load data data into memory
                loadSquirrel()
                'Create the pipeline on the loaded data
                updateAnimal()
                'Set up the camera
                updateCamera(RenderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer())
                updateCamera(RenderWindowControl2.RenderWindow.GetRenderers().GetFirstRenderer())
            End If
            If (ToolStripComboBox1.Text = "Flying Squirrel") Then
                'Load data data into memory
                loadFlyingSquirrel()
                'Create the pipeline on the loaded data
                updateAnimal()
                'Set up the camera
                updateCamera(RenderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer())
                updateCamera(RenderWindowControl2.RenderWindow.GetRenderers().GetFirstRenderer())
            End If
            'Rerender the windows
            RenderWindowControl1.RenderWindow.Render()
            RenderWindowControl2.RenderWindow.Render()
        End If

    End Sub

    Private Sub RenderWindowControl1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenderWindowControl1.Load
        Console.WriteLine("HERE")
    End Sub
    ' <summary>
    ' Slave Camera 1
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Sub Camera2_Modified(ByVal sender As vtkObject, ByVal e As vtkObjectEventArgs)
        'don't execute this if any camera is being modified
        If (ModifyingCamera = False) Then
            ModifyingCamera = True

            Dim camera1 As vtkCamera
            Dim camera2 As vtkCamera

            camera2 = RenderWindowControl2.RenderWindow.GetRenderers().GetFirstRenderer().GetActiveCamera()
            camera1 = RenderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer().GetActiveCamera()

            'Set camera 1's position to camera 2's position
            camera1.SetPosition(camera2.GetPosition()(0), camera2.GetPosition()(1), camera2.GetPosition()(2))
            camera1.SetFocalPoint(camera2.GetFocalPoint()(0), camera2.GetFocalPoint()(1), camera2.GetFocalPoint()(2))
            camera1.SetRoll(camera2.GetRoll())

            RenderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer().ResetCameraClippingRange()
            RenderWindowControl1.RenderWindow.Render()
            ModifyingCamera = False
        End If
    End Sub

    ' <summary>
    ' Slave Camera 2
    ' </summary>
    ' <param name="sender"></param>
    ' <param name="e"></param>
    Sub Camera1_Modified(ByVal sender As vtkObject, ByVal e As vtkObjectEventArgs)
        'don't execute this if any camera is being modified
        If (ModifyingCamera = False) Then
            ModifyingCamera = True

            Dim camera1 As vtkCamera
            Dim camera2 As vtkCamera

            camera1 = RenderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer().GetActiveCamera()
            camera2 = RenderWindowControl2.RenderWindow.GetRenderers().GetFirstRenderer().GetActiveCamera()

            'Set camera 2's position to camera 1's position
            camera2.SetPosition(camera1.GetPosition()(0), camera1.GetPosition()(1), camera1.GetPosition()(2))
            camera2.SetFocalPoint(camera1.GetFocalPoint()(0), camera1.GetFocalPoint()(1), camera1.GetFocalPoint()(2))
            camera2.SetRoll(camera1.GetRoll())

            RenderWindowControl2.RenderWindow.GetRenderers().GetFirstRenderer().ResetCameraClippingRange()
            RenderWindowControl2.RenderWindow.Render()
            ModifyingCamera = False
        End If
    End Sub

    Private Sub ToolStripTextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ToolStripTextBox1.KeyPress

    End Sub
End Class
