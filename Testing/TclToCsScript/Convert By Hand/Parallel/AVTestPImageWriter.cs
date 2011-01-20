using System.IO;
using Kitware.VTK;
using System;
// input file is C:\VTK\Parallel\Testing\Tcl\TestPImageWriter.tcl
// output file is AVTestPImageWriter.cs
/// <summary>
/// The testing class derived from AVTestPImageWriter
/// </summary>
public class AVTestPImageWriterClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVTestPImageWriter(String [] argv)
  {
  //Prefix Content is: ""
  
  // Image pipeline[]
  image1 = new vtkTIFFReader();
  image1.SetFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/beach.tif");
  // "beach.tif" image contains ORIENTATION tag which is []
  // ORIENTATION_TOPLEFT (row 0 top, col 0 lhs) type. The TIFF []
  // reader parses this tag and sets the internal TIFF image []
  // orientation accordingly.  To overwrite this orientation with a vtk[]
  // convention of ORIENTATION_BOTLEFT (row 0 bottom, col 0 lhs ), invoke[]
  // SetOrientationType method with parameter value of 4.[]
  image1.SetOrientationType((uint)4);
  image1.Update();
  //[]
  // If the current directory is writable, then test the witers[]
  //[]
  try
  {
      channel = new StreamWriter("test.tmp");
      tryCatchError = "NOERROR";
  }
  catch(Exception)
  {tryCatchError = "ERROR";}
  
if(tryCatchError.Equals("NOERROR"))
  {
      channel.Close();
      File.Delete("test.tmp");
      piw = new vtkPImageWriter();
      piw.SetInputConnection((vtkAlgorithmOutput)image1.GetOutputPort());
      piw.SetFileName((string)"piw.raw");
      piw.SetMemoryLimit((uint)1);
      piw.Write();
      File.Delete("piw.raw");
    }

  
  viewer = new vtkImageViewer();
  viewer.SetInputConnection((vtkAlgorithmOutput)image1.GetOutputPort());
  viewer.SetColorWindow((double)255);
  viewer.SetColorLevel((double)127.5);
  viewer.Render();
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkTIFFReader image1;
static string tryCatchError;
static StreamWriter channel;
static vtkPImageWriter piw;
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
        public static vtkTIFFReader Getimage1()
        {
            return image1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setimage1(vtkTIFFReader toSet)
        {
            image1 = toSet;
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
        public static vtkPImageWriter Getpiw()
        {
            return piw;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpiw(vtkPImageWriter toSet)
        {
            piw = toSet;
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
  	if(image1!= null){image1.Dispose();}
  	if(piw!= null){piw.Dispose();}
  	if(viewer!= null){viewer.Dispose();}
  }

}
//--- end of script --//

