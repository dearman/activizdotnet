# If calling as a cmake -P script, you have to define these variables:
#
if("${ActiVizDotNet_BINARY_DIR}" STREQUAL "")
  message(FATAL_ERROR "error: ActiVizDotNet_BINARY_DIR is empty")
endif()

if("${AVDN_VTK_SOURCE_DIR}" STREQUAL "")
  message(FATAL_ERROR "error: AVDN_VTK_SOURCE_DIR is empty")
endif()


# What directory is this file in?
# (EventFragment.cs.in is expected to be in the same directory...)
#
get_filename_component(gef_dir "${CMAKE_CURRENT_LIST_FILE}" PATH)


# Need this to enable CONFIGURE_FILE without error messages in CMake 2.4:
#
if(NOT DEFINED CMAKE_BACKWARDS_COMPATIBILITY)
  set(CMAKE_BACKWARDS_COMPATIBILITY ${CMAKE_MAJOR_VERSION}.${CMAKE_MINOR_VERSION})
endif()


# Parse the enum in vtkCommand.h and compute the set of events that vtkObject
# supports so that we can generate C# event code to include in vtkObject's C#
# class definition.
#
file(READ "${AVDN_VTK_SOURCE_DIR}/Common/Core/vtkCommand.h" AVDN_vtk_command_h)
string(REGEX REPLACE ";" "\\\\;" AVDN_vtk_command_h "${AVDN_vtk_command_h}")
string(REGEX REPLACE "\n" "E;" AVDN_vtk_command_h "${AVDN_vtk_command_h}")

set(in_enum 0)
set(event_enum_value 0)

foreach(line ${AVDN_vtk_command_h})
  if(in_enum)
    if("${line}" STREQUAL "  };E")
      set(in_enum 0)
    endif()
  endif()

  if(in_enum)
    if(line MATCHES "^[\t ]*([^\t ]*)Event.*E$")
      string(REGEX REPLACE "[\t ]*([^\t ]*)Event.*E$" "\\1" event_basename "${line}")

      set(explicit_event_enum_value "-8888")
      if(line MATCHES "^.*${event_basename}Event.*=[\t ]*([0-9]+).*E$")
        string(REGEX REPLACE "^.*${event_basename}Event.*=[\t ]*([0-9]+).*E$" "\\1" explicit_event_enum_value "${line}")
        set(event_enum_value ${explicit_event_enum_value})
      endif()

      set(event_name "${event_basename}Evt")
        # Evt == not "" and not "Event", (which would be the two preferred
        # suffixes, but they both result in name clashes between events and
        # methods...)

      configure_file(
        "${gef_dir}/EventFragment.cs.in"
        "${ActiVizDotNet_BINARY_DIR}/csharp/EventFragment_${event_basename}.cs"
        @ONLY
      )

      math(EXPR event_enum_value "${event_enum_value} + 1")
    endif()
  endif()

  if("${line}" STREQUAL "  enum EventIds {E")
    set(in_enum 1)
  endif()
endforeach()


# If nothing got picked up parsing out the old style vtkCommand.h list, give
# it a whirl with the new style:
if(event_enum_value EQUAL 0)

set(in_event_list 0)
set(event_enum_value 0)

foreach(line ${AVDN_vtk_command_h})
  if(in_event_list)
    if("${line}" STREQUAL "E")
      set(in_event_list 0)
    endif()
  endif()

  if(in_event_list)
    if(line MATCHES "^[\t ]*_vtk_add_event\\([\t ]*([^\t ]*)Event[\t ]*\\).*E$")
      string(REGEX REPLACE
        "^[\t ]*_vtk_add_event\\([\t ]*([^\t ]*)Event[\t ]*\\).*E$" "\\1"
        event_basename "${line}")

      set(event_name "${event_basename}Evt")
        # Evt == not "" and not "Event", (which would be the two preferred
        # suffixes, but they both result in name clashes between events and
        # methods...)

      configure_file(
        "${gef_dir}/EventFragment.cs.in"
        "${ActiVizDotNet_BINARY_DIR}/csharp/EventFragment_${event_basename}.cs"
        @ONLY
      )

      math(EXPR event_enum_value "${event_enum_value} + 1")
    endif()
  endif()

  if("${line}" STREQUAL "#define VTK_EVENT_TYPES \\E")
    set(in_event_list 1)
  endif()

  # Sometime between 5.8 and 6.0, it changed to this line:
  if("${line}" STREQUAL "#define vtkAllEventsMacro() \\E")
    set(in_event_list 1)
  endif()
endforeach()

# In addition to parsing the list, also add the two pre-defined ones that
# are not included in the list mechanism:
#
if(NOT event_enum_value EQUAL 0)
  foreach(event_basename No User)
    set(event_name "${event_basename}Evt")

    configure_file(
      "${gef_dir}/EventFragment.cs.in"
      "${ActiVizDotNet_BINARY_DIR}/csharp/EventFragment_${event_basename}.cs"
      @ONLY
    )
  endforeach()
endif()

endif()


if(event_enum_value EQUAL 0)
  message(FATAL_ERROR "could not parse list of vtkCommand events from vtkCommand.h
need more code in file '${CMAKE_CURRENT_LIST_FILE}'")
endif()

#message(STATUS "event_enum_value='${event_enum_value}'")


# Touch the sentinel file to avoid re-running this script:
#
file(APPEND
  "${ActiVizDotNet_BINARY_DIR}/csharp/GenerateEventFragmentsSentinel.txt"
  "${CMAKE_CURRENT_LIST_FILE}\n"
)
