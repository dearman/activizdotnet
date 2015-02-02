using System.IO;
using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\fieldToPolyData.tcl
// output file is AVfieldToPolyData.cs
/// <summary>
/// The testing class derived from AVfieldToPolyData
/// </summary>
public class AVfieldToPolyDataClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVfieldToPolyData(String [] argv)
  {
  //Prefix Content is: ""
  
  // This example demonstrates the reading of a field and conversion to PolyData[]
  // The output should be the same as polyEx.tcl.[]
  // get the interactor ui[]
  // Create a reader and write out the field[]
  reader = new vtkPolyDataReader();
  reader.SetFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/polyEx.vtk");
  ds2do = new vtkDataSetToDataObjectFilter();
  ds2do.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  try
    {
    channel = new StreamWriter("PolyField.vtk");
    tryCatchError = "NOERROR";
    }
  catch(Exception)
  {tryCatchError = "ERROR";}
  
  if(tryCatchError.Equals("NOERROR"))
    {
      channel.Close();
      writer = new vtkDataObjectWriter();
      writer.SetInputConnection((vtkAlgorithmOutput)ds2do.GetOutputPort());
      writer.SetFileName((string)"PolyField.vtk");
      writer.Write();
      // create pipeline[]
      //[]
      dor = new vtkDataObjectReader();
      dor.SetFileName((string)"PolyField.vtk");
      do2ds = new vtkDataObjectToDataSetFilter();
      do2ds.SetInputConnection((vtkAlgorithmOutput)dor.GetOutputPort());
      do2ds.SetDataSetTypeToPolyData();
      do2ds.SetPointComponent((int)0,(string)"Points",(int)0);
      do2ds.SetPointComponent((int)1,(string)"Points",(int)1);
      do2ds.SetPointComponent((int)2,(string)"Points",(int)2);
      do2ds.SetPolysComponent((string)"Polys",(int)0);

      fd2ad = new vtkFieldDataToAttributeDataFilter();
      fd2ad.SetInputConnection(do2ds.GetOutputPort());
      fd2ad.SetInputFieldToDataObjectField();
      fd2ad.SetOutputAttributeDataToPointData();
      fd2ad.SetScalarComponent((int)0,(string)"my_scalars",(int)0);
      mapper = vtkPolyDataMapper.New();
      mapper.SetInputConnection(fd2ad.GetOutputPort());
      mapper.SetScalarRange((double)((vtkDataSet)fd2ad.GetOutput()).GetScalarRange()[0],(double)((vtkDataSet)fd2ad.GetOutput()).GetScalarRange()[1]);
      actor = new vtkActor();
      actor.SetMapper((vtkMapper)mapper);
      // Create the RenderWindow, Renderer and both Actors[]
      ren1 = vtkRenderer.New();
      renWin = vtkRenderWindow.New();
      renWin.AddRenderer((vtkRenderer)ren1);
      iren = new vtkRenderWindowInteractor();
      iren.SetRenderWindow((vtkRenderWindow)renWin);
      ren1.AddActor((vtkProp)actor);
      ren1.SetBackground((double)1,(double)1,(double)1);
      renWin.SetSize((int)300,(int)300);
      ren1.ResetCamera();
      cam1 = ren1.GetActiveCamera();
      cam1.SetClippingRange((double).348,(double)17.43);
      cam1.SetPosition((double)2.92,(double)2.62,(double)-0.836);
      cam1.SetViewUp((double)-0.436,(double)-0.067,(double)-0.897);
      cam1.Azimuth((double)90);
      // render the image[]
      //[]
      renWin.Render();
          File.Delete("PolyField.vtk");  
    }

  
  // prevent the tk window from showing up then start the event loop[]
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkPolyDataReader reader;
static vtkDataSetToDataObjectFilter ds2do;
static string tryCatchError;
static StreamWriter channel;
static vtkDataObjectWriter writer;
static vtkDataObjectReader dor;
static vtkDataObjectToDataSetFilter do2ds;
static vtkFieldDataToAttributeDataFilter fd2ad;
static vtkPolyDataMapper mapper;
static vtkActor actor;
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
        public static vtkPolyDataReader Getreader()
        {
            return reader;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setreader(vtkPolyDataReader toSet)
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
        public static vtkPolyDataMapper Getmapper()
        {
            return mapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setmapper(vtkPolyDataMapper toSet)
        {
            mapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getactor()
        {
            return actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setactor(vtkActor toSet)
        {
            actor = toSet;
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
  	if(ds2do!= null){ds2do.Dispose();}
  	if(writer!= null){writer.Dispose();}
  	if(dor!= null){dor.Dispose();}
  	if(do2ds!= null){do2ds.Dispose();}
  	if(fd2ad!= null){fd2ad.Dispose();}
  	if(mapper!= null){mapper.Dispose();}
  	if(actor!= null){actor.Dispose();}
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(cam1!= null){cam1.Dispose();}
  }

}
//--- end of script --//

