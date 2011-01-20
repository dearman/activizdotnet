using System.IO;
using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\fieldToRGrid.tcl
// output file is AVfieldToRGrid.cs
/// <summary>
/// The testing class derived from AVfieldToRGrid
/// </summary>
public class AVfieldToRGridClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVfieldToRGrid(String [] argv)
  {
  //Prefix Content is: ""
  
  //# Generate a rectilinear grid from a field.[]
  //#[]
  // get the interactor ui[]
  // Create a reader and write out the field[]
  reader = new vtkDataSetReader();
  reader.SetFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/RectGrid2.vtk");
  ds2do = new vtkDataSetToDataObjectFilter();
  ds2do.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  try
  {
     channel = new StreamWriter("RGridField.vtk");
      tryCatchError = "NOERROR";
  }
  catch(Exception)
  {tryCatchError = "ERROR";}
  
if(tryCatchError.Equals("NOERROR"))
  {
      channel.Close();
      writer = new vtkDataObjectWriter();
      writer.SetInputConnection((vtkAlgorithmOutput)ds2do.GetOutputPort());
      writer.SetFileName((string)"RGridField.vtk");
      writer.Write();
      // Read the field[]
      //[]
      dor = new vtkDataObjectReader();
      dor.SetFileName((string)"RGridField.vtk");
      do2ds = new vtkDataObjectToDataSetFilter();
      do2ds.SetInputConnection((vtkAlgorithmOutput)dor.GetOutputPort());
      do2ds.SetDataSetTypeToRectilinearGrid();
      do2ds.SetDimensionsComponent((string)"Dimensions",(int)0);
      do2ds.SetPointComponent((int)0,(string)"XCoordinates",(int)0);
      do2ds.SetPointComponent((int)1,(string)"YCoordinates",(int)0);
      do2ds.SetPointComponent((int)2,(string)"ZCoordinates",(int)0);
      fd2ad = new vtkFieldDataToAttributeDataFilter();
      fd2ad.SetInput((vtkDataObject)do2ds.GetRectilinearGridOutput());
      fd2ad.SetInputFieldToDataObjectField();
      fd2ad.SetOutputAttributeDataToPointData();
      fd2ad.SetVectorComponent((int)0,(string)"vectors",(int)0);
      fd2ad.SetVectorComponent((int)1,(string)"vectors",(int)1);
      fd2ad.SetVectorComponent((int)2,(string)"vectors",(int)2);
      fd2ad.SetScalarComponent((int)0,(string)"scalars",(int)0);
      fd2ad.Update();
      // create pipeline[]
      //[]
      plane = new vtkRectilinearGridGeometryFilter();
      plane.SetInput((vtkDataObject)fd2ad.GetRectilinearGridOutput());
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
      cutPlane.SetOrigin(fd2ad.GetOutput().GetCenter()[0],fd2ad.GetOutput().GetCenter()[1],fd2ad.GetOutput().GetCenter()[2]);
      cutPlane.SetNormal((double)1,(double)0,(double)0);
      planeCut = new vtkCutter();
      planeCut.SetInput((vtkDataObject)fd2ad.GetRectilinearGridOutput());
      planeCut.SetCutFunction((vtkImplicitFunction)cutPlane);
      cutMapper = new vtkDataSetMapper();
      cutMapper.SetInputConnection((vtkAlgorithmOutput)planeCut.GetOutputPort());
      cutMapper.SetScalarRange(
          (double)((vtkDataSet)fd2ad.GetOutput()).GetPointData().GetScalars().GetRange()[0],
          (double)((vtkDataSet)fd2ad.GetOutput()).GetPointData().GetScalars().GetRange()[1]);
      cutActor = new vtkActor();
      cutActor.SetMapper((vtkMapper)cutMapper);
      iso = new vtkContourFilter();
      iso.SetInput((vtkDataObject)fd2ad.GetRectilinearGridOutput());
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
      streamer.SetInputConnection((vtkAlgorithmOutput)fd2ad.GetOutputPort());
      streamer.SetStartPosition((double)-1.2,(double)-0.1,(double)1.3);
      streamer.SetMaximumPropagationTime((double)500);
      streamer.SetStepLength((double)0.05);
      streamer.SetIntegrationStepLength((double)0.05);
      streamer.SetIntegrationDirectionToIntegrateBothDirections();
      streamTube = new vtkTubeFilter();
      streamTube.SetInputConnection((vtkAlgorithmOutput)streamer.GetOutputPort());
      streamTube.SetRadius((double)0.025);
      streamTube.SetNumberOfSides((int)6);
      streamTube.SetVaryRadiusToVaryRadiusByVector();
      mapStreamTube = vtkPolyDataMapper.New();
      mapStreamTube.SetInputConnection((vtkAlgorithmOutput)streamTube.GetOutputPort());
      mapStreamTube.SetScalarRange(
          (double)((vtkDataSet)fd2ad.GetOutput()).GetPointData().GetScalars().GetRange()[0],
          (double)((vtkDataSet)fd2ad.GetOutput()).GetPointData().GetScalars().GetRange()[1]);
      streamTubeActor = new vtkActor();
      streamTubeActor.SetMapper((vtkMapper)mapStreamTube);
      streamTubeActor.GetProperty().BackfaceCullingOn();
      outline = new vtkOutlineFilter();
      outline.SetInput((vtkDataObject)fd2ad.GetRectilinearGridOutput());
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
      renWin.SetSize((int)300,(int)300);
      ren1.GetActiveCamera().SetPosition((double)0.0390893,(double)0.184813,(double)-3.94026);
      ren1.GetActiveCamera().SetFocalPoint((double)-0.00578326,(double)0,(double)0.701967);
      ren1.GetActiveCamera().SetViewAngle((double)30);
      ren1.GetActiveCamera().SetViewUp((double)0.00850257,(double)0.999169,(double)0.0398605);
      ren1.GetActiveCamera().SetClippingRange((double)3.08127,(double)6.62716);
      iren.Initialize();
      // render the image[]
      //[]

          File.Delete("RGridField.vtk");

      
    }

  
  // prevent the tk window from showing up then start the event loop[]
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkDataSetReader reader;
static vtkDataSetToDataObjectFilter ds2do;
static string tryCatchError;
static StreamWriter channel;
static vtkDataObjectWriter writer;
static vtkDataObjectReader dor;
static vtkDataObjectToDataSetFilter do2ds;
static vtkFieldDataToAttributeDataFilter fd2ad;
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
        public static vtkDataSetToDataObjectFilter Getds2do()
        {
            return ds2do;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setds2do(vtkDataSetToDataObjectFilter toSet)
        {
            ds2do = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static string GettryCatchError()
        {
            return tryCatchError;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SettryCatchError(string toSet)
        {
            tryCatchError = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static StreamWriter Getchannel()
        {
            return channel;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setchannel(StreamWriter toSet)
        {
            channel = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataObjectWriter Getwriter()
        {
            return writer;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setwriter(vtkDataObjectWriter toSet)
        {
            writer = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataObjectReader Getdor()
        {
            return dor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setdor(vtkDataObjectReader toSet)
        {
            dor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataObjectToDataSetFilter Getdo2ds()
        {
            return do2ds;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setdo2ds(vtkDataObjectToDataSetFilter toSet)
        {
            do2ds = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkFieldDataToAttributeDataFilter Getfd2ad()
        {
            return fd2ad;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setfd2ad(vtkFieldDataToAttributeDataFilter toSet)
        {
            fd2ad = toSet;
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
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(reader!= null){reader.Dispose();}
  	if(ds2do!= null){ds2do.Dispose();}
  	if(writer!= null){writer.Dispose();}
  	if(dor!= null){dor.Dispose();}
  	if(do2ds!= null){do2ds.Dispose();}
  	if(fd2ad!= null){fd2ad.Dispose();}
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
  }

}
//--- end of script --//

