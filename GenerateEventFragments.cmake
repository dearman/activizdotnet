# If calling as a cmake -P script, you have to define these variables:
#
IF("${ActiVizDotNet_BINARY_DIR}" STREQUAL "")
  MESSAGE(FATAL_ERROR "error: ActiVizDotNet_BINARY_DIR is empty")
ENDIF("${ActiVizDotNet_BINARY_DIR}" STREQUAL "")

IF("${AVDN_VTK_SOURCE_DIR}" STREQUAL "")
  MESSAGE(FATAL_ERROR "error: AVDN_VTK_SOURCE_DIR is empty")
ENDIF("${AVDN_VTK_SOURCE_DIR}" STREQUAL "")


# What directory is this file in?
# (EventFragment.cs.in is expected to be in the same directory...)
#
GET_FILENAME_COMPONENT(gef_dir "${CMAKE_CURRENT_LIST_FILE}" PATH)


# Need this to enable CONFIGURE_FILE without error messages in CMake 2.4:
#
IF(NOT DEFINED CMAKE_BACKWARDS_COMPATIBILITY)
  SET(CMAKE_BACKWARDS_COMPATIBILITY ${CMAKE_MAJOR_VERSION}.${CMAKE_MINOR_VERSION})
ENDIF(NOT DEFINED CMAKE_BACKWARDS_COMPATIBILITY)


# Parse the enum in vtkCommand.h and compute the set of events that vtkObject
# supports so that we can generate C# event code to include in vtkObject's C#
# class definition.
#
FILE(READ "${AVDN_VTK_SOURCE_DIR}/Common/vtkCommand.h" AVDN_vtk_command_h)
STRING(REGEX REPLACE ";" "\\\\;" AVDN_vtk_command_h "${AVDN_vtk_command_h}")
STRING(REGEX REPLACE "\n" "E;" AVDN_vtk_command_h "${AVDN_vtk_command_h}")

SET(in_enum 0)
SET(event_enum_value 0)

FOREACH(line ${AVDN_vtk_command_h})
  IF(in_enum)
    IF("${line}" STREQUAL "  };E")
      SET(in_enum 0)
    ENDIF("${line}" STREQUAL "  };E")
  ENDIF(in_enum)

  IF(in_enum)
    IF(line MATCHES "^[\t ]*([^\t ]*)Event.*E$")
      STRING(REGEX REPLACE "[\t ]*([^\t ]*)Event.*E$" "\\1" event_basename "${line}")

      SET(explicit_event_enum_value "-8888")
      IF(line MATCHES "^.*${event_basename}Event.*=[\t ]*([0-9]+).*E$")
        STRING(REGEX REPLACE "^.*${event_basename}Event.*=[\t ]*([0-9]+).*E$" "\\1" explicit_event_enum_value "${line}")
        SET(event_enum_value ${explicit_event_enum_value})
      ENDIF(line MATCHES "^.*${event_basename}Event.*=[\t ]*([0-9]+).*E$")

      SET(event_name "${event_basename}Evt")
        # Evt == not "" and not "Event", (which would be the two preferred
        # suffixes, but they both result in name clashes between events and
        # methods...)

      CONFIGURE_FILE(
        "${gef_dir}/EventFragment.cs.in"
        "${ActiVizDotNet_BINARY_DIR}/csharp/EventFragment_${event_basename}.cs"
        @ONLY
      )

      MATH(EXPR event_enum_value "${event_enum_value} + 1")
    ENDIF(line MATCHES "^[\t ]*([^\t ]*)Event.*E$")
  ENDIF(in_enum)

  IF("${line}" STREQUAL "  enum EventIds {E")
    SET(in_enum 1)
  ENDIF("${line}" STREQUAL "  enum EventIds {E")
ENDFOREACH(line)


# If nothing got picked up parsing out the old style vtkCommand.h list, give
# it a whirl with the new style:
IF(event_enum_value EQUAL 0)

SET(in_event_list 0)
SET(event_enum_value 0)

FOREACH(line ${AVDN_vtk_command_h})
  IF(in_event_list)
    IF("${line}" STREQUAL "E")
      SET(in_event_list 0)
    ENDIF()
  ENDIF()

  IF(in_event_list)
    IF(line MATCHES "^[\t ]*_vtk_add_event\\([\t ]*([^\t ]*)Event[\t ]*\\).*E$")
      STRING(REGEX REPLACE
        "^[\t ]*_vtk_add_event\\([\t ]*([^\t ]*)Event[\t ]*\\).*E$" "\\1"
        event_basename "${line}")

      SET(event_name "${event_basename}Evt")
        # Evt == not "" and not "Event", (which would be the two preferred
        # suffixes, but they both result in name clashes between events and
        # methods...)

      CONFIGURE_FILE(
        "${gef_dir}/EventFragment.cs.in"
        "${ActiVizDotNet_BINARY_DIR}/csharp/EventFragment_${event_basename}.cs"
        @ONLY
      )

      MATH(EXPR event_enum_value "${event_enum_value} + 1")
    ENDIF()
  ENDIF()

  IF("${line}" STREQUAL "#define VTK_EVENT_TYPES \\E")
    SET(in_event_list 1)
  ENDIF()
ENDFOREACH()

# In addition to parsing the list, also add the two pre-defined ones that
# are not included in the list mechanism:
#
IF(NOT event_enum_value EQUAL 0)
  FOREACH(event_basename No User)
    SET(event_name "${event_basename}Evt")

    CONFIGURE_FILE(
      "${gef_dir}/EventFragment.cs.in"
      "${ActiVizDotNet_BINARY_DIR}/csharp/EventFragment_${event_basename}.cs"
      @ONLY
    )
  ENDFOREACH()
ENDIF()

ENDIF()


IF(event_enum_value EQUAL 0)
  MESSAGE(FATAL_ERROR "could not parse list of vtkCommand events from vtkCommand.h
need more code in file '${CMAKE_CURRENT_LIST_FILE}'")
ENDIF()

#MESSAGE(STATUS "event_enum_value='${event_enum_value}'")


# Touch the sentinel file to avoid re-running this script:
#
FILE(APPEND
  "${ActiVizDotNet_BINARY_DIR}/csharp/GenerateEventFragmentsSentinel.txt"
  "${CMAKE_CURRENT_LIST_FILE}\n"
)
