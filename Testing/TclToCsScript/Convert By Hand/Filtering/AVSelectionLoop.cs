using Kitware.VTK;
using System;
// input file is C:\VTK\Filtering\Testing\Tcl\SelectionLoop.tcl
// output file is AVSelectionLoop.cs
/// <summary>
/// The testing class derived from AVSelectionLoop
/// </summary>
public class AVSelectionLoopClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVSelectionLoop(String [] argv)
  {
  //Prefix Content is: ""
  
  //[]
  // Demonstrate the use of implicit selection loop as well as closest point[]
  // connectivity[]
  //[]
  // create pipeline[]
  //[]
  sphere = new vtkSphereSource();
  sphere.SetRadius((double)1);
  sphere.SetPhiResolution((int)100);
  sphere.SetThetaResolution((int)100);
  selectionPoints = new vtkPoints();
  selectionPoints.InsertPoint((int)0,(double)0.07325,(double)0.8417,(double)0.5612);
  selectionPoints.InsertPoint((int)1,(double)0.07244,(double)0.6568,(double)0.7450);
  selectionPoints.InsertPoint((int)2,(double)0.1727,(double)0.4597,(double)0.8850);
  selectionPoints.InsertPoint((int)3,(double)0.3265,(double)0.6054,(double)0.7309);
  selectionPoints.InsertPoint((int)4,(double)0.5722,(double)0.5848,(double)0.5927);
  selectionPoints.InsertPoint((int)5,(double)0.4305,(double)0.8138,(double)0.4189);
  loop = new vtkImplicitSelectionLoop();
  loop.SetLoop((vtkPoints)selectionPoints);
  extract = new vtkExtractGeometry();
  extract.SetInputConnection((vtkAlgorithmOutput)sphere.GetOutputPort());
  extract.SetImplicitFunction((vtkImplicitFunction)loop);
  connect = new vtkConnectivityFilter();
  connect.SetInputConnection((vtkAlgorithmOutput)extract.GetOutputPort());
  connect.SetExtractionModeToClosestPointRegion();
  connect.SetClosestPoint((double)selectionPoints.GetPoint((int)0)[0], (double)selectionPoints.GetPoint((int)0)[1],(double)selectionPoints.GetPoint((int)0)[2]);
  clipMapper = new vtkDataSetMapper();
  clipMapper.SetInputConnection((vtkAlgorithmOutput)connect.GetOutputPort());
  backProp = new vtkProperty();
  backProp.SetDiffuseColor((double) 1.0000, 0.3882, 0.2784 );
  clipActor = new vtkActor();
  clipActor.SetMapper((vtkMapper)clipMapper);
  clipActor.GetProperty().SetColor((double) 0.2000, 0.6300, 0.7900 );
  clipActor.SetBackfaceProperty((vtkProperty)backProp);
  // Create graphics stuff[]
  //[]
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  // Add the actors to the renderer, set the background and size[]
  //[]
  ren1.AddActor((vtkProp)clipActor);
  ren1.SetBackground((double)1,(double)1,(double)1);
  ren1.ResetCamera();
  ren1.GetActiveCamera().Azimuth((double)30);
  ren1.GetActiveCamera().Elevation((double)30);
  ren1.GetActiveCamera().Dolly((double)1.2);
  ren1.ResetCameraClippingRange();
  renWin.SetSize((int)400,(int)400);
  renWin.Render();
  // render the image[]
  //[]
  // prevent the tk window from showing up then start the event loop[]
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkSphereSource sphere;
static vtkPoints selectionPoints;
static vtkImplicitSelectionLoop loop;
static vtkExtractGeometry extract;
static vtkConnectivityFilter connect;
static vtkDataSetMapper clipMapper;
static vtkProperty backProp;
static vtkActor clipActor;
static vtkRenderer ren1;
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
        public static vtkPoints GetselectionPoints()
        {
            return selectionPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetselectionPoints(vtkPoints toSet)
        {
            selectionPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkImplicitSelectionLoop Getloop()
        {
            return loop;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setloop(vtkImplicitSelectionLoop toSet)
        {
            loop = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkExtractGeometry Getextract()
        {
            return extract;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setextract(vtkExtractGeometry toSet)
        {
            extract = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkConnectivityFilter Getconnect()
        {
            return connect;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setconnect(vtkConnectivityFilter toSet)
        {
            connect = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetclipMapper()
        {
            return clipMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetclipMapper(vtkDataSetMapper toSet)
        {
            clipMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkProperty GetbackProp()
        {
            return backProp;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetbackProp(vtkProperty toSet)
        {
            backProp = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetclipActor()
        {
            return clipActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetclipActor(vtkActor toSet)
        {
            clipActor = toSet;
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
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(sphere!= null){sphere.Dispose();}
  	if(selectionPoints!= null){selectionPoints.Dispose();}
  	if(loop!= null){loop.Dispose();}
  	if(extract!= null){extract.Dispose();}
  	if(connect!= null){connect.Dispose();}
  	if(clipMapper!= null){clipMapper.Dispose();}
  	if(backProp!= null){backProp.Dispose();}
  	if(clipActor!= null){clipActor.Dispose();}
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  }

}
//--- end of script --//

