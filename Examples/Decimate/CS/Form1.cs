using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Kitware.VTK;

namespace Decimate
{
    /// <summary>
    /// This is an example that shows use of the vtkDecimatePro filter.
    /// It also shows how to map textures using predefined
    /// UV coordinates in a vtk file, make your own texture
    /// coordinates using vtkTextureMapToSphere, and smooth
    /// polydata.
    /// </summary>
    public partial class Form1 : Form
    {
        //Polydata files for the models
        vtkAlgorithmOutput animalData;
        vtkAlgorithmOutput eyeData1;
        vtkAlgorithmOutput eyeData2;

        //textures for the models
        vtkTexture animalColorTexture = vtkTexture.New();
        vtkTexture eyeColorTexture1 = vtkTexture.New();
        vtkTexture eyeColorTexture2 = vtkTexture.New();
        
        vtkTexture deciAnimalColorTexture = vtkTexture.New();
        vtkTexture deciEyeColorTexture1 = vtkTexture.New();
        vtkTexture deciEyeColorTexture2 = vtkTexture.New();

        //full polygon actors 
        vtkActor animalActor = vtkActor.New();
        vtkActor eyeActor1 = vtkActor.New();
        vtkActor eyeActor2 = vtkActor.New();

        //decimated actors
        vtkActor deciAnimalActor = vtkActor.New();
        vtkActor deciEyeActor1 = vtkActor.New();
        vtkActor deciEyeActor2 = vtkActor.New();
        
        //text showing number of polygons in each window
        vtkTextActor textBefore = vtkTextActor.New();
        vtkTextActor textAfter = vtkTextActor.New();

        //decimated mappers
        vtkDataSetMapper deciAnimalMapper = vtkDataSetMapper.New();
        vtkMapper deciEyeMapper1 = vtkDataSetMapper.New();
        vtkMapper deciEyeMapper2 = vtkDataSetMapper.New();
        
        //full poly mappers
        vtkDataSetMapper animalMapper = vtkDataSetMapper.New();
        vtkMapper eyeMapper1 = vtkDataSetMapper.New();
        vtkMapper eyeMapper2 = vtkDataSetMapper.New();

        //filters for the body model
        vtkTriangleFilter triangleAnimal = vtkTriangleFilter.New();
        vtkDecimatePro decimateAnimal = vtkDecimatePro.New();
        vtkCleanPolyData cleanAnimal = vtkCleanPolyData.New();
        vtkWindowedSincPolyDataFilter smoothAnimal = vtkWindowedSincPolyDataFilter.New();
        vtkPolyDataNormals normalsAnimal = vtkPolyDataNormals.New();

        //filters for the eye models
        vtkTriangleFilter triangles = vtkTriangleFilter.New();
        vtkDecimatePro decimate = vtkDecimatePro.New();
        vtkCleanPolyData clean = vtkCleanPolyData.New();
        vtkWindowedSincPolyDataFilter smooth = vtkWindowedSincPolyDataFilter.New();
        vtkPolyDataNormals normals = vtkPolyDataNormals.New();
        vtkTextureMapToSphere sphereTexture = vtkTextureMapToSphere.New();

        //position of the left eye
        double eyeX = 0;
        double eyeY = 0;
        double eyeZ = 0;

        //makes sure the readers only read once
        bool rabbitLoaded = false;
        bool squirrelLoaded = false;
        bool flyingSquirrelLoaded = false;
        bool chinchillaLoaded = false;

        //Don't let Camera_Modified trigger itself
        bool ModifyingCamera = false;

        //Readers for the models and the textures
        vtkDataSetReader rabbitReader = vtkDataSetReader.New();
        vtkDataSetReader eyeReader = vtkDataSetReader.New();
        vtkPNGReader rabbitColorReader = vtkPNGReader.New();
        vtkPNGReader eyeColorReader = vtkPNGReader.New();
        vtkDataSetReader squirrelReader = vtkDataSetReader.New();
        vtkDataSetReader squirrelEyeReader = vtkDataSetReader.New();
        vtkDataSetReader squirrelEyeReader2 = vtkDataSetReader.New();
        vtkPNGReader squirrelColorReader = vtkPNGReader.New();
        vtkPNGReader squirrelEyeColorReader = vtkPNGReader.New();
        vtkPNGReader squirrelEyeColorReader2 = vtkPNGReader.New();
        vtkDataSetReader flyingSquirrelReader = vtkDataSetReader.New();
        vtkDataSetReader flyingSquirreleyeReader = vtkDataSetReader.New();
        vtkPNGReader flyingSquirrelColorReader = vtkPNGReader.New();
        vtkPNGReader flyingSquirrelEyeColorReader = vtkPNGReader.New();
        vtkDataSetReader chinchillaReader = vtkDataSetReader.New();
        vtkDataSetReader chinchillaEyeReader = vtkDataSetReader.New();
        vtkPNGReader chinchillaColorReader = vtkPNGReader.New();
        vtkPNGReader chinchillaEyeColorReader = vtkPNGReader.New();

        /// <summary>
        /// Loads the Rabbit model and textures
        /// into the algorithms and textures
        /// </summary>
        public void loadRabbit()
        {
            //Set a predefined position for the eyes
            //that matches the .blend file
            eyeX = 0.057;
            eyeY = -0.311;
            eyeZ = 1.879;
           
            //load the rabbit model and textures if 
            //they are not already loaded
            if (!rabbitLoaded)
            {
                rabbitReader.SetFileName("../../../models/rabbit.vtk");
                rabbitReader.Update();
                eyeReader.SetFileName("../../../models/rabbit_eye.vtk");
                eyeReader.Update();
                rabbitColorReader.SetFileName("../../../textures/rabbit_skin_col.png");
                rabbitColorReader.Update();
                eyeColorReader.SetFileName("../../../textures/rabbit_eye.png");
                eyeColorReader.Update();
                rabbitLoaded = true;
            }
            //Set the algorithms and textures to the
            //ouput of the readers
            animalData = rabbitReader.GetOutputPort();

            eyeData1 = eyeReader.GetOutputPort();
            eyeData2 = eyeData1;

            animalColorTexture.InterpolateOn();
            animalColorTexture.SetInput(rabbitColorReader.GetOutput());

            deciAnimalColorTexture.InterpolateOn();
            deciAnimalColorTexture.SetInput(rabbitColorReader.GetOutput());

            eyeColorTexture1.InterpolateOn();
            eyeColorTexture1.SetInput(eyeColorReader.GetOutput());

            deciEyeColorTexture1.InterpolateOn();
            deciEyeColorTexture1.SetInput(eyeColorReader.GetOutput());

            eyeColorTexture2.InterpolateOn();
            eyeColorTexture2.SetInput(eyeColorReader.GetOutput());

            deciEyeColorTexture2.InterpolateOn();
            deciEyeColorTexture2.SetInput(eyeColorReader.GetOutput());
        }
        /// <summary>
        /// Loads the Squirrel model and textures
        /// into the algorithms and textures
        /// </summary>
        public void loadSquirrel()
        {
            //Set a predefined position for the eyes
            //that matches the .blend file
            eyeX = 0.076;
            eyeY = -0.178;
            eyeZ = 0.675;

            //load the squirrel model and textures if 
            //they are not already loaded
            if (!squirrelLoaded)
            {
                squirrelReader.SetFileName("../../../models/squirrel.vtk");
                squirrelReader.Update();
                squirrelEyeReader.SetFileName("../../../models/squirrel_eyeR.vtk");
                squirrelEyeReader.Update();
                squirrelEyeReader2.SetFileName("../../../models/squirrel_eyeL.vtk");
                squirrelEyeReader2.Update();
                squirrelColorReader.SetFileName("../../../textures/squirrel_skin_col.png");
                squirrelColorReader.Update();
                squirrelEyeColorReader.SetFileName("../../../textures/squirrel_eyeR.png");
                squirrelEyeColorReader.Update();
                squirrelEyeColorReader2.SetFileName("../../../textures/squirrel_eyeL.png");
                squirrelEyeColorReader2.Update();
                squirrelLoaded = true;
            }

            //Set the algorithms and textures to the
            //ouput of the readers
            eyeColorTexture1.InterpolateOn();
            eyeColorTexture1.SetInput(squirrelEyeColorReader.GetOutput());

            deciEyeColorTexture1.InterpolateOn();
            deciEyeColorTexture1.SetInput(squirrelEyeColorReader.GetOutput());

            eyeColorTexture2.InterpolateOn();
            eyeColorTexture2.SetInput(squirrelEyeColorReader2.GetOutput());

            deciEyeColorTexture2.InterpolateOn();
            deciEyeColorTexture2.SetInput(squirrelEyeColorReader2.GetOutput());

            animalColorTexture.InterpolateOn();
            animalColorTexture.SetInput(squirrelColorReader.GetOutput());

            deciAnimalColorTexture.InterpolateOn();
            deciAnimalColorTexture.SetInput(squirrelColorReader.GetOutput());

            eyeData2 = squirrelEyeReader2.GetOutputPort();
            eyeData1 = squirrelEyeReader.GetOutputPort();

            animalData = squirrelReader.GetOutputPort();
        }
        /// <summary>
        /// Loads the Flying Squirrel model and textures
        /// into the algorithms and textures
        /// </summary>
        public void loadFlyingSquirrel()
        {
            //Set a predefined position for the eyes
            //that matches the .blend file
            eyeX = 0.054;
            eyeY = -0.189;
            eyeZ = 0.427;

            //load the flyingsquirrel model and textures if 
            //they are not already loaded
            if (!flyingSquirrelLoaded)
            {
                flyingSquirrelReader.SetFileName("../../../models/flyingsquirrel.vtk");
                flyingSquirrelReader.Update();
                flyingSquirreleyeReader.SetFileName("../../../models/flyingsquirrel_eye.vtk");
                flyingSquirreleyeReader.Update();
                flyingSquirrelColorReader.SetFileName("../../../textures/flyingsquirrel_skin_col.png");
                flyingSquirrelColorReader.Update();
                flyingSquirrelEyeColorReader.SetFileName("../../../textures/flyingsquirrel_eye.png");
                flyingSquirrelEyeColorReader.Update();
                flyingSquirrelLoaded = true;
            }

            //Set the algorithms and textures to the
            //ouput of the readers
            animalData = flyingSquirrelReader.GetOutputPort();

            eyeData1 = flyingSquirreleyeReader.GetOutputPort();
            eyeData2 = eyeData1;

            animalColorTexture.InterpolateOn();
            animalColorTexture.SetInput(flyingSquirrelColorReader.GetOutput());
            
            deciAnimalColorTexture.InterpolateOn();
            deciAnimalColorTexture.SetInput(flyingSquirrelColorReader.GetOutput());
            
            eyeColorTexture1.InterpolateOn();
            eyeColorTexture1.SetInput(flyingSquirrelEyeColorReader.GetOutput());
            
            deciEyeColorTexture1.InterpolateOn();
            deciEyeColorTexture1.SetInput(flyingSquirrelEyeColorReader.GetOutput());
            
            eyeColorTexture2.InterpolateOn();
            eyeColorTexture2.SetInput(flyingSquirrelEyeColorReader.GetOutput());
            
            deciEyeColorTexture2.InterpolateOn();
            deciEyeColorTexture2.SetInput(flyingSquirrelEyeColorReader.GetOutput());   
        }
        /// <summary>
        /// Loads the Chinchilla model and textures
        /// into the algorithms and textures
        /// </summary>
        public void loadChinchilla()
        {
            //Set a predefined position for the eyes
            //that matches the .blend file
            eyeX = 0.052;
            eyeY = -0.144;
            eyeZ = 0.424;

            //load the chinchilla model and textures if 
            //they are not already loaded
            if (!chinchillaLoaded)
            {
                chinchillaReader.SetFileName("../../../models/chinchilla.vtk");
                chinchillaReader.Update();
                chinchillaEyeReader.SetFileName("../../../models/chinchilla_eye.vtk");
                chinchillaEyeReader.Update();
                chinchillaColorReader.SetFileName("../../../textures/chinchilla_skin_col.png");
                chinchillaColorReader.Update();
                chinchillaEyeColorReader.SetFileName("../../../textures/chinchilla_eye.png");
                chinchillaEyeColorReader.Update();
                chinchillaLoaded = true;
            }

            //Set the algorithms and textures to the
            //ouput of the readers
            animalData = chinchillaReader.GetOutputPort();

            eyeData1 = chinchillaEyeReader.GetOutputPort();
            eyeData2 = eyeData1;

            animalColorTexture.InterpolateOn();
            animalColorTexture.SetInput(chinchillaColorReader.GetOutput());

            deciAnimalColorTexture.InterpolateOn();
            deciAnimalColorTexture.SetInput(chinchillaColorReader.GetOutput());

            eyeColorTexture1.InterpolateOn();
            eyeColorTexture1.SetInput(chinchillaEyeColorReader.GetOutput());

            deciEyeColorTexture1.InterpolateOn();
            deciEyeColorTexture1.SetInput(chinchillaEyeColorReader.GetOutput());

            eyeColorTexture2.InterpolateOn();
            eyeColorTexture2.SetInput(chinchillaEyeColorReader.GetOutput());

            deciEyeColorTexture2.InterpolateOn();
            deciEyeColorTexture2.SetInput(chinchillaEyeColorReader.GetOutput());
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();   
        }

        /// <summary>
        /// Changes the actors to whatever the 
        /// animal currently loaded is
        /// </summary>
        public void updateAnimal()
        {
            //----Go through the pipeline for the animal body

            //Convert the polydata to triangles (in the default files they are rectangles)
            triangleAnimal.SetInputConnection(animalData);

            if (this.checkBox1.Checked)
            {
                //smooth the polydata
                cleanAnimal.SetInputConnection(triangleAnimal.GetOutputPort());
                smoothAnimal.SetInputConnection(cleanAnimal.GetOutputPort());
                normalsAnimal.SetInputConnection(smoothAnimal.GetOutputPort());
                //connect the smoothed data to a mapper
                animalMapper.SetInputConnection(normalsAnimal.GetOutputPort());
                //decimate the smoothed polydata
                decimateAnimal.SetInputConnection(normalsAnimal.GetOutputPort());
            }
            else
            {
                //connect the triangle polydata to a mapper before decimation
                animalMapper.SetInputConnection(triangleAnimal.GetOutputPort());
                //decimate the triangled data
                decimateAnimal.SetInputConnection(triangleAnimal.GetOutputPort());
            }

            decimateAnimal.SetTargetReduction(System.Convert.ToDouble(toolStripTextBox1.Text));
            decimateAnimal.SetPreserveTopology(0);
            //connect the decimated polydata a mapper
            deciAnimalMapper.SetInputConnection(decimateAnimal.GetOutputPort());
            
            //----Go through the pipeline for the first eye

            //Convert the polydata to triangles (in the default files they are rectangles)
            triangles.SetInputConnection(eyeData1);
            if (this.checkBox1.Checked)
            {
                //smooth the polydata
                clean.SetInputConnection(triangles.GetOutputPort());
                smooth.SetInputConnection(clean.GetOutputPort());
                normals.SetInputConnection(smooth.GetOutputPort());
                //connect the smoothed data to a mapper
                sphereTexture.SetInputConnection(normals.GetOutputPort());
                //decimate the smoothed polydata
                eyeMapper1.SetInputConnection(sphereTexture.GetOutputPort());
            }
            else
            {
                sphereTexture.SetInputConnection(triangles.GetOutputPort());
                //connect the triangle polydata to a mapper before decimation
                eyeMapper1.SetInputConnection(sphereTexture.GetOutputPort());
            }
            decimate.SetInputConnection(sphereTexture.GetOutputPort());
            decimate.SetTargetReduction(System.Convert.ToDouble(toolStripTextBox1.Text));
            decimate.SetPreserveTopology(0);
            //connect the decimated polydata a mapper
            deciEyeMapper1.SetInputConnection(decimate.GetOutputPort());
            //----Go through the pipeline for the second eye

            //Convert the polydata to triangles (in the default files they are rectangles)
            triangles.SetInputConnection(eyeData1);
            if (this.checkBox1.Checked)
            {
                //smooth the polydata
                clean.SetInputConnection(triangles.GetOutputPort());
                smooth.SetInputConnection(clean.GetOutputPort());
                normals.SetInputConnection(smooth.GetOutputPort());
                //connect the smoothed data to a mapper
                sphereTexture.SetInputConnection(normals.GetOutputPort());
                //decimate the smoothed polydata
                eyeMapper2.SetInputConnection(sphereTexture.GetOutputPort());
            }
            else
            {
                sphereTexture.SetInputConnection(triangles.GetOutputPort());
                //connect the triangle polydata to a mapper before decimation
                eyeMapper2.SetInputConnection(sphereTexture.GetOutputPort()); 
            }
            decimate.SetInputConnection(sphereTexture.GetOutputPort());
            decimate.SetTargetReduction(System.Convert.ToDouble(toolStripTextBox1.Text));
            decimate.SetPreserveTopology(0);
            //connect the decimated polydata a mapper
            deciEyeMapper2.SetInputConnection(decimate.GetOutputPort());

            //----Set the textures and position of the decimated model
            deciAnimalActor.SetMapper(deciAnimalMapper);
            if (this.checkBox2.Checked)
            {
                deciAnimalActor.SetTexture(deciAnimalColorTexture);
            }
            else
            {
                deciAnimalActor.SetTexture(null);
            }
            deciEyeActor1.SetMapper(deciEyeMapper1);
            if (this.checkBox2.Checked)
            {
                deciEyeActor1.SetTexture(eyeColorTexture1);
                deciEyeActor1.SetTexture(deciEyeColorTexture1);
            }
            else
            {
                deciEyeActor1.SetTexture(null);
            }
            deciEyeActor1.SetPosition(eyeX, eyeY, eyeZ);
            deciEyeActor2.SetMapper(deciEyeMapper2);
            if (this.checkBox2.Checked)
            {
                deciEyeActor2.SetTexture(eyeColorTexture2);
                deciEyeActor2.SetTexture(deciEyeColorTexture2);
            }
            else
            {
                deciEyeActor2.SetTexture(null);
            }
            deciEyeActor2.SetPosition(-eyeX, eyeY, eyeZ);

            //----Set the text to the decimated poly count

            //Update the mappers to get the number of polygons
            deciAnimalMapper.Update();
            deciEyeMapper1.Update();
            deciEyeMapper2.Update();
            textAfter.SetInput(((((vtkPolyData)deciAnimalMapper.GetInput()).GetNumberOfPolys() + ((vtkPolyData)deciEyeMapper1.GetInput()).GetNumberOfPolys() + ((vtkPolyData)deciEyeMapper2.GetInput()).GetNumberOfPolys())).ToString() + " Polygons");
            textAfter.SetDisplayPosition(10, 10);
            //----Set the textures and position of the decimated model
            animalActor.SetMapper(animalMapper);
            if (this.checkBox2.Checked)
            {
                animalActor.SetTexture(animalColorTexture);
            }
            else
            {
                animalActor.SetTexture(null);
            }
            eyeActor1.SetMapper(eyeMapper1);
            if (this.checkBox2.Checked)
            {
                eyeActor1.SetTexture(eyeColorTexture1);
            }
            else
            {
                eyeActor1.SetTexture(null);
            }
            eyeActor1.SetPosition(eyeX, eyeY, eyeZ);

            eyeActor2.SetMapper(eyeMapper2);
            if (this.checkBox2.Checked)
            {
                eyeActor2.SetTexture(eyeColorTexture2);
            }
            else
            {
                eyeActor2.SetTexture(null);
            }
            eyeActor2.SetPosition(-eyeX, eyeY, eyeZ);
            
            //Update the pipeline to get the number of polygons
            animalMapper.Update();
            eyeMapper1.Update();
            eyeMapper2.Update();
            //----Set the text to the full poly count
            textBefore.SetInput((((vtkPolyData)animalMapper.GetInput()).GetNumberOfPolys() + ((vtkPolyData)eyeMapper1.GetInput()).GetNumberOfPolys() + ((vtkPolyData)eyeMapper2.GetInput()).GetNumberOfPolys()).ToString() + " Polygons");
            textBefore.SetDisplayPosition(10, 10);
        }
        /// <summary>
        /// Make the camera look at the currently
        /// loaded animal's eye
        /// </summary>
        /// <param name="ren"></param>
        public void updateCamera(vtkRenderer ren)
        {
            //The models are loaded on their bellys so set Z to be up
            ren.GetActiveCamera().SetViewUp(0, 0, 1);
            //look at the center of the animal's head
            ren.GetActiveCamera().SetFocalPoint(0, eyeY, eyeZ);
            //dolly the camera out from the animal's head
            ren.GetActiveCamera().SetPosition(0, eyeY - 3, eyeZ);
            ren.Render();
        }
        /// <summary>
        /// Smooth or unsmooth the animal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //rerun through the pipeline
            updateAnimal();
            //Rerender the window
            renderWindowControl1.RenderWindow.Render();
            renderWindowControl2.RenderWindow.Render();
        }
        /// <summary>
        /// Texture or untexture the animal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //update the decimated animal
            updateAnimal();
            //Rerender the window
            renderWindowControl1.RenderWindow.Render();
            renderWindowControl2.RenderWindow.Render();
        }

        /// <summary>
        /// Redecimate the animal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //update the decimated animal
            updateAnimal();
            //Rerender the second window
            renderWindowControl2.RenderWindow.Render();
        }
        /// <summary>
        /// Show bigbuckbunny.org
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            new Form2().Show();
        }
        /// <summary>
        /// Clean up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeleteAllVTKObjects();
        }

        /// <summary>
        /// Change the loaded model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox1.Text == "Bunny")
            {
                //Load data data into memory
                loadRabbit();
                //Create the pipeline on the loaded data
                updateAnimal();
                //Set up the camera
                updateCamera(renderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer());
                updateCamera(renderWindowControl2.RenderWindow.GetRenderers().GetFirstRenderer());
            }
            if (toolStripComboBox1.Text == "Chinchilla")
            {
                //Load data data into memory
                loadChinchilla();
                //Create the pipeline on the loaded data
                updateAnimal();
                //Set up the camera
                updateCamera(renderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer());
                updateCamera(renderWindowControl2.RenderWindow.GetRenderers().GetFirstRenderer());
            }
            if (toolStripComboBox1.Text == "Squirrel")
            {
                //Load data data into memory
                loadSquirrel();
                //Create the pipeline on the loaded data
                updateAnimal();
                //Set up the camera
                updateCamera(renderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer());
                updateCamera(renderWindowControl2.RenderWindow.GetRenderers().GetFirstRenderer());
            }
            if (toolStripComboBox1.Text == "Flying Squirrel")
            {
                //Load data data into memory
                loadFlyingSquirrel();
                //Create the pipeline on the loaded data
                updateAnimal();
                //Set up the camera
                updateCamera(renderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer());
                updateCamera(renderWindowControl2.RenderWindow.GetRenderers().GetFirstRenderer());
            }
            //Rerender the windows
            renderWindowControl1.RenderWindow.Render();
            renderWindowControl2.RenderWindow.Render();
        }
        /// <summary>
        /// Initialize the render windows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Activated(object sender, EventArgs e)
        {
            //do this once on startup
            if (!rabbitLoaded)
            {
                //load the model
                loadRabbit();
                //Create the pipeline
                updateAnimal();

                //get the left renderer
                vtkRenderer ren = renderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer();
      
                //add full poly actors and text
                ren.AddActor(textBefore);
                ren.AddActor(eyeActor1);
                ren.AddActor(eyeActor2);
                ren.AddActor(animalActor);
                //look at the head of the rabbit
                updateCamera(ren);

                //get the right renderer
                ren = renderWindowControl2.RenderWindow.GetRenderers().GetFirstRenderer();
                //add decimated actors and text
                ren.AddActor2D(textAfter);
                ren.AddActor(deciEyeActor1);
                ren.AddActor(deciEyeActor2);
                ren.AddActor(deciAnimalActor);
                //look at the head of the rabbit
                updateCamera(ren);

                //Add Handlers
                renderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer().GetActiveCamera().ModifiedEvt += new vtkObject.vtkObjectEventHandler(Camera1_Modified);
                renderWindowControl2.RenderWindow.GetRenderers().GetFirstRenderer().GetActiveCamera().ModifiedEvt += new vtkObject.vtkObjectEventHandler(Camera2_Modified);
            }
        }
        /// <summary>
        /// Slave camera 1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Camera2_Modified(vtkObject sender, vtkObjectEventArgs e)
        {
            //don't execute this if any camera is being modified
            if (!ModifyingCamera)
            {
                ModifyingCamera = true;

                vtkCamera camera1;
                vtkCamera camera2;

                camera2 = renderWindowControl2.RenderWindow.GetRenderers().GetFirstRenderer().GetActiveCamera();
                camera1 = renderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer().GetActiveCamera();
               
                //Set camera 1's position to camera 2's position
                camera1.SetPosition(camera2.GetPosition()[0], camera2.GetPosition()[1], camera2.GetPosition()[2]);
                camera1.SetFocalPoint(camera2.GetFocalPoint()[0], camera2.GetFocalPoint()[1], camera2.GetFocalPoint()[2]);
                camera1.SetRoll(camera2.GetRoll());

                renderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer().ResetCameraClippingRange();
                renderWindowControl1.RenderWindow.Render();
                ModifyingCamera = false;
            }
        }
        /// <summary>
        /// Slave camera 2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Camera1_Modified(vtkObject sender, vtkObjectEventArgs e)
        {
            //don't execute this if any camera is being modified
            if (!ModifyingCamera)
            {
                ModifyingCamera = true;

                vtkCamera camera1;
                vtkCamera camera2;
                
                camera1 = renderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer().GetActiveCamera();
                camera2 = renderWindowControl2.RenderWindow.GetRenderers().GetFirstRenderer().GetActiveCamera();

                //Set camera 2's position to camera 1's position
                camera2.SetPosition(camera1.GetPosition()[0], camera1.GetPosition()[1], camera1.GetPosition()[2]);
                camera2.SetFocalPoint(camera1.GetFocalPoint()[0], camera1.GetFocalPoint()[1], camera1.GetFocalPoint()[2]);
                camera2.SetRoll(camera1.GetRoll());

                renderWindowControl2.RenderWindow.GetRenderers().GetFirstRenderer().ResetCameraClippingRange();
                renderWindowControl2.RenderWindow.Render();
                ModifyingCamera = false;
            }
        }
        /// <summary>
        /// Listen for the enter key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                this.toolStripButton1_Click(sender,e);
            }
        }

        /// <summary>
        /// Clean up the global variables
        /// </summary>
        public void DeleteAllVTKObjects()
        {
            animalActor.Dispose();
            deciAnimalActor.Dispose();
            eyeActor1.Dispose();
            eyeActor2.Dispose();
            deciEyeActor1.Dispose();
            deciEyeActor2.Dispose();
            textAfter.Dispose();
            textBefore.Dispose();

            deciAnimalMapper.Dispose();
            deciEyeMapper1.Dispose();
            deciEyeMapper2.Dispose();
            cleanAnimal.Dispose();
            smoothAnimal.Dispose();
            normalsAnimal.Dispose();
            triangleAnimal.Dispose();
            decimateAnimal.Dispose();

            animalMapper.Dispose();
            eyeMapper1.Dispose();
            eyeMapper2.Dispose();
            clean.Dispose();
            smooth.Dispose();
            normals.Dispose();
            triangles.Dispose();
            decimate.Dispose();

            rabbitReader.Dispose();
            eyeReader.Dispose();
            rabbitColorReader.Dispose();
            eyeColorReader.Dispose();
            squirrelReader.Dispose();
            squirrelEyeReader.Dispose();
            squirrelEyeReader2.Dispose();
            squirrelColorReader.Dispose();
            squirrelEyeColorReader.Dispose();
            squirrelEyeColorReader2.Dispose();
            flyingSquirrelReader.Dispose();
            flyingSquirreleyeReader.Dispose();
            flyingSquirrelColorReader.Dispose();
            flyingSquirrelEyeColorReader.Dispose();
            chinchillaReader.Dispose();
            chinchillaEyeReader.Dispose();
            chinchillaColorReader.Dispose();
            chinchillaEyeColorReader.Dispose();
        }
    }
}
