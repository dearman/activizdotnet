using Kitware.VTK;
using System;
// input file is C:\VTK\Parallel\Testing\Tcl\TestPolyDataPieces.tcl
// output file is AVTestPolyDataPieces.cs
/// <summary>
/// The testing class derived from AVTestPolyDataPieces
/// </summary>
public class AVTestPolyDataPiecesClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVTestPolyDataPieces(String [] argv)
  {
  //Prefix Content is: ""
  
  math = new vtkMath();
  vtkMath.RandomSeed((int)22);
  pf = new vtkParallelFactory();
  vtkParallelFactory.RegisterFactory((vtkObjectFactory)pf);
  sphere = new vtkSphereSource();
  sphere.SetPhiResolution((int)32);
  sphere.SetThetaResolution((int)32);
  extract = new vtkExtractPolyDataPiece();
  extract.SetInputConnection((vtkAlgorithmOutput)sphere.GetOutputPort());
  normals = new vtkPolyDataNormals();
  normals.SetInputConnection((vtkAlgorithmOutput)extract.GetOutputPort());
  ps = new vtkPieceScalars();
  ps.SetInputConnection((vtkAlgorithmOutput)normals.GetOutputPort());
  mapper = vtkPolyDataMapper.New();
  mapper.SetInputConnection((vtkAlgorithmOutput)ps.GetOutputPort());
  mapper.SetNumberOfPieces((int)2);
  actor = new vtkActor();
  actor.SetMapper((vtkMapper)mapper);
  sphere2 = new vtkSphereSource();
  sphere2.SetPhiResolution((int)32);
  sphere2.SetThetaResolution((int)32);
  extract2 = new vtkExtractPolyDataPiece();
  extract2.SetInputConnection((vtkAlgorithmOutput)sphere2.GetOutputPort());
  mapper2 = vtkPolyDataMapper.New();
  mapper2.SetInputConnection((vtkAlgorithmOutput)extract2.GetOutputPort());
  mapper2.SetNumberOfPieces((int)2);
  mapper2.SetPiece((int)1);
  mapper2.SetScalarRange((double)0,(double)4);
  mapper2.SetScalarModeToUseCellFieldData();
  mapper2.SetColorModeToMapScalars();
  mapper2.ColorByArrayComponent((string)"vtkGhostLevels",(int)0);
  mapper2.SetGhostLevel((int)4);
  // check the pipeline size[]
  extract2.UpdateInformation();
  psize = new vtkPipelineSize();
  if ((psize.GetEstimatedSize((vtkAlgorithm)extract2,(int)0,(int)0)) > 100)
    {
      //puts skipedputs ['stderr', '"ERROR: Pipeline Size increased"']
    }

  
  if ((psize.GetNumberOfSubPieces((uint)10,(vtkPolyDataMapper)mapper2)) != 2)
    {
      //puts skipedputs ['stderr', '"ERROR: Number of sub pieces changed"']
    }

  
  actor2 = new vtkActor();
  actor2.SetMapper((vtkMapper)mapper2);
  actor2.SetPosition((double)1.5,(double)0,(double)0);
  sphere3 = new vtkSphereSource();
  sphere3.SetPhiResolution((int)32);
  sphere3.SetThetaResolution((int)32);
  extract3 = new vtkExtractPolyDataPiece();
  extract3.SetInputConnection((vtkAlgorithmOutput)sphere3.GetOutputPort());
  ps3 = new vtkPieceScalars();
  ps3.SetInputConnection((vtkAlgorithmOutput)extract3.GetOutputPort());
  mapper3 = vtkPolyDataMapper.New();
  mapper3.SetInputConnection((vtkAlgorithmOutput)ps3.GetOutputPort());
  mapper3.SetNumberOfSubPieces((int)8);
  mapper3.SetScalarRange((double)0,(double)8);
  actor3 = new vtkActor();
  actor3.SetMapper((vtkMapper)mapper3);
  actor3.SetPosition((double)0,(double)-1.5,(double)0);
  sphere4 = new vtkSphereSource();
  sphere4.SetPhiResolution((int)32);
  sphere4.SetThetaResolution((int)32);
  extract4 = new vtkExtractPolyDataPiece();
  extract4.SetInputConnection((vtkAlgorithmOutput)sphere4.GetOutputPort());
  ps4 = new vtkPieceScalars();
  ps4.RandomModeOn();
  ps4.SetScalarModeToCellData();
  ps4.SetInputConnection((vtkAlgorithmOutput)extract4.GetOutputPort());
  mapper4 = vtkPolyDataMapper.New();
  mapper4.SetInputConnection((vtkAlgorithmOutput)ps4.GetOutputPort());
  mapper4.SetNumberOfSubPieces((int)8);
  mapper4.SetScalarRange((double)0,(double)8);
  actor4 = new vtkActor();
  actor4.SetMapper((vtkMapper)mapper4);
  actor4.SetPosition((double)1.5,(double)-1.5,(double)0);
  ren = vtkRenderer.New();
  ren.AddActor((vtkProp)actor);
  ren.AddActor((vtkProp)actor2);
  ren.AddActor((vtkProp)actor3);
  ren.AddActor((vtkProp)actor4);
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
static vtkSphereSource sphere;
static vtkExtractPolyDataPiece extract;
static vtkPolyDataNormals normals;
static vtkPieceScalars ps;
static vtkPolyDataMapper mapper;
static vtkActor actor;
static vtkSphereSource sphere2;
static vtkExtractPolyDataPiece extract2;
static vtkPolyDataMapper mapper2;
static vtkPipelineSize psize;
static vtkActor actor2;
static vtkSphereSource sphere3;
static vtkExtractPolyDataPiece extract3;
static vtkPieceScalars ps3;
static vtkPolyDataMapper mapper3;
static vtkActor actor3;
static vtkSphereSource sphere4;
static vtkExtractPolyDataPiece extract4;
static vtkPieceScalars ps4;
static vtkPolyDataMapper mapper4;
static vtkActor actor4;
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
        public static vtkSphereSource Getsphere()
        {
            return sphere;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setsphere(vtkSphereSource toSet)
        {
            sphere = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkExtractPolyDataPiece Getextract()
        {
            return extract;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setextract(vtkExtractPolyDataPiece toSet)
        {
            extract = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataNormals Getnormals()
        {
            return normals;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setnormals(vtkPolyDataNormals toSet)
        {
            normals = toSet;
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
        public static vtkSphereSource Getsphere2()
        {
            return sphere2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setsphere2(vtkSphereSource toSet)
        {
            sphere2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkExtractPolyDataPiece Getextract2()
        {
            return extract2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setextract2(vtkExtractPolyDataPiece toSet)
        {
            extract2 = toSet;
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
        public static vtkPipelineSize Getpsize()
        {
            return psize;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpsize(vtkPipelineSize toSet)
        {
            psize = toSet;
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
        public static vtkSphereSource Getsphere3()
        {
            return sphere3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setsphere3(vtkSphereSource toSet)
        {
            sphere3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkExtractPolyDataPiece Getextract3()
        {
            return extract3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setextract3(vtkExtractPolyDataPiece toSet)
        {
            extract3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPieceScalars Getps3()
        {
            return ps3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setps3(vtkPieceScalars toSet)
        {
            ps3 = toSet;
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
        public static vtkSphereSource Getsphere4()
        {
            return sphere4;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setsphere4(vtkSphereSource toSet)
        {
            sphere4 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkExtractPolyDataPiece Getextract4()
        {
            return extract4;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setextract4(vtkExtractPolyDataPiece toSet)
        {
            extract4 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPieceScalars Getps4()
        {
            return ps4;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setps4(vtkPieceScalars toSet)
        {
            ps4 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Getmapper4()
        {
            return mapper4;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setmapper4(vtkPolyDataMapper toSet)
        {
            mapper4 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getactor4()
        {
            return actor4;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setactor4(vtkActor toSet)
        {
            actor4 = toSet;
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
  	if(math!= null){math.Dispose();}
  	if(pf!= null){pf.Dispose();}
  	if(sphere!= null){sphere.Dispose();}
  	if(extract!= null){extract.Dispose();}
  	if(normals!= null){normals.Dispose();}
  	if(ps!= null){ps.Dispose();}
  	if(mapper!= null){mapper.Dispose();}
  	if(actor!= null){actor.Dispose();}
  	if(sphere2!= null){sphere2.Dispose();}
  	if(extract2!= null){extract2.Dispose();}
  	if(mapper2!= null){mapper2.Dispose();}
  	if(psize!= null){psize.Dispose();}
  	if(actor2!= null){actor2.Dispose();}
  	if(sphere3!= null){sphere3.Dispose();}
  	if(extract3!= null){extract3.Dispose();}
  	if(ps3!= null){ps3.Dispose();}
  	if(mapper3!= null){mapper3.Dispose();}
  	if(actor3!= null){actor3.Dispose();}
  	if(sphere4!= null){sphere4.Dispose();}
  	if(extract4!= null){extract4.Dispose();}
  	if(ps4!= null){ps4.Dispose();}
  	if(mapper4!= null){mapper4.Dispose();}
  	if(actor4!= null){actor4.Dispose();}
  	if(ren!= null){ren.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  }

}
//--- end of script --//

