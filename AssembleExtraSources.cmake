# If calling as a cmake -P script, you have to define these variables:
#
IF("${ActiVizDotNet_BINARY_DIR}" STREQUAL "")
  MESSAGE(FATAL_ERROR "error: ActiVizDotNet_BINARY_DIR is empty")
ENDIF("${ActiVizDotNet_BINARY_DIR}" STREQUAL "")

IF("${aes_class}" STREQUAL "")
  MESSAGE(FATAL_ERROR "error: aes_class is empty")
ENDIF("${aes_class}" STREQUAL "")


# What directory is this file in?
# (${aes_class}_EventFragments.cmake is expected to be in the same directory...)
#
GET_FILENAME_COMPONENT(aes_dir "${CMAKE_CURRENT_LIST_FILE}" PATH)


# Need this to enable CONFIGURE_FILE without error messages in CMake 2.4:
#
IF(NOT DEFINED CMAKE_BACKWARDS_COMPATIBILITY)
  SET(CMAKE_BACKWARDS_COMPATIBILITY ${CMAKE_MAJOR_VERSION}.${CMAKE_MINOR_VERSION})
ENDIF(NOT DEFINED CMAKE_BACKWARDS_COMPATIBILITY)


# Build ${aes_class}_EventFragments_CODE from each fragment file before
# configuring the Extra.cs file at the end...
#
SET(${aes_class}_EventFragments_CODE "")
SET(${aes_class}_RemoveEvents_CODE "")

INCLUDE("${aes_dir}/${aes_class}_EventFragments.cmake")

FOREACH(fragment ${${aes_class}_EventFragments})
  FILE(READ "${ActiVizDotNet_BINARY_DIR}/csharp/EventFragment_${fragment}.cs" AVDN_fragment_cs)
  STRING(REGEX REPLACE ";" "\\\\;" AVDN_fragment_cs "${AVDN_fragment_cs}")
  STRING(REGEX REPLACE "\n" "E;" AVDN_fragment_cs "${AVDN_fragment_cs}")

  FOREACH(line ${AVDN_fragment_cs})
    STRING(REGEX REPLACE "^(.*)E$" "\\1" actual_line "${line}")
    SET(${aes_class}_EventFragments_CODE "${${aes_class}_EventFragments_CODE}${actual_line}\n")
  ENDFOREACH(line)
  SET(${aes_class}_EventFragments_CODE "${${aes_class}_EventFragments_CODE}\n\n")

  SET(actual_line "    if (null != this.${fragment}EvtRelay)\n    {\n      this.${fragment}EvtRelay.RemoveAllHandlers();\n      this.${fragment}EvtRelay = null;\n    }\n")
  SET(${aes_class}_RemoveEvents_CODE "${${aes_class}_RemoveEvents_CODE}${actual_line}\n")
ENDFOREACH(fragment)

CONFIGURE_FILE(
  "${aes_dir}/${aes_class}_Extra.cs.in"
  "${ActiVizDotNet_BINARY_DIR}/csharp/${aes_class}_Extra.cs"
  @ONLY
)


# Touch the sentinel file to avoid re-running this script:
#
FILE(APPEND
  "${ActiVizDotNet_BINARY_DIR}/csharp/AssembleExtraSourcesSentinel.txt"
  "${CMAKE_CURRENT_LIST_FILE}\n"
)
