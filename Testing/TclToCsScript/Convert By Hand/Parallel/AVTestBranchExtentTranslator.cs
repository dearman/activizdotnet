using Kitware.VTK;
using System;
// input file is C:\VTK\Parallel\Testing\Tcl\TestBranchExtentTranslator.tcl
// output file is AVTestBranchExtentTranslator.cs
/// <summary>
/// The testing class derived from AVTestBranchExtentTranslator
/// </summary>
public class AVTestBranchExtentTranslatorClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVTestBranchExtentTranslator(String [] argv)
  {
  //Prefix Content is: ""
  
  gauss = new vtkImageGaussianSource();
  gauss.SetWholeExtent((int)0,(int)30,(int)0,(int)30,(int)0,(int)2);
  gauss.SetCenter((double)18,(double)12,(double)0);
  gauss.SetMaximum((double)1.0);
  gauss.SetStandardDeviation((double)6.0);
  gauss.Update();
  translator = new vtkBranchExtentTranslator();
  translator.SetOriginalSource((vtkImageData)gauss.GetOutput());
  gauss.GetOutput().SetExtentTranslator((vtkExtentTranslator)translator);
  clip1 = new vtkImageClip();
  clip1.SetOutputWholeExtent((int)7,(int)28,(int)0,(int)15,(int)1,(int)1);
  clip1.SetInputConnection((vtkAlgorithmOutput)gauss.GetOutputPort());
  surf1 = new vtkDataSetSurfaceFilter();
  surf1.SetInputConnection((vtkAlgorithmOutput)clip1.GetOutputPort());
  tf1 = new vtkTriangleFilter();
  tf1.SetInputConnection((vtkAlgorithmOutput)surf1.GetOutputPort());
  mapper1 = vtkPolyDataMapper.New();
  mapper1.SetInputConnection((vtkAlgorithmOutput)tf1.GetOutputPort());
  mapper1.SetScalarRange((double)0,(double)1);
  mapper1.SetNumberOfPieces((int)4);
  mapper1.SetPiece((int)1);
  actor1 = new vtkActor();
  actor1.SetMapper((vtkMapper)mapper1);
  actor1.SetPosition((double)0,(double)0,(double)0);
  // For coverage, a case where all four sides get clipped by the whole extent.[]
  clip2 = new vtkImageClip();
  clip2.SetOutputWholeExtent((int)16,(int)18,(int)3,(int)10,(int)0,(int)0);
  clip2.SetInputConnection((vtkAlgorithmOutput)gauss.GetOutputPort());
  surf2 = new vtkDataSetSurfaceFilter();
  surf2.SetInputConnection((vtkAlgorithmOutput)clip2.GetOutputPort());
  tf2 = new vtkTriangleFilter();
  tf2.SetInputConnection((vtkAlgorithmOutput)surf2.GetOutputPort());
  mapper2 = vtkPolyDataMapper.New();
  mapper2.SetInputConnection((vtkAlgorithmOutput)tf2.GetOutputPort());
  mapper2.SetScalarRange((double)0,(double)1);
  mapper2.SetNumberOfPieces((int)4);
  mapper2.SetPiece((int)1);
  actor2 = new vtkActor();
  actor2.SetMapper((vtkMapper)mapper2);
  actor2.SetPosition((double)15,(double)0,(double)0);
  // nothing in intersection (empty case)[]
  clip3 = new vtkImageClip();
  clip3.SetOutputWholeExtent((int)1,(int)10,(int)0,(int)15,(int)0,(int)2);
  clip3.SetInputConnection((vtkAlgorithmOutput)gauss.GetOutputPort());
  surf3 = new vtkDataSetSurfaceFilter();
  surf3.SetInputConnection((vtkAlgorithmOutput)clip3.GetOutputPort());
  tf3 = new vtkTriangleFilter();
  tf3.SetInputConnection((vtkAlgorithmOutput)surf3.GetOutputPort());
  mapper3 = vtkPolyDataMapper.New();
  mapper3.SetInputConnection((vtkAlgorithmOutput)tf3.GetOutputPort());
  mapper3.SetScalarRange((double)0,(double)1);
  mapper3.SetNumberOfPieces((int)4);
  mapper3.SetPiece((int)1);
  actor3 = new vtkActor();
  actor3.SetMapper((vtkMapper)mapper3);
  actor3.SetPosition((double)30,(double)0,(double)0);
  ren = vtkRenderer.New();
  ren.AddActor((vtkProp)actor1);
  ren.AddActor((vtkProp)actor2);
  ren.AddActor((vtkProp)actor3);
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren);
  //set cam [ren GetActiveCamera][]
  //ren ResetCamera[]
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  iren.Initialize();
  renWin.Render();
  // break loop to avoid a memory leak.[]
  translator.SetOriginalSource(null);
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkImageGaussianSource gauss;
static vtkBranchExtentTranslator translator;
static vtkImageClip clip1;
static vtkDataSetSurfaceFilter surf1;
static vtkTriangleFilter tf1;
static vtkPolyDataMapper mapper1;
static vtkActor actor1;
static vtkImageClip clip2;
static vtkDataSetSurfaceFilter surf2;
static vtkTriangleFilter tf2;
static vtkPolyDataMapper mapper2;
static vtkActor actor2;
static vtkImageClip clip3;
static vtkDataSetSurfaceFilter surf3;
static vtkTriangleFilter tf3;
static vtkPolyDataMapper mapper3;
static vtkActor actor3;
static vtkRenderer ren;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;


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
        public static vtkImageGaussianSource Getgauss()
        {
            return gauss;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setgauss(vtkImageGaussianSource toSet)
        {
            gauss = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkBranchExtentTranslator Gettranslator()
        {
            return translator;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settranslator(vtkBranchExtentTranslator toSet)
        {
            translator = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageClip Getclip1()
        {
            return clip1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setclip1(vtkImageClip toSet)
        {
            clip1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetSurfaceFilter Getsurf1()
        {
            return surf1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setsurf1(vtkDataSetSurfaceFilter toSet)
        {
            surf1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTriangleFilter Gettf1()
        {
            return tf1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settf1(vtkTriangleFilter toSet)
        {
            tf1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Getmapper1()
        {
            return mapper1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setmapper1(vtkPolyDataMapper toSet)
        {
            mapper1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getactor1()
        {
            return actor1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setactor1(vtkActor toSet)
        {
            actor1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageClip Getclip2()
        {
            return clip2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setclip2(vtkImageClip toSet)
        {
            clip2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetSurfaceFilter Getsurf2()
        {
            return surf2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setsurf2(vtkDataSetSurfaceFilter toSet)
        {
            surf2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTriangleFilter Gettf2()
        {
            return tf2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settf2(vtkTriangleFilter toSet)
        {
            tf2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Getmapper2()
        {
            return mapper2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setmapper2(vtkPolyDataMapper toSet)
        {
            mapper2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getactor2()
        {
            return actor2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setactor2(vtkActor toSet)
        {
            actor2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImageClip Getclip3()
        {
            return clip3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setclip3(vtkImageClip toSet)
        {
            clip3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetSurfaceFilter Getsurf3()
        {
            return surf3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setsurf3(vtkDataSetSurfaceFilter toSet)
        {
            surf3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTriangleFilter Gettf3()
        {
            return tf3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settf3(vtkTriangleFilter toSet)
        {
            tf3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Getmapper3()
        {
            return mapper3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setmapper3(vtkPolyDataMapper toSet)
        {
            mapper3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getactor3()
        {
            return actor3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setactor3(vtkActor toSet)
        {
            actor3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRenderer Getren()
        {
            return ren;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setren(vtkRenderer toSet)
        {
            ren = toSet;
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
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(gauss!= null){gauss.Dispose();}
  	if(translator!= null){translator.Dispose();}
  	if(clip1!= null){clip1.Dispose();}
  	if(surf1!= null){surf1.Dispose();}
  	if(tf1!= null){tf1.Dispose();}
  	if(mapper1!= null){mapper1.Dispose();}
  	if(actor1!= null){actor1.Dispose();}
  	if(clip2!= null){clip2.Dispose();}
  	if(surf2!= null){surf2.Dispose();}
  	if(tf2!= null){tf2.Dispose();}
  	if(mapper2!= null){mapper2.Dispose();}
  	if(actor2!= null){actor2.Dispose();}
  	if(clip3!= null){clip3.Dispose();}
  	if(surf3!= null){surf3.Dispose();}
  	if(tf3!= null){tf3.Dispose();}
  	if(mapper3!= null){mapper3.Dispose();}
  	if(actor3!= null){actor3.Dispose();}
  	if(ren!= null){ren.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  }

}
//--- end of script --//

