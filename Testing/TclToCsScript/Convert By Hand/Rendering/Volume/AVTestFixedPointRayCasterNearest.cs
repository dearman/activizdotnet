using Kitware.VTK;
using System;
// input file is C:\VTK\VolumeRendering\Testing\Tcl\TestFixedPointRayCasterNearest.tcl
// output file is AVTestFixedPointRayCasterNearest.cs
/// <summary>
/// The testing class derived from AVTestFixedPointRayCasterNearest
/// </summary>
public class AVTestFixedPointRayCasterNearestClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVTestFixedPointRayCasterNearest(String [] argv)
  {
  //Prefix Content is: ""
  
  // Create a gaussian[]
  gs = new vtkImageGaussianSource();
  gs.SetWholeExtent((int)0,(int)30,(int)0,(int)30,(int)0,(int)30);
  gs.SetMaximum((double)255.0);
  gs.SetStandardDeviation((double)5);
  gs.SetCenter((double)15,(double)15,(double)15);
  // threshold to leave a gap that should show up for[]
  // gradient opacity[]
  t = new vtkImageThreshold();
  t.SetInputConnection((vtkAlgorithmOutput)gs.GetOutputPort());
  t.ReplaceInOn();
  t.SetInValue((double)0);
  t.ThresholdBetween((double)150,(double)200);
  // Use a shift scale to convert to unsigned char[]
  ss = new vtkImageShiftScale();
  ss.SetInputConnection((vtkAlgorithmOutput)t.GetOutputPort());
  ss.SetOutputScalarTypeToUnsignedChar();
  // grid will be used for two component dependent[]
  grid0 = new vtkImageGridSource();
  grid0.SetDataScalarTypeToUnsignedChar();
  grid0.SetGridSpacing((int)10,(int)10,(int)10);
  grid0.SetLineValue((double)200);
  grid0.SetFillValue((double)10);
  grid0.SetDataExtent((int)0,(int)30,(int)0,(int)30,(int)0,(int)30);
  // use dilation to thicken the grid[]
  d = new vtkImageContinuousDilate3D();
  d.SetInputConnection((vtkAlgorithmOutput)grid0.GetOutputPort());
  d.SetKernelSize((int)3,(int)3,(int)3);
  // Now make a two component dependent[]
  iac = new vtkImageAppendComponents();
  iac.AddInputConnection(d.GetOutputPort());
  iac.AddInputConnection(ss.GetOutputPort());
  // Some more gaussians for the four component indepent case[]
  gs1 = new vtkImageGaussianSource();
  gs1.SetWholeExtent((int)0,(int)30,(int)0,(int)30,(int)0,(int)30);
  gs1.SetMaximum((double)255.0);
  gs1.SetStandardDeviation((double)4);
  gs1.SetCenter((double)5,(double)5,(double)5);
  t1 = new vtkImageThreshold();
  t1.SetInputConnection((vtkAlgorithmOutput)gs1.GetOutputPort());
  t1.ReplaceInOn();
  t1.SetInValue((double)0);
  t1.ThresholdBetween((double)150,(double)256);
  gs2 = new vtkImageGaussianSource();
  gs2.SetWholeExtent((int)0,(int)30,(int)0,(int)30,(int)0,(int)30);
  gs2.SetMaximum((double)255.0);
  gs2.SetStandardDeviation((double)4);
  gs2.SetCenter((double)12,(double)12,(double)12);
  gs3 = new vtkImageGaussianSource();
  gs3.SetWholeExtent((int)0,(int)30,(int)0,(int)30,(int)0,(int)30);
  gs3.SetMaximum((double)255.0);
  gs3.SetStandardDeviation((double)4);
  gs3.SetCenter((double)19,(double)19,(double)19);
  t3 = new vtkImageThreshold();
  t3.SetInputConnection((vtkAlgorithmOutput)gs3.GetOutputPort());
  t3.ReplaceInOn();
  t3.SetInValue((double)0);
  t3.ThresholdBetween((double)150,(double)256);
  gs4 = new vtkImageGaussianSource();
  gs4.SetWholeExtent((int)0,(int)30,(int)0,(int)30,(int)0,(int)30);
  gs4.SetMaximum((double)255.0);
  gs4.SetStandardDeviation((double)4);
  gs4.SetCenter((double)26,(double)26,(double)26);
  //tk window skipped..
  iac1 = new vtkImageAppendComponents();
  iac1.AddInputConnection(t1.GetOutputPort());
  iac1.AddInputConnection(gs2.GetOutputPort());
  iac2 = new vtkImageAppendComponents();
  iac2.AddInputConnection(iac1.GetOutputPort());
  iac2.AddInputConnection(t3.GetOutputPort());
  iac3 = new vtkImageAppendComponents();
  iac3.AddInputConnection(iac2.GetOutputPort());
  iac3.AddInputConnection(gs4.GetOutputPort());
  // create the four component dependend - []
  // use lines in x, y, z for colors[]
  gridR = new vtkImageGridSource();
  gridR.SetDataScalarTypeToUnsignedChar();
  gridR.SetGridSpacing((int)10,(int)100,(int)100);
  gridR.SetLineValue((double)250);
  gridR.SetFillValue((double)100);
  gridR.SetDataExtent((int)0,(int)30,(int)0,(int)30,(int)0,(int)30);
  dR = new vtkImageContinuousDilate3D();
  dR.SetInputConnection((vtkAlgorithmOutput)gridR.GetOutputPort());
  dR.SetKernelSize((int)2,(int)2,(int)2);
  gridG = new vtkImageGridSource();
  gridG.SetDataScalarTypeToUnsignedChar();
  gridG.SetGridSpacing((int)100,(int)10,(int)100);
  gridG.SetLineValue((double)250);
  gridG.SetFillValue((double)100);
  gridG.SetDataExtent((int)0,(int)30,(int)0,(int)30,(int)0,(int)30);
  dG = new vtkImageContinuousDilate3D();
  dG.SetInputConnection((vtkAlgorithmOutput)gridG.GetOutputPort());
  dG.SetKernelSize((int)2,(int)2,(int)2);
  gridB = new vtkImageGridSource();
  gridB.SetDataScalarTypeToUnsignedChar();
  gridB.SetGridSpacing((int)100,(int)100,(int)10);
  gridB.SetLineValue((double)0);
  gridB.SetFillValue((double)250);
  gridB.SetDataExtent((int)0,(int)30,(int)0,(int)30,(int)0,(int)30);
  dB = new vtkImageContinuousDilate3D();
  dB.SetInputConnection((vtkAlgorithmOutput)gridB.GetOutputPort());
  dB.SetKernelSize((int)2,(int)2,(int)2);
  // need some appending[]
  iacRG = new vtkImageAppendComponents();
  iacRG.AddInputConnection(dR.GetOutputPort());
  iacRG.AddInputConnection(dG.GetOutputPort());
  iacRGB = new vtkImageAppendComponents();
  iacRGB.AddInputConnection(iacRG.GetOutputPort());
  iacRGB.AddInputConnection(dB.GetOutputPort());
  iacRGBA = new vtkImageAppendComponents();
  iacRGBA.AddInputConnection(iacRGB.GetOutputPort());
  iacRGBA.AddInputConnection(ss.GetOutputPort());
  // We need a bunch of opacity functions[]
  // this one is a simple ramp to .2[]
  rampPoint2 = new vtkPiecewiseFunction();
  rampPoint2.AddPoint((double)0,(double)0.0);
  rampPoint2.AddPoint((double)255,(double)0.2);
  // this one is a simple ramp to 1[]
  ramp1 = new vtkPiecewiseFunction();
  ramp1.AddPoint((double)0,(double)0.0);
  ramp1.AddPoint((double)255,(double)1.0);
  // this one shows a sharp surface[]
  surface = new vtkPiecewiseFunction();
  surface.AddPoint((double)0,(double)0.0);
  surface.AddPoint((double)10,(double)0.0);
  surface.AddPoint((double)50,(double)1.0);
  surface.AddPoint((double)255,(double)1.0);
  // this one is constant 1[]
  constant1 = new vtkPiecewiseFunction();
  constant1.AddPoint((double)0,(double)1.0);
  constant1.AddPoint((double)255,(double)1.0);
  // this one is used for gradient opacity[]
  gop = new vtkPiecewiseFunction();
  gop.AddPoint((double)0,(double)0.0);
  gop.AddPoint((double)20,(double)0.0);
  gop.AddPoint((double)60,(double)1.0);
  gop.AddPoint((double)255,(double)1.0);
  // We need a bunch of color functions[]
  // This one is a simple rainbow[]
  rainbow = new vtkColorTransferFunction();
  rainbow.SetColorSpaceToHSV();
  rainbow.HSVWrapOff();
  rainbow.AddHSVPoint((double)0,(double)0.1,(double)1.0,(double)1.0);
  rainbow.AddHSVPoint((double)255,(double)0.9,(double)1.0,(double)1.0);
  // this is constant red[]
  red = new vtkColorTransferFunction();
  red.AddRGBPoint((double)0,(double)1,(double)0,(double)0);
  red.AddRGBPoint((double)255,(double)1,(double)0,(double)0);
  // this is constant green[]
  green = new vtkColorTransferFunction();
  green.AddRGBPoint((double)0,(double)0,(double)1,(double)0);
  green.AddRGBPoint((double)255,(double)0,(double)1,(double)0);
  // this is constant blue[]
  blue = new vtkColorTransferFunction();
  blue.AddRGBPoint((double)0,(double)0,(double)0,(double)1);
  blue.AddRGBPoint((double)255,(double)0,(double)0,(double)1);
  // this is constant yellow[]
  yellow = new vtkColorTransferFunction();
  yellow.AddRGBPoint((double)0,(double)1,(double)1,(double)0);
  yellow.AddRGBPoint((double)255,(double)1,(double)1,(double)0);
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  renWin.SetSize((int)500,(int)500);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  ren1.GetCullers().InitTraversal();
  culler = (vtkFrustumCoverageCuller)ren1.GetCullers().GetNextItem();
  culler.SetSortingStyleToBackToFront();
  // We need 25 mapper / actor pairs which we will render[]
  // in a grid. Going down we will vary the input data[]
  // with the top row unsigned char, then float, then[]
  // two dependent components, then four dependent components[]
  // then four independent components. Going across we[]
  // will vary the rendering method with MIP, Composite,[]
  // Composite Shade, Composite GO, and Composite GO Shade.[]
  j = 0;
  while((j) < 5)
    {
      i = 0;
      while((i) < 5)
        {
          volumeProperty[i,j] = new vtkVolumeProperty();
          volumeMapper[i,j] = new vtkFixedPointVolumeRayCastMapper();
          volumeMapper[i,j].SetSampleDistance((float)0.25);
          volume[i,j] = new vtkVolume();
          volume[i,j].SetMapper((vtkAbstractVolumeMapper)volumeMapper[i,j]);
          volume[i,j].SetProperty((vtkVolumeProperty)volumeProperty[i,j]);
          volume[i,j].AddPosition((double)i*30,(double)j*30,(double)0);
          ren1.AddVolume((vtkProp)volume[i,j]);
          i = i + 1;
        }

      j = j + 1;
    }

  i = 0;
  while((i) < 5)
    {
      volumeMapper[0,i].SetInputConnection(t.GetOutputPort());
      volumeMapper[1,i].SetInputConnection(ss.GetOutputPort());
      volumeMapper[2,i].SetInputConnection(iac.GetOutputPort());
      volumeMapper[3,i].SetInputConnection(iac3.GetOutputPort());
      volumeMapper[4,i].SetInputConnection(iacRGBA.GetOutputPort());
      volumeMapper[i,0].SetBlendModeToMaximumIntensity();
      volumeMapper[i,1].SetBlendModeToComposite();
      volumeMapper[i,2].SetBlendModeToComposite();
      volumeMapper[i,3].SetBlendModeToComposite();
      volumeMapper[i,4].SetBlendModeToComposite();
      volumeProperty[0,i].IndependentComponentsOn();
      volumeProperty[1,i].IndependentComponentsOn();
      volumeProperty[2,i].IndependentComponentsOff();
      volumeProperty[3,i].IndependentComponentsOn();
      volumeProperty[4,i].IndependentComponentsOff();
      volumeProperty[0,i].SetColor(rainbow);
      volumeProperty[0,i].SetScalarOpacity(rampPoint2);
      volumeProperty[0,i].SetGradientOpacity(constant1);
      volumeProperty[1,i].SetColor(rainbow);
      volumeProperty[1,i].SetScalarOpacity(rampPoint2);
      volumeProperty[1,i].SetGradientOpacity(constant1);
      volumeProperty[2,i].SetColor(rainbow);
      volumeProperty[2,i].SetScalarOpacity(rampPoint2);
      volumeProperty[2,i].SetGradientOpacity(constant1);
      volumeProperty[3,i].SetColor(0, red);
      volumeProperty[3,i].SetColor(1, green);
      volumeProperty[3,i].SetColor(2, blue );
      volumeProperty[3,i].SetColor(3, yellow);
      volumeProperty[3,i].SetScalarOpacity(0,rampPoint2);
      volumeProperty[3,i].SetScalarOpacity(1,rampPoint2);
      volumeProperty[3,i].SetScalarOpacity(2,rampPoint2);
      volumeProperty[3,i].SetScalarOpacity(3,rampPoint2);
      volumeProperty[3,i].SetGradientOpacity(0,constant1);
      volumeProperty[3,i].SetGradientOpacity(1,constant1);
      volumeProperty[3,i].SetGradientOpacity(2,constant1);
      volumeProperty[3,i].SetGradientOpacity(3,constant1);
      volumeProperty[3,i].SetComponentWeight(0,1);
      volumeProperty[3,i].SetComponentWeight(1,1);
      volumeProperty[3,i].SetComponentWeight(2,1);
      volumeProperty[3,i].SetComponentWeight(3,1);
      volumeProperty[4,i].SetColor(rainbow);
      volumeProperty[4,i].SetScalarOpacity(rampPoint2);
      volumeProperty[4,i].SetGradientOpacity(constant1);
      volumeProperty[i,2].ShadeOn();
      volumeProperty[i,4].ShadeOn((int)0);
      volumeProperty[i,4].ShadeOn((int)1);
      volumeProperty[i,4].ShadeOn((int)2);
      volumeProperty[i,4].ShadeOn((int)3);
      i = i + 1;
    }

  volumeProperty[0,0].SetScalarOpacity((vtkPiecewiseFunction)ramp1);
  volumeProperty[1,0].SetScalarOpacity((vtkPiecewiseFunction)ramp1);
  volumeProperty[2,0].SetScalarOpacity((vtkPiecewiseFunction)ramp1);
  volumeProperty[3,0].SetScalarOpacity((int)0,(vtkPiecewiseFunction)surface);
  volumeProperty[3,0].SetScalarOpacity((int)1,(vtkPiecewiseFunction)surface);
  volumeProperty[3,0].SetScalarOpacity((int)2,(vtkPiecewiseFunction)surface);
  volumeProperty[3,0].SetScalarOpacity((int)3,(vtkPiecewiseFunction)surface);
  volumeProperty[4,0].SetScalarOpacity((vtkPiecewiseFunction)ramp1);
  volumeProperty[0,2].SetScalarOpacity((vtkPiecewiseFunction)surface);
  volumeProperty[1,2].SetScalarOpacity((vtkPiecewiseFunction)surface);
  volumeProperty[2,2].SetScalarOpacity((vtkPiecewiseFunction)surface);
  volumeProperty[3,2].SetScalarOpacity((int)0,(vtkPiecewiseFunction)surface);
  volumeProperty[3,2].SetScalarOpacity((int)1,(vtkPiecewiseFunction)surface);
  volumeProperty[3,2].SetScalarOpacity((int)2,(vtkPiecewiseFunction)surface);
  volumeProperty[3,2].SetScalarOpacity((int)3,(vtkPiecewiseFunction)surface);
  volumeProperty[4,2].SetScalarOpacity((vtkPiecewiseFunction)surface);
  volumeProperty[0,4].SetScalarOpacity((vtkPiecewiseFunction)surface);
  volumeProperty[1,4].SetScalarOpacity((vtkPiecewiseFunction)surface);
  volumeProperty[2,4].SetScalarOpacity((vtkPiecewiseFunction)surface);
  volumeProperty[3,4].SetScalarOpacity((int)0,(vtkPiecewiseFunction)surface);
  volumeProperty[3,4].SetScalarOpacity((int)1,(vtkPiecewiseFunction)surface);
  volumeProperty[3,4].SetScalarOpacity((int)2,(vtkPiecewiseFunction)surface);
  volumeProperty[3,4].SetScalarOpacity((int)3,(vtkPiecewiseFunction)surface);
  volumeProperty[4,4].SetScalarOpacity((vtkPiecewiseFunction)surface);
  volumeProperty[0,3].SetGradientOpacity((vtkPiecewiseFunction)gop);
  volumeProperty[1,3].SetGradientOpacity((vtkPiecewiseFunction)gop);
  volumeProperty[2,3].SetGradientOpacity((vtkPiecewiseFunction)gop);
  volumeProperty[3,3].SetGradientOpacity((int)0,(vtkPiecewiseFunction)gop);
  volumeProperty[3,3].SetGradientOpacity((int)2,(vtkPiecewiseFunction)gop);
  volumeProperty[4,3].SetGradientOpacity((vtkPiecewiseFunction)gop);
  volumeProperty[3,3].SetScalarOpacity((int)0,(vtkPiecewiseFunction)ramp1);
  volumeProperty[3,3].SetScalarOpacity((int)2,(vtkPiecewiseFunction)ramp1);
  volumeProperty[0,4].SetGradientOpacity((vtkPiecewiseFunction)gop);
  volumeProperty[1,4].SetGradientOpacity((vtkPiecewiseFunction)gop);
  volumeProperty[2,4].SetGradientOpacity((vtkPiecewiseFunction)gop);
  volumeProperty[3,4].SetGradientOpacity((int)0,(vtkPiecewiseFunction)gop);
  volumeProperty[3,4].SetGradientOpacity((int)2,(vtkPiecewiseFunction)gop);
  volumeProperty[4,4].SetGradientOpacity((vtkPiecewiseFunction)gop);
  renWin.Render();
  ren1.GetActiveCamera().Dolly((double)1.3);
  ren1.GetActiveCamera().Azimuth((double)15);
  ren1.GetActiveCamera().Elevation((double)5);
  ren1.ResetCameraClippingRange();
  iren.Initialize();
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkImageGaussianSource gs;
static vtkImageThreshold t;
static vtkImageShiftScale ss;
static vtkImageGridSource grid0;
static vtkImageContinuousDilate3D d;
static vtkImageAppendComponents iac;
static vtkImageGaussianSource gs1;
static vtkImageThreshold t1;
static vtkImageGaussianSource gs2;
static vtkImageGaussianSource gs3;
static vtkImageThreshold t3;
static vtkImageGaussianSource gs4;
static vtkImageAppendComponents iac1;
static vtkImageAppendComponents iac2;
static vtkImageAppendComponents iac3;
static vtkImageGridSource gridR;
static vtkImageContinuousDilate3D dR;
static vtkImageGridSource gridG;
static vtkImageContinuousDilate3D dG;
static vtkImageGridSource gridB;
static vtkImageContinuousDilate3D dB;
static vtkImageAppendComponents iacRG;
static vtkImageAppendComponents iacRGB;
static vtkImageAppendComponents iacRGBA;
static vtkPiecewiseFunction rampPoint2;
static vtkPiecewiseFunction ramp1;
static vtkPiecewiseFunction surface;
static vtkPiecewiseFunction constant1;
static vtkPiecewiseFunction gop;
static vtkColorTransferFunction rainbow;
static vtkColorTransferFunction red;
static vtkColorTransferFunction green;
static vtkColorTransferFunction blue;
static vtkColorTransferFunction yellow;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkFrustumCoverageCuller culler;
static int j;
static int i;
static vtkVolumeProperty[,] volumeProperty = new vtkVolumeProperty[100,100];
static vtkFixedPointVolumeRayCastMapper[,] volumeMapper = new vtkFixedPointVolumeRayCastMapper[100,100];
static vtkVolume[,] volume = new vtkVolume[100,100];


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
        public static vtkImageGaussianSource Getgs()
        {
            return gs;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setgs(vtkImageGaussianSource toSet)
        {
            gs = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageThreshold Gett()
        {
            return t;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sett(vtkImageThreshold toSet)
        {
            t = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageShiftScale Getss()
        {
            return ss;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setss(vtkImageShiftScale toSet)
        {
            ss = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageGridSource Getgrid0()
        {
            return grid0;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setgrid0(vtkImageGridSource toSet)
        {
            grid0 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageContinuousDilate3D Getd()
        {
            return d;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setd(vtkImageContinuousDilate3D toSet)
        {
            d = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageAppendComponents Getiac()
        {
            return iac;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setiac(vtkImageAppendComponents toSet)
        {
            iac = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageGaussianSource Getgs1()
        {
            return gs1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setgs1(vtkImageGaussianSource toSet)
        {
            gs1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageThreshold Gett1()
        {
            return t1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sett1(vtkImageThreshold toSet)
        {
            t1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageGaussianSource Getgs2()
        {
            return gs2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setgs2(vtkImageGaussianSource toSet)
        {
            gs2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageGaussianSource Getgs3()
        {
            return gs3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setgs3(vtkImageGaussianSource toSet)
        {
            gs3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageThreshold Gett3()
        {
            return t3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sett3(vtkImageThreshold toSet)
        {
            t3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageGaussianSource Getgs4()
        {
            return gs4;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setgs4(vtkImageGaussianSource toSet)
        {
            gs4 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageAppendComponents Getiac1()
        {
            return iac1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setiac1(vtkImageAppendComponents toSet)
        {
            iac1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageAppendComponents Getiac2()
        {
            return iac2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setiac2(vtkImageAppendComponents toSet)
        {
            iac2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageAppendComponents Getiac3()
        {
            return iac3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setiac3(vtkImageAppendComponents toSet)
        {
            iac3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageGridSource GetgridR()
        {
            return gridR;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetgridR(vtkImageGridSource toSet)
        {
            gridR = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageContinuousDilate3D GetdR()
        {
            return dR;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetdR(vtkImageContinuousDilate3D toSet)
        {
            dR = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageGridSource GetgridG()
        {
            return gridG;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetgridG(vtkImageGridSource toSet)
        {
            gridG = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageContinuousDilate3D GetdG()
        {
            return dG;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetdG(vtkImageContinuousDilate3D toSet)
        {
            dG = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageGridSource GetgridB()
        {
            return gridB;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetgridB(vtkImageGridSource toSet)
        {
            gridB = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageContinuousDilate3D GetdB()
        {
            return dB;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetdB(vtkImageContinuousDilate3D toSet)
        {
            dB = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageAppendComponents GetiacRG()
        {
            return iacRG;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetiacRG(vtkImageAppendComponents toSet)
        {
            iacRG = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageAppendComponents GetiacRGB()
        {
            return iacRGB;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetiacRGB(vtkImageAppendComponents toSet)
        {
            iacRGB = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageAppendComponents GetiacRGBA()
        {
            return iacRGBA;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetiacRGBA(vtkImageAppendComponents toSet)
        {
            iacRGBA = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPiecewiseFunction GetrampPoint2()
        {
            return rampPoint2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetrampPoint2(vtkPiecewiseFunction toSet)
        {
            rampPoint2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPiecewiseFunction Getramp1()
        {
            return ramp1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setramp1(vtkPiecewiseFunction toSet)
        {
            ramp1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPiecewiseFunction Getsurface()
        {
            return surface;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setsurface(vtkPiecewiseFunction toSet)
        {
            surface = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPiecewiseFunction Getconstant1()
        {
            return constant1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setconstant1(vtkPiecewiseFunction toSet)
        {
            constant1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPiecewiseFunction Getgop()
        {
            return gop;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setgop(vtkPiecewiseFunction toSet)
        {
            gop = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkColorTransferFunction Getrainbow()
        {
            return rainbow;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setrainbow(vtkColorTransferFunction toSet)
        {
            rainbow = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkColorTransferFunction Getred()
        {
            return red;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setred(vtkColorTransferFunction toSet)
        {
            red = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkColorTransferFunction Getgreen()
        {
            return green;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setgreen(vtkColorTransferFunction toSet)
        {
            green = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkColorTransferFunction Getblue()
        {
            return blue;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setblue(vtkColorTransferFunction toSet)
        {
            blue = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkColorTransferFunction Getyellow()
        {
            return yellow;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setyellow(vtkColorTransferFunction toSet)
        {
            yellow = toSet;
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
        public static vtkFrustumCoverageCuller Getculler()
        {
            return culler;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setculler(vtkFrustumCoverageCuller toSet)
        {
            culler = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int Getj()
        {
            return j;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setj(int toSet)
        {
            j = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int Geti()
        {
            return i;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Seti(int toSet)
        {
            i = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkVolumeProperty[,] GetvolumeProperty()
        {
            return volumeProperty;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetvolumeProperty(vtkVolumeProperty[,] toSet)
        {
            volumeProperty = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkFixedPointVolumeRayCastMapper[,] GetvolumeMapper()
        {
            return volumeMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetvolumeMapper(vtkFixedPointVolumeRayCastMapper[,] toSet)
        {
            volumeMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkVolume[,] Getvolume()
        {
            return volume;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setvolume(vtkVolume[,] toSet)
        {
            volume = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(gs!= null){gs.Dispose();}
  	if(t!= null){t.Dispose();}
  	if(ss!= null){ss.Dispose();}
  	if(grid0!= null){grid0.Dispose();}
  	if(d!= null){d.Dispose();}
  	if(iac!= null){iac.Dispose();}
  	if(gs1!= null){gs1.Dispose();}
  	if(t1!= null){t1.Dispose();}
  	if(gs2!= null){gs2.Dispose();}
  	if(gs3!= null){gs3.Dispose();}
  	if(t3!= null){t3.Dispose();}
  	if(gs4!= null){gs4.Dispose();}
  	if(iac1!= null){iac1.Dispose();}
  	if(iac2!= null){iac2.Dispose();}
  	if(iac3!= null){iac3.Dispose();}
  	if(gridR!= null){gridR.Dispose();}
  	if(dR!= null){dR.Dispose();}
  	if(gridG!= null){gridG.Dispose();}
  	if(dG!= null){dG.Dispose();}
  	if(gridB!= null){gridB.Dispose();}
  	if(dB!= null){dB.Dispose();}
  	if(iacRG!= null){iacRG.Dispose();}
  	if(iacRGB!= null){iacRGB.Dispose();}
  	if(iacRGBA!= null){iacRGBA.Dispose();}
  	if(rampPoint2!= null){rampPoint2.Dispose();}
  	if(ramp1!= null){ramp1.Dispose();}
  	if(surface!= null){surface.Dispose();}
  	if(constant1!= null){constant1.Dispose();}
  	if(gop!= null){gop.Dispose();}
  	if(rainbow!= null){rainbow.Dispose();}
  	if(red!= null){red.Dispose();}
  	if(green!= null){green.Dispose();}
  	if(blue!= null){blue.Dispose();}
  	if(yellow!= null){yellow.Dispose();}
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(culler!= null){culler.Dispose();}
  }

}
//--- end of script --//

