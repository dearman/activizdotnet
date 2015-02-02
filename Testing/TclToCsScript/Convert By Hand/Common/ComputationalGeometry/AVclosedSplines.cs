using Kitware.VTK;
using System;
// input file is C:\VTK\Filtering\Testing\Tcl\closedSplines.tcl
// output file is AVclosedSplines.cs
/// <summary>
/// The testing class derived from AVclosedSplines
/// </summary>
public class AVclosedSplinesClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVclosedSplines(String [] argv)
  {
  //Prefix Content is: ""
  
  // get the interactor ui[]
  // Now create the RenderWindow, Renderer and Interactor[]
  //[]
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  math = new vtkMath();
  numberOfInputPoints = 30;
  aKSplineX = new vtkKochanekSpline();
  aKSplineX.ClosedOn();
  aKSplineY = new vtkKochanekSpline();
  aKSplineY.ClosedOn();
  aKSplineZ = new vtkKochanekSpline();
  aKSplineZ.ClosedOn();
  aCSplineX = new vtkCardinalSpline();
  aCSplineX.ClosedOn();
  aCSplineY = new vtkCardinalSpline();
  aCSplineY.ClosedOn();
  aCSplineZ = new vtkCardinalSpline();
  aCSplineZ.ClosedOn();
  // add some points[]
  inputPoints = new vtkPoints();
  x = -1.0;
  y = -1.0;
  z = 0.0;
  aKSplineX.AddPoint((double)0,(double)x);
  aKSplineY.AddPoint((double)0,(double)y);
  aKSplineZ.AddPoint((double)0,(double)z);
  aCSplineX.AddPoint((double)0,(double)x);
  aCSplineY.AddPoint((double)0,(double)y);
  aCSplineZ.AddPoint((double)0,(double)z);
  inputPoints.InsertPoint((int)0,(double)x,(double)y,(double)z);
  x = 1.0;
  y = -1.0;
  z = 0.0;
  aKSplineX.AddPoint((double)1,(double)x);
  aKSplineY.AddPoint((double)1,(double)y);
  aKSplineZ.AddPoint((double)1,(double)z);
  aCSplineX.AddPoint((double)1,(double)x);
  aCSplineY.AddPoint((double)1,(double)y);
  aCSplineZ.AddPoint((double)1,(double)z);
  inputPoints.InsertPoint((int)1,(double)x,(double)y,(double)z);
  x = 1.0;
  y = 1.0;
  z = 0.0;
  aKSplineX.AddPoint((double)2,(double)x);
  aKSplineY.AddPoint((double)2,(double)y);
  aKSplineZ.AddPoint((double)2,(double)z);
  aCSplineX.AddPoint((double)2,(double)x);
  aCSplineY.AddPoint((double)2,(double)y);
  aCSplineZ.AddPoint((double)2,(double)z);
  inputPoints.InsertPoint((int)2,(double)x,(double)y,(double)z);
  x = -1.0;
  y = 1.0;
  z = 0.0;
  aKSplineX.AddPoint((double)3,(double)x);
  aKSplineY.AddPoint((double)3,(double)y);
  aKSplineZ.AddPoint((double)3,(double)z);
  aCSplineX.AddPoint((double)3,(double)x);
  aCSplineY.AddPoint((double)3,(double)y);
  aCSplineZ.AddPoint((double)3,(double)z);
  inputPoints.InsertPoint((int)3,(double)x,(double)y,(double)z);
  inputData = new vtkPolyData();
  inputData.SetPoints((vtkPoints)inputPoints);
  balls = new vtkSphereSource();
  balls.SetRadius((double).04);
  balls.SetPhiResolution((int)10);
  balls.SetThetaResolution((int)10);
  balls.Update();
  glyphPoints = new vtkGlyph3D();
  glyphPoints.SetInputData((vtkDataObject)inputData);
  glyphPoints.SetSourceData((vtkPolyData)balls.GetOutput());
  glyphMapper = vtkPolyDataMapper.New();
  glyphMapper.SetInputConnection((vtkAlgorithmOutput)glyphPoints.GetOutputPort());
  glyph = new vtkActor();
  glyph.SetMapper((vtkMapper)glyphMapper);
  glyph.GetProperty().SetDiffuseColor((double) 1.0000, 0.3882, 0.2784 );
  glyph.GetProperty().SetSpecular((double).3);
  glyph.GetProperty().SetSpecularPower((double)30);
  ren1.AddActor((vtkProp)glyph);
  Kpoints = new vtkPoints();
  Cpoints = new vtkPoints();
  profileKData = new vtkPolyData();
  profileCData = new vtkPolyData();
  numberOfInputPoints = 5;
  numberOfOutputPoints = 100;
  offset = 1.0;
  //method moved
  fit();
  lines = new vtkCellArray();
  lines.InsertNextCell((int)numberOfOutputPoints);
  i = 0;
  while((i) < numberOfOutputPoints)
    {
      lines.InsertCellPoint((int)i);
      i = i + 1;
    }

  profileKData.SetPoints((vtkPoints)Kpoints);
  profileKData.SetLines((vtkCellArray)lines);
  profileCData.SetPoints((vtkPoints)Cpoints);
  profileCData.SetLines((vtkCellArray)lines);
  profileKTubes = new vtkTubeFilter();
  profileKTubes.SetNumberOfSides((int)8);
  profileKTubes.SetInputData((vtkDataObject)profileKData);
  profileKTubes.SetRadius((double).01);
  profileKMapper = vtkPolyDataMapper.New();
  profileKMapper.SetInputConnection((vtkAlgorithmOutput)profileKTubes.GetOutputPort());
  profileK = new vtkActor();
  profileK.SetMapper((vtkMapper)profileKMapper);
  profileK.GetProperty().SetDiffuseColor((double) 0.8900, 0.8100, 0.3400 );
  profileK.GetProperty().SetSpecular((double).3);
  profileK.GetProperty().SetSpecularPower((double)30);
  ren1.AddActor((vtkProp)profileK);
  profileCTubes = new vtkTubeFilter();
  profileCTubes.SetNumberOfSides((int)8);
  profileCTubes.SetInputData((vtkDataObject)profileCData);
  profileCTubes.SetRadius((double).01);
  profileCMapper = vtkPolyDataMapper.New();
  profileCMapper.SetInputConnection((vtkAlgorithmOutput)profileCTubes.GetOutputPort());
  profileC = new vtkActor();
  profileC.SetMapper((vtkMapper)profileCMapper);
  profileC.GetProperty().SetDiffuseColor((double) 0.2000, 0.6300, 0.7900 );
  profileC.GetProperty().SetSpecular((double).3);
  profileC.GetProperty().SetSpecularPower((double)30);
  ren1.AddActor((vtkProp)profileC);
  ren1.ResetCamera();
  ren1.GetActiveCamera().Dolly((double)1.5);
  ren1.ResetCameraClippingRange();
  renWin.SetSize((int)300,(int)300);
  // render the image[]
  //[]
  iren.Initialize();
  // prevent the tk window from showing up then start the event loop[]
  //method moved
  //method moved
  //method moved
  //method moved
  //method moved
  //method moved
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkMath math;
static int numberOfInputPoints;
static vtkKochanekSpline aKSplineX;
static vtkKochanekSpline aKSplineY;
static vtkKochanekSpline aKSplineZ;
static vtkCardinalSpline aCSplineX;
static vtkCardinalSpline aCSplineY;
static vtkCardinalSpline aCSplineZ;
static vtkPoints inputPoints;
static double x;
static double y;
static double z;
static vtkPolyData inputData;
static vtkSphereSource balls;
static vtkGlyph3D glyphPoints;
static vtkPolyDataMapper glyphMapper;
static vtkActor glyph;
static vtkPoints Kpoints;
static vtkPoints Cpoints;
static vtkPolyData profileKData;
static vtkPolyData profileCData;
static int numberOfOutputPoints;
static double offset;
static int i;
static double t;
static vtkCellArray lines;
static vtkTubeFilter profileKTubes;
static vtkPolyDataMapper profileKMapper;
static vtkActor profileK;
static vtkTubeFilter profileCTubes;
static vtkPolyDataMapper profileCMapper;
static vtkActor profileC;
static double bias;
static double tension;
static double Continuity;


  /// <summary>
  ///A process translated from the tcl scripts
  /// </summary>
  public static void fit ( )
    {
      //Global Variable Declaration Skipped
      Kpoints.Reset();
      Cpoints.Reset();
      i = 0;
      while((i) < numberOfOutputPoints)
        {
          t = (numberOfInputPoints-offset)/(numberOfOutputPoints-1)*i;
          Kpoints.InsertPoint((int)i,(double)aKSplineX.Evaluate((double)t),(double)aKSplineY.Evaluate((double)t),(double)aKSplineZ.Evaluate((double)t));
          Cpoints.InsertPoint((int)i,(double)aCSplineX.Evaluate((double)t),(double)aCSplineY.Evaluate((double)t),(double)aCSplineZ.Evaluate((double)t));
          i = i + 1;
        }

      profileKData.Modified();
      profileCData.Modified();
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
        public static vtkMath Getmath()
        {
            return math;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setmath(vtkMath toSet)
        {
            math = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int GetnumberOfInputPoints()
        {
            return numberOfInputPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetnumberOfInputPoints(int toSet)
        {
            numberOfInputPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkKochanekSpline GetaKSplineX()
        {
            return aKSplineX;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaKSplineX(vtkKochanekSpline toSet)
        {
            aKSplineX = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkKochanekSpline GetaKSplineY()
        {
            return aKSplineY;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaKSplineY(vtkKochanekSpline toSet)
        {
            aKSplineY = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkKochanekSpline GetaKSplineZ()
        {
            return aKSplineZ;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaKSplineZ(vtkKochanekSpline toSet)
        {
            aKSplineZ = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCardinalSpline GetaCSplineX()
        {
            return aCSplineX;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaCSplineX(vtkCardinalSpline toSet)
        {
            aCSplineX = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCardinalSpline GetaCSplineY()
        {
            return aCSplineY;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaCSplineY(vtkCardinalSpline toSet)
        {
            aCSplineY = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCardinalSpline GetaCSplineZ()
        {
            return aCSplineZ;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaCSplineZ(vtkCardinalSpline toSet)
        {
            aCSplineZ = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GetinputPoints()
        {
            return inputPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetinputPoints(vtkPoints toSet)
        {
            inputPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double Getx()
        {
            return x;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setx(double toSet)
        {
            x = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double Gety()
        {
            return y;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sety(double toSet)
        {
            y = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double Getz()
        {
            return z;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setz(double toSet)
        {
            z = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyData GetinputData()
        {
            return inputData;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetinputData(vtkPolyData toSet)
        {
            inputData = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkSphereSource Getballs()
        {
            return balls;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setballs(vtkSphereSource toSet)
        {
            balls = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkGlyph3D GetglyphPoints()
        {
            return glyphPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetglyphPoints(vtkGlyph3D toSet)
        {
            glyphPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetglyphMapper()
        {
            return glyphMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetglyphMapper(vtkPolyDataMapper toSet)
        {
            glyphMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getglyph()
        {
            return glyph;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setglyph(vtkActor toSet)
        {
            glyph = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GetKpoints()
        {
            return Kpoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetKpoints(vtkPoints toSet)
        {
            Kpoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GetCpoints()
        {
            return Cpoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetCpoints(vtkPoints toSet)
        {
            Cpoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyData GetprofileKData()
        {
            return profileKData;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetprofileKData(vtkPolyData toSet)
        {
            profileKData = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyData GetprofileCData()
        {
            return profileCData;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetprofileCData(vtkPolyData toSet)
        {
            profileCData = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int GetnumberOfOutputPoints()
        {
            return numberOfOutputPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetnumberOfOutputPoints(int toSet)
        {
            numberOfOutputPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double Getoffset()
        {
            return offset;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setoffset(double toSet)
        {
            offset = toSet;
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
        public static double Gett()
        {
            return t;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sett(double toSet)
        {
            t = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCellArray Getlines()
        {
            return lines;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setlines(vtkCellArray toSet)
        {
            lines = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTubeFilter GetprofileKTubes()
        {
            return profileKTubes;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetprofileKTubes(vtkTubeFilter toSet)
        {
            profileKTubes = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetprofileKMapper()
        {
            return profileKMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetprofileKMapper(vtkPolyDataMapper toSet)
        {
            profileKMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetprofileK()
        {
            return profileK;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetprofileK(vtkActor toSet)
        {
            profileK = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTubeFilter GetprofileCTubes()
        {
            return profileCTubes;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetprofileCTubes(vtkTubeFilter toSet)
        {
            profileCTubes = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetprofileCMapper()
        {
            return profileCMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetprofileCMapper(vtkPolyDataMapper toSet)
        {
            profileCMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetprofileC()
        {
            return profileC;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetprofileC(vtkActor toSet)
        {
            profileC = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double Getbias()
        {
            return bias;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbias(double toSet)
        {
            bias = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double Gettension()
        {
            return tension;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settension(double toSet)
        {
            tension = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static double GetContinuity()
        {
            return Continuity;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetContinuity(double toSet)
        {
            Continuity = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(math!= null){math.Dispose();}
  	if(aKSplineX!= null){aKSplineX.Dispose();}
  	if(aKSplineY!= null){aKSplineY.Dispose();}
  	if(aKSplineZ!= null){aKSplineZ.Dispose();}
  	if(aCSplineX!= null){aCSplineX.Dispose();}
  	if(aCSplineY!= null){aCSplineY.Dispose();}
  	if(aCSplineZ!= null){aCSplineZ.Dispose();}
  	if(inputPoints!= null){inputPoints.Dispose();}
  	if(inputData!= null){inputData.Dispose();}
  	if(balls!= null){balls.Dispose();}
  	if(glyphPoints!= null){glyphPoints.Dispose();}
  	if(glyphMapper!= null){glyphMapper.Dispose();}
  	if(glyph!= null){glyph.Dispose();}
  	if(Kpoints!= null){Kpoints.Dispose();}
  	if(Cpoints!= null){Cpoints.Dispose();}
  	if(profileKData!= null){profileKData.Dispose();}
  	if(profileCData!= null){profileCData.Dispose();}
  	if(lines!= null){lines.Dispose();}
  	if(profileKTubes!= null){profileKTubes.Dispose();}
  	if(profileKMapper!= null){profileKMapper.Dispose();}
  	if(profileK!= null){profileK.Dispose();}
  	if(profileCTubes!= null){profileCTubes.Dispose();}
  	if(profileCMapper!= null){profileCMapper.Dispose();}
  	if(profileC!= null){profileC.Dispose();}
  }

}
//--- end of script --//

