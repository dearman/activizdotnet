##############################################################################
# A SuperBuild for ActiViz .NET and its pre-requisites

cmake_minimum_required(VERSION 2.8.6 FATAL_ERROR)
project(ActiVizDotNetSuperBuild NONE)

include(ExternalProject)
include(CTest)


##############################################################################
# ActiViz .NET requires tools, data and libraries.
#
# Tools: (these must be installed before configuring this SuperBuild)
#   cmake 2.8.6 or later
#   python 2.6 or later
#   cvs
#   git
#
#
# The following are all built by this SuperBuild before ActiViz .NET:
#
# Tools:
#   gccxml
#   mummy (depends on gccxml)
#
# Data:
#   VTKData
#
# Libraries:
#   VTK (depends on VTKData)
#
#
# If doxygen is installed, the mummy build will include some doxygen
# generated docs.
#
# Python must be installed in order to configure ActiViz .NET: the test suite
# has some tests that are generated using python scripts.


##############################################################################
# CMAKE_GENERATOR restriction
#
# On Windows, with the 32-bit Visual Studio generators, this SuperBuild
# includes targets for both 32-bit and 64-bit VTK and ActiViz .NET.
#
# The mummy build also includes both 32-bit and 64-bit outputs when
# configured with the 32-bit generators. Therefore, we disallow the Win64
# generators.
#
# gccxml and mummy are built with the chosen CMAKE_GENERATOR. When the
# generator matches "Visual Studio" VTK and ActiViz .NET get targets with
# both "${CMAKE_GENERATOR}" and "${CMAKE_GENERATOR} Win64"...

if(CMAKE_GENERATOR MATCHES "Win64")
  set(msg "Do not use a Win64 generator with the ActiViz .NET SuperBuild.")
  set(msg "${msg} The SuperBuild includes targets for both x86 and x64 when")
  set(msg "${msg} you use the non-Win64 generator...")
  message(FATAL_ERROR "${msg}")
endif()

set(AVSB_build_x64_targets OFF)
if(CMAKE_GENERATOR MATCHES "Visual Studio")
  set(AVSB_build_x64_targets ON)
endif()


##############################################################################
# Versions this SuperBuild references:

set(gccxml_version "2011-11-03 01:00:00 UTC")
set(mummy_version "1.0.2")

set(vtk_version "5.8.0")
if(NOT "$ENV{AVSB_VTK_VERSION}" STREQUAL "")
  set(vtk_version "$ENV{AVSB_VTK_VERSION}")
endif()

set(vtk_git_tag "v${vtk_version}")
if(NOT "$ENV{AVSB_VTK_GIT_TAG}" STREQUAL "")
  set(vtk_git_tag "$ENV{AVSB_VTK_GIT_TAG}")
endif()

set(vtkdata_git_tag "v${vtk_version}-data")
if(NOT "$ENV{AVSB_VTKDATA_GIT_TAG}" STREQUAL "")
  set(vtkdata_git_tag "$ENV{AVSB_VTKDATA_GIT_TAG}")
endif()


##############################################################################
# SuperBuild options:

option(BUILD_SHARED_LIBS "Build pre-requisite libs as shared libraries" ON)
option(AVSB_BUILD_FOR_REDIST "Strong-name sign build products, create installers" ON)
set(AVSB_SNKEYFILE "" CACHE FILEPATH "Strong name signing key")


##############################################################################
# Double checks:

set(avdn_redist_args "")

if(AVSB_BUILD_FOR_REDIST)
  set(avdn_redist_args "-DAVDN_BUILD_CSHARP_DEBUG:BOOL=OFF")

  if(NOT AVSB_SNKEYFILE)
    set(msg "Empty AVSB_SNKEYFILE value, C# dlls will not be correctly")
    set(msg "${msg} strong-name signed...")
    message(AUTHOR_WARNING "${msg}")
      # If this warning is emitted, the mummy and ActiViz .NET test suites
      # will not run correctly on this machine unless strong name validation
      # is disabled...
  endif()
endif()


##############################################################################
# gccxml

ExternalProject_Add(gccxml
  CVS_REPOSITORY ":pserver:anoncvs:@www.gccxml.org:/cvsroot/GCC_XML"
  CVS_MODULE gccxml
  CVS_TAG -D "${gccxml_version}"
  UPDATE_COMMAND "" # update unnecessary, because it's a fixed static tag
  CMAKE_ARGS
    -DCMAKE_INSTALL_PREFIX:PATH=<INSTALL_DIR>
)

ExternalProject_Get_Property(gccxml binary_dir)
set(gccxml_binary_dir "${binary_dir}")

ExternalProject_Get_Property(gccxml install_dir)
set(gccxml_dir "${install_dir}")
set(gccxml_exe "${gccxml_dir}/bin/gccxml")

if(WIN32)
  ExternalProject_Add_Step(gccxml gccxml_clean_install
    COMMAND ${CMAKE_COMMAND} -E remove_directory ${gccxml_dir}/bin
    COMMAND ${CMAKE_COMMAND} -E remove_directory ${gccxml_dir}/share
    WORKING_DIRECTORY ${gccxml_dir}
    DEPENDEES build # happens after build...
    DEPENDERS install # ...but before install
  )

  ExternalProject_Add_Step(gccxml gccxml_vcconfig
    COMMAND gccxml_vcconfig.bat
    WORKING_DIRECTORY ${gccxml_dir}/bin
    DEPENDEES install
  )
endif()


##############################################################################
# mummy

set(mummy_keyfile_args "")
if(AVSB_SNKEYFILE)
  set(mummy_keyfile_args "-DMummy_SNKEYFILE:FILEPATH=${AVSB_SNKEYFILE}")
endif()

ExternalProject_Add(mummy
  GIT_REPOSITORY "git://public.kitware.com/mummy.git"
  GIT_TAG "v${mummy_version}"
  UPDATE_COMMAND "" # update unnecessary, because it's a fixed static tag
  CMAKE_ARGS
    -Dgccxml_EXECUTABLE:FILEPATH=${gccxml_exe}
    -DMummy_INSTALL_PREFIX:PATH=<INSTALL_DIR>
    ${mummy_keyfile_args}
  DEPENDS gccxml
)

ExternalProject_Get_Property(mummy binary_dir)
set(mummy_binary_dir "${binary_dir}")

ExternalProject_Get_Property(mummy install_dir)
set(mummy_dir "${install_dir}/share/mummy-${mummy_version}")


##############################################################################
# VTKData

ExternalProject_Add(vtkdata
  GIT_REPOSITORY "git://vtk.org/VTKData.git"
  GIT_TAG "${vtkdata_git_tag}"
  UPDATE_COMMAND "" # update unnecessary, because it's a fixed static tag
  CONFIGURE_COMMAND ""
  BUILD_COMMAND ""
  INSTALL_COMMAND ""
)

ExternalProject_Get_Property(vtkdata source_dir)
set(vtkdata_dir "${source_dir}")


##############################################################################
# VTK

ExternalProject_Add(vtksource
  GIT_REPOSITORY "git://vtk.org/VTK.git"
  GIT_TAG "${vtk_git_tag}"
  UPDATE_COMMAND "" # update unnecessary, because it's a fixed static tag
  CONFIGURE_COMMAND ""
  BUILD_COMMAND ""
  INSTALL_COMMAND ""
  DEPENDS
    vtkdata
)

ExternalProject_Get_Property(vtksource source_dir)
set(vtksource_dir "${source_dir}")

function(build_vtk suffix generator)
  ExternalProject_Add(vtk${suffix}
    SOURCE_DIR "${vtksource_dir}"
    DOWNLOAD_COMMAND ""
    UPDATE_COMMAND ""
    CMAKE_ARGS
      -DBUILD_SHARED_LIBS:BOOL=${BUILD_SHARED_LIBS}
      -DCMAKE_INSTALL_PREFIX:PATH=<INSTALL_DIR>
      -DVTK_DATA_ROOT:PATH=${vtkdata_dir}
      -DVTK_DEBUG_LEAKS:BOOL=OFF
      -DVTK_USE_PARALLEL:BOOL=ON
    CMAKE_GENERATOR ${generator}
    INSTALL_COMMAND ""
    DEPENDS
      vtksource
  )
endfunction()

build_vtk("" "${CMAKE_GENERATOR}")

if(AVSB_build_x64_targets)
  build_vtk("_x64" "${CMAKE_GENERATOR} Win64")
endif()


##############################################################################
# avdn

get_filename_component(dir "${CMAKE_CURRENT_LIST_FILE}" PATH)
get_filename_component(avdn_source_dir "${dir}" PATH)
message(STATUS "avdn_source_dir='${avdn_source_dir}'")

set(avdn_keyfile_args "")
if(AVSB_SNKEYFILE)
  set(avdn_keyfile_args "-DAVDN_SNKEYFILE:FILEPATH=${AVSB_SNKEYFILE}")
endif()

function(build_avdn suffix generator)
  ExternalProject_Get_Property(vtk${suffix} binary_dir)
  set(vtk_dir "${binary_dir}")

  ExternalProject_Add(avdn${suffix}
    DOWNLOAD_COMMAND ""
    UPDATE_COMMAND ""
    SOURCE_DIR "${avdn_source_dir}"
    CMAKE_ARGS
      -DAVDN_INSTALL_PREFIX:PATH=<INSTALL_DIR>
      ${avdn_keyfile_args}
      ${avdn_redist_args}
      -Dgccxml_EXECUTABLE:FILEPATH=${gccxml_exe}
      -DMummy_DIR:PATH=${mummy_dir}
      -DVTK_DIR:PATH=${vtk_dir}
    CMAKE_GENERATOR ${generator}
    INSTALL_COMMAND ""
    DEPENDS
      mummy
      vtk${suffix}
  )
endfunction()

build_avdn("" "${CMAKE_GENERATOR}")

if(AVSB_build_x64_targets)
  build_avdn("_x64" "${CMAKE_GENERATOR} Win64")
endif()


##############################################################################
# Installers

function(build_installer proj)
  ExternalProject_Get_Property(${proj} binary_dir)
  set(proj_dir "${binary_dir}")

  ExternalProject_Add(${proj}-installer
    BINARY_DIR "${proj_dir}"
    DOWNLOAD_COMMAND ""
    UPDATE_COMMAND ""
    CONFIGURE_COMMAND ""
    BUILD_COMMAND ${CMAKE_COMMAND}
      --build ${proj_dir} --config ${CMAKE_CFG_INTDIR} --target package
    INSTALL_COMMAND ${CMAKE_COMMAND} -E echo
      "The '${proj}' installer is now available in '${proj_dir}'"
    DEPENDS
      ${proj}
  )
endfunction()

build_installer(gccxml)
build_installer(mummy)

build_installer(avdn)

if(AVSB_build_x64_targets)
  build_installer(avdn_x64)
endif()


##############################################################################
# Tests

include(ProcessorCount)
ProcessorCount(num_procs)

function(add_test_suite proj)
  ExternalProject_Get_Property(${proj} binary_dir)
  set(dir "${binary_dir}")

  add_test(NAME ${proj}-tests
    COMMAND ${CMAKE_CTEST_COMMAND} -C $<CONFIGURATION> -j ${num_procs}
      -D ExperimentalTest --output-on-failure
    WORKING_DIRECTORY ${dir}
  )

  # Since we're using all processors to run the target test suite in "full
  # parallel," run this "test" (which is actually a full suite of tests for
  # the named target) by itself:

  set_property(TEST ${proj}-tests PROPERTY RUN_SERIAL ON)
endfunction()

add_test_suite(gccxml)
add_test_suite(mummy)
add_test_suite(vtk)
add_test_suite(avdn)

if(AVSB_build_x64_targets)
  add_test_suite(vtk_x64)
  add_test_suite(avdn_x64)
endif()


##############################################################################
# Log all variable values:

#message(STATUS "========== VARIABLES ==========")
#get_cmake_property(vs VARIABLES)
#foreach(v ${vs})
#  message(STATUS "${v}='${${v}}'")
#endforeach()
