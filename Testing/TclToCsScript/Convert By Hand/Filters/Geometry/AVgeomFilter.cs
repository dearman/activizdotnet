using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\geomFilter.tcl
// output file is AVgeomFilter.cs
/// <summary>
/// The testing class derived from AVgeomFilter
/// </summary>
public class AVgeomFilterClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVgeomFilter(String [] argv)
  {
  //Prefix Content is: ""
  
  // create pipeline - structured grid[]
  //[]
  pl3d = new vtkMultiBlockPLOT3DReader();
  pl3d.SetXYZFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/combxyz.bin");
  pl3d.SetQFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/combq.bin");
  pl3d.SetScalarFunctionNumber((int)100);
  pl3d.SetVectorFunctionNumber((int)202);
  pl3d.Update();
  gf = new vtkGeometryFilter();
  gf.SetInputData((vtkDataSet)pl3d.GetOutput().GetBlock(0));
  gMapper = vtkPolyDataMapper.New();
  gMapper.SetInputConnection((vtkAlgorithmOutput)gf.GetOutputPort());
  gMapper.SetScalarRange((double)((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetScalarRange()[0],
      (double)((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetScalarRange()[1]);
  gActor = new vtkActor();
  gActor.SetMapper((vtkMapper)gMapper);
  gf2 = new vtkGeometryFilter();
  gf2.SetInputData((vtkDataSet)pl3d.GetOutput().GetBlock(0));
  gf2.ExtentClippingOn();
  gf2.SetExtent((double)10,(double)17,(double)-6,(double)6,(double)23,(double)37);
  gf2.PointClippingOn();
  gf2.SetPointMinimum((int)0);
  gf2.SetPointMaximum((int)10000);
  gf2.CellClippingOn();
  gf2.SetCellMinimum((int)0);
  gf2.SetCellMaximum((int)7500);
  g2Mapper = vtkPolyDataMapper.New();
  g2Mapper.SetInputConnection((vtkAlgorithmOutput)gf2.GetOutputPort());
  g2Mapper.SetScalarRange((double)((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetScalarRange()[0],
      (double)((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetScalarRange()[1]);
  g2Actor = new vtkActor();
  g2Actor.SetMapper((vtkMapper)g2Mapper);
  g2Actor.AddPosition((double)0,(double)15,(double)0);
  // create pipeline - poly data[]
  //[]
  gf3 = new vtkGeometryFilter();
  gf3.SetInputConnection((vtkAlgorithmOutput)gf.GetOutputPort());
  g3Mapper = vtkPolyDataMapper.New();
  g3Mapper.SetInputConnection((vtkAlgorithmOutput)gf3.GetOutputPort());
  g3Mapper.SetScalarRange((double)((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetScalarRange()[0],
      (double)((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetScalarRange()[1]);
  g3Actor = new vtkActor();
  g3Actor.SetMapper((vtkMapper)g3Mapper);
  g3Actor.AddPosition((double)0,(double)0,(double)15);
  gf4 = new vtkGeometryFilter();
  gf4.SetInputConnection((vtkAlgorithmOutput)gf2.GetOutputPort());
  gf4.ExtentClippingOn();
  gf4.SetExtent((double)10,(double)17,(double)-6,(double)6,(double)23,(double)37);
  gf4.PointClippingOn();
  gf4.SetPointMinimum((int)0);
  gf4.SetPointMaximum((int)10000);
  gf4.CellClippingOn();
  gf4.SetCellMinimum((int)0);
  gf4.SetCellMaximum((int)7500);
  g4Mapper = vtkPolyDataMapper.New();
  g4Mapper.SetInputConnection((vtkAlgorithmOutput)gf4.GetOutputPort());
  g4Mapper.SetScalarRange((double)((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetScalarRange()[0],
      (double)((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetScalarRange()[1]);
  g4Actor = new vtkActor();
  g4Actor.SetMapper((vtkMapper)g4Mapper);
  g4Actor.AddPosition((double)0,(double)15,(double)15);
  // create pipeline - unstructured grid[]
  //[]
  s = new vtkSphere();
  s.SetCenter(((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetCenter()[0], 
              ((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetCenter()[1], 
              ((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetCenter()[2]);
  s.SetRadius((double)100.0);
  //everything[]
  eg = new vtkExtractGeometry();
  eg.SetInputData((vtkDataSet)pl3d.GetOutput().GetBlock(0));
  eg.SetImplicitFunction((vtkImplicitFunction)s);
  gf5 = new vtkGeometryFilter();
  gf5.SetInputConnection((vtkAlgorithmOutput)eg.GetOutputPort());
  g5Mapper = vtkPolyDataMapper.New();
  g5Mapper.SetInputConnection((vtkAlgorithmOutput)gf5.GetOutputPort());
  g5Mapper.SetScalarRange((double)((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetScalarRange()[0],
      (double)((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetScalarRange()[1]);
  g5Actor = new vtkActor();
  g5Actor.SetMapper((vtkMapper)g5Mapper);
  g5Actor.AddPosition((double)0,(double)0,(double)30);
  gf6 = new vtkGeometryFilter();
  gf6.SetInputConnection((vtkAlgorithmOutput)eg.GetOutputPort());
  gf6.ExtentClippingOn();
  gf6.SetExtent((double)10,(double)17,(double)-6,(double)6,(double)23,(double)37);
  gf6.PointClippingOn();
  gf6.SetPointMinimum((int)0);
  gf6.SetPointMaximum((int)10000);
  gf6.CellClippingOn();
  gf6.SetCellMinimum((int)0);
  gf6.SetCellMaximum((int)7500);
  g6Mapper = vtkPolyDataMapper.New();
  g6Mapper.SetInputConnection((vtkAlgorithmOutput)gf6.GetOutputPort());
  g6Mapper.SetScalarRange((double)((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetScalarRange()[0],
      (double)((vtkDataSet)pl3d.GetOutput().GetBlock(0)).GetScalarRange()[1]);
  g6Actor = new vtkActor();
  g6Actor.SetMapper((vtkMapper)g6Mapper);
  g6Actor.AddPosition((double)0,(double)15,(double)30);
  // create pipeline - rectilinear grid[]
  //[]
  rgridReader = new vtkRectilinearGridReader();
  rgridReader.SetFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/RectGrid2.vtk");
  rgridReader.Update();
  gf7 = new vtkGeometryFilter();
  gf7.SetInputConnection((vtkAlgorithmOutput)rgridReader.GetOutputPort());
  g7Mapper = vtkPolyDataMapper.New();
  g7Mapper.SetInputConnection((vtkAlgorithmOutput)gf7.GetOutputPort());
  g7Mapper.SetScalarRange((double)((vtkDataSet)rgridReader.GetOutput()).GetScalarRange()[0],(double)((vtkDataSet)rgridReader.GetOutput()).GetScalarRange()[1]);
  g7Actor = new vtkActor();
  g7Actor.SetMapper((vtkMapper)g7Mapper);
  g7Actor.SetScale((double)3,(double)3,(double)3);
  gf8 = new vtkGeometryFilter();
  gf8.SetInputConnection((vtkAlgorithmOutput)rgridReader.GetOutputPort());
  gf8.ExtentClippingOn();
  gf8.SetExtent((double)0,(double)1,(double)-2,(double)2,(double)0,(double)4);
  gf8.PointClippingOn();
  gf8.SetPointMinimum((int)0);
  gf8.SetPointMaximum((int)10000);
  gf8.CellClippingOn();
  gf8.SetCellMinimum((int)0);
  gf8.SetCellMaximum((int)7500);
  g8Mapper = vtkPolyDataMapper.New();
  g8Mapper.SetInputConnection((vtkAlgorithmOutput)gf8.GetOutputPort());
  g8Mapper.SetScalarRange((double)((vtkDataSet)rgridReader.GetOutput()).GetScalarRange()[0],(double)((vtkDataSet)rgridReader.GetOutput()).GetScalarRange()[1]);
  g8Actor = new vtkActor();
  g8Actor.SetMapper((vtkMapper)g8Mapper);
  g8Actor.SetScale((double)3,(double)3,(double)3);
  g8Actor.AddPosition((double)0,(double)15,(double)0);
  // Create the RenderWindow, Renderer and both Actors[]
  //[]
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  ren1.AddActor((vtkProp)gActor);
  ren1.AddActor((vtkProp)g2Actor);
  ren1.AddActor((vtkProp)g3Actor);
  ren1.AddActor((vtkProp)g4Actor);
  ren1.AddActor((vtkProp)g5Actor);
  ren1.AddActor((vtkProp)g6Actor);
  ren1.AddActor((vtkProp)g7Actor);
  ren1.AddActor((vtkProp)g8Actor);
  renWin.SetSize((int)340,(int)550);
  cam1 = ren1.GetActiveCamera();
  cam1.SetClippingRange((double)84,(double)174);
  cam1.SetFocalPoint((double)5.22824,(double)6.09412,(double)35.9813);
  cam1.SetPosition((double)100.052,(double)62.875,(double)102.818);
  cam1.SetViewUp((double)-0.307455,(double)-0.464269,(double)0.830617);
  iren.Initialize();
  // prevent the tk window from showing up then start the event loop[]
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkMultiBlockPLOT3DReader pl3d;
static vtkGeometryFilter gf;
static vtkPolyDataMapper gMapper;
static vtkActor gActor;
static vtkGeometryFilter gf2;
static vtkPolyDataMapper g2Mapper;
static vtkActor g2Actor;
static vtkGeometryFilter gf3;
static vtkPolyDataMapper g3Mapper;
static vtkActor g3Actor;
static vtkGeometryFilter gf4;
static vtkPolyDataMapper g4Mapper;
static vtkActor g4Actor;
static vtkSphere s;
static vtkExtractGeometry eg;
static vtkGeometryFilter gf5;
static vtkPolyDataMapper g5Mapper;
static vtkActor g5Actor;
static vtkGeometryFilter gf6;
static vtkPolyDataMapper g6Mapper;
static vtkActor g6Actor;
static vtkRectilinearGridReader rgridReader;
static vtkGeometryFilter gf7;
static vtkPolyDataMapper g7Mapper;
static vtkActor g7Actor;
static vtkGeometryFilter gf8;
static vtkPolyDataMapper g8Mapper;
static vtkActor g8Actor;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkCamera cam1;


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
        public static vtkGeometryFilter Getgf()
        {
            return gf;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setgf(vtkGeometryFilter toSet)
        {
            gf = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetgMapper()
        {
            return gMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetgMapper(vtkPolyDataMapper toSet)
        {
            gMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetgActor()
        {
            return gActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetgActor(vtkActor toSet)
        {
            gActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkGeometryFilter Getgf2()
        {
            return gf2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setgf2(vtkGeometryFilter toSet)
        {
            gf2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Getg2Mapper()
        {
            return g2Mapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setg2Mapper(vtkPolyDataMapper toSet)
        {
            g2Mapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getg2Actor()
        {
            return g2Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setg2Actor(vtkActor toSet)
        {
            g2Actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkGeometryFilter Getgf3()
        {
            return gf3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setgf3(vtkGeometryFilter toSet)
        {
            gf3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Getg3Mapper()
        {
            return g3Mapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setg3Mapper(vtkPolyDataMapper toSet)
        {
            g3Mapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getg3Actor()
        {
            return g3Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setg3Actor(vtkActor toSet)
        {
            g3Actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkGeometryFilter Getgf4()
        {
            return gf4;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setgf4(vtkGeometryFilter toSet)
        {
            gf4 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Getg4Mapper()
        {
            return g4Mapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setg4Mapper(vtkPolyDataMapper toSet)
        {
            g4Mapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getg4Actor()
        {
            return g4Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setg4Actor(vtkActor toSet)
        {
            g4Actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkSphere Gets()
        {
            return s;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sets(vtkSphere toSet)
        {
            s = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkExtractGeometry Geteg()
        {
            return eg;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Seteg(vtkExtractGeometry toSet)
        {
            eg = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkGeometryFilter Getgf5()
        {
            return gf5;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setgf5(vtkGeometryFilter toSet)
        {
            gf5 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Getg5Mapper()
        {
            return g5Mapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setg5Mapper(vtkPolyDataMapper toSet)
        {
            g5Mapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getg5Actor()
        {
            return g5Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setg5Actor(vtkActor toSet)
        {
            g5Actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkGeometryFilter Getgf6()
        {
            return gf6;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setgf6(vtkGeometryFilter toSet)
        {
            gf6 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Getg6Mapper()
        {
            return g6Mapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setg6Mapper(vtkPolyDataMapper toSet)
        {
            g6Mapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getg6Actor()
        {
            return g6Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setg6Actor(vtkActor toSet)
        {
            g6Actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkRectilinearGridReader GetrgridReader()
        {
            return rgridReader;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetrgridReader(vtkRectilinearGridReader toSet)
        {
            rgridReader = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkGeometryFilter Getgf7()
        {
            return gf7;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setgf7(vtkGeometryFilter toSet)
        {
            gf7 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Getg7Mapper()
        {
            return g7Mapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setg7Mapper(vtkPolyDataMapper toSet)
        {
            g7Mapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getg7Actor()
        {
            return g7Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setg7Actor(vtkActor toSet)
        {
            g7Actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkGeometryFilter Getgf8()
        {
            return gf8;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setgf8(vtkGeometryFilter toSet)
        {
            gf8 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Getg8Mapper()
        {
            return g8Mapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setg8Mapper(vtkPolyDataMapper toSet)
        {
            g8Mapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getg8Actor()
        {
            return g8Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setg8Actor(vtkActor toSet)
        {
            g8Actor = toSet;
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
        public static vtkCamera Getcam1()
        {
            return cam1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setcam1(vtkCamera toSet)
        {
            cam1 = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(pl3d!= null){pl3d.Dispose();}
  	if(gf!= null){gf.Dispose();}
  	if(gMapper!= null){gMapper.Dispose();}
  	if(gActor!= null){gActor.Dispose();}
  	if(gf2!= null){gf2.Dispose();}
  	if(g2Mapper!= null){g2Mapper.Dispose();}
  	if(g2Actor!= null){g2Actor.Dispose();}
  	if(gf3!= null){gf3.Dispose();}
  	if(g3Mapper!= null){g3Mapper.Dispose();}
  	if(g3Actor!= null){g3Actor.Dispose();}
  	if(gf4!= null){gf4.Dispose();}
  	if(g4Mapper!= null){g4Mapper.Dispose();}
  	if(g4Actor!= null){g4Actor.Dispose();}
  	if(s!= null){s.Dispose();}
  	if(eg!= null){eg.Dispose();}
  	if(gf5!= null){gf5.Dispose();}
  	if(g5Mapper!= null){g5Mapper.Dispose();}
  	if(g5Actor!= null){g5Actor.Dispose();}
  	if(gf6!= null){gf6.Dispose();}
  	if(g6Mapper!= null){g6Mapper.Dispose();}
  	if(g6Actor!= null){g6Actor.Dispose();}
  	if(rgridReader!= null){rgridReader.Dispose();}
  	if(gf7!= null){gf7.Dispose();}
  	if(g7Mapper!= null){g7Mapper.Dispose();}
  	if(g7Actor!= null){g7Actor.Dispose();}
  	if(gf8!= null){gf8.Dispose();}
  	if(g8Mapper!= null){g8Mapper.Dispose();}
  	if(g8Actor!= null){g8Actor.Dispose();}
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(cam1!= null){cam1.Dispose();}
  }

}
//--- end of script --//

