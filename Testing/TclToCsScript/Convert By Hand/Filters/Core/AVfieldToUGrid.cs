using System.IO;
using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\fieldToUGrid.tcl
// output file is AVfieldToUGrid.cs
/// <summary>
/// The testing class derived from AVfieldToUGrid
/// </summary>
public class AVfieldToUGridClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVfieldToUGrid(String [] argv)
  {
  //Prefix Content is: ""
  
  // Read a field representing unstructured grid and display it (similar to blow.tcl)[]
  // create a reader and write out field daya[]
  reader = new vtkUnstructuredGridReader();
  reader.SetFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/blow.vtk");
  reader.SetScalarsName((string)"thickness9");
  reader.SetVectorsName((string)"displacement9");
  ds2do = new vtkDataSetToDataObjectFilter();
  ds2do.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  // we must be able to write here[]
  try
  {
     channel = new StreamWriter("UGridField.vtk");
      tryCatchError = "NOERROR";
  }
  catch(Exception)
  {tryCatchError = "ERROR";}

  if (tryCatchError.Equals("NOERROR"))
  {
      channel.Close();
      write = new vtkDataObjectWriter();
      write.SetInputConnection((vtkAlgorithmOutput)ds2do.GetOutputPort());
      write.SetFileName((string)"UGridField.vtk");
      write.Write();
      // Read the field and convert to unstructured grid.[]
      dor = new vtkDataObjectReader();
      dor.SetFileName((string)"UGridField.vtk");
      do2ds = new vtkDataObjectToDataSetFilter();
      do2ds.SetInputConnection((vtkAlgorithmOutput)dor.GetOutputPort());
      do2ds.SetDataSetTypeToUnstructuredGrid();
      do2ds.SetPointComponent((int)0,(string)"Points",(int)0);
      do2ds.SetPointComponent((int)1,(string)"Points",(int)1);
      do2ds.SetPointComponent((int)2,(string)"Points",(int)2);
      do2ds.SetCellTypeComponent((string)"CellTypes",(int)0);
      do2ds.SetCellConnectivityComponent((string)"Cells",(int)0);
      do2ds.Update();

      fd2ad = new vtkFieldDataToAttributeDataFilter();
      fd2ad.SetInputData((vtkDataObject)do2ds.GetUnstructuredGridOutput());
      fd2ad.SetInputFieldToDataObjectField();
      fd2ad.SetOutputAttributeDataToPointData();
      fd2ad.SetVectorComponent((int)0,(string)"displacement9",(int)0);
      fd2ad.SetVectorComponent((int)1,(string)"displacement9",(int)1);
      fd2ad.SetVectorComponent((int)2,(string)"displacement9",(int)2);
      fd2ad.SetScalarComponent((int)0,(string)"thickness9",(int)0);
      fd2ad.Update();

      // Now start visualizing[]
      warp = new vtkWarpVector();
      warp.SetInputData((vtkDataObject)fd2ad.GetUnstructuredGridOutput());
      // extract mold from mesh using connectivity[]
      connect = new vtkConnectivityFilter();
      connect.SetInputConnection((vtkAlgorithmOutput)warp.GetOutputPort());
      connect.SetExtractionModeToSpecifiedRegions();
      connect.AddSpecifiedRegion((int)0);
      connect.AddSpecifiedRegion((int)1);
      moldMapper = new vtkDataSetMapper();
      moldMapper.SetInputConnection((vtkAlgorithmOutput)connect.GetOutputPort());
      moldMapper.ScalarVisibilityOff();
      moldActor = new vtkActor();
      moldActor.SetMapper((vtkMapper)moldMapper);
      moldActor.GetProperty().SetColor((double).2,(double).2,(double).2);
      moldActor.GetProperty().SetRepresentationToWireframe();
      // extract parison from mesh using connectivity[]
      connect2 = new vtkConnectivityFilter();
      connect2.SetInputConnection((vtkAlgorithmOutput)warp.GetOutputPort());
      connect2.SetExtractionModeToSpecifiedRegions();
      connect2.AddSpecifiedRegion((int)2);
      parison = new vtkGeometryFilter();
      parison.SetInputConnection((vtkAlgorithmOutput)connect2.GetOutputPort());
      normals2 = new vtkPolyDataNormals();
      normals2.SetInputConnection((vtkAlgorithmOutput)parison.GetOutputPort());
      normals2.SetFeatureAngle((double)60);
      lut = new vtkLookupTable();
      lut.SetHueRange((double)0.0,(double)0.66667);
      parisonMapper = vtkPolyDataMapper.New();
      parisonMapper.SetInputConnection((vtkAlgorithmOutput)normals2.GetOutputPort());
      parisonMapper.SetLookupTable((vtkScalarsToColors)lut);
      parisonMapper.SetScalarRange((double)0.12,(double)1.0);
      parisonActor = new vtkActor();
      parisonActor.SetMapper((vtkMapper)parisonMapper);
      cf = new vtkContourFilter();
      cf.SetInputConnection((vtkAlgorithmOutput)connect2.GetOutputPort());
      cf.SetValue((int)0,(double).5);
      contourMapper = vtkPolyDataMapper.New();
      contourMapper.SetInputConnection((vtkAlgorithmOutput)cf.GetOutputPort());
      contours = new vtkActor();
      contours.SetMapper((vtkMapper)contourMapper);
      // Create graphics stuff[]
      ren1 = vtkRenderer.New();
      renWin = vtkRenderWindow.New();
      renWin.AddRenderer((vtkRenderer)ren1);
      iren = new vtkRenderWindowInteractor();
      iren.SetRenderWindow((vtkRenderWindow)renWin);
      // Add the actors to the renderer, set the background and size[]
      ren1.AddActor((vtkProp)moldActor);
      ren1.AddActor((vtkProp)parisonActor);
      ren1.AddActor((vtkProp)contours);
      ren1.ResetCamera();
      ren1.GetActiveCamera().Azimuth((double)60);
      ren1.GetActiveCamera().Roll((double)-90);
      ren1.GetActiveCamera().Dolly((double)2);
      ren1.ResetCameraClippingRange();
      ren1.SetBackground((double)1,(double)1,(double)1);
      renWin.SetSize((int)375,(int)200);
      iren.Initialize();
    }

  
  // prevent the tk window from showing up then start the event loop[]
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkUnstructuredGridReader reader;
static vtkDataSetToDataObjectFilter ds2do;
static string tryCatchError;
static StreamWriter channel;
static vtkDataObjectWriter write;
static vtkDataObjectReader dor;
static vtkDataObjectToDataSetFilter do2ds;
static vtkFieldDataToAttributeDataFilter fd2ad;
static vtkWarpVector warp;
static vtkConnectivityFilter connect;
static vtkDataSetMapper moldMapper;
static vtkActor moldActor;
static vtkConnectivityFilter connect2;
static vtkGeometryFilter parison;
static vtkPolyDataNormals normals2;
static vtkLookupTable lut;
static vtkPolyDataMapper parisonMapper;
static vtkActor parisonActor;
static vtkContourFilter cf;
static vtkPolyDataMapper contourMapper;
static vtkActor contours;
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
        public static vtkUnstructuredGridReader Getreader()
        {
            return reader;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setreader(vtkUnstructuredGridReader toSet)
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
        public static vtkDataObjectWriter Getwrite()
        {
            return write;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setwrite(vtkDataObjectWriter toSet)
        {
            write = toSet;
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
        public static vtkWarpVector Getwarp()
        {
            return warp;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setwarp(vtkWarpVector toSet)
        {
            warp = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkConnectivityFilter Getconnect()
        {
            return connect;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setconnect(vtkConnectivityFilter toSet)
        {
            connect = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetmoldMapper()
        {
            return moldMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmoldMapper(vtkDataSetMapper toSet)
        {
            moldMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetmoldActor()
        {
            return moldActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmoldActor(vtkActor toSet)
        {
            moldActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkConnectivityFilter Getconnect2()
        {
            return connect2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setconnect2(vtkConnectivityFilter toSet)
        {
            connect2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkGeometryFilter Getparison()
        {
            return parison;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setparison(vtkGeometryFilter toSet)
        {
            parison = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataNormals Getnormals2()
        {
            return normals2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setnormals2(vtkPolyDataNormals toSet)
        {
            normals2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkLookupTable Getlut()
        {
            return lut;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setlut(vtkLookupTable toSet)
        {
            lut = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetparisonMapper()
        {
            return parisonMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetparisonMapper(vtkPolyDataMapper toSet)
        {
            parisonMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetparisonActor()
        {
            return parisonActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetparisonActor(vtkActor toSet)
        {
            parisonActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkContourFilter Getcf()
        {
            return cf;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcf(vtkContourFilter toSet)
        {
            cf = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetcontourMapper()
        {
            return contourMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetcontourMapper(vtkPolyDataMapper toSet)
        {
            contourMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getcontours()
        {
            return contours;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcontours(vtkActor toSet)
        {
            contours = toSet;
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
  	if(write!= null){write.Dispose();}
  	if(dor!= null){dor.Dispose();}
  	if(do2ds!= null){do2ds.Dispose();}
  	if(fd2ad!= null){fd2ad.Dispose();}
  	if(warp!= null){warp.Dispose();}
  	if(connect!= null){connect.Dispose();}
  	if(moldMapper!= null){moldMapper.Dispose();}
  	if(moldActor!= null){moldActor.Dispose();}
  	if(connect2!= null){connect2.Dispose();}
  	if(parison!= null){parison.Dispose();}
  	if(normals2!= null){normals2.Dispose();}
  	if(lut!= null){lut.Dispose();}
  	if(parisonMapper!= null){parisonMapper.Dispose();}
  	if(parisonActor!= null){parisonActor.Dispose();}
  	if(cf!= null){cf.Dispose();}
  	if(contourMapper!= null){contourMapper.Dispose();}
  	if(contours!= null){contours.Dispose();}
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  }

}
//--- end of script --//

