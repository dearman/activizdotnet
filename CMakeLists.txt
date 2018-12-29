CMAKE_MINIMUM_REQUIRED(VERSION 2.8.8 FATAL_ERROR)
PROJECT(ActiVizDotNet)

set(VTK_BUILD_SHARED_LIBS "${BUILD_SHARED_LIBS}")

# Determine if we're building ActiVizDotNet (AVDN) as a "top level" project.
#
set(AVDN_BUILD_IS_TOP_LEVEL 0)
if("${ActiVizDotNet_BINARY_DIR}" STREQUAL "${CMAKE_BINARY_DIR}")
  set(AVDN_BUILD_IS_TOP_LEVEL 1)
endif("${ActiVizDotNet_BINARY_DIR}" STREQUAL "${CMAKE_BINARY_DIR}")

if(AVDN_BUILD_IS_TOP_LEVEL)
  if(NOT LIBRARY_OUTPUT_PATH)
    set(LIBRARY_OUTPUT_PATH ${ActiVizDotNet_BINARY_DIR}/bin CACHE INTERNAL "Single output directory for building all libraries.")
  endif(NOT LIBRARY_OUTPUT_PATH)
  if(NOT EXECUTABLE_OUTPUT_PATH)
    set(EXECUTABLE_OUTPUT_PATH ${ActiVizDotNet_BINARY_DIR}/bin CACHE INTERNAL "Single output directory for building all executables.")
  endif(NOT EXECUTABLE_OUTPUT_PATH)
endif(AVDN_BUILD_IS_TOP_LEVEL)

# Make it clear that ActiViz is now open source: set the edition to
# "OpenSource" - the Commercial Edition is no longer for sale, but the
# OpenSource Edition is equivalent to it functionally. The Personal Edition
# restrictions are nowhere to be found... :-)
#
#set(AVDN_EDITION "OpenSource") 
set(AVDN_EDITION "Supported") 
set(AVDN_EDITION_SUFFIX "-Supported")

option(BUILD_TESTING "Build testing" OFF)
if(BUILD_TESTING)
  INCLUDE(CTest)
  ENABLE_TESTING()
endif()

option(BUILD_EXAMPLES "Build examples" OFF)

# List of MSVC dlls for our INSTALL rules below:
#
SET(CMAKE_INSTALL_SYSTEM_RUNTIME_LIBS_SKIP 1)
  # because we need the list and we'll add the INSTALL rule conditionally below...

#SET(CMAKE_INSTALL_DEBUG_LIBRARIES 1)
  # uncomment if you need to make a debug install/package...

INCLUDE(InstallRequiredSystemLibraries)

# Mummy:
#
find_package(Mummy REQUIRED) # >= 1.0.3
if(NOT Mummy_BASE_DIR)
  message(FATAL_ERROR "error: Mummy_BASE_DIR not defined. Please set Mummy_DIR to the directory containing MummyConfig.cmake")
endif()


#message(STATUS "<mummy>")
foreach(v
  Mummy_BASE_DIR
  Mummy_BIN_DIR
  Mummy_CMAKE_BUILD_TYPE
  Mummy_CMAKE_CONFIGURATION_TYPES
  Mummy_CONFIG_DIR
  Mummy_CONFIG_DIR_SUFFIX
  Mummy_CONFIG_FILE
  Mummy_CONFIGURATION_TYPE
  Mummy_DIR
  mummy_EXECUTABLE
  Mummy_INCLUDE_DIRS
  Mummy_LIB_DIR
  Mummy_SELECTED_CONFIGURATION
  Mummy_VERSION_MAJOR
  Mummy_VERSION_MINOR
  Mummy_VERSION_PATCH
  Mummy_VERSION_STRING
  )
  #message(STATUS "${v}='${${v}}'")
endforeach()
#message(STATUS "</mummy>")


# Macros to make building C# libs and exes easy:
#
include("${Mummy_DIR}/MummyCMakeMacros.cmake")


# Set CSC_DEBUG_FLAG prior to including FindCsharp.cmake if we want
# to force C# debugging info... If it's set, it's honored. If it's
# not defined, then FindCsharp.cmake uses its own logic to choose
# a reasonable default for CSC_DEBUG_FLAG.
#
if(DEFINED BUILDNAME)
  # Likely built via a script, default debug setting is OFF:
  set(AVDN_BUILD_CSHARP_DEBUG_DEFAULT OFF)
else()
  # Likely built by a real live developer, default debug setting is ON:
  set(AVDN_BUILD_CSHARP_DEBUG_DEFAULT ON)
endif()

option(AVDN_BUILD_CSHARP_DEBUG
  "Build C# components with '/debug' command line flag"
  ${AVDN_BUILD_CSHARP_DEBUG_DEFAULT}
)

if(AVDN_BUILD_CSHARP_DEBUG)
  set(CSC_DEBUG_FLAG "/debug")
endif()


if(NOT DEFINED AVDN_CONFIG)
  if(AVDN_BUILD_CSHARP_DEBUG)
    set(AVDN_CONFIG_DEFAULT "Debug")
  else()
    set(AVDN_CONFIG_DEFAULT "Release")
  endif()
  set(AVDN_CONFIG "${AVDN_CONFIG_DEFAULT}") # CACHE?
endif()

# Define MUMMY_ADD_CSHARP_CONFIG so the linkresource arguments get put
# into the response file instead of directly on the csc command line,
# because there are too many VTK dlls to list them all on an 8K
# command line now...
set(MUMMY_ADD_CSHARP_CONFIG "${AVDN_CONFIG}")


# Tools locations - either already found by parent project, specified on the
# command line, or available in the PATH... If not, you'll have to find
# these things yourself at configure time... :-)
#
include("${Mummy_DIR}/FindCsharp.cmake")

if(NOT csc_EXECUTABLE)
  find_program(csc_EXECUTABLE csc)
endif()
if(NOT csc_EXECUTABLE)
  message(FATAL_ERROR "error: C# compiler not found. csc_EXECUTABLE='${csc_EXECUTABLE}'")
endif()

if(NOT mummy_EXECUTABLE)
  find_program(mummy_EXECUTABLE mummy)
endif()
if(NOT mummy_EXECUTABLE)
  message(FATAL_ERROR "error: mummy not found. mummy_EXECUTABLE='${mummy_EXECUTABLE}'")
endif()

if(NOT gccxml_EXECUTABLE)
  find_program(gccxml_EXECUTABLE gccxml)
endif()
if(NOT gccxml_EXECUTABLE)
  message(FATAL_ERROR "error: gccxml not found. gccxml_EXECUTABLE='${gccxml_EXECUTABLE}'")
endif()


include_directories(${Mummy_INCLUDE_DIRS})


#set(gccxml_compiler "${CMAKE_CXX_COMPILER}")
#if(MSVC80)
#  set(gccxml_compiler "msvc8")
#endif()
#if(MSVC90)
#  set(gccxml_compiler "msvc9")
#endif()

# VTK:
#
FIND_PACKAGE(VTK)
IF(NOT VTK_USE_FILE)
  MESSAGE(FATAL_ERROR "error: VTK_USE_FILE not defined. Please set VTK_DIR to a compatible VTK build/binary tree...")
ENDIF(NOT VTK_USE_FILE)
INCLUDE(${VTK_USE_FILE})

# Read VTK_SOURCE_DIR value from "${VTK_DIR}/CMakeCache.txt" (assumes that
# VTK_DIR points to a build tree of VTK where the source directory is also
# still valid/in-sync -- does not work with an install tree of VTK...)
# Save the value in our own variable, AVDN_VTK_SOURCE_DIR.
#
# We need this value to access the @AVDN_VTK_SOURCE_DIR@/Wrapping/hints file
# which is referenced by MummySettings.xml.
#
# We also need it down in the Testing subdirectory to access *.tcl
# scripts so they can be converted to C# equivalents.
#
# We also pull out the values of the following VTK cache variables for our
# own information:
#   VTK_DATA_ROOT
#   VTK_USE_PARALLEL
#   VTK_USE_SYSTEM_EXPAT
#   VTK_USE_SYSTEM_FREETYPE
#   VTK_USE_SYSTEM_JPEG
#   VTK_USE_SYSTEM_LIBPROJ4
#   VTK_USE_SYSTEM_LIBXML2
#   VTK_USE_SYSTEM_PNG
#   VTK_USE_SYSTEM_TIFF
#   VTK_USE_SYSTEM_ZLIB
#
FILE(READ "${VTK_DIR}/CMakeCache.txt" AVDN_vtk_cmakecache)
STRING(REGEX REPLACE ";" "\\\\;" AVDN_vtk_cmakecache "${AVDN_vtk_cmakecache}")
STRING(REGEX REPLACE "\n" "E;" AVDN_vtk_cmakecache "${AVDN_vtk_cmakecache}")
SET(AVDN_VTK_SOURCE_DIR "")

FOREACH(line ${AVDN_vtk_cmakecache})
 IF(NOT line MATCHES "-ADVANCED:INTERNAL=")

  IF(line MATCHES "^VTK_SOURCE_DIR.*=(.*)E$")
    STRING(REGEX REPLACE "^VTK_SOURCE_DIR.*=(.*)E$" "\\1" AVDN_VTK_SOURCE_DIR "${line}")
  ENDIF(line MATCHES "^VTK_SOURCE_DIR.*=(.*)E$")

  IF(line MATCHES "^Module_vtkParallelMPI.*=(.*)E$")
    STRING(REGEX REPLACE "^Module_vtkParallelMPI.*=(.*)E$" "\\1" AVDN_VTK_PARALLEL_MPI "${line}")
  ENDIF(line MATCHES "^Module_vtkParallelMPI.*=(.*)E$")

  IF(line MATCHES "^VTK_USE_SYSTEM_EXPAT.*=(.*)E$")
    STRING(REGEX REPLACE "^VTK_USE_SYSTEM_EXPAT.*=(.*)E$" "\\1" AVDN_VTK_USE_SYSTEM_EXPAT "${line}")
  ENDIF(line MATCHES "^VTK_USE_SYSTEM_EXPAT.*=(.*)E$")

  IF(line MATCHES "^VTK_USE_SYSTEM_FREETYPE.*=(.*)E$")
    STRING(REGEX REPLACE "^VTK_USE_SYSTEM_FREETYPE.*=(.*)E$" "\\1" AVDN_VTK_USE_SYSTEM_FREETYPE "${line}")
  ENDIF(line MATCHES "^VTK_USE_SYSTEM_FREETYPE.*=(.*)E$")

  IF(line MATCHES "^VTK_USE_SYSTEM_JPEG.*=(.*)E$")
    STRING(REGEX REPLACE "^VTK_USE_SYSTEM_JPEG.*=(.*)E$" "\\1" AVDN_VTK_USE_SYSTEM_JPEG "${line}")
  ENDIF(line MATCHES "^VTK_USE_SYSTEM_JPEG.*=(.*)E$")

  IF(line MATCHES "^VTK_USE_SYSTEM_LIBPROJ4.*=(.*)E$")
    STRING(REGEX REPLACE "^VTK_USE_SYSTEM_LIBPROJ4.*=(.*)E$" "\\1" AVDN_VTK_USE_SYSTEM_LIBPROJ4 "${line}")
  ENDIF(line MATCHES "^VTK_USE_SYSTEM_LIBPROJ4.*=(.*)E$")

  IF(line MATCHES "^VTK_USE_SYSTEM_LIBXML2.*=(.*)E$")
    STRING(REGEX REPLACE "^VTK_USE_SYSTEM_LIBXML2.*=(.*)E$" "\\1" AVDN_VTK_USE_SYSTEM_LIBXML2 "${line}")
  ENDIF(line MATCHES "^VTK_USE_SYSTEM_LIBXML2.*=(.*)E$")

  IF(line MATCHES "^VTK_USE_SYSTEM_PNG.*=(.*)E$")
    STRING(REGEX REPLACE "^VTK_USE_SYSTEM_PNG.*=(.*)E$" "\\1" AVDN_VTK_USE_SYSTEM_PNG "${line}")
  ENDIF(line MATCHES "^VTK_USE_SYSTEM_PNG.*=(.*)E$")

  IF(line MATCHES "^VTK_USE_SYSTEM_TIFF.*=(.*)E$")
    STRING(REGEX REPLACE "^VTK_USE_SYSTEM_TIFF.*=(.*)E$" "\\1" AVDN_VTK_USE_SYSTEM_TIFF "${line}")
  ENDIF(line MATCHES "^VTK_USE_SYSTEM_TIFF.*=(.*)E$")

  IF(line MATCHES "^VTK_USE_SYSTEM_ZLIB.*=(.*)E$")
    STRING(REGEX REPLACE "^VTK_USE_SYSTEM_ZLIB.*=(.*)E$" "\\1" AVDN_VTK_USE_SYSTEM_ZLIB "${line}")
  ENDIF(line MATCHES "^VTK_USE_SYSTEM_ZLIB.*=(.*)E$")

 ENDIF(NOT line MATCHES "-ADVANCED:INTERNAL=")
ENDFOREACH(line)

set(AVDN_EXTRA_EXCLUDE_CLASSES ${AVDN_EXTRA_EXCLUDE_CLASSES} CACHE STRING
  "Force the wrap_exclude to 1 for these classes")

set(AVDN_EXTRA_EXCLUDE_CLASSES_XML "")
foreach(class ${AVDN_EXTRA_EXCLUDE_CLASSES})
  set(AVDN_EXTRA_EXCLUDE_CLASSES_XML
    "${AVDN_EXTRA_EXCLUDE_CLASSES_XML}  <Class name=\"${class}\" wrappable=\"false\"/>\n")
endforeach()
if(NOT "${AVDN_EXTRA_EXCLUDE_CLASSES_XML}" STREQUAL "")
  set(AVDN_EXTRA_EXCLUDE_CLASSES_XML "\n  <!-- AVDN_EXTRA_EXCLUDE_CLASSES_XML -->\n${AVDN_EXTRA_EXCLUDE_CLASSES_XML}")
endif()

# Set the VTK Data root
set(AVDN_VTK_DATA_ROOT "${VTK_DIR}/ExternalData/Testing")


# If a real strong name signing keyfile has not been predefined,
# then use the public key file in the source tree to /delaysign
# .NET assemblies...
#
set(AVDN_SNKEYFILE "" CACHE FILEPATH "Specify the SNKEY" )
if(AVDN_SNKEYFILE STREQUAL "")
  get_filename_component(AVDN_SNKEYFILE "${ActiVizDotNet_SOURCE_DIR}/Kitware.VTK.pub.snk" ABSOLUTE)
endif()


message(STATUS "AVDN_EDITION='${AVDN_EDITION}'")
message(STATUS "AVDN_EXTRA_EXCLUDE_CLASSES='${AVDN_EXTRA_EXCLUDE_CLASSES}'")
message(STATUS "AVDN_SNKEYFILE='${AVDN_SNKEYFILE}'")
message(STATUS "AVDN_VTK_DATA_ROOT='${AVDN_VTK_DATA_ROOT}'")
message(STATUS "AVDN_VTK_SOURCE_DIR='${AVDN_VTK_SOURCE_DIR}'")
message(STATUS "AVDN_VTK_USE_SYSTEM_EXPAT='${AVDN_VTK_USE_SYSTEM_EXPAT}'")
message(STATUS "AVDN_VTK_USE_SYSTEM_FREETYPE='${AVDN_VTK_USE_SYSTEM_FREETYPE}'")
message(STATUS "AVDN_VTK_USE_SYSTEM_JPEG='${AVDN_VTK_USE_SYSTEM_JPEG}'")
message(STATUS "AVDN_VTK_USE_SYSTEM_LIBPROJ4='${AVDN_VTK_USE_SYSTEM_LIBPROJ4}'")
message(STATUS "AVDN_VTK_USE_SYSTEM_LIBXML2='${AVDN_VTK_USE_SYSTEM_LIBXML2}'")
message(STATUS "AVDN_VTK_USE_SYSTEM_PNG='${AVDN_VTK_USE_SYSTEM_PNG}'")
message(STATUS "AVDN_VTK_USE_SYSTEM_TIFF='${AVDN_VTK_USE_SYSTEM_TIFF}'")
message(STATUS "AVDN_VTK_USE_SYSTEM_ZLIB='${AVDN_VTK_USE_SYSTEM_ZLIB}'")

message(STATUS "VTK_BUILD_SHARED_LIBS='${VTK_BUILD_SHARED_LIBS}'")
message(STATUS "VTK_DIR='${VTK_DIR}'")

message(STATUS "csc_EXECUTABLE='${csc_EXECUTABLE}'")
message(STATUS "gccxml_EXECUTABLE='${gccxml_EXECUTABLE}'")
message(STATUS "mono_EXECUTABLE='${mono_EXECUTABLE}'")
message(STATUS "mummy_EXECUTABLE='${mummy_EXECUTABLE}'")

message(STATUS "gccxml_compiler='${gccxml_compiler}'...")


# Organization:
#
# If MV_ONE_EXPORT_LAYER_DLL is 0 then all the generated export-layer/*.cxx
# files get built into multiple export layer DLLs called
# ${CMAKE_SHARED_LIBRARY_PREFIX}${module}EL${CMAKE_SHARED_LIBRARY_SUFFIX}.
# If VTK is built as dlls, we need to use multiple export layer DLLs.
#
# If MV_ONE_EXPORT_LAYER_DLL is 1 then all the generated export-layer/*.cxx
# files get built into one large export layer DLL called
# ${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.VTK.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}.
# If VTK is built as static libs, we need to use one large export layer DLL.
#
if(VTK_BUILD_SHARED_LIBS)
  set(MV_ONE_EXPORT_LAYER_DLL 0)
else()
  set(MV_ONE_EXPORT_LAYER_DLL 1)
endif()


set(AVDN_SOURCE_TREE_IS_SVN_CHECKOUT 0)
set(AVDN_SVN_STATUS "")
set(AVDN_SVN_STATUS_BRIEF "")
set(AVDN_SVN_STATUS_ENCODED "")
set(AVDN_WC_LAST_CHANGED_REV "1009")
  #
  # This value is an artificial number that is simply incremented manually
  # now after switching to git from svn... Whenever we do "real packaging
  # releases" this number should be incremented manually until this code
  # is updated to reflect our shiny, new git repo-ness.

# Version
#
configure_file(
  "${ActiVizDotNet_SOURCE_DIR}/ActiVizDotNetVersion.cmake.in"
  "${ActiVizDotNet_BINARY_DIR}/ActiVizDotNetVersion.cmake"
  @ONLY
)
include("${ActiVizDotNet_BINARY_DIR}/ActiVizDotNetVersion.cmake")

include_directories(${ActiVizDotNet_SOURCE_DIR})


set(AVDN_INSTALL_FILES "")
set(AVDN_INSTALL_TARGETS "")


# Events
#
# Still TODO:
#
# (1) - Generate correct RemoveAllRelayHandlers code for each object
# that has any events.
#
# (2) - figure out a way to get RemoveAllRelayHandlers called from the
# right place in the Dispose method...
#
# (3) - Use ADD_CUSTOM_COMMAND and a custom target to set up dependencies
# on the "EventFragment-*" output files from this script and the "extra.cs"
# files where the fragments will eventually end up. For now, just put all
# of them into vtkObject_Extra.cs
#
add_custom_command(
  OUTPUT ${ActiVizDotNet_BINARY_DIR}/csharp/GenerateEventFragmentsSentinel.txt
  COMMAND ${CMAKE_COMMAND}
    "-DActiVizDotNet_BINARY_DIR:STRING=${ActiVizDotNet_BINARY_DIR}"
    "-DAVDN_VTK_SOURCE_DIR:STRING=${AVDN_VTK_SOURCE_DIR}"
    -P "${ActiVizDotNet_SOURCE_DIR}/GenerateEventFragments.cmake"
  DEPENDS
    ${ActiVizDotNet_SOURCE_DIR}/GenerateEventFragments.cmake
    ${AVDN_VTK_SOURCE_DIR}/Common/Core/vtkCommand.h
    ${CMAKE_COMMAND}
)


# Use these settings when building with the Msvc compiler:
#
include("${ActiVizDotNet_SOURCE_DIR}/MsvcMacros.cmake")
#MSVC_LINK_TO_STATIC_CRT()
MSVC80_FORCE_MANIFEST_LINKER_FLAG()
MSVC80_SUPPRESS_CRT_DEPRECATED_WARNINGS()


set(exe_dir "${VTK_BINARY_DIR}/${CMAKE_CFG_INTDIR}")
if(EXECUTABLE_OUTPUT_PATH)
  set(exe_dir "${EXECUTABLE_OUTPUT_PATH}/${CMAKE_CFG_INTDIR}")
endif()


# Run gccxml/mummy on each wrapped class:
#
set(WRAPPED_CLASS_TABLE "")
set(generated_files "")
set(ALLMODULES_EL_SOURCES "")
set(ALLMODULES_CS_SOURCES "")
set(ALLMODULES_SENTINELS "")

configure_file(
  "${ActiVizDotNet_SOURCE_DIR}/MummySettings.xml.in"
  "${ActiVizDotNet_BINARY_DIR}/xml/MummySettings.xml"
  @ONLY
)

configure_file(
  "${ActiVizDotNet_SOURCE_DIR}/vtkObjectEL_Extra.cxx.in"
  "${ActiVizDotNet_BINARY_DIR}/export-layer/vtkObjectEL_Extra.cxx"
  @ONLY
)

#configure_file(
#  "${ActiVizDotNet_SOURCE_DIR}/vtkObject_Extra.cs.in"
#  "${ActiVizDotNet_BINARY_DIR}/csharp/vtkObject_Extra.cs"
#  @ONLY
#)

add_custom_command(
  OUTPUT ${ActiVizDotNet_BINARY_DIR}/csharp/AssembleExtraSourcesSentinel.txt
  COMMAND ${CMAKE_COMMAND}
    "-DActiVizDotNet_BINARY_DIR:STRING=${ActiVizDotNet_BINARY_DIR}"
    "-Daes_class:STRING=vtkObject"
    -P "${ActiVizDotNet_SOURCE_DIR}/AssembleExtraSources.cmake"
  DEPENDS
    ${ActiVizDotNet_SOURCE_DIR}/AssembleExtraSources.cmake
    ${ActiVizDotNet_SOURCE_DIR}/vtkObject_Extra.cs.in
    ${ActiVizDotNet_SOURCE_DIR}/vtkObject_EventFragments.cmake
    ${ActiVizDotNet_BINARY_DIR}/csharp/GenerateEventFragmentsSentinel.txt
    ${CMAKE_COMMAND}
)

add_custom_target(
  AssembleExtraSources ALL
  DEPENDS ${ActiVizDotNet_BINARY_DIR}/csharp/AssembleExtraSourcesSentinel.txt
)


# Set variables for proper incremental rebuilds when hand-crafted
# C# or C++ export layer files are modified. Any files listed as
# extra code files in MummySettings.xml.in should be listed in
# some class's _EXTRA_DEPENDENCIES variable...
#
set(vtkImageData_EXTRA_DEPENDENCIES
  "${ActiVizDotNet_SOURCE_DIR}/vtkImageDataEL_Extra.cxx"
  "${ActiVizDotNet_SOURCE_DIR}/vtkImageData_Extra.cs"
)

set(vtkObject_EXTRA_DEPENDENCIES
  "${ActiVizDotNet_BINARY_DIR}/export-layer/vtkObjectEL_Extra.cxx"
  "${ActiVizDotNet_BINARY_DIR}/csharp/vtkObject_Extra.cs"
)

set(vtkProgrammableAttributeDataFilter_EXTRA_DEPENDENCIES
  "${ActiVizDotNet_SOURCE_DIR}/vtkProgrammableAttributeDataFilterEL_Extra.cxx"
  "${ActiVizDotNet_SOURCE_DIR}/vtkProgrammableAttributeDataFilter_Extra.cs"
)

set(vtkStringArray_EXTRA_DEPENDENCIES
  "${ActiVizDotNet_SOURCE_DIR}/vtkStringArrayEL_Extra.cxx"
  "${ActiVizDotNet_SOURCE_DIR}/vtkStringArray_Extra.cs"
)


macro(derive_gccxml_include_args)
  set(gccxml_include_args "")
  set(dirs ${ARGV})
  list(REMOVE_DUPLICATES dirs)
  foreach(dir ${ARGV})
    set(gccxml_include_args ${gccxml_include_args} "-I${dir}")
  endforeach()
endmacro()

#
# Exclude Modules
#
set(vtkDomainsChemistry_EXCLUDE_FROM_WRAPPING 1)
set(vtkDomainsChemistryOpenGL2_EXCLUDE_FROM_WRAPPING 1)

set(modules ${VTK_MODULES_ENABLED})
foreach(module ${modules})
  if(NOT ${module}_EXCLUDE_FROM_WRAPPING)
    message(STATUS "CSharp wrapping '${module}'...")

    foreach(class ${AVDN_EXTRA_EXCLUDE_CLASSES})
      set(VTK_CLASS_WRAP_EXCLUDE_${class} 1)
    endforeach()

    set(${module}_EL_SOURCES "")
    set(${module}_CS_SOURCES "")
    set(${module}_SENTINELS "")

    vtk_module_load(${module})
    vtk_module_headers_load(${module})
    vtk_module_dep_includes(${module})

    derive_gccxml_include_args(
      ${Mummy_INCLUDE_DIRS}
      ${${module}_DEPENDS_INCLUDE_DIRS}
      ${${module}_INCLUDE_DIRS}
      ${${module}_SYSTEM_INCLUDE_DIRS}
      )
	  
	# List of classes that should be parsed by GCCXML with the #define __WRAP__ option  
	set(AVDN_VTK__WRAP__CLASSES 
		#vtkBitArray #already have the functions
		vtkCharArray
		vtkDoubleArray
		vtkFloatArray
		vtkIdTypeArray
		vtkIntArray
		vtkLongArray
		vtkLongLongArray
		vtkShortArray
		vtkSignedCharArray
		vtkUnsignedCharArray
		vtkUnsignedIntArray
		vtkUnsignedLongArray
		vtkUnsignedLongLongArray
		vtkUnsignedShortArray
		vtkSMPTools
		vtkHyperTreeGrid
		vtkMappedUnstructuredGrid)

    foreach(class ${${module}_HEADERS})
    if(NOT ${module}_HEADER_${class}_WRAP_EXCLUDE)
      set(header "${class}.h")
      set(cxxclass "${class}")

      # Build one master in-memory table so that we don't have to re-do all the
      # logic in this nested FOREACH loop later on... Instead we'll simply iterate
      # the in-memory table built here:
      #
      set(WRAPPED_CLASS_TABLE ${WRAPPED_CLASS_TABLE} "${cxxclass} ${module} ${header}")

	  # If the class is an array we need to #define the __WRAP__ option for gccxml to pickup
	  # the real base class (because it's templated).
      list(FIND AVDN_VTK__WRAP__CLASSES ${class} FileUseVTK__WRAP__)

	  if(FileUseVTK__WRAP__ EQUAL -1)
	    configure_file(
			"${ActiVizDotNet_SOURCE_DIR}/gccxml.cxx.in"
			"${ActiVizDotNet_BINARY_DIR}/xml/${cxxclass}_gccxml.cxx"
			@ONLY
		 )
	  else()
		configure_file(
			"${ActiVizDotNet_SOURCE_DIR}/gccxml.wrapvtk.cxx.in"
			"${ActiVizDotNet_BINARY_DIR}/xml/${cxxclass}_gccxml.cxx"
			@ONLY
		 )
	  endif()
	
 
IF(HasMyModule EQUAL -1)
  message(FATAL_ERROR "You must build VTK with more modules.")
ENDIF()


      add_custom_command(
        OUTPUT ${ActiVizDotNet_BINARY_DIR}/xml/${cxxclass}.xml
        COMMAND ${gccxml_EXECUTABLE}
          -fxml=${ActiVizDotNet_BINARY_DIR}/xml/${cxxclass}.xml
          -fxml-start=_cable_
          ${gccxml_include_args} -DCABLE_CONFIGURATION 
          --gccxml-compiler ${gccxml_compiler}
          ${ActiVizDotNet_BINARY_DIR}/xml/${cxxclass}_gccxml.cxx
        DEPENDS
          ${ActiVizDotNet_BINARY_DIR}/xml/${cxxclass}_gccxml.cxx
          ${header}
          ${gccxml_EXECUTABLE}
      )

#
# *before* custom command re-arranging:
#
#          OUTPUT ${ActiVizDotNet_BINARY_DIR}/csharp/${cxxclass}.cs
#            ${ActiVizDotNet_BINARY_DIR}/export-layer/${cxxclass}EL.cxx
#
# Do not list the *.cs or *EL.cxx files as outputs of this custom command.
# If you do, the custom command chains out into other targets rather than
# being defined solely in the "GenerateCSharp" custom target.
#
# The output of this command is the generated sentinel file that the
# "GenerateCSharp" target depends on. The other files are generated as an
# intentional "side effect" and after the target is done building, the other
# targets that build the generated source code may build... That is controlled
# by target level dependencies to reduce complexity.
#

      add_custom_command(
        OUTPUT ${ActiVizDotNet_BINARY_DIR}/csharp/${cxxclass}-sentinel.txt
        COMMAND ${mummy_EXECUTABLE}
          --suppress-warnings 6005 6006 6009 6010 6012 6013 6015 6016 6017 6018 6019 7003
          --settings-file ${ActiVizDotNet_BINARY_DIR}/xml/MummySettings.xml
          --gccxml-file ${ActiVizDotNet_BINARY_DIR}/xml/${cxxclass}.xml
          --csharp-file ${ActiVizDotNet_BINARY_DIR}/csharp/${cxxclass}.cs
          --export-layer-file ${ActiVizDotNet_BINARY_DIR}/export-layer/${cxxclass}EL.cxx
        COMMAND ${CMAKE_COMMAND} -E touch
          ${ActiVizDotNet_BINARY_DIR}/csharp/${cxxclass}-sentinel.txt
        DEPENDS
          ${ActiVizDotNet_BINARY_DIR}/xml/${cxxclass}.xml
          ${ActiVizDotNet_BINARY_DIR}/xml/MummySettings.xml
          ${mummy_EXECUTABLE}
          ${${cxxclass}_EXTRA_DEPENDENCIES}
      )

      set(${module}_EL_SOURCES ${${module}_EL_SOURCES}
        "${ActiVizDotNet_BINARY_DIR}/export-layer/${cxxclass}EL.cxx")
      set(ALLMODULES_EL_SOURCES ${ALLMODULES_EL_SOURCES}
        "${ActiVizDotNet_BINARY_DIR}/export-layer/${cxxclass}EL.cxx")

      set(${module}_CS_SOURCES ${${module}_CS_SOURCES}
        "${ActiVizDotNet_BINARY_DIR}/csharp/${cxxclass}.cs")
      set(ALLMODULES_CS_SOURCES ${ALLMODULES_CS_SOURCES}
        "${ActiVizDotNet_BINARY_DIR}/csharp/${cxxclass}.cs")

      set(${module}_SENTINELS ${${module}_SENTINELS}
        "${ActiVizDotNet_BINARY_DIR}/csharp/${cxxclass}-sentinel.txt")
      set(ALLMODULES_SENTINELS ${ALLMODULES_SENTINELS}
        "${ActiVizDotNet_BINARY_DIR}/csharp/${cxxclass}-sentinel.txt")
    endif()
    endforeach()

    if(NOT "${${module}_SENTINELS}" STREQUAL "")
      add_custom_target(
        "${module}GenerateCSharp" ALL
        DEPENDS ${${module}_SENTINELS}
      )

      if("${module}" STREQUAL "vtkCommonCore")
        add_dependencies(${module}GenerateCSharp AssembleExtraSources)
      endif()
    endif()
#  else()
#    message(STATUS "info: no '${module}' classes to wrap for CSharp")
  endif()
endforeach()


set_source_files_properties(
  ${ALLMODULES_EL_SOURCES}
#  ${ALLMODULES_CS_SOURCES}
  PROPERTIES GENERATED 1)


# Set list of export-layer dlls in C# syntax as CMake variable
# MV_EXPORTLAYER_DLL_VARIABLES. This gets configured into
# WrappedObject.cs below.
#
set(MV_EXPORTLAYER_DLL_VARIABLES "")
foreach(module ${VTK_MODULES_ENABLED})
  if(NOT ${module}_EXCLUDE_FROM_WRAPPING)
  set(MV_EXPORTLAYER_DLL_VARIABLES "${MV_EXPORTLAYER_DLL_VARIABLES}    /// <summary>\n")
  set(MV_EXPORTLAYER_DLL_VARIABLES "${MV_EXPORTLAYER_DLL_VARIABLES}    /// Export layer functions for '${module}' are exported from\n")
  set(MV_EXPORTLAYER_DLL_VARIABLES "${MV_EXPORTLAYER_DLL_VARIABLES}    /// the DLL named by the value of this variable.\n")
  set(MV_EXPORTLAYER_DLL_VARIABLES "${MV_EXPORTLAYER_DLL_VARIABLES}    /// </summary>\n")

  if(MV_ONE_EXPORT_LAYER_DLL)
    set(MV_EXPORTLAYER_DLL_VARIABLES "${MV_EXPORTLAYER_DLL_VARIABLES}    public const string ${module}EL_dll = \"${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.VTK.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}\";\n")
  else()
    set(MV_EXPORTLAYER_DLL_VARIABLES "${MV_EXPORTLAYER_DLL_VARIABLES}    public const string ${module}EL_dll = \"${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.VTK.${module}.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}\";\n")
  endif()
  endif()
endforeach()


# Configure list of C# source files:
#
set(csharp_namespace "Kitware.VTK")
configure_file(
  "${ActiVizDotNet_SOURCE_DIR}/WrappedObject.cs.in"
  "${ActiVizDotNet_BINARY_DIR}/csharp/WrappedObject.cs"
  @ONLY
)

configure_file(
  "${ActiVizDotNet_SOURCE_DIR}/AssemblyInfo.cs.in"
  "${ActiVizDotNet_BINARY_DIR}/csharp/AssemblyInfo.cs"
  @ONLY
)

set(ALLMODULES_CS_SOURCES
  ${ALLMODULES_CS_SOURCES}
  "${ActiVizDotNet_SOURCE_DIR}/RenderWindowControl/RenderWindowControl.cs"
  "${ActiVizDotNet_SOURCE_DIR}/RenderWindowControl/RenderWindowControl.Designer.cs"
  "${ActiVizDotNet_BINARY_DIR}/csharp/WrappedObject.cs"
  "${ActiVizDotNet_BINARY_DIR}/csharp/AssemblyInfo.cs"
)


macro(ADD_CUSTOM_COMMAND_COPYFILE srcFile dstFile)
  add_custom_command(
    OUTPUT "${dstFile}"
    DEPENDS "${srcFile}"
    COMMAND ${CMAKE_COMMAND}
    ARGS -E copy "${srcFile}" "${dstFile}"
  )
endmacro()


macro(ADD_CUSTOM_COMMAND_SYMLINK srcFile linkFile)
  get_filename_component(srcFilePath "${srcFile}" PATH)
  get_filename_component(linkFilePath "${linkFile}" PATH)

  if("${srcFilePath}" STREQUAL "${linkFilePath}")
    get_filename_component(srcFileName "${srcFile}" NAME)
    set(srcFileName "./${srcFileName}")
  else()
    set(srcFileName "${srcFile}")
  endif()

  add_custom_command(
    OUTPUT "${linkFile}"
    DEPENDS "${srcFile}"
    COMMAND ${CMAKE_COMMAND}
    ARGS -E create_symlink "${srcFileName}" "${linkFile}"
  )
endmacro()


set(vtkListOfDLLs "")

set(AVDN_DO_COPY_LIBRARIES_STEPS 1)
if(AVDN_DO_COPY_LIBRARIES_STEPS)
  #
  # Add custom commands/target to copy the mummy Runtime libs to
  # LIBRARY_OUTPUT_PATH and dlls to EXECUTABLE_OUTPUT_PATH:
  #
  set(lib_dir "${VTK_BINARY_DIR}/${CMAKE_CFG_INTDIR}")
  if(LIBRARY_OUTPUT_PATH)
    set(lib_dir "${LIBRARY_OUTPUT_PATH}/${CMAKE_CFG_INTDIR}")
  endif()

  set(exe_dir "${VTK_BINARY_DIR}/${CMAKE_CFG_INTDIR}")
  if(EXECUTABLE_OUTPUT_PATH)
    set(exe_dir "${EXECUTABLE_OUTPUT_PATH}/${CMAKE_CFG_INTDIR}")
  endif()

  # x86 binaries (exe, dll and lib) are in ${Mummy_BIN_DIR}:
  #
  set(bin_src_dir "${Mummy_BIN_DIR}")
  set(lib_src_dir "${Mummy_BIN_DIR}")

  # x64 binaries (exe, dll and lib) are in ${Mummy_BIN_x64_DIR}:
  #
  if(CMAKE_SIZEOF_VOID_P EQUAL 8)
    set(bin_src_dir "${Mummy_BIN_x64_DIR}")
    set(lib_src_dir "${Mummy_BIN_x64_DIR}")
  endif()

  if(WIN32)
    # .lib file is Windows only...
    ADD_CUSTOM_COMMAND_COPYFILE(
      "${lib_src_dir}/Kitware.mummy.Runtime.Unmanaged.lib"
      "${lib_dir}/Kitware.mummy.Runtime.Unmanaged.lib"
    )
  endif()

  ADD_CUSTOM_COMMAND_COPYFILE(
    "${bin_src_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.mummy.Runtime.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}"
    "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.mummy.Runtime.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}"
  )

  ADD_CUSTOM_COMMAND_COPYFILE(
    "${bin_src_dir}/Kitware.mummy.Runtime.dll"
    "${exe_dir}/Kitware.mummy.Runtime.dll"
  )

  set(MCL_deps
    "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.mummy.Runtime.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}"
    "${exe_dir}/Kitware.mummy.Runtime.dll"
  )

  if(WIN32)
    # .lib file is Windows only...
    set(MCL_deps ${MCL_deps}
      "${lib_dir}/Kitware.mummy.Runtime.Unmanaged.lib"
    )
  endif()

  # Rules to copy pdb files only get added if the source pdb files actually
  # exist at CMake configure time...
  #
  if(EXISTS "${bin_src_dir}/kitware.mummy.runtime.unmanaged.pdb")
    ADD_CUSTOM_COMMAND_COPYFILE(
      "${bin_src_dir}/kitware.mummy.runtime.unmanaged.pdb"
      "${exe_dir}/kitware.mummy.runtime.unmanaged.pdb"
    )
    set(MCL_deps ${MCL_deps}
      "${exe_dir}/kitware.mummy.runtime.unmanaged.pdb"
    )
  endif()

  if(EXISTS "${bin_src_dir}/Kitware.mummy.Runtime.pdb")
    ADD_CUSTOM_COMMAND_COPYFILE(
      "${bin_src_dir}/Kitware.mummy.Runtime.pdb"
      "${exe_dir}/Kitware.mummy.Runtime.pdb"
    )
    set(MCL_deps ${MCL_deps}
      "${exe_dir}/Kitware.mummy.Runtime.pdb"
    )
  endif()

  add_custom_target(
    Kitware.mummy.CopyLibraries ALL
    DEPENDS ${MCL_deps}
  )

  IF(VTK_BUILD_SHARED_LIBS)
  IF(EXISTS "${VTK_DIR}/bin")
    #
    # Add custom commands/target to copy the VTK Runtime dlls to
    # EXECUTABLE_OUTPUT_PATH:
    #
    SET(VCL_deps "")
    SET(vtk_dll_dir "${VTK_DIR}/bin/${CMAKE_CFG_INTDIR}")
	SET(vtk_lib_dir "${VTK_DIR}/lib/${CMAKE_CFG_INTDIR}")

    # Since this whole chunk copies files from ${vtk_dll_dir} to
    # ${exe_dir}, only add the commands to do it if those directories
    # are different:
    #
    IF(NOT "${vtk_dll_dir}" STREQUAL "${exe_dir}")
      # Use the list of modules as the basis for which DLLs to copy:
      #
	  foreach(module ${VTK_MODULES_ENABLED})
	    if(NOT ${module}_EXCLUDE_FROM_WRAPPING)
		  set(vtkListOfDLLs ${vtkListOfDLLs} ${module})
		endif()  
	  endforeach()

      # Add extra utility DLLs the kit DLLs depend on:
      #
      SET(vtkListOfDLLs ${vtkListOfDLLs}
        #Cosmo
        #LSDyna
        #VPIC
		vtkoggtheora
		vtkgl2ps
        vtkalglib
        vtkDICOMParser
        vtkexoIIc
        #vtkftgl
        #vtkhdf5
        #vtkhdf5_hl
        vtkmetaio
        vtkNetCDF
        vtkNetCDF_cxx
        vtksys
        vtkverdict
		vtkjsoncpp
		vtkglew
      )

      IF(NOT AVDN_VTK_USE_SYSTEM_EXPAT)
        SET(vtkListOfDLLs ${vtkListOfDLLs}
          vtkexpat
        )
      ENDIF(NOT AVDN_VTK_USE_SYSTEM_EXPAT)

      IF(NOT AVDN_VTK_USE_SYSTEM_FREETYPE)
        SET(vtkListOfDLLs ${vtkListOfDLLs}
          vtkfreetype
        )
      ENDIF(NOT AVDN_VTK_USE_SYSTEM_FREETYPE)

      IF(NOT AVDN_VTK_USE_SYSTEM_JPEG)
        SET(vtkListOfDLLs ${vtkListOfDLLs}
          vtkjpeg
        )
      ENDIF(NOT AVDN_VTK_USE_SYSTEM_JPEG)

      IF(NOT AVDN_VTK_USE_SYSTEM_LIBXML2)
        SET(vtkListOfDLLs ${vtkListOfDLLs}
          vtklibxml2
        )
      ENDIF(NOT AVDN_VTK_USE_SYSTEM_LIBXML2)

      IF(NOT AVDN_VTK_USE_SYSTEM_PNG)
        SET(vtkListOfDLLs ${vtkListOfDLLs}
          vtkpng
        )
      ENDIF(NOT AVDN_VTK_USE_SYSTEM_PNG)

      IF(NOT AVDN_VTK_USE_SYSTEM_LIBPROJ4)
        SET(vtkListOfDLLs ${vtkListOfDLLs}
          vtkproj4
        )
      ENDIF(NOT AVDN_VTK_USE_SYSTEM_LIBPROJ4)

      IF(NOT AVDN_VTK_USE_SYSTEM_TIFF)
        SET(vtkListOfDLLs ${vtkListOfDLLs}
          vtktiff
        )
      ENDIF(NOT AVDN_VTK_USE_SYSTEM_TIFF)

      IF(NOT AVDN_VTK_USE_SYSTEM_ZLIB)
        SET(vtkListOfDLLs ${vtkListOfDLLs}
          vtkzlib
        )
      ENDIF(NOT AVDN_VTK_USE_SYSTEM_ZLIB)

      # Only on non-Win32 systems, vtksqlite *is* built as a shared lib:
      #
      IF(NOT WIN32)
        SET(vtkListOfDLLs ${vtkListOfDLLs}
          vtksqlite
        )
      ENDIF(NOT WIN32)

      # On Linux/Mac, VTK's *real* shared libs are named with ".so.5.8.0" (for example)
      # at the end of the file name. The ".so" and ".so.5.8" files are symlinks to the
      # real shared lib...
      #
      SET(vtk_sl_suffix "")
      SET(vtk_sl_base_suffix "")
	  SET(vtk_dl_suffix "")
      if(WIN32)
	    set(vtk_dl_suffix "-${VTK_MAJOR_VERSION}.${VTK_MINOR_VERSION}")
	    SET(vtk_major_minor ".${VTK_MAJOR_VERSION}.${VTK_MINOR_VERSION}")
	  else()
        SET(vtk_sl_base_suffix ".${VTK_MAJOR_VERSION}.${VTK_MINOR_VERSION}.${VTK_BUILD_VERSION}")
        SET(vtk_major_minor ".${VTK_MAJOR_VERSION}.${VTK_MINOR_VERSION}")
      ENDIF()

      # Loop over all the VTK dlls and add a custom command to copy each one to the
      # build tree's "exe_dir". Also add each one to the list of files to install.
      #
      FOREACH(vtkDLL ${vtkListOfDLLs})
        IF(NOT vtk_sl_base_suffix STREQUAL "")
          SET(vtk_sl_suffix "${vtk_sl_base_suffix}")
        ENDIF(NOT vtk_sl_base_suffix STREQUAL "")

        ADD_CUSTOM_COMMAND_COPYFILE(
          "${vtk_dll_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${vtk_dl_suffix}${CMAKE_SHARED_LIBRARY_SUFFIX}${vtk_sl_suffix}"
          "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${vtk_dl_suffix}${CMAKE_SHARED_LIBRARY_SUFFIX}${vtk_sl_suffix}"
        )

        SET(AVDN_INSTALL_FILES ${AVDN_INSTALL_FILES}
          "${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${vtk_dl_suffix}${CMAKE_SHARED_LIBRARY_SUFFIX}${vtk_sl_suffix}"
        )

        SET(VCL_deps ${VCL_deps}
          "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${vtk_dl_suffix}${CMAKE_SHARED_LIBRARY_SUFFIX}${vtk_sl_suffix}"
        )

        IF(NOT vtk_sl_base_suffix STREQUAL "")
          ADD_CUSTOM_COMMAND_SYMLINK(
            "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${vtk_dl_suffix}${CMAKE_SHARED_LIBRARY_SUFFIX}${vtk_sl_suffix}"
            "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${vtk_dl_suffix}${CMAKE_SHARED_LIBRARY_SUFFIX}${vtk_major_minor}"
          )
          ADD_CUSTOM_COMMAND_SYMLINK(
            "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${vtk_dl_suffix}${CMAKE_SHARED_LIBRARY_SUFFIX}${vtk_major_minor}"
            "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${vtk_dl_suffix}${CMAKE_SHARED_LIBRARY_SUFFIX}"
          )

          SET(AVDN_INSTALL_FILES ${AVDN_INSTALL_FILES}
            "${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${vtk_dl_suffix}${CMAKE_SHARED_LIBRARY_SUFFIX}${vtk_major_minor}"
            "${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${vtk_dl_suffix}${CMAKE_SHARED_LIBRARY_SUFFIX}"
          )

          SET(VCL_deps ${VCL_deps}
            "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${vtk_dl_suffix}${CMAKE_SHARED_LIBRARY_SUFFIX}${vtk_major_minor}"
            "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${vtk_dl_suffix}${CMAKE_SHARED_LIBRARY_SUFFIX}"
          )
        ENDIF(NOT vtk_sl_base_suffix STREQUAL "")
      ENDFOREACH(vtkDLL)

      ADD_CUSTOM_TARGET(
        Kitware.VTK.CopyDLLs ALL
        DEPENDS ${VCL_deps}
      )
      SET(AVDN_HAS_TARGET_KITWARE_VTK_COPYDLLS 1)
    ENDIF(NOT "${vtk_dll_dir}" STREQUAL "${exe_dir}")
  ENDIF(EXISTS "${VTK_DIR}/bin")
  ENDIF()

  # Also copy over the MSVC runtime dlls (so we can use them by
  # file name only as /linkresource args to the C# compiler)
  #
  if(CMAKE_INSTALL_SYSTEM_RUNTIME_LIBS)
    set(lib_deps "")
    foreach(lib ${CMAKE_INSTALL_SYSTEM_RUNTIME_LIBS})
      get_filename_component(f "${lib}" NAME)
      ADD_CUSTOM_COMMAND_COPYFILE(
        "${lib}"
        "${exe_dir}/${f}"
      )
      set(AVDN_INSTALL_FILES ${AVDN_INSTALL_FILES} "${f}")
      set(lib_deps ${lib_deps} "${exe_dir}/${f}")
    endforeach()
    add_custom_target(
      Kitware.VTK.CopyMsvcDLLs ALL
      DEPENDS ${lib_deps}
    )
    set(AVDN_HAS_TARGET_KITWARE_VTK_COPYMSVCDLLS 1)
  endif()
endif()


# The big one or the many little ones:
#
set(unmanaged_targets "")
set(unmanaged_dlls "")
if(MV_ONE_EXPORT_LAYER_DLL)
  set(AVDN_INSTALL_TARGETS ${AVDN_INSTALL_TARGETS} "Kitware.VTK.Unmanaged")
  add_library(Kitware.VTK.Unmanaged SHARED ${ALLMODULES_EL_SOURCES})
  set(unmanaged_targets ${unmanaged_targets} "Kitware.VTK.Unmanaged")
  set(unmanaged_dlls ${unmanaged_dlls} "${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.VTK.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}")
  if(WIN32)
    target_link_libraries(Kitware.VTK.Unmanaged "${exe_dir}/Kitware.mummy.Runtime.Unmanaged.lib")
  else()
    target_link_libraries(Kitware.VTK.Unmanaged "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.mummy.Runtime.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}")
  endif()
  foreach(module ${VTK_MODULES_ENABLED})
    if(NOT ${module}_EXCLUDE_FROM_WRAPPING)
      set_property(TARGET Kitware.VTK.Unmanaged APPEND
        PROPERTY INCLUDE_DIRECTORIES
        ${${module}_DEPENDS_INCLUDE_DIRS}
        ${${module}_INCLUDE_DIRS}
      )
      if(${module}_IMPLEMENTS)
        set_property(TARGET Kitware.VTK.Unmanaged APPEND PROPERTY
          COMPILE_DEFINITIONS "${module}_AUTOINIT=1(${module})")
      endif()
    endif()
  endforeach()
  add_dependencies(Kitware.VTK.Unmanaged Kitware.mummy.CopyLibraries)
  foreach(module ${VTK_MODULES_ENABLED})
    if(NOT ${module}_EXCLUDE_FROM_WRAPPING)
      target_link_libraries(Kitware.VTK.Unmanaged ${module})
      foreach(dep ${${module}_LINK_DEPENDS})
        if(NOT "${dep}" STREQUAL "${module}")
          if(NOT ${dep}_EXCLUDE_FROM_WRAPPING)
            target_link_libraries(Kitware.VTK.Unmanaged ${dep})
          endif()
        endif()
      endforeach()
      add_dependencies(Kitware.VTK.Unmanaged "${module}GenerateCSharp")
    endif()
  endforeach()
else()
  foreach(module ${VTK_MODULES_ENABLED})
    if(NOT ${module}_EXCLUDE_FROM_WRAPPING)
      set(AVDN_INSTALL_TARGETS ${AVDN_INSTALL_TARGETS} "Kitware.VTK.${module}.Unmanaged")
      add_library(Kitware.VTK.${module}.Unmanaged SHARED ${${module}_EL_SOURCES})
      set(unmanaged_targets ${unmanaged_targets} "Kitware.VTK.${module}.Unmanaged")
      set(unmanaged_dlls ${unmanaged_dlls} "${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.VTK.${module}.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}")
      if(WIN32)
        target_link_libraries(Kitware.VTK.${module}.Unmanaged "${exe_dir}/Kitware.mummy.Runtime.Unmanaged.lib")
      else()
        target_link_libraries(Kitware.VTK.${module}.Unmanaged "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.mummy.Runtime.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}")
      endif()
      target_link_libraries(Kitware.VTK.${module}.Unmanaged ${module})
	  
      # This is odd and should be changed (by JJ) 
	  # (edited by Mark D)
	  SET(vtk_lib_dir "${VTK_DIR}/lib/${CMAKE_CFG_INTDIR}")
      target_link_libraries(Kitware.VTK.${module}.Unmanaged ${vtk_lib_dir}/vtkRenderingContextOpenGL2-7.1.lib ${vtk_lib_dir}/vtkRenderingOpenGL2-7.1.lib ${vtk_lib_dir}/vtkRenderingFreeType-7.1.lib ${vtk_lib_dir}/vtkRenderingVolumeOpenGL2-7.1.lib ${vtk_lib_dir}/vtkInteractionStyle-7.1.lib ${vtk_lib_dir}/vtkRenderingGL2PSOpenGL2-7.1.lib)
	  #vtkRenderingFreeTypeOpenGL vtkRenderingFreeType vtkInteractionStyle vtkRenderingVolumeOpenGL)
	  
      foreach(dep ${${module}_LINK_DEPENDS})
        if(NOT "${dep}" STREQUAL "${module}")
          #if(NOT ${dep}_EXCLUDE_FROM_WRAPPING)
            target_link_libraries(Kitware.VTK.${module}.Unmanaged ${dep})
          #endif()
        endif()
      endforeach()
	  
      set_property(TARGET Kitware.VTK.${module}.Unmanaged APPEND
        PROPERTY INCLUDE_DIRECTORIES
        ${${module}_DEPENDS_INCLUDE_DIRS}
        ${${module}_INCLUDE_DIRS}
      )
      if(${module}_IMPLEMENTS)
        set_property(TARGET Kitware.VTK.${module}.Unmanaged APPEND PROPERTY
          COMPILE_DEFINITIONS "${module}_AUTOINIT=1(${module})")
      endif()
      add_dependencies(Kitware.VTK.${module}.Unmanaged Kitware.mummy.CopyLibraries)
      add_dependencies(Kitware.VTK.${module}.Unmanaged "${module}GenerateCSharp")
    endif()
  endforeach()
endif()


foreach(vtkDLL ${vtkListOfDLLs})
  set(unmanaged_dlls ${unmanaged_dlls} "${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${vtk_dl_suffix}${CMAKE_SHARED_LIBRARY_SUFFIX}")
endforeach()

foreach(lib ${CMAKE_INSTALL_SYSTEM_RUNTIME_LIBS})
  get_filename_component(f "${lib}" NAME)
  set(unmanaged_dlls ${unmanaged_dlls} "${f}")
endforeach()

if(AVDN_HAS_TARGET_KITWARE_VTK_COPYMSVCDLLS)
  set(unmanaged_targets ${unmanaged_targets} "Kitware.VTK.CopyMsvcDLLs")
endif()


set(AVDN_VTK_CSC_REFS "Kitware.mummy.Runtime")

if(NOT WIN32)
  #
  # The mono C# compiler does not appear to add these common refs by default
  # like the MS C# compiler does... add them explicitly:
  #
  set(AVDN_VTK_CSC_REFS ${AVDN_VTK_CSC_REFS}
    "System.Drawing"
    "System.Data"
    "System.Windows.Forms"
    )
endif()

set(MUMMY_ADD_CSHARP_BINDIR "${ActiVizDotNet_BINARY_DIR}")
set(MUMMY_ADD_CSHARP_SRCDIR "${ActiVizDotNet_SOURCE_DIR}")

ADD_CSHARP_LIBRARY(
  "Kitware.VTK" # name of library
  "${unmanaged_targets}" # list of CMake targets that need to build first
  "${AVDN_VTK_CSC_REFS}" # list of csc "/reference:" args
#  "${unmanaged_dlls}" # list of csc "/linkresource:" args
  "" # Stop automatic bundling of deps
  "${AVDN_SNKEYFILE}" # strong name signing keyfile
  # Source files:
  ${ALLMODULES_CS_SOURCES}
)


foreach(module ${VTK_MODULES_ENABLED})
  if(NOT ${module}_EXCLUDE_FROM_WRAPPING)
    add_dependencies(Kitware.VTK "${module}GenerateCSharp")
  endif()
endforeach()

if(AVDN_EXTRA_DEPENDENCIES)
  add_dependencies(Kitware.VTK ${AVDN_EXTRA_DEPENDENCIES})
endif()


set(AVDN_BUILD_TESTING ${BUILD_TESTING})
set(AVDN_BUILD_EXAMPLES ${BUILD_EXAMPLES})


macro(CONFIGURE_EXAMPLE ce_name)
  # Copy the whole example directory given to its parallel in the build tree:
  #
  execute_process(COMMAND ${CMAKE_COMMAND} -E copy_directory
    "${ActiVizDotNet_SOURCE_DIR}/Examples/${ce_name}"
    "${ActiVizDotNet_BINARY_DIR}/Examples/${ce_name}"
    )

  if(CMAKE_SIZEOF_VOID_P EQUAL 8)
    set(AVDN_PLATFORM "x64")
  else()
    set(AVDN_PLATFORM "x86")
  endif()

  set(AVDN_MUMMY_QUALIFIED_REFERENCE_NAME
    "Kitware.mummy.Runtime, Version=${Mummy_VERSION_MAJOR}.${Mummy_VERSION_MINOR}.${Mummy_VERSION_PATCH}.${Mummy_VERSION_SVN_REVISION}, Culture=neutral, PublicKeyToken=995c7fb9db2c1b44, processorArchitecture=${AVDN_PLATFORM}")

  set(AVDN_VTK_QUALIFIED_REFERENCE_NAME
    "Kitware.VTK, Version=${AVDN_VERSION_STRING}.${AVDN_VERSION_SVN_REVISION}, Culture=neutral, PublicKeyToken=995c7fb9db2c1b44, processorArchitecture=${AVDN_PLATFORM}")

  configure_file(${ActiVizDotNet_SOURCE_DIR}/Examples/${ce_name}/CS/${ce_name}.sln.in
                 ${ActiVizDotNet_BINARY_DIR}/Examples/${ce_name}/CS/${ce_name}.sln @ONLY)

  configure_file(${ActiVizDotNet_SOURCE_DIR}/Examples/${ce_name}/CS/${ce_name}.csproj.in
                 ${ActiVizDotNet_BINARY_DIR}/Examples/${ce_name}/CS/${ce_name}.csproj @ONLY)

  if(EXISTS "${ActiVizDotNet_SOURCE_DIR}/Examples/${ce_name}/VB/${ce_name}.sln.in" AND
    EXISTS "${ActiVizDotNet_SOURCE_DIR}/Examples/${ce_name}/VB/${ce_name}.vbproj.in")
    configure_file(${ActiVizDotNet_SOURCE_DIR}/Examples/${ce_name}/VB/${ce_name}.sln.in
                   ${ActiVizDotNet_BINARY_DIR}/Examples/${ce_name}/VB/${ce_name}.sln @ONLY)

    configure_file(${ActiVizDotNet_SOURCE_DIR}/Examples/${ce_name}/VB/${ce_name}.vbproj.in
                   ${ActiVizDotNet_BINARY_DIR}/Examples/${ce_name}/VB/${ce_name}.vbproj @ONLY)
  else()
    message(STATUS "info: Example '${ce_name}' has no VB implementation")
  endif()
endmacro()

CONFIGURE_EXAMPLE("BoxWidget")
CONFIGURE_EXAMPLE("CubeAxes")
CONFIGURE_EXAMPLE("Decimate")
CONFIGURE_EXAMPLE("DelMesh")
CONFIGURE_EXAMPLE("Dialog")
CONFIGURE_EXAMPLE("EventMonitor")
CONFIGURE_EXAMPLE("FileTree")
CONFIGURE_EXAMPLE("HelloVTKConsole")
CONFIGURE_EXAMPLE("HelloVTKForm")
CONFIGURE_EXAMPLE("SpherePuzzle")
CONFIGURE_EXAMPLE("Streamline")
CONFIGURE_EXAMPLE("VolumeRendering")
CONFIGURE_EXAMPLE("Wikipedia")


if(AVDN_BUILD_EXAMPLES)
  include("${ActiVizDotNet_SOURCE_DIR}/BuildExamples.cmake")
endif()


if(AVDN_BUILD_TESTING)
  add_subdirectory(Testing)
endif()


# "make install" rules
#
set(AVDN_ADD_INSTALL_RULES 1)

if(AVDN_ADD_INSTALL_RULES)
  if(AVDN_INSTALL_TARGETS)
    install(TARGETS ${AVDN_INSTALL_TARGETS}
      RUNTIME DESTINATION "bin"
      LIBRARY DESTINATION "bin"
    )
  endif()

  # These dll files only get built on Windows, and they are only
  # in the \${BUILD_TYPE} subdirectory for build systems that use
  # CMAKE_CONFIGURATION_TYPES:
  #
  if(CMAKE_CONFIGURATION_TYPES)
    set(exe_dir "${VTK_BINARY_DIR}/\${BUILD_TYPE}")
    if(EXECUTABLE_OUTPUT_PATH)
      set(exe_dir "${EXECUTABLE_OUTPUT_PATH}/\${BUILD_TYPE}")
    endif()
  else()
    set(exe_dir "${VTK_BINARY_DIR}")
    if(EXECUTABLE_OUTPUT_PATH)
      set(exe_dir "${EXECUTABLE_OUTPUT_PATH}")
    endif()
  endif()

  set(AVDN_INSTALL_FILES ${AVDN_INSTALL_FILES}
    "Kitware.mummy.Runtime.dll"
    "${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.mummy.Runtime.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}"
    "Kitware.VTK.dll"
    "Kitware.VTK.xml"
  )

  if(AVDN_BUILD_EXAMPLES AND AVDN_BUILD_SLN_FILES)
    set(AVDN_INSTALL_FILES ${AVDN_INSTALL_FILES}
      "EventMonitor.exe"
      "FileTree.exe"
    )
  endif()

  set(fullfiles "" )
  foreach(ff ${AVDN_INSTALL_FILES})
    set(fullfiles ${fullfiles} "${exe_dir}/${ff}")
  endforeach()

  install(FILES ${fullfiles}
    DESTINATION "bin"
  )

  install(FILES
    #"${ActiVizDotNet_SOURCE_DIR}/Documentation/UsersGuide.pdf"
    "${ActiVizDotNet_SOURCE_DIR}/License${AVDN_EDITION_SUFFIX}.txt"
    DESTINATION "."
  )

  # Also install the Examples directory (except for the
  # version control system bits of the tree and the *.in
  # files used to configure .sln/.csproj/.vbproj files...)
  #
  install(DIRECTORY "${ActiVizDotNet_BINARY_DIR}/Examples"
    DESTINATION "."
    REGEX "^(.*/\\.svn.*|.*\\.in)$" EXCLUDE
  )
endif()


# To build an installer, configure with -DBUILD_ACTIVIZDOTNET_INSTALLER:BOOL=ON
#
option(BUILD_ACTIVIZDOTNET_INSTALLER "Build the installer" OFF)
mark_as_advanced(BUILD_ACTIVIZDOTNET_INSTALLER)
if(BUILD_ACTIVIZDOTNET_INSTALLER)
  include("${ActiVizDotNet_SOURCE_DIR}/ActiVizDotNetCPack.cmake")
endif()


# Configure Doxygen
#
option(AVDN_GENERATE_DOXYGEN
  "Generate doxygen documentation"
  OFF
  )
mark_as_advanced(AVDN_GENERATE_DOXYGEN)
if(AVDN_GENERATE_DOXYGEN)

  if(NOT DOXYGEN_EXECUTABLE)
    find_package(Doxygen)
  endif()
  if(NOT DOXYGEN_EXECUTABLE)
    message(FATAL_ERROR "error: doxygen executable not found. doygen_EXECUTABLE = '${DOXYGEN_EXECUTABLE}'")
  endif()

  get_filename_component(DOXYGEN_PATH "${DOXYGEN_EXECUTABLE}" PATH)

  set(HAVE_DOT_YESNO NO)
  if(DOXYGEN_DOT_EXECUTABLE)
    set(HAVE_DOT_YESNO YES)
  endif()


  configure_file(
  "${ActiVizDotNet_SOURCE_DIR}/Doxyfile.in"
  "${ActiVizDotNet_BINARY_DIR}/Doxygen/Doxyfile"
  @ONLY
  )

  add_custom_target(AVDN_doxygen ALL
    ${DOXYGEN_EXECUTABLE} ${ActiVizDotNet_BINARY_DIR}/Doxygen/Doxyfile
    )
  add_dependencies(AVDN_doxygen Kitware.VTK)

endif()
