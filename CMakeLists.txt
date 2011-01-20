CMAKE_MINIMUM_REQUIRED(VERSION 2.8.3 FATAL_ERROR)
IF(COMMAND CMAKE_POLICY)
  CMAKE_POLICY(SET CMP0003 NEW)
ENDIF(COMMAND CMAKE_POLICY)
PROJECT(ActiVizDotNet)


# Determine if we're building ActiVizDotNet (AVDN) as a "top level" project.
#
SET(AVDN_BUILD_IS_TOP_LEVEL 0)
IF("${ActiVizDotNet_BINARY_DIR}" STREQUAL "${CMAKE_BINARY_DIR}")
  SET(AVDN_BUILD_IS_TOP_LEVEL 1)
ENDIF("${ActiVizDotNet_BINARY_DIR}" STREQUAL "${CMAKE_BINARY_DIR}")

IF(AVDN_BUILD_IS_TOP_LEVEL)
  IF(NOT LIBRARY_OUTPUT_PATH)
    SET(LIBRARY_OUTPUT_PATH ${ActiVizDotNet_BINARY_DIR}/bin CACHE INTERNAL "Single output directory for building all libraries.")
  ENDIF(NOT LIBRARY_OUTPUT_PATH)
  IF(NOT EXECUTABLE_OUTPUT_PATH)
    SET(EXECUTABLE_OUTPUT_PATH ${ActiVizDotNet_BINARY_DIR}/bin CACHE INTERNAL "Single output directory for building all executables.")
  ENDIF(NOT EXECUTABLE_OUTPUT_PATH)
ENDIF(AVDN_BUILD_IS_TOP_LEVEL)


# Make it clear that ActiViz is now open source: set the edition to
# "Open Source" - the Commercial Edition is no longer for sale, but the
# Open Source Edition is equivalent to it functionally. The Personal Edition
# restrictions are nowhere to be found... :-)
#
SET(AVDN_EDITION "Open Source")
SET(AVDN_EDITION_SUFFIX "-OS")


# List of MSVC dlls for our INSTALL rules below:
#
SET(CMAKE_INSTALL_SYSTEM_RUNTIME_LIBS_SKIP 1)
  # because we need the list and we'll add the INSTALL rule conditionally below...

#SET(CMAKE_INSTALL_DEBUG_LIBRARIES 1)
  # uncomment if you need to make a debug install/package...

INCLUDE(InstallRequiredSystemLibraries)


# Mummy:
#
FIND_PACKAGE(Mummy REQUIRED)
IF(NOT Mummy_BASE_DIR)
  MESSAGE(FATAL_ERROR "error: Mummy_BASE_DIR not defined. Please set Mummy_DIR to the directory containing MummyConfig.cmake")
ENDIF(NOT Mummy_BASE_DIR)


#MESSAGE(STATUS "<mummy>")
FOREACH(v
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
  #MESSAGE(STATUS "${v}='${${v}}'")
ENDFOREACH(v)
#MESSAGE(STATUS "</mummy>")


# Macros to make building C# libs and exes easy:
#
INCLUDE("${Mummy_DIR}/MummyCMakeMacros.cmake")


# Set CSC_DEBUG_FLAG prior to including FindCsharp.cmake if we want
# to force C# debugging info... If it's set, it's honored. If it's
# not defined, then FindCsharp.cmake uses its own logic to choose
# a reasonable default for CSC_DEBUG_FLAG.
#
IF(DEFINED BUILDNAME)
  # Likely built via a script, default debug setting is OFF:
  SET(AVDN_BUILD_CSHARP_DEBUG_DEFAULT OFF)
ELSE(DEFINED BUILDNAME)
  # Likely built by a real live developer, default debug setting is ON:
  SET(AVDN_BUILD_CSHARP_DEBUG_DEFAULT ON)
ENDIF(DEFINED BUILDNAME)

OPTION(AVDN_BUILD_CSHARP_DEBUG
  "Build C# components with '/debug' command line flag"
  ${AVDN_BUILD_CSHARP_DEBUG_DEFAULT}
)

IF(AVDN_BUILD_CSHARP_DEBUG)
  SET(CSC_DEBUG_FLAG "/debug")
ENDIF(AVDN_BUILD_CSHARP_DEBUG)


# Tools locations - either already found by parent project, specified on the
# command line, or available in the PATH... If not, you'll have to find
# these things yourself at configure time... :-)
#
INCLUDE("${Mummy_DIR}/FindCsharp.cmake")

IF(NOT csc_EXECUTABLE)
  FIND_PROGRAM(csc_EXECUTABLE csc)
ENDIF(NOT csc_EXECUTABLE)
IF(NOT csc_EXECUTABLE)
  MESSAGE(FATAL_ERROR "error: C# compiler not found. csc_EXECUTABLE='${csc_EXECUTABLE}'")
ENDIF(NOT csc_EXECUTABLE)

IF(NOT mummy_EXECUTABLE)
  FIND_PROGRAM(mummy_EXECUTABLE mummy)
ENDIF(NOT mummy_EXECUTABLE)
IF(NOT mummy_EXECUTABLE)
  MESSAGE(FATAL_ERROR "error: mummy not found. mummy_EXECUTABLE='${mummy_EXECUTABLE}'")
ENDIF(NOT mummy_EXECUTABLE)

IF(NOT gccxml_EXECUTABLE)
  FIND_PROGRAM(gccxml_EXECUTABLE gccxml)
ENDIF(NOT gccxml_EXECUTABLE)
IF(NOT gccxml_EXECUTABLE)
  MESSAGE(FATAL_ERROR "error: gccxml not found. gccxml_EXECUTABLE='${gccxml_EXECUTABLE}'")
ENDIF(NOT gccxml_EXECUTABLE)


INCLUDE_DIRECTORIES(${Mummy_INCLUDE_DIRS})


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

  IF(line MATCHES "^VTK_DATA_ROOT.*=(.*)E$")
    STRING(REGEX REPLACE "^VTK_DATA_ROOT.*=(.*)E$" "\\1" AVDN_VTK_DATA_ROOT "${line}")
  ENDIF(line MATCHES "^VTK_DATA_ROOT.*=(.*)E$")

  IF(line MATCHES "^VTK_USE_PARALLEL.*=(.*)E$")
    STRING(REGEX REPLACE "^VTK_USE_PARALLEL.*=(.*)E$" "\\1" AVDN_VTK_USE_PARALLEL "${line}")
  ENDIF(line MATCHES "^VTK_USE_PARALLEL.*=(.*)E$")

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


IF(${VTK_MAJOR_VERSION}.${VTK_MINOR_VERSION} LESS 5.6)
  SET(AVDN_EXTRA_EXCLUDE_CLASSES ${AVDN_EXTRA_EXCLUDE_CLASSES}
    vtkRenderPass
    )
ENDIF()

SET(AVDN_EXTRA_EXCLUDE_CLASSES ${AVDN_EXTRA_EXCLUDE_CLASSES} CACHE STRING
  "Force the wrap_exclude to 1 for these classes")

SET(AVDN_EXTRA_EXCLUDE_CLASSES_XML "")
FOREACH(class ${AVDN_EXTRA_EXCLUDE_CLASSES})
  SET(AVDN_EXTRA_EXCLUDE_CLASSES_XML
    "${AVDN_EXTRA_EXCLUDE_CLASSES_XML}  <Class name=\"${class}\" wrappable=\"false\"/>\n")
ENDFOREACH()
IF(NOT "${AVDN_EXTRA_EXCLUDE_CLASSES_XML}" STREQUAL "")
  SET(AVDN_EXTRA_EXCLUDE_CLASSES_XML "\n  <!-- AVDN_EXTRA_EXCLUDE_CLASSES_XML -->\n${AVDN_EXTRA_EXCLUDE_CLASSES_XML}")
ENDIF()


MESSAGE(STATUS "AVDN_EDITION='${AVDN_EDITION}'")

MESSAGE(STATUS "VTK_DIR='${VTK_DIR}'")
MESSAGE(STATUS "AVDN_VTK_SOURCE_DIR='${AVDN_VTK_SOURCE_DIR}'")

MESSAGE(STATUS "VTK_BUILD_SHARED_LIBS='${VTK_BUILD_SHARED_LIBS}'")
MESSAGE(STATUS "AVDN_VTK_DATA_ROOT='${AVDN_VTK_DATA_ROOT}'")
MESSAGE(STATUS "AVDN_VTK_USE_PARALLEL='${AVDN_VTK_USE_PARALLEL}'")
MESSAGE(STATUS "AVDN_EXTRA_EXCLUDE_CLASSES='${AVDN_EXTRA_EXCLUDE_CLASSES}'")

MESSAGE(STATUS "AVDN_VTK_USE_SYSTEM_EXPAT='${AVDN_VTK_USE_SYSTEM_EXPAT}'")
MESSAGE(STATUS "AVDN_VTK_USE_SYSTEM_FREETYPE='${AVDN_VTK_USE_SYSTEM_FREETYPE}'")
MESSAGE(STATUS "AVDN_VTK_USE_SYSTEM_JPEG='${AVDN_VTK_USE_SYSTEM_JPEG}'")
MESSAGE(STATUS "AVDN_VTK_USE_SYSTEM_LIBPROJ4='${AVDN_VTK_USE_SYSTEM_LIBPROJ4}'")
MESSAGE(STATUS "AVDN_VTK_USE_SYSTEM_LIBXML2='${AVDN_VTK_USE_SYSTEM_LIBXML2}'")
MESSAGE(STATUS "AVDN_VTK_USE_SYSTEM_PNG='${AVDN_VTK_USE_SYSTEM_PNG}'")
MESSAGE(STATUS "AVDN_VTK_USE_SYSTEM_TIFF='${AVDN_VTK_USE_SYSTEM_TIFF}'")
MESSAGE(STATUS "AVDN_VTK_USE_SYSTEM_ZLIB='${AVDN_VTK_USE_SYSTEM_ZLIB}'")

MESSAGE(STATUS "csc_EXECUTABLE='${csc_EXECUTABLE}'")
MESSAGE(STATUS "gccxml_EXECUTABLE='${gccxml_EXECUTABLE}'")
MESSAGE(STATUS "mono_EXECUTABLE='${mono_EXECUTABLE}'")
MESSAGE(STATUS "mummy_EXECUTABLE='${mummy_EXECUTABLE}'")


# Set of VTK kits to wrap (caller may set AVDN_KITS)...
# By default, wrap each known kit that has a "vtk${kit}Kit.cmake" file.
#
IF(NOT AVDN_KITS)
  FILE(GLOB ks "${VTK_KITS_DIR}/vtk*Kit.cmake")
  LIST(SORT ks)
  FOREACH(k ${ks})
    STRING(REGEX REPLACE "^.*/vtk(.*)Kit.cmake$" "\\1" kit "${k}")
    IF(EXISTS "${VTK_KITS_DIR}/vtk${kit}Kit.cmake")
      SET(AVDN_KITS ${AVDN_KITS} "${kit}")
    ENDIF(EXISTS "${VTK_KITS_DIR}/vtk${kit}Kit.cmake")
  ENDFOREACH(k)
ENDIF(NOT AVDN_KITS)

SET(kits ${AVDN_KITS})


# Organization:
#
# If MV_ONE_EXPORT_LAYER_DLL is 0 then all the generated export-layer/*.cxx
# files get built into multiple export layer DLLs called ${CMAKE_SHARED_LIBRARY_PREFIX}vtk${kit}EL${CMAKE_SHARED_LIBRARY_SUFFIX}.
# If VTK is built as dlls, we need to use multiple export layer DLLs.
#
# If MV_ONE_EXPORT_LAYER_DLL is 1 then all the generated export-layer/*.cxx
# files get built into one large export layer DLL called ${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.VTK.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}.
# If VTK is built as static libs, we need to use one large export layer DLL.
#
IF(VTK_BUILD_SHARED_LIBS)
  SET(MV_ONE_EXPORT_LAYER_DLL 0)
ELSE(VTK_BUILD_SHARED_LIBS)
  SET(MV_ONE_EXPORT_LAYER_DLL 1)
ENDIF(VTK_BUILD_SHARED_LIBS)


# Read svn info/status prior to configuring version information.
#
INCLUDE("${Mummy_DIR}/SvnMacros.cmake")

SET(AVDN_SOURCE_TREE_IS_SVN_CHECKOUT 0)
IF(EXISTS "${CMAKE_CURRENT_SOURCE_DIR}/.svn")
  SET(AVDN_SOURCE_TREE_IS_SVN_CHECKOUT 1)
ENDIF(EXISTS "${CMAKE_CURRENT_SOURCE_DIR}/.svn")

# Retrieve current state of the source tree according to svn,
# if this source tree is an svn checkout. Otherwise, status
# is just empty...
#
SET(AVDN_SVN_STATUS "")
IF(AVDN_SOURCE_TREE_IS_SVN_CHECKOUT)
  SVN_STATUS("${CMAKE_CURRENT_SOURCE_DIR}" AVDN_SVN_STATUS)
ENDIF(AVDN_SOURCE_TREE_IS_SVN_CHECKOUT)

IF("${AVDN_SVN_STATUS}" STREQUAL "")
  SET(AVDN_SVN_STATUS_BRIEF "")
  SET(AVDN_SVN_STATUS_ENCODED "")
ELSE("${AVDN_SVN_STATUS}" STREQUAL "")
  SET(AVDN_SVN_STATUS_BRIEF " [warning: experimental development build!]")
  STRING(REGEX REPLACE "\\\\" "/" AVDN_SVN_STATUS_ENCODED "${AVDN_SVN_STATUS}")
  STRING(REGEX REPLACE "\n" "\\\\n" AVDN_SVN_STATUS_ENCODED "${AVDN_SVN_STATUS_ENCODED}")
  STRING(REGEX REPLACE "\"" "\\\\\"" AVDN_SVN_STATUS_ENCODED "${AVDN_SVN_STATUS_ENCODED}")
ENDIF("${AVDN_SVN_STATUS}" STREQUAL "")

# Pull in FindSubversion.cmake to get the Subversion_WC_INFO macro:
#
SET(AVDN_WC_LAST_CHANGED_REV "1")
IF(AVDN_SOURCE_TREE_IS_SVN_CHECKOUT)
  SET(Subversion_SVN_EXECUTABLE "${svn_EXECUTABLE}")
  FIND_PACKAGE(Subversion)
  IF(Subversion_FOUND)
    Subversion_WC_INFO("${ActiVizDotNet_SOURCE_DIR}" AVDN)

    # AVDN_WC_LAST_CHANGED_REV value from this Subversion_WC_INFO call is
    # required to configure AVDN_VERSION_SVN_REVISION properly...

    # Now the following variables are available for use:
  #  MESSAGE("Subversion_VERSION_SVN='${Subversion_VERSION_SVN}'")
  #  MESSAGE("Subversion_LAST_CHANGED_LOG='${Subversion_LAST_CHANGED_LOG}'")
  #  MESSAGE("AVDN_WC_INFO='${AVDN_WC_INFO}'")
  #  MESSAGE("AVDN_WC_URL='${AVDN_WC_URL}'")
  #  MESSAGE("AVDN_WC_REVISION='${AVDN_WC_REVISION}'")
  #  MESSAGE("AVDN_WC_LAST_CHANGED_AUTHOR='${AVDN_WC_LAST_CHANGED_AUTHOR}'")
  #  MESSAGE("AVDN_WC_LAST_CHANGED_REV='${AVDN_WC_LAST_CHANGED_REV}'")
  #  MESSAGE("AVDN_WC_LAST_CHANGED_DATE='${AVDN_WC_LAST_CHANGED_DATE}'")
  ENDIF(Subversion_FOUND)
ENDIF(AVDN_SOURCE_TREE_IS_SVN_CHECKOUT)


# Version
#
CONFIGURE_FILE(
  "${CMAKE_CURRENT_SOURCE_DIR}/ActiVizDotNetVersion.cmake.in"
  "${CMAKE_CURRENT_BINARY_DIR}/ActiVizDotNetVersion.cmake"
  @ONLY
)
INCLUDE("${CMAKE_CURRENT_BINARY_DIR}/ActiVizDotNetVersion.cmake")

INCLUDE_DIRECTORIES(${CMAKE_CURRENT_SOURCE_DIR})


# Default installation location
#
IF(AVDN_BUILD_IS_TOP_LEVEL)
  SET(AVDN_INSTALL_PREFIX "${CMAKE_BINARY_DIR} Install" CACHE STRING "")
  SET(CMAKE_INSTALL_PREFIX "${AVDN_INSTALL_PREFIX}" CACHE INTERNAL "" FORCE)
ENDIF(AVDN_BUILD_IS_TOP_LEVEL)

SET(AVDN_INSTALL_FILES "")
SET(AVDN_INSTALL_TARGETS "")


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
ADD_CUSTOM_COMMAND(
  OUTPUT ${ActiVizDotNet_BINARY_DIR}/csharp/GenerateEventFragmentsSentinel.txt
  COMMAND ${CMAKE_COMMAND}
  ARGS
    "-DActiVizDotNet_BINARY_DIR:STRING=${ActiVizDotNet_BINARY_DIR}"
    "-DAVDN_VTK_SOURCE_DIR:STRING=${AVDN_VTK_SOURCE_DIR}"
    -P "${CMAKE_CURRENT_SOURCE_DIR}/GenerateEventFragments.cmake"
  DEPENDS
    ${CMAKE_CURRENT_SOURCE_DIR}/GenerateEventFragments.cmake
    ${AVDN_VTK_SOURCE_DIR}/Common/vtkCommand.h
    ${CMAKE_COMMAND}
)


# Use these settings when building with the Msvc compiler:
#
INCLUDE("${CMAKE_CURRENT_SOURCE_DIR}/MsvcMacros.cmake")
#MSVC_LINK_TO_STATIC_CRT()
MSVC80_FORCE_MANIFEST_LINKER_FLAG()
MSVC80_SUPPRESS_CRT_DEPRECATED_WARNINGS()


# Compute
#
SET(gccxml_include_args "")
FOREACH(dir ${Mummy_INCLUDE_DIRS} ${VTK_INCLUDE_DIRS})
  SET(gccxml_include_args ${gccxml_include_args} "-I${dir}")
ENDFOREACH(dir)

SET(exe_dir "${CMAKE_CURRENT_BINARY_DIR}/${CMAKE_CFG_INTDIR}")
IF(EXECUTABLE_OUTPUT_PATH)
  SET(exe_dir "${EXECUTABLE_OUTPUT_PATH}/${CMAKE_CFG_INTDIR}")
ENDIF(EXECUTABLE_OUTPUT_PATH)


# Run gccxml/mummy on each wrapped class:
#
SET(WRAPPED_CLASS_TABLE "")
SET(generated_files "")
SET(ALLKITS_EL_SOURCES "")
SET(ALLKITS_CS_SOURCES "")
SET(ALLKITS_SENTINELS "")

CONFIGURE_FILE(
  "${CMAKE_CURRENT_SOURCE_DIR}/MummySettings.xml.in"
  "${CMAKE_CURRENT_BINARY_DIR}/xml/MummySettings.xml"
  @ONLY
)

CONFIGURE_FILE(
  "${CMAKE_CURRENT_SOURCE_DIR}/vtkObjectEL_Extra.cxx.in"
  "${CMAKE_CURRENT_BINARY_DIR}/export-layer/vtkObjectEL_Extra.cxx"
  @ONLY
)

#CONFIGURE_FILE(
#  "${CMAKE_CURRENT_SOURCE_DIR}/vtkObject_Extra.cs.in"
#  "${CMAKE_CURRENT_BINARY_DIR}/csharp/vtkObject_Extra.cs"
#  @ONLY
#)

ADD_CUSTOM_COMMAND(
  OUTPUT ${ActiVizDotNet_BINARY_DIR}/csharp/AssembleExtraSourcesSentinel.txt
  COMMAND ${CMAKE_COMMAND}
  ARGS
    "-DActiVizDotNet_BINARY_DIR:STRING=${ActiVizDotNet_BINARY_DIR}"
    "-Daes_class:STRING=vtkObject"
    -P "${CMAKE_CURRENT_SOURCE_DIR}/AssembleExtraSources.cmake"
  DEPENDS
    ${CMAKE_CURRENT_SOURCE_DIR}/AssembleExtraSources.cmake
    ${CMAKE_CURRENT_SOURCE_DIR}/vtkObject_Extra.cs.in
    ${CMAKE_CURRENT_SOURCE_DIR}/vtkObject_EventFragments.cmake
    ${ActiVizDotNet_BINARY_DIR}/csharp/GenerateEventFragmentsSentinel.txt
    ${CMAKE_COMMAND}
)

ADD_CUSTOM_TARGET(
  AssembleExtraSources ALL
  DEPENDS ${ActiVizDotNet_BINARY_DIR}/csharp/AssembleExtraSourcesSentinel.txt
)


# Set variables for proper incremental rebuilds when hand-crafted
# C# or C++ export layer files are modified. Any files listed as
# extra code files in MummySettings.xml.in should be listed in
# some class's _EXTRA_DEPENDENCIES variable...
#
SET(vtkImageData_EXTRA_DEPENDENCIES
  "${CMAKE_CURRENT_SOURCE_DIR}/vtkImageDataEL_Extra.cxx"
  "${CMAKE_CURRENT_SOURCE_DIR}/vtkImageData_Extra.cs"
)

SET(vtkObject_EXTRA_DEPENDENCIES
  "${CMAKE_CURRENT_BINARY_DIR}/export-layer/vtkObjectEL_Extra.cxx"
  "${CMAKE_CURRENT_BINARY_DIR}/csharp/vtkObject_Extra.cs"
)

SET(vtkProgrammableAttributeDataFilter_EXTRA_DEPENDENCIES
  "${CMAKE_CURRENT_SOURCE_DIR}/vtkProgrammableAttributeDataFilterEL_Extra.cxx"
  "${CMAKE_CURRENT_SOURCE_DIR}/vtkProgrammableAttributeDataFilter_Extra.cs"
)

SET(vtkStringArray_EXTRA_DEPENDENCIES
  "${CMAKE_CURRENT_SOURCE_DIR}/vtkStringArrayEL_Extra.cxx"
  "${CMAKE_CURRENT_SOURCE_DIR}/vtkStringArray_Extra.cs"
)


MESSAGE(STATUS "Processing VTK_KITS_DIR='${VTK_KITS_DIR}'...")


SET(gccxml_compiler "${CMAKE_CXX_COMPILER}")
IF(MSVC80)
  SET(gccxml_compiler "msvc8")
ENDIF(MSVC80)
IF(MSVC90)
  SET(gccxml_compiler "msvc9")
ENDIF(MSVC90)
MESSAGE(STATUS "gccxml_compiler='${gccxml_compiler}'...")


FOREACH(kit ${kits})
  IF(EXISTS "${VTK_KITS_DIR}/vtk${kit}Kit.cmake")
    MESSAGE(STATUS "Including 'vtk${kit}Kit.cmake'...")
    INCLUDE("${VTK_KITS_DIR}/vtk${kit}Kit.cmake")

    # Try this at home. Do NOT exclude vtkCommand from this wrapping. It will be cool.
    #
    SET(VTK_CLASS_WRAP_EXCLUDE_vtkCommand 0)

    FOREACH(class ${AVDN_EXTRA_EXCLUDE_CLASSES})
      SET(VTK_CLASS_WRAP_EXCLUDE_${class} 1)
    ENDFOREACH()

    STRING(TOUPPER "${kit}" ukit)
    IF("${kit}" STREQUAL "GenericFiltering")
      SET(ukit "GENERIC_FILTERING")
    ENDIF("${kit}" STREQUAL "GenericFiltering")

    SET(${kit}_EL_SOURCES "")
    SET(${kit}_CS_SOURCES "")
    SET(${kit}_SENTINELS "")

  #  SET(VTK_COMMON_CLASSES vtkObjectBase vtkObject vtkVersion)
    FOREACH(class ${VTK_${ukit}_CLASSES})
      IF(NOT VTK_CLASS_WRAP_EXCLUDE_${class})
        SET(header "${VTK_${ukit}_HEADER_DIR}/${class}.h")
        SET(cxxclass "${class}")

        # handle full paths
        IF("${class}" MATCHES "^(\\/|.\\/|.\\\\|.:\\/|.:\\\\)")
          SET(header "${class}.h")
          STRING(REGEX MATCH "[^/]*$" cxxclass "${class}")

          GET_FILENAME_COMPONENT(dir "${header}" PATH)
          INCLUDE_DIRECTORIES("${dir}")
        ENDIF("${class}" MATCHES "^(\\/|.\\/|.\\\\|.:\\/|.:\\\\)")

        SET(abstract 0)
        IF(VTK_CLASS_ABSTRACT_${class})
          SET(abstract 1)
          SET_SOURCE_FILES_PROPERTIES(${header} PROPERTIES ABSTRACT 1)
        ENDIF(VTK_CLASS_ABSTRACT_${class})

        # Build one master in-memory table so that we don't have to re-do all the
        # logic in this nested FOREACH loop later on... Instead we'll simply iterate
        # the in-memory table built here:
        #
        SET(WRAPPED_CLASS_TABLE ${WRAPPED_CLASS_TABLE} "${cxxclass} ${kit} ${abstract} ${header}")


        SET(include_source_text "")
        STRING(REGEX REPLACE "(.*).h" "\\1.cxx" source "${header}")
        IF(EXISTS "${source}")
          SET(include_source_text "#include \"${source}\"")
        ELSE(EXISTS "${source}")
          MESSAGE(FATAL_ERROR "error: Source file '${source}' does not exist...")
        ENDIF(EXISTS "${source}")


        CONFIGURE_FILE(
          "${CMAKE_CURRENT_SOURCE_DIR}/gccxml.cxx.in"
          "${CMAKE_CURRENT_BINARY_DIR}/xml/${cxxclass}_gccxml.cxx"
          @ONLY
        )

        ADD_CUSTOM_COMMAND(
          OUTPUT ${CMAKE_CURRENT_BINARY_DIR}/xml/${cxxclass}.xml
          COMMAND ${gccxml_EXECUTABLE}
          ARGS
            -fxml=${CMAKE_CURRENT_BINARY_DIR}/xml/${cxxclass}.xml
            -fxml-start=_cable_
            ${gccxml_include_args} -DCABLE_CONFIGURATION
            --gccxml-compiler ${gccxml_compiler}
            ${CMAKE_CURRENT_BINARY_DIR}/xml/${cxxclass}_gccxml.cxx
          DEPENDS
            ${CMAKE_CURRENT_BINARY_DIR}/xml/${cxxclass}_gccxml.cxx
            ${header}
            ${gccxml_EXECUTABLE}
        )

#
# *before* custom command re-arranging:
#
#          OUTPUT ${CMAKE_CURRENT_BINARY_DIR}/csharp/${cxxclass}.cs
#            ${CMAKE_CURRENT_BINARY_DIR}/export-layer/${cxxclass}EL.cxx
#
# Do not list the *.cs or *EL.cxx files as outputs of this custom command.
# If you do, the custom command chains out into other targets rather than
# being defined solely in the "generate wrappers" custom target.
#
# The output of this command is the generated sentinel file that the
# "generate wrappers" target depends on. The other files are generated as an
# intentional "side effect" and after the target is done building, the other
# targets that build the generated source code may build... That is controlled
# by target level dependencies to reduce complexity.
#
        ADD_CUSTOM_COMMAND(
          OUTPUT ${CMAKE_CURRENT_BINARY_DIR}/csharp/${cxxclass}-sentinel.txt
          COMMAND ${mummy_EXECUTABLE}
            --suppress-warnings 6005 6006 6009 6010 6012 6013 6015 6016 6017 6018 6019
            --settings-file ${CMAKE_CURRENT_BINARY_DIR}/xml/MummySettings.xml
            --gccxml-file ${CMAKE_CURRENT_BINARY_DIR}/xml/${cxxclass}.xml
            --csharp-file ${CMAKE_CURRENT_BINARY_DIR}/csharp/${cxxclass}.cs
            --export-layer-file ${CMAKE_CURRENT_BINARY_DIR}/export-layer/${cxxclass}EL.cxx
          COMMAND ${CMAKE_COMMAND} -E touch
            ${CMAKE_CURRENT_BINARY_DIR}/csharp/${cxxclass}-sentinel.txt
          DEPENDS
            ${CMAKE_CURRENT_BINARY_DIR}/xml/${cxxclass}.xml
            ${CMAKE_CURRENT_BINARY_DIR}/xml/MummySettings.xml
            ${mummy_EXECUTABLE}
            ${${cxxclass}_EXTRA_DEPENDENCIES}
        )

        SET(${kit}_EL_SOURCES ${${kit}_EL_SOURCES} "${CMAKE_CURRENT_BINARY_DIR}/export-layer/${cxxclass}EL.cxx")
        SET(ALLKITS_EL_SOURCES ${ALLKITS_EL_SOURCES} "${CMAKE_CURRENT_BINARY_DIR}/export-layer/${cxxclass}EL.cxx")

        SET(${kit}_CS_SOURCES ${${kit}_CS_SOURCES} "${CMAKE_CURRENT_BINARY_DIR}/csharp/${cxxclass}.cs")
        SET(ALLKITS_CS_SOURCES ${ALLKITS_CS_SOURCES} "${CMAKE_CURRENT_BINARY_DIR}/csharp/${cxxclass}.cs")

        SET(${kit}_SENTINELS ${${kit}_SENTINELS} "${CMAKE_CURRENT_BINARY_DIR}/csharp/${cxxclass}-sentinel.txt")
        SET(ALLKITS_SENTINELS ${ALLKITS_SENTINELS} "${CMAKE_CURRENT_BINARY_DIR}/csharp/${cxxclass}-sentinel.txt")
      ENDIF(NOT VTK_CLASS_WRAP_EXCLUDE_${class})
    ENDFOREACH(class)

    IF(NOT "${${kit}_SENTINELS}" STREQUAL "")
      ADD_CUSTOM_TARGET(
        "vtk${kit}GenerateWrappers" ALL
        DEPENDS ${${kit}_SENTINELS}
      )
    ENDIF(NOT "${${kit}_SENTINELS}" STREQUAL "")
  ELSE(EXISTS "${VTK_KITS_DIR}/vtk${kit}Kit.cmake")
    MESSAGE(STATUS "warning: VTK kit '${kit}' not found. Is that an optional kit that is switched off?")
  ENDIF(EXISTS "${VTK_KITS_DIR}/vtk${kit}Kit.cmake")
ENDFOREACH(kit)

SET_SOURCE_FILES_PROPERTIES(
  ${ALLKITS_EL_SOURCES}
  ${ALLKITS_CS_SOURCES}
  PROPERTIES GENERATED 1)

MESSAGE(STATUS "Done processing VTK_KITS_DIR.")


# Set list of export-layer dlls in C# syntax as CMake variable
# MV_EXPORTLAYER_DLL_VARIABLES. This gets configured into
# WrappedObject.cs below.
#
SET(MV_EXPORTLAYER_DLL_VARIABLES "")
FOREACH(kit ${kits})
  SET(MV_EXPORTLAYER_DLL_VARIABLES "${MV_EXPORTLAYER_DLL_VARIABLES}    /// <summary>\n")
  SET(MV_EXPORTLAYER_DLL_VARIABLES "${MV_EXPORTLAYER_DLL_VARIABLES}    /// Export layer functions for 'vtk${kit}' are exported from\n")
  SET(MV_EXPORTLAYER_DLL_VARIABLES "${MV_EXPORTLAYER_DLL_VARIABLES}    /// the DLL named by the value of this variable.\n")
  SET(MV_EXPORTLAYER_DLL_VARIABLES "${MV_EXPORTLAYER_DLL_VARIABLES}    /// </summary>\n")

  IF(MV_ONE_EXPORT_LAYER_DLL)
    SET(MV_EXPORTLAYER_DLL_VARIABLES "${MV_EXPORTLAYER_DLL_VARIABLES}    public const string vtk${kit}EL_dll = \"${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.VTK.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}\";\n")
  ELSE(MV_ONE_EXPORT_LAYER_DLL)
    SET(MV_EXPORTLAYER_DLL_VARIABLES "${MV_EXPORTLAYER_DLL_VARIABLES}    public const string vtk${kit}EL_dll = \"${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.VTK.vtk${kit}.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}\";\n")
  ENDIF(MV_ONE_EXPORT_LAYER_DLL)
ENDFOREACH(kit)


# Configure list of C# source files:
#
SET(csharp_namespace "Kitware.VTK")
CONFIGURE_FILE(
  "${CMAKE_CURRENT_SOURCE_DIR}/WrappedObject.cs.in"
  "${CMAKE_CURRENT_BINARY_DIR}/csharp/WrappedObject.cs"
  @ONLY
)

CONFIGURE_FILE(
  "${CMAKE_CURRENT_SOURCE_DIR}/AssemblyInfo.cs.in"
  "${CMAKE_CURRENT_BINARY_DIR}/csharp/AssemblyInfo.cs"
  @ONLY
)

SET(ALLKITS_CS_SOURCES
  ${ALLKITS_CS_SOURCES}
  "${CMAKE_CURRENT_SOURCE_DIR}/RenderWindowControl/RenderWindowControl.cs"
  "${CMAKE_CURRENT_SOURCE_DIR}/RenderWindowControl/RenderWindowControl.Designer.cs"
  "${CMAKE_CURRENT_BINARY_DIR}/csharp/WrappedObject.cs"
  "${CMAKE_CURRENT_BINARY_DIR}/csharp/AssemblyInfo.cs"
)


MACRO(ADD_CUSTOM_COMMAND_COPYFILE srcFile dstFile)
  ADD_CUSTOM_COMMAND(
    OUTPUT "${dstFile}"
    DEPENDS "${srcFile}"
    COMMAND ${CMAKE_COMMAND}
    ARGS -E copy "${srcFile}" "${dstFile}"
  )
ENDMACRO(ADD_CUSTOM_COMMAND_COPYFILE)


MACRO(ADD_CUSTOM_COMMAND_SYMLINK srcFile linkFile)
  GET_FILENAME_COMPONENT(srcFilePath "${srcFile}" PATH)
  GET_FILENAME_COMPONENT(linkFilePath "${linkFile}" PATH)

  IF("${srcFilePath}" STREQUAL "${linkFilePath}")
    GET_FILENAME_COMPONENT(srcFileName "${srcFile}" NAME)
    SET(srcFileName "./${srcFileName}")
  ELSE("${srcFilePath}" STREQUAL "${linkFilePath}")
    SET(srcFileName "${srcFile}")
  ENDIF("${srcFilePath}" STREQUAL "${linkFilePath}")

  ADD_CUSTOM_COMMAND(
    OUTPUT "${linkFile}"
    DEPENDS "${srcFile}"
    COMMAND ${CMAKE_COMMAND}
    ARGS -E create_symlink "${srcFileName}" "${linkFile}"
  )
ENDMACRO(ADD_CUSTOM_COMMAND_SYMLINK)


SET(vtkListOfDLLs "")

SET(AVDN_DO_COPY_LIBRARIES_STEPS 1)
IF(AVDN_DO_COPY_LIBRARIES_STEPS)
  #
  # Add custom commands/target to copy the mummy Runtime libs to
  # LIBRARY_OUTPUT_PATH and dlls to EXECUTABLE_OUTPUT_PATH:
  #
  SET(lib_dir "${CMAKE_CURRENT_BINARY_DIR}/${CMAKE_CFG_INTDIR}")
  IF(LIBRARY_OUTPUT_PATH)
    SET(lib_dir "${LIBRARY_OUTPUT_PATH}/${CMAKE_CFG_INTDIR}")
  ENDIF(LIBRARY_OUTPUT_PATH)

  SET(exe_dir "${CMAKE_CURRENT_BINARY_DIR}/${CMAKE_CFG_INTDIR}")
  IF(EXECUTABLE_OUTPUT_PATH)
    SET(exe_dir "${EXECUTABLE_OUTPUT_PATH}/${CMAKE_CFG_INTDIR}")
  ENDIF(EXECUTABLE_OUTPUT_PATH)

  # x86 binaries (exe, dll and lib) are in ${Mummy_BIN_DIR}:
  #
  SET(bin_src_dir "${Mummy_BIN_DIR}")
  SET(lib_src_dir "${Mummy_BIN_DIR}")

  # x64 binaries (exe, dll and lib) are in ${Mummy_BIN_x64_DIR}:
  #
  IF(CMAKE_SIZEOF_VOID_P EQUAL 8)
    SET(bin_src_dir "${Mummy_BIN_x64_DIR}")
    SET(lib_src_dir "${Mummy_BIN_x64_DIR}")
  ENDIF(CMAKE_SIZEOF_VOID_P EQUAL 8)

  IF(WIN32)
    # .lib file is Windows only...
    ADD_CUSTOM_COMMAND_COPYFILE(
      "${lib_src_dir}/Kitware.mummy.Runtime.Unmanaged.lib"
      "${lib_dir}/Kitware.mummy.Runtime.Unmanaged.lib"
    )
  ENDIF(WIN32)

  ADD_CUSTOM_COMMAND_COPYFILE(
    "${bin_src_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.mummy.Runtime.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}"
    "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.mummy.Runtime.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}"
  )

  ADD_CUSTOM_COMMAND_COPYFILE(
    "${bin_src_dir}/Kitware.mummy.Runtime.dll"
    "${exe_dir}/Kitware.mummy.Runtime.dll"
  )

  SET(MCL_deps
    "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.mummy.Runtime.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}"
    "${exe_dir}/Kitware.mummy.Runtime.dll"
  )

  IF(WIN32)
    # .lib file is Windows only...
    SET(MCL_deps ${MCL_deps}
      "${lib_dir}/Kitware.mummy.Runtime.Unmanaged.lib"
    )
  ENDIF(WIN32)

  # Rules to copy pdb files only get added if the source pdb files actually
  # exist at CMake configure time...
  #
  IF(EXISTS "${bin_src_dir}/kitware.mummy.runtime.unmanaged.pdb")
    ADD_CUSTOM_COMMAND_COPYFILE(
      "${bin_src_dir}/kitware.mummy.runtime.unmanaged.pdb"
      "${exe_dir}/kitware.mummy.runtime.unmanaged.pdb"
    )
    SET(MCL_deps ${MCL_deps}
      "${exe_dir}/kitware.mummy.runtime.unmanaged.pdb"
    )
  ENDIF(EXISTS "${bin_src_dir}/kitware.mummy.runtime.unmanaged.pdb")

  IF(EXISTS "${bin_src_dir}/Kitware.mummy.Runtime.pdb")
    ADD_CUSTOM_COMMAND_COPYFILE(
      "${bin_src_dir}/Kitware.mummy.Runtime.pdb"
      "${exe_dir}/Kitware.mummy.Runtime.pdb"
    )
    SET(MCL_deps ${MCL_deps}
      "${exe_dir}/Kitware.mummy.Runtime.pdb"
    )
  ENDIF(EXISTS "${bin_src_dir}/Kitware.mummy.Runtime.pdb")

  # david.cole::fix -- TODO -- rename this CopyLibraries and eliminate
  # the "other" CopyLibraries in the VE source tree... Putting it here
  # and having the VE depend on ActiVizDotNet is sufficient...
  #
  ADD_CUSTOM_TARGET(
    Kitware.mummy.CopyLibraries2 ALL
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

    # Since this whole chunk copies files from ${vtk_dll_dir} to
    # ${exe_dir}, only add the commands to do it if those directories
    # are different:
    #
    IF(NOT "${vtk_dll_dir}" STREQUAL "${exe_dir}")
      # Use the list of "kits in use" as the basis for which DLLs to copy:
      #
      FOREACH(kit ${kits})
        SET(vtkListOfDLLs ${vtkListOfDLLs} "vtk${kit}")
      ENDFOREACH(kit)

      # Add extra utility DLLs the kit DLLs depend on:
      #
      SET(vtkListOfDLLs ${vtkListOfDLLs}
        Cosmo
        VPIC
        vtkalglib
        vtkDICOMParser
        vtkexoIIc
        vtkftgl
        vtkmetaio
        vtkNetCDF
        vtkNetCDF_cxx
        vtksys
        vtkverdict
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

      # On Linux/Mac, VTK's *real* shared libs are named with ".so.5.4.0" (for example)
      # at the end of the file name. The ".so" and ".so.5.4" files are symlinks to the
      # real shared lib...
      #
      SET(vtk_sl_suffix "")
      SET(vtk_sl_base_suffix "")
      IF(NOT WIN32)
        SET(vtk_sl_base_suffix ".${VTK_MAJOR_VERSION}.${VTK_MINOR_VERSION}.${VTK_BUILD_VERSION}")
        SET(vtk_major_minor ".${VTK_MAJOR_VERSION}.${VTK_MINOR_VERSION}")
      ENDIF(NOT WIN32)

      # Loop over all the VTK dlls and add a custom command to copy each one to the
      # build tree's "exe_dir". Also add each one to the list of files to install.
      #
      FOREACH(vtkDLL ${vtkListOfDLLs})
        IF(NOT vtk_sl_base_suffix STREQUAL "")
          SET(vtk_sl_suffix "${vtk_sl_base_suffix}")
        ENDIF(NOT vtk_sl_base_suffix STREQUAL "")

        ADD_CUSTOM_COMMAND_COPYFILE(
          "${vtk_dll_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${CMAKE_SHARED_LIBRARY_SUFFIX}${vtk_sl_suffix}"
          "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${CMAKE_SHARED_LIBRARY_SUFFIX}${vtk_sl_suffix}"
        )

        SET(AVDN_INSTALL_FILES ${AVDN_INSTALL_FILES}
          "${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${CMAKE_SHARED_LIBRARY_SUFFIX}${vtk_sl_suffix}"
        )

        SET(VCL_deps ${VCL_deps}
          "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${CMAKE_SHARED_LIBRARY_SUFFIX}${vtk_sl_suffix}"
        )

        IF(NOT vtk_sl_base_suffix STREQUAL "")
          ADD_CUSTOM_COMMAND_SYMLINK(
            "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${CMAKE_SHARED_LIBRARY_SUFFIX}${vtk_sl_suffix}"
            "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${CMAKE_SHARED_LIBRARY_SUFFIX}${vtk_major_minor}"
          )
          ADD_CUSTOM_COMMAND_SYMLINK(
            "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${CMAKE_SHARED_LIBRARY_SUFFIX}${vtk_major_minor}"
            "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${CMAKE_SHARED_LIBRARY_SUFFIX}"
          )

          SET(AVDN_INSTALL_FILES ${AVDN_INSTALL_FILES}
            "${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${CMAKE_SHARED_LIBRARY_SUFFIX}${vtk_major_minor}"
            "${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${CMAKE_SHARED_LIBRARY_SUFFIX}"
          )

          SET(VCL_deps ${VCL_deps}
            "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${CMAKE_SHARED_LIBRARY_SUFFIX}${vtk_major_minor}"
            "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${CMAKE_SHARED_LIBRARY_SUFFIX}"
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
  ENDIF(VTK_BUILD_SHARED_LIBS)

  # Also copy over the MSVC runtime dlls (so we can use them by
  # file name only as /linkresource args to the C# compiler)
  #
  IF(CMAKE_INSTALL_SYSTEM_RUNTIME_LIBS)
    SET(lib_deps "")
    FOREACH(lib ${CMAKE_INSTALL_SYSTEM_RUNTIME_LIBS})
      GET_FILENAME_COMPONENT(f "${lib}" NAME)
      ADD_CUSTOM_COMMAND_COPYFILE(
        "${lib}"
        "${exe_dir}/${f}"
      )
      SET(AVDN_INSTALL_FILES ${AVDN_INSTALL_FILES} "${f}")
      SET(lib_deps ${lib_deps} "${exe_dir}/${f}")
    ENDFOREACH(lib)
    ADD_CUSTOM_TARGET(
      Kitware.VTK.CopyMsvcDLLs ALL
      DEPENDS ${lib_deps}
    )
    SET(AVDN_HAS_TARGET_KITWARE_VTK_COPYMSVCDLLS 1)
  ENDIF(CMAKE_INSTALL_SYSTEM_RUNTIME_LIBS)
ENDIF(AVDN_DO_COPY_LIBRARIES_STEPS)


ADD_DEPENDENCIES(vtkCommonGenerateWrappers AssembleExtraSources)


# The big one or the many little ones:
#
SET(unmanaged_targets "")
SET(unmanaged_dlls "")
IF(MV_ONE_EXPORT_LAYER_DLL)
  SET(AVDN_INSTALL_TARGETS ${AVDN_INSTALL_TARGETS} "Kitware.VTK.Unmanaged")
  ADD_LIBRARY(Kitware.VTK.Unmanaged SHARED ${ALLKITS_EL_SOURCES})
  SET(unmanaged_targets ${unmanaged_targets} "Kitware.VTK.Unmanaged")
  SET(unmanaged_dlls ${unmanaged_dlls} "${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.VTK.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}")
  IF(WIN32)
    TARGET_LINK_LIBRARIES(Kitware.VTK.Unmanaged "${exe_dir}/Kitware.mummy.Runtime.Unmanaged.lib")
  ELSE(WIN32)
    TARGET_LINK_LIBRARIES(Kitware.VTK.Unmanaged "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.mummy.Runtime.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}")
  ENDIF(WIN32)
  # david.cole::fix -- TODO -- rename this CopyLibraries here too...
  ADD_DEPENDENCIES(Kitware.VTK.Unmanaged Kitware.mummy.CopyLibraries2)
  FOREACH(kit ${kits})
    TARGET_LINK_LIBRARIES(Kitware.VTK.Unmanaged vtk${kit})
    ADD_DEPENDENCIES(Kitware.VTK.Unmanaged "vtk${kit}GenerateWrappers")
  ENDFOREACH(kit)
ELSE(MV_ONE_EXPORT_LAYER_DLL)
  FOREACH(kit ${kits})
    SET(AVDN_INSTALL_TARGETS ${AVDN_INSTALL_TARGETS} "Kitware.VTK.vtk${kit}.Unmanaged")
    ADD_LIBRARY(Kitware.VTK.vtk${kit}.Unmanaged SHARED ${${kit}_EL_SOURCES})
    SET(unmanaged_targets ${unmanaged_targets} "Kitware.VTK.vtk${kit}.Unmanaged")
    SET(unmanaged_dlls ${unmanaged_dlls} "${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.VTK.vtk${kit}.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}")
    IF(WIN32)
      TARGET_LINK_LIBRARIES(Kitware.VTK.vtk${kit}.Unmanaged "${exe_dir}/Kitware.mummy.Runtime.Unmanaged.lib")
    ELSE(WIN32)
      TARGET_LINK_LIBRARIES(Kitware.VTK.vtk${kit}.Unmanaged "${exe_dir}/${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.mummy.Runtime.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}")
    ENDIF(WIN32)
    TARGET_LINK_LIBRARIES(Kitware.VTK.vtk${kit}.Unmanaged vtk${kit})
    # david.cole::fix -- TODO -- rename this CopyLibraries here too...
    ADD_DEPENDENCIES(Kitware.VTK.vtk${kit}.Unmanaged Kitware.mummy.CopyLibraries2)
    ADD_DEPENDENCIES(Kitware.VTK.vtk${kit}.Unmanaged "vtk${kit}GenerateWrappers")
  ENDFOREACH(kit)
ENDIF(MV_ONE_EXPORT_LAYER_DLL)


FOREACH(vtkDLL ${vtkListOfDLLs})
  SET(unmanaged_dlls ${unmanaged_dlls} "${CMAKE_SHARED_LIBRARY_PREFIX}${vtkDLL}${CMAKE_SHARED_LIBRARY_SUFFIX}")
ENDFOREACH(vtkDLL)

FOREACH(lib ${CMAKE_INSTALL_SYSTEM_RUNTIME_LIBS})
  GET_FILENAME_COMPONENT(f "${lib}" NAME)
  SET(unmanaged_dlls ${unmanaged_dlls} "${f}")
ENDFOREACH(lib)

IF(AVDN_HAS_TARGET_KITWARE_VTK_COPYDLLS)
  SET(unmanaged_targets ${unmanaged_targets} "Kitware.VTK.CopyDLLs")
ENDIF(AVDN_HAS_TARGET_KITWARE_VTK_COPYDLLS)

IF(AVDN_HAS_TARGET_KITWARE_VTK_COPYMSVCDLLS)
  SET(unmanaged_targets ${unmanaged_targets} "Kitware.VTK.CopyMsvcDLLs")
ENDIF(AVDN_HAS_TARGET_KITWARE_VTK_COPYMSVCDLLS)


# If a real strong name signing keyfile has not been predefined,
# then use the public key file in the source tree to /delaysign
# .NET assemblies...
#
IF(NOT DEFINED AVDN_SNKEYFILE)
  GET_FILENAME_COMPONENT(AVDN_SNKEYFILE "${CMAKE_CURRENT_SOURCE_DIR}/Kitware.VTK.pub.snk" ABSOLUTE)
ENDIF(NOT DEFINED AVDN_SNKEYFILE)
MESSAGE(STATUS "AVDN_SNKEYFILE='${AVDN_SNKEYFILE}'")


SET(AVDN_VTK_CSC_REFS "Kitware.mummy.Runtime")

IF(NOT WIN32)
  #
  # The mono C# compiler does not appear to add these common refs by default
  # like the MS C# compiler does... add them explicitly:
  #
  SET(AVDN_VTK_CSC_REFS ${AVDN_VTK_CSC_REFS}
    "System.Drawing"
    "System.Data"
    "System.Windows.Forms"
    )
ENDIF(NOT WIN32)

# david.cole::fix -- TODO -- rename this CopyLibraries here too...
ADD_CSHARP_LIBRARY(
  "Kitware.VTK" # name of library
  "${unmanaged_targets}" # list of CMake targets that need to build first
  "${AVDN_VTK_CSC_REFS}" # list of csc "/reference:" args
  "${unmanaged_dlls}" # list of csc "/linkresource:" args
  "${AVDN_SNKEYFILE}" # strong name signing keyfile
  # Source files:
  ${ALLKITS_CS_SOURCES}
)


IF(WIN32)
  IF(VTK_BUILD_SHARED_LIBS)
    ADD_DEPENDENCIES(Kitware.VTK Kitware.VTK.CopyDLLs)
  ENDIF(VTK_BUILD_SHARED_LIBS)
ENDIF(WIN32)

FOREACH(kit ${kits})
  ADD_DEPENDENCIES(Kitware.VTK "vtk${kit}GenerateWrappers")
ENDFOREACH(kit)

IF(AVDN_EXTRA_DEPENDENCIES)
  ADD_DEPENDENCIES(Kitware.VTK ${AVDN_EXTRA_DEPENDENCIES})
ENDIF(AVDN_EXTRA_DEPENDENCIES)


# Avoid the "Continuous, Experimental, Nightly and NightlyMemoryCheck" custom
# targets for Visual Studio builds:
#
IF(CMAKE_GENERATOR MATCHES "Visual Studio")
IF(COMMAND SET_PROPERTY)
  SET_PROPERTY(GLOBAL PROPERTY CTEST_TARGETS_ADDED 1)
ENDIF(COMMAND SET_PROPERTY)
ENDIF(CMAKE_GENERATOR MATCHES "Visual Studio")


IF(AVDN_BUILD_IS_TOP_LEVEL)
  MESSAGE(STATUS "info: AVDN_BUILD_IS_TOP_LEVEL='${AVDN_BUILD_IS_TOP_LEVEL}'")
  INCLUDE(CTest)
  ENABLE_TESTING()
  SET(AVDN_BUILD_TESTING 1)
  SET(AVDN_BUILD_EXAMPLES 1)
ENDIF(AVDN_BUILD_IS_TOP_LEVEL)


MACRO(CONFIGURE_EXAMPLE ce_name)
  # Copy the whole example directory given to its parallel in the build tree:
  #
  EXECUTE_PROCESS(COMMAND ${CMAKE_COMMAND} -E copy_directory
    "${ActiVizDotNet_SOURCE_DIR}/Examples/${ce_name}"
    "${ActiVizDotNet_BINARY_DIR}/Examples/${ce_name}"
    )

  IF(CMAKE_SIZEOF_VOID_P EQUAL 8)
    SET(AVDN_PLATFORM "x64")
  ELSE(CMAKE_SIZEOF_VOID_P EQUAL 8)
    SET(AVDN_PLATFORM "x86")
  ENDIF(CMAKE_SIZEOF_VOID_P EQUAL 8)

  SET(AVDN_MUMMY_QUALIFIED_REFERENCE_NAME
    "Kitware.mummy.Runtime, Version=${Mummy_VERSION_MAJOR}.${Mummy_VERSION_MINOR}.${Mummy_VERSION_PATCH}.${Mummy_VERSION_SVN_REVISION}, Culture=neutral, PublicKeyToken=995c7fb9db2c1b44, processorArchitecture=${AVDN_PLATFORM}")

  SET(AVDN_VTK_QUALIFIED_REFERENCE_NAME
    "Kitware.VTK, Version=${AVDN_VERSION_STRING}.${AVDN_VERSION_SVN_REVISION}, Culture=neutral, PublicKeyToken=995c7fb9db2c1b44, processorArchitecture=${AVDN_PLATFORM}")

  CONFIGURE_FILE(${ActiVizDotNet_SOURCE_DIR}/Examples/${ce_name}/CS/${ce_name}.sln.in
                 ${ActiVizDotNet_BINARY_DIR}/Examples/${ce_name}/CS/${ce_name}.sln @ONLY)

  CONFIGURE_FILE(${ActiVizDotNet_SOURCE_DIR}/Examples/${ce_name}/CS/${ce_name}.csproj.in
                 ${ActiVizDotNet_BINARY_DIR}/Examples/${ce_name}/CS/${ce_name}.csproj @ONLY)

  CONFIGURE_FILE(${ActiVizDotNet_SOURCE_DIR}/Examples/${ce_name}/VB/${ce_name}.sln.in
                 ${ActiVizDotNet_BINARY_DIR}/Examples/${ce_name}/VB/${ce_name}.sln @ONLY)

  CONFIGURE_FILE(${ActiVizDotNet_SOURCE_DIR}/Examples/${ce_name}/VB/${ce_name}.vbproj.in
                 ${ActiVizDotNet_BINARY_DIR}/Examples/${ce_name}/VB/${ce_name}.vbproj @ONLY)

ENDMACRO(CONFIGURE_EXAMPLE)

CONFIGURE_EXAMPLE("BoxWidget")
CONFIGURE_EXAMPLE("CubeAxes")
CONFIGURE_EXAMPLE("Decimate")
CONFIGURE_EXAMPLE("DelMesh")
CONFIGURE_EXAMPLE("Dialog")
CONFIGURE_EXAMPLE("FileTree")
CONFIGURE_EXAMPLE("SpherePuzzle")
CONFIGURE_EXAMPLE("Streamline")
CONFIGURE_EXAMPLE("VolumeRendering")
CONFIGURE_EXAMPLE("Wikipedia")
CONFIGURE_EXAMPLE("HelloVTKConsole")
CONFIGURE_EXAMPLE("HelloVTKForm")


IF(AVDN_BUILD_EXAMPLES)
  MESSAGE(STATUS "info: AVDN_BUILD_EXAMPLES='${AVDN_BUILD_EXAMPLES}'")
  INCLUDE("${ActiVizDotNet_SOURCE_DIR}/BuildExamples.cmake")
ENDIF(AVDN_BUILD_EXAMPLES)


IF(AVDN_BUILD_TESTING)
  MESSAGE(STATUS "info: AVDN_BUILD_TESTING='${AVDN_BUILD_TESTING}'")
  ADD_SUBDIRECTORY(Testing)
ENDIF(AVDN_BUILD_TESTING)


# "make install" rules
#
IF(AVDN_BUILD_IS_TOP_LEVEL)
  SET(AVDN_ADD_INSTALL_RULES 1)
ENDIF(AVDN_BUILD_IS_TOP_LEVEL)

IF(AVDN_ADD_INSTALL_RULES)
  IF(AVDN_INSTALL_TARGETS)
    INSTALL(TARGETS ${AVDN_INSTALL_TARGETS}
      RUNTIME DESTINATION "bin"
      LIBRARY DESTINATION "bin"
    )
  ENDIF(AVDN_INSTALL_TARGETS)

  # These dll files only get built on Windows, and they are only
  # in the \${BUILD_TYPE} subdirectory for build systems that use
  # CMAKE_CONFIGURATION_TYPES:
  #
  IF(CMAKE_CONFIGURATION_TYPES)
    SET(exe_dir "${CMAKE_CURRENT_BINARY_DIR}/\${BUILD_TYPE}")
    IF(EXECUTABLE_OUTPUT_PATH)
      SET(exe_dir "${EXECUTABLE_OUTPUT_PATH}/\${BUILD_TYPE}")
    ENDIF(EXECUTABLE_OUTPUT_PATH)
  ELSE(CMAKE_CONFIGURATION_TYPES)
    SET(exe_dir "${CMAKE_CURRENT_BINARY_DIR}")
    IF(EXECUTABLE_OUTPUT_PATH)
      SET(exe_dir "${EXECUTABLE_OUTPUT_PATH}")
    ENDIF(EXECUTABLE_OUTPUT_PATH)
  ENDIF(CMAKE_CONFIGURATION_TYPES)

  SET(AVDN_INSTALL_FILES ${AVDN_INSTALL_FILES}
    "Kitware.mummy.Runtime.dll"
    "${CMAKE_SHARED_LIBRARY_PREFIX}Kitware.mummy.Runtime.Unmanaged${CMAKE_SHARED_LIBRARY_SUFFIX}"
    "Kitware.VTK.dll"
    "Kitware.VTK.xml"
  )

  IF(AVDN_BUILD_EXAMPLES AND AVDN_BUILD_SLN_FILES)
    SET(AVDN_INSTALL_FILES ${AVDN_INSTALL_FILES}
      "FileTree.exe"
    )
  ENDIF(AVDN_BUILD_EXAMPLES AND AVDN_BUILD_SLN_FILES)

  SET(fullfiles "" )
  FOREACH(ff ${AVDN_INSTALL_FILES})
    SET(fullfiles ${fullfiles} "${exe_dir}/${ff}")
  ENDFOREACH(ff)

  INSTALL(FILES ${fullfiles}
    DESTINATION "bin"
  )

  INSTALL(FILES
    "${ActiVizDotNet_SOURCE_DIR}/Documentation/UsersGuide.pdf"
    "${ActiVizDotNet_SOURCE_DIR}/License${AVDN_EDITION_SUFFIX}.txt"
    DESTINATION "."
  )

  # Also install the Examples directory (except for the
  # version control system bits of the tree and the *.in
  # files used to configure .sln/.csproj/.vbproj files...)
  #
  INSTALL(DIRECTORY "${ActiVizDotNet_BINARY_DIR}/Examples"
    DESTINATION "."
    REGEX "^(.*/\\.svn.*|.*\\.in)$" EXCLUDE
  )
ENDIF(AVDN_ADD_INSTALL_RULES)


# Build an installer
#
IF(AVDN_BUILD_IS_TOP_LEVEL AND AVDN_ADD_INSTALL_RULES)
  SET(AVDN_BUILD_INSTALLER 1)
ENDIF(AVDN_BUILD_IS_TOP_LEVEL AND AVDN_ADD_INSTALL_RULES)

IF(AVDN_BUILD_INSTALLER)
  INCLUDE("${CMAKE_CURRENT_SOURCE_DIR}/ActiVizDotNetCPack.cmake")
ENDIF(AVDN_BUILD_INSTALLER)

#Configure Doxygen
#
OPTION(GENERATE_DOXYGEN
  "Generate doxygen documentation"
  OFF
  )
IF(GENERATE_DOXYGEN)

  IF(NOT DOXYGEN_EXECUTABLE)
    FIND_PACKAGE(Doxygen)
  ENDIF(NOT DOXYGEN_EXECUTABLE)
  IF(NOT DOXYGEN_EXECUTABLE)
    MESSAGE(FATAL_ERROR "error: doxygen executable not found. doygen_EXECUTABLE = '${DOXYGEN_EXECUTABLE}'")
  ENDIF(NOT DOXYGEN_EXECUTABLE)

  GET_FILENAME_COMPONENT(DOXYGEN_PATH "${DOXYGEN_EXECUTABLE}" PATH)

  SET(HAVE_DOT_YESNO NO)
  IF(DOXYGEN_DOT_EXECUTABLE)
    SET(HAVE_DOT_YESNO YES)
  ENDIF(DOXYGEN_DOT_EXECUTABLE)
  
  
  CONFIGURE_FILE(
  "${CMAKE_CURRENT_SOURCE_DIR}/Doxyfile.in"
  "${CMAKE_CURRENT_BINARY_DIR}/Doxygen/Doxyfile"
  @ONLY
  )

  ADD_CUSTOM_TARGET(AVDN_doxygen ALL
    ${DOXYGEN_EXECUTABLE} ${CMAKE_CURRENT_BINARY_DIR}/Doxygen/Doxyfile
    )
  ADD_DEPENDENCIES(AVDN_doxygen Kitware.VTK)

ENDIF(GENERATE_DOXYGEN)



# Hide stuff nobody cares about and stuff we don't want people changing...
#
IF(AVDN_BUILD_IS_TOP_LEVEL)
  MARK_AS_ADVANCED(BUILD_TESTING)
  MARK_AS_ADVANCED(CMAKE_BACKWARDS_COMPATIBILITY)
  MARK_AS_ADVANCED(DART_ROOT)
ENDIF(AVDN_BUILD_IS_TOP_LEVEL)
