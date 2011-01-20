using Kitware.VTK;
using System;
// input file is C:\VTK\Parallel\Testing\Tcl\TestImageStreamer.tcl
// output file is AVTestImageStreamer.cs
/// <summary>
/// The testing class derived from AVTestImageStreamer
/// </summary>
public class AVTestImageStreamerClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVTestImageStreamer(String [] argv)
  {
  //Prefix Content is: ""
  
  reader = new vtkImageReader();
  reader.ReleaseDataFlagOff();
  reader.SetDataByteOrderToLittleEndian();
  reader.SetDataExtent((int)0,(int)63,(int)0,(int)63,(int)1,(int)93);
  reader.SetFilePrefix((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/headsq/quarter");
  reader.SetDataMask((int)0x7fff);
  rangeStart = 0.0;
  rangeEnd = 0.2;
  LUT = new vtkLookupTable();
  LUT.SetTableRange((double)0,(double)1800);
  LUT.SetSaturationRange((double)1,(double)1);
  LUT.SetHueRange((double)rangeStart,(double)rangeEnd);
  LUT.SetValueRange((double)1,(double)1);
  LUT.SetAlphaRange((double)1,(double)1);
  LUT.Build();
  // added these unused default arguments so that the prototype[]
  // matches as required in python.[]
  //method moved
  mapToRGBA = new vtkImageMapToColors();
  mapToRGBA.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  mapToRGBA.SetOutputFormatToRGBA();
  mapToRGBA.SetLookupTable((vtkScalarsToColors)LUT);
 
  mapToRGBA.EndEvt += new Kitware.VTK.vtkObject.vtkObjectEventHandler(changeLUT_Command.Execute);

  streamer = new vtkMemoryLimitImageDataStreamer();
  streamer.SetInputConnection((vtkAlgorithmOutput)mapToRGBA.GetOutputPort());
  streamer.SetMemoryLimit((uint)100);
  streamer.UpdateWholeExtent();
  // set the window/level to 255.0/127.5 to view full range[]
  viewer = new vtkImageViewer();
  viewer.SetInputConnection((vtkAlgorithmOutput)streamer.GetOutputPort());
  viewer.SetColorWindow((double)255.0);
  viewer.SetColorLevel((double)127.5);
  viewer.SetZSlice((int)50);
  viewer.Render();
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkImageReader reader;
static double rangeStart;
static double rangeEnd;
static vtkLookupTable LUT;
static vtkImageMapToColors mapToRGBA;
static vtkMemoryLimitImageDataStreamer streamer;
static vtkImageViewer viewer;


/// <summary>
/// Event handler for EndEvt
/// </summary>
public class changeLUT_Command
{  ///<summary>execute command</summary>
  public static void Execute(vtkObject sender, vtkObjectEventArgs e)
  {
  //Global Variable Declaration Skipped
  rangeStart = rangeStart+0.1;
  rangeEnd = rangeEnd+0.1;
  if ((rangeEnd) > 1.0)
    {
    rangeStart = 0.0;
    rangeEnd = 0.2;
    }

  LUT.SetHueRange((double)rangeStart,(double)rangeEnd);
  LUT.Build();
  }
}

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
        public static vtkImageReader Getreader()
        {
            return reader;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setreader(vtkImageReader toSet)
        {
            reader = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double GetrangeStart()
        {
            return rangeStart;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetrangeStart(double toSet)
        {
            rangeStart = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double GetrangeEnd()
        {
            return rangeEnd;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetrangeEnd(double toSet)
        {
            rangeEnd = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkLookupTable GetLUT()
        {
            return LUT;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetLUT(vtkLookupTable toSet)
        {
            LUT = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageMapToColors GetmapToRGBA()
        {
            return mapToRGBA;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetmapToRGBA(vtkImageMapToColors toSet)
        {
            mapToRGBA = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkMemoryLimitImageDataStreamer Getstreamer()
        {
            return streamer;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setstreamer(vtkMemoryLimitImageDataStreamer toSet)
        {
            streamer = toSet;
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
  	if(reader!= null){reader.Dispose();}
  	if(LUT!= null){LUT.Dispose();}
  	if(mapToRGBA!= null){mapToRGBA.Dispose();}
  	if(streamer!= null){streamer.Dispose();}
  	if(viewer!= null){viewer.Dispose();}
  }

}
//--- end of script --//

