using Kitware.VTK;
using System;
// input file is C:\VTK\VolumeRendering\Testing\Tcl\cursor3D.tcl
// output file is AVcursor3D.cs
/// <summary>
/// The testing class derived from AVcursor3D
/// </summary>
public class AVcursor3DClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVcursor3D(String [] argv)
  {
  //Prefix Content is: ""
  
  // This little example shows how a cursor can be created in []
  // image viewers, and renderers.  The standard TkImageViewerWidget and[]
  // TkRenderWidget bindings are used.  There is a new binding:[]
  // middle button in the image viewer sets the position of the cursor.  []
  // First we include the VTK Tcl packages which will make available []
  // all of the vtk commands to Tcl[]
  // Global values[]
  CURSOR_X = 20;
  CURSOR_Y = 20;
  CURSOR_Z = 20;
  IMAGE_MAG_X = 4;
  IMAGE_MAG_Y = 4;
  IMAGE_MAG_Z = 1;
  // Pipeline stuff[]
  reader = new vtkSLCReader();
  reader.SetFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/neghip.slc");
  // Cursor stuff[]
  magnify = new vtkImageMagnify();
  magnify.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  magnify.SetMagnificationFactors((int)IMAGE_MAG_X,(int)IMAGE_MAG_Y,(int)IMAGE_MAG_Z);
  image_cursor = new vtkImageCursor3D();
  image_cursor.SetInputConnection((vtkAlgorithmOutput)magnify.GetOutputPort());
  image_cursor.SetCursorPosition((double)CURSOR_X*IMAGE_MAG_X,(double)CURSOR_Y*IMAGE_MAG_Y,(double)CURSOR_Z*IMAGE_MAG_Z);
  image_cursor.SetCursorValue((double)255);
  image_cursor.SetCursorRadius((int)50*IMAGE_MAG_X);
  axes = new vtkAxes();
  axes.SymmetricOn();
  axes.SetOrigin((double)CURSOR_X,(double)CURSOR_Y,(double)CURSOR_Z);
  axes.SetScaleFactor((double)50.0);
  axes_mapper = vtkPolyDataMapper.New();
  axes_mapper.SetInputConnection((vtkAlgorithmOutput)axes.GetOutputPort());
  axesActor = new vtkActor();
  axesActor.SetMapper((vtkMapper)axes_mapper);
  axesActor.GetProperty().SetAmbient((double)0.5);
  // Image viewer stuff[]
  viewer = new vtkImageViewer();
  viewer.SetInputConnection((vtkAlgorithmOutput)image_cursor.GetOutputPort());
  viewer.SetZSlice((int)CURSOR_Z*IMAGE_MAG_Z);
  viewer.SetColorWindow((double)256);
  viewer.SetColorLevel((double)128);
  //method moved
  //method moved
  //method moved
  // Create transfer functions for opacity and color[]
  opacity_transfer_function = new vtkPiecewiseFunction();
  opacity_transfer_function.AddPoint((double)20,(double)0.0);
  opacity_transfer_function.AddPoint((double)255,(double)0.2);
  color_transfer_function = new vtkColorTransferFunction();
  color_transfer_function.AddRGBPoint((double)0,(double)0,(double)0,(double)0);
  color_transfer_function.AddRGBPoint((double)64,(double)1,(double)0,(double)0);
  color_transfer_function.AddRGBPoint((double)128,(double)0,(double)0,(double)1);
  color_transfer_function.AddRGBPoint((double)192,(double)0,(double)1,(double)0);
  color_transfer_function.AddRGBPoint((double)255,(double)0,(double).2,(double)0);
  // Create properties, mappers, volume actors, and ray cast function[]
  volume_property = new vtkVolumeProperty();
  volume_property.SetColor((vtkColorTransferFunction)color_transfer_function);
  volume_property.SetScalarOpacity((vtkPiecewiseFunction)opacity_transfer_function);
  composite_function = new vtkVolumeRayCastCompositeFunction();
  volume_mapper = new vtkVolumeRayCastMapper();
  volume_mapper.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  volume_mapper.SetVolumeRayCastFunction((vtkVolumeRayCastFunction)composite_function);
  volume = new vtkVolume();
  volume.SetMapper((vtkAbstractVolumeMapper)volume_mapper);
  volume.SetProperty((vtkVolumeProperty)volume_property);
  // Create outline[]
  outline = new vtkOutlineFilter();
  outline.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  outline_mapper = vtkPolyDataMapper.New();
  outline_mapper.SetInputConnection((vtkAlgorithmOutput)outline.GetOutputPort());
  outlineActor = new vtkActor();
  outlineActor.SetMapper((vtkMapper)outline_mapper);
  outlineActor.GetProperty().SetColor((double)1,(double)1,(double)1);
  // Create the renderer[]
  ren1 = vtkRenderer.New();
  ren1.AddActor((vtkProp)axesActor);
  ren1.AddVolume((vtkProp)volume);
  ren1.SetBackground((double)0.1,(double)0.2,(double)0.4);
  renWin2 = vtkRenderWindow.New();
  renWin2.AddRenderer((vtkRenderer)ren1);
  renWin2.SetSize((int)256,(int)256);
  // Create the GUI: two renderer widgets and a quit button[]
  //tk window skipped..
  // Set the window manager (wm command) so that it registers a[]
  // command to handle the WM_DELETE_WINDOW protocal request. This[]
  // request is triggered when the widget is closed using the standard[]
  // window manager icons or buttons. In this case the exit callback[]
  // will be called and it will free up any objects we created then exit[]
  // the application.[]
  // Help label, frame and quit button[]
  //tk window skipped..
  //tk window skipped..
  //tk window skipped..
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static int CURSOR_X;
static int CURSOR_Y;
static int CURSOR_Z;
static int IMAGE_MAG_X;
static int IMAGE_MAG_Y;
static int IMAGE_MAG_Z;
static vtkSLCReader reader;
static vtkImageMagnify magnify;
static vtkImageCursor3D image_cursor;
static vtkAxes axes;
static vtkPolyDataMapper axes_mapper;
static vtkActor axesActor;
static vtkImageViewer viewer;
static vtkPiecewiseFunction opacity_transfer_function;
static vtkColorTransferFunction color_transfer_function;
static vtkVolumeProperty volume_property;
static vtkVolumeRayCastCompositeFunction composite_function;
static vtkVolumeRayCastMapper volume_mapper;
static vtkVolume volume;
static vtkOutlineFilter outline;
static vtkPolyDataMapper outline_mapper;
static vtkActor outlineActor;
static vtkRenderer ren1;
static vtkRenderWindow renWin2;
static Object help_label;
static Object display_frame;
static Object quit_button;


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
        public static int GetCURSOR_X()
        {
            return CURSOR_X;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetCURSOR_X(int toSet)
        {
            CURSOR_X = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int GetCURSOR_Y()
        {
            return CURSOR_Y;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetCURSOR_Y(int toSet)
        {
            CURSOR_Y = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int GetCURSOR_Z()
        {
            return CURSOR_Z;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetCURSOR_Z(int toSet)
        {
            CURSOR_Z = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int GetIMAGE_MAG_X()
        {
            return IMAGE_MAG_X;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetIMAGE_MAG_X(int toSet)
        {
            IMAGE_MAG_X = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int GetIMAGE_MAG_Y()
        {
            return IMAGE_MAG_Y;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetIMAGE_MAG_Y(int toSet)
        {
            IMAGE_MAG_Y = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int GetIMAGE_MAG_Z()
        {
            return IMAGE_MAG_Z;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetIMAGE_MAG_Z(int toSet)
        {
            IMAGE_MAG_Z = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkSLCReader Getreader()
        {
            return reader;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setreader(vtkSLCReader toSet)
        {
            reader = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageMagnify Getmagnify()
        {
            return magnify;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setmagnify(vtkImageMagnify toSet)
        {
            magnify = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageCursor3D Getimage_cursor()
        {
            return image_cursor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setimage_cursor(vtkImageCursor3D toSet)
        {
            image_cursor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkAxes Getaxes()
        {
            return axes;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setaxes(vtkAxes toSet)
        {
            axes = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Getaxes_mapper()
        {
            return axes_mapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setaxes_mapper(vtkPolyDataMapper toSet)
        {
            axes_mapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaxesActor()
        {
            return axesActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaxesActor(vtkActor toSet)
        {
            axesActor = toSet;
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
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPiecewiseFunction Getopacity_transfer_function()
        {
            return opacity_transfer_function;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setopacity_transfer_function(vtkPiecewiseFunction toSet)
        {
            opacity_transfer_function = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkColorTransferFunction Getcolor_transfer_function()
        {
            return color_transfer_function;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcolor_transfer_function(vtkColorTransferFunction toSet)
        {
            color_transfer_function = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkVolumeProperty Getvolume_property()
        {
            return volume_property;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setvolume_property(vtkVolumeProperty toSet)
        {
            volume_property = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkVolumeRayCastCompositeFunction Getcomposite_function()
        {
            return composite_function;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcomposite_function(vtkVolumeRayCastCompositeFunction toSet)
        {
            composite_function = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkVolumeRayCastMapper Getvolume_mapper()
        {
            return volume_mapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setvolume_mapper(vtkVolumeRayCastMapper toSet)
        {
            volume_mapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkVolume Getvolume()
        {
            return volume;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setvolume(vtkVolume toSet)
        {
            volume = toSet;
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
        public static vtkPolyDataMapper Getoutline_mapper()
        {
            return outline_mapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setoutline_mapper(vtkPolyDataMapper toSet)
        {
            outline_mapper = toSet;
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
        public static vtkRenderWindow GetrenWin2()
        {
            return renWin2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetrenWin2(vtkRenderWindow toSet)
        {
            renWin2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static Object Gethelp_label()
        {
            return help_label;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sethelp_label(Object toSet)
        {
            help_label = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static Object Getdisplay_frame()
        {
            return display_frame;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setdisplay_frame(Object toSet)
        {
            display_frame = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static Object Getquit_button()
        {
            return quit_button;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setquit_button(Object toSet)
        {
            quit_button = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(reader!= null){reader.Dispose();}
  	if(magnify!= null){magnify.Dispose();}
  	if(image_cursor!= null){image_cursor.Dispose();}
  	if(axes!= null){axes.Dispose();}
  	if(axes_mapper!= null){axes_mapper.Dispose();}
  	if(axesActor!= null){axesActor.Dispose();}
  	if(viewer!= null){viewer.Dispose();}
  	if(opacity_transfer_function!= null){opacity_transfer_function.Dispose();}
  	if(color_transfer_function!= null){color_transfer_function.Dispose();}
  	if(volume_property!= null){volume_property.Dispose();}
  	if(composite_function!= null){composite_function.Dispose();}
  	if(volume_mapper!= null){volume_mapper.Dispose();}
  	if(volume!= null){volume.Dispose();}
  	if(outline!= null){outline.Dispose();}
  	if(outline_mapper!= null){outline_mapper.Dispose();}
  	if(outlineActor!= null){outlineActor.Dispose();}
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin2!= null){renWin2.Dispose();}
  }

}
//--- end of script --//

