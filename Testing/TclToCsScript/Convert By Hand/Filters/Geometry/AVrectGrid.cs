using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\rectGrid.tcl
// output file is AVrectGrid.cs
/// <summary>
/// The testing class derived from AVrectGrid
/// </summary>
public class AVrectGridClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVrectGrid(String [] argv)
  {
  //Prefix Content is: ""
  
  VTK_VARY_RADIUS_BY_VECTOR = 2;
  // create pipeline[]
  //[]
  reader = new vtkDataSetReader();
  reader.SetFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/RectGrid2.vtk");
  reader.Update();
  toRectilinearGrid = new vtkCastToConcrete();
  toRectilinearGrid.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  toRectilinearGrid.Update();
  plane = new vtkRectilinearGridGeometryFilter();
  plane.SetInputData((vtkDataObject)toRectilinearGrid.GetRectilinearGridOutput());
  plane.SetExtent((int)0,(int)100,(int)0,(int)100,(int)15,(int)15);
  warper = new vtkWarpVector();
  warper.SetInputConnection((vtkAlgorithmOutput)plane.GetOutputPort());
  warper.SetScaleFactor((double)0.05);
  planeMapper = new vtkDataSetMapper();
  planeMapper.SetInputConnection((vtkAlgorithmOutput)warper.GetOutputPort());
  planeMapper.SetScalarRange((double)0.197813,(double)0.710419);
  planeActor = new vtkActor();
  planeActor.SetMapper((vtkMapper)planeMapper);
  cutPlane = new vtkPlane();
  cutPlane.SetOrigin(reader.GetOutput().GetCenter()[0],reader.GetOutput().GetCenter()[1],reader.GetOutput().GetCenter()[2]);
  cutPlane.SetNormal((double)1,(double)0,(double)0);
  planeCut = new vtkCutter();
  planeCut.SetInputData((vtkDataObject)toRectilinearGrid.GetRectilinearGridOutput());
  planeCut.SetCutFunction((vtkImplicitFunction)cutPlane);
  cutMapper = new vtkDataSetMapper();
  cutMapper.SetInputConnection((vtkAlgorithmOutput)planeCut.GetOutputPort());
  cutMapper.SetScalarRange((double)((vtkDataSet)reader.GetOutput()).GetPointData().GetScalars().GetRange()[0], (double)((vtkDataSet)reader.GetOutput()).GetPointData().GetScalars().GetRange()[1]);
  cutActor = new vtkActor();
  cutActor.SetMapper((vtkMapper)cutMapper);
  iso = new vtkContourFilter();
  iso.SetInputData((vtkDataObject)toRectilinearGrid.GetRectilinearGridOutput());
  iso.SetValue((int)0,(double)0.7);
  normals = new vtkPolyDataNormals();
  normals.SetInputConnection((vtkAlgorithmOutput)iso.GetOutputPort());
  normals.SetFeatureAngle((double)45);
  isoMapper = vtkPolyDataMapper.New();
  isoMapper.SetInputConnection((vtkAlgorithmOutput)normals.GetOutputPort());
  isoMapper.ScalarVisibilityOff();
  isoActor = new vtkActor();
  isoActor.SetMapper((vtkMapper)isoMapper);
  isoActor.GetProperty().SetColor((double) 1.0000, 0.8941, 0.7686 );
  isoActor.GetProperty().SetRepresentationToWireframe();
  streamer = new vtkStreamLine();
  streamer.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  streamer.SetStartPosition((double)-1.2,(double)-0.1,(double)1.3);
  streamer.SetMaximumPropagationTime((double)500);
  streamer.SetStepLength((double)0.05);
  streamer.SetIntegrationStepLength((double)0.05);
  streamer.SetIntegrationDirectionToIntegrateBothDirections();
  streamTube = new vtkTubeFilter();
  streamTube.SetInputConnection((vtkAlgorithmOutput)streamer.GetOutputPort());
  streamTube.SetRadius((double)0.025);
  streamTube.SetNumberOfSides((int)6);
  streamTube.SetVaryRadius((int)VTK_VARY_RADIUS_BY_VECTOR);
  mapStreamTube = vtkPolyDataMapper.New();
  mapStreamTube.SetInputConnection((vtkAlgorithmOutput)streamTube.GetOutputPort());
  mapStreamTube.SetScalarRange((double)((vtkDataSet)reader.GetOutput()).GetPointData().GetScalars().GetRange()[0], (double)((vtkDataSet)reader.GetOutput()).GetPointData().GetScalars().GetRange()[1]);
  streamTubeActor = new vtkActor();
  streamTubeActor.SetMapper((vtkMapper)mapStreamTube);
  streamTubeActor.GetProperty().BackfaceCullingOn();
  outline = new vtkOutlineFilter();
  outline.SetInputData((vtkDataObject)toRectilinearGrid.GetRectilinearGridOutput());
  outlineMapper = vtkPolyDataMapper.New();
  outlineMapper.SetInputConnection((vtkAlgorithmOutput)outline.GetOutputPort());
  outlineActor = new vtkActor();
  outlineActor.SetMapper((vtkMapper)outlineMapper);
  outlineActor.GetProperty().SetColor((double) 0.0000, 0.0000, 0.0000 );
  // Graphics stuff[]
  // Create the RenderWindow, Renderer and both Actors[]
  //[]
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.SetMultiSamples(0);
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  // Add the actors to the renderer, set the background and size[]
  //[]
  ren1.AddActor((vtkProp)outlineActor);
  ren1.AddActor((vtkProp)planeActor);
  ren1.AddActor((vtkProp)cutActor);
  ren1.AddActor((vtkProp)isoActor);
  ren1.AddActor((vtkProp)streamTubeActor);
  ren1.SetBackground((double)1,(double)1,(double)1);
  renWin.SetSize((int)400,(int)400);
  cam1 = ren1.GetActiveCamera();
  cam1.SetClippingRange((double)3.76213,(double)10.712);
  cam1.SetFocalPoint((double)-0.0842503,(double)-0.136905,(double)0.610234);
  cam1.SetPosition((double)2.53813,(double)2.2678,(double)-5.22172);
  cam1.SetViewUp((double)-0.241047,(double)0.930635,(double)0.275343);
  iren.Initialize();
  // render the image[]
  //[]
  // prevent the tk window from showing up then start the event loop[]
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static int VTK_VARY_RADIUS_BY_VECTOR;
static vtkDataSetReader reader;
static vtkCastToConcrete toRectilinearGrid;
static vtkRectilinearGridGeometryFilter plane;
static vtkWarpVector warper;
static vtkDataSetMapper planeMapper;
static vtkActor planeActor;
static vtkPlane cutPlane;
static vtkCutter planeCut;
static vtkDataSetMapper cutMapper;
static vtkActor cutActor;
static vtkContourFilter iso;
static vtkPolyDataNormals normals;
static vtkPolyDataMapper isoMapper;
static vtkActor isoActor;
static vtkStreamLine streamer;
static vtkTubeFilter streamTube;
static vtkPolyDataMapper mapStreamTube;
static vtkActor streamTubeActor;
static vtkOutlineFilter outline;
static vtkPolyDataMapper outlineMapper;
static vtkActor outlineActor;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkCamera cam1;


        ///<summary> A Get Method for Static Variables </summary>
        public static string GetVTK_DATA_ROOT()
        {
            return VTK_DATA_ROOT;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetVTK_DATA_ROOT(string toSet)
        {
            VTK_DATA_ROOT = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int Getthreshold()
        {
            return threshold;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setthreshold(int toSet)
        {
            threshold = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int GetVTK_VARY_RADIUS_BY_VECTOR()
        {
            return VTK_VARY_RADIUS_BY_VECTOR;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetVTK_VARY_RADIUS_BY_VECTOR(int toSet)
        {
            VTK_VARY_RADIUS_BY_VECTOR = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetReader Getreader()
        {
            return reader;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setreader(vtkDataSetReader toSet)
        {
            reader = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCastToConcrete GettoRectilinearGrid()
        {
            return toRectilinearGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SettoRectilinearGrid(vtkCastToConcrete toSet)
        {
            toRectilinearGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRectilinearGridGeometryFilter Getplane()
        {
            return plane;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setplane(vtkRectilinearGridGeometryFilter toSet)
        {
            plane = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkWarpVector Getwarper()
        {
            return warper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setwarper(vtkWarpVector toSet)
        {
            warper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetplaneMapper()
        {
            return planeMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetplaneMapper(vtkDataSetMapper toSet)
        {
            planeMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetplaneActor()
        {
            return planeActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetplaneActor(vtkActor toSet)
        {
            planeActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPlane GetcutPlane()
        {
            return cutPlane;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetcutPlane(vtkPlane toSet)
        {
            cutPlane = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCutter GetplaneCut()
        {
            return planeCut;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetplaneCut(vtkCutter toSet)
        {
            planeCut = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetcutMapper()
        {
            return cutMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetcutMapper(vtkDataSetMapper toSet)
        {
            cutMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetcutActor()
        {
            return cutActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetcutActor(vtkActor toSet)
        {
            cutActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkContourFilter Getiso()
        {
            return iso;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setiso(vtkContourFilter toSet)
        {
            iso = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataNormals Getnormals()
        {
            return normals;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setnormals(vtkPolyDataNormals toSet)
        {
            normals = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetisoMapper()
        {
            return isoMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetisoMapper(vtkPolyDataMapper toSet)
        {
            isoMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetisoActor()
        {
            return isoActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetisoActor(vtkActor toSet)
        {
            isoActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStreamLine Getstreamer()
        {
            return streamer;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setstreamer(vtkStreamLine toSet)
        {
            streamer = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTubeFilter GetstreamTube()
        {
            return streamTube;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetstreamTube(vtkTubeFilter toSet)
        {
            streamTube = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetmapStreamTube()
        {
            return mapStreamTube;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapStreamTube(vtkPolyDataMapper toSet)
        {
            mapStreamTube = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetstreamTubeActor()
        {
            return streamTubeActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetstreamTubeActor(vtkActor toSet)
        {
            streamTubeActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkOutlineFilter Getoutline()
        {
            return outline;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setoutline(vtkOutlineFilter toSet)
        {
            outline = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetoutlineMapper()
        {
            return outlineMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetoutlineMapper(vtkPolyDataMapper toSet)
        {
            outlineMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetoutlineActor()
        {
            return outlineActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetoutlineActor(vtkActor toSet)
        {
            outlineActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRenderer Getren1()
        {
            return ren1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setren1(vtkRenderer toSet)
        {
            ren1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRenderWindow GetrenWin()
        {
            return renWin;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetrenWin(vtkRenderWindow toSet)
        {
            renWin = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRenderWindowInteractor Getiren()
        {
            return iren;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setiren(vtkRenderWindowInteractor toSet)
        {
            iren = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCamera Getcam1()
        {
            return cam1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcam1(vtkCamera toSet)
        {
            cam1 = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(reader!= null){reader.Dispose();}
  	if(toRectilinearGrid!= null){toRectilinearGrid.Dispose();}
  	if(plane!= null){plane.Dispose();}
  	if(warper!= null){warper.Dispose();}
  	if(planeMapper!= null){planeMapper.Dispose();}
  	if(planeActor!= null){planeActor.Dispose();}
  	if(cutPlane!= null){cutPlane.Dispose();}
  	if(planeCut!= null){planeCut.Dispose();}
  	if(cutMapper!= null){cutMapper.Dispose();}
  	if(cutActor!= null){cutActor.Dispose();}
  	if(iso!= null){iso.Dispose();}
  	if(normals!= null){normals.Dispose();}
  	if(isoMapper!= null){isoMapper.Dispose();}
  	if(isoActor!= null){isoActor.Dispose();}
  	if(streamer!= null){streamer.Dispose();}
  	if(streamTube!= null){streamTube.Dispose();}
  	if(mapStreamTube!= null){mapStreamTube.Dispose();}
  	if(streamTubeActor!= null){streamTubeActor.Dispose();}
  	if(outline!= null){outline.Dispose();}
  	if(outlineMapper!= null){outlineMapper.Dispose();}
  	if(outlineActor!= null){outlineActor.Dispose();}
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(cam1!= null){cam1.Dispose();}
  }

}
//--- end of script --//

