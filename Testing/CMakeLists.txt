SET(EXE_DIR ".")
IF(EXECUTABLE_OUTPUT_PATH)
  SET(EXE_DIR "${EXECUTABLE_OUTPUT_PATH}")
ENDIF(EXECUTABLE_OUTPUT_PATH)

# C# tests:
#
SET(AVDN_CSHARP_TESTS
#  "${CMAKE_CURRENT_SOURCE_DIR}/ExhaustMemoryQuicklyTest.cs"
  "${CMAKE_CURRENT_SOURCE_DIR}/RenderWindowControlTest.cs"
  "${CMAKE_CURRENT_SOURCE_DIR}/vtkActor2DTest.cs"
  "${CMAKE_CURRENT_SOURCE_DIR}/vtkConeSourceTest.cs"
  "${CMAKE_CURRENT_SOURCE_DIR}/vtkImageDataExtraTests.cs"
  "${CMAKE_CURRENT_SOURCE_DIR}/vtkObjectModifiedEvtTest.cs"
  "${CMAKE_CURRENT_SOURCE_DIR}/vtkObjectTest.cs"
  "${CMAKE_CURRENT_SOURCE_DIR}/vtkPointsTest.cs"
  "${CMAKE_CURRENT_SOURCE_DIR}/vtkProgrammableAttributeDataFilterTest.cs"
  "${CMAKE_CURRENT_SOURCE_DIR}/vtkVersionTest.cs"
  "${CMAKE_CURRENT_SOURCE_DIR}/vtkViewSelectionChangedEvtTest.cs"
  "${CMAKE_CURRENT_SOURCE_DIR}/vtkWin32OpenGLRenderWindowTest.cs"
)

IF(WIN32)
  SET(AVDN_CSHARP_TESTS ${AVDN_CSHARP_TESTS}
    "${CMAKE_CURRENT_SOURCE_DIR}/vtkCommandTest.cs"
    "${CMAKE_CURRENT_SOURCE_DIR}/vtkImplicitFunctionSubclassTest.cs"
  )
ENDIF()

CONFIGURE_FILE(
  "${Mummy_DIR}/CSharpTestDriver.cs.in"
  "${CMAKE_CURRENT_BINARY_DIR}/CSharpTestDriver.cs"
  @ONLY
)

SET(AVDN_TEST_CSC_REFS "Kitware.VTK;Kitware.mummy.Runtime")

IF(NOT WIN32)
  #
  # The mono C# compiler does not appear to add these common refs by default
  # like the MS C# compiler does... add them explicitly:
  #
  SET(AVDN_TEST_CSC_REFS ${AVDN_TEST_CSC_REFS}
    "System.Drawing"
    "System.Data"
    "System.Windows.Forms"
    )
ENDIF(NOT WIN32)

ADD_CSHARP_EXECUTABLE(
  "Kitware.VTK.CSharpTests"
  "Kitware.VTK"
  "${AVDN_TEST_CSC_REFS}"
  ""
  ""
  # Source files:
  "${CMAKE_CURRENT_BINARY_DIR}/CSharpTestDriver.cs"
  ${AVDN_CSHARP_TESTS}
)

# C++ tests:
#
SET(AVDN_CXX_TESTS
  "IntentionallyUninitializedData.cxx"
)

# CMake generated cxx test driver uses strcpy and scanf.
# Disable the warnings about them from the MS compiler:
#
#ADD_DEFINITIONS(-D_CRT_NONSTDC_NO_DEPRECATE)
ADD_DEFINITIONS(-D_CRT_SECURE_NO_DEPRECATE)
#ADD_DEFINITIONS(-D_SCL_SECURE_NO_DEPRECATE)

CREATE_TEST_SOURCELIST(AVDN_CXX_TESTS_SOURCELIST
  Kitware.VTK.CxxTests.cxx
  ${AVDN_CXX_TESTS}
)

ADD_EXECUTABLE(
  "Kitware.VTK.CxxTests"
  ${AVDN_CXX_TESTS_SOURCELIST}
)

TARGET_LINK_LIBRARIES("Kitware.VTK.CxxTests"
  vtkViews
  vtkVolumeRendering
)

# Each C# test listed above:
#
FOREACH(test ${AVDN_CSHARP_TESTS})
  GET_FILENAME_COMPONENT(name "${test}" NAME)
  GET_FILENAME_COMPONENT(name_we "${test}" NAME_WE)
  ADD_CSHARP_TEST(${name}
    ${EXE_DIR}/Kitware.VTK.CSharpTests.exe ${name_we}
    -D ${AVDN_VTK_DATA_ROOT}
  )
  SET_TESTS_PROPERTIES(${name} PROPERTIES
    FAIL_REGULAR_EXPRESSION "(ERROR|Warning): In"
  )
ENDFOREACH(test)

# Each C++ test listed above, executed with no arguments:
#
FOREACH(test ${AVDN_CXX_TESTS})
  GET_FILENAME_COMPONENT(name "${test}" NAME)
  GET_FILENAME_COMPONENT(name_we "${test}" NAME_WE)
  ADD_TEST(${name}
    ${EXE_DIR}/Kitware.VTK.CxxTests ${name_we}
  )
  SET_TESTS_PROPERTIES(${name} PROPERTIES
    FAIL_REGULAR_EXPRESSION "(ERROR|Warning): In"
  )
ENDFOREACH(test)

# The "PrintAll" test. Execute vtkObjectTest with a list of
# classnames to instantiate and print...
#
SET(allclasses_file "${CMAKE_CURRENT_BINARY_DIR}/PrintAllTestInput.txt")
FILE(WRITE "${allclasses_file}" "")

SET(table ${WRAPPED_CLASS_TABLE})
FOREACH(entry ${table})
  # each 'entry' looks like: "${cxxclass} ${kit} ${abstract} ${header}"
  STRING(REGEX REPLACE "^([^ ]+) ([^ ]+) ([^ ]+) (.*)$" "\\1" cxxclass "${entry}")

  # The exclusions presently cause this test to crash or fail...
  # Fix VTK and then remove these exclusions:
  #
  IF(NOT cxxclass MATCHES "^(vtkImageViewer2|vtkInteractorStyleTrackball|vtkObjectBase|vtkOrientedPolygonalHandleRepresentation3D|vtkPolygonalHandleRepresentation3D|vtkStructuredPointsGeometryFilter|vtkTreeMapViewer)$")
    FILE(APPEND "${allclasses_file}" "${cxxclass}\n")
  ENDIF(NOT cxxclass MATCHES "^(vtkImageViewer2|vtkInteractorStyleTrackball|vtkObjectBase|vtkOrientedPolygonalHandleRepresentation3D|vtkPolygonalHandleRepresentation3D|vtkStructuredPointsGeometryFilter|vtkTreeMapViewer)$")
ENDFOREACH(entry)

ADD_CSHARP_TEST(PrintAll
  ${EXE_DIR}/Kitware.VTK.CSharpTests.exe vtkObjectTest --classnames-file "${allclasses_file}"
)
SET_TESTS_PROPERTIES(PrintAll PROPERTIES
  FAIL_REGULAR_EXPRESSION "(ERROR|Warning): In"
)

# Special properties for some tests:
#
#SET_TESTS_PROPERTIES(ExhaustMemoryQuicklyTest.cs PROPERTIES
#  WILL_FAIL 1
#)

# Converted tcl tests:
#
ADD_SUBDIRECTORY(TclToCsScript)
