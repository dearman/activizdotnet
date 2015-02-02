# If calling as a cmake -P script, you have to define these variables:
#
if("${ActiVizDotNet_BINARY_DIR}" STREQUAL "")
  message(FATAL_ERROR "error: ActiVizDotNet_BINARY_DIR is empty")
endif()

if("${aes_class}" STREQUAL "")
  message(FATAL_ERROR "error: aes_class is empty")
endif()


# What directory is this file in?
# (${aes_class}_EventFragments.cmake is expected to be in the same directory...)
#
get_filename_component(aes_dir "${CMAKE_CURRENT_LIST_FILE}" PATH)


# Need this to enable CONFIGURE_FILE without error messages in CMake 2.4:
#
if(NOT DEFINED CMAKE_BACKWARDS_COMPATIBILITY)
  set(CMAKE_BACKWARDS_COMPATIBILITY ${CMAKE_MAJOR_VERSION}.${CMAKE_MINOR_VERSION})
endif()


# Build ${aes_class}_EventFragments_CODE from each fragment file before
# configuring the Extra.cs file at the end...
#
set(${aes_class}_EventFragments_CODE "")
set(${aes_class}_RemoveEvents_CODE "")

include("${aes_dir}/${aes_class}_EventFragments.cmake")

foreach(fragment ${${aes_class}_EventFragments})
  file(READ "${ActiVizDotNet_BINARY_DIR}/csharp/EventFragment_${fragment}.cs" AVDN_fragment_cs)
  string(REGEX REPLACE ";" "\\\\;" AVDN_fragment_cs "${AVDN_fragment_cs}")
  string(REGEX REPLACE "\n" "E;" AVDN_fragment_cs "${AVDN_fragment_cs}")

  foreach(line ${AVDN_fragment_cs})
    string(REGEX REPLACE "^(.*)E$" "\\1" actual_line "${line}")
    set(${aes_class}_EventFragments_CODE "${${aes_class}_EventFragments_CODE}${actual_line}\n")
  endforeach()
  set(${aes_class}_EventFragments_CODE "${${aes_class}_EventFragments_CODE}\n\n")

  set(actual_line "    if (null != this.${fragment}EvtRelay)\n    {\n      this.${fragment}EvtRelay.RemoveAllHandlers();\n      this.${fragment}EvtRelay = null;\n    }\n")
  set(${aes_class}_RemoveEvents_CODE "${${aes_class}_RemoveEvents_CODE}${actual_line}\n")
endforeach()

configure_file(
  "${aes_dir}/${aes_class}_Extra.cs.in"
  "${ActiVizDotNet_BINARY_DIR}/csharp/${aes_class}_Extra.cs"
  @ONLY
)


# Touch the sentinel file to avoid re-running this script:
#
file(APPEND
  "${ActiVizDotNet_BINARY_DIR}/csharp/AssembleExtraSourcesSentinel.txt"
  "${CMAKE_CURRENT_LIST_FILE}\n"
)
