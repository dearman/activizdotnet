# Activiz.NET-7.1.1
*by Kitware* https://www.kitware.eu/product/activiz
* Open-source software system for 3D visualization wrapped in C#
* Allows to quickly develop production-ready, interactive 3D applications in the .NET environment
* Built around the Visualization ToolKit VTK
* Supports a wide variety of visualization algorithms including scalar, vector, tensor, texture, and volumetric methods
* ActiViz includes advanced modeling techniques such as implicit modeling, polygon reduction, mesh smoothing, cutting, contouring, and Delaunay triangulation

ActiViz .NET provides an integration layer for The Visualization Toolkit (see the next
section for more information about VTK) enabling VTK to be used in the Microsoft
.NET framework. This means that you can tap into the power of VTK using .NET
programming languages such as C# and Visual Basic .NET. ActiViz .NET is designed for
the application developer creating software in the Microsoft .NET Framework.

VTK is an open-source system written in C++ that you can download and use for free.
However using VTK requires significant C++ developer skills, and VTK does not easily
integrate into the Microsoft development environment. ActiViz .NET provides the
appropriate integration layer so that VTK seamlessly fits into the .NET framework. This
means that you can use languages such as C# and Visual Basic to add powerful 3D
visualization capabilities to your own applications. This integration layer provides the
benefits of the .NET layer including on-line documentation and intelligent coding.

## Binary Downloads
Although Activiz was originally distributed as a Windows Installer MSI, this is not an ideal solution.
I will be distributing all future binary releases, simply as a zip of the install tree, including all dependencies used in the build.  

As part of this change, I have modified the build script so that the VTK binaries are no longer included as resources with the Kitware.VTK assembly.  This behavior was highly problematic. Since CMake could not accurately determine the state of the VS Solution Configuration at the time of Generation, the dependences built with the Assembly were often incorrect for the End-User.

Developers using Activiz, should be managing their own dependencies.  
This is a complex mixed C++/C# project.  Dependency Walker is your friend.

#### Visual Studio 2013 / ICC 2016 / CSC .NET 4.0 (x86) Debug Build 1200
*VTK-7.1.1 (my fork) / HDF5 1.8.20*   
https://helium.cascadeacoustic.com/activiz/activizdotnet-711-1200-x86-debug.7z

#### Dependency Walker 2.2
http://www.dependencywalker.com/
