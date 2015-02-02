using System.IO;
using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\TestCellDerivs.tcl
// output file is AVTestCellDerivs.cs
/// <summary>
/// The testing class derived from AVTestCellDerivs
/// </summary>
public class AVTestCellDerivsClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVTestCellDerivs(String [] argv)
  {
  //Prefix Content is: ""
  
  // Demonstrates vtkCellDerivatives for all cell types[]
  //[]
  // get the interactor ui[]
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
  aVoxelMapper.SetInputData((vtkDataSet)aVoxelGrid);
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
  aHexahedronMapper.SetInputData((vtkDataSet)aHexahedronGrid);
  aHexahedronActor = new vtkActor();
  aHexahedronActor.SetMapper((vtkMapper)aHexahedronMapper);
  aHexahedronActor.AddPosition((double)2,(double)0,(double)0);
  aHexahedronActor.GetProperty().BackfaceCullingOn();
  // Tetra[]
  tetraPoints = new vtkPoints();
  tetraPoints.SetNumberOfPoints((int)4);
  tetraPoints.InsertPoint((int)0,(double)0,(double)0,(double)0);
  tetraPoints.InsertPoint((int)1,(double)1,(double)0,(double)0);
  tetraPoints.InsertPoint((int)2,(double)0,(double)1,(double)0);
  tetraPoints.InsertPoint((int)3,(double)1,(double)1,(double)1);
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
  aTetraMapper.SetInputData((vtkDataSet)aTetraGrid);
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
  aWedgeMapper.SetInputData((vtkDataSet)aWedgeGrid);
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
  aPyramidMapper.SetInputData((vtkDataSet)aPyramidGrid);
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
  aPixelMapper.SetInputData((vtkDataSet)aPixelGrid);
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
  aQuadMapper.SetInputData((vtkDataSet)aQuadGrid);
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
  triangleTCoords = new vtkFloatArray();
  triangleTCoords.SetNumberOfComponents((int)2);
  triangleTCoords.SetNumberOfTuples((int)3);
  triangleTCoords.InsertTuple2((int)0,(double)1,(double)1);
  triangleTCoords.InsertTuple2((int)1,(double)2,(double)2);
  triangleTCoords.InsertTuple2((int)2,(double)3,(double)3);
  aTriangle = new vtkTriangle();
  aTriangle.GetPointIds().SetId((int)0,(int)0);
  aTriangle.GetPointIds().SetId((int)1,(int)1);
  aTriangle.GetPointIds().SetId((int)2,(int)2);
  aTriangleGrid = new vtkUnstructuredGrid();
  aTriangleGrid.Allocate((int)1,(int)1);
  aTriangleGrid.InsertNextCell((int)aTriangle.GetCellType(),(vtkIdList)aTriangle.GetPointIds());
  aTriangleGrid.SetPoints((vtkPoints)trianglePoints);
  aTriangleGrid.GetPointData().SetTCoords((vtkDataArray)triangleTCoords);
  aTriangleMapper = new vtkDataSetMapper();
  aTriangleMapper.SetInputData((vtkDataSet)aTriangleGrid);
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
  aPolygonMapper.SetInputData((vtkDataSet)aPolygonGrid);
  aPolygonActor = new vtkActor();
  aPolygonActor.SetMapper((vtkMapper)aPolygonMapper);
  aPolygonActor.AddPosition((double)6,(double)0,(double)2);
  aPolygonActor.GetProperty().BackfaceCullingOn();
  // Triangle strip[]
  triangleStripPoints = new vtkPoints();
  triangleStripPoints.SetNumberOfPoints((int)5);
  triangleStripPoints.InsertPoint((int)0,(double)0,(double)1,(double)0);
  triangleStripPoints.InsertPoint((int)1,(double)0,(double)0,(double)0);
  triangleStripPoints.InsertPoint((int)2,(double)1,(double)1,(double)0);
  triangleStripPoints.InsertPoint((int)3,(double)1,(double)0,(double)0);
  triangleStripPoints.InsertPoint((int)4,(double)2,(double)1,(double)0);
  triangleStripTCoords = new vtkFloatArray();
  triangleStripTCoords.SetNumberOfComponents((int)2);
  triangleStripTCoords.SetNumberOfTuples((int)3);
  triangleStripTCoords.InsertTuple2((int)0,(double)1,(double)1);
  triangleStripTCoords.InsertTuple2((int)1,(double)2,(double)2);
  triangleStripTCoords.InsertTuple2((int)2,(double)3,(double)3);
  triangleStripTCoords.InsertTuple2((int)3,(double)4,(double)4);
  triangleStripTCoords.InsertTuple2((int)4,(double)5,(double)5);
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
  aTriangleStripGrid.GetPointData().SetTCoords((vtkDataArray)triangleStripTCoords);
  aTriangleStripMapper = new vtkDataSetMapper();
  aTriangleStripMapper.SetInputData((vtkDataSet)aTriangleStripGrid);
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
  aLineMapper.SetInputData((vtkDataSet)aLineGrid);
  aLineActor = new vtkActor();
  aLineActor.SetMapper((vtkMapper)aLineMapper);
  aLineActor.AddPosition((double)0,(double)0,(double)4);
  aLineActor.GetProperty().BackfaceCullingOn();
  // Polyline[]
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
  aPolyLineMapper.SetInputData((vtkDataSet)aPolyLineGrid);
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
  aVertexMapper.SetInputData((vtkDataSet)aVertexGrid);
  aVertexActor = new vtkActor();
  aVertexActor.SetMapper((vtkMapper)aVertexMapper);
  aVertexActor.AddPosition((double)0,(double)0,(double)6);
  aVertexActor.GetProperty().BackfaceCullingOn();
  // Polyvertex[]
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
  aPolyVertexMapper.SetInputData((vtkDataSet)aPolyVertexGrid);
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
  aPentaMapper.SetInputData((vtkDataSet)aPentaGrid);
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
  aHexaMapper.SetInputData((vtkDataSet)aHexaGrid);
  aHexaActor = new vtkActor();
  aHexaActor.SetMapper((vtkMapper)aHexaMapper);
  aHexaActor.AddPosition((double)12,(double)0,(double)0);
  aHexaActor.GetProperty().BackfaceCullingOn();
  ren1.SetBackground((double)1,(double)1,(double)1);
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
  aPentaActor.GetProperty().SetDiffuseColor((double)1,(double)1,(double)0);
  ren1.AddActor((vtkProp)aHexaActor);
  aHexaActor.GetProperty().SetDiffuseColor((double)1,(double)1,(double)0);
  //[]
  // get the cell center of each type and put a glyph there[]
  //[]
  ball = new vtkSphereSource();
  ball.SetRadius((double).2);

      
            bool tryWorked = false;
      aVoxelScalars = new vtkFloatArray();
      N = aVoxelGrid.GetNumberOfPoints();
      aVoxelScalar = new vtkFloatArray();
      aVoxelScalar.SetNumberOfTuples((int)N);
      aVoxelScalar.SetNumberOfComponents(1);
      i = 0;
      while((i) < N)
        {
          aVoxelScalar.SetValue(i,0);
          i = i + 1;
        }

      aVoxelScalar.SetValue(0,4);
      aVoxelGrid.GetPointData().SetScalars(aVoxelScalar);
      
      aHexahedronScalars = new vtkFloatArray();
      N = aHexahedronGrid.GetNumberOfPoints();
      aHexahedronScalar = new vtkFloatArray();
      aHexahedronScalar.SetNumberOfTuples((int)N);
      aHexahedronScalar.SetNumberOfComponents(1);
      i = 0;
      while((i) < N)
        {
          aHexahedronScalar.SetValue(i,0);
          i = i + 1;
        }

      aHexahedronScalar.SetValue(0,4);
      aHexahedronGrid.GetPointData().SetScalars(aHexahedronScalar);
      
      aWedgeScalars = new vtkFloatArray();
      N = aWedgeGrid.GetNumberOfPoints();
      aWedgeScalar = new vtkFloatArray();
      aWedgeScalar.SetNumberOfTuples((int)N);
      aWedgeScalar.SetNumberOfComponents(1);
      i = 0;
      while((i) < N)
        {
          aWedgeScalar.SetValue(i,0);
          i = i + 1;
        }

      aWedgeScalar.SetValue(0,4);
      aWedgeGrid.GetPointData().SetScalars(aWedgeScalar);
      
      aPyramidScalars = new vtkFloatArray();
      N = aPyramidGrid.GetNumberOfPoints();
      aPyramidScalar = new vtkFloatArray();
      aPyramidScalar.SetNumberOfTuples((int)N);
      aPyramidScalar.SetNumberOfComponents(1);
      i = 0;
      while((i) < N)
        {
          aPyramidScalar.SetValue(i,0);
          i = i + 1;
        }

      aPyramidScalar.SetValue(0,4);
      aPyramidGrid.GetPointData().SetScalars(aPyramidScalar);
      
      aTetraScalars = new vtkFloatArray();
      N = aTetraGrid.GetNumberOfPoints();
      aTetraScalar = new vtkFloatArray();
      aTetraScalar.SetNumberOfTuples((int)N);
      aTetraScalar.SetNumberOfComponents(1);
      i = 0;
      while((i) < N)
        {
          aTetraScalar.SetValue(i,0);
          i = i + 1;
        }

      aTetraScalar.SetValue(0,4);
      aTetraGrid.GetPointData().SetScalars(aTetraScalar);
      
      aQuadScalars = new vtkFloatArray();
      N = aQuadGrid.GetNumberOfPoints();
      aQuadScalar = new vtkFloatArray();
      aQuadScalar.SetNumberOfTuples((int)N);
      aQuadScalar.SetNumberOfComponents(1);
      i = 0;
      while((i) < N)
        {
          aQuadScalar.SetValue(i,0);
          i = i + 1;
        }

      aQuadScalar.SetValue(0,4);
      aQuadGrid.GetPointData().SetScalars(aQuadScalar);
      
      aTriangleScalars = new vtkFloatArray();
      N = aTriangleGrid.GetNumberOfPoints();
      aTriangleScalar = new vtkFloatArray();
      aTriangleScalar.SetNumberOfTuples((int)N);
      aTriangleScalar.SetNumberOfComponents(1);
      i = 0;
      while((i) < N)
        {
          aTriangleScalar.SetValue(i,0);
          i = i + 1;
        }

      aTriangleScalar.SetValue(0,4);
      aTriangleGrid.GetPointData().SetScalars(aTriangleScalar);
      
      aTriangleStripScalars = new vtkFloatArray();
      N = aTriangleStripGrid.GetNumberOfPoints();
      aTriangleStripScalar = new vtkFloatArray();
      aTriangleStripScalar.SetNumberOfTuples((int)N);
      aTriangleStripScalar.SetNumberOfComponents(1);
      i = 0;
      while((i) < N)
        {
          aTriangleStripScalar.SetValue(i,0);
          i = i + 1;
        }

      aTriangleStripScalar.SetValue(0,4);
      aTriangleStripGrid.GetPointData().SetScalars(aTriangleStripScalar);
      
      aLineScalars = new vtkFloatArray();
      N = aLineGrid.GetNumberOfPoints();
      aLineScalar = new vtkFloatArray();
      aLineScalar.SetNumberOfTuples((int)N);
      aLineScalar.SetNumberOfComponents(1);
      i = 0;
      while((i) < N)
        {
          aLineScalar.SetValue(i,0);
          i = i + 1;
        }

      aLineScalar.SetValue(0,4);
      aLineGrid.GetPointData().SetScalars(aLineScalar);
      
      aPolyLineScalars = new vtkFloatArray();
      N = aPolyLineGrid.GetNumberOfPoints();
      aPolyLineScalar = new vtkFloatArray();
      aPolyLineScalar.SetNumberOfTuples((int)N);
      aPolyLineScalar.SetNumberOfComponents(1);
      i = 0;
      while((i) < N)
        {
          aPolyLineScalar.SetValue(i,0);
          i = i + 1;
        }

      aPolyLineScalar.SetValue(0,4);
      aPolyLineGrid.GetPointData().SetScalars(aPolyLineScalar);
      
      aVertexScalars = new vtkFloatArray();
      N = aVertexGrid.GetNumberOfPoints();
      aVertexScalar = new vtkFloatArray();
      aVertexScalar.SetNumberOfTuples((int)N);
      aVertexScalar.SetNumberOfComponents(1);
      i = 0;
      while((i) < N)
        {
          aVertexScalar.SetValue(i,0);
          i = i + 1;
        }

      aVertexScalar.SetValue(0,4);
      aVertexGrid.GetPointData().SetScalars(aVertexScalar);
      
      aPolyVertexScalars = new vtkFloatArray();
      N = aPolyVertexGrid.GetNumberOfPoints();
      aPolyVertexScalar = new vtkFloatArray();
      aPolyVertexScalar.SetNumberOfTuples((int)N);
      aPolyVertexScalar.SetNumberOfComponents(1);
      i = 0;
      while((i) < N)
        {
          aPolyVertexScalar.SetValue(i,0);
          i = i + 1;
        }

      aPolyVertexScalar.SetValue(0,4);
      aPolyVertexGrid.GetPointData().SetScalars(aPolyVertexScalar);
      
      aPixelScalars = new vtkFloatArray();
      N = aPixelGrid.GetNumberOfPoints();
      aPixelScalar = new vtkFloatArray();
      aPixelScalar.SetNumberOfTuples((int)N);
      aPixelScalar.SetNumberOfComponents(1);
      i = 0;
      while((i) < N)
        {
          aPixelScalar.SetValue(i,0);
          i = i + 1;
        }

      aPixelScalar.SetValue(0,4);
      aPixelGrid.GetPointData().SetScalars(aPixelScalar);
      
      aPolygonScalars = new vtkFloatArray();
      N = aPolygonGrid.GetNumberOfPoints();
      aPolygonScalar = new vtkFloatArray();
      aPolygonScalar.SetNumberOfTuples((int)N);
      aPolygonScalar.SetNumberOfComponents(1);
      i = 0;
      while((i) < N)
        {
          aPolygonScalar.SetValue(i,0);
          i = i + 1;
        }

      aPolygonScalar.SetValue(0,4);
      aPolygonGrid.GetPointData().SetScalars(aPolygonScalar);
      
      aPentaScalars = new vtkFloatArray();
      N = aPentaGrid.GetNumberOfPoints();
      aPentaScalar = new vtkFloatArray();
      aPentaScalar.SetNumberOfTuples((int)N);
      aPentaScalar.SetNumberOfComponents(1);
      i = 0;
      while((i) < N)
        {
          aPentaScalar.SetValue(i,0);
          i = i + 1;
        }

      aPentaScalar.SetValue(0,4);
      aPentaGrid.GetPointData().SetScalars(aPentaScalar);
      
      aHexaScalars = new vtkFloatArray();
      N = aHexaGrid.GetNumberOfPoints();
      aHexaScalar = new vtkFloatArray();
      aHexaScalar.SetNumberOfTuples((int)N);
      aHexaScalar.SetNumberOfComponents(1);
      i = 0;
      while((i) < N)
        {
          aHexaScalar.SetValue(i,0);
          i = i + 1;
        }

      aHexaScalar.SetValue(0,4);
      aHexaGrid.GetPointData().SetScalars(aHexaScalar);
      
  // write to the temp directory if possible, otherwise use .[]
  dir = ".";
      dir = TclToCsScriptTestDriver.GetTempDirectory();
    
      aVoxelderivs = new vtkCellDerivatives();
      aVoxelderivs.SetInputData(aVoxelGrid);
      aVoxelderivs.SetVectorModeToComputeGradient();
      FileName = dir;
      FileName += "/aVoxel";
      FileName += ".vtk";
      // make sure the directory is writeable first[]
      tryWorked = false;
      try
      {
         channel = new StreamWriter("" + (dir.ToString()) + "/test.tmp");
         tryWorked = true;
      }
      catch(Exception)
        {
        tryWorked = false;
        }
      if(tryWorked)
        {
          channel.Close();
          File.Delete("" + (dir.ToString()) + "/test.tmp");
          aVoxelWriter = new vtkUnstructuredGridWriter();
          aVoxelWriter.SetInputConnection(aVoxelderivs.GetOutputPort());
          aVoxelWriter.SetFileName(FileName);
          aVoxelWriter.Write();
          // delete the file[]
          File.Delete("FileName");
        }

      
      aVoxelCenters = new vtkCellCenters();
      aVoxelCenters.SetInputConnection(aVoxelderivs.GetOutputPort());
      aVoxelCenters.VertexCellsOn();
      aVoxelhog = new vtkHedgeHog();
      aVoxelhog.SetInputConnection(aVoxelCenters.GetOutputPort());
      aVoxelmapHog = vtkPolyDataMapper.New();
      aVoxelmapHog.SetInputConnection(aVoxelhog.GetOutputPort());
      aVoxelmapHog.SetScalarModeToUseCellData();
      aVoxelmapHog.ScalarVisibilityOff();
      aVoxelhogActor = new vtkActor();
      aVoxelhogActor.SetMapper(aVoxelmapHog);
      aVoxelhogActor.GetProperty().SetColor(0,1,0);
      aVoxelGlyph3D = new vtkGlyph3D();
      aVoxelGlyph3D.SetInputConnection(aVoxelCenters.GetOutputPort());
      aVoxelGlyph3D.SetSourceConnection(ball.GetOutputPort());
      aVoxelCentersMapper = vtkPolyDataMapper.New();
      aVoxelCentersMapper.SetInputConnection(aVoxelGlyph3D.GetOutputPort());
      aVoxelCentersActor = new vtkActor();
        aVoxelCentersActor.SetMapper(aVoxelCentersMapper);
        aVoxelhogActor.SetPosition(aVoxelActor.GetPosition()[0],aVoxelActor.GetPosition()[1],aVoxelActor.GetPosition()[2]);
        ren1.AddActor((vtkProp)aVoxelhogActor);
        aVoxelhogActor.GetProperty().SetRepresentationToWireframe();
        
  aHexahedronderivs = new vtkCellDerivatives();
  aHexahedronderivs.SetInputData(aHexahedronGrid);
        aHexahedronderivs.SetVectorModeToComputeGradient();
        FileName = dir;
        FileName += "/aHexahedron";
      FileName += ".vtk";
      // make sure the directory is writeable first[]
      tryWorked = false;
      try
      {
         channel = new StreamWriter("" + (dir.ToString()) + "/test.tmp");
         tryWorked = true;
      }
      catch(Exception)
        {
        tryWorked = false;
        }
      if(tryWorked)
        {
          channel.Close();
          File.Delete("" + (dir.ToString()) + "/test.tmp");
          aHexahedronWriter = new vtkUnstructuredGridWriter();
          aHexahedronWriter.SetInputConnection(aHexahedronderivs.GetOutputPort());
          aHexahedronWriter.SetFileName(FileName);
          aHexahedronWriter.Write();
          // delete the file[]
          File.Delete("FileName");
        }

      
      aHexahedronCenters = new vtkCellCenters();
      aHexahedronCenters.SetInputConnection(aHexahedronderivs.GetOutputPort());
      aHexahedronCenters.VertexCellsOn();
      aHexahedronhog = new vtkHedgeHog();
      aHexahedronhog.SetInputConnection(aHexahedronCenters.GetOutputPort());
      aHexahedronmapHog = vtkPolyDataMapper.New();
      aHexahedronmapHog.SetInputConnection(aHexahedronhog.GetOutputPort());
      aHexahedronmapHog.SetScalarModeToUseCellData();
      aHexahedronmapHog.ScalarVisibilityOff();
      aHexahedronhogActor = new vtkActor();
      aHexahedronhogActor.SetMapper(aHexahedronmapHog);
      aHexahedronhogActor.GetProperty().SetColor(0,1,0);
      aHexahedronGlyph3D = new vtkGlyph3D();
      aHexahedronGlyph3D.SetInputConnection(aHexahedronCenters.GetOutputPort());
      aHexahedronGlyph3D.SetSourceConnection(ball.GetOutputPort());
      aHexahedronCentersMapper = vtkPolyDataMapper.New();
      aHexahedronCentersMapper.SetInputConnection(aHexahedronGlyph3D.GetOutputPort());
      aHexahedronCentersActor = new vtkActor();
      aHexahedronCentersActor.SetMapper(aHexahedronCentersMapper);
      aHexahedronhogActor.SetPosition(aHexahedronActor.GetPosition()[0],aHexahedronActor.GetPosition()[1],aHexahedronActor.GetPosition()[2]);
      ren1.AddActor((vtkProp)aHexahedronhogActor);
      aHexahedronhogActor.GetProperty().SetRepresentationToWireframe();
      
aWedgederivs = new vtkCellDerivatives();
aWedgederivs.SetInputData(aWedgeGrid);
      aWedgederivs.SetVectorModeToComputeGradient();
      FileName = dir;
      FileName += "/aWedge";
      FileName += ".vtk";
      // make sure the directory is writeable first[]
      tryWorked = false;
      try
      {
         channel = new StreamWriter("" + (dir.ToString()) + "/test.tmp");
         tryWorked = true;
      }
      catch(Exception)
        {
        tryWorked = false;
        }
      if(tryWorked)
        {
          channel.Close();
          File.Delete("" + (dir.ToString()) + "/test.tmp");
          aWedgeWriter = new vtkUnstructuredGridWriter();
          aWedgeWriter.SetInputConnection(aWedgederivs.GetOutputPort());
          aWedgeWriter.SetFileName(FileName);
          aWedgeWriter.Write();
          // delete the file[]
          File.Delete("FileName");
        }

      
      aWedgeCenters = new vtkCellCenters();
      aWedgeCenters.SetInputConnection(aWedgederivs.GetOutputPort());
      aWedgeCenters.VertexCellsOn();
      aWedgehog = new vtkHedgeHog();
      aWedgehog.SetInputConnection(aWedgeCenters.GetOutputPort());
      aWedgemapHog = vtkPolyDataMapper.New();
      aWedgemapHog.SetInputConnection(aWedgehog.GetOutputPort());
      aWedgemapHog.SetScalarModeToUseCellData();
      aWedgemapHog.ScalarVisibilityOff();
      aWedgehogActor = new vtkActor();
      aWedgehogActor.SetMapper(aWedgemapHog);
      aWedgehogActor.GetProperty().SetColor(0,1,0);
      aWedgeGlyph3D = new vtkGlyph3D();
      aWedgeGlyph3D.SetInputConnection(aWedgeCenters.GetOutputPort());
      aWedgeGlyph3D.SetSourceConnection(ball.GetOutputPort());
      aWedgeCentersMapper = vtkPolyDataMapper.New();
      aWedgeCentersMapper.SetInputConnection(aWedgeGlyph3D.GetOutputPort());
      aWedgeCentersActor = new vtkActor();
      aWedgeCentersActor.SetMapper(aWedgeCentersMapper);
      aWedgehogActor.SetPosition(aWedgeActor.GetPosition()[0],aWedgeActor.GetPosition()[1],aWedgeActor.GetPosition()[2]);
      ren1.AddActor((vtkProp)aWedgehogActor);
      aWedgehogActor.GetProperty().SetRepresentationToWireframe();
      
aPyramidderivs = new vtkCellDerivatives();
aPyramidderivs.SetInputData(aPyramidGrid);
      aPyramidderivs.SetVectorModeToComputeGradient();
      FileName = dir;
      FileName += "/aPyramid";
      FileName += ".vtk";
      // make sure the directory is writeable first[]
      tryWorked = false;
      try
      {
         channel = new StreamWriter("" + (dir.ToString()) + "/test.tmp");
         tryWorked = true;
      }
      catch(Exception)
        {
        tryWorked = false;
        }
      if(tryWorked)
        {
          channel.Close();
          File.Delete("" + (dir.ToString()) + "/test.tmp");
          aPyramidWriter = new vtkUnstructuredGridWriter();
          aPyramidWriter.SetInputConnection(aPyramidderivs.GetOutputPort());
          aPyramidWriter.SetFileName(FileName);
          aPyramidWriter.Write();
          // delete the file[]
          File.Delete("FileName");
        }

      
      aPyramidCenters = new vtkCellCenters();
      aPyramidCenters.SetInputConnection(aPyramidderivs.GetOutputPort());
      aPyramidCenters.VertexCellsOn();
      aPyramidhog = new vtkHedgeHog();
      aPyramidhog.SetInputConnection(aPyramidCenters.GetOutputPort());
      aPyramidmapHog = vtkPolyDataMapper.New();
      aPyramidmapHog.SetInputConnection(aPyramidhog.GetOutputPort());
      aPyramidmapHog.SetScalarModeToUseCellData();
      aPyramidmapHog.ScalarVisibilityOff();
      aPyramidhogActor = new vtkActor();
      aPyramidhogActor.SetMapper(aPyramidmapHog);
      aPyramidhogActor.GetProperty().SetColor(0,1,0);
      aPyramidGlyph3D = new vtkGlyph3D();
      aPyramidGlyph3D.SetInputConnection(aPyramidCenters.GetOutputPort());
      aPyramidGlyph3D.SetSourceConnection(ball.GetOutputPort());
      aPyramidCentersMapper = vtkPolyDataMapper.New();
      aPyramidCentersMapper.SetInputConnection(aPyramidGlyph3D.GetOutputPort());
      aPyramidCentersActor = new vtkActor();
      aPyramidCentersActor.SetMapper(aPyramidCentersMapper);
      aPyramidhogActor.SetPosition(aPyramidActor.GetPosition()[0],aPyramidActor.GetPosition()[1],aPyramidActor.GetPosition()[2]);
      ren1.AddActor((vtkProp)aPyramidhogActor);
      aPyramidhogActor.GetProperty().SetRepresentationToWireframe();
      
aTetraderivs = new vtkCellDerivatives();
aTetraderivs.SetInputData(aTetraGrid);
      aTetraderivs.SetVectorModeToComputeGradient();
      FileName = dir;
      FileName += "/aTetra";
      FileName += ".vtk";
      // make sure the directory is writeable first[]
      tryWorked = false;
      try
      {
         channel = new StreamWriter("" + (dir.ToString()) + "/test.tmp");
         tryWorked = true;
      }
      catch(Exception)
        {
        tryWorked = false;
        }
      if(tryWorked)
        {
          channel.Close();
          File.Delete("" + (dir.ToString()) + "/test.tmp");
          aTetraWriter = new vtkUnstructuredGridWriter();
          aTetraWriter.SetInputConnection(aTetraderivs.GetOutputPort());
          aTetraWriter.SetFileName(FileName);
          aTetraWriter.Write();
          // delete the file[]
          File.Delete("FileName");
        }

      
      aTetraCenters = new vtkCellCenters();
      aTetraCenters.SetInputConnection(aTetraderivs.GetOutputPort());
      aTetraCenters.VertexCellsOn();
      aTetrahog = new vtkHedgeHog();
      aTetrahog.SetInputConnection(aTetraCenters.GetOutputPort());
      aTetramapHog = vtkPolyDataMapper.New();
      aTetramapHog.SetInputConnection(aTetrahog.GetOutputPort());
      aTetramapHog.SetScalarModeToUseCellData();
      aTetramapHog.ScalarVisibilityOff();
      aTetrahogActor = new vtkActor();
      aTetrahogActor.SetMapper(aTetramapHog);
      aTetrahogActor.GetProperty().SetColor(0,1,0);
      aTetraGlyph3D = new vtkGlyph3D();
      aTetraGlyph3D.SetInputConnection(aTetraCenters.GetOutputPort());
      aTetraGlyph3D.SetSourceConnection(ball.GetOutputPort());
      aTetraCentersMapper = vtkPolyDataMapper.New();
      aTetraCentersMapper.SetInputConnection(aTetraGlyph3D.GetOutputPort());
      aTetraCentersActor = new vtkActor();
      aTetraCentersActor.SetMapper(aTetraCentersMapper);
      aTetrahogActor.SetPosition(aTetraActor.GetPosition()[0],aTetraActor.GetPosition()[1],aTetraActor.GetPosition()[2]);
      ren1.AddActor((vtkProp)aTetrahogActor);
      aTetrahogActor.GetProperty().SetRepresentationToWireframe();
      
aQuadderivs = new vtkCellDerivatives();
aQuadderivs.SetInputData(aQuadGrid);
      aQuadderivs.SetVectorModeToComputeGradient();
      FileName = dir;
      FileName += "/aQuad";
      FileName += ".vtk";
      // make sure the directory is writeable first[]
      tryWorked = false;
      try
      {
         channel = new StreamWriter("" + (dir.ToString()) + "/test.tmp");
         tryWorked = true;
      }
      catch(Exception)
        {
        tryWorked = false;
        }
      if(tryWorked)
        {
          channel.Close();
          File.Delete("" + (dir.ToString()) + "/test.tmp");
          aQuadWriter = new vtkUnstructuredGridWriter();
          aQuadWriter.SetInputConnection(aQuadderivs.GetOutputPort());
          aQuadWriter.SetFileName(FileName);
          aQuadWriter.Write();
          // delete the file[]
          File.Delete("FileName");
        }

      
      aQuadCenters = new vtkCellCenters();
      aQuadCenters.SetInputConnection(aQuadderivs.GetOutputPort());
      aQuadCenters.VertexCellsOn();
      aQuadhog = new vtkHedgeHog();
      aQuadhog.SetInputConnection(aQuadCenters.GetOutputPort());
      aQuadmapHog = vtkPolyDataMapper.New();
      aQuadmapHog.SetInputConnection(aQuadhog.GetOutputPort());
      aQuadmapHog.SetScalarModeToUseCellData();
      aQuadmapHog.ScalarVisibilityOff();
      aQuadhogActor = new vtkActor();
      aQuadhogActor.SetMapper(aQuadmapHog);
      aQuadhogActor.GetProperty().SetColor(0,1,0);
      aQuadGlyph3D = new vtkGlyph3D();
      aQuadGlyph3D.SetInputConnection(aQuadCenters.GetOutputPort());
      aQuadGlyph3D.SetSourceConnection(ball.GetOutputPort());
      aQuadCentersMapper = vtkPolyDataMapper.New();
      aQuadCentersMapper.SetInputConnection(aQuadGlyph3D.GetOutputPort());
      aQuadCentersActor = new vtkActor();
      aQuadCentersActor.SetMapper(aQuadCentersMapper);
      aQuadhogActor.SetPosition(aQuadActor.GetPosition()[0],aQuadActor.GetPosition()[1],aQuadActor.GetPosition()[2]);
      ren1.AddActor((vtkProp)aQuadhogActor);
      aQuadhogActor.GetProperty().SetRepresentationToWireframe();
      
aTrianglederivs = new vtkCellDerivatives();
aTrianglederivs.SetInputData(aTriangleGrid);
      aTrianglederivs.SetVectorModeToComputeGradient();
      FileName = dir;
      FileName += "/aTriangle";
      FileName += ".vtk";
      // make sure the directory is writeable first[]
      tryWorked = false;
      try
      {
         channel = new StreamWriter("" + (dir.ToString()) + "/test.tmp");
         tryWorked = true;
      }
      catch(Exception)
        {
        tryWorked = false;
        }
      if(tryWorked)
        {
          channel.Close();
          File.Delete("" + (dir.ToString()) + "/test.tmp");
          aTriangleWriter = new vtkUnstructuredGridWriter();
          aTriangleWriter.SetInputConnection(aTrianglederivs.GetOutputPort());
          aTriangleWriter.SetFileName(FileName);
          aTriangleWriter.Write();
          // delete the file[]
          File.Delete("FileName");
        }

      
      aTriangleCenters = new vtkCellCenters();
      aTriangleCenters.SetInputConnection(aTrianglederivs.GetOutputPort());
      aTriangleCenters.VertexCellsOn();
      aTrianglehog = new vtkHedgeHog();
      aTrianglehog.SetInputConnection(aTriangleCenters.GetOutputPort());
      aTrianglemapHog = vtkPolyDataMapper.New();
      aTrianglemapHog.SetInputConnection(aTrianglehog.GetOutputPort());
      aTrianglemapHog.SetScalarModeToUseCellData();
      aTrianglemapHog.ScalarVisibilityOff();
      aTrianglehogActor = new vtkActor();
      aTrianglehogActor.SetMapper(aTrianglemapHog);
      aTrianglehogActor.GetProperty().SetColor(0,1,0);
      aTriangleGlyph3D = new vtkGlyph3D();
      aTriangleGlyph3D.SetInputConnection(aTriangleCenters.GetOutputPort());
      aTriangleGlyph3D.SetSourceConnection(ball.GetOutputPort());
      aTriangleCentersMapper = vtkPolyDataMapper.New();
      aTriangleCentersMapper.SetInputConnection(aTriangleGlyph3D.GetOutputPort());
      aTriangleCentersActor = new vtkActor();
      aTriangleCentersActor.SetMapper(aTriangleCentersMapper);
      aTrianglehogActor.SetPosition(aTriangleActor.GetPosition()[0],aTriangleActor.GetPosition()[1],aTriangleActor.GetPosition()[2]);
      ren1.AddActor((vtkProp)aTrianglehogActor);
      aTrianglehogActor.GetProperty().SetRepresentationToWireframe();
      
aTriangleStripderivs = new vtkCellDerivatives();
aTriangleStripderivs.SetInputData(aTriangleStripGrid);
      aTriangleStripderivs.SetVectorModeToComputeGradient();
      FileName = dir;
      FileName += "/aTriangleStrip";
      FileName += ".vtk";
      // make sure the directory is writeable first[]
      tryWorked = false;
      try
      {
         channel = new StreamWriter("" + (dir.ToString()) + "/test.tmp");
         tryWorked = true;
      }
      catch(Exception)
        {
        tryWorked = false;
        }
      if(tryWorked)
        {
          channel.Close();
          File.Delete("" + (dir.ToString()) + "/test.tmp");
          aTriangleStripWriter = new vtkUnstructuredGridWriter();
          aTriangleStripWriter.SetInputConnection(aTriangleStripderivs.GetOutputPort());
          aTriangleStripWriter.SetFileName(FileName);
          aTriangleStripWriter.Write();
          // delete the file[]
          File.Delete("FileName");
        }

      
      aTriangleStripCenters = new vtkCellCenters();
      aTriangleStripCenters.SetInputConnection(aTriangleStripderivs.GetOutputPort());
      aTriangleStripCenters.VertexCellsOn();
      aTriangleStriphog = new vtkHedgeHog();
      aTriangleStriphog.SetInputConnection(aTriangleStripCenters.GetOutputPort());
      aTriangleStripmapHog = vtkPolyDataMapper.New();
      aTriangleStripmapHog.SetInputConnection(aTriangleStriphog.GetOutputPort());
      aTriangleStripmapHog.SetScalarModeToUseCellData();
      aTriangleStripmapHog.ScalarVisibilityOff();
      aTriangleStriphogActor = new vtkActor();
      aTriangleStriphogActor.SetMapper(aTriangleStripmapHog);
      aTriangleStriphogActor.GetProperty().SetColor(0,1,0);
      aTriangleStripGlyph3D = new vtkGlyph3D();
      aTriangleStripGlyph3D.SetInputConnection(aTriangleStripCenters.GetOutputPort());
      aTriangleStripGlyph3D.SetSourceConnection(ball.GetOutputPort());
      aTriangleStripCentersMapper = vtkPolyDataMapper.New();
      aTriangleStripCentersMapper.SetInputConnection(aTriangleStripGlyph3D.GetOutputPort());
      aTriangleStripCentersActor = new vtkActor();
      aTriangleStripCentersActor.SetMapper(aTriangleStripCentersMapper);
      aTriangleStriphogActor.SetPosition(aTriangleStripActor.GetPosition()[0],aTriangleStripActor.GetPosition()[1],aTriangleStripActor.GetPosition()[2]);
      ren1.AddActor((vtkProp)aTriangleStriphogActor);
      aTriangleStriphogActor.GetProperty().SetRepresentationToWireframe();
      
aLinederivs = new vtkCellDerivatives();
aLinederivs.SetInputData(aLineGrid);
      aLinederivs.SetVectorModeToComputeGradient();
      FileName = dir;
      FileName += "/aLine";
      FileName += ".vtk";
      // make sure the directory is writeable first[]
      tryWorked = false;
      try
      {
         channel = new StreamWriter("" + (dir.ToString()) + "/test.tmp");
         tryWorked = true;
      }
      catch(Exception)
        {
        tryWorked = false;
        }
      if(tryWorked)
        {
          channel.Close();
          File.Delete("" + (dir.ToString()) + "/test.tmp");
          aLineWriter = new vtkUnstructuredGridWriter();
          aLineWriter.SetInputConnection(aLinederivs.GetOutputPort());
          aLineWriter.SetFileName(FileName);
          aLineWriter.Write();
          // delete the file[]
          File.Delete("FileName");
        }

      
      aLineCenters = new vtkCellCenters();
      aLineCenters.SetInputConnection(aLinederivs.GetOutputPort());
      aLineCenters.VertexCellsOn();
      aLinehog = new vtkHedgeHog();
      aLinehog.SetInputConnection(aLineCenters.GetOutputPort());
      aLinemapHog = vtkPolyDataMapper.New();
      aLinemapHog.SetInputConnection(aLinehog.GetOutputPort());
      aLinemapHog.SetScalarModeToUseCellData();
      aLinemapHog.ScalarVisibilityOff();
      aLinehogActor = new vtkActor();
      aLinehogActor.SetMapper(aLinemapHog);
      aLinehogActor.GetProperty().SetColor(0,1,0);
      aLineGlyph3D = new vtkGlyph3D();
      aLineGlyph3D.SetInputConnection(aLineCenters.GetOutputPort());
      aLineGlyph3D.SetSourceConnection(ball.GetOutputPort());
      aLineCentersMapper = vtkPolyDataMapper.New();
      aLineCentersMapper.SetInputConnection(aLineGlyph3D.GetOutputPort());
      aLineCentersActor = new vtkActor();
      aLineCentersActor.SetMapper(aLineCentersMapper);
      aLinehogActor.SetPosition(aLineActor.GetPosition()[0],aLineActor.GetPosition()[1],aLineActor.GetPosition()[2]);
      ren1.AddActor((vtkProp)aLinehogActor);
      aLinehogActor.GetProperty().SetRepresentationToWireframe();
      
aPolyLinederivs = new vtkCellDerivatives();
aPolyLinederivs.SetInputData(aPolyLineGrid);
      aPolyLinederivs.SetVectorModeToComputeGradient();
      FileName = dir;
      FileName += "/aPolyLine";
      FileName += ".vtk";
      // make sure the directory is writeable first[]
      tryWorked = false;
      try
      {
         channel = new StreamWriter("" + (dir.ToString()) + "/test.tmp");
         tryWorked = true;
      }
      catch(Exception)
        {
        tryWorked = false;
        }
      if(tryWorked)
        {
          channel.Close();
          File.Delete("" + (dir.ToString()) + "/test.tmp");
          aPolyLineWriter = new vtkUnstructuredGridWriter();
          aPolyLineWriter.SetInputConnection(aPolyLinederivs.GetOutputPort());
          aPolyLineWriter.SetFileName(FileName);
          aPolyLineWriter.Write();
          // delete the file[]
          File.Delete("FileName");
        }

      
      aPolyLineCenters = new vtkCellCenters();
      aPolyLineCenters.SetInputConnection(aPolyLinederivs.GetOutputPort());
      aPolyLineCenters.VertexCellsOn();
      aPolyLinehog = new vtkHedgeHog();
      aPolyLinehog.SetInputConnection(aPolyLineCenters.GetOutputPort());
      aPolyLinemapHog = vtkPolyDataMapper.New();
      aPolyLinemapHog.SetInputConnection(aPolyLinehog.GetOutputPort());
      aPolyLinemapHog.SetScalarModeToUseCellData();
      aPolyLinemapHog.ScalarVisibilityOff();
      aPolyLinehogActor = new vtkActor();
      aPolyLinehogActor.SetMapper(aPolyLinemapHog);
      aPolyLinehogActor.GetProperty().SetColor(0,1,0);
      aPolyLineGlyph3D = new vtkGlyph3D();
      aPolyLineGlyph3D.SetInputConnection(aPolyLineCenters.GetOutputPort());
      aPolyLineGlyph3D.SetSourceConnection(ball.GetOutputPort());
      aPolyLineCentersMapper = vtkPolyDataMapper.New();
      aPolyLineCentersMapper.SetInputConnection(aPolyLineGlyph3D.GetOutputPort());
      aPolyLineCentersActor = new vtkActor();
      aPolyLineCentersActor.SetMapper(aPolyLineCentersMapper);
      aPolyLinehogActor.SetPosition(aPolyLineActor.GetPosition()[0],aPolyLineActor.GetPosition()[1],aPolyLineActor.GetPosition()[2]);
      ren1.AddActor((vtkProp)aPolyLinehogActor);
      aPolyLinehogActor.GetProperty().SetRepresentationToWireframe();
      
aVertexderivs = new vtkCellDerivatives();
aVertexderivs.SetInputData(aVertexGrid);
      aVertexderivs.SetVectorModeToComputeGradient();
      FileName = dir;
      FileName += "/aVertex";
      FileName += ".vtk";
      // make sure the directory is writeable first[]
      tryWorked = false;
      try
      {
         channel = new StreamWriter("" + (dir.ToString()) + "/test.tmp");
         tryWorked = true;
      }
      catch(Exception)
        {
        tryWorked = false;
        }
      if(tryWorked)
        {
          channel.Close();
          File.Delete("" + (dir.ToString()) + "/test.tmp");
          aVertexWriter = new vtkUnstructuredGridWriter();
          aVertexWriter.SetInputConnection(aVertexderivs.GetOutputPort());
          aVertexWriter.SetFileName(FileName);
          aVertexWriter.Write();
          // delete the file[]
          File.Delete("FileName");
        }

      
      aVertexCenters = new vtkCellCenters();
      aVertexCenters.SetInputConnection(aVertexderivs.GetOutputPort());
      aVertexCenters.VertexCellsOn();
      aVertexhog = new vtkHedgeHog();
      aVertexhog.SetInputConnection(aVertexCenters.GetOutputPort());
      aVertexmapHog = vtkPolyDataMapper.New();
      aVertexmapHog.SetInputConnection(aVertexhog.GetOutputPort());
      aVertexmapHog.SetScalarModeToUseCellData();
      aVertexmapHog.ScalarVisibilityOff();
      aVertexhogActor = new vtkActor();
      aVertexhogActor.SetMapper(aVertexmapHog);
      aVertexhogActor.GetProperty().SetColor(0,1,0);
      aVertexGlyph3D = new vtkGlyph3D();
      aVertexGlyph3D.SetInputConnection(aVertexCenters.GetOutputPort());
      aVertexGlyph3D.SetSourceConnection(ball.GetOutputPort());
      aVertexCentersMapper = vtkPolyDataMapper.New();
      aVertexCentersMapper.SetInputConnection(aVertexGlyph3D.GetOutputPort());
      aVertexCentersActor = new vtkActor();
      aVertexCentersActor.SetMapper(aVertexCentersMapper);
      aVertexhogActor.SetPosition(aVertexActor.GetPosition()[0],aVertexActor.GetPosition()[1],aVertexActor.GetPosition()[2]);
      ren1.AddActor((vtkProp)aVertexhogActor);
      aVertexhogActor.GetProperty().SetRepresentationToWireframe();
      
aPolyVertexderivs = new vtkCellDerivatives();
aPolyVertexderivs.SetInputData(aPolyVertexGrid);
      aPolyVertexderivs.SetVectorModeToComputeGradient();
      FileName = dir;
      FileName += "/aPolyVertex";
      FileName += ".vtk";
      // make sure the directory is writeable first[]
      tryWorked = false;
      try
      {
         channel = new StreamWriter("" + (dir.ToString()) + "/test.tmp");
         tryWorked = true;
      }
      catch(Exception)
        {
        tryWorked = false;
        }
      if(tryWorked)
        {
          channel.Close();
          File.Delete("" + (dir.ToString()) + "/test.tmp");
          aPolyVertexWriter = new vtkUnstructuredGridWriter();
          aPolyVertexWriter.SetInputConnection(aPolyVertexderivs.GetOutputPort());
          aPolyVertexWriter.SetFileName(FileName);
          aPolyVertexWriter.Write();
          // delete the file[]
          File.Delete("FileName");
        }

      
      aPolyVertexCenters = new vtkCellCenters();
      aPolyVertexCenters.SetInputConnection(aPolyVertexderivs.GetOutputPort());
      aPolyVertexCenters.VertexCellsOn();
      aPolyVertexhog = new vtkHedgeHog();
      aPolyVertexhog.SetInputConnection(aPolyVertexCenters.GetOutputPort());
      aPolyVertexmapHog = vtkPolyDataMapper.New();
      aPolyVertexmapHog.SetInputConnection(aPolyVertexhog.GetOutputPort());
      aPolyVertexmapHog.SetScalarModeToUseCellData();
      aPolyVertexmapHog.ScalarVisibilityOff();
      aPolyVertexhogActor = new vtkActor();
      aPolyVertexhogActor.SetMapper(aPolyVertexmapHog);
      aPolyVertexhogActor.GetProperty().SetColor(0,1,0);
      aPolyVertexGlyph3D = new vtkGlyph3D();
      aPolyVertexGlyph3D.SetInputConnection(aPolyVertexCenters.GetOutputPort());
      aPolyVertexGlyph3D.SetSourceConnection(ball.GetOutputPort());
      aPolyVertexCentersMapper = vtkPolyDataMapper.New();
      aPolyVertexCentersMapper.SetInputConnection(aPolyVertexGlyph3D.GetOutputPort());
      aPolyVertexCentersActor = new vtkActor();
      aPolyVertexCentersActor.SetMapper(aPolyVertexCentersMapper);
      aPolyVertexhogActor.SetPosition(aPolyVertexActor.GetPosition()[0],aPolyVertexActor.GetPosition()[1],aPolyVertexActor.GetPosition()[2]);
      ren1.AddActor((vtkProp)aPolyVertexhogActor);
      aPolyVertexhogActor.GetProperty().SetRepresentationToWireframe();
      
aPixelderivs = new vtkCellDerivatives();
aPixelderivs.SetInputData(aPixelGrid);
      aPixelderivs.SetVectorModeToComputeGradient();
      FileName = dir;
      FileName += "/aPixel";
      FileName += ".vtk";
      // make sure the directory is writeable first[]
      tryWorked = false;
      try
      {
         channel = new StreamWriter("" + (dir.ToString()) + "/test.tmp");
         tryWorked = true;
      }
      catch(Exception)
        {
        tryWorked = false;
        }
      if(tryWorked)
        {
          channel.Close();
          File.Delete("" + (dir.ToString()) + "/test.tmp");
          aPixelWriter = new vtkUnstructuredGridWriter();
          aPixelWriter.SetInputConnection(aPixelderivs.GetOutputPort());
          aPixelWriter.SetFileName(FileName);
          aPixelWriter.Write();
          // delete the file[]
          File.Delete("FileName");
        }

      
      aPixelCenters = new vtkCellCenters();
      aPixelCenters.SetInputConnection(aPixelderivs.GetOutputPort());
      aPixelCenters.VertexCellsOn();
      aPixelhog = new vtkHedgeHog();
      aPixelhog.SetInputConnection(aPixelCenters.GetOutputPort());
      aPixelmapHog = vtkPolyDataMapper.New();
      aPixelmapHog.SetInputConnection(aPixelhog.GetOutputPort());
      aPixelmapHog.SetScalarModeToUseCellData();
      aPixelmapHog.ScalarVisibilityOff();
      aPixelhogActor = new vtkActor();
      aPixelhogActor.SetMapper(aPixelmapHog);
      aPixelhogActor.GetProperty().SetColor(0,1,0);
      aPixelGlyph3D = new vtkGlyph3D();
      aPixelGlyph3D.SetInputConnection(aPixelCenters.GetOutputPort());
      aPixelGlyph3D.SetSourceConnection(ball.GetOutputPort());
      aPixelCentersMapper = vtkPolyDataMapper.New();
      aPixelCentersMapper.SetInputConnection(aPixelGlyph3D.GetOutputPort());
      aPixelCentersActor = new vtkActor();
      aPixelCentersActor.SetMapper(aPixelCentersMapper);
      aPixelhogActor.SetPosition(aPixelActor.GetPosition()[0],aPixelActor.GetPosition()[1],aPixelActor.GetPosition()[2]);
      ren1.AddActor((vtkProp)aPixelhogActor);
      aPixelhogActor.GetProperty().SetRepresentationToWireframe();
      
aPolygonderivs = new vtkCellDerivatives();
aPolygonderivs.SetInputData(aPolygonGrid);
      aPolygonderivs.SetVectorModeToComputeGradient();
      FileName = dir;
      FileName += "/aPolygon";
      FileName += ".vtk";
      // make sure the directory is writeable first[]
      tryWorked = false;
      try
      {
         channel = new StreamWriter("" + (dir.ToString()) + "/test.tmp");
         tryWorked = true;
      }
      catch(Exception)
        {
        tryWorked = false;
        }
      if(tryWorked)
        {
          channel.Close();
          File.Delete("" + (dir.ToString()) + "/test.tmp");
          aPolygonWriter = new vtkUnstructuredGridWriter();
          aPolygonWriter.SetInputConnection(aPolygonderivs.GetOutputPort());
          aPolygonWriter.SetFileName(FileName);
          aPolygonWriter.Write();
          // delete the file[]
          File.Delete("FileName");
        }

      
      aPolygonCenters = new vtkCellCenters();
      aPolygonCenters.SetInputConnection(aPolygonderivs.GetOutputPort());
      aPolygonCenters.VertexCellsOn();
      aPolygonhog = new vtkHedgeHog();
      aPolygonhog.SetInputConnection(aPolygonCenters.GetOutputPort());
      aPolygonmapHog = vtkPolyDataMapper.New();
      aPolygonmapHog.SetInputConnection(aPolygonhog.GetOutputPort());
      aPolygonmapHog.SetScalarModeToUseCellData();
      aPolygonmapHog.ScalarVisibilityOff();
      aPolygonhogActor = new vtkActor();
      aPolygonhogActor.SetMapper(aPolygonmapHog);
      aPolygonhogActor.GetProperty().SetColor(0,1,0);
      aPolygonGlyph3D = new vtkGlyph3D();
      aPolygonGlyph3D.SetInputConnection(aPolygonCenters.GetOutputPort());
      aPolygonGlyph3D.SetSourceConnection(ball.GetOutputPort());
      aPolygonCentersMapper = vtkPolyDataMapper.New();
      aPolygonCentersMapper.SetInputConnection(aPolygonGlyph3D.GetOutputPort());
      aPolygonCentersActor = new vtkActor();
      aPolygonCentersActor.SetMapper(aPolygonCentersMapper);
      aPolygonhogActor.SetPosition(aPolygonActor.GetPosition()[0],aPolygonActor.GetPosition()[1],aPolygonActor.GetPosition()[2]);
      ren1.AddActor((vtkProp)aPolygonhogActor);
      aPolygonhogActor.GetProperty().SetRepresentationToWireframe();
      
aPentaderivs = new vtkCellDerivatives();
aPentaderivs.SetInputData(aPentaGrid);
      aPentaderivs.SetVectorModeToComputeGradient();
      FileName = dir;
      FileName += "/aPenta";
      FileName += ".vtk";
      // make sure the directory is writeable first[]
      tryWorked = false;
      try
      {
         channel = new StreamWriter("" + (dir.ToString()) + "/test.tmp");
         tryWorked = true;
      }
      catch(Exception)
        {
        tryWorked = false;
        }
      if(tryWorked)
        {
          channel.Close();
          File.Delete("" + (dir.ToString()) + "/test.tmp");
          aPentaWriter = new vtkUnstructuredGridWriter();
          aPentaWriter.SetInputConnection(aPentaderivs.GetOutputPort());
          aPentaWriter.SetFileName(FileName);
          aPentaWriter.Write();
          // delete the file[]
          File.Delete("FileName");
        }

      
      aPentaCenters = new vtkCellCenters();
      aPentaCenters.SetInputConnection(aPentaderivs.GetOutputPort());
      aPentaCenters.VertexCellsOn();
      aPentahog = new vtkHedgeHog();
      aPentahog.SetInputConnection(aPentaCenters.GetOutputPort());
      aPentamapHog = vtkPolyDataMapper.New();
      aPentamapHog.SetInputConnection(aPentahog.GetOutputPort());
      aPentamapHog.SetScalarModeToUseCellData();
      aPentamapHog.ScalarVisibilityOff();
      aPentahogActor = new vtkActor();
      aPentahogActor.SetMapper(aPentamapHog);
      aPentahogActor.GetProperty().SetColor(0,1,0);
      aPentaGlyph3D = new vtkGlyph3D();
      aPentaGlyph3D.SetInputConnection(aPentaCenters.GetOutputPort());
      aPentaGlyph3D.SetSourceConnection(ball.GetOutputPort());
      aPentaCentersMapper = vtkPolyDataMapper.New();
      aPentaCentersMapper.SetInputConnection(aPentaGlyph3D.GetOutputPort());
      aPentaCentersActor = new vtkActor();
      aPentaCentersActor.SetMapper(aPentaCentersMapper);
      aPentahogActor.SetPosition(aPentaActor.GetPosition()[0],aPentaActor.GetPosition()[1],aPentaActor.GetPosition()[2]);
      ren1.AddActor((vtkProp)aPentahogActor);
      aPentahogActor.GetProperty().SetRepresentationToWireframe();
      
aHexaderivs = new vtkCellDerivatives();
aHexaderivs.SetInputData(aHexaGrid);
      aHexaderivs.SetVectorModeToComputeGradient();
      FileName = dir;
      FileName += "/aHexa";
      FileName += ".vtk";
      // make sure the directory is writeable first[]
      tryWorked = false;
      try
      {
         channel = new StreamWriter("" + (dir.ToString()) + "/test.tmp");
         tryWorked = true;
      }
      catch(Exception)
        {
        tryWorked = false;
        }
      if(tryWorked)
        {
          channel.Close();
          File.Delete("" + (dir.ToString()) + "/test.tmp");
          aHexaWriter = new vtkUnstructuredGridWriter();
          aHexaWriter.SetInputConnection(aHexaderivs.GetOutputPort());
          aHexaWriter.SetFileName(FileName);
          aHexaWriter.Write();
          // delete the file[]
          File.Delete("FileName");
        }

      
      aHexaCenters = new vtkCellCenters();
      aHexaCenters.SetInputConnection(aHexaderivs.GetOutputPort());
      aHexaCenters.VertexCellsOn();
      aHexahog = new vtkHedgeHog();
      aHexahog.SetInputConnection(aHexaCenters.GetOutputPort());
      aHexamapHog = vtkPolyDataMapper.New();
      aHexamapHog.SetInputConnection(aHexahog.GetOutputPort());
      aHexamapHog.SetScalarModeToUseCellData();
      aHexamapHog.ScalarVisibilityOff();
      aHexahogActor = new vtkActor();
      aHexahogActor.SetMapper(aHexamapHog);
      aHexahogActor.GetProperty().SetColor(0,1,0);
      aHexaGlyph3D = new vtkGlyph3D();
      aHexaGlyph3D.SetInputConnection(aHexaCenters.GetOutputPort());
      aHexaGlyph3D.SetSourceConnection(ball.GetOutputPort());
      aHexaCentersMapper = vtkPolyDataMapper.New();
      aHexaCentersMapper.SetInputConnection(aHexaGlyph3D.GetOutputPort());
      aHexaCentersActor = new vtkActor();
      aHexaCentersActor.SetMapper(aHexaCentersMapper);
      aHexahogActor.SetPosition(aHexaActor.GetPosition()[0],aHexaActor.GetPosition()[1],aHexaActor.GetPosition()[2]);
      ren1.AddActor((vtkProp)aHexahogActor);
      aHexahogActor.GetProperty().SetRepresentationToWireframe();
      

      


  ren1.ResetCamera();
  ren1.GetActiveCamera().Azimuth((double)30);
  ren1.GetActiveCamera().Elevation((double)20);
  ren1.GetActiveCamera().Dolly((double)3.0);
  ren1.ResetCameraClippingRange();
  renWin.SetSize((int)300,(int)150);
  renWin.Render();
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
static vtkFloatArray triangleTCoords;
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
static vtkFloatArray triangleStripTCoords;
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
static vtkSphereSource ball;
static long N;
static int i;
static string dir;
static string FileName;
static StreamWriter channel;
static vtkFloatArray aVoxelScalars;
static vtkFloatArray aVoxelScalar;

static vtkFloatArray aHexahedronScalars;
static vtkFloatArray aHexahedronScalar;

static vtkFloatArray aWedgeScalars;
static vtkFloatArray aWedgeScalar;

static vtkFloatArray aPyramidScalars;
static vtkFloatArray aPyramidScalar;

static vtkFloatArray aTetraScalars;
static vtkFloatArray aTetraScalar;

static vtkFloatArray aQuadScalars;
static vtkFloatArray aQuadScalar;

static vtkFloatArray aTriangleScalars;
static vtkFloatArray aTriangleScalar;

static vtkFloatArray aTriangleStripScalars;
static vtkFloatArray aTriangleStripScalar;

static vtkFloatArray aLineScalars;
static vtkFloatArray aLineScalar;

static vtkFloatArray aPolyLineScalars;
static vtkFloatArray aPolyLineScalar;

static vtkFloatArray aVertexScalars;
static vtkFloatArray aVertexScalar;

static vtkFloatArray aPolyVertexScalars;
static vtkFloatArray aPolyVertexScalar;

static vtkFloatArray aPixelScalars;
static vtkFloatArray aPixelScalar;

static vtkFloatArray aPolygonScalars;
static vtkFloatArray aPolygonScalar;

static vtkFloatArray aPentaScalars;
static vtkFloatArray aPentaScalar;

static vtkFloatArray aHexaScalars;
static vtkFloatArray aHexaScalar;

static vtkCellDerivatives aVoxelderivs;
static vtkUnstructuredGridWriter aVoxelWriter;
static vtkCellCenters aVoxelCenters;
static vtkHedgeHog aVoxelhog;
static vtkPolyDataMapper aVoxelmapHog;
static vtkActor aVoxelhogActor;
static vtkGlyph3D aVoxelGlyph3D;
static vtkPolyDataMapper aVoxelCentersMapper;
static vtkActor aVoxelCentersActor;

static vtkCellDerivatives aHexahedronderivs;
static vtkUnstructuredGridWriter aHexahedronWriter;
static vtkCellCenters aHexahedronCenters;
static vtkHedgeHog aHexahedronhog;
static vtkPolyDataMapper aHexahedronmapHog;
static vtkActor aHexahedronhogActor;
static vtkGlyph3D aHexahedronGlyph3D;
static vtkPolyDataMapper aHexahedronCentersMapper;
static vtkActor aHexahedronCentersActor;

static vtkCellDerivatives aWedgederivs;
static vtkUnstructuredGridWriter aWedgeWriter;
static vtkCellCenters aWedgeCenters;
static vtkHedgeHog aWedgehog;
static vtkPolyDataMapper aWedgemapHog;
static vtkActor aWedgehogActor;
static vtkGlyph3D aWedgeGlyph3D;
static vtkPolyDataMapper aWedgeCentersMapper;
static vtkActor aWedgeCentersActor;

static vtkCellDerivatives aPyramidderivs;
static vtkUnstructuredGridWriter aPyramidWriter;
static vtkCellCenters aPyramidCenters;
static vtkHedgeHog aPyramidhog;
static vtkPolyDataMapper aPyramidmapHog;
static vtkActor aPyramidhogActor;
static vtkGlyph3D aPyramidGlyph3D;
static vtkPolyDataMapper aPyramidCentersMapper;
static vtkActor aPyramidCentersActor;

static vtkCellDerivatives aTetraderivs;
static vtkUnstructuredGridWriter aTetraWriter;
static vtkCellCenters aTetraCenters;
static vtkHedgeHog aTetrahog;
static vtkPolyDataMapper aTetramapHog;
static vtkActor aTetrahogActor;
static vtkGlyph3D aTetraGlyph3D;
static vtkPolyDataMapper aTetraCentersMapper;
static vtkActor aTetraCentersActor;

static vtkCellDerivatives aQuadderivs;
static vtkUnstructuredGridWriter aQuadWriter;
static vtkCellCenters aQuadCenters;
static vtkHedgeHog aQuadhog;
static vtkPolyDataMapper aQuadmapHog;
static vtkActor aQuadhogActor;
static vtkGlyph3D aQuadGlyph3D;
static vtkPolyDataMapper aQuadCentersMapper;
static vtkActor aQuadCentersActor;

static vtkCellDerivatives aTrianglederivs;
static vtkUnstructuredGridWriter aTriangleWriter;
static vtkCellCenters aTriangleCenters;
static vtkHedgeHog aTrianglehog;
static vtkPolyDataMapper aTrianglemapHog;
static vtkActor aTrianglehogActor;
static vtkGlyph3D aTriangleGlyph3D;
static vtkPolyDataMapper aTriangleCentersMapper;
static vtkActor aTriangleCentersActor;

static vtkCellDerivatives aTriangleStripderivs;
static vtkUnstructuredGridWriter aTriangleStripWriter;
static vtkCellCenters aTriangleStripCenters;
static vtkHedgeHog aTriangleStriphog;
static vtkPolyDataMapper aTriangleStripmapHog;
static vtkActor aTriangleStriphogActor;
static vtkGlyph3D aTriangleStripGlyph3D;
static vtkPolyDataMapper aTriangleStripCentersMapper;
static vtkActor aTriangleStripCentersActor;

static vtkCellDerivatives aLinederivs;
static vtkUnstructuredGridWriter aLineWriter;
static vtkCellCenters aLineCenters;
static vtkHedgeHog aLinehog;
static vtkPolyDataMapper aLinemapHog;
static vtkActor aLinehogActor;
static vtkGlyph3D aLineGlyph3D;
static vtkPolyDataMapper aLineCentersMapper;
static vtkActor aLineCentersActor;

static vtkCellDerivatives aPolyLinederivs;
static vtkUnstructuredGridWriter aPolyLineWriter;
static vtkCellCenters aPolyLineCenters;
static vtkHedgeHog aPolyLinehog;
static vtkPolyDataMapper aPolyLinemapHog;
static vtkActor aPolyLinehogActor;
static vtkGlyph3D aPolyLineGlyph3D;
static vtkPolyDataMapper aPolyLineCentersMapper;
static vtkActor aPolyLineCentersActor;

static vtkCellDerivatives aVertexderivs;
static vtkUnstructuredGridWriter aVertexWriter;
static vtkCellCenters aVertexCenters;
static vtkHedgeHog aVertexhog;
static vtkPolyDataMapper aVertexmapHog;
static vtkActor aVertexhogActor;
static vtkGlyph3D aVertexGlyph3D;
static vtkPolyDataMapper aVertexCentersMapper;
static vtkActor aVertexCentersActor;

static vtkCellDerivatives aPolyVertexderivs;
static vtkUnstructuredGridWriter aPolyVertexWriter;
static vtkCellCenters aPolyVertexCenters;
static vtkHedgeHog aPolyVertexhog;
static vtkPolyDataMapper aPolyVertexmapHog;
static vtkActor aPolyVertexhogActor;
static vtkGlyph3D aPolyVertexGlyph3D;
static vtkPolyDataMapper aPolyVertexCentersMapper;
static vtkActor aPolyVertexCentersActor;

static vtkCellDerivatives aPixelderivs;
static vtkUnstructuredGridWriter aPixelWriter;
static vtkCellCenters aPixelCenters;
static vtkHedgeHog aPixelhog;
static vtkPolyDataMapper aPixelmapHog;
static vtkActor aPixelhogActor;
static vtkGlyph3D aPixelGlyph3D;
static vtkPolyDataMapper aPixelCentersMapper;
static vtkActor aPixelCentersActor;

static vtkCellDerivatives aPolygonderivs;
static vtkUnstructuredGridWriter aPolygonWriter;
static vtkCellCenters aPolygonCenters;
static vtkHedgeHog aPolygonhog;
static vtkPolyDataMapper aPolygonmapHog;
static vtkActor aPolygonhogActor;
static vtkGlyph3D aPolygonGlyph3D;
static vtkPolyDataMapper aPolygonCentersMapper;
static vtkActor aPolygonCentersActor;

static vtkCellDerivatives aPentaderivs;
static vtkUnstructuredGridWriter aPentaWriter;
static vtkCellCenters aPentaCenters;
static vtkHedgeHog aPentahog;
static vtkPolyDataMapper aPentamapHog;
static vtkActor aPentahogActor;
static vtkGlyph3D aPentaGlyph3D;
static vtkPolyDataMapper aPentaCentersMapper;
static vtkActor aPentaCentersActor;

static vtkCellDerivatives aHexaderivs;
static vtkUnstructuredGridWriter aHexaWriter;
static vtkCellCenters aHexaCenters;
static vtkHedgeHog aHexahog;
static vtkPolyDataMapper aHexamapHog;
static vtkActor aHexahogActor;
static vtkGlyph3D aHexaGlyph3D;
static vtkPolyDataMapper aHexaCentersMapper;
static vtkActor aHexaCentersActor;
     


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
        public static vtkFloatArray GettriangleTCoords()
        {
            return triangleTCoords;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SettriangleTCoords(vtkFloatArray toSet)
        {
            triangleTCoords = toSet;
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
        public static vtkFloatArray GettriangleStripTCoords()
        {
            return triangleStripTCoords;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SettriangleStripTCoords(vtkFloatArray toSet)
        {
            triangleStripTCoords = toSet;
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
        public static vtkSphereSource Getball()
        {
            return ball;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setball(vtkSphereSource toSet)
        {
            ball = toSet;
        }
        
        
        ///<summary> A Get Method for Static Variables </summary>
        public static long GetN()
        {
            return N;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetN(long toSet)
        {
            N = toSet;
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
        public static string Getdir()
        {
            return dir;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setdir(string toSet)
        {
            dir = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static string GetFileName()
        {
            return FileName;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetFileName(string toSet)
        {
            FileName = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static StreamWriter Getchannel()
        {
            return channel;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setchannel(StreamWriter toSet)
        {
            channel = toSet;
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
  	if(triangleTCoords!= null){triangleTCoords.Dispose();}
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
  	if(triangleStripTCoords!= null){triangleStripTCoords.Dispose();}
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
  	if(ball!= null){ball.Dispose();

      aVoxelScalars.Dispose();
      aVoxelScalar.Dispose();
      
      aHexahedronScalars.Dispose();
      aHexahedronScalar.Dispose();
      
      aWedgeScalars.Dispose();
      aWedgeScalar.Dispose();
      
      aPyramidScalars.Dispose();
      aPyramidScalar.Dispose();
      
      aTetraScalars.Dispose();
      aTetraScalar.Dispose();
      
      aQuadScalars.Dispose();
      aQuadScalar.Dispose();
      
      aTriangleScalars.Dispose();
      aTriangleScalar.Dispose();
      
      aTriangleStripScalars.Dispose();
      aTriangleStripScalar.Dispose();
      
      aLineScalars.Dispose();
      aLineScalar.Dispose();
      
      aPolyLineScalars.Dispose();
      aPolyLineScalar.Dispose();
      
      aVertexScalars.Dispose();
      aVertexScalar.Dispose();
      
      aPolyVertexScalars.Dispose();
      aPolyVertexScalar.Dispose();
      
      aPixelScalars.Dispose();
      aPixelScalar.Dispose();
      
      aPolygonScalars.Dispose();
      aPolygonScalar.Dispose();
      
      aPentaScalars.Dispose();
      aPentaScalar.Dispose();
      
      aHexaScalars.Dispose();
      aHexaScalar.Dispose();
      
      aVoxelderivs.Dispose();
      aVoxelWriter.Dispose(); 
      aVoxelCenters.Dispose();
      aVoxelhog.Dispose();
      aVoxelmapHog.Dispose();
      aVoxelhogActor.Dispose();
      aVoxelGlyph3D.Dispose();
      aVoxelCentersMapper.Dispose();
      aVoxelCentersActor.Dispose();
      
      aHexahedronderivs.Dispose();
      aHexahedronWriter.Dispose(); 
      aHexahedronCenters.Dispose();
      aHexahedronhog.Dispose();
      aHexahedronmapHog.Dispose();
      aHexahedronhogActor.Dispose();
      aHexahedronGlyph3D.Dispose();
      aHexahedronCentersMapper.Dispose();
      aHexahedronCentersActor.Dispose();
      
      aWedgederivs.Dispose();
      aWedgeWriter.Dispose(); 
      aWedgeCenters.Dispose();
      aWedgehog.Dispose();
      aWedgemapHog.Dispose();
      aWedgehogActor.Dispose();
      aWedgeGlyph3D.Dispose();
      aWedgeCentersMapper.Dispose();
      aWedgeCentersActor.Dispose();
      
      aPyramidderivs.Dispose();
      aPyramidWriter.Dispose(); 
      aPyramidCenters.Dispose();
      aPyramidhog.Dispose();
      aPyramidmapHog.Dispose();
      aPyramidhogActor.Dispose();
      aPyramidGlyph3D.Dispose();
      aPyramidCentersMapper.Dispose();
      aPyramidCentersActor.Dispose();
      
      aTetraderivs.Dispose();
      aTetraWriter.Dispose(); 
      aTetraCenters.Dispose();
      aTetrahog.Dispose();
      aTetramapHog.Dispose();
      aTetrahogActor.Dispose();
      aTetraGlyph3D.Dispose();
      aTetraCentersMapper.Dispose();
      aTetraCentersActor.Dispose();
      
      aQuadderivs.Dispose();
      aQuadWriter.Dispose(); 
      aQuadCenters.Dispose();
      aQuadhog.Dispose();
      aQuadmapHog.Dispose();
      aQuadhogActor.Dispose();
      aQuadGlyph3D.Dispose();
      aQuadCentersMapper.Dispose();
      aQuadCentersActor.Dispose();
      
      aTrianglederivs.Dispose();
      aTriangleWriter.Dispose(); 
      aTriangleCenters.Dispose();
      aTrianglehog.Dispose();
      aTrianglemapHog.Dispose();
      aTrianglehogActor.Dispose();
      aTriangleGlyph3D.Dispose();
      aTriangleCentersMapper.Dispose();
      aTriangleCentersActor.Dispose();
      
      aTriangleStripderivs.Dispose();
      aTriangleStripWriter.Dispose(); 
      aTriangleStripCenters.Dispose();
      aTriangleStriphog.Dispose();
      aTriangleStripmapHog.Dispose();
      aTriangleStriphogActor.Dispose();
      aTriangleStripGlyph3D.Dispose();
      aTriangleStripCentersMapper.Dispose();
      aTriangleStripCentersActor.Dispose();
      
      aLinederivs.Dispose();
      aLineWriter.Dispose(); 
      aLineCenters.Dispose();
      aLinehog.Dispose();
      aLinemapHog.Dispose();
      aLinehogActor.Dispose();
      aLineGlyph3D.Dispose();
      aLineCentersMapper.Dispose();
      aLineCentersActor.Dispose();
      
      aPolyLinederivs.Dispose();
      aPolyLineWriter.Dispose(); 
      aPolyLineCenters.Dispose();
      aPolyLinehog.Dispose();
      aPolyLinemapHog.Dispose();
      aPolyLinehogActor.Dispose();
      aPolyLineGlyph3D.Dispose();
      aPolyLineCentersMapper.Dispose();
      aPolyLineCentersActor.Dispose();
      
      aVertexderivs.Dispose();
      aVertexWriter.Dispose(); 
      aVertexCenters.Dispose();
      aVertexhog.Dispose();
      aVertexmapHog.Dispose();
      aVertexhogActor.Dispose();
      aVertexGlyph3D.Dispose();
      aVertexCentersMapper.Dispose();
      aVertexCentersActor.Dispose();
      
      aPolyVertexderivs.Dispose();
      aPolyVertexWriter.Dispose(); 
      aPolyVertexCenters.Dispose();
      aPolyVertexhog.Dispose();
      aPolyVertexmapHog.Dispose();
      aPolyVertexhogActor.Dispose();
      aPolyVertexGlyph3D.Dispose();
      aPolyVertexCentersMapper.Dispose();
      aPolyVertexCentersActor.Dispose();
      
      aPixelderivs.Dispose();
      aPixelWriter.Dispose(); 
      aPixelCenters.Dispose();
      aPixelhog.Dispose();
      aPixelmapHog.Dispose();
      aPixelhogActor.Dispose();
      aPixelGlyph3D.Dispose();
      aPixelCentersMapper.Dispose();
      aPixelCentersActor.Dispose();
      
      aPolygonderivs.Dispose();
      aPolygonWriter.Dispose(); 
      aPolygonCenters.Dispose();
      aPolygonhog.Dispose();
      aPolygonmapHog.Dispose();
      aPolygonhogActor.Dispose();
      aPolygonGlyph3D.Dispose();
      aPolygonCentersMapper.Dispose();
      aPolygonCentersActor.Dispose();
      
      aPentaderivs.Dispose();
      aPentaWriter.Dispose(); 
      aPentaCenters.Dispose();
      aPentahog.Dispose();
      aPentamapHog.Dispose();
      aPentahogActor.Dispose();
      aPentaGlyph3D.Dispose();
      aPentaCentersMapper.Dispose();
      aPentaCentersActor.Dispose();
      
      aHexaderivs.Dispose();
      aHexaWriter.Dispose(); 
      aHexaCenters.Dispose();
      aHexahog.Dispose();
      aHexamapHog.Dispose();
      aHexahogActor.Dispose();
      aHexaGlyph3D.Dispose();
      aHexaCentersMapper.Dispose();
      aHexaCentersActor.Dispose();    
    }
  }

}
//--- end of script --//

