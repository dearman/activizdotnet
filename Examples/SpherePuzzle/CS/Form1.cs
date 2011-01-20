using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Kitware.VTK;

namespace SpherePuzzle
{
    /// <summary>
    /// An example converted from a vtk Tcl example
    /// and embeded in a CSharp Dialog
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        vtkSpherePuzzle puzzle = vtkSpherePuzzle.New();
        vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
        vtkActor actor = vtkActor.New();
        vtkSpherePuzzleArrows arrows = vtkSpherePuzzleArrows.New();
        vtkPolyDataMapper mapper2 = vtkPolyDataMapper.New();
        vtkActor actor2 = vtkActor.New();
        Boolean once = true;

        bool in_piece_rotation = false;
        double LastVal = 0;
        bool LastValExists = false;

        /// <summary>
        /// Clean up globals
        /// </summary>
        public void disposeAllVTKObjects()
        {
            puzzle.Dispose();
            mapper.Dispose();
            actor.Dispose();
            arrows.Dispose();
            mapper2.Dispose();
            actor2.Dispose();
        }

        /// <summary>
        /// Set up the dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renderWindowControl1_Load(object sender, EventArgs e)
        {
            //Setup the variables and the background
            vtkRenderer ren1 = renderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer();
            mapper.SetInputConnection(puzzle.GetOutputPort());
            mapper2.SetInputConnection(arrows.GetOutputPort());
            actor.SetMapper(mapper);
            actor2.SetMapper(mapper2);
            ren1.AddActor(actor);
            ren1.AddActor(actor2);
            ren1.SetBackground(0.1, 0.2, 0.4);

            //Set up the camera
            ren1.ResetCamera();
            vtkCamera cam = ren1.GetActiveCamera();
            cam.Elevation(-40);
            renderWindowControl1.RenderWindow.Render();

            //Change the style to a trackball style
            //Equivalent of pressing 't'
            vtkRenderWindowInteractor iren = renderWindowControl1.RenderWindow.GetInteractor();
            vtkInteractorStyleSwitch istyle = vtkInteractorStyleSwitch.New();
            iren.SetInteractorStyle(istyle);
            (istyle).SetCurrentStyleToTrackballCamera();

            //Add events to the iren instead of Observers
            iren.MouseMoveEvt += new vtkObject.vtkObjectEventHandler(MotionCallback);
            iren.CharEvt += new vtkObject.vtkObjectEventHandler(CharCallback);
        }

        /// <summary>
        /// Highlights pieces
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MotionCallback(vtkObject sender, vtkObjectEventArgs e)
        {
            //Make sure the piece isn't in an animation
            //durring a click or bad things happen
            if (!in_piece_rotation)
            {
                vtkRenderWindowInteractor iren = renderWindowControl1.RenderWindow.GetInteractor();
                vtkInteractorStyleSwitch istyle = (vtkInteractorStyleSwitch)iren.GetInteractorStyle();

                //return if the user is performing interaction
                if (istyle.GetState()!=0)
                {
                    return;
                }

                int[] pos = iren.GetEventPosition();
                int x = pos[0];
                int y = pos[1];

                vtkRenderer ren1 = renderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer();
                ren1.SetDisplayPoint(x, y, ren1.GetZ(x, y));
                ren1.DisplayToWorld();
                double [] pt = ren1.GetWorldPoint();
                double val = puzzle.SetPoint(pt[0], pt[1], pt[2]);
                if (!LastValExists || val != LastVal)
                {
                    renderWindowControl1.RenderWindow.Render();
                    LastVal = val;
                    LastValExists = true;
                }
            }
        }
        /// <summary>
        /// Called when a key is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CharCallback(vtkObject sender, vtkObjectEventArgs e)
        {
            vtkRenderWindowInteractor iren = renderWindowControl1.RenderWindow.GetInteractor();
            sbyte keycode = iren.GetKeyCode();
            //if the keycode is not M
            if (keycode != 109 && keycode != 77)
            {
                return;
            }
            int[] pos = iren.GetEventPosition();
            ButtonCallback(pos[0], pos[1]);
        }
        /// <summary>
        /// Moves the sphere when the mouse is clicked in
        /// position (x,y)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void ButtonCallback(double x, double y)
        {
            if (!in_piece_rotation)
            {
                in_piece_rotation = true;
                vtkRenderer ren1 = renderWindowControl1.RenderWindow.GetRenderers().GetFirstRenderer();
                ren1.SetDisplayPoint(x,y,ren1.GetZ((int)x,(int)y));
                ren1.DisplayToWorld();
                double[] pt = ren1.GetWorldPoint();

                 x = pt[0];
                 y = pt[1];
                 double z = pt[2];

                for (int i = 0; i <= 100; i += 10)
                {
                    puzzle.SetPoint(x, y, z);
                    puzzle.MovePoint(i);
                    renderWindowControl1.RenderWindow.Render();
                    this.Update();
                }
                in_piece_rotation = false;
            }
        }

        /// <summary>
        /// Reset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            puzzle.Reset();
            renderWindowControl1.RenderWindow.Render();
        }

        /// <summary>
        /// Quit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Clean up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Closed(object sender, FormClosedEventArgs e)
        {
            disposeAllVTKObjects();
        }

        /// <summary>
        /// Scrambles the puzzle when the form first becomes
        /// visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Activated(object sender, EventArgs e)
        {
            if (once)
            {
                ButtonCallback(218, 195);
                ButtonCallback(261, 128);
                ButtonCallback(213, 107);
                ButtonCallback(203, 162);
                ButtonCallback(134, 186);
                once = false;
            }
        }

    }
}
