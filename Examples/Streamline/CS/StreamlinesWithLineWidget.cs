using Kitware.VTK;
using System;

/// <summary>
/// Class Containing Main Method
/// </summary>
public class StreamlinesWithLineWidgetClass
{
    /// <summary>
    /// Entry Point
    /// </summary>
    /// <param name="argv"></param>
    public static void Main(String[] argv)
    {
        // This example demonstrates how to use the vtkLineWidget to seed
        // and manipulate streamlines. Two line widgets are created. One is
        // invoked by pressing 'W', the other by pressing 'L'. Both can exist
        // together.
        // Start by loading some data.
        pl3d = vtkPLOT3DReader.New();
        pl3d.SetXYZFileName("../../../combxyz.bin");
        pl3d.SetQFileName("../../../combq.bin");
        pl3d.SetScalarFunctionNumber(100);
        pl3d.SetVectorFunctionNumber(202);
        pl3d.Update();
        // The line widget is used seed the streamlines.
        lineWidget = vtkLineWidget.New();
        seeds = vtkPolyData.New();

        lineWidget.SetInput(pl3d.GetOutput());
        lineWidget.SetAlignToYAxis();
        lineWidget.PlaceWidget();
        lineWidget.GetPolyData(seeds);
        lineWidget.ClampToBoundsOn();

        rk4 = vtkRungeKutta4.New();
        streamer = vtkStreamLine.New();
        streamer.SetInputConnection(pl3d.GetOutputPort());
        streamer.SetSource(seeds);
        streamer.SetMaximumPropagationTime(100);
        streamer.SetIntegrationStepLength(.2);
        streamer.SetStepLength(.001);
        streamer.SetNumberOfThreads(1);
        streamer.SetIntegrationDirectionToForward();
        streamer.VorticityOn();
        streamer.SetIntegrator(rk4);

        rf = vtkRibbonFilter.New();
        rf.SetInputConnection(streamer.GetOutputPort());
        rf.SetWidth(0.1);
        rf.SetWidthFactor(5);

        streamMapper = vtkPolyDataMapper.New();
        streamMapper.SetInputConnection(rf.GetOutputPort());
        streamMapper.SetScalarRange(pl3d.GetOutput().GetScalarRange()[0], pl3d.GetOutput().GetScalarRange()[1]);
        streamline = vtkActor.New();
        streamline.SetMapper(streamMapper);
        streamline.VisibilityOff();

        // The second line widget is used seed more streamlines.
        lineWidget2 = vtkLineWidget.New();
        seeds2 = vtkPolyData.New();
        lineWidget2.SetInput(pl3d.GetOutput());
        lineWidget2.PlaceWidget();
        lineWidget2.GetPolyData(seeds2);
        lineWidget2.SetKeyPressActivationValue((sbyte)108);

        streamer2 = vtkStreamLine.New();
        streamer2.SetInputConnection(pl3d.GetOutputPort());
        streamer2.SetSource(seeds2);
        streamer2.SetMaximumPropagationTime(100);
        streamer2.SetIntegrationStepLength(.2);
        streamer2.SetStepLength(.001);
        streamer2.SetNumberOfThreads(1);
        streamer2.SetIntegrationDirectionToForward();
        streamer2.VorticityOn();
        streamer2.SetIntegrator(rk4);

        rf2 = vtkRibbonFilter.New();
        rf2.SetInputConnection(streamer2.GetOutputPort());
        rf2.SetWidth(0.1);
        rf2.SetWidthFactor(5);

        streamMapper2 = vtkPolyDataMapper.New();
        streamMapper2.SetInputConnection(rf2.GetOutputPort());
        streamMapper2.SetScalarRange(pl3d.GetOutput().GetScalarRange()[0], pl3d.GetOutput().GetScalarRange()[1]);
        
        streamline2 = vtkActor.New();
        streamline2.SetMapper(streamMapper2);
        streamline2.VisibilityOff();

        outline = vtkStructuredGridOutlineFilter.New();
        outline.SetInputConnection(pl3d.GetOutputPort());

        outlineMapper = vtkPolyDataMapper.New();
        outlineMapper.SetInputConnection(outline.GetOutputPort());
        outlineActor = vtkActor.New();
        outlineActor.SetMapper(outlineMapper);

        // Create the RenderWindow, Renderer and both Actors
        ren1 = vtkRenderer.New();
        renWin = vtkRenderWindow.New();
        renWin.AddRenderer(ren1);
        iren = vtkRenderWindowInteractor.New();
        iren.SetRenderWindow(renWin);

        // Associate the line widget with the interactor
        lineWidget.SetInteractor(iren);
        lineWidget.StartInteractionEvt += new vtkObject.vtkObjectEventHandler(BeginInteraction);
        lineWidget.InteractionEvt += new vtkObject.vtkObjectEventHandler(GenerateStreamlines);
        lineWidget2.SetInteractor(iren);
        lineWidget2.StartInteractionEvt += new vtkObject.vtkObjectEventHandler(BeginInteraction2);
        lineWidget2.EndInteractionEvt += new vtkObject.vtkObjectEventHandler(GenerateStreamlines2);
       
        // Add the actors to the renderer, set the background and size
        ren1.AddActor(outlineActor);
        ren1.AddActor(streamline);
        ren1.AddActor(streamline2);
        
        ren1.SetBackground(1, 1, 1);
        renWin.SetSize(300, 300);
        ren1.SetBackground(0.1, 0.2, 0.4);
        
        cam1 = ren1.GetActiveCamera();
        cam1.SetClippingRange(3.95297, 50);
        cam1.SetFocalPoint(9.71821, 0.458166, 29.3999);
        cam1.SetPosition(2.7439, -37.3196, 38.7167);
        cam1.SetViewUp(-0.16123, 0.264271, 0.950876);
        
        // render the image
        renWin.Render();
        lineWidget2.On(); 
        iren.Initialize();
        iren.Start();

        //Clean Up
        deleteAllVTKObjects();
    }

    static vtkPLOT3DReader pl3d;
    static vtkLineWidget lineWidget;
    static vtkPolyData seeds;
    static vtkRungeKutta4 rk4;
    static vtkStreamLine streamer;
    static vtkRibbonFilter rf;
    static vtkPolyDataMapper streamMapper;
    static vtkActor streamline;
    static vtkLineWidget lineWidget2;
    static vtkPolyData seeds2;
    static vtkStreamLine streamer2;
    static vtkRibbonFilter rf2;
    static vtkPolyDataMapper streamMapper2;
    static vtkActor streamline2;
    static vtkStructuredGridOutlineFilter outline;
    static vtkPolyDataMapper outlineMapper;
    static vtkActor outlineActor;
    static vtkRenderer ren1;
    static vtkRenderWindow renWin;
    static vtkRenderWindowInteractor iren;
    static vtkCamera cam1;


    /// <summary>
    /// Callback function for lineWidget.StartInteractionEvt
    /// </summary>
    public static void BeginInteraction(vtkObject sender, vtkObjectEventArgs e)
    {
        streamline.VisibilityOn();
    }

    /// <summary>
    /// Callback function for lineWidget.InteractionEvt
    /// </summary>
    public static void GenerateStreamlines(vtkObject sender, vtkObjectEventArgs e)
    {
        lineWidget.GetPolyData(seeds);
    }

    /// <summary>
    /// Callback function for lineWidget2.StartInteractionEvt
    /// </summary>
    public static void BeginInteraction2(vtkObject sender, vtkObjectEventArgs e)
    {
        streamline2.VisibilityOn();
    }

    /// <summary>
    /// Callback function for lineWidget2.InteractionEvt
    /// </summary>
    public static void GenerateStreamlines2(vtkObject sender, vtkObjectEventArgs e)
    {
        lineWidget2.GetPolyData(seeds2);
        renWin.Render();
    }

    ///<summary>Deletes all static objects created</summary>
    public static void deleteAllVTKObjects()
    {
        //clean up vtk objects
        if (pl3d != null) { pl3d.Dispose(); }
        if (lineWidget != null) { lineWidget.Dispose(); }
        if (seeds != null) { seeds.Dispose(); }
        if (rk4 != null) { rk4.Dispose(); }
        if (streamer != null) { streamer.Dispose(); }
        if (rf != null) { rf.Dispose(); }
        if (streamMapper != null) { streamMapper.Dispose(); }
        if (streamline != null) { streamline.Dispose(); }
        if (lineWidget2 != null) { lineWidget2.Dispose(); }
        if (seeds2 != null) { seeds2.Dispose(); }
        if (streamer2 != null) { streamer2.Dispose(); }
        if (rf2 != null) { rf2.Dispose(); }
        if (streamMapper2 != null) { streamMapper2.Dispose(); }
        if (streamline2 != null) { streamline2.Dispose(); }
        if (outline != null) { outline.Dispose(); }
        if (outlineMapper != null) { outlineMapper.Dispose(); }
        if (outlineActor != null) { outlineActor.Dispose(); }
        if (ren1 != null) { ren1.Dispose(); }
        if (renWin != null) { renWin.Dispose(); }
        if (iren != null) { iren.Dispose(); }
        if (cam1 != null) { cam1.Dispose(); }
    }

}
//--- end of script --//

