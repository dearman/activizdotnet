using Kitware.VTK;
using System;
// input file is C:\VTK\VolumeRendering\Testing\Tcl\gaussian.tcl
// output file is AVgaussian.cs
/// <summary>
/// The testing class derived from AVgaussian
/// </summary>
public class AVgaussianClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVgaussian(String [] argv)
  {
  //Prefix Content is: ""
  
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  renWin.SetSize((int)300,(int)300);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  camera = new vtkCamera();
  camera.ParallelProjectionOn();
  camera.SetViewUp((double)0,(double)1,(double)0);
  camera.SetFocalPoint((double)12,(double)10.5,(double)15);
  camera.SetPosition((double)-70,(double)15,(double)34);
  camera.ComputeViewPlaneNormal();
  ren1.SetActiveCamera((vtkCamera)camera);
  // Create the reader for the data[]
  //vtkStructuredPointsReader reader[]
  reader = new vtkGaussianCubeReader();
  reader.SetFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/m4_TotalDensity.cube");
  reader.SetHBScale((double)1.1);
  reader.SetBScale((double)10);
  reader.Update();
  range = reader.GetGridOutput().GetPointData().GetScalars().GetRange();
  min = (double)(lindex(range,0));
  max = (double)(lindex(range,1));
  readerSS = new vtkImageShiftScale();
  readerSS.SetInput((vtkDataObject)reader.GetGridOutput());
  readerSS.SetShift((double)min*-1);
  readerSS.SetScale((double)255/(max-min));
  readerSS.SetOutputScalarTypeToUnsignedChar();
  bounds = new vtkOutlineFilter();
  bounds.SetInput((vtkDataObject)reader.GetGridOutput());
  boundsMapper = vtkPolyDataMapper.New();
  boundsMapper.SetInputConnection((vtkAlgorithmOutput)bounds.GetOutputPort());
  boundsActor = new vtkActor();
  boundsActor.SetMapper((vtkMapper)boundsMapper);
  boundsActor.GetProperty().SetColor((double)0,(double)0,(double)0);
  contour = new vtkContourFilter();
  contour.SetInput((vtkDataObject)reader.GetGridOutput());
  contour.GenerateValues((int)5,(double)0,(double).05);
  contourMapper = vtkPolyDataMapper.New();
  contourMapper.SetInputConnection((vtkAlgorithmOutput)contour.GetOutputPort());
  contourMapper.SetScalarRange((double)0,(double).1);
  ((vtkLookupTable)contourMapper.GetLookupTable()).SetHueRange(0.32,0);
  contourActor = new vtkActor();
  contourActor.SetMapper((vtkMapper)contourMapper);
  contourActor.GetProperty().SetOpacity((double).5);
  // Create transfer mapping scalar value to opacity[]
  opacityTransferFunction = new vtkPiecewiseFunction();
  opacityTransferFunction.AddPoint((double)0,(double)0.01);
  opacityTransferFunction.AddPoint((double)255,(double)0.35);
  opacityTransferFunction.ClampingOn();
  // Create transfer mapping scalar value to color[]
  colorTransferFunction = new vtkColorTransferFunction();
  colorTransferFunction.AddHSVPoint((double)0.0,(double)0.66,(double)1.0,(double)1.0);
  colorTransferFunction.AddHSVPoint((double)50.0,(double)0.33,(double)1.0,(double)1.0);
  colorTransferFunction.AddHSVPoint((double)100.0,(double)0.00,(double)1.0,(double)1.0);
  // The property describes how the data will look[]
  volumeProperty = new vtkVolumeProperty();
  volumeProperty.SetColor((vtkColorTransferFunction)colorTransferFunction);
  volumeProperty.SetScalarOpacity((vtkPiecewiseFunction)opacityTransferFunction);
  volumeProperty.SetInterpolationTypeToLinear();
  // The mapper / ray cast function know how to render the data[]
  compositeFunction = new vtkVolumeRayCastCompositeFunction();
  volumeMapper = new vtkVolumeRayCastMapper();
  //vtkVolumeTextureMapper2D volumeMapper[]
  volumeMapper.SetVolumeRayCastFunction((vtkVolumeRayCastFunction)compositeFunction);
  volumeMapper.SetInputConnection((vtkAlgorithmOutput)readerSS.GetOutputPort());
  // The volume holds the mapper and the property and[]
  // can be used to position/orient the volume[]
  volume = new vtkVolume();
  volume.SetMapper((vtkAbstractVolumeMapper)volumeMapper);
  volume.SetProperty((vtkVolumeProperty)volumeProperty);
  ren1.AddVolume((vtkProp)volume);
  //ren1 AddActor contourActor[]
  ren1.AddActor((vtkProp)boundsActor);
  //#####################################################################[]
  Sphere = new vtkSphereSource();
  Sphere.SetCenter((double)0,(double)0,(double)0);
  Sphere.SetRadius((double)1);
  Sphere.SetThetaResolution((int)16);
  Sphere.SetStartTheta((double)0);
  Sphere.SetEndTheta((double)360);
  Sphere.SetPhiResolution((int)16);
  Sphere.SetStartPhi((double)0);
  Sphere.SetEndPhi((double)180);
  Glyph = new vtkGlyph3D();
  Glyph.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  Glyph.SetOrient((int)1);
  Glyph.SetColorMode((int)1);
  //Glyph ScalingOn[]
  Glyph.SetScaleMode((int)2);
  Glyph.SetScaleFactor((double).6);
  Glyph.SetSource((vtkPolyData)Sphere.GetOutput());
  AtomsMapper = vtkPolyDataMapper.New();
  AtomsMapper.SetInputConnection((vtkAlgorithmOutput)Glyph.GetOutputPort());
  AtomsMapper.SetImmediateModeRendering((int)1);
  AtomsMapper.UseLookupTableScalarRangeOff();
  AtomsMapper.SetScalarVisibility((int)1);
  AtomsMapper.SetScalarModeToDefault();
  Atoms = new vtkActor();
  Atoms.SetMapper((vtkMapper)AtomsMapper);
  Atoms.GetProperty().SetRepresentationToSurface();
  Atoms.GetProperty().SetInterpolationToGouraud();
  Atoms.GetProperty().SetAmbient((double)0.15);
  Atoms.GetProperty().SetDiffuse((double)0.85);
  Atoms.GetProperty().SetSpecular((double)0.1);
  Atoms.GetProperty().SetSpecularPower((double)100);
  Atoms.GetProperty().SetSpecularColor((double)1,(double)1,(double)1);
  Atoms.GetProperty().SetColor((double)1,(double)1,(double)1);
  Tube = new vtkTubeFilter();
  Tube.SetInputConnection((vtkAlgorithmOutput)reader.GetOutputPort());
  Tube.SetNumberOfSides((int)16);
  Tube.SetCapping((int)0);
  Tube.SetRadius((double)0.2);
  Tube.SetVaryRadius((int)0);
  Tube.SetRadiusFactor((double)10);
  BondsMapper = vtkPolyDataMapper.New();
  BondsMapper.SetInputConnection((vtkAlgorithmOutput)Tube.GetOutputPort());
  BondsMapper.SetImmediateModeRendering((int)1);
  BondsMapper.UseLookupTableScalarRangeOff();
  BondsMapper.SetScalarVisibility((int)1);
  BondsMapper.SetScalarModeToDefault();
  Bonds = new vtkActor();
  Bonds.SetMapper((vtkMapper)BondsMapper);
  Bonds.GetProperty().SetRepresentationToSurface();
  Bonds.GetProperty().SetInterpolationToGouraud();
  Bonds.GetProperty().SetAmbient((double)0.15);
  Bonds.GetProperty().SetDiffuse((double)0.85);
  Bonds.GetProperty().SetSpecular((double)0.1);
  Bonds.GetProperty().SetSpecularPower((double)100);
  Bonds.GetProperty().SetSpecularColor((double)1,(double)1,(double)1);
  Bonds.GetProperty().SetColor((double)1,(double)1,(double)1);
  ren1.AddActor((vtkProp)Bonds);
  ren1.AddActor((vtkProp)Atoms);
  //###################################################[]
  ren1.SetBackground((double)1,(double)1,(double)1);
  ren1.ResetCamera();
  renWin.Render();
  //method moved
  
  renWin.AbortCheckEvt += new Kitware.VTK.vtkObject.vtkObjectEventHandler(TkCheckAbort_Command.Execute); 
  iren.Initialize();
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkCamera camera;
static vtkGaussianCubeReader reader;
static double[] range;
static double min;
static double max;
static vtkImageShiftScale readerSS;
static vtkOutlineFilter bounds;
static vtkPolyDataMapper boundsMapper;
static vtkActor boundsActor;
static vtkContourFilter contour;
static vtkPolyDataMapper contourMapper;
static vtkActor contourActor;
static vtkPiecewiseFunction opacityTransferFunction;
static vtkColorTransferFunction colorTransferFunction;
static vtkVolumeProperty volumeProperty;
static vtkVolumeRayCastCompositeFunction compositeFunction;
static vtkVolumeRayCastMapper volumeMapper;
static vtkVolume volume;
static vtkSphereSource Sphere;
static vtkGlyph3D Glyph;
static vtkPolyDataMapper AtomsMapper;
static vtkActor Atoms;
static vtkTubeFilter Tube;
static vtkPolyDataMapper BondsMapper;
static vtkActor Bonds;
static int foo;


/// <summary>
/// Creates a class that contains a method for AddObserver and simlar methods to use
/// </summary>
public class TkCheckAbort_Command
{  ///<summary>execute command</summary>
  public static void Execute(vtkObject sender, vtkObjectEventArgs e)
  {
  foo = renWin.GetEventPending();
  if ((foo) != 0)
    {
    renWin.SetAbortRender((int)1);
    }
  }
}

        /// <summary>
        /// Returns the variable in the index [i] of the System.Array [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="i"></param>   
        public static Object lindex(System.Array arr, int i)
        {
            return arr.GetValue(i);
        }

        /// <summary>
        /// Returns the variable in the index [index] of the array [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="index"></param>   
        public static double lindex(IntPtr arr, int index)
        {
            double[] destination = new double[index + 1];
            System.Runtime.InteropServices.Marshal.Copy(arr, destination, 0, index + 1);
            return destination[index];
        }

        /// <summary>
        /// Returns the variable in the index [index] of the vtkLookupTable [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="index"></param>
        public static long lindex(vtkLookupTable arr, double index)
        {
            return (int)(arr.GetIndex(index));
        }

        /// <summary>
        /// Returns the substring ([index], [index]+1) in the string [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="index"></param>
        public static int lindex(String arr, int index)
        {
           string[] str = arr.Split(new char[]{' '});      
           return System.Int32.Parse(str[index]);
        }

        /// <summary>
        /// Returns the index [index] in the int array [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="index"></param>
        public static int lindex(int[] arr, int index)
        {
          return arr[index];
        }

        /// <summary>
        /// Returns the index [index] in the float array [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="index"></param>
        public static float lindex(float[] arr, int index)
        {
          return arr[index];
        }

        /// <summary>
        /// Returns the index [index] in the double array [arr]
        /// </summary>
        /// <param name="arr"></param>          
        /// <param name="index"></param>
        public static double lindex(double[] arr, int index)
        {
          return arr[index];
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
        public static vtkCamera Getcamera()
        {
            return camera;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcamera(vtkCamera toSet)
        {
            camera = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkGaussianCubeReader Getreader()
        {
            return reader;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setreader(vtkGaussianCubeReader toSet)
        {
            reader = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double[] Getrange()
        {
            return range;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setrange(double[] toSet)
        {
            range = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double Getmin()
        {
            return min;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setmin(double toSet)
        {
            min = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double Getmax()
        {
            return max;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setmax(double toSet)
        {
            max = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageShiftScale GetreaderSS()
        {
            return readerSS;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetreaderSS(vtkImageShiftScale toSet)
        {
            readerSS = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkOutlineFilter Getbounds()
        {
            return bounds;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbounds(vtkOutlineFilter toSet)
        {
            bounds = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetboundsMapper()
        {
            return boundsMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetboundsMapper(vtkPolyDataMapper toSet)
        {
            boundsMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetboundsActor()
        {
            return boundsActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetboundsActor(vtkActor toSet)
        {
            boundsActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkContourFilter Getcontour()
        {
            return contour;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcontour(vtkContourFilter toSet)
        {
            contour = toSet;
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
        public static vtkActor GetcontourActor()
        {
            return contourActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetcontourActor(vtkActor toSet)
        {
            contourActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPiecewiseFunction GetopacityTransferFunction()
        {
            return opacityTransferFunction;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetopacityTransferFunction(vtkPiecewiseFunction toSet)
        {
            opacityTransferFunction = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkColorTransferFunction GetcolorTransferFunction()
        {
            return colorTransferFunction;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetcolorTransferFunction(vtkColorTransferFunction toSet)
        {
            colorTransferFunction = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkVolumeProperty GetvolumeProperty()
        {
            return volumeProperty;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetvolumeProperty(vtkVolumeProperty toSet)
        {
            volumeProperty = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkVolumeRayCastCompositeFunction GetcompositeFunction()
        {
            return compositeFunction;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetcompositeFunction(vtkVolumeRayCastCompositeFunction toSet)
        {
            compositeFunction = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkVolumeRayCastMapper GetvolumeMapper()
        {
            return volumeMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetvolumeMapper(vtkVolumeRayCastMapper toSet)
        {
            volumeMapper = toSet;
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
        public static vtkSphereSource GetSphere()
        {
            return Sphere;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetSphere(vtkSphereSource toSet)
        {
            Sphere = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkGlyph3D GetGlyph()
        {
            return Glyph;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetGlyph(vtkGlyph3D toSet)
        {
            Glyph = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetAtomsMapper()
        {
            return AtomsMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetAtomsMapper(vtkPolyDataMapper toSet)
        {
            AtomsMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetAtoms()
        {
            return Atoms;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetAtoms(vtkActor toSet)
        {
            Atoms = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTubeFilter GetTube()
        {
            return Tube;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetTube(vtkTubeFilter toSet)
        {
            Tube = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetBondsMapper()
        {
            return BondsMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetBondsMapper(vtkPolyDataMapper toSet)
        {
            BondsMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetBonds()
        {
            return Bonds;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetBonds(vtkActor toSet)
        {
            Bonds = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int Getfoo()
        {
            return foo;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setfoo(int toSet)
        {
            foo = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(camera!= null){camera.Dispose();}
  	if(reader!= null){reader.Dispose();}
  	if(readerSS!= null){readerSS.Dispose();}
  	if(bounds!= null){bounds.Dispose();}
  	if(boundsMapper!= null){boundsMapper.Dispose();}
  	if(boundsActor!= null){boundsActor.Dispose();}
  	if(contour!= null){contour.Dispose();}
  	if(contourMapper!= null){contourMapper.Dispose();}
  	if(contourActor!= null){contourActor.Dispose();}
  	if(opacityTransferFunction!= null){opacityTransferFunction.Dispose();}
  	if(colorTransferFunction!= null){colorTransferFunction.Dispose();}
  	if(volumeProperty!= null){volumeProperty.Dispose();}
  	if(compositeFunction!= null){compositeFunction.Dispose();}
  	if(volumeMapper!= null){volumeMapper.Dispose();}
  	if(volume!= null){volume.Dispose();}
  	if(Sphere!= null){Sphere.Dispose();}
  	if(Glyph!= null){Glyph.Dispose();}
  	if(AtomsMapper!= null){AtomsMapper.Dispose();}
  	if(Atoms!= null){Atoms.Dispose();}
  	if(Tube!= null){Tube.Dispose();}
  	if(BondsMapper!= null){BondsMapper.Dispose();}
  	if(Bonds!= null){Bonds.Dispose();}
  }

}
//--- end of script --//

