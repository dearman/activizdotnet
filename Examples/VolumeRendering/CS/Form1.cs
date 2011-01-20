using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Kitware.VTK;
namespace VolumeRendering
{
    /// <summary>
    /// Application to load and display a volume
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool MouseDown1 = false;
        private vtkRenderWindowInteractor Interactor = null;
        private vtkRenderWindow RenderWindow = null;
        private vtkRenderer Renderer = null;
        private vtkImageActor ImageActor = null;
        private vtkImageClip Clip = null;
        string fileName = "";

        /// <summary>
        /// Tell the application when the mouse is being dragged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void iren_LeftButtonReleaseEvt(vtkObject sender, vtkObjectEventArgs e)
        {
            this.MouseDown1 = false;
        }
        /// <summary>
        /// Tell the application when the mouse is being dragged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void iren_LeftButtonPressEvt(vtkObject sender, vtkObjectEventArgs e)
        {
            this.MouseDown1 = true;
        }
        
        /// <summary>
        /// Display the render window with the 3D Volume in it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renderWindowControl2_Load(object sender, EventArgs e)
        {
            //Create all the objects for the pipeline
            vtkRenderer renderer = renderWindowControl2.RenderWindow.GetRenderers().GetFirstRenderer();
            vtkXMLImageDataReader reader = vtkXMLImageDataReader.New();
            vtkFixedPointVolumeRayCastMapper texMapper = vtkFixedPointVolumeRayCastMapper.New();
            vtkVolume vol = vtkVolume.New();
            vtkColorTransferFunction ctf = vtkColorTransferFunction.New();
            vtkPiecewiseFunction spwf = vtkPiecewiseFunction.New();
            vtkPiecewiseFunction gpwf = vtkPiecewiseFunction.New();
            
            //Read in the file
            reader.SetFileName(fileName);
            reader.Update();
            
            //Go through the visulizatin pipeline
            texMapper.SetInputConnection(reader.GetOutputPort());
            
            //Set the color curve for the volume
            ctf.AddHSVPoint(0, .67, .07, 1);
            ctf.AddHSVPoint(94, .67, .07, 1);
            ctf.AddHSVPoint(139, 0, 0, 0);
            ctf.AddHSVPoint(160, .28, .047, 1);
            ctf.AddHSVPoint(254, .38, .013, 1);
            
            //Set the opacity curve for the volume
            spwf.AddPoint(84, 0);
            spwf.AddPoint(151, .1);
            spwf.AddPoint(255, 1);
            
            //Set the gradient curve for the volume
            gpwf.AddPoint(0, .2);
            gpwf.AddPoint(10, .2);
            gpwf.AddPoint(25, 1);
            
            vol.GetProperty().SetColor(ctf);
            vol.GetProperty().SetScalarOpacity(spwf);
            vol.GetProperty().SetGradientOpacity(gpwf);
            
            vol.SetMapper(texMapper);

            //Go through the Graphics Pipeline
            renderer.AddVolume(vol);
        }
        
        
        /// <summary>
        /// Display the render window with the slice in it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renderWindowControl1_Load(object sender, EventArgs e)
        {
            //Get the name of the Unsigned Char volume that you want to load
            fileName = "../../../head.vti";
  
            //Create all the objects for the pipeline
            vtkXMLImageDataReader reader = vtkXMLImageDataReader.New();
            vtkImageActor iactor = vtkImageActor.New();
            vtkImageClip clip = vtkImageClip.New();
            vtkContourFilter contour = vtkContourFilter.New();
            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            vtkActor actor = vtkActor.New();
            vtkInteractorStyleImage style = vtkInteractorStyleImage.New();
            
            vtkRenderer renderer = renderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer();

            //Read the Image
            reader.SetFileName(fileName);
            
            //Go through the visulization pipeline
            iactor.SetInput(reader.GetOutput());
            renderer.AddActor(iactor);
            reader.Update();
            int[] extent = reader.GetOutput().GetWholeExtent();
            iactor.SetDisplayExtent(extent[0], extent[1], extent[2], extent[3],
                        (extent[4] + extent[5]) / 2,
                        (extent[4] + extent[5]) / 2);
            
            clip.SetInputConnection(reader.GetOutputPort());
            clip.SetOutputWholeExtent(extent[0], extent[1], extent[2], extent[3],
                        (extent[4] + extent[5]) / 2,
                        (extent[4] + extent[5]) / 2);
            
            contour.SetInputConnection(clip.GetOutputPort());
            contour.SetValue(0, 100);
           
            mapper.SetInputConnection(contour.GetOutputPort());
            mapper.SetScalarVisibility(1);
   
            //Go through the graphics pipeline
            actor.SetMapper(mapper);
            actor.GetProperty().SetColor(0, 1, 0);

            renderer.AddActor(actor);
            
            //Give a new style to the interactor
            vtkRenderWindowInteractor iren = renderWindowControl1.RenderWindow.GetInteractor();
            iren.SetInteractorStyle(style);

            //Add new events to the interactor style
            style.LeftButtonPressEvt += new vtkObject.vtkObjectEventHandler(iren_LeftButtonPressEvt);
            style.LeftButtonReleaseEvt += new vtkObject.vtkObjectEventHandler(iren_LeftButtonReleaseEvt);
            style.MouseMoveEvt += new vtkObject.vtkObjectEventHandler(iren_MouseMoveEvt);

            //Update global variables
            this.trackBar1.Maximum = extent[5];
            this.trackBar1.Minimum = extent[4];
            this.Interactor = iren;
            this.RenderWindow = renderWindowControl1.RenderWindow;
            this.Renderer = renderer;
            this.Clip = clip;
            this.ImageActor = iactor;
        }
        
        /// <summary>
        /// Move the slice when the trackbar is moved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (ImageActor != null)
            {
                int[] lastPos = this.Interactor.GetLastEventPosition();
                int[] size = this.RenderWindow.GetSize();
                int[] dim = this.ImageActor.GetInput().GetDimensions();

                int newSlice = (int)(trackBar1.Value);

                if (newSlice >= 0 && newSlice < dim[2])
                {
                    this.Clip.SetOutputWholeExtent(0, dim[0] - 1, 0, dim[1] - 1, newSlice, newSlice);
                    this.ImageActor.SetDisplayExtent(0, dim[0] - 1, 0, dim[1] - 1, newSlice, newSlice);
                    this.Renderer.ResetCameraClippingRange();
                    this.RenderWindow.Render();
                }
            }
        }
         
        /// <summary>
        /// Move the slice and the trackbar when the mouse is dragged on the render window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void iren_MouseMoveEvt(vtkObject sender, vtkObjectEventArgs e)
        {
            if (this.MouseDown1 && this.ImageActor != null)
            {
                int[] lastPos = this.Interactor.GetLastEventPosition();
                int[] size = this.RenderWindow.GetSize();
                int[] dim = this.ImageActor.GetInput().GetDimensions();

                int newSlice = (int)((double)(dim[2] - 1.0) * (double)(lastPos[1]) / (double)(size[1]));
               
                if (newSlice >= 0 && newSlice < dim[2])
                {
                    this.trackBar1.Value = newSlice;
                    this.Clip.SetOutputWholeExtent(0, dim[0] - 1, 0, dim[1] - 1, newSlice, newSlice);
                    this.ImageActor.SetDisplayExtent(0, dim[0] - 1, 0, dim[1] - 1, newSlice, newSlice);
                    this.Renderer.ResetCameraClippingRange();
                    this.RenderWindow.Render();
                }
            }
        }
        
        /// <summary>
        /// Clean Up any global variables that might still be around
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Closed(object sender, FormClosedEventArgs e)
        {
            if (this.Interactor != null)
            {
                this.Interactor.Dispose();
            }
            if (this.ImageActor != null)
            {
                this.ImageActor.Dispose();
            }
            if (this.Clip != null)
            {
                this.Clip.Dispose();
            }
        }
    }
}
