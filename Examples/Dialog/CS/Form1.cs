using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Kitware.VTK;

namespace Dialog
{
    /// <summary>
    /// Simple Example that loads images and displays them
    /// </summary>
    public partial class Form1 : Form
    {
        public vtkProp3D imgProp = null;

        /// <summary>
        /// Constructor for the form
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load an image with an open dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Get the name of the file you want to open from the dialog 
                string fileName = openFileDialog1.FileName;

                vtkRenderer ren = (vtkRenderer)this.renderWindowControl1.RenderWindow.GetRenderers().GetItemAsObject(0);

                //Get rid of any props already there
                if (imgProp != null)
                {
                    ren.RemoveActor(imgProp);
                    imgProp.Dispose();
                    imgProp = null;
                }

                //Look at known file types to see if they are readable
                if (fileName.Contains(".png")
                    || fileName.Contains(".jpg")
                    || fileName.Contains(".jpeg")
                    || fileName.Contains(".tif")
                    || fileName.Contains(".slc")
                    || fileName.Contains(".dicom")
                    || fileName.Contains(".minc")
                    || fileName.Contains(".bmp")
                    || fileName.Contains(".pmn"))
                {
                    Kitware.VTK.vtkImageReader2 rdr =
                        Kitware.VTK.vtkImageReader2Factory.CreateImageReader2(fileName);
                    rdr.SetFileName(fileName);
                    rdr.Update();
                    imgProp = vtkImageActor.New();
                    ((vtkImageActor)imgProp).SetInput(rdr.GetOutput());
                    rdr.Dispose();
                }

                //.vtk files need a DataSetReader instead of a ImageReader2
                //some .vtk files need a different kind of reader, but this
                //will read most and serve our purposes
                else if (fileName.Contains(".vtk"))
                {
                    vtkDataSetReader dataReader = vtkDataSetReader.New();
                    vtkDataSetMapper dataMapper = vtkDataSetMapper.New();
                    imgProp = vtkActor.New();
                    dataReader.SetFileName(fileName);
                    dataReader.Update();
                    dataMapper.SetInput(dataReader.GetOutput());
                    ((vtkActor)imgProp).SetMapper(dataMapper);
                    dataMapper.Dispose();
                    dataMapper = null;
                    dataReader.Dispose();
                    dataReader = null;
                }
                else
                {
                    return;
                }
                ren.AddActor(imgProp);
                //Reset the camera to show the image
                //Equivilant of pressing 'r'
                ren.ResetCamera();
                //Rerender the screen
                //NOTE: sometimes you have to drag the mouse
                //a little before the image shows up
                renderWindowControl1.RenderWindow.Render();
                ren.Render();

            }
        }
        /// <summary>
        /// Clean up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (imgProp != null)
            {
                imgProp.Dispose();
            }
            if (this.renderWindowControl1 != null)
            {
                this.renderWindowControl1.Dispose();
            }
            System.GC.Collect();
        }
    }
}