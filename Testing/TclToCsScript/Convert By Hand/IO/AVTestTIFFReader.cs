using Kitware.VTK;
using System;
// input file is C:\VTK\IO\Testing\Tcl\TestTIFFReader.tcl
// output file is AVTestTIFFReader.cs
/// <summary>
/// The testing class derived from AVTestTIFFReader
/// </summary>
public class AVTestTIFFReaderClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVTestTIFFReader(String [] argv)
  {
  //Prefix Content is: ""
  
  // Image pipeline[]
  createReader = new vtkImageReader2Factory();
  reader = vtkImageReader2Factory.CreateImageReader2((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/beach.tif");
  reader.SetFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/beach.tif");
  // "beach.tif" image contains ORIENTATION tag which is []
  // ORIENTATION_TOPLEFT (row 0 top, col 0 lhs) type. The TIFF []
  // reader parses this tag and sets the internal TIFF image []
  // orientation accordingly.  To overwrite this orientation with a vtk[]
  // convention of ORIENTATION_BOTLEFT (row 0 bottom, col 0 lhs ), invoke[]
  // SetOrientationType method with parameter value of 4.[]
  ((vtkTIFFReader)reader).SetOrientationType(4);
  viewer = new vtkImageViewer();
  viewer.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  viewer.SetColorWindow((double)256);
  viewer.SetColorLevel((double)127.5);
  //make interface[]
  viewer.Render();
  //reader.Unregister(['UnRegister', 'viewer']) Skipped
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkImageReader2Factory createReader;
static vtkImageReader2 reader;
static vtkImageViewer viewer;


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
        public static vtkImageReader2Factory GetcreateReader()
        {
            return createReader;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetcreateReader(vtkImageReader2Factory toSet)
        {
            createReader = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageReader2 Getreader()
        {
            return reader;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setreader(vtkImageReader2 toSet)
        {
            reader = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageViewer Getviewer()
        {
            return viewer;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setviewer(vtkImageViewer toSet)
        {
            viewer = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(createReader!= null){createReader.Dispose();}
  	if(reader!= null){reader.Dispose();}
  	if(viewer!= null){viewer.Dispose();}
  }

}
//--- end of script --//

