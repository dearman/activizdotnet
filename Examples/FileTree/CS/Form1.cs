using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Kitware.VTK;
using System.Text.RegularExpressions;


namespace FileTree
{
    /// <summary>
    /// This is an example that displays the 
    /// file layout in a particular directory.
    /// The size of the file is relative size 
    /// of the node. The color of the node is 
    /// the level of depth in the file system,
    /// blue being the lowest depth and red 
    /// being the greatest.
    /// </summary>
    public partial class Form1 : Form
    {
        // The view for rendering the graph.
        public vtkTreeMapView view = null;
        public vtkMutableDirectedGraph g;
        public bool initialized;
        string SelectedPath;

        /// <summary>
        /// Constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the view in the render window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renWinCTRL_Load(object sender, EventArgs e)
        {
            view = vtkTreeMapView.New();
        }

        /// <summary>
        /// Open the explorer window of the selected file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void view_SelectionChangedEvt(vtkObject sender, vtkObjectEventArgs e)
        {
            // Get the selection
            //
            vtkDataRepresentation dr = view.GetRepresentation(0);
            vtkAnnotationLink al = dr.GetAnnotationLink();
            vtkSelection sel = al.GetCurrentSelection();
            vtkStringArray arr1 = null;
            vtkSelectionNode node = sel.GetNode(0);
            if (null != node)
            {
                arr1 = (vtkStringArray)node.GetSelectionList();
            }

            string path = "";

            if (null != arr1)
            {
                // If it is a directory open it, if not cut off the end
                // and open the directory
                //
                if (Directory.Exists(arr1.GetValue(0)))
                {
                    path = arr1.GetValue(0);
                }
                else
                {
                    path = Regex.Replace(arr1.GetValue(0),
                        "\\\\[^\\\\]+\\.[^\\\\]*$", "");
                }
            }

            if (path != SelectedPath)
            {
                SelectedPath = path;

                System.Diagnostics.Debug.WriteLine(SelectedPath);

                if ("" != SelectedPath)
                {
                    //Create the process
                    System.Diagnostics.ProcessStartInfo psi =
                        new System.Diagnostics.ProcessStartInfo("cmd.exe",
                          "/C explorer " + SelectedPath);
                    psi.CreateNoWindow = true;
                    psi.UseShellExecute = false;
                    System.Diagnostics.Process p =
                        System.Diagnostics.Process.Start(psi);
                }
            }
        }

        /// <summary>
        /// Clean up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.GC.Collect();
        }

        /// <summary>
        /// Recursive function that creates a level of the file tree
        /// </summary>
        /// <param name="g"></param>
        /// <param name="parent"></param>
        /// <param name="path"></param>
        private void buildTree(vtkMutableDirectedGraph g, long parent, string path)
        {
            vtkStringArray name = (vtkStringArray)g.GetVertexData().GetAbstractArray("name");
            vtkLongLongArray size = (vtkLongLongArray)g.GetVertexData().GetAbstractArray("size");
            vtkStringArray fullpath = (vtkStringArray)g.GetVertexData().GetAbstractArray("path");
            
            if (Directory.Exists(path))
            {
                long v = 0;
                if (parent == -1)
                {
                    v = g.AddVertex();
                }
                else
                {
                    v = g.AddChild((int)parent);
                }
                string[] pathparts = path.Split('\\');
                int ipaths = pathparts.GetUpperBound(0);
                fullpath.InsertNextValue(path);
                name.InsertNextValue(pathparts[ipaths]);
                size.InsertNextValue(1024);
                this.label1.Text = "Processing " + path;
                this.Update();
                try
                {
                    foreach (string f in Directory.GetFiles(path))
                    {
                        Console.Out.WriteLine(f);
                        buildTree(g, v, f);
                    }

                    foreach (string d in Directory.GetDirectories(path))
                    {
                        Console.Out.WriteLine(d);
                        buildTree(g, v, d);
                    }
                }
                catch (System.Exception excpt)
                {
                    Console.Error.WriteLine(excpt.Message);
                }
            }
            else if (File.Exists(path))
            {
                FileInfo info = new FileInfo(path);

                //Do not graph files smaller than 1K
                if (info.Length > 1024)
                {
                    g.AddChild((int)parent);
                    fullpath.InsertNextValue(path);
                    name.InsertNextValue(Path.GetFileName(path));
                    size.InsertNextValue(info.Length);
                }
            }
        }

        /// <summary>
        /// Open a folder browser and graph the 
        /// selected folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setupView_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog().Equals(DialogResult.OK))
            {
                //Add the view to the render window
                if (!initialized)
                {
                    initialized = true;

                    this.view.GetRenderWindow().SetParentId(
                      this.renWinCTRL.RenderWindow.GetGenericWindowId());
                    this.ResizeTreeMapView();

                    //Add a handler to the view
                    view.SelectionChangedEvt += new vtkObject.vtkObjectEventHandler(this.view_SelectionChangedEvt);
                }

                g = vtkMutableDirectedGraph.New();
                vtkStringArray name = vtkStringArray.New();
                name.SetName("name");
                g.GetVertexData().AddArray(name);
                vtkStringArray path = vtkStringArray.New();
                path.SetName("path");
                g.GetVertexData().SetPedigreeIds(path);
                vtkLongLongArray size = vtkLongLongArray.New();
                size.SetName("size");
                g.GetVertexData().AddArray(size);
                string cur = Directory.GetCurrentDirectory();


                buildTree(g, -1, this.folderBrowserDialog1.SelectedPath);

                this.label1.Text = "Viewing "+this.folderBrowserDialog1.SelectedPath;

                vtkTree t = vtkTree.New();
                t.ShallowCopy(g);

                view.SetLayoutStrategyToSquarify();

                view.SetAreaLabelArrayName("name");
                view.SetAreaHoverArrayName("path");
                view.SetAreaColorArrayName("level");
                view.SetAreaSizeArrayName("size");

                view.AddRepresentationFromInput(t);
                view.GetRenderer().ResetCamera();
                view.Update();
                view.Render();
            }
        }

        private void ResizeTreeMapView()
        {
          int[] size = this.renWinCTRL.RenderWindow.GetSize();
          this.view.GetRenderWindow().SetSize(size[0], size[1]);
        }

        private void renWinCTRL_Resize(object sender, EventArgs e)
        {
          this.ResizeTreeMapView();
        }
    }
}
