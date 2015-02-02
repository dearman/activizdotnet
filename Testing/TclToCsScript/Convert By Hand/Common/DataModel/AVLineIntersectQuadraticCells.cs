using Kitware.VTK;
using System;
// input file is C:\VTK\Graphics\Testing\Tcl\LineIntersectQuadraticCells.tcl
// output file is AVLineIntersectQuadraticCells.cs
/// <summary>
/// The testing class derived from AVLineIntersectQuadraticCells
/// </summary>
public class AVLineIntersectQuadraticCellsClass
{
  /// <summary>
  /// The main entry method called by the CSharp driver
  /// </summary>
  /// <param name="argv"></param>
  public static void AVLineIntersectQuadraticCells(String [] argv)
  {
  //Prefix Content is: ""
  
  // Contour every quadratic cell type[]
  // Create a scene with one of each cell type.[]
  // QuadraticEdge[]
  edgePoints = new vtkPoints();
  edgePoints.SetNumberOfPoints((int)3);
  edgePoints.InsertPoint((int)0,(double)0,(double)0,(double)0);
  edgePoints.InsertPoint((int)1,(double)1.0,(double)0,(double)0);
  edgePoints.InsertPoint((int)2,(double)0.5,(double)0.25,(double)0);
  edgeScalars = new vtkFloatArray();
  edgeScalars.SetNumberOfTuples((int)3);
  edgeScalars.InsertValue((int)0,(float)0.0);
  edgeScalars.InsertValue((int)1,(float)0.0);
  edgeScalars.InsertValue((int)2,(float)0.9);
  aEdge = new vtkQuadraticEdge();
  aEdge.GetPointIds().SetId((int)0,(int)0);
  aEdge.GetPointIds().SetId((int)1,(int)1);
  aEdge.GetPointIds().SetId((int)2,(int)2);
  aEdgeGrid = new vtkUnstructuredGrid();
  aEdgeGrid.Allocate((int)1,(int)1);
  aEdgeGrid.InsertNextCell((int)aEdge.GetCellType(),(vtkIdList)aEdge.GetPointIds());
  aEdgeGrid.SetPoints((vtkPoints)edgePoints);
  aEdgeGrid.GetPointData().SetScalars((vtkDataArray)edgeScalars);
  aEdgeMapper = new vtkDataSetMapper();
  aEdgeMapper.SetInputData((vtkDataSet)aEdgeGrid);
  aEdgeMapper.ScalarVisibilityOff();
  aEdgeActor = new vtkActor();
  aEdgeActor.SetMapper((vtkMapper)aEdgeMapper);
  aEdgeActor.GetProperty().SetRepresentationToWireframe();
  aEdgeActor.GetProperty().SetAmbient((double)1.0);
  // Quadratic triangle[]
  triPoints = new vtkPoints();
  triPoints.SetNumberOfPoints((int)6);
  triPoints.InsertPoint((int)0,(double)0.0,(double)0.0,(double)0.0);
  triPoints.InsertPoint((int)1,(double)1.0,(double)0.0,(double)0.0);
  triPoints.InsertPoint((int)2,(double)0.5,(double)0.8,(double)0.0);
  triPoints.InsertPoint((int)3,(double)0.5,(double)0.0,(double)0.0);
  triPoints.InsertPoint((int)4,(double)0.75,(double)0.4,(double)0.0);
  triPoints.InsertPoint((int)5,(double)0.25,(double)0.4,(double)0.0);
  triScalars = new vtkFloatArray();
  triScalars.SetNumberOfTuples((int)6);
  triScalars.InsertValue((int)0,(float)0.0);
  triScalars.InsertValue((int)1,(float)0.0);
  triScalars.InsertValue((int)2,(float)0.0);
  triScalars.InsertValue((int)3,(float)1.0);
  triScalars.InsertValue((int)4,(float)0.0);
  triScalars.InsertValue((int)5,(float)0.0);
  aTri = new vtkQuadraticTriangle();
  aTri.GetPointIds().SetId((int)0,(int)0);
  aTri.GetPointIds().SetId((int)1,(int)1);
  aTri.GetPointIds().SetId((int)2,(int)2);
  aTri.GetPointIds().SetId((int)3,(int)3);
  aTri.GetPointIds().SetId((int)4,(int)4);
  aTri.GetPointIds().SetId((int)5,(int)5);
  aTriGrid = new vtkUnstructuredGrid();
  aTriGrid.Allocate((int)1,(int)1);
  aTriGrid.InsertNextCell((int)aTri.GetCellType(),(vtkIdList)aTri.GetPointIds());
  aTriGrid.SetPoints((vtkPoints)triPoints);
  aTriGrid.GetPointData().SetScalars((vtkDataArray)triScalars);
  aTriMapper = new vtkDataSetMapper();
  aTriMapper.SetInputData((vtkDataSet)aTriGrid);
  aTriMapper.ScalarVisibilityOff();
  aTriActor = new vtkActor();
  aTriActor.SetMapper((vtkMapper)aTriMapper);
  aTriActor.GetProperty().SetRepresentationToWireframe();
  aTriActor.GetProperty().SetAmbient((double)1.0);
  // Quadratic quadrilateral[]
  quadPoints = new vtkPoints();
  quadPoints.SetNumberOfPoints((int)8);
  quadPoints.InsertPoint((int)0,(double)0.0,(double)0.0,(double)0.0);
  quadPoints.InsertPoint((int)1,(double)1.0,(double)0.0,(double)0.0);
  quadPoints.InsertPoint((int)2,(double)1.0,(double)1.0,(double)0.0);
  quadPoints.InsertPoint((int)3,(double)0.0,(double)1.0,(double)0.0);
  quadPoints.InsertPoint((int)4,(double)0.5,(double)0.0,(double)0.0);
  quadPoints.InsertPoint((int)5,(double)1.0,(double)0.5,(double)0.0);
  quadPoints.InsertPoint((int)6,(double)0.5,(double)1.0,(double)0.0);
  quadPoints.InsertPoint((int)7,(double)0.0,(double)0.5,(double)0.0);
  quadScalars = new vtkFloatArray();
  quadScalars.SetNumberOfTuples((int)8);
  quadScalars.InsertValue((int)0,(float)0.0);
  quadScalars.InsertValue((int)1,(float)0.0);
  quadScalars.InsertValue((int)2,(float)1.0);
  quadScalars.InsertValue((int)3,(float)1.0);
  quadScalars.InsertValue((int)4,(float)1.0);
  quadScalars.InsertValue((int)5,(float)0.0);
  quadScalars.InsertValue((int)6,(float)0.0);
  quadScalars.InsertValue((int)7,(float)0.0);
  aQuad = new vtkQuadraticQuad();
  aQuad.GetPointIds().SetId((int)0,(int)0);
  aQuad.GetPointIds().SetId((int)1,(int)1);
  aQuad.GetPointIds().SetId((int)2,(int)2);
  aQuad.GetPointIds().SetId((int)3,(int)3);
  aQuad.GetPointIds().SetId((int)4,(int)4);
  aQuad.GetPointIds().SetId((int)5,(int)5);
  aQuad.GetPointIds().SetId((int)6,(int)6);
  aQuad.GetPointIds().SetId((int)7,(int)7);
  aQuadGrid = new vtkUnstructuredGrid();
  aQuadGrid.Allocate((int)1,(int)1);
  aQuadGrid.InsertNextCell((int)aQuad.GetCellType(),(vtkIdList)aQuad.GetPointIds());
  aQuadGrid.SetPoints((vtkPoints)quadPoints);
  aQuadGrid.GetPointData().SetScalars((vtkDataArray)quadScalars);
  aQuadMapper = new vtkDataSetMapper();
  aQuadMapper.SetInputData((vtkDataSet)aQuadGrid);
  aQuadMapper.ScalarVisibilityOff();
  aQuadActor = new vtkActor();
  aQuadActor.SetMapper((vtkMapper)aQuadMapper);
  aQuadActor.GetProperty().SetRepresentationToWireframe();
  aQuadActor.GetProperty().SetAmbient((double)1.0);
  // Quadratic tetrahedron[]
  tetPoints = new vtkPoints();
  tetPoints.SetNumberOfPoints((int)10);
  tetPoints.InsertPoint((int)0,(double)0.0,(double)0.0,(double)0.0);
  tetPoints.InsertPoint((int)1,(double)1.0,(double)0.0,(double)0.0);
  tetPoints.InsertPoint((int)2,(double)0.5,(double)0.8,(double)0.0);
  tetPoints.InsertPoint((int)3,(double)0.5,(double)0.4,(double)1.0);
  tetPoints.InsertPoint((int)4,(double)0.5,(double)0.0,(double)0.0);
  tetPoints.InsertPoint((int)5,(double)0.75,(double)0.4,(double)0.0);
  tetPoints.InsertPoint((int)6,(double)0.25,(double)0.4,(double)0.0);
  tetPoints.InsertPoint((int)7,(double)0.25,(double)0.2,(double)0.5);
  tetPoints.InsertPoint((int)8,(double)0.75,(double)0.2,(double)0.5);
  tetPoints.InsertPoint((int)9,(double)0.50,(double)0.6,(double)0.5);
  tetScalars = new vtkFloatArray();
  tetScalars.SetNumberOfTuples((int)10);
  tetScalars.InsertValue((int)0,(float)1.0);
  tetScalars.InsertValue((int)1,(float)1.0);
  tetScalars.InsertValue((int)2,(float)1.0);
  tetScalars.InsertValue((int)3,(float)1.0);
  tetScalars.InsertValue((int)4,(float)0.0);
  tetScalars.InsertValue((int)5,(float)0.0);
  tetScalars.InsertValue((int)6,(float)0.0);
  tetScalars.InsertValue((int)7,(float)0.0);
  tetScalars.InsertValue((int)8,(float)0.0);
  tetScalars.InsertValue((int)9,(float)0.0);
  aTet = new vtkQuadraticTetra();
  aTet.GetPointIds().SetId((int)0,(int)0);
  aTet.GetPointIds().SetId((int)1,(int)1);
  aTet.GetPointIds().SetId((int)2,(int)2);
  aTet.GetPointIds().SetId((int)3,(int)3);
  aTet.GetPointIds().SetId((int)4,(int)4);
  aTet.GetPointIds().SetId((int)5,(int)5);
  aTet.GetPointIds().SetId((int)6,(int)6);
  aTet.GetPointIds().SetId((int)7,(int)7);
  aTet.GetPointIds().SetId((int)8,(int)8);
  aTet.GetPointIds().SetId((int)9,(int)9);
  aTetGrid = new vtkUnstructuredGrid();
  aTetGrid.Allocate((int)1,(int)1);
  aTetGrid.InsertNextCell((int)aTet.GetCellType(),(vtkIdList)aTet.GetPointIds());
  aTetGrid.SetPoints((vtkPoints)tetPoints);
  aTetGrid.GetPointData().SetScalars((vtkDataArray)tetScalars);
  aTetMapper = new vtkDataSetMapper();
  aTetMapper.SetInputData((vtkDataSet)aTetGrid);
  aTetMapper.ScalarVisibilityOff();
  aTetActor = new vtkActor();
  aTetActor.SetMapper((vtkMapper)aTetMapper);
  aTetActor.GetProperty().SetRepresentationToWireframe();
  aTetActor.GetProperty().SetAmbient((double)1.0);
  // Quadratic hexahedron[]
  hexPoints = new vtkPoints();
  hexPoints.SetNumberOfPoints((int)20);
  hexPoints.InsertPoint((int)0,(double)0,(double)0,(double)0);
  hexPoints.InsertPoint((int)1,(double)1,(double)0,(double)0);
  hexPoints.InsertPoint((int)2,(double)1,(double)1,(double)0);
  hexPoints.InsertPoint((int)3,(double)0,(double)1,(double)0);
  hexPoints.InsertPoint((int)4,(double)0,(double)0,(double)1);
  hexPoints.InsertPoint((int)5,(double)1,(double)0,(double)1);
  hexPoints.InsertPoint((int)6,(double)1,(double)1,(double)1);
  hexPoints.InsertPoint((int)7,(double)0,(double)1,(double)1);
  hexPoints.InsertPoint((int)8,(double)0.5,(double)0,(double)0);
  hexPoints.InsertPoint((int)9,(double)1,(double)0.5,(double)0);
  hexPoints.InsertPoint((int)10,(double)0.5,(double)1,(double)0);
  hexPoints.InsertPoint((int)11,(double)0,(double)0.5,(double)0);
  hexPoints.InsertPoint((int)12,(double)0.5,(double)0,(double)1);
  hexPoints.InsertPoint((int)13,(double)1,(double)0.5,(double)1);
  hexPoints.InsertPoint((int)14,(double)0.5,(double)1,(double)1);
  hexPoints.InsertPoint((int)15,(double)0,(double)0.5,(double)1);
  hexPoints.InsertPoint((int)16,(double)0,(double)0,(double)0.5);
  hexPoints.InsertPoint((int)17,(double)1,(double)0,(double)0.5);
  hexPoints.InsertPoint((int)18,(double)1,(double)1,(double)0.5);
  hexPoints.InsertPoint((int)19,(double)0,(double)1,(double)0.5);
  hexScalars = new vtkFloatArray();
  hexScalars.SetNumberOfTuples((int)20);
  hexScalars.InsertValue((int)0,(float)1.0);
  hexScalars.InsertValue((int)1,(float)1.0);
  hexScalars.InsertValue((int)2,(float)1.0);
  hexScalars.InsertValue((int)3,(float)1.0);
  hexScalars.InsertValue((int)4,(float)1.0);
  hexScalars.InsertValue((int)5,(float)1.0);
  hexScalars.InsertValue((int)6,(float)1.0);
  hexScalars.InsertValue((int)7,(float)1.0);
  hexScalars.InsertValue((int)8,(float)0.0);
  hexScalars.InsertValue((int)9,(float)0.0);
  hexScalars.InsertValue((int)10,(float)0.0);
  hexScalars.InsertValue((int)11,(float)0.0);
  hexScalars.InsertValue((int)12,(float)0.0);
  hexScalars.InsertValue((int)13,(float)0.0);
  hexScalars.InsertValue((int)14,(float)0.0);
  hexScalars.InsertValue((int)15,(float)0.0);
  hexScalars.InsertValue((int)16,(float)0.0);
  hexScalars.InsertValue((int)17,(float)0.0);
  hexScalars.InsertValue((int)18,(float)0.0);
  hexScalars.InsertValue((int)19,(float)0.0);
  aHex = new vtkQuadraticHexahedron();
  aHex.GetPointIds().SetId((int)0,(int)0);
  aHex.GetPointIds().SetId((int)1,(int)1);
  aHex.GetPointIds().SetId((int)2,(int)2);
  aHex.GetPointIds().SetId((int)3,(int)3);
  aHex.GetPointIds().SetId((int)4,(int)4);
  aHex.GetPointIds().SetId((int)5,(int)5);
  aHex.GetPointIds().SetId((int)6,(int)6);
  aHex.GetPointIds().SetId((int)7,(int)7);
  aHex.GetPointIds().SetId((int)8,(int)8);
  aHex.GetPointIds().SetId((int)9,(int)9);
  aHex.GetPointIds().SetId((int)10,(int)10);
  aHex.GetPointIds().SetId((int)11,(int)11);
  aHex.GetPointIds().SetId((int)12,(int)12);
  aHex.GetPointIds().SetId((int)13,(int)13);
  aHex.GetPointIds().SetId((int)14,(int)14);
  aHex.GetPointIds().SetId((int)15,(int)15);
  aHex.GetPointIds().SetId((int)16,(int)16);
  aHex.GetPointIds().SetId((int)17,(int)17);
  aHex.GetPointIds().SetId((int)18,(int)18);
  aHex.GetPointIds().SetId((int)19,(int)19);
  aHexGrid = new vtkUnstructuredGrid();
  aHexGrid.Allocate((int)1,(int)1);
  aHexGrid.InsertNextCell((int)aHex.GetCellType(),(vtkIdList)aHex.GetPointIds());
  aHexGrid.SetPoints((vtkPoints)hexPoints);
  aHexGrid.GetPointData().SetScalars((vtkDataArray)hexScalars);
  aHexMapper = new vtkDataSetMapper();
  aHexMapper.SetInputData((vtkDataSet)aHexGrid);
  aHexMapper.ScalarVisibilityOff();
  aHexActor = new vtkActor();
  aHexActor.SetMapper((vtkMapper)aHexMapper);
  aHexActor.GetProperty().SetRepresentationToWireframe();
  aHexActor.GetProperty().SetAmbient((double)1.0);
  // Quadratic wedge[]
  wedgePoints = new vtkPoints();
  wedgePoints.SetNumberOfPoints((int)15);
  wedgePoints.InsertPoint((int)0,(double)0,(double)0,(double)0);
  wedgePoints.InsertPoint((int)1,(double)1,(double)0,(double)0);
  wedgePoints.InsertPoint((int)2,(double)0,(double)1,(double)0);
  wedgePoints.InsertPoint((int)3,(double)0,(double)0,(double)1);
  wedgePoints.InsertPoint((int)4,(double)1,(double)0,(double)1);
  wedgePoints.InsertPoint((int)5,(double)0,(double)1,(double)1);
  wedgePoints.InsertPoint((int)6,(double)0.5,(double)0,(double)0);
  wedgePoints.InsertPoint((int)7,(double)0.5,(double)0.5,(double)0);
  wedgePoints.InsertPoint((int)8,(double)0,(double)0.5,(double)0);
  wedgePoints.InsertPoint((int)9,(double)0.5,(double)0,(double)1);
  wedgePoints.InsertPoint((int)10,(double)0.5,(double)0.5,(double)1);
  wedgePoints.InsertPoint((int)11,(double)0,(double)0.5,(double)1);
  wedgePoints.InsertPoint((int)12,(double)0,(double)0,(double)0.5);
  wedgePoints.InsertPoint((int)13,(double)1,(double)0,(double)0.5);
  wedgePoints.InsertPoint((int)14,(double)0,(double)1,(double)0.5);
  wedgeScalars = new vtkFloatArray();
  wedgeScalars.SetNumberOfTuples((int)15);
  wedgeScalars.InsertValue((int)0,(float)1.0);
  wedgeScalars.InsertValue((int)1,(float)1.0);
  wedgeScalars.InsertValue((int)2,(float)1.0);
  wedgeScalars.InsertValue((int)3,(float)1.0);
  wedgeScalars.InsertValue((int)4,(float)1.0);
  wedgeScalars.InsertValue((int)5,(float)1.0);
  wedgeScalars.InsertValue((int)6,(float)1.0);
  wedgeScalars.InsertValue((int)7,(float)1.0);
  wedgeScalars.InsertValue((int)8,(float)0.0);
  wedgeScalars.InsertValue((int)9,(float)0.0);
  wedgeScalars.InsertValue((int)10,(float)0.0);
  wedgeScalars.InsertValue((int)11,(float)0.0);
  wedgeScalars.InsertValue((int)12,(float)0.0);
  wedgeScalars.InsertValue((int)13,(float)0.0);
  wedgeScalars.InsertValue((int)14,(float)0.0);
  aWedge = new vtkQuadraticWedge();
  aWedge.GetPointIds().SetId((int)0,(int)0);
  aWedge.GetPointIds().SetId((int)1,(int)1);
  aWedge.GetPointIds().SetId((int)2,(int)2);
  aWedge.GetPointIds().SetId((int)3,(int)3);
  aWedge.GetPointIds().SetId((int)4,(int)4);
  aWedge.GetPointIds().SetId((int)5,(int)5);
  aWedge.GetPointIds().SetId((int)6,(int)6);
  aWedge.GetPointIds().SetId((int)7,(int)7);
  aWedge.GetPointIds().SetId((int)8,(int)8);
  aWedge.GetPointIds().SetId((int)9,(int)9);
  aWedge.GetPointIds().SetId((int)10,(int)10);
  aWedge.GetPointIds().SetId((int)11,(int)11);
  aWedge.GetPointIds().SetId((int)12,(int)12);
  aWedge.GetPointIds().SetId((int)13,(int)13);
  aWedge.GetPointIds().SetId((int)14,(int)14);
  aWedgeGrid = new vtkUnstructuredGrid();
  aWedgeGrid.Allocate((int)1,(int)1);
  aWedgeGrid.InsertNextCell((int)aWedge.GetCellType(),(vtkIdList)aWedge.GetPointIds());
  aWedgeGrid.SetPoints((vtkPoints)wedgePoints);
  aWedgeGrid.GetPointData().SetScalars((vtkDataArray)wedgeScalars);
  wedgeContours = new vtkClipDataSet();
  wedgeContours.SetInputData((vtkDataObject)aWedgeGrid);
  wedgeContours.SetValue((double)0.5);
  aWedgeContourMapper = new vtkDataSetMapper();
  aWedgeContourMapper.SetInputConnection((vtkAlgorithmOutput)wedgeContours.GetOutputPort());
  aWedgeContourMapper.ScalarVisibilityOff();
  aWedgeMapper = new vtkDataSetMapper();
  aWedgeMapper.SetInputData((vtkDataSet)aWedgeGrid);
  aWedgeMapper.ScalarVisibilityOff();
  aWedgeActor = new vtkActor();
  aWedgeActor.SetMapper((vtkMapper)aWedgeMapper);
  aWedgeActor.GetProperty().SetRepresentationToWireframe();
  aWedgeActor.GetProperty().SetAmbient((double)1.0);
  aWedgeContourActor = new vtkActor();
  aWedgeContourActor.SetMapper((vtkMapper)aWedgeContourMapper);
  aWedgeContourActor.GetProperty().SetAmbient((double)1.0);
  // Quadratic pyramid[]
  pyraPoints = new vtkPoints();
  pyraPoints.SetNumberOfPoints((int)13);
  pyraPoints.InsertPoint((int)0,(double)0,(double)0,(double)0);
  pyraPoints.InsertPoint((int)1,(double)1,(double)0,(double)0);
  pyraPoints.InsertPoint((int)2,(double)1,(double)1,(double)0);
  pyraPoints.InsertPoint((int)3,(double)0,(double)1,(double)0);
  pyraPoints.InsertPoint((int)4,(double)0,(double)0,(double)1);
  pyraPoints.InsertPoint((int)5,(double)0.5,(double)0,(double)0);
  pyraPoints.InsertPoint((int)6,(double)1,(double)0.5,(double)0);
  pyraPoints.InsertPoint((int)7,(double)0.5,(double)1,(double)0);
  pyraPoints.InsertPoint((int)8,(double)0,(double)0.5,(double)0);
  pyraPoints.InsertPoint((int)9,(double)0,(double)0,(double)0.5);
  pyraPoints.InsertPoint((int)10,(double)0.5,(double)0,(double)0.5);
  pyraPoints.InsertPoint((int)11,(double)0.5,(double)0.5,(double)0.5);
  pyraPoints.InsertPoint((int)12,(double)0,(double)0.5,(double)0.5);
  pyraScalars = new vtkFloatArray();
  pyraScalars.SetNumberOfTuples((int)13);
  pyraScalars.InsertValue((int)0,(float)1.0);
  pyraScalars.InsertValue((int)1,(float)1.0);
  pyraScalars.InsertValue((int)2,(float)1.0);
  pyraScalars.InsertValue((int)3,(float)1.0);
  pyraScalars.InsertValue((int)4,(float)1.0);
  pyraScalars.InsertValue((int)5,(float)1.0);
  pyraScalars.InsertValue((int)6,(float)1.0);
  pyraScalars.InsertValue((int)7,(float)1.0);
  pyraScalars.InsertValue((int)8,(float)0.0);
  pyraScalars.InsertValue((int)9,(float)0.0);
  pyraScalars.InsertValue((int)10,(float)0.0);
  pyraScalars.InsertValue((int)11,(float)0.0);
  pyraScalars.InsertValue((int)12,(float)0.0);
  aPyramid = new vtkQuadraticPyramid();
  aPyramid.GetPointIds().SetId((int)0,(int)0);
  aPyramid.GetPointIds().SetId((int)1,(int)1);
  aPyramid.GetPointIds().SetId((int)2,(int)2);
  aPyramid.GetPointIds().SetId((int)3,(int)3);
  aPyramid.GetPointIds().SetId((int)4,(int)4);
  aPyramid.GetPointIds().SetId((int)5,(int)5);
  aPyramid.GetPointIds().SetId((int)6,(int)6);
  aPyramid.GetPointIds().SetId((int)7,(int)7);
  aPyramid.GetPointIds().SetId((int)8,(int)8);
  aPyramid.GetPointIds().SetId((int)9,(int)9);
  aPyramid.GetPointIds().SetId((int)10,(int)10);
  aPyramid.GetPointIds().SetId((int)11,(int)11);
  aPyramid.GetPointIds().SetId((int)12,(int)12);
  aPyramidGrid = new vtkUnstructuredGrid();
  aPyramidGrid.Allocate((int)1,(int)1);
  aPyramidGrid.InsertNextCell((int)aPyramid.GetCellType(),(vtkIdList)aPyramid.GetPointIds());
  aPyramidGrid.SetPoints((vtkPoints)pyraPoints);
  aPyramidGrid.GetPointData().SetScalars((vtkDataArray)pyraScalars);
  pyraContours = new vtkClipDataSet();
  pyraContours.SetInputData((vtkDataObject)aPyramidGrid);
  pyraContours.SetValue((double)0.5);
  aPyramidContourMapper = new vtkDataSetMapper();
  aPyramidContourMapper.SetInputConnection((vtkAlgorithmOutput)pyraContours.GetOutputPort());
  aPyramidContourMapper.ScalarVisibilityOff();
  aPyramidMapper = new vtkDataSetMapper();
  aPyramidMapper.SetInputData((vtkDataSet)aPyramidGrid);
  aPyramidMapper.ScalarVisibilityOff();
  aPyramidActor = new vtkActor();
  aPyramidActor.SetMapper((vtkMapper)aPyramidMapper);
  aPyramidActor.GetProperty().SetRepresentationToWireframe();
  aPyramidActor.GetProperty().SetAmbient((double)1.0);
  aPyramidContourActor = new vtkActor();
  aPyramidContourActor.SetMapper((vtkMapper)aPyramidContourMapper);
  aPyramidContourActor.GetProperty().SetAmbient((double)1.0);
  // Create the rendering related stuff.[]
  // Since some of our actors are a single vertex, we need to remove all[]
  // cullers so the single vertex actors will render[]
  ren1 = vtkRenderer.New();
  ren1.GetCullers().RemoveAllItems();
  renWin = vtkRenderWindow.New();
  renWin.SetMultiSamples(0);
  renWin.AddRenderer((vtkRenderer)ren1);
  iren = new vtkRenderWindowInteractor();
  iren.SetRenderWindow((vtkRenderWindow)renWin);
  ren1.SetBackground((double).1,(double).2,(double).3);
  renWin.SetSize((int)400,(int)200);
  // specify properties[]
  ren1.AddActor((vtkProp)aEdgeActor);
  ren1.AddActor((vtkProp)aTriActor);
  ren1.AddActor((vtkProp)aQuadActor);
  ren1.AddActor((vtkProp)aTetActor);
  ren1.AddActor((vtkProp)aHexActor);
  ren1.AddActor((vtkProp)aWedgeActor);
  ren1.AddActor((vtkProp)aPyramidActor);
  // places everyone!![]
  aTriActor.AddPosition((double)2,(double)0,(double)0);
  aQuadActor.AddPosition((double)4,(double)0,(double)0);
  aTetActor.AddPosition((double)6,(double)0,(double)0);
  aHexActor.AddPosition((double)8,(double)0,(double)0);
  aWedgeActor.AddPosition((double)10,(double)0,(double)0);
  aPyramidActor.AddPosition((double)12,(double)0,(double)0);
  BuildBackdrop(-1, 15, -1, 4, -1, 2, .1);
  ren1.AddActor((vtkProp)base1);
  base1.GetProperty().SetDiffuseColor((double).2,(double).2,(double).2);
  ren1.AddActor((vtkProp)left);
  left.GetProperty().SetDiffuseColor((double).2,(double).2,(double).2);
  ren1.AddActor((vtkProp)back);
  back.GetProperty().SetDiffuseColor((double).2,(double).2,(double).2);
  ren1.ResetCamera();
  ren1.GetActiveCamera().Dolly((double)2.5);
  ren1.ResetCameraClippingRange();
  renWin.Render();
  // create a little scorecard above each of the cells. These are displayed[]
  // if a ray cast hits the cell, otherwise they are not shown.[]
  pm = new vtkPlaneSource();
  pm.SetXResolution((int)1);
  pm.SetYResolution((int)1);
  pmapper = vtkPolyDataMapper.New();
  pmapper.SetInputConnection((vtkAlgorithmOutput)pm.GetOutputPort());
  // now try intersecting rays with the cell[]
  cellPicker = new vtkCellPicker();
  edgeCheck = new vtkActor();
  edgeCheck.SetMapper((vtkMapper)pmapper);
  edgeCheck.AddPosition((double)0.5,(double)2.5,(double)0);
  cellPicker.Pick((double)87,(double)71,(double)0,(vtkRenderer)ren1);
  if ((cellPicker.GetCellId()) != -1)
    {
      ren1.AddActor((vtkProp)edgeCheck);
    }

  
  triCheck = new vtkActor();
  triCheck.SetMapper((vtkMapper)pmapper);
  triCheck.AddPosition((double)2.5,(double)2.5,(double)0);
  cellPicker.Pick((double)139,(double)72,(double)0,(vtkRenderer)ren1);
  if ((cellPicker.GetCellId()) != -1)
    {
      ren1.AddActor((vtkProp)triCheck);
    }

  
  quadCheck = new vtkActor();
  quadCheck.SetMapper((vtkMapper)pmapper);
  quadCheck.AddPosition((double)4.5,(double)2.5,(double)0);
  cellPicker.Pick((double)192,(double)78,(double)0,(vtkRenderer)ren1);
  if ((cellPicker.GetCellId()) != -1)
    {
      ren1.AddActor((vtkProp)quadCheck);
    }

  
  tetCheck = new vtkActor();
  tetCheck.SetMapper((vtkMapper)pmapper);
  tetCheck.AddPosition((double)6.5,(double)2.5,(double)0);
  cellPicker.Pick((double)233,(double)70,(double)0,(vtkRenderer)ren1);
  if ((cellPicker.GetCellId()) != -1)
    {
      ren1.AddActor((vtkProp)tetCheck);
    }

  
  hexCheck = new vtkActor();
  hexCheck.SetMapper((vtkMapper)pmapper);
  hexCheck.AddPosition((double)8.5,(double)2.5,(double)0);
  cellPicker.Pick((double)287,(double)80,(double)0,(vtkRenderer)ren1);
  if ((cellPicker.GetCellId()) != -1)
    {
      ren1.AddActor((vtkProp)hexCheck);
    }

  
  wedgeCheck = new vtkActor();
  wedgeCheck.SetMapper((vtkMapper)pmapper);
  wedgeCheck.AddPosition((double)10.5,(double)2.5,(double)0);
  cellPicker.Pick((double)287,(double)80,(double)0,(vtkRenderer)ren1);
  if ((cellPicker.GetCellId()) != -1)
    {
      ren1.AddActor((vtkProp)wedgeCheck);
    }

  
  pyraCheck = new vtkActor();
  pyraCheck.SetMapper((vtkMapper)pmapper);
  pyraCheck.AddPosition((double)12.5,(double)2.5,(double)0);
  cellPicker.Pick((double)287,(double)80,(double)0,(vtkRenderer)ren1);
  if ((cellPicker.GetCellId()) != -1)
    {
      ren1.AddActor((vtkProp)pyraCheck);
    }

  
  // render the image[]
  //[]
  iren.Initialize();
  
//deleteAllVTKObjects();
  }
static string VTK_DATA_ROOT;
static int threshold;
static vtkPoints edgePoints;
static vtkFloatArray edgeScalars;
static vtkQuadraticEdge aEdge;
static vtkUnstructuredGrid aEdgeGrid;
static vtkDataSetMapper aEdgeMapper;
static vtkActor aEdgeActor;
static vtkPoints triPoints;
static vtkFloatArray triScalars;
static vtkQuadraticTriangle aTri;
static vtkUnstructuredGrid aTriGrid;
static vtkDataSetMapper aTriMapper;
static vtkActor aTriActor;
static vtkPoints quadPoints;
static vtkFloatArray quadScalars;
static vtkQuadraticQuad aQuad;
static vtkUnstructuredGrid aQuadGrid;
static vtkDataSetMapper aQuadMapper;
static vtkActor aQuadActor;
static vtkPoints tetPoints;
static vtkFloatArray tetScalars;
static vtkQuadraticTetra aTet;
static vtkUnstructuredGrid aTetGrid;
static vtkDataSetMapper aTetMapper;
static vtkActor aTetActor;
static vtkPoints hexPoints;
static vtkFloatArray hexScalars;
static vtkQuadraticHexahedron aHex;
static vtkUnstructuredGrid aHexGrid;
static vtkDataSetMapper aHexMapper;
static vtkActor aHexActor;
static vtkPoints wedgePoints;
static vtkFloatArray wedgeScalars;
static vtkQuadraticWedge aWedge;
static vtkUnstructuredGrid aWedgeGrid;
static vtkClipDataSet wedgeContours;
static vtkDataSetMapper aWedgeContourMapper;
static vtkDataSetMapper aWedgeMapper;
static vtkActor aWedgeActor;
static vtkActor aWedgeContourActor;
static vtkPoints pyraPoints;
static vtkFloatArray pyraScalars;
static vtkQuadraticPyramid aPyramid;
static vtkUnstructuredGrid aPyramidGrid;
static vtkClipDataSet pyraContours;
static vtkDataSetMapper aPyramidContourMapper;
static vtkDataSetMapper aPyramidMapper;
static vtkActor aPyramidActor;
static vtkActor aPyramidContourActor;
static vtkRenderer ren1;
static vtkRenderWindow renWin;
static vtkRenderWindowInteractor iren;
static vtkCubeSource base1Plane;
static vtkPolyDataMapper base1Mapper;
static vtkActor base1;
static vtkCubeSource backPlane;
static vtkPolyDataMapper backMapper;
static vtkActor back;
static vtkCubeSource leftPlane;
static vtkPolyDataMapper leftMapper;
static vtkActor left;
static vtkPlaneSource pm;
static vtkPolyDataMapper pmapper;
static vtkCellPicker cellPicker;
static vtkActor edgeCheck;
static vtkActor triCheck;
static vtkActor quadCheck;
static vtkActor tetCheck;
static vtkActor hexCheck;
static vtkActor wedgeCheck;
static vtkActor pyraCheck;


  /// <summary>
  ///A process translated from the tcl scripts
  /// </summary>
  public static void BuildBackdrop (int minX,int maxX,int minY,int maxY,int minZ,int maxZ,double thickness)
    {
      if ((base1Plane) == null)
        {
          base1Plane = new vtkCubeSource();
        }

      
      base1Plane.SetCenter((double)(maxX+minX)/2.0,(double)minY,(double)(maxZ+minZ)/2.0);
      base1Plane.SetXLength((double)(maxX-minX));
      base1Plane.SetYLength((double)thickness);
      base1Plane.SetZLength((double)(maxZ-minZ));
      if ((base1Mapper) == null)
        {
          base1Mapper = vtkPolyDataMapper.New();
        }


      base1Mapper.SetInputData((vtkPolyData)base1Plane.GetOutput());
      if ((base1) == null)
        {
          base1 = new vtkActor();
        }

      
      base1.SetMapper((vtkMapper)base1Mapper);
      if ((backPlane) == null)
        {
          backPlane = new vtkCubeSource();
        }

      
      backPlane.SetCenter((double)(maxX+minX)/2.0,(double)(maxY+minY)/2.0,(double)minZ);
      backPlane.SetXLength((double)(maxX-minX));
      backPlane.SetYLength((double)(maxY-minY));
      backPlane.SetZLength((double)thickness);
      if ((backMapper) == null)
        {
          backMapper = vtkPolyDataMapper.New();
        }


      backMapper.SetInputData((vtkPolyData)backPlane.GetOutput());
      if ((back) == null)
        {
          back = new vtkActor();
        }

      
      back.SetMapper((vtkMapper)backMapper);
      if ((leftPlane) == null)
        {
          leftPlane = new vtkCubeSource();
        }

      
      leftPlane.SetCenter((double)minX,(double)(maxY+minY)/2.0,(double)(maxZ+minZ)/2.0);
      leftPlane.SetXLength((double)thickness);
      leftPlane.SetYLength((double)(maxY-minY));
      leftPlane.SetZLength((double)(maxZ-minZ));
      if ((leftMapper) == null)
        {
          leftMapper = vtkPolyDataMapper.New();
        }


      leftMapper.SetInputData((vtkPolyData)leftPlane.GetOutput());
      if ((left) == null)
        {
          left = new vtkActor();
        }

      
      left.SetMapper((vtkMapper)leftMapper);
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
        public static vtkPoints GetedgePoints()
        {
            return edgePoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetedgePoints(vtkPoints toSet)
        {
            edgePoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkFloatArray GetedgeScalars()
        {
            return edgeScalars;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetedgeScalars(vtkFloatArray toSet)
        {
            edgeScalars = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkQuadraticEdge GetaEdge()
        {
            return aEdge;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaEdge(vtkQuadraticEdge toSet)
        {
            aEdge = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkUnstructuredGrid GetaEdgeGrid()
        {
            return aEdgeGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaEdgeGrid(vtkUnstructuredGrid toSet)
        {
            aEdgeGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaEdgeMapper()
        {
            return aEdgeMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaEdgeMapper(vtkDataSetMapper toSet)
        {
            aEdgeMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaEdgeActor()
        {
            return aEdgeActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaEdgeActor(vtkActor toSet)
        {
            aEdgeActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GettriPoints()
        {
            return triPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SettriPoints(vtkPoints toSet)
        {
            triPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkFloatArray GettriScalars()
        {
            return triScalars;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SettriScalars(vtkFloatArray toSet)
        {
            triScalars = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkQuadraticTriangle GetaTri()
        {
            return aTri;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaTri(vtkQuadraticTriangle toSet)
        {
            aTri = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkUnstructuredGrid GetaTriGrid()
        {
            return aTriGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaTriGrid(vtkUnstructuredGrid toSet)
        {
            aTriGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaTriMapper()
        {
            return aTriMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaTriMapper(vtkDataSetMapper toSet)
        {
            aTriMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaTriActor()
        {
            return aTriActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaTriActor(vtkActor toSet)
        {
            aTriActor = toSet;
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
        public static vtkFloatArray GetquadScalars()
        {
            return quadScalars;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetquadScalars(vtkFloatArray toSet)
        {
            quadScalars = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkQuadraticQuad GetaQuad()
        {
            return aQuad;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaQuad(vtkQuadraticQuad toSet)
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
        public static vtkPoints GettetPoints()
        {
            return tetPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SettetPoints(vtkPoints toSet)
        {
            tetPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkFloatArray GettetScalars()
        {
            return tetScalars;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SettetScalars(vtkFloatArray toSet)
        {
            tetScalars = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkQuadraticTetra GetaTet()
        {
            return aTet;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaTet(vtkQuadraticTetra toSet)
        {
            aTet = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkUnstructuredGrid GetaTetGrid()
        {
            return aTetGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaTetGrid(vtkUnstructuredGrid toSet)
        {
            aTetGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaTetMapper()
        {
            return aTetMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaTetMapper(vtkDataSetMapper toSet)
        {
            aTetMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaTetActor()
        {
            return aTetActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaTetActor(vtkActor toSet)
        {
            aTetActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GethexPoints()
        {
            return hexPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SethexPoints(vtkPoints toSet)
        {
            hexPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkFloatArray GethexScalars()
        {
            return hexScalars;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SethexScalars(vtkFloatArray toSet)
        {
            hexScalars = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkQuadraticHexahedron GetaHex()
        {
            return aHex;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaHex(vtkQuadraticHexahedron toSet)
        {
            aHex = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkUnstructuredGrid GetaHexGrid()
        {
            return aHexGrid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaHexGrid(vtkUnstructuredGrid toSet)
        {
            aHexGrid = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaHexMapper()
        {
            return aHexMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaHexMapper(vtkDataSetMapper toSet)
        {
            aHexMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetaHexActor()
        {
            return aHexActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaHexActor(vtkActor toSet)
        {
            aHexActor = toSet;
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
        public static vtkFloatArray GetwedgeScalars()
        {
            return wedgeScalars;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetwedgeScalars(vtkFloatArray toSet)
        {
            wedgeScalars = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkQuadraticWedge GetaWedge()
        {
            return aWedge;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaWedge(vtkQuadraticWedge toSet)
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
        public static vtkClipDataSet GetwedgeContours()
        {
            return wedgeContours;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetwedgeContours(vtkClipDataSet toSet)
        {
            wedgeContours = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaWedgeContourMapper()
        {
            return aWedgeContourMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaWedgeContourMapper(vtkDataSetMapper toSet)
        {
            aWedgeContourMapper = toSet;
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
        public static vtkActor GetaWedgeContourActor()
        {
            return aWedgeContourActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaWedgeContourActor(vtkActor toSet)
        {
            aWedgeContourActor = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPoints GetpyraPoints()
        {
            return pyraPoints;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetpyraPoints(vtkPoints toSet)
        {
            pyraPoints = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkFloatArray GetpyraScalars()
        {
            return pyraScalars;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetpyraScalars(vtkFloatArray toSet)
        {
            pyraScalars = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkQuadraticPyramid GetaPyramid()
        {
            return aPyramid;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPyramid(vtkQuadraticPyramid toSet)
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
        public static vtkClipDataSet GetpyraContours()
        {
            return pyraContours;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetpyraContours(vtkClipDataSet toSet)
        {
            pyraContours = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkDataSetMapper GetaPyramidContourMapper()
        {
            return aPyramidContourMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPyramidContourMapper(vtkDataSetMapper toSet)
        {
            aPyramidContourMapper = toSet;
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
        public static vtkActor GetaPyramidContourActor()
        {
            return aPyramidContourActor;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetaPyramidContourActor(vtkActor toSet)
        {
            aPyramidContourActor = toSet;
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
        public static vtkCubeSource Getbase1Plane()
        {
            return base1Plane;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbase1Plane(vtkCubeSource toSet)
        {
            base1Plane = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Getbase1Mapper()
        {
            return base1Mapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbase1Mapper(vtkPolyDataMapper toSet)
        {
            base1Mapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getbase1()
        {
            return base1;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setbase1(vtkActor toSet)
        {
            base1 = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCubeSource GetbackPlane()
        {
            return backPlane;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetbackPlane(vtkCubeSource toSet)
        {
            backPlane = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetbackMapper()
        {
            return backMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetbackMapper(vtkPolyDataMapper toSet)
        {
            backMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getback()
        {
            return back;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setback(vtkActor toSet)
        {
            back = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkCubeSource GetleftPlane()
        {
            return leftPlane;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetleftPlane(vtkCubeSource toSet)
        {
            leftPlane = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper GetleftMapper()
        {
            return leftMapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetleftMapper(vtkPolyDataMapper toSet)
        {
            leftMapper = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor Getleft()
        {
            return left;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setleft(vtkActor toSet)
        {
            left = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPlaneSource Getpm()
        {
            return pm;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpm(vtkPlaneSource toSet)
        {
            pm = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkPolyDataMapper Getpmapper()
        {
            return pmapper;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void Setpmapper(vtkPolyDataMapper toSet)
        {
            pmapper = toSet;
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
        public static vtkActor GetedgeCheck()
        {
            return edgeCheck;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetedgeCheck(vtkActor toSet)
        {
            edgeCheck = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GettriCheck()
        {
            return triCheck;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SettriCheck(vtkActor toSet)
        {
            triCheck = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetquadCheck()
        {
            return quadCheck;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetquadCheck(vtkActor toSet)
        {
            quadCheck = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GettetCheck()
        {
            return tetCheck;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SettetCheck(vtkActor toSet)
        {
            tetCheck = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GethexCheck()
        {
            return hexCheck;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SethexCheck(vtkActor toSet)
        {
            hexCheck = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetwedgeCheck()
        {
            return wedgeCheck;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetwedgeCheck(vtkActor toSet)
        {
            wedgeCheck = toSet;
        }
        
        ///<summary> A Get Method for Static Variables </summary>
        public static vtkActor GetpyraCheck()
        {
            return pyraCheck;
        }
        
        ///<summary> A Set Method for Static Variables </summary>
        public static void SetpyraCheck(vtkActor toSet)
        {
            pyraCheck = toSet;
        }
        
  ///<summary>Deletes all static objects created</summary>
  public static void deleteAllVTKObjects()
  {
  	//clean up vtk objects
  	if(edgePoints!= null){edgePoints.Dispose();}
  	if(edgeScalars!= null){edgeScalars.Dispose();}
  	if(aEdge!= null){aEdge.Dispose();}
  	if(aEdgeGrid!= null){aEdgeGrid.Dispose();}
  	if(aEdgeMapper!= null){aEdgeMapper.Dispose();}
  	if(aEdgeActor!= null){aEdgeActor.Dispose();}
  	if(triPoints!= null){triPoints.Dispose();}
  	if(triScalars!= null){triScalars.Dispose();}
  	if(aTri!= null){aTri.Dispose();}
  	if(aTriGrid!= null){aTriGrid.Dispose();}
  	if(aTriMapper!= null){aTriMapper.Dispose();}
  	if(aTriActor!= null){aTriActor.Dispose();}
  	if(quadPoints!= null){quadPoints.Dispose();}
  	if(quadScalars!= null){quadScalars.Dispose();}
  	if(aQuad!= null){aQuad.Dispose();}
  	if(aQuadGrid!= null){aQuadGrid.Dispose();}
  	if(aQuadMapper!= null){aQuadMapper.Dispose();}
  	if(aQuadActor!= null){aQuadActor.Dispose();}
  	if(tetPoints!= null){tetPoints.Dispose();}
  	if(tetScalars!= null){tetScalars.Dispose();}
  	if(aTet!= null){aTet.Dispose();}
  	if(aTetGrid!= null){aTetGrid.Dispose();}
  	if(aTetMapper!= null){aTetMapper.Dispose();}
  	if(aTetActor!= null){aTetActor.Dispose();}
  	if(hexPoints!= null){hexPoints.Dispose();}
  	if(hexScalars!= null){hexScalars.Dispose();}
  	if(aHex!= null){aHex.Dispose();}
  	if(aHexGrid!= null){aHexGrid.Dispose();}
  	if(aHexMapper!= null){aHexMapper.Dispose();}
  	if(aHexActor!= null){aHexActor.Dispose();}
  	if(wedgePoints!= null){wedgePoints.Dispose();}
  	if(wedgeScalars!= null){wedgeScalars.Dispose();}
  	if(aWedge!= null){aWedge.Dispose();}
  	if(aWedgeGrid!= null){aWedgeGrid.Dispose();}
  	if(wedgeContours!= null){wedgeContours.Dispose();}
  	if(aWedgeContourMapper!= null){aWedgeContourMapper.Dispose();}
  	if(aWedgeMapper!= null){aWedgeMapper.Dispose();}
  	if(aWedgeActor!= null){aWedgeActor.Dispose();}
  	if(aWedgeContourActor!= null){aWedgeContourActor.Dispose();}
  	if(pyraPoints!= null){pyraPoints.Dispose();}
  	if(pyraScalars!= null){pyraScalars.Dispose();}
  	if(aPyramid!= null){aPyramid.Dispose();}
  	if(aPyramidGrid!= null){aPyramidGrid.Dispose();}
  	if(pyraContours!= null){pyraContours.Dispose();}
  	if(aPyramidContourMapper!= null){aPyramidContourMapper.Dispose();}
  	if(aPyramidMapper!= null){aPyramidMapper.Dispose();}
  	if(aPyramidActor!= null){aPyramidActor.Dispose();}
  	if(aPyramidContourActor!= null){aPyramidContourActor.Dispose();}
  	if(ren1!= null){ren1.Dispose();}
  	if(renWin!= null){renWin.Dispose();}
  	if(iren!= null){iren.Dispose();}
  	if(base1Plane!= null){base1Plane.Dispose();}
  	if(base1Mapper!= null){base1Mapper.Dispose();}
  	if(base1!= null){base1.Dispose();}
  	if(backPlane!= null){backPlane.Dispose();}
  	if(backMapper!= null){backMapper.Dispose();}
  	if(back!= null){back.Dispose();}
  	if(leftPlane!= null){leftPlane.Dispose();}
  	if(leftMapper!= null){leftMapper.Dispose();}
  	if(left!= null){left.Dispose();}
  	if(pm!= null){pm.Dispose();}
  	if(pmapper!= null){pmapper.Dispose();}
  	if(cellPicker!= null){cellPicker.Dispose();}
  	if(edgeCheck!= null){edgeCheck.Dispose();}
  	if(triCheck!= null){triCheck.Dispose();}
  	if(quadCheck!= null){quadCheck.Dispose();}
  	if(tetCheck!= null){tetCheck.Dispose();}
  	if(hexCheck!= null){hexCheck.Dispose();}
  	if(wedgeCheck!= null){wedgeCheck.Dispose();}
  	if(pyraCheck!= null){pyraCheck.Dispose();}
  }

}
//--- end of script --//

