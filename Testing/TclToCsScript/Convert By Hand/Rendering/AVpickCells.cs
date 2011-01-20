using Kitware.VTK;
using System;
// input file is C:\VTK\Rendering\Testing\Tcl\pickCells.tcl
// output file is AVpickCells.cs
/// <summary>
/// The testing class derived from AVpickCells
/// </summary>
public class AVpickCellsClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVpickCells(String [] argv)
  {
  //Prefix Content is: ""
  
  ren1 = vtkRenderer.New();
  renWin = vtkRenderWindow.New();
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  // create a scene with one of each cell type[]
  // Voxel[]
  voxelPoints = new vtkPoints();
  voxelPoints.SetNumberOfPoints((int)8);
  voxelPoints.InsertPoint((int)0,(double)0,(double)0,(double)0);
  voxelPoints.InsertPoint((int)1,(double)1,(double)0,(double)0);
  voxelPoints.InsertPoint((int)2,(double)0,(double)1,(double)0);
  voxelPoints.InsertPoint((int)3,(double)1,(double)1,(double)0);
  voxelPoints.InsertPoint((int)4,(double)0,(double)0,(double)1);
  voxelPoints.InsertPoint((int)5,(double)1,(double)0,(double)1);
  voxelPoints.InsertPoint((int)6,(double)0,(double)1,(double)1);
  voxelPoints.InsertPoint((int)7,(double)1,(double)1,(double)1);
  aVoxel = new vtkVoxel();
  aVoxel.GetPointIds().SetId((int)0,(int)0);
  aVoxel.GetPointIds().SetId((int)1,(int)1);
  aVoxel.GetPointIds().SetId((int)2,(int)2);
  aVoxel.GetPointIds().SetId((int)3,(int)3);
  aVoxel.GetPointIds().SetId((int)4,(int)4);
  aVoxel.GetPointIds().SetId((int)5,(int)5);
  aVoxel.GetPointIds().SetId((int)6,(int)6);
  aVoxel.GetPointIds().SetId((int)7,(int)7);
  aVoxelGrid = new vtkUnstructuredGrid();
  aVoxelGrid.Allocate((int)1,(int)1);
  aVoxelGrid.InsertNextCell((int)aVoxel.GetCellType(),(vtkIdList)aVoxel.GetPointIds());
  aVoxelGrid.SetPoints((vtkPoints)voxelPoints);
  aVoxelMapper = new vtkDataSetMapper();
  aVoxelMapper.SetInput((vtkDataSet)aVoxelGrid);
  aVoxelActor = new vtkActor();
  aVoxelActor.SetMapper((vtkMapper)aVoxelMapper);
  aVoxelActor.GetProperty().BackfaceCullingOn();
  // Hexahedron[]
  hexahedronPoints = new vtkPoints();
  hexahedronPoints.SetNumberOfPoints((int)8);
  hexahedronPoints.InsertPoint((int)0,(double)0,(double)0,(double)0);
  hexahedronPoints.InsertPoint((int)1,(double)1,(double)0,(double)0);
  hexahedronPoints.InsertPoint((int)2,(double)1,(double)1,(double)0);
  hexahedronPoints.InsertPoint((int)3,(double)0,(double)1,(double)0);
  hexahedronPoints.InsertPoint((int)4,(double)0,(double)0,(double)1);
  hexahedronPoints.InsertPoint((int)5,(double)1,(double)0,(double)1);
  hexahedronPoints.InsertPoint((int)6,(double)1,(double)1,(double)1);
  hexahedronPoints.InsertPoint((int)7,(double)0,(double)1,(double)1);
  aHexahedron = new vtkHexahedron();
  aHexahedron.GetPointIds().SetId((int)0,(int)0);
  aHexahedron.GetPointIds().SetId((int)1,(int)1);
  aHexahedron.GetPointIds().SetId((int)2,(int)2);
  aHexahedron.GetPointIds().SetId((int)3,(int)3);
  aHexahedron.GetPointIds().SetId((int)4,(int)4);
  aHexahedron.GetPointIds().SetId((int)5,(int)5);
  aHexahedron.GetPointIds().SetId((int)6,(int)6);
  aHexahedron.GetPointIds().SetId((int)7,(int)7);
  aHexahedronGrid = new vtkUnstructuredGrid();
  aHexahedronGrid.Allocate((int)1,(int)1);
  aHexahedronGrid.InsertNextCell((int)aHexahedron.GetCellType(),(vtkIdList)aHexahedron.GetPointIds());
  aHexahedronGrid.SetPoints((vtkPoints)hexahedronPoints);
  aHexahedronMapper = new vtkDataSetMapper();
  aHexahedronMapper.SetInput((vtkDataSet)aHexahedronGrid);
  aHexahedronActor = new vtkActor();
  aHexahedronActor.SetMapper((vtkMapper)aHexahedronMapper);
  aHexahedronActor.AddPosition((double)2,(double)0,(double)0);
  aHexahedronActor.GetProperty().BackfaceCullingOn();
  // Tetra[]
  tetraPoints = new vtkPoints();
  tetraPoints.SetNumberOfPoints((int)4);
  tetraPoints.InsertPoint((int)0,(double)0,(double)0,(double)0);
  tetraPoints.InsertPoint((int)1,(double)1,(double)0,(double)0);
  tetraPoints.InsertPoint((int)2,(double).5,(double)1,(double)0);
  tetraPoints.InsertPoint((int)3,(double).5,(double).5,(double)1);
  aTetra = new vtkTetra();
  aTetra.GetPointIds().SetId((int)0,(int)0);
  aTetra.GetPointIds().SetId((int)1,(int)1);
  aTetra.GetPointIds().SetId((int)2,(int)2);
  aTetra.GetPointIds().SetId((int)3,(int)3);
  aTetraGrid = new vtkUnstructuredGrid();
  aTetraGrid.Allocate((int)1,(int)1);
  aTetraGrid.InsertNextCell((int)aTetra.GetCellType(),(vtkIdList)aTetra.GetPointIds());
  aTetraGrid.SetPoints((vtkPoints)tetraPoints);
  aTetraMapper = new vtkDataSetMapper();
  aTetraMapper.SetInput((vtkDataSet)aTetraGrid);
  aTetraActor = new vtkActor();
  aTetraActor.SetMapper((vtkMapper)aTetraMapper);
  aTetraActor.AddPosition((double)4,(double)0,(double)0);
  aTetraActor.GetProperty().BackfaceCullingOn();
  // Wedge[]
  wedgePoints = new vtkPoints();
  wedgePoints.SetNumberOfPoints((int)6);
  wedgePoints.InsertPoint((int)0,(double)0,(double)1,(double)0);
  wedgePoints.InsertPoint((int)1,(double)0,(double)0,(double)0);
  wedgePoints.InsertPoint((int)2,(double)0,(double).5,(double).5);
  wedgePoints.InsertPoint((int)3,(double)1,(double)1,(double)0);
  wedgePoints.InsertPoint((int)4,(double)1,(double)0,(double)0);
  wedgePoints.InsertPoint((int)5,(double)1,(double).5,(double).5);
  aWedge = new vtkWedge();
  aWedge.GetPointIds().SetId((int)0,(int)0);
  aWedge.GetPointIds().SetId((int)1,(int)1);
  aWedge.GetPointIds().SetId((int)2,(int)2);
  aWedge.GetPointIds().SetId((int)3,(int)3);
  aWedge.GetPointIds().SetId((int)4,(int)4);
  aWedge.GetPointIds().SetId((int)5,(int)5);
  aWedgeGrid = new vtkUnstructuredGrid();
  aWedgeGrid.Allocate((int)1,(int)1);
  aWedgeGrid.InsertNextCell((int)aWedge.GetCellType(),(vtkIdList)aWedge.GetPointIds());
  aWedgeGrid.SetPoints((vtkPoints)wedgePoints);
  aWedgeMapper = new vtkDataSetMapper();
  aWedgeMapper.SetInput((vtkDataSet)aWedgeGrid);
  aWedgeActor = new vtkActor();
  aWedgeActor.SetMapper((vtkMapper)aWedgeMapper);
  aWedgeActor.AddPosition((double)6,(double)0,(double)0);
  aWedgeActor.GetProperty().BackfaceCullingOn();
  // Pyramid[]
  pyramidPoints = new vtkPoints();
  pyramidPoints.SetNumberOfPoints((int)5);
  pyramidPoints.InsertPoint((int)0,(double)0,(double)0,(double)0);
  pyramidPoints.InsertPoint((int)1,(double)1,(double)0,(double)0);
  pyramidPoints.InsertPoint((int)2,(double)1,(double)1,(double)0);
  pyramidPoints.InsertPoint((int)3,(double)0,(double)1,(double)0);
  pyramidPoints.InsertPoint((int)4,(double).5,(double).5,(double)1);
  aPyramid = new vtkPyramid();
  aPyramid.GetPointIds().SetId((int)0,(int)0);
  aPyramid.GetPointIds().SetId((int)1,(int)1);
  aPyramid.GetPointIds().SetId((int)2,(int)2);
  aPyramid.GetPointIds().SetId((int)3,(int)3);
  aPyramid.GetPointIds().SetId((int)4,(int)4);
  aPyramidGrid = new vtkUnstructuredGrid();
  aPyramidGrid.Allocate((int)1,(int)1);
  aPyramidGrid.InsertNextCell((int)aPyramid.GetCellType(),(vtkIdList)aPyramid.GetPointIds());
  aPyramidGrid.SetPoints((vtkPoints)pyramidPoints);
  aPyramidMapper = new vtkDataSetMapper();
  aPyramidMapper.SetInput((vtkDataSet)aPyramidGrid);
  aPyramidActor = new vtkActor();
  aPyramidActor.SetMapper((vtkMapper)aPyramidMapper);
  aPyramidActor.AddPosition((double)8,(double)0,(double)0);
  aPyramidActor.GetProperty().BackfaceCullingOn();
  // Pixel[]
  pixelPoints = new vtkPoints();
  pixelPoints.SetNumberOfPoints((int)4);
  pixelPoints.InsertPoint((int)0,(double)0,(double)0,(double)0);
  pixelPoints.InsertPoint((int)1,(double)1,(double)0,(double)0);
  pixelPoints.InsertPoint((int)2,(double)0,(double)1,(double)0);
  pixelPoints.InsertPoint((int)3,(double)1,(double)1,(double)0);
  aPixel = new vtkPixel();
  aPixel.GetPointIds().SetId((int)0,(int)0);
  aPixel.GetPointIds().SetId((int)1,(int)1);
  aPixel.GetPointIds().SetId((int)2,(int)2);
  aPixel.GetPointIds().SetId((int)3,(int)3);
  aPixelGrid = new vtkUnstructuredGrid();
  aPixelGrid.Allocate((int)1,(int)1);
  aPixelGrid.InsertNextCell((int)aPixel.GetCellType(),(vtkIdList)aPixel.GetPointIds());
  aPixelGrid.SetPoints((vtkPoints)pixelPoints);
  aPixelMapper = new vtkDataSetMapper();
  aPixelMapper.SetInput((vtkDataSet)aPixelGrid);
  aPixelActor = new vtkActor();
  aPixelActor.SetMapper((vtkMapper)aPixelMapper);
  aPixelActor.AddPosition((double)0,(double)0,(double)2);
  aPixelActor.GetProperty().BackfaceCullingOn();
  // Quad[]
  quadPoints = new vtkPoints();
  quadPoints.SetNumberOfPoints((int)4);
  quadPoints.InsertPoint((int)0,(double)0,(double)0,(double)0);
  quadPoints.InsertPoint((int)1,(double)1,(double)0,(double)0);
  quadPoints.InsertPoint((int)2,(double)1,(double)1,(double)0);
  quadPoints.InsertPoint((int)3,(double)0,(double)1,(double)0);
  aQuad = new vtkQuad();
  aQuad.GetPointIds().SetId((int)0,(int)0);
  aQuad.GetPointIds().SetId((int)1,(int)1);
  aQuad.GetPointIds().SetId((int)2,(int)2);
  aQuad.GetPointIds().SetId((int)3,(int)3);
  aQuadGrid = new vtkUnstructuredGrid();
  aQuadGrid.Allocate((int)1,(int)1);
  aQuadGrid.InsertNextCell((int)aQuad.GetCellType(),(vtkIdList)aQuad.GetPointIds());
  aQuadGrid.SetPoints((vtkPoints)quadPoints);
  aQuadMapper = new vtkDataSetMapper();
  aQuadMapper.SetInput((vtkDataSet)aQuadGrid);
  aQuadActor = new vtkActor();
  aQuadActor.SetMapper((vtkMapper)aQuadMapper);
  aQuadActor.AddPosition((double)2,(double)0,(double)2);
  aQuadActor.GetProperty().BackfaceCullingOn();
  // Triangle[]
  trianglePoints = new vtkPoints();
  trianglePoints.SetNumberOfPoints((int)3);
  trianglePoints.InsertPoint((int)0,(double)0,(double)0,(double)0);
  trianglePoints.InsertPoint((int)1,(double)1,(double)0,(double)0);
  trianglePoints.InsertPoint((int)2,(double).5,(double).5,(double)0);
  aTriangle = new vtkTriangle();
  aTriangle.GetPointIds().SetId((int)0,(int)0);
  aTriangle.GetPointIds().SetId((int)1,(int)1);
  aTriangle.GetPointIds().SetId((int)2,(int)2);
  aTriangleGrid = new vtkUnstructuredGrid();
  aTriangleGrid.Allocate((int)1,(int)1);
  aTriangleGrid.InsertNextCell((int)aTriangle.GetCellType(),(vtkIdList)aTriangle.GetPointIds());
  aTriangleGrid.SetPoints((vtkPoints)trianglePoints);
  aTriangleMapper = new vtkDataSetMapper();
  aTriangleMapper.SetInput((vtkDataSet)aTriangleGrid);
  aTriangleActor = new vtkActor();
  aTriangleActor.SetMapper((vtkMapper)aTriangleMapper);
  aTriangleActor.AddPosition((double)4,(double)0,(double)2);
  aTriangleActor.GetProperty().BackfaceCullingOn();
  // Polygon[]
  polygonPoints = new vtkPoints();
  polygonPoints.SetNumberOfPoints((int)4);
  polygonPoints.InsertPoint((int)0,(double)0,(double)0,(double)0);
  polygonPoints.InsertPoint((int)1,(double)1,(double)0,(double)0);
  polygonPoints.InsertPoint((int)2,(double)1,(double)1,(double)0);
  polygonPoints.InsertPoint((int)3,(double)0,(double)1,(double)0);
  aPolygon = new vtkPolygon();
  aPolygon.GetPointIds().SetNumberOfIds((int)4);
  aPolygon.GetPointIds().SetId((int)0,(int)0);
  aPolygon.GetPointIds().SetId((int)1,(int)1);
  aPolygon.GetPointIds().SetId((int)2,(int)2);
  aPolygon.GetPointIds().SetId((int)3,(int)3);
  aPolygonGrid = new vtkUnstructuredGrid();
  aPolygonGrid.Allocate((int)1,(int)1);
  aPolygonGrid.InsertNextCell((int)aPolygon.GetCellType(),(vtkIdList)aPolygon.GetPointIds());
  aPolygonGrid.SetPoints((vtkPoints)polygonPoints);
  aPolygonMapper = new vtkDataSetMapper();
  aPolygonMapper.SetInput((vtkDataSet)aPolygonGrid);
  aPolygonActor = new vtkActor();
  aPolygonActor.SetMapper((vtkMapper)aPolygonMapper);
  aPolygonActor.AddPosition((double)6,(double)0,(double)2);
  aPolygonActor.GetProperty().BackfaceCullingOn();
  // Triangle Strip[]
  triangleStripPoints = new vtkPoints();
  triangleStripPoints.SetNumberOfPoints((int)5);
  triangleStripPoints.InsertPoint((int)0,(double)0,(double)1,(double)0);
  triangleStripPoints.InsertPoint((int)1,(double)0,(double)0,(double)0);
  triangleStripPoints.InsertPoint((int)2,(double)1,(double)1,(double)0);
  triangleStripPoints.InsertPoint((int)3,(double)1,(double)0,(double)0);
  triangleStripPoints.InsertPoint((int)4,(double)2,(double)1,(double)0);
  aTriangleStrip = new vtkTriangleStrip();
  aTriangleStrip.GetPointIds().SetNumberOfIds((int)5);
  aTriangleStrip.GetPointIds().SetId((int)0,(int)0);
  aTriangleStrip.GetPointIds().SetId((int)1,(int)1);
  aTriangleStrip.GetPointIds().SetId((int)2,(int)2);
  aTriangleStrip.GetPointIds().SetId((int)3,(int)3);
  aTriangleStrip.GetPointIds().SetId((int)4,(int)4);
  aTriangleStripGrid = new vtkUnstructuredGrid();
  aTriangleStripGrid.Allocate((int)1,(int)1);
  aTriangleStripGrid.InsertNextCell((int)aTriangleStrip.GetCellType(),(vtkIdList)aTriangleStrip.GetPointIds());
  aTriangleStripGrid.SetPoints((vtkPoints)triangleStripPoints);
  aTriangleStripMapper = new vtkDataSetMapper();
  aTriangleStripMapper.SetInput((vtkDataSet)aTriangleStripGrid);
  aTriangleStripActor = new vtkActor();
  aTriangleStripActor.SetMapper((vtkMapper)aTriangleStripMapper);
  aTriangleStripActor.AddPosition((double)8,(double)0,(double)2);
  aTriangleStripActor.GetProperty().BackfaceCullingOn();
  // Line[]
  linePoints = new vtkPoints();
  linePoints.SetNumberOfPoints((int)2);
  linePoints.InsertPoint((int)0,(double)0,(double)0,(double)0);
  linePoints.InsertPoint((int)1,(double)1,(double)1,(double)0);
  aLine = new vtkLine();
  aLine.GetPointIds().SetId((int)0,(int)0);
  aLine.GetPointIds().SetId((int)1,(int)1);
  aLineGrid = new vtkUnstructuredGrid();
  aLineGrid.Allocate((int)1,(int)1);
  aLineGrid.InsertNextCell((int)aLine.GetCellType(),(vtkIdList)aLine.GetPointIds());
  aLineGrid.SetPoints((vtkPoints)linePoints);
  aLineMapper = new vtkDataSetMapper();
  aLineMapper.SetInput((vtkDataSet)aLineGrid);
  aLineActor = new vtkActor();
  aLineActor.SetMapper((vtkMapper)aLineMapper);
  aLineActor.AddPosition((double)0,(double)0,(double)4);
  aLineActor.GetProperty().BackfaceCullingOn();
  // Poly line[]
  polyLinePoints = new vtkPoints();
  polyLinePoints.SetNumberOfPoints((int)3);
  polyLinePoints.InsertPoint((int)0,(double)0,(double)0,(double)0);
  polyLinePoints.InsertPoint((int)1,(double)1,(double)1,(double)0);
  polyLinePoints.InsertPoint((int)2,(double)1,(double)0,(double)0);
  aPolyLine = new vtkPolyLine();
  aPolyLine.GetPointIds().SetNumberOfIds((int)3);
  aPolyLine.GetPointIds().SetId((int)0,(int)0);
  aPolyLine.GetPointIds().SetId((int)1,(int)1);
  aPolyLine.GetPointIds().SetId((int)2,(int)2);
  aPolyLineGrid = new vtkUnstructuredGrid();
  aPolyLineGrid.Allocate((int)1,(int)1);
  aPolyLineGrid.InsertNextCell((int)aPolyLine.GetCellType(),(vtkIdList)aPolyLine.GetPointIds());
  aPolyLineGrid.SetPoints((vtkPoints)polyLinePoints);
  aPolyLineMapper = new vtkDataSetMapper();
  aPolyLineMapper.SetInput((vtkDataSet)aPolyLineGrid);
  aPolyLineActor = new vtkActor();
  aPolyLineActor.SetMapper((vtkMapper)aPolyLineMapper);
  aPolyLineActor.AddPosition((double)2,(double)0,(double)4);
  aPolyLineActor.GetProperty().BackfaceCullingOn();
  // Vertex[]
  vertexPoints = new vtkPoints();
  vertexPoints.SetNumberOfPoints((int)1);
  vertexPoints.InsertPoint((int)0,(double)0,(double)0,(double)0);
  aVertex = new vtkVertex();
  aVertex.GetPointIds().SetId((int)0,(int)0);
  aVertexGrid = new vtkUnstructuredGrid();
  aVertexGrid.Allocate((int)1,(int)1);
  aVertexGrid.InsertNextCell((int)aVertex.GetCellType(),(vtkIdList)aVertex.GetPointIds());
  aVertexGrid.SetPoints((vtkPoints)vertexPoints);
  aVertexMapper = new vtkDataSetMapper();
  aVertexMapper.SetInput((vtkDataSet)aVertexGrid);
  aVertexActor = new vtkActor();
  aVertexActor.SetMapper((vtkMapper)aVertexMapper);
  aVertexActor.AddPosition((double)0,(double)0,(double)6);
  aVertexActor.GetProperty().BackfaceCullingOn();
  // Poly Vertex[]
  polyVertexPoints = new vtkPoints();
  polyVertexPoints.SetNumberOfPoints((int)3);
  polyVertexPoints.InsertPoint((int)0,(double)0,(double)0,(double)0);
  polyVertexPoints.InsertPoint((int)1,(double)1,(double)0,(double)0);
  polyVertexPoints.InsertPoint((int)2,(double)1,(double)1,(double)0);
  aPolyVertex = new vtkPolyVertex();
  aPolyVertex.GetPointIds().SetNumberOfIds((int)3);
  aPolyVertex.GetPointIds().SetId((int)0,(int)0);
  aPolyVertex.GetPointIds().SetId((int)1,(int)1);
  aPolyVertex.GetPointIds().SetId((int)2,(int)2);
  aPolyVertexGrid = new vtkUnstructuredGrid();
  aPolyVertexGrid.Allocate((int)1,(int)1);
  aPolyVertexGrid.InsertNextCell((int)aPolyVertex.GetCellType(),(vtkIdList)aPolyVertex.GetPointIds());
  aPolyVertexGrid.SetPoints((vtkPoints)polyVertexPoints);
  aPolyVertexMapper = new vtkDataSetMapper();
  aPolyVertexMapper.SetInput((vtkDataSet)aPolyVertexGrid);
  aPolyVertexActor = new vtkActor();
  aPolyVertexActor.SetMapper((vtkMapper)aPolyVertexMapper);
  aPolyVertexActor.AddPosition((double)2,(double)0,(double)6);
  aPolyVertexActor.GetProperty().BackfaceCullingOn();
  // Pentagonal prism[]
  pentaPoints = new vtkPoints();
  pentaPoints.SetNumberOfPoints((int)10);
  pentaPoints.InsertPoint((int)0,(double)0.25,(double)0.0,(double)0.0);
  pentaPoints.InsertPoint((int)1,(double)0.75,(double)0.0,(double)0.0);
  pentaPoints.InsertPoint((int)2,(double)1.0,(double)0.5,(double)0.0);
  pentaPoints.InsertPoint((int)3,(double)0.5,(double)1.0,(double)0.0);
  pentaPoints.InsertPoint((int)4,(double)0.0,(double)0.5,(double)0.0);
  pentaPoints.InsertPoint((int)5,(double)0.25,(double)0.0,(double)1.0);
  pentaPoints.InsertPoint((int)6,(double)0.75,(double)0.0,(double)1.0);
  pentaPoints.InsertPoint((int)7,(double)1.0,(double)0.5,(double)1.0);
  pentaPoints.InsertPoint((int)8,(double)0.5,(double)1.0,(double)1.0);
  pentaPoints.InsertPoint((int)9,(double)0.0,(double)0.5,(double)1.0);
  aPenta = new vtkPentagonalPrism();
  aPenta.GetPointIds().SetId((int)0,(int)0);
  aPenta.GetPointIds().SetId((int)1,(int)1);
  aPenta.GetPointIds().SetId((int)2,(int)2);
  aPenta.GetPointIds().SetId((int)3,(int)3);
  aPenta.GetPointIds().SetId((int)4,(int)4);
  aPenta.GetPointIds().SetId((int)5,(int)5);
  aPenta.GetPointIds().SetId((int)6,(int)6);
  aPenta.GetPointIds().SetId((int)7,(int)7);
  aPenta.GetPointIds().SetId((int)8,(int)8);
  aPenta.GetPointIds().SetId((int)9,(int)9);
  aPentaGrid = new vtkUnstructuredGrid();
  aPentaGrid.Allocate((int)1,(int)1);
  aPentaGrid.InsertNextCell((int)aPenta.GetCellType(),(vtkIdList)aPenta.GetPointIds());
  aPentaGrid.SetPoints((vtkPoints)pentaPoints);
  aPentaMapper = new vtkDataSetMapper();
  aPentaMapper.SetInput((vtkDataSet)aPentaGrid);
  aPentaActor = new vtkActor();
  aPentaActor.SetMapper((vtkMapper)aPentaMapper);
  aPentaActor.AddPosition((double)10,(double)0,(double)0);
  aPentaActor.GetProperty().BackfaceCullingOn();
  // Hexagonal prism[]
  hexaPoints = new vtkPoints();
  hexaPoints.SetNumberOfPoints((int)12);
  hexaPoints.InsertPoint((int)0,(double)0.0,(double)0.0,(double)0.0);
  hexaPoints.InsertPoint((int)1,(double)0.5,(double)0.0,(double)0.0);
  hexaPoints.InsertPoint((int)2,(double)1.0,(double)0.5,(double)0.0);
  hexaPoints.InsertPoint((int)3,(double)1.0,(double)1.0,(double)0.0);
  hexaPoints.InsertPoint((int)4,(double)0.5,(double)1.0,(double)0.0);
  hexaPoints.InsertPoint((int)5,(double)0.0,(double)0.5,(double)0.0);
  hexaPoints.InsertPoint((int)6,(double)0.0,(double)0.0,(double)1.0);
  hexaPoints.InsertPoint((int)7,(double)0.5,(double)0.0,(double)1.0);
  hexaPoints.InsertPoint((int)8,(double)1.0,(double)0.5,(double)1.0);
  hexaPoints.InsertPoint((int)9,(double)1.0,(double)1.0,(double)1.0);
  hexaPoints.InsertPoint((int)10,(double)0.5,(double)1.0,(double)1.0);
  hexaPoints.InsertPoint((int)11,(double)0.0,(double)0.5,(double)1.0);
  aHexa = new vtkHexagonalPrism();
  aHexa.GetPointIds().SetId((int)0,(int)0);
  aHexa.GetPointIds().SetId((int)1,(int)1);
  aHexa.GetPointIds().SetId((int)2,(int)2);
  aHexa.GetPointIds().SetId((int)3,(int)3);
  aHexa.GetPointIds().SetId((int)4,(int)4);
  aHexa.GetPointIds().SetId((int)5,(int)5);
  aHexa.GetPointIds().SetId((int)6,(int)6);
  aHexa.GetPointIds().SetId((int)7,(int)7);
  aHexa.GetPointIds().SetId((int)8,(int)8);
  aHexa.GetPointIds().SetId((int)9,(int)9);
  aHexa.GetPointIds().SetId((int)10,(int)10);
  aHexa.GetPointIds().SetId((int)11,(int)11);
  aHexaGrid = new vtkUnstructuredGrid();
  aHexaGrid.Allocate((int)1,(int)1);
  aHexaGrid.InsertNextCell((int)aHexa.GetCellType(),(vtkIdList)aHexa.GetPointIds());
  aHexaGrid.SetPoints((vtkPoints)hexaPoints);
  aHexaMapper = new vtkDataSetMapper();
  aHexaMapper.SetInput((vtkDataSet)aHexaGrid);
  aHexaActor = new vtkActor();
  aHexaActor.SetMapper((vtkMapper)aHexaMapper);
  aHexaActor.AddPosition((double)12,(double)0,(double)0);
  aHexaActor.GetProperty().BackfaceCullingOn();
  ren1.SetBackground((double).1,(double).2,(double).4);
  ren1.AddActor((vtkProp)aVoxelActor);
  aVoxelActor.GetProperty().SetDiffuseColor((double)1,(double)0,(double)0);
  ren1.AddActor((vtkProp)aHexahedronActor);
  aHexahedronActor.GetProperty().SetDiffuseColor((double)1,(double)1,(double)0);
  ren1.AddActor((vtkProp)aTetraActor);
  aTetraActor.GetProperty().SetDiffuseColor((double)0,(double)1,(double)0);
  ren1.AddActor((vtkProp)aWedgeActor);
  aWedgeActor.GetProperty().SetDiffuseColor((double)0,(double)1,(double)1);
  ren1.AddActor((vtkProp)aPyramidActor);
  aPyramidActor.GetProperty().SetDiffuseColor((double)1,(double)0,(double)1);
  ren1.AddActor((vtkProp)aPixelActor);
  aPixelActor.GetProperty().SetDiffuseColor((double)0,(double)1,(double)1);
  ren1.AddActor((vtkProp)aQuadActor);
  aQuadActor.GetProperty().SetDiffuseColor((double)1,(double)0,(double)1);
  ren1.AddActor((vtkProp)aTriangleActor);
  aTriangleActor.GetProperty().SetDiffuseColor((double).3,(double)1,(double).5);
  ren1.AddActor((vtkProp)aPolygonActor);
  aPolygonActor.GetProperty().SetDiffuseColor((double)1,(double).4,(double).5);
  ren1.AddActor((vtkProp)aTriangleStripActor);
  aTriangleStripActor.GetProperty().SetDiffuseColor((double).3,(double).7,(double)1);
  ren1.AddActor((vtkProp)aLineActor);
  aLineActor.GetProperty().SetDiffuseColor((double).2,(double)1,(double)1);
  ren1.AddActor((vtkProp)aPolyLineActor);
  aPolyLineActor.GetProperty().SetDiffuseColor((double)1,(double)1,(double)1);
  ren1.AddActor((vtkProp)aVertexActor);
  aVertexActor.GetProperty().SetDiffuseColor((double)1,(double)1,(double)1);
  ren1.AddActor((vtkProp)aPolyVertexActor);
  aPolyVertexActor.GetProperty().SetDiffuseColor((double)1,(double)1,(double)1);
  ren1.AddActor((vtkProp)aPentaActor);
  aPentaActor.GetProperty().SetDiffuseColor((double).2,(double).4,(double).7);
  ren1.AddActor((vtkProp)aHexaActor);
  aHexaActor.GetProperty().SetDiffuseColor((double).7,(double).5,(double)1);
  ren1.ResetCamera();
  ren1.GetActiveCamera().Azimuth((double)30);
  ren1.GetActiveCamera().Elevation((double)20);
  ren1.GetActiveCamera().Dolly((double)1.25);
  ren1.ResetCameraClippingRange();
  renWin.Render();
  cellPicker = new vtkCellPicker();
  pointPicker = new vtkPointPicker();
  worldPicker = new vtkWorldPointPicker();
  cellCount = 0;
  pointCount = 0;
  ren1.IsInViewport((int)0,(int)0);
  x = 0;
  while((x) <= 265)
    {
      y = 100;
      while((y) <= 200)
        {
          cellPicker.Pick((double)x,(double)y,(double)0,(vtkRenderer)ren1);
          pointPicker.Pick((double)x,(double)y,(double)0,(vtkRenderer)ren1);
          worldPicker.Pick((double)x,(double)y,(double)0,(vtkRenderer)ren1);
          if ((cellPicker.GetCellId()) != -1)
            {
              cellCount = cellCount + 1;
            }

          
          if ((pointPicker.GetPointId()) != -1)
            {
              pointCount = pointCount + 1;
            }

          
          y = y + 6;
        }

      x = x + 6;
    }

  // render the image[]
  //[]
  iren.Initialize();
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkPoints voxelPoints;
static vtkVoxel aVoxel;
static vtkUnstructuredGrid aVoxelGrid;
static vtkDataSetMapper aVoxelMapper;
static vtkActor aVoxelActor;
static vtkPoints hexahedronPoints;
static vtkHexahedron aHexahedron;
static vtkUnstructuredGrid aHexahedronGrid;
static vtkDataSetMapper aHexahedronMapper;
static vtkActor aHexahedronActor;
static vtkPoints tetraPoints;
static vtkTetra aTetra;
static vtkUnstructuredGrid aTetraGrid;
static vtkDataSetMapper aTetraMapper;
static vtkActor aTetraActor;
static vtkPoints wedgePoints;
static vtkWedge aWedge;
static vtkUnstructuredGrid aWedgeGrid;
static vtkDataSetMapper aWedgeMapper;
static vtkActor aWedgeActor;
static vtkPoints pyramidPoints;
static vtkPyramid aPyramid;
static vtkUnstructuredGrid aPyramidGrid;
static vtkDataSetMapper aPyramidMapper;
static vtkActor aPyramidActor;
static vtkPoints pixelPoints;
static vtkPixel aPixel;
static vtkUnstructuredGrid aPixelGrid;
static vtkDataSetMapper aPixelMapper;
static vtkActor aPixelActor;
static vtkPoints quadPoints;
static vtkQuad aQuad;
static vtkUnstructuredGrid aQuadGrid;
static vtkDataSetMapper aQuadMapper;
static vtkActor aQuadActor;
static vtkPoints trianglePoints;
static vtkTriangle aTriangle;
static vtkUnstructuredGrid aTriangleGrid;
static vtkDataSetMapper aTriangleMapper;
static vtkActor aTriangleActor;
static vtkPoints polygonPoints;
static vtkPolygon aPolygon;
static vtkUnstructuredGrid aPolygonGrid;
static vtkDataSetMapper aPolygonMapper;
static vtkActor aPolygonActor;
static vtkPoints triangleStripPoints;
static vtkTriangleStrip aTriangleStrip;
static vtkUnstructuredGrid aTriangleStripGrid;
static vtkDataSetMapper aTriangleStripMapper;
static vtkActor aTriangleStripActor;
static vtkPoints linePoints;
static vtkLine aLine;
static vtkUnstructuredGrid aLineGrid;
static vtkDataSetMapper aLineMapper;
static vtkActor aLineActor;
static vtkPoints polyLinePoints;
static vtkPolyLine aPolyLine;
static vtkUnstructuredGrid aPolyLineGrid;
static vtkDataSetMapper aPolyLineMapper;
static vtkActor aPolyLineActor;
static vtkPoints vertexPoints;
static vtkVertex aVertex;
static vtkUnstructuredGrid aVertexGrid;
static vtkDataSetMapper aVertexMapper;
static vtkActor aVertexActor;
static vtkPoints polyVertexPoints;
static vtkPolyVertex aPolyVertex;
static vtkUnstructuredGrid aPolyVertexGrid;
static vtkDataSetMapper aPolyVertexMapper;
static vtkActor aPolyVertexActor;
static vtkPoints pentaPoints;
static vtkPentagonalPrism aPenta;
static vtkUnstructuredGrid aPentaGrid;
static vtkDataSetMapper aPentaMapper;
static vtkActor aPentaActor;
static vtkPoints hexaPoints;
static vtkHexagonalPrism aHexa;
static vtkUnstructuredGrid aHexaGrid;
static vtkDataSetMapper aHexaMapper;
static vtkActor aHexaActor;
static vtkCellPicker cellPicker;
static vtkPointPicker pointPicker;
static vtkWorldPointPicker worldPicker;
static int cellCount;
static int pointCount;
static int x;
static int y;


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
        public static vtkPoints GetvoxelPoints()
        {
            return voxelPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetvoxelPoints(vtkPoints toSet)
        {
            voxelPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkVoxel GetaVoxel()
        {
            return aVoxel;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaVoxel(vtkVoxel toSet)
        {
            aVoxel = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkUnstructuredGrid GetaVoxelGrid()
        {
            return aVoxelGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaVoxelGrid(vtkUnstructuredGrid toSet)
        {
            aVoxelGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaVoxelMapper()
        {
            return aVoxelMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaVoxelMapper(vtkDataSetMapper toSet)
        {
            aVoxelMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaVoxelActor()
        {
            return aVoxelActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaVoxelActor(vtkActor toSet)
        {
            aVoxelActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GethexahedronPoints()
        {
            return hexahedronPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SethexahedronPoints(vtkPoints toSet)
        {
            hexahedronPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkHexahedron GetaHexahedron()
        {
            return aHexahedron;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaHexahedron(vtkHexahedron toSet)
        {
            aHexahedron = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkUnstructuredGrid GetaHexahedronGrid()
        {
            return aHexahedronGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaHexahedronGrid(vtkUnstructuredGrid toSet)
        {
            aHexahedronGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaHexahedronMapper()
        {
            return aHexahedronMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaHexahedronMapper(vtkDataSetMapper toSet)
        {
            aHexahedronMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaHexahedronActor()
        {
            return aHexahedronActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaHexahedronActor(vtkActor toSet)
        {
            aHexahedronActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GettetraPoints()
        {
            return tetraPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SettetraPoints(vtkPoints toSet)
        {
            tetraPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTetra GetaTetra()
        {
            return aTetra;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaTetra(vtkTetra toSet)
        {
            aTetra = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkUnstructuredGrid GetaTetraGrid()
        {
            return aTetraGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaTetraGrid(vtkUnstructuredGrid toSet)
        {
            aTetraGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaTetraMapper()
        {
            return aTetraMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaTetraMapper(vtkDataSetMapper toSet)
        {
            aTetraMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaTetraActor()
        {
            return aTetraActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaTetraActor(vtkActor toSet)
        {
            aTetraActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GetwedgePoints()
        {
            return wedgePoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetwedgePoints(vtkPoints toSet)
        {
            wedgePoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkWedge GetaWedge()
        {
            return aWedge;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaWedge(vtkWedge toSet)
        {
            aWedge = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkUnstructuredGrid GetaWedgeGrid()
        {
            return aWedgeGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaWedgeGrid(vtkUnstructuredGrid toSet)
        {
            aWedgeGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaWedgeMapper()
        {
            return aWedgeMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaWedgeMapper(vtkDataSetMapper toSet)
        {
            aWedgeMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaWedgeActor()
        {
            return aWedgeActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaWedgeActor(vtkActor toSet)
        {
            aWedgeActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GetpyramidPoints()
        {
            return pyramidPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetpyramidPoints(vtkPoints toSet)
        {
            pyramidPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPyramid GetaPyramid()
        {
            return aPyramid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPyramid(vtkPyramid toSet)
        {
            aPyramid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkUnstructuredGrid GetaPyramidGrid()
        {
            return aPyramidGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPyramidGrid(vtkUnstructuredGrid toSet)
        {
            aPyramidGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaPyramidMapper()
        {
            return aPyramidMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPyramidMapper(vtkDataSetMapper toSet)
        {
            aPyramidMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaPyramidActor()
        {
            return aPyramidActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPyramidActor(vtkActor toSet)
        {
            aPyramidActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GetpixelPoints()
        {
            return pixelPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetpixelPoints(vtkPoints toSet)
        {
            pixelPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPixel GetaPixel()
        {
            return aPixel;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPixel(vtkPixel toSet)
        {
            aPixel = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkUnstructuredGrid GetaPixelGrid()
        {
            return aPixelGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPixelGrid(vtkUnstructuredGrid toSet)
        {
            aPixelGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaPixelMapper()
        {
            return aPixelMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPixelMapper(vtkDataSetMapper toSet)
        {
            aPixelMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaPixelActor()
        {
            return aPixelActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPixelActor(vtkActor toSet)
        {
            aPixelActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GetquadPoints()
        {
            return quadPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetquadPoints(vtkPoints toSet)
        {
            quadPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkQuad GetaQuad()
        {
            return aQuad;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaQuad(vtkQuad toSet)
        {
            aQuad = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkUnstructuredGrid GetaQuadGrid()
        {
            return aQuadGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaQuadGrid(vtkUnstructuredGrid toSet)
        {
            aQuadGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaQuadMapper()
        {
            return aQuadMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaQuadMapper(vtkDataSetMapper toSet)
        {
            aQuadMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaQuadActor()
        {
            return aQuadActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaQuadActor(vtkActor toSet)
        {
            aQuadActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GettrianglePoints()
        {
            return trianglePoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SettrianglePoints(vtkPoints toSet)
        {
            trianglePoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTriangle GetaTriangle()
        {
            return aTriangle;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaTriangle(vtkTriangle toSet)
        {
            aTriangle = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkUnstructuredGrid GetaTriangleGrid()
        {
            return aTriangleGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaTriangleGrid(vtkUnstructuredGrid toSet)
        {
            aTriangleGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaTriangleMapper()
        {
            return aTriangleMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaTriangleMapper(vtkDataSetMapper toSet)
        {
            aTriangleMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaTriangleActor()
        {
            return aTriangleActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaTriangleActor(vtkActor toSet)
        {
            aTriangleActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GetpolygonPoints()
        {
            return polygonPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetpolygonPoints(vtkPoints toSet)
        {
            polygonPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolygon GetaPolygon()
        {
            return aPolygon;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPolygon(vtkPolygon toSet)
        {
            aPolygon = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkUnstructuredGrid GetaPolygonGrid()
        {
            return aPolygonGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPolygonGrid(vtkUnstructuredGrid toSet)
        {
            aPolygonGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaPolygonMapper()
        {
            return aPolygonMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPolygonMapper(vtkDataSetMapper toSet)
        {
            aPolygonMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaPolygonActor()
        {
            return aPolygonActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPolygonActor(vtkActor toSet)
        {
            aPolygonActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GettriangleStripPoints()
        {
            return triangleStripPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SettriangleStripPoints(vtkPoints toSet)
        {
            triangleStripPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkTriangleStrip GetaTriangleStrip()
        {
            return aTriangleStrip;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaTriangleStrip(vtkTriangleStrip toSet)
        {
            aTriangleStrip = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkUnstructuredGrid GetaTriangleStripGrid()
        {
            return aTriangleStripGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaTriangleStripGrid(vtkUnstructuredGrid toSet)
        {
            aTriangleStripGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaTriangleStripMapper()
        {
            return aTriangleStripMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaTriangleStripMapper(vtkDataSetMapper toSet)
        {
            aTriangleStripMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaTriangleStripActor()
        {
            return aTriangleStripActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaTriangleStripActor(vtkActor toSet)
        {
            aTriangleStripActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GetlinePoints()
        {
            return linePoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetlinePoints(vtkPoints toSet)
        {
            linePoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkLine GetaLine()
        {
            return aLine;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaLine(vtkLine toSet)
        {
            aLine = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkUnstructuredGrid GetaLineGrid()
        {
            return aLineGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaLineGrid(vtkUnstructuredGrid toSet)
        {
            aLineGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaLineMapper()
        {
            return aLineMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaLineMapper(vtkDataSetMapper toSet)
        {
            aLineMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaLineActor()
        {
            return aLineActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaLineActor(vtkActor toSet)
        {
            aLineActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GetpolyLinePoints()
        {
            return polyLinePoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetpolyLinePoints(vtkPoints toSet)
        {
            polyLinePoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyLine GetaPolyLine()
        {
            return aPolyLine;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPolyLine(vtkPolyLine toSet)
        {
            aPolyLine = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkUnstructuredGrid GetaPolyLineGrid()
        {
            return aPolyLineGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPolyLineGrid(vtkUnstructuredGrid toSet)
        {
            aPolyLineGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaPolyLineMapper()
        {
            return aPolyLineMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPolyLineMapper(vtkDataSetMapper toSet)
        {
            aPolyLineMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaPolyLineActor()
        {
            return aPolyLineActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPolyLineActor(vtkActor toSet)
        {
            aPolyLineActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GetvertexPoints()
        {
            return vertexPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetvertexPoints(vtkPoints toSet)
        {
            vertexPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkVertex GetaVertex()
        {
            return aVertex;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaVertex(vtkVertex toSet)
        {
            aVertex = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkUnstructuredGrid GetaVertexGrid()
        {
            return aVertexGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaVertexGrid(vtkUnstructuredGrid toSet)
        {
            aVertexGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaVertexMapper()
        {
            return aVertexMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaVertexMapper(vtkDataSetMapper toSet)
        {
            aVertexMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaVertexActor()
        {
            return aVertexActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaVertexActor(vtkActor toSet)
        {
            aVertexActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GetpolyVertexPoints()
        {
            return polyVertexPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetpolyVertexPoints(vtkPoints toSet)
        {
            polyVertexPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyVertex GetaPolyVertex()
        {
            return aPolyVertex;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPolyVertex(vtkPolyVertex toSet)
        {
            aPolyVertex = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkUnstructuredGrid GetaPolyVertexGrid()
        {
            return aPolyVertexGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPolyVertexGrid(vtkUnstructuredGrid toSet)
        {
            aPolyVertexGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaPolyVertexMapper()
        {
            return aPolyVertexMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPolyVertexMapper(vtkDataSetMapper toSet)
        {
            aPolyVertexMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaPolyVertexActor()
        {
            return aPolyVertexActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPolyVertexActor(vtkActor toSet)
        {
            aPolyVertexActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GetpentaPoints()
        {
            return pentaPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetpentaPoints(vtkPoints toSet)
        {
            pentaPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPentagonalPrism GetaPenta()
        {
            return aPenta;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPenta(vtkPentagonalPrism toSet)
        {
            aPenta = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkUnstructuredGrid GetaPentaGrid()
        {
            return aPentaGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPentaGrid(vtkUnstructuredGrid toSet)
        {
            aPentaGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaPentaMapper()
        {
            return aPentaMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPentaMapper(vtkDataSetMapper toSet)
        {
            aPentaMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaPentaActor()
        {
            return aPentaActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPentaActor(vtkActor toSet)
        {
            aPentaActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GethexaPoints()
        {
            return hexaPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SethexaPoints(vtkPoints toSet)
        {
            hexaPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkHexagonalPrism GetaHexa()
        {
            return aHexa;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaHexa(vtkHexagonalPrism toSet)
        {
            aHexa = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkUnstructuredGrid GetaHexaGrid()
        {
            return aHexaGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaHexaGrid(vtkUnstructuredGrid toSet)
        {
            aHexaGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaHexaMapper()
        {
            return aHexaMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaHexaMapper(vtkDataSetMapper toSet)
        {
            aHexaMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaHexaActor()
        {
            return aHexaActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaHexaActor(vtkActor toSet)
        {
            aHexaActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCellPicker GetcellPicker()
        {
            return cellPicker;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetcellPicker(vtkCellPicker toSet)
        {
            cellPicker = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPointPicker GetpointPicker()
        {
            return pointPicker;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetpointPicker(vtkPointPicker toSet)
        {
            pointPicker = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkWorldPointPicker GetworldPicker()
        {
            return worldPicker;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetworldPicker(vtkWorldPointPicker toSet)
        {
            worldPicker = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int GetcellCount()
        {
            return cellCount;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetcellCount(int toSet)
        {
            cellCount = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int GetpointCount()
        {
            return pointCount;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetpointCount(int toSet)
        {
            pointCount = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int Getx()
        {
            return x;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setx(int toSet)
        {
            x = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static int Gety()
        {
            return y;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Sety(int toSet)
        {
            y = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(voxelPoints!= null){voxelPoints.Dispose();}
  	if(aVoxel!= null){aVoxel.Dispose();}
  	if(aVoxelGrid!= null){aVoxelGrid.Dispose();}
  	if(aVoxelMapper!= null){aVoxelMapper.Dispose();}
  	if(aVoxelActor!= null){aVoxelActor.Dispose();}
  	if(hexahedronPoints!= null){hexahedronPoints.Dispose();}
  	if(aHexahedron!= null){aHexahedron.Dispose();}
  	if(aHexahedronGrid!= null){aHexahedronGrid.Dispose();}
  	if(aHexahedronMapper!= null){aHexahedronMapper.Dispose();}
  	if(aHexahedronActor!= null){aHexahedronActor.Dispose();}
  	if(tetraPoints!= null){tetraPoints.Dispose();}
  	if(aTetra!= null){aTetra.Dispose();}
  	if(aTetraGrid!= null){aTetraGrid.Dispose();}
  	if(aTetraMapper!= null){aTetraMapper.Dispose();}
  	if(aTetraActor!= null){aTetraActor.Dispose();}
  	if(wedgePoints!= null){wedgePoints.Dispose();}
  	if(aWedge!= null){aWedge.Dispose();}
  	if(aWedgeGrid!= null){aWedgeGrid.Dispose();}
  	if(aWedgeMapper!= null){aWedgeMapper.Dispose();}
  	if(aWedgeActor!= null){aWedgeActor.Dispose();}
  	if(pyramidPoints!= null){pyramidPoints.Dispose();}
  	if(aPyramid!= null){aPyramid.Dispose();}
  	if(aPyramidGrid!= null){aPyramidGrid.Dispose();}
  	if(aPyramidMapper!= null){aPyramidMapper.Dispose();}
  	if(aPyramidActor!= null){aPyramidActor.Dispose();}
  	if(pixelPoints!= null){pixelPoints.Dispose();}
  	if(aPixel!= null){aPixel.Dispose();}
  	if(aPixelGrid!= null){aPixelGrid.Dispose();}
  	if(aPixelMapper!= null){aPixelMapper.Dispose();}
  	if(aPixelActor!= null){aPixelActor.Dispose();}
  	if(quadPoints!= null){quadPoints.Dispose();}
  	if(aQuad!= null){aQuad.Dispose();}
  	if(aQuadGrid!= null){aQuadGrid.Dispose();}
  	if(aQuadMapper!= null){aQuadMapper.Dispose();}
  	if(aQuadActor!= null){aQuadActor.Dispose();}
  	if(trianglePoints!= null){trianglePoints.Dispose();}
  	if(aTriangle!= null){aTriangle.Dispose();}
  	if(aTriangleGrid!= null){aTriangleGrid.Dispose();}
  	if(aTriangleMapper!= null){aTriangleMapper.Dispose();}
  	if(aTriangleActor!= null){aTriangleActor.Dispose();}
  	if(polygonPoints!= null){polygonPoints.Dispose();}
  	if(aPolygon!= null){aPolygon.Dispose();}
  	if(aPolygonGrid!= null){aPolygonGrid.Dispose();}
  	if(aPolygonMapper!= null){aPolygonMapper.Dispose();}
  	if(aPolygonActor!= null){aPolygonActor.Dispose();}
  	if(triangleStripPoints!= null){triangleStripPoints.Dispose();}
  	if(aTriangleStrip!= null){aTriangleStrip.Dispose();}
  	if(aTriangleStripGrid!= null){aTriangleStripGrid.Dispose();}
  	if(aTriangleStripMapper!= null){aTriangleStripMapper.Dispose();}
  	if(aTriangleStripActor!= null){aTriangleStripActor.Dispose();}
  	if(linePoints!= null){linePoints.Dispose();}
  	if(aLine!= null){aLine.Dispose();}
  	if(aLineGrid!= null){aLineGrid.Dispose();}
  	if(aLineMapper!= null){aLineMapper.Dispose();}
  	if(aLineActor!= null){aLineActor.Dispose();}
  	if(polyLinePoints!= null){polyLinePoints.Dispose();}
  	if(aPolyLine!= null){aPolyLine.Dispose();}
  	if(aPolyLineGrid!= null){aPolyLineGrid.Dispose();}
  	if(aPolyLineMapper!= null){aPolyLineMapper.Dispose();}
  	if(aPolyLineActor!= null){aPolyLineActor.Dispose();}
  	if(vertexPoints!= null){vertexPoints.Dispose();}
  	if(aVertex!= null){aVertex.Dispose();}
  	if(aVertexGrid!= null){aVertexGrid.Dispose();}
  	if(aVertexMapper!= null){aVertexMapper.Dispose();}
  	if(aVertexActor!= null){aVertexActor.Dispose();}
  	if(polyVertexPoints!= null){polyVertexPoints.Dispose();}
  	if(aPolyVertex!= null){aPolyVertex.Dispose();}
  	if(aPolyVertexGrid!= null){aPolyVertexGrid.Dispose();}
  	if(aPolyVertexMapper!= null){aPolyVertexMapper.Dispose();}
  	if(aPolyVertexActor!= null){aPolyVertexActor.Dispose();}
  	if(pentaPoints!= null){pentaPoints.Dispose();}
  	if(aPenta!= null){aPenta.Dispose();}
  	if(aPentaGrid!= null){aPentaGrid.Dispose();}
  	if(aPentaMapper!= null){aPentaMapper.Dispose();}
  	if(aPentaActor!= null){aPentaActor.Dispose();}
  	if(hexaPoints!= null){hexaPoints.Dispose();}
  	if(aHexa!= null){aHexa.Dispose();}
  	if(aHexaGrid!= null){aHexaGrid.Dispose();}
  	if(aHexaMapper!= null){aHexaMapper.Dispose();}
  	if(aHexaActor!= null){aHexaActor.Dispose();}
  	if(cellPicker!= null){cellPicker.Dispose();}
  	if(pointPicker!= null){pointPicker.Dispose();}
  	if(worldPicker!= null){worldPicker.Dispose();}
  }

}
//--- end of script --//

