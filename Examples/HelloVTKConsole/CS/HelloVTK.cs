using System;
using System.Collections.Generic;
using System.Text;

using Kitware.VTK;

namespace HelloVTKConsole
{
    class HelloVTK
    {
        ///<summary>Entry Point</summary>
        static void Main(string[] args)
        {
            // Create a simple sphere. A pipeline is created.
            sphere = vtkSphereSource.New();
            sphere.SetThetaResolution(8);
            sphere.SetPhiResolution(16);

            shrink = vtkShrinkPolyData.New();
            shrink.SetInputConnection(sphere.GetOutputPort());
            shrink.SetShrinkFactor(0.9);

            mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(shrink.GetOutputPort());

            // The actor links the data pipeline to the rendering subsystem
            actor = vtkActor.New();
            actor.SetMapper(mapper);
            actor.GetProperty().SetColor(1, 0, 0);

            // Create components of the rendering subsystem
            //
            ren1 = vtkRenderer.New();
            renWin = vtkRenderWindow.New();
            renWin.AddRenderer(ren1);
            iren = vtkRenderWindowInteractor.New();
            iren.SetRenderWindow(renWin);

            // Add the actors to the renderer, set the window size
            //
            ren1.AddViewProp(actor);
            renWin.SetSize(250, 250);
            renWin.Render();
            camera = ren1.GetActiveCamera();
            camera.Zoom(1.5);

            // render the image and start the event loop
            //
            renWin.Render();

            iren.Initialize();
            iren.Start();

            deleteAllVTKObjects();
        }

        static vtkSphereSource sphere;
        static vtkShrinkPolyData shrink;
        static vtkPolyDataMapper mapper;
        static vtkActor actor;
        static vtkRenderer ren1;
        static vtkRenderWindow renWin;
        static vtkRenderWindowInteractor iren;
        static vtkCamera camera;

        ///<summary>Deletes all static objects created</summary>
        public static void deleteAllVTKObjects()
        {
            //clean up vtk objects
            if (sphere != null) { sphere.Dispose(); }
            if (mapper != null) { mapper.Dispose(); }
            if (actor != null) { actor.Dispose(); }
            if (ren1 != null) { ren1.Dispose(); }
            if (renWin != null) { renWin.Dispose(); }
            if (iren != null) { iren.Dispose(); }
            if (camera != null) { camera.Dispose(); }
        }
    }
}
