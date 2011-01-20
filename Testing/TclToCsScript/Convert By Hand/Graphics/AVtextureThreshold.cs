using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\textureThreshold.tcl
// output file is AVtextureThreshold.cs
/// <summary>
/// The testing class derived from AVtextureThreshold
/// </summary>
public class AVtextureThresholdClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVtextureThreshold(String [] argv)
  {
  //Prefix Content is: ""
  
  // Create the RenderWindow, Renderer and both Actors[]
  //[]
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  // read data[]
  //[]
  pl3d = new vtkPLOT3DReader();
  pl3d.SetXYZFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/bluntfinxyz.bin");
  pl3d.SetQFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/bluntfinq.bin");
  pl3d.SetScalarFunctionNumber((int)100);
  pl3d.SetVectorFunctionNumber((int)202);
  pl3d.Update();
  // wall[]
  //[]
  wall = new vtkStructuredGridGeometryFilter();
  wall.SetInputConnection((vtkAlgorithmOutput)pl3d.GetOutputPort());
  wall.SetExtent((int)0,(int)100,(int)0,(int)0,(int)0,(int)100);
  wallMap = vtkPolyDataMapper.New();
  wallMap.SetInputConnection((vtkAlgorithmOutput)wall.GetOutputPort());
  wallMap.ScalarVisibilityOff();
  wallActor = new vtkActor();
  wallActor.SetMapper((vtkMapper)wallMap);
  wallActor.GetProperty().SetColor((double)0.8,(double)0.8,(double)0.8);
  // fin[]
  // []
  fin = new vtkStructuredGridGeometryFilter();
  fin.SetInputConnection((vtkAlgorithmOutput)pl3d.GetOutputPort());
  fin.SetExtent((int)0,(int)100,(int)0,(int)100,(int)0,(int)0);
  finMap = vtkPolyDataMapper.New();
  finMap.SetInputConnection((vtkAlgorithmOutput)fin.GetOutputPort());
  finMap.ScalarVisibilityOff();
  finActor = new vtkActor();
  finActor.SetMapper((vtkMapper)finMap);
  finActor.GetProperty().SetColor((double)0.8,(double)0.8,(double)0.8);
  // planes to threshold[]
  tmap = new vtkStructuredPointsReader();
  tmap.SetFileName((string)"" + (VTK_DATA_ROOT.ToString()) + "/Data/texThres2.vtk");
  texture = new vtkTexture();
  texture.SetInputConnection((vtkAlgorithmOutput)tmap.GetOutputPort());
  texture.InterpolateOff();
  texture.RepeatOff();
  plane1 = new vtkStructuredGridGeometryFilter();
  plane1.SetInputConnection((vtkAlgorithmOutput)pl3d.GetOutputPort());
  plane1.SetExtent((int)10,(int)10,(int)0,(int)100,(int)0,(int)100);
  thresh1 = new vtkThresholdTextureCoords();
  thresh1.SetInputConnection((vtkAlgorithmOutput)plane1.GetOutputPort());
  thresh1.ThresholdByUpper((double)1.5);
  plane1Map = new vtkDataSetMapper();
  plane1Map.SetInputConnection((vtkAlgorithmOutput)thresh1.GetOutputPort());
  plane1Map.SetScalarRange((double)((vtkDataSet)pl3d.GetOutput()).GetScalarRange()[0],
      (double)((vtkDataSet)pl3d.GetOutput()).GetScalarRange()[1]);
  plane1Actor = new vtkActor();
  plane1Actor.SetMapper((vtkMapper)plane1Map);
  plane1Actor.SetTexture((vtkTexture)texture);
  plane1Actor.GetProperty().SetOpacity((double)0.999);
  plane2 = new vtkStructuredGridGeometryFilter();
  plane2.SetInputConnection((vtkAlgorithmOutput)pl3d.GetOutputPort());
  plane2.SetExtent((int)30,(int)30,(int)0,(int)100,(int)0,(int)100);
  thresh2 = new vtkThresholdTextureCoords();
  thresh2.SetInputConnection((vtkAlgorithmOutput)plane2.GetOutputPort());
  thresh2.ThresholdByLower((double)1.5);
  plane2Map = new vtkDataSetMapper();
  plane2Map.SetInputConnection((vtkAlgorithmOutput)thresh2.GetOutputPort());
  plane2Map.SetScalarRange((double)((vtkDataSet)pl3d.GetOutput()).GetScalarRange()[0],
      (double)((vtkDataSet)pl3d.GetOutput()).GetScalarRange()[1]);
  plane2Actor = new vtkActor();
  plane2Actor.SetMapper((vtkMapper)plane2Map);
  plane2Actor.SetTexture((vtkTexture)texture);
  plane2Actor.GetProperty().SetOpacity((double)0.999);
  plane3 = new vtkStructuredGridGeometryFilter();
  plane3.SetInputConnection((vtkAlgorithmOutput)pl3d.GetOutputPort());
  plane3.SetExtent((int)35,(int)35,(int)0,(int)100,(int)0,(int)100);
  thresh3 = new vtkThresholdTextureCoords();
  thresh3.SetInputConnection((vtkAlgorithmOutput)plane3.GetOutputPort());
  thresh3.ThresholdBetween((double)1.5,(double)1.8);
  plane3Map = new vtkDataSetMapper();
  plane3Map.SetInputConnection((vtkAlgorithmOutput)thresh3.GetOutputPort());
  plane3Map.SetScalarRange((double)((vtkDataSet)pl3d.GetOutput()).GetScalarRange()[0],
      (double)((vtkDataSet)pl3d.GetOutput()).GetScalarRange()[1]);
  plane3Actor = new vtkActor();
  plane3Actor.SetMapper((vtkMapper)plane3Map);
  plane3Actor.SetTexture((vtkTexture)texture);
  plane3Actor.GetProperty().SetOpacity((double)0.999);
  // outline[]
  outline = new vtkStructuredGridOutlineFilter();
  outline.SetInputConnection((vtkAlgorithmOutput)pl3d.GetOutputPort());
  outlineMapper = vtkPolyDataMapper.New();
  outlineMapper.SetInputConnection((vtkAlgorithmOutput)outline.GetOutputPort());
  outlineActor = new vtkActor();
  outlineActor.SetMapper((vtkMapper)outlineMapper);
  outlineProp = outlineActor.GetProperty();
  outlineProp.SetColor((double)0,(double)0,(double)0);
  // Add the actors to the renderer, set the background and size[]
  //[]
  ren1.AddActor((vtkProp)outlineActor);
  ren1.AddActor((vtkProp)wallActor);
  ren1.AddActor((vtkProp)finActor);
  ren1.AddActor((vtkProp)plane1Actor);
  ren1.AddActor((vtkProp)plane2Actor);
  ren1.AddActor((vtkProp)plane3Actor);
  ren1.SetBackground((double)1,(double)1,(double)1);
  renWin.SetSize((int)256,(int)256);
  cam1 = new vtkCamera();
  cam1.SetClippingRange((double)1.51176,(double)75.5879);
  cam1.SetFocalPoint((double)2.33749,(double)2.96739,(double)3.61023);
  cam1.SetPosition((double)10.8787,(double)5.27346,(double)15.8687);
  cam1.SetViewAngle((double)30);
  cam1.SetViewUp((double)-0.0610856,(double)0.987798,(double)-0.143262);
  ren1.SetActiveCamera((vtkCamera)cam1);
  iren.Initialize();
  // render the image[]
  //[]
  // prevent the tk window from showing up then start the event loop[]
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkPLOT3DReader pl3d;
static vtkStructuredGridGeometryFilter wall;
static vtkPolyDataMapper wallMap;
static vtkActor wallActor;
static vtkStructuredGridGeometryFilter fin;
static vtkPolyDataMapper finMap;
static vtkActor finActor;
static vtkStructuredPointsReader tmap;
static vtkTexture texture;
static vtkStructuredGridGeometryFilter plane1;
static vtkThresholdTextureCoords thresh1;
static vtkDataSetMapper plane1Map;
static vtkActor plane1Actor;
static vtkStructuredGridGeometryFilter plane2;
static vtkThresholdTextureCoords thresh2;
static vtkDataSetMapper plane2Map;
static vtkActor plane2Actor;
static vtkStructuredGridGeometryFilter plane3;
static vtkThresholdTextureCoords thresh3;
static vtkDataSetMapper plane3Map;
static vtkActor plane3Actor;
static vtkStructuredGridOutlineFilter outline;
static vtkPolyDataMapper outlineMapper;
static vtkActor outlineActor;
static vtkProperty outlineProp;
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
        public static vtkPLOT3DReader Getpl3d()
        {
            return pl3d;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpl3d(vtkPLOT3DReader toSet)
        {
            pl3d = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getwall()
        {
            return wall;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setwall(vtkStructuredGridGeometryFilter toSet)
        {
            wall = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetwallMap()
        {
            return wallMap;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetwallMap(vtkPolyDataMapper toSet)
        {
            wallMap = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetwallActor()
        {
            return wallActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetwallActor(vtkActor toSet)
        {
            wallActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getfin()
        {
            return fin;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setfin(vtkStructuredGridGeometryFilter toSet)
        {
            fin = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetfinMap()
        {
            return finMap;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetfinMap(vtkPolyDataMapper toSet)
        {
            finMap = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetfinActor()
        {
            return finActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetfinActor(vtkActor toSet)
        {
            finActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredPointsReader Gettmap()
        {
            return tmap;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settmap(vtkStructuredPointsReader toSet)
        {
            tmap = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTexture Gettexture()
        {
            return texture;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Settexture(vtkTexture toSet)
        {
            texture = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getplane1()
        {
            return plane1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setplane1(vtkStructuredGridGeometryFilter toSet)
        {
            plane1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkThresholdTextureCoords Getthresh1()
        {
            return thresh1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setthresh1(vtkThresholdTextureCoords toSet)
        {
            thresh1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper Getplane1Map()
        {
            return plane1Map;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setplane1Map(vtkDataSetMapper toSet)
        {
            plane1Map = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getplane1Actor()
        {
            return plane1Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setplane1Actor(vtkActor toSet)
        {
            plane1Actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getplane2()
        {
            return plane2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setplane2(vtkStructuredGridGeometryFilter toSet)
        {
            plane2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkThresholdTextureCoords Getthresh2()
        {
            return thresh2;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setthresh2(vtkThresholdTextureCoords toSet)
        {
            thresh2 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper Getplane2Map()
        {
            return plane2Map;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setplane2Map(vtkDataSetMapper toSet)
        {
            plane2Map = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getplane2Actor()
        {
            return plane2Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setplane2Actor(vtkActor toSet)
        {
            plane2Actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridGeometryFilter Getplane3()
        {
            return plane3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setplane3(vtkStructuredGridGeometryFilter toSet)
        {
            plane3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkThresholdTextureCoords Getthresh3()
        {
            return thresh3;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setthresh3(vtkThresholdTextureCoords toSet)
        {
            thresh3 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper Getplane3Map()
        {
            return plane3Map;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setplane3Map(vtkDataSetMapper toSet)
        {
            plane3Map = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getplane3Actor()
        {
            return plane3Actor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setplane3Actor(vtkActor toSet)
        {
            plane3Actor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkStructuredGridOutlineFilter Getoutline()
        {
            return outline;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setoutline(vtkStructuredGridOutlineFilter toSet)
        {
            outline = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetoutlineMapper()
        {
            return outlineMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetoutlineMapper(vtkPolyDataMapper toSet)
        {
            outlineMapper = toSet;
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
        public static vtkProperty GetoutlineProp()
        {
            return outlineProp;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetoutlineProp(vtkProperty toSet)
        {
            outlineProp = toSet;
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
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(pl3d!= null){pl3d.Dispose();}
  	if(wall!= null){wall.Dispose();}
  	if(wallMap!= null){wallMap.Dispose();}
  	if(wallActor!= null){wallActor.Dispose();}
  	if(fin!= null){fin.Dispose();}
  	if(finMap!= null){finMap.Dispose();}
  	if(finActor!= null){finActor.Dispose();}
  	if(tmap!= null){tmap.Dispose();}
  	if(texture!= null){texture.Dispose();}
  	if(plane1!= null){plane1.Dispose();}
  	if(thresh1!= null){thresh1.Dispose();}
  	if(plane1Map!= null){plane1Map.Dispose();}
  	if(plane1Actor!= null){plane1Actor.Dispose();}
  	if(plane2!= null){plane2.Dispose();}
  	if(thresh2!= null){thresh2.Dispose();}
  	if(plane2Map!= null){plane2Map.Dispose();}
  	if(plane2Actor!= null){plane2Actor.Dispose();}
  	if(plane3!= null){plane3.Dispose();}
  	if(thresh3!= null){thresh3.Dispose();}
  	if(plane3Map!= null){plane3Map.Dispose();}
  	if(plane3Actor!= null){plane3Actor.Dispose();}
  	if(outline!= null){outline.Dispose();}
  	if(outlineMapper!= null){outlineMapper.Dispose();}
  	if(outlineActor!= null){outlineActor.Dispose();}
  	if(outlineProp!= null){outlineProp.Dispose();}
  	if(cam1!= null){cam1.Dispose();}
  }

}
//--- end of script --//

