using Kitware.VTK;
using System;
// input file is C:\VTK\Parallel\Testing\Tcl\TestUnstructuredPieces.tcl
// output file is AVTestUnstructuredPieces.cs
/// <summary>
/// The testing class derived from AVTestUnstructuredPieces
/// </summary>
public class AVTestUnstructuredPiecesClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVTestUnstructuredPieces(String [] argv)
  {
  //Prefix Content is: ""
  
  math = new vtkMath();
  vtkMath.RandomSeed((int)22);
  pf = new vtkParallelFactory();
  vtkParallelFactory.RegisterFactory((vtkObjectFactory)pf);
  pl3d = new vtkMultiBlockPLOT3DReader();
  pl3d.SetXYZFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/combxyz.bin");
  pl3d.SetQFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/combq.bin");
  pl3d.SetScalarFunctionNumber((int)100);
  dst = new vtkDataSetTriangleFilter();
  dst.SetInputData((vtkDataSet)pl3d.GetOutput().GetBlock(0));
  extract = new vtkExtractUnstructuredGridPiece();
  extract.SetInputConnection((vtkAlgorithmOutput)dst.GetOutputPort());
  cf = new vtkContourFilter();
  cf.SetInputConnection((vtkAlgorithmOutput)extract.GetOutputPort());
  cf.SetValue((int)0,(double)0.24);
  pdn = new vtkPolyDataNormals();
  pdn.SetInputConnection((vtkAlgorithmOutput)cf.GetOutputPort());
  ps = new vtkPieceScalars();
  ps.SetInputConnection((vtkAlgorithmOutput)pdn.GetOutputPort());
  mapper = vtkPolyDataMapper.New();
  mapper.SetInputConnection((vtkAlgorithmOutput)ps.GetOutputPort());
  mapper.SetNumberOfPieces((int)3);
  actor = new vtkActor();
  actor.SetMapper((vtkMapper)mapper);
  ren = vtkRenderer.New();
  ren.AddActor((vtkProp)actor);
  ren.ResetCamera();
  camera = ren.GetActiveCamera();
  //$camera SetPosition 68.1939 -23.4323 12.6465[]
  //$camera SetViewUp 0.46563 0.882375 0.0678508  []
  //$camera SetFocalPoint 3.65707 11.4552 1.83509 []
  //$camera SetClippingRange 59.2626 101.825 []
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  iren.Initialize();
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkMath math;
static vtkParallelFactory pf;
static vtkMultiBlockPLOT3DReader pl3d;
static vtkDataSetTriangleFilter dst;
static vtkExtractUnstructuredGridPiece extract;
static vtkContourFilter cf;
static vtkPolyDataNormals pdn;
static vtkPieceScalars ps;
static vtkPolyDataMapper mapper;
static vtkActor actor;
static vtkRenderer ren;
static vtkCamera camera;
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
        public static vtkParallelFactory Getpf()
        {
            return pf;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpf(vtkParallelFactory toSet)
        {
            pf = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkMultiBlockPLOT3DReader Getpl3d()
        {
            return pl3d;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpl3d(vtkMultiBlockPLOT3DReader toSet)
        {
            pl3d = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetTriangleFilter Getdst()
        {
            return dst;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setdst(vtkDataSetTriangleFilter toSet)
        {
            dst = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkExtractUnstructuredGridPiece Getextract()
        {
            return extract;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setextract(vtkExtractUnstructuredGridPiece toSet)
        {
            extract = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkContourFilter Getcf()
        {
            return cf;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcf(vtkContourFilter toSet)
        {
            cf = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataNormals Getpdn()
        {
            return pdn;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpdn(vtkPolyDataNormals toSet)
        {
            pdn = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPieceScalars Getps()
        {
            return ps;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setps(vtkPieceScalars toSet)
        {
            ps = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Getmapper()
        {
            return mapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setmapper(vtkPolyDataMapper toSet)
        {
            mapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getactor()
        {
            return actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setactor(vtkActor toSet)
        {
            actor = toSet;
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
  	if(math!= null){math.Dispose();}
  	if(pf!= null){pf.Dispose();}
  	if(pl3d!= null){pl3d.Dispose();}
  	if(dst!= null){dst.Dispose();}
  	if(extract!= null){extract.Dispose();}
  	if(cf!= null){cf.Dispose();}
  	if(pdn!= null){pdn.Dispose();}
  	if(ps!= null){ps.Dispose();}
  	if(mapper!= null){mapper.Dispose();}
  	if(actor!= null){actor.Dispose();}
  	if(ren!= null){ren.Dispose();}
  	if(camera!= null){camera.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  }

}
//--- end of script --//

