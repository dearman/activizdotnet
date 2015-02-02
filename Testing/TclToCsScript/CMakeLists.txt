# See if AVDN_VTK_SOURCE_DIR is available... if not we are out of luck.
# We need AVDN_VTK_SOURCE_DIR to be able to convert the list of tcl tests.
#
IF(NOT AVDN_VTK_SOURCE_DIR)
  MESSAGE(FATAL_ERROR "required variable AVDN_VTK_SOURCE_DIR is not set...")
ENDIF(NOT AVDN_VTK_SOURCE_DIR)

FIND_PROGRAM(PYTHON_EXE "python")
IF(NOT PYTHON_EXE)
  MESSAGE(FATAL_ERROR "python required to convert tcl tests")
ENDIF(NOT PYTHON_EXE)


# Copy-if-necessary *.py from current source dir to current binary dir. Run
# all python scripts in the binary dir so we can do two or more builds at
# once. When we used to generate *.py scripts in the source tree, multiple
# simultaneous builds would occasionally clobber each other's changes to the
# source tree... Also copy over any files that the python scripts expect to
# be in the "current working directory" when they are run.
foreach(py_file
  GenerateVtkColors.py
  GenerateVtkCommandList.py
  recursiveglob.py
  refdouble.hints
  TclToCsConvertorDriver.py
  TclToCsHashMap.py
  XML_Parser.py
  vtkTclParser.py
  vtkTclToCsConvertor.py
)
  configure_file(
    ${CMAKE_CURRENT_SOURCE_DIR}/${py_file}
    ${CMAKE_CURRENT_BINARY_DIR}/${py_file}
    COPYONLY
  )
endforeach()

# Generate the list of vtkColors
MESSAGE(STATUS "Generating vtkColors.py.in / vtkColors.py")
EXECUTE_PROCESS (COMMAND ${PYTHON_EXE} GenerateVtkColors.py
  -vtkdir ${AVDN_VTK_SOURCE_DIR}
  RESULT_VARIABLE RES_VAR
  WORKING_DIRECTORY ${CMAKE_CURRENT_BINARY_DIR})
configure_file(
  ${CMAKE_CURRENT_BINARY_DIR}/vtkColors.py.in
  ${CMAKE_CURRENT_BINARY_DIR}/vtkColors.py
  COPYONLY
)

# Generate the list of vtkCommands
MESSAGE(STATUS "Generating vtkCommandList.py.in / vtkCommandList.py")
EXECUTE_PROCESS (COMMAND ${PYTHON_EXE} GenerateVtkCommandList.py
  -vtkdir ${AVDN_VTK_SOURCE_DIR}
  RESULT_VARIABLE RES_VAR
  WORKING_DIRECTORY ${CMAKE_CURRENT_BINARY_DIR})
configure_file(
  ${CMAKE_CURRENT_BINARY_DIR}/vtkCommandList.py.in
  ${CMAKE_CURRENT_BINARY_DIR}/vtkCommandList.py
  COPYONLY
)

# vtkClassList.py
FILE(WRITE "${CMAKE_CURRENT_BINARY_DIR}/vtkClassList.py.in" "VTKClassList = [\n\"___list_begins___\",\n")
FOREACH(entry ${WRAPPED_CLASS_TABLE})
  STRING(REGEX REPLACE "^([^ ]+) .*$" "\\1" cxxclass "${entry}")
    # each 'entry' looks like: "${cxxclass} ${kit} ${abstract} ${header}"
  FILE(APPEND "${CMAKE_CURRENT_BINARY_DIR}/vtkClassList.py.in" "\"${cxxclass}\",\n")
ENDFOREACH(entry)
FILE(APPEND "${CMAKE_CURRENT_BINARY_DIR}/vtkClassList.py.in" "\"__list_ends__\"\n]\n\ndef get_vtk_classes():\n  return VTKClassList\n")
configure_file(
  ${CMAKE_CURRENT_BINARY_DIR}/vtkClassList.py.in
  ${CMAKE_CURRENT_BINARY_DIR}/vtkClassList.py
  COPYONLY
)

MACRO(CONVERT_VTK_TCL_SCRIPT_TO_CSHARP cvtstc_file cvtstc_listvar)
  GET_FILENAME_COMPONENT(cvtstc_name_we "${cvtstc_file}" NAME_WE)
  
  ADD_CUSTOM_COMMAND(
    OUTPUT ${CMAKE_CURRENT_BINARY_DIR}/AV${cvtstc_name_we}.cs
    COMMAND ${PYTHON_EXE}
    ARGS
      ${CMAKE_CURRENT_BINARY_DIR}/vtkTclToCsConvertor.py
      -i "${cvtstc_file}"
      -o "${CMAKE_CURRENT_BINARY_DIR}/AV${cvtstc_name_we}.cs"
      -xmldir "${ActiVizDotNet_BINARY_DIR}/xml"
    DEPENDS
      "${cvtstc_file}"
      ${CMAKE_CURRENT_BINARY_DIR}/vtkTclToCsConvertor.py
  )

  SET(${cvtstc_listvar} ${${cvtstc_listvar}} "${CMAKE_CURRENT_BINARY_DIR}/AV${cvtstc_name_we}.cs")
ENDMACRO(CONVERT_VTK_TCL_SCRIPT_TO_CSHARP)

SET(Other_outputlist "")

FILE(READ "Currently_Compiling_Tests.txt" testlist)
STRING(REGEX REPLACE ";" "\\\\;" testlist "${testlist}")
STRING(REGEX REPLACE "\n" "E;" testlist "${testlist}")

FOREACH(t ${testlist})
  IF("${t}" MATCHES "^(.*.tcl).*E$")
  IF(NOT "${t}" MATCHES "^#")
    STRING(REGEX REPLACE "^(.*.tcl).*E$" "\\1" tclfile "${t}")

    IF("${AVDN_VTK_SOURCE_DIR}/${tclfile}" MATCHES "^.*/([^/]+)/Testing/Tcl/.*$")
      STRING(REGEX REPLACE "^.*/([^/]+/[^/]+)/Testing/Tcl/.*$" "\\1" kit "${AVDN_VTK_SOURCE_DIR}/${tclfile}")
	  STRING(REGEX REPLACE "/" "_" kit "${kit}")
    ELSE()
      SET(kit "Other")
    ENDIF()
    
	# Add the list of kits
    if(NOT ${kit} EQUAL "Other")
	  list(FIND kits ${kit} KITFOUND)
	  if(KITFOUND EQUAL "-1")
	    set(kits ${kits} ${kit})
	   endif()
	 endif()
    #MESSAGE(STATUS "kit='${kit}' tclfile='${tclfile}'")
    CONVERT_VTK_TCL_SCRIPT_TO_CSHARP("${AVDN_VTK_SOURCE_DIR}/${tclfile}" ${kit}_outputlist)
  ENDIF(NOT "${t}" MATCHES "^#")
  ENDIF("${t}" MATCHES "^(.*.tcl).*E$")
ENDFOREACH(t)


# Custom targets to organize things in the VS .sln file and to ensure
# that Kitware.VTK builds before these targets. (So that all the xml
# files from all the gccxml steps are guaranteed to be there before
# running the python script to convert a test, which may require an
# arbitrary set of the xml files to process any given test...)
#
ADD_CUSTOM_TARGET(
  "ConvertTcl-Other" ALL
  DEPENDS ${Other_outputlist}
)
ADD_DEPENDENCIES("ConvertTcl-Other" Kitware.VTK)

FOREACH(kit ${kits})
  ADD_CUSTOM_TARGET(
    "ConvertTcl-${kit}Tests" ALL
    DEPENDS ${${kit}_outputlist}
  )
  ADD_DEPENDENCIES("ConvertTcl-${kit}Tests" Kitware.VTK)
ENDFOREACH(kit)


SET(AVDN_CONVERTED_TCL_TESTS ${Other_outputlist})
FOREACH(kit ${kits})
  SET(AVDN_CONVERTED_TCL_TESTS ${AVDN_CONVERTED_TCL_TESTS} ${${kit}_outputlist})
ENDFOREACH(kit)

ADD_CSHARP_EXECUTABLE(
  "Kitware.VTK.ConvertedTclTests"
  "Kitware.VTK"
  "Kitware.VTK;Kitware.mummy.Runtime"
  ""
  ""
  "${CMAKE_CURRENT_SOURCE_DIR}/CSharpTestDriver.cs"
  ${AVDN_CONVERTED_TCL_TESTS}
)

# Ensure Kitware.VTK.ConvertedTclTests builds *after* all the
# "ConvertTcl-*" projects. Otherwise, it tries to build the
# custom command steps in parallel with the ConvertTcl projects
# and some end up building twice because of this...
#
ADD_DEPENDENCIES("Kitware.VTK.ConvertedTclTests" "ConvertTcl-Other")
FOREACH(kit ${kits})
  ADD_DEPENDENCIES("Kitware.VTK.ConvertedTclTests" "ConvertTcl-${kit}Tests")
ENDFOREACH(kit)

# Tell test driver whether or not to do the image regression
SET(fail_on_image_diff_flag "--fail-on-image-diff")

FOREACH(kit ${kits})

  set(create_test 1)
  # For now disable the parallel test if VTK was not built with parallel
  if(NOT AVDN_VTK_PARALLEL_MPI)
    if(${kit} MATCHES "Parallel")
	  set(create_test 0)
    endif()
  endif()
  
  if(${create_test})
  FOREACH(test ${${kit}_outputlist})
	STRING(REGEX REPLACE "_" "/" kitdir "${kit}")
    GET_FILENAME_COMPONENT(name "${test}" NAME)
    GET_FILENAME_COMPONENT(name_we "${test}" NAME_WE)
    STRING(REGEX REPLACE "^AV(.*)$" "\\1" pngname "${name_we}")
    ADD_CSHARP_TEST(${name}
      ${EXE_DIR}/Kitware.VTK.ConvertedTclTests.exe ${name_we}
      -D ${AVDN_VTK_DATA_ROOT}
      -T ${CMAKE_BINARY_DIR}/Testing/Temporary
      -V ${VTK_DIR}/ExternalData/${kitdir}/Testing/Data/Baseline/${pngname}.png
      ${fail_on_image_diff_flag}
    )
    SET_TESTS_PROPERTIES(${name} PROPERTIES
      FAIL_REGULAR_EXPRESSION "(ERROR|Warning): In"
    )
  ENDFOREACH(test)
  endif()
ENDFOREACH(kit)


FILE(GLOB_RECURSE HAND_CONVERTED_TESTS "Convert By Hand/*.cs")

#Remove Parallel test if Parallel is not built
FOREACH(test ${HAND_CONVERTED_TESTS})
  GET_FILENAME_COMPONENT(path1 "${test}" PATH)
  GET_FILENAME_COMPONENT(dir "${path1}" NAME)
  GET_FILENAME_COMPONENT(path2 "${path1}" PATH)
  GET_FILENAME_COMPONENT(dir2 "${path2}" NAME)
  IF(${dir} STREQUAL "Parallel" OR ${dir2} STREQUAL "Parallel")
    IF(NOT AVDN_VTK_PARALLEL_MPI)
      LIST(REMOVE_ITEM HAND_CONVERTED_TESTS ${test})
    ENDIF()
  ENDIF()
ENDFOREACH()

#classes that use parallel classes but are not in the parallel folder
IF(NOT AVDN_VTK_PARALLEL_MPI)
  LIST(REMOVE_ITEM HAND_CONVERTED_TESTS "${CMAKE_CURRENT_SOURCE_DIR}/Convert By Hand/Graphics/AVStreamPolyData.cs")
ENDIF(NOT AVDN_VTK_PARALLEL_MPI)

ADD_CSHARP_EXECUTABLE(
  "Kitware.VTK.ConvertedByHandTclTests"
  "Kitware.VTK"
  "Kitware.VTK;Kitware.mummy.Runtime"
  ""
  ""
  "${CMAKE_CURRENT_SOURCE_DIR}/CSharpTestDriver.cs"
  ${HAND_CONVERTED_TESTS}
)

# List of tests that should be running but if TCL is not enabled for
# VTK then the baseline images might not be downloaded from MIDAS
set(TEST_WITHOUT_BASELINE
AVTestPolygonWriters.cs
)

FOREACH(test ${HAND_CONVERTED_TESTS})
  GET_FILENAME_COMPONENT(path "${test}" PATH)
  GET_FILENAME_COMPONENT(dir "${path}" NAME)
  GET_FILENAME_COMPONENT(path2 "${path}" PATH)
  GET_FILENAME_COMPONENT(dir2 "${path2}" NAME)
  GET_FILENAME_COMPONENT(name "${test}" NAME)
  GET_FILENAME_COMPONENT(name_we "${test}" NAME_WE)
  
  # Check if the current name is in the list of tests without baseline
  SET(fail_on_image_diff_flag "--fail-on-image-diff")
  list(FIND TEST_WITHOUT_BASELINE ${name} HasNoBaseline)
  IF(NOT HasNoBaseline EQUAL -1)
    set(fail_on_image_diff_flag "")
  ENDIF()

  STRING(REGEX REPLACE "^AV(.*)$" "\\1" pngname "${name_we}")
  ADD_CSHARP_TEST(${name}
    ${EXE_DIR}/Kitware.VTK.ConvertedByHandTclTests.exe ${name_we}
    -D ${AVDN_VTK_DATA_ROOT}
    -T ${CMAKE_BINARY_DIR}/Testing/Temporary
    -V ${VTK_DIR}/ExternalData/${dir2}/${dir}/Testing/Data/Baseline/${pngname}.png
    ${fail_on_image_diff_flag}
  )
  SET_TESTS_PROPERTIES(${name} PROPERTIES
    FAIL_REGULAR_EXPRESSION "(ERROR): In"
#    FAIL_REGULAR_EXPRESSION "(ERROR|Warning): In"
  )
ENDFOREACH(test)

