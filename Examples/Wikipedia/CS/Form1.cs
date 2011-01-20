using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Kitware.VTK;

namespace Wikipedia
{
    public partial class Form1 : Form
    {
        //The graph and the view for the graph
        vtkGraphLayoutView view = null;
        vtkMutableDirectedGraph g;

        //The loading graphic's actor and renderer.
        vtkRenderer logoRenderer;
        vtkImageActor logoActor;

        //An array list that only gets filled with
        //links when the graph is expanding.
        System.Collections.ArrayList arrListSmall;

        //Flag to track whether we've automatically
        //created the initial graph object:
        bool CreatedFirstGraph = false;

        //Flag to track whether we've processed certain
        //messages yet:
        bool HaveActivated = false;
        bool HavePainted = false;

        /// <summary>
        /// Application Constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            //Initialize the loading graphic
            System.Reflection.Assembly ass = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream stream = ass.GetManifestResourceStream(ass.GetName().Name + ".logo.png");
            //Read in the logo as a stream
            System.Drawing.Image img = Image.FromStream(stream);
            //Convert the System.Drawing.Image to Kitware.VTK.ImageData
            vtkImageData idata = vtkImageData.FromImage(img);

            logoActor = vtkImageActor.New();
            logoActor.SetInput(idata);
            logoRenderer = vtkRenderer.New();
            logoRenderer.AddActor(logoActor);
            //Look at the center of the logo instead of the default bottom right corner
            logoActor.SetOrigin((idata.GetDimensions()[0] / 2), (idata.GetDimensions()[1] / 2), 0);
            vtkCamera cam = logoRenderer.GetActiveCamera();
            cam.SetFocalPoint(logoActor.GetCenter()[0], logoActor.GetCenter()[1], logoActor.GetCenter()[2]);
            cam.SetPosition(logoActor.GetCenter()[0], logoActor.GetCenter()[1], 900);
        }

        /// <summary>
        /// Load the vtkRenderWindowControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void renderWindowControl1_Load(object sender, EventArgs e)
        {
            //Set up the view and make it fire a SelectionChangedEvt
            view = vtkGraphLayoutView.New();
            view.SelectionChangedEvt += new vtkObject.vtkObjectEventHandler(view_SelectionChangedEvt);
            view.GetRenderWindow().SetParentId(renderWindowControl1.RenderWindow.GetGenericWindowId());
        }

        /// <summary>
        /// Event fired when the graph selection is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void view_SelectionChangedEvt(vtkObject sender, vtkObjectEventArgs e)
        {
            //Get the selection
            vtkDataRepresentation dr = view.GetRepresentation(0);
            vtkAnnotationLink al = dr.GetAnnotationLink();
            vtkSelection sel = al.GetCurrentSelection();
            vtkAbstractArray arr = null;
            vtkSelectionNode node = sel.GetNode(0);
            if (null != node)
            {
                arr = node.GetSelectionList();
            }

            vtkIdTypeArray arr1 = (vtkIdTypeArray) arr;

            // Short circuit if nothing is selected...
            int numSelected = (int) arr1.GetNumberOfTuples();
            if (0 == numSelected)
            {
                return;
            }

            vtkStringArray arr2 = ((vtkStringArray)g.GetVertexData().GetAbstractArray("label"));

            //get a pseudo random name if more than one are selected
            string name = arr2.GetValue(
                (int)arr1.GetValue(System.DateTime.Now.Millisecond % numSelected));

            //reset the small arrayList 
            arrListSmall = new System.Collections.ArrayList();
            int hops;
            try
            {
                hops = System.Convert.ToInt32(toolStripTextBox3.Text);
            }
            catch (Exception)
            {
                hops = 1;
            }

            toolStripTextBox1.Text = name;
            toolStripTextBox1.Invalidate();
            this.Update();

            //Start the loading graphic and add another renderer on top for the spinning logo
            vtkRenderWindow win = this.renderWindowControl1.RenderWindow;
            win.AddRenderer(logoRenderer);
            win.Render();
            this.webBrowser1.Url = new Uri("http://en.wikipedia.org/wiki/" + name.Replace(" ", "_"));
            //Start the work
            addLinks(g, name, hops);
            //Remove the spinning logo after the work is done
            win.RemoveRenderer(logoRenderer);
            win.Render();
        }

        /// <summary>
        /// Rotates the logo 
        /// </summary>
        public void rotateLogo()
        {
            //rotate the logo a number of degrees that does not 
            //evenly go into 90
            logoActor.RotateY(13.5834957834857839474);
            logoRenderer.ResetCameraClippingRange();
            renderWindowControl1.RenderWindow.Render();
        }

        /// <summary>
        /// Clears the graph and makes a new one
        /// </summary>
        private void createNewGraph()
        {
            String lookupValue = toolStripTextBox1.Text;

            //clean up any old graph views in the renderer
            vtkMutableDirectedGraph g_temp = g;
            g = vtkMutableDirectedGraph.New();
            if (g_temp != null)
            {
                g_temp.Dispose();
            }
            //reset array lists
            arrListSmall = new System.Collections.ArrayList();
            vtkStringArray label = vtkStringArray.New();
            label.SetName("label");
            //give the graph a starting point
            g.GetVertexData().AddArray(label);
            g.AddVertex();
            label.InsertNextValue(lookupValue);
            arrListSmall.Add(lookupValue);
            int hops;
            try
            {
                hops = System.Convert.ToInt32(toolStripTextBox3.Text);
            }
            catch (Exception)
            {
                hops = 1;
            }
            //Start the loading graphic and switch renderers
            vtkRenderWindow win = this.renderWindowControl1.RenderWindow;
            win.AddRenderer(logoRenderer);
            win.Render();

            this.webBrowser1.Url = new Uri("http://en.wikipedia.org/wiki/" + lookupValue.Replace(" ", "_"));
            //Start the work
            addLinks(g, lookupValue, hops);
            //Go back to the graph view after the work is done
            win.RemoveRenderer(logoRenderer);

            //Setup the view properties
            view.SetLayoutStrategyToSimple2D();
            view.AddRepresentationFromInput(g);
            view.SetVertexLabelArrayName("label");
            view.VertexLabelVisibilityOn();
            view.SetVertexColorArrayName("VertexDegree");
            view.ColorVerticesOn();
            view.GetRenderer().ResetCamera();
            view.Update();
        }

        /// <summary>
        /// Calls createNewGraph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.createNewGraph();
        }

        /// <summary>
        /// Recursive function that finds and 
        /// graphs Wikipedia links
        /// </summary>
        /// <param name="g">The graph</param>
        /// <param name="lookupValue">Name of orgin article</param>
        /// <param name="hops">How many degrees of separation from the original article</param>
        private void addLinks(Kitware.VTK.vtkMutableDirectedGraph g, string lookupValue, int hops)
        {
            vtkStringArray label = (vtkStringArray)g.GetVertexData().GetAbstractArray("label");
            long parent = label.LookupValue(lookupValue);
            //if lookupValue is not in the graph add it
            if (parent < 0)
            {
                rotateLogo();
                parent = g.AddVertex();
                label.InsertNextValue(lookupValue);
                arrListSmall.Add(lookupValue);
            }
            //Parse Wikipedia for the lookupValue
            string underscores = lookupValue.Replace(' ', '_');
            System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("http://en.wikipedia.org/wiki/Special:Export/" + underscores);
            webRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;
            webRequest.Accept = "text/xml";
            try
            {
                System.Net.HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();
                System.IO.Stream responseStream = webResponse.GetResponseStream();
                System.Xml.XmlReader reader = new System.Xml.XmlTextReader(responseStream);
                String NS = "http://www.mediawiki.org/xml/export-0.4/";
                System.Xml.XPath.XPathDocument doc = new System.Xml.XPath.XPathDocument(reader);
                reader.Close();
                webResponse.Close();
                System.Xml.XPath.XPathNavigator myXPahtNavigator = doc.CreateNavigator();
                System.Xml.XPath.XPathNodeIterator nodesText = myXPahtNavigator.SelectDescendants("text", NS, false);

                String fullText = "";
                //Parse the wiki page for links
                while (nodesText.MoveNext())
                    fullText += nodesText.Current.InnerXml + " ";
                System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(fullText, "\\[\\[.*?\\]\\]");
                int max;
                try
                {
                    max = System.Convert.ToInt32(toolStripTextBox2.Text);
                }
                catch (Exception)
                {
                    max = -1;
                }
                int count = 0;
                while (m.Success && ((count < max) || (max < 0)))
                {
                    String s = m.ToString();
                    int index = s.IndexOf('|');
                    String substring = "";
                    if (index > 0)
                    {
                        substring = s.Substring(2, index - 2);
                    }
                    else
                    {
                        substring = s.Substring(2, s.Length - 4);
                    }
                    //if the new substring is not already there add it
                    long v = label.LookupValue(substring);
                    if (v < 0)
                    {
                        rotateLogo();
                        v = g.AddVertex();
                        label.InsertNextValue(substring);
                        arrListSmall.Add(substring);
                        if (hops > 1)
                        {
                            addLinks(g, substring, hops - 1);
                        }
                    }
                    else if (arrListSmall.IndexOf(substring) < 0)
                    {
                        arrListSmall.Add(substring);
                        if (hops > 1)
                        {
                            addLinks(g, substring, hops - 1);
                        }
                    }
                    //Make sure nothing is linked to twice by expanding the graph
                    vtkAdjacentVertexIterator avi = vtkAdjacentVertexIterator.New();
                    g.GetAdjacentVertices((int)parent, avi);
                    m = m.NextMatch();
                    ++count;

                    while (avi.HasNext())
                    {
                        long id = avi.Next();
                        if (id == v)
                        {
                            return;
                        }
                    }
                    rotateLogo();
                    g.AddGraphEdge((int)parent, (int)v);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Clean Up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (view != null)
            {
                view.Dispose();
            }
            if (logoRenderer != null)
            {
                logoRenderer.Dispose();
            }
            if (logoActor != null)
            {
                logoActor.Dispose();
            }
            view = null;
            System.GC.Collect();
        }

        /// <summary>
        /// Listen for the enter key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            toolStripTextBox3_KeyPress(sender, e);
        }

        /// <summary>
        /// Listen for the enter key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            toolStripTextBox3_KeyPress(sender, e);
        }

        /// <summary>
        /// Listen for the enter key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar) == '\r')
            {
                //if enter was pressed on a text box simulate a click on the Go button
                toolStripButton1_Click(sender, e);
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            this.HaveActivated = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            this.HavePainted = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!this.CreatedFirstGraph &&
              this.HaveActivated &&
              this.HavePainted &&
              this.Visible)
            {
                CreatedFirstGraph = true;

                this.createNewGraph();

                this.timer1.Enabled = false;
            }
        }
    }
}
