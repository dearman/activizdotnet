using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\MatrixToTransform.tcl
// output file is AVMatrixToTransform.cs
/// <summary>
/// The testing class derived from AVMatrixToTransform
/// </summary>
public class AVMatrixToTransformClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVMatrixToTransform(String [] argv)
  {
  //Prefix Content is: ""
  
  // This example demonstrates how to use a matrix in place of a transfrom[]
  // via vtkMatrixToLinearTransform and vtkMatrixToHomogeneousTransform.[]
  // create a rendering window[]
  renWin = vtkRenderWindow.New();
  renWin.SetSize((int)600,(int)300);
  // set up first set of polydata[]
  p1 = new vtkPlaneSource();
  p1.SetOrigin((double)0.5,(double)0.508,(double)-0.5);
  p1.SetPoint1((double)-0.5,(double)0.508,(double)-0.5);
  p1.SetPoint2((double)0.5,(double)0.508,(double)0.5);
  p1.SetXResolution((int)5);
  p1.SetYResolution((int)5);
  p2 = new vtkPlaneSource();
  p2.SetOrigin((double)-0.508,(double)0.5,(double)-0.5);
  p2.SetPoint1((double)-0.508,(double)-0.5,(double)-0.5);
  p2.SetPoint2((double)-0.508,(double)0.5,(double)0.5);
  p2.SetXResolution((int)5);
  p2.SetYResolution((int)5);
  p3 = new vtkPlaneSource();
  p3.SetOrigin((double)-0.5,(double)-0.508,(double)-0.5);
  p3.SetPoint1((double)0.5,(double)-0.508,(double)-0.5);
  p3.SetPoint2((double)-0.5,(double)-0.508,(double)0.5);
  p3.SetXResolution((int)5);
  p3.SetYResolution((int)5);
  p4 = new vtkPlaneSource();
  p4.SetOrigin((double)0.508,(double)-0.5,(double)-0.5);
  p4.SetPoint1((double)0.508,(double)0.5,(double)-0.5);
  p4.SetPoint2((double)0.508,(double)-0.5,(double)0.5);
  p4.SetXResolution((int)5);
  p4.SetYResolution((int)5);
  p5 = new vtkPlaneSource();
  p5.SetOrigin((double)0.5,(double)0.5,(double)-0.508);
  p5.SetPoint1((double)0.5,(double)-0.5,(double)-0.508);
  p5.SetPoint2((double)-0.5,(double)0.5,(double)-0.508);
  p5.SetXResolution((int)5);
  p5.SetYResolution((int)5);
  p6 = new vtkPlaneSource();
  p6.SetOrigin((double)0.5,(double)0.5,(double)0.508);
  p6.SetPoint1((double)-0.5,(double)0.5,(double)0.508);
  p6.SetPoint2((double)0.5,(double)-0.5,(double)0.508);
  p6.SetXResolution((int)5);
  p6.SetYResolution((int)5);
  // append together[]
  ap = new vtkAppendPolyData();
  ap.AddInputConnection(p1.GetOutputPort());
  ap.AddInputConnection(p2.GetOutputPort());
  ap.AddInputConnection(p3.GetOutputPort());
  ap.AddInputConnection(p4.GetOutputPort());
  ap.AddInputConnection(p5.GetOutputPort());
  ap.AddInputConnection(p6.GetOutputPort());
  //--------------------------[]
  // linear transform matrix[]
  t1 = new vtkMatrixToLinearTransform();
  m1 = new vtkMatrix4x4();
  t1.SetInput((vtkMatrix4x4)m1);
  m1.SetElement((int)0,(int)0,(double)1.127631);
  m1.SetElement((int)0,(int)1,(double)0.205212);
  m1.SetElement((int)0,(int)2,(double)-0.355438);
  m1.SetElement((int)1,(int)0,(double)0.000000);
  m1.SetElement((int)1,(int)1,(double)0.692820);
  m1.SetElement((int)1,(int)2,(double)0.400000);
  m1.SetElement((int)2,(int)0,(double)0.200000);
  m1.SetElement((int)2,(int)1,(double)-0.469846);
  m1.SetElement((int)2,(int)2,(double)0.813798);
  f11 = new vtkTransformPolyDataFilter();
  f11.SetInputConnection((vtkAlgorithmOutput)ap.GetOutputPort());
  f11.SetTransform((vtkAbstractTransform)t1);
  m11 = new vtkDataSetMapper();
  m11.SetInputConnection((vtkAlgorithmOutput)f11.GetOutputPort());
  a11 = new vtkActor();
  a11.SetMapper((vtkMapper)m11);
  a11.GetProperty().SetColor((double)1,(double)0,(double)0);
  a11.GetProperty().SetRepresentationToWireframe();
  ren11 = vtkRenderer.New();
  ren11.SetViewport((double)0.0,(double)0.5,(double)0.25,(double)1.0);
  ren11.ResetCamera((double)-0.5,(double)0.5,(double)-0.5,(double)0.5,(double)-1,(double)1);
  ren11.AddActor((vtkProp)a11);
  renWin.AddRenderer((vtkRenderer)ren11);
  // inverse identity transform[]
  f12 = new vtkTransformPolyDataFilter();
  f12.SetInputConnection((vtkAlgorithmOutput)ap.GetOutputPort());
  f12.SetTransform((vtkAbstractTransform)t1.GetInverse());
  m12 = new vtkDataSetMapper();
  m12.SetInputConnection((vtkAlgorithmOutput)f12.GetOutputPort());
  a12 = new vtkActor();
  a12.SetMapper((vtkMapper)m12);
  a12.GetProperty().SetColor((double)0.9,(double)0.9,(double)0);
  a12.GetProperty().SetRepresentationToWireframe();
  ren12 = vtkRenderer.New();
  ren12.SetViewport((double)0.0,(double)0.0,(double)0.25,(double)0.5);
  ren12.ResetCamera((double)-0.5,(double)0.5,(double)-0.5,(double)0.5,(double)-1,(double)1);
  ren12.AddActor((vtkProp)a12);
  renWin.AddRenderer((vtkRenderer)ren12);
  //--------------------------[]
  // perspective transform matrix[]
  m2 = new vtkMatrix4x4();
  m2.SetElement((int)3,(int)0,(double)-0.11);
  m2.SetElement((int)3,(int)1,(double)0.3);
  m2.SetElement((int)3,(int)2,(double)0.2);
  t2 = new vtkMatrixToHomogeneousTransform();
  t2.SetInput((vtkMatrix4x4)m2);
  f21 = new vtkTransformPolyDataFilter();
  f21.SetInputConnection((vtkAlgorithmOutput)ap.GetOutputPort());
  f21.SetTransform((vtkAbstractTransform)t2);
  m21 = new vtkDataSetMapper();
  m21.SetInputConnection((vtkAlgorithmOutput)f21.GetOutputPort());
  a21 = new vtkActor();
  a21.SetMapper((vtkMapper)m21);
  a21.GetProperty().SetColor((double)1,(double)0,(double)0);
  a21.GetProperty().SetRepresentationToWireframe();
  ren21 = vtkRenderer.New();
  ren21.SetViewport((double)0.25,(double)0.5,(double)0.50,(double)1.0);
  ren21.ResetCamera((double)-0.5,(double)0.5,(double)-0.5,(double)0.5,(double)-1,(double)1);
  ren21.AddActor((vtkProp)a21);
  renWin.AddRenderer((vtkRenderer)ren21);
  // inverse linear transform[]
  f22 = new vtkTransformPolyDataFilter();
  f22.SetInputConnection((vtkAlgorithmOutput)ap.GetOutputPort());
  f22.SetTransform((vtkAbstractTransform)t2.GetInverse());
  m22 = new vtkDataSetMapper();
  m22.SetInputConnection((vtkAlgorithmOutput)f22.GetOutputPort());
  a22 = new vtkActor();
  a22.SetMapper((vtkMapper)m22);
  a22.GetProperty().SetColor((double)0.9,(double)0.9,(double)0);
  a22.GetProperty().SetRepresentationToWireframe();
  ren22 = vtkRenderer.New();
  ren22.SetViewport((double)0.25,(double)0.0,(double)0.50,(double)0.5);
  ren22.ResetCamera((double)-0.5,(double)0.5,(double)-0.5,(double)0.5,(double)-1,(double)1);
  ren22.AddActor((vtkProp)a22);
  renWin.AddRenderer((vtkRenderer)ren22);
  //--------------------------[]
  // linear concatenation - should end up with identity here[]
  t3 = new vtkTransform();
  t3.Concatenate((vtkLinearTransform)t1);
  t3.Concatenate((vtkLinearTransform)t1.GetInverse());
  f31 = new vtkTransformPolyDataFilter();
  f31.SetInputConnection((vtkAlgorithmOutput)ap.GetOutputPort());
  f31.SetTransform((vtkAbstractTransform)t3);
  m31 = new vtkDataSetMapper();
  m31.SetInputConnection((vtkAlgorithmOutput)f31.GetOutputPort());
  a31 = new vtkActor();
  a31.SetMapper((vtkMapper)m31);
  a31.GetProperty().SetColor((double)1,(double)0,(double)0);
  a31.GetProperty().SetRepresentationToWireframe();
  ren31 = vtkRenderer.New();
  ren31.SetViewport((double)0.50,(double)0.5,(double)0.75,(double)1.0);
  ren31.ResetCamera((double)-0.5,(double)0.5,(double)-0.5,(double)0.5,(double)-1,(double)1);
  ren31.AddActor((vtkProp)a31);
  renWin.AddRenderer((vtkRenderer)ren31);
  // inverse linear transform[]
  f32 = new vtkTransformPolyDataFilter();
  f32.SetInputConnection((vtkAlgorithmOutput)ap.GetOutputPort());
  f32.SetTransform((vtkAbstractTransform)t3.GetInverse());
  m32 = new vtkDataSetMapper();
  m32.SetInputConnection((vtkAlgorithmOutput)f32.GetOutputPort());
  a32 = new vtkActor();
  a32.SetMapper((vtkMapper)m32);
  a32.GetProperty().SetColor((double)0.9,(double)0.9,(double)0);
  a32.GetProperty().SetRepresentationToWireframe();
  ren32 = vtkRenderer.New();
  ren32.SetViewport((double)0.5,(double)0.0,(double)0.75,(double)0.5);
  ren32.ResetCamera((double)-0.5,(double)0.5,(double)-0.5,(double)0.5,(double)-1,(double)1);
  ren32.AddActor((vtkProp)a32);
  renWin.AddRenderer((vtkRenderer)ren32);
  //--------------------------[]
  // perspective transform concatenation[]
  t4 = new vtkPerspectiveTransform();
  t4.Concatenate((vtkHomogeneousTransform)t1);
  t4.Concatenate((vtkHomogeneousTransform)t2);
  t4.Concatenate((vtkHomogeneousTransform)t3);
  f41 = new vtkTransformPolyDataFilter();
  f41.SetInputConnection((vtkAlgorithmOutput)ap.GetOutputPort());
  f41.SetTransform((vtkAbstractTransform)t4);
  m41 = new vtkDataSetMapper();
  m41.SetInputConnection((vtkAlgorithmOutput)f41.GetOutputPort());
  a41 = new vtkActor();
  a41.SetMapper((vtkMapper)m41);
  a41.GetProperty().SetColor((double)1,(double)0,(double)0);
  a41.GetProperty().SetRepresentationToWireframe();
  ren41 = vtkRenderer.New();
  ren41.SetViewport((double)0.75,(double)0.5,(double)1.0,(double)1.0);
  ren41.ResetCamera((double)-0.5,(double)0.5,(double)-0.5,(double)0.5,(double)-1,(double)1);
  ren41.AddActor((vtkProp)a41);
  renWin.AddRenderer((vtkRenderer)ren41);
  // inverse of transform concatenation[]
  f42 = new vtkTransformPolyDataFilter();
  f42.SetInputConnection((vtkAlgorithmOutput)ap.GetOutputPort());
  f42.SetTransform((vtkAbstractTransform)t4.GetInverse());
  m42 = new vtkDataSetMapper();
  m42.SetInputConnection((vtkAlgorithmOutput)f42.GetOutputPort());
  a42 = new vtkActor();
  a42.SetMapper((vtkMapper)m42);
  a42.GetProperty().SetColor((double)0.9,(double)0.9,(double)0);
  a42.GetProperty().SetRepresentationToWireframe();
  ren42 = vtkRenderer.New();
  ren42.SetViewport((double)0.75,(double)0.0,(double)1.0,(double)0.5);
  ren42.ResetCamera((double)-0.5,(double)0.5,(double)-0.5,(double)0.5,(double)-1,(double)1);
  ren42.AddActor((vtkProp)a42);
  renWin.AddRenderer((vtkRenderer)ren42);
  renWin.Render();
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkRenderWindow renWin;
static vtkPlaneSource p1;
static vtkPlaneSource p2;
static vtkPlaneSource p3;
static vtkPlaneSource p4;
static vtkPlaneSource p5;
static vtkPlaneSource p6;
static vtkAppendPolyData ap;
static vtkMatrixToLinearTransform t1;
static vtkMatrix4x4 m1;
static vtkTransformPolyDataFilter f11;
static vtkDataSetMapper m11;
static vtkActor a11;
static vtkRenderer ren11;
static vtkTransformPolyDataFilter f12;
static vtkDataSetMapper m12;
static vtkActor a12;
static vtkRenderer ren12;
static vtkMatrix4x4 m2;
static vtkMatrixToHomogeneousTransform t2;
static vtkTransformPolyDataFilter f21;
static vtkDataSetMapper m21;
static vtkActor a21;
static vtkRenderer ren21;
static vtkTransformPolyDataFilter f22;
static vtkDataSetMapper m22;
static vtkActor a22;
static vtkRenderer ren22;
static vtkTransform t3;
static vtkTransformPolyDataFilter f31;
static vtkDataSetMapper m31;
static vtkActor a31;
static vtkRenderer ren31;
static vtkTransformPolyDataFilter f32;
static vtkDataSetMapper m32;
static vtkActor a32;
static vtkRenderer ren32;
static vtkPerspectiveTransform t4;
static vtkTransformPolyDataFilter f41;
static vtkDataSetMapper m41;
static vtkActor a41;
static vtkRenderer ren41;
static vtkTransformPolyDataFilter f42;
static vtkDataSetMapper m42;
static vtkActor a42;
static vtkRenderer ren42;


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
        public static vtkPlaneSource Getp1()
        {
            return p1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setp1(vtkPlaneSource toSet)
        {
            p1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPlaneSource Getp2()
        {
            return p2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setp2(vtkPlaneSource toSet)
        {
            p2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPlaneSource Getp3()
        {
            return p3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setp3(vtkPlaneSource toSet)
        {
            p3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPlaneSource Getp4()
        {
            return p4;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setp4(vtkPlaneSource toSet)
        {
            p4 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPlaneSource Getp5()
        {
            return p5;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setp5(vtkPlaneSource toSet)
        {
            p5 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPlaneSource Getp6()
        {
            return p6;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setp6(vtkPlaneSource toSet)
        {
            p6 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkAppendPolyData Getap()
        {
            return ap;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setap(vtkAppendPolyData toSet)
        {
            ap = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkMatrixToLinearTransform Gett1()
        {
            return t1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sett1(vtkMatrixToLinearTransform toSet)
        {
            t1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkMatrix4x4 Getm1()
        {
            return m1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setm1(vtkMatrix4x4 toSet)
        {
            m1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTransformPolyDataFilter Getf11()
        {
            return f11;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setf11(vtkTransformPolyDataFilter toSet)
        {
            f11 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper Getm11()
        {
            return m11;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setm11(vtkDataSetMapper toSet)
        {
            m11 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Geta11()
        {
            return a11;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Seta11(vtkActor toSet)
        {
            a11 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRenderer Getren11()
        {
            return ren11;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setren11(vtkRenderer toSet)
        {
            ren11 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTransformPolyDataFilter Getf12()
        {
            return f12;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setf12(vtkTransformPolyDataFilter toSet)
        {
            f12 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper Getm12()
        {
            return m12;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setm12(vtkDataSetMapper toSet)
        {
            m12 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Geta12()
        {
            return a12;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Seta12(vtkActor toSet)
        {
            a12 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRenderer Getren12()
        {
            return ren12;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setren12(vtkRenderer toSet)
        {
            ren12 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkMatrix4x4 Getm2()
        {
            return m2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setm2(vtkMatrix4x4 toSet)
        {
            m2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkMatrixToHomogeneousTransform Gett2()
        {
            return t2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sett2(vtkMatrixToHomogeneousTransform toSet)
        {
            t2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTransformPolyDataFilter Getf21()
        {
            return f21;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setf21(vtkTransformPolyDataFilter toSet)
        {
            f21 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper Getm21()
        {
            return m21;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setm21(vtkDataSetMapper toSet)
        {
            m21 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Geta21()
        {
            return a21;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Seta21(vtkActor toSet)
        {
            a21 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRenderer Getren21()
        {
            return ren21;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setren21(vtkRenderer toSet)
        {
            ren21 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTransformPolyDataFilter Getf22()
        {
            return f22;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setf22(vtkTransformPolyDataFilter toSet)
        {
            f22 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper Getm22()
        {
            return m22;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setm22(vtkDataSetMapper toSet)
        {
            m22 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Geta22()
        {
            return a22;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Seta22(vtkActor toSet)
        {
            a22 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRenderer Getren22()
        {
            return ren22;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setren22(vtkRenderer toSet)
        {
            ren22 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTransform Gett3()
        {
            return t3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sett3(vtkTransform toSet)
        {
            t3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTransformPolyDataFilter Getf31()
        {
            return f31;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setf31(vtkTransformPolyDataFilter toSet)
        {
            f31 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper Getm31()
        {
            return m31;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setm31(vtkDataSetMapper toSet)
        {
            m31 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Geta31()
        {
            return a31;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Seta31(vtkActor toSet)
        {
            a31 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRenderer Getren31()
        {
            return ren31;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setren31(vtkRenderer toSet)
        {
            ren31 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTransformPolyDataFilter Getf32()
        {
            return f32;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setf32(vtkTransformPolyDataFilter toSet)
        {
            f32 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper Getm32()
        {
            return m32;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setm32(vtkDataSetMapper toSet)
        {
            m32 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Geta32()
        {
            return a32;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Seta32(vtkActor toSet)
        {
            a32 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRenderer Getren32()
        {
            return ren32;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setren32(vtkRenderer toSet)
        {
            ren32 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPerspectiveTransform Gett4()
        {
            return t4;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sett4(vtkPerspectiveTransform toSet)
        {
            t4 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTransformPolyDataFilter Getf41()
        {
            return f41;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setf41(vtkTransformPolyDataFilter toSet)
        {
            f41 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper Getm41()
        {
            return m41;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setm41(vtkDataSetMapper toSet)
        {
            m41 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Geta41()
        {
            return a41;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Seta41(vtkActor toSet)
        {
            a41 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRenderer Getren41()
        {
            return ren41;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setren41(vtkRenderer toSet)
        {
            ren41 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTransformPolyDataFilter Getf42()
        {
            return f42;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setf42(vtkTransformPolyDataFilter toSet)
        {
            f42 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper Getm42()
        {
            return m42;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setm42(vtkDataSetMapper toSet)
        {
            m42 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Geta42()
        {
            return a42;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Seta42(vtkActor toSet)
        {
            a42 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRenderer Getren42()
        {
            return ren42;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setren42(vtkRenderer toSet)
        {
            ren42 = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(renWin!= null){renWin.Dispose();}
  	if(p1!= null){p1.Dispose();}
  	if(p2!= null){p2.Dispose();}
  	if(p3!= null){p3.Dispose();}
  	if(p4!= null){p4.Dispose();}
  	if(p5!= null){p5.Dispose();}
  	if(p6!= null){p6.Dispose();}
  	if(ap!= null){ap.Dispose();}
  	if(t1!= null){t1.Dispose();}
  	if(m1!= null){m1.Dispose();}
  	if(f11!= null){f11.Dispose();}
  	if(m11!= null){m11.Dispose();}
  	if(a11!= null){a11.Dispose();}
  	if(ren11!= null){ren11.Dispose();}
  	if(f12!= null){f12.Dispose();}
  	if(m12!= null){m12.Dispose();}
  	if(a12!= null){a12.Dispose();}
  	if(ren12!= null){ren12.Dispose();}
  	if(m2!= null){m2.Dispose();}
  	if(t2!= null){t2.Dispose();}
  	if(f21!= null){f21.Dispose();}
  	if(m21!= null){m21.Dispose();}
  	if(a21!= null){a21.Dispose();}
  	if(ren21!= null){ren21.Dispose();}
  	if(f22!= null){f22.Dispose();}
  	if(m22!= null){m22.Dispose();}
  	if(a22!= null){a22.Dispose();}
  	if(ren22!= null){ren22.Dispose();}
  	if(t3!= null){t3.Dispose();}
  	if(f31!= null){f31.Dispose();}
  	if(m31!= null){m31.Dispose();}
  	if(a31!= null){a31.Dispose();}
  	if(ren31!= null){ren31.Dispose();}
  	if(f32!= null){f32.Dispose();}
  	if(m32!= null){m32.Dispose();}
  	if(a32!= null){a32.Dispose();}
  	if(ren32!= null){ren32.Dispose();}
  	if(t4!= null){t4.Dispose();}
  	if(f41!= null){f41.Dispose();}
  	if(m41!= null){m41.Dispose();}
  	if(a41!= null){a41.Dispose();}
  	if(ren41!= null){ren41.Dispose();}
  	if(f42!= null){f42.Dispose();}
  	if(m42!= null){m42.Dispose();}
  	if(a42!= null){a42.Dispose();}
  	if(ren42!= null){ren42.Dispose();}
  }

}
//--- end of script --//

