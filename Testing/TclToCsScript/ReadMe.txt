--Read Me on how to convert Tcl tests to CSharp Test

--vtkTclToPythonConvertor.py
 -is the main script that does the converting
 -command line arguments are:
      -i <path to input file> -o <path to output file> [-xmldir <path to the directory with the C++ xml documentation>] 
 -depends on: (these files must be in the same directory)
    XML_Parser.py
    vtkTclParser.py
    TclToCsHashMap.py
    vktColors.py <-Generated at Runtime (see below)
    vtkClassList.py <-Generated at Runtime (see below)
    vtkCommandList.py <-Generated at Runtime (see below)

--TclToCsConvertorDriver.cmake
 -Looks for all the tcl tests and runs them through the python script
 -This needs to be in the same directory as GenerateVtkColors.py GenerateVtkCommandList.py and GenerateVTKClassList.py, these files generate the last three files needed for vtkTclToPythonConvertor to work
 -Takes in the following arguments:
   VTK_SOURCE - Path to the VTK Source Tree
   XML_DIR - Path to the directory with the C++ xml documentation
   PYTHON_PATH - Path to the python compiler
   CSHARP_PATH - Path to the CSharp compiler
   REFERENCES_DIR - This should contain the path to the directory where Kitware.VTK.dll and Kitware.mummy.runtime.dll are kept
 -You may also define the variables EXCLUDE and EXECUTE_ONLY
   EXCLUDE should be the name of a text file that contains the path to the tcl tests you want to exclude relative to the VTK_SOURCE dir
   EXECUTE_ONLY should be the name of a text file that contains only the name without an extension of the tests that you do not want to exclude
   If neither of these variables are set all tcl tests in the VTK dir will be included
   EXCLUDE takes precedence over EXECUTE ONLY
 -The output of this file will be in a directory called Converted CSharp and the directory name will have a time stamp on it
 -The CSharp compiler will compile every test generated and if the compiler returns a success the .cs test will be placed in the Compiling directory, otherwise it will be placed in the NonCompiling directory
 -If the python script fails, no CSharp test will be generated, but it will be recorded in a text file called NonTranslatable.txt
 -to run TclToCsConvertorDriver as a -P script run the command line cmake [[-D EXCLUDE=excludelist.txt][-D EXECUTE_ONLY=includelist.txt]] -P TclToCsConvertorDriver.cmake

--Notes
 -In The Failing Tests directory there are a bunch of .txt files that list the names of certian tests. These are tests that fail for certian resons and might be fixed in the future
 -Classes Without The Methods They Call.txt - Tests that use methods that are not in their declared class
 -DoubleDotProblem.txt - Tests that use Tcl Windows
 -FixByHand.txt - Tests that should easily be fixable by hand
 -info.txt - Tests that use the Tcl info command
 -Method Param Type - Tests that have self defined methods that cannot figure out the type of their arguments
 -Methods as Paramters - Tests with function calls that take a function as a parameter
 -namespace.eval() - tests that contain the phrase namespace.eval()
 -ref double - tests that have a method that takes in a ref double, this ref double should acctully be an array
 -ScalarRange - the same as a ref double
 -SetOrigin - the same as a ref double
 -SourceProblems - tests that are either sourced in by a tcl test or have the source command
 -Unknown - name says it all
 -Unnecessary - tests that fail, but we don't need them anyway
 -wm - test that contain the phrase .wm which is a tcl windows manager
//-------------------------------------------------------

Issues with the vtkTclToCsConvertor.py:

In TestRectilinearGridToTetrahedra.tcl vtkDataObject tries to call GetNumberOfCells but it does not own a method called GetNumberOfCells, GetInput needs to be casted to something

vtkPlane.SetOrgin needs to take in an array of doubles instead of a ref double

EnSightRectGridASCII.tcl uses GetCenter on a vtkDataObject and vtkDataObject doesn't have GetCenter

vtkRenderWindowInteractor.SetInteractorStyle is given "" as an argument, but all it can take so far is a vtkInteractorObserver in C:\VTK\Example\Gui\Testing\SphereWidget.cs

vtkDataSetMapper.ScalarVisibilityOf in C:\VTK\Graphics\Testing\Tcl\clipQuadraticCells

base/base1 does not seem to have been defined in C:\VTK\Graphics\Testing\Tcl\clipQuadraticCells

there is a method being called named SetDifuseColor and the object calling it has a method named SetDiffuseColor, but not SetDifuseColor. Possible Error?

clipVolume 2 is trying to take in a ref double, but just listing the numbers seems to work fine will this work for the other ref doubles?

vtkCylinderSource.SetResolveCoincidentTopologyToPolygonOffset in C:\VTK\Example\Rendering\Tcl
