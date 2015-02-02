using System.IO;
using Kitware.VTK;
using System;
// input file is C:\VTK\IO\Testing\Tcl\TestImageWriters.tcl
// output file is AVTestImageWriters.cs
/// <summary>
/// The testing class derived from AVTestImageWriters
/// </summary>
public class AVTestImageWritersClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVTestImageWriters(String [] argv)
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
  sp = new vtkStructuredPoints();
  sp.SetDimensions(image1.GetOutput().GetDimensions()[0],image1.GetOutput().GetDimensions()[1],image1.GetOutput().GetDimensions()[2]);
  sp.SetExtent(
      image1.GetOutput().GetExtent()[0],
      image1.GetOutput().GetExtent()[1],
      image1.GetOutput().GetExtent()[2],
      image1.GetOutput().GetExtent()[3],
      image1.GetOutput().GetExtent()[4],
      image1.GetOutput().GetExtent()[5]);
  //sp.SetScalarType((int)image1.GetOutput().GetScalarType());
  //sp.SetNumberOfScalarComponents((int)image1.GetOutput().GetNumberOfScalarComponents());
  vtkDataObject.SetPointDataActiveScalarInfo(sp.GetInformation(), (int)image1.GetOutput().GetScalarType(), (int)image1.GetOutput().GetNumberOfScalarComponents());
          
  sp.GetPointData().SetScalars((vtkDataArray)image1.GetOutput().GetPointData().GetScalars());
  luminance = new vtkImageLuminance();
  luminance.SetInputData((vtkDataObject)sp);
  //[]
  // write to the temp directory if possible, otherwise use .[]
  //[]
  dir = ".";
      dir = TclToCsScriptTestDriver.GetTempDirectory();

  
  // make sure it is writeable first[]
  try
  {
     channel = new StreamWriter("" + (dir.ToString()) + "/test.tmp");
      tryCatchError = "NOERROR";
  }
  catch(Exception)
  {tryCatchError = "ERROR";}
  
if(tryCatchError.Equals("NOERROR"))
  {
      channel.Close();
      File.Delete("" + (dir.ToString()) + "/test.tmp");
      tiff1 = new vtkTIFFWriter();
      tiff1.SetInputConnection((vtkAlgorithmOutput)image1.GetOutputPort());
      tiff1.SetFileName((string)"" + (dir.ToString()) + "/tiff1.tif");
      tiff2 = new vtkTIFFWriter();
      tiff2.SetInputConnection((vtkAlgorithmOutput)luminance.GetOutputPort());
      tiff2.SetFileName((string)"" + (dir.ToString()) + "/tiff2.tif");
      bmp1 = new vtkBMPWriter();
      bmp1.SetInputConnection((vtkAlgorithmOutput)image1.GetOutputPort());
      bmp1.SetFileName((string)"" + (dir.ToString()) + "/bmp1.bmp");
      bmp2 = new vtkBMPWriter();
      bmp2.SetInputConnection((vtkAlgorithmOutput)luminance.GetOutputPort());
      bmp2.SetFileName((string)"" + (dir.ToString()) + "/bmp2.bmp");
      pnm1 = new vtkPNMWriter();
      pnm1.SetInputConnection((vtkAlgorithmOutput)image1.GetOutputPort());
      pnm1.SetFileName((string)"" + (dir.ToString()) + "/pnm1.pnm");
      pnm2 = new vtkPNMWriter();
      pnm2.SetInputConnection((vtkAlgorithmOutput)luminance.GetOutputPort());
      pnm2.SetFileName((string)"" + (dir.ToString()) + "/pnm2.pnm");
      psw1 = new vtkPostScriptWriter();
      psw1.SetInputConnection((vtkAlgorithmOutput)image1.GetOutputPort());
      psw1.SetFileName((string)"" + (dir.ToString()) + "/psw1.ps");
      psw2 = new vtkPostScriptWriter();
      psw2.SetInputConnection((vtkAlgorithmOutput)luminance.GetOutputPort());
      psw2.SetFileName((string)"" + (dir.ToString()) + "/psw2.ps");
      pngw1 = new vtkPNGWriter();
      pngw1.SetInputConnection((vtkAlgorithmOutput)image1.GetOutputPort());
      pngw1.SetFileName((string)"" + (dir.ToString()) + "/pngw1.png");
      pngw2 = new vtkPNGWriter();
      pngw2.SetInputConnection((vtkAlgorithmOutput)luminance.GetOutputPort());
      pngw2.SetFileName((string)"" + (dir.ToString()) + "/pngw2.png");
      jpgw1 = new vtkJPEGWriter();
      jpgw1.SetInputConnection((vtkAlgorithmOutput)image1.GetOutputPort());
      jpgw1.SetFileName((string)"" + (dir.ToString()) + "/jpgw1.jpg");
      jpgw2 = new vtkJPEGWriter();
      jpgw2.SetInputConnection((vtkAlgorithmOutput)luminance.GetOutputPort());
      jpgw2.SetFileName((string)"" + (dir.ToString()) + "/jpgw2.jpg");
      tiff1.Write();
      tiff2.Write();
      bmp1.Write();
      bmp2.Write();
      pnm1.Write();
      pnm2.Write();
      psw1.Write();
      psw2.Write();
      pngw1.Write();
      pngw2.Write();
      jpgw1.Write();
      jpgw2.Write();
      File.Delete("" + (dir.ToString()) + "/tiff1.tif");
      File.Delete("" + (dir.ToString()) + "/tiff2.tif");
      File.Delete("" + (dir.ToString()) + "/bmp1.bmp");
      File.Delete("" + (dir.ToString()) + "/bmp2.bmp");
      File.Delete("" + (dir.ToString()) + "/pnm1.pnm");
      File.Delete("" + (dir.ToString()) + "/pnm2.pnm");
      File.Delete("" + (dir.ToString()) + "/psw1.ps");
      File.Delete("" + (dir.ToString()) + "/psw2.ps");
      File.Delete("" + (dir.ToString()) + "/pngw1.png");
      File.Delete("" + (dir.ToString()) + "/pngw2.png");
      File.Delete("" + (dir.ToString()) + "/jpgw1.jpg");
      File.Delete("" + (dir.ToString()) + "/jpgw2.jpg");
    }

  
  viewer = new vtkImageViewer();
  viewer.SetInputConnection((vtkAlgorithmOutput)luminance.GetOutputPort());
  viewer.SetColorWindow((double)255);
  viewer.SetColorLevel((double)127.5);
  viewer.Render();
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkTIFFReader image1;
static vtkStructuredPoints sp;
static vtkImageLuminance luminance;
static string dir;
static string tryCatchError;
static StreamWriter channel;
static vtkTIFFWriter tiff1;
static vtkTIFFWriter tiff2;
static vtkBMPWriter bmp1;
static vtkBMPWriter bmp2;
static vtkPNMWriter pnm1;
static vtkPNMWriter pnm2;
static vtkPostScriptWriter psw1;
static vtkPostScriptWriter psw2;
static vtkPNGWriter pngw1;
static vtkPNGWriter pngw2;
static vtkJPEGWriter jpgw1;
static vtkJPEGWriter jpgw2;
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
        public static vtkStructuredPoints Getsp()
        {
            return sp;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setsp(vtkStructuredPoints toSet)
        {
            sp = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageLuminance Getluminance()
        {
            return luminance;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setluminance(vtkImageLuminance toSet)
        {
            luminance = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static string Getdir()
        {
            return dir;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setdir(string toSet)
        {
            dir = toSet;
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
        public static vtkTIFFWriter Gettiff1()
        {
            return tiff1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settiff1(vtkTIFFWriter toSet)
        {
            tiff1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTIFFWriter Gettiff2()
        {
            return tiff2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settiff2(vtkTIFFWriter toSet)
        {
            tiff2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkBMPWriter Getbmp1()
        {
            return bmp1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbmp1(vtkBMPWriter toSet)
        {
            bmp1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkBMPWriter Getbmp2()
        {
            return bmp2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbmp2(vtkBMPWriter toSet)
        {
            bmp2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPNMWriter Getpnm1()
        {
            return pnm1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpnm1(vtkPNMWriter toSet)
        {
            pnm1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPNMWriter Getpnm2()
        {
            return pnm2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpnm2(vtkPNMWriter toSet)
        {
            pnm2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPostScriptWriter Getpsw1()
        {
            return psw1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpsw1(vtkPostScriptWriter toSet)
        {
            psw1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPostScriptWriter Getpsw2()
        {
            return psw2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpsw2(vtkPostScriptWriter toSet)
        {
            psw2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPNGWriter Getpngw1()
        {
            return pngw1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpngw1(vtkPNGWriter toSet)
        {
            pngw1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPNGWriter Getpngw2()
        {
            return pngw2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpngw2(vtkPNGWriter toSet)
        {
            pngw2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkJPEGWriter Getjpgw1()
        {
            return jpgw1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setjpgw1(vtkJPEGWriter toSet)
        {
            jpgw1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkJPEGWriter Getjpgw2()
        {
            return jpgw2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setjpgw2(vtkJPEGWriter toSet)
        {
            jpgw2 = toSet;
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
  	if(sp!= null){sp.Dispose();}
  	if(luminance!= null){luminance.Dispose();}
  	if(tiff1!= null){tiff1.Dispose();}
  	if(tiff2!= null){tiff2.Dispose();}
  	if(bmp1!= null){bmp1.Dispose();}
  	if(bmp2!= null){bmp2.Dispose();}
  	if(pnm1!= null){pnm1.Dispose();}
  	if(pnm2!= null){pnm2.Dispose();}
  	if(psw1!= null){psw1.Dispose();}
  	if(psw2!= null){psw2.Dispose();}
  	if(pngw1!= null){pngw1.Dispose();}
  	if(pngw2!= null){pngw2.Dispose();}
  	if(jpgw1!= null){jpgw1.Dispose();}
  	if(jpgw2!= null){jpgw2.Dispose();}
  	if(viewer!= null){viewer.Dispose();}
  }

}
//--- end of script --//

