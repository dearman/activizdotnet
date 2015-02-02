macro(ADD_EXTERNAL_CSHARP_PROJECT aecp_name aecp_dir aecp_sln aecp_target_cpu aecp_out aecp_out_dir aecp_dependencies aecp_sources aecp_dependent_dlls)
  add_custom_command(
    OUTPUT "${aecp_out_dir}/${aecp_out}"
    DEPENDS ${aecp_dir}/${aecp_sln} ${aecp_sources} ${aecp_dependent_dlls}
    WORKING_DIRECTORY ${aecp_dir}
    COMMAND ${CMAKE_MAKE_PROGRAM}
    ARGS "${aecp_sln}" /Build "\"${CMAKE_CFG_INTDIR}|${aecp_target_cpu}\""
    )

  add_custom_target(
    "${aecp_name}" ALL
    DEPENDS "${aecp_out_dir}/${aecp_out}"
    )

  add_dependencies("${aecp_name}" ${aecp_dependencies})
endmacro()


macro(MODIFY_CSPROJFILE mc_csprojfile)
  execute_process(COMMAND ${CMAKE_COMMAND} -E copy
    "${mc_csprojfile}"
    "${mc_csprojfile}.backup"
    )

  # Read in the .csproj file:
  #
  file(READ "${mc_csprojfile}" contents)
  string(REGEX REPLACE ";" "\\\\;" contents "${contents}")
  string(REGEX REPLACE "\n" "E;" contents "${contents}")

  # Write a new empty file:
  #
  file(WRITE "${mc_csprojfile}.new" "")

  # Scan for "<OutputPath>.*</OutputPath>" matches:
  #
  foreach(line ${contents})
    if(line MATCHES "^(.*)<OutputPath>.*</OutputPath>(.*)E$")
      string(REGEX REPLACE "^(.*)<OutputPath>.*</OutputPath>(.*)E$" "\\1<OutputPath>..\\\\..\\\\..\\\\bin\\\\$\(Configuration\)</OutputPath>\\2" newline "${line}")
    else()
      string(REGEX REPLACE "^(.*)E$" "\\1" newline "${line}")
    endif()

    # Append to the new file:
    #
    file(APPEND "${mc_csprojfile}.new" "${newline}\n")
  endforeach()

  # Copy the new one on top of the original input (in-place replacement)
  # and then get rid of the new one:
  #
  execute_process(COMMAND ${CMAKE_COMMAND} -E copy
    "${mc_csprojfile}.new"
    "${mc_csprojfile}"
    )
  execute_process(COMMAND ${CMAKE_COMMAND} -E remove
    "${mc_csprojfile}.new"
    )
endmacro()


macro(ADD_EXAMPLE ae_name ae_dir ae_sln ae_output ae_sources)
  # Copy the whole example directory given to its parallel in the build tree:
  #
  execute_process(COMMAND ${CMAKE_COMMAND} -E copy_directory
    "${ActiVizDotNet_BINARY_DIR}/Examples/${ae_dir}"
    "${ActiVizDotNet_BINARY_DIR}/ExamplesBuild/${ae_dir}"
    )


  # ae_sources is a list of source files relative to
  # "${ActiVizDotNet_BINARY_DIR}/ExamplesBuild/${ae_dir}"
  #
  # Create a list of full path sources.
  #
  set(ae_full_sources "")
  foreach(s ${ae_sources})
    set(ae_full_sources ${ae_full_sources} "${ActiVizDotNet_BINARY_DIR}/ExamplesBuild/${ae_dir}/${s}")

    if(s MATCHES "\\.csproj$")
      message(STATUS "Modifying .csproj file: '${ActiVizDotNet_BINARY_DIR}/ExamplesBuild/${ae_dir}/${s}'")
      MODIFY_CSPROJFILE("${ActiVizDotNet_BINARY_DIR}/ExamplesBuild/${ae_dir}/${s}")
    endif()
  endforeach()

  # Upgrade the project to the latest version of visual studio
  #
  execute_process(COMMAND
    "${CMAKE_MAKE_PROGRAM}" "${ActiVizDotNet_BINARY_DIR}/ExamplesBuild/${ae_dir}/${ae_sln}" "/upgrade"
    )

  # Add a custom target that builds the example via its .sln file:
  #
  if(CMAKE_SIZEOF_VOID_P EQUAL 8)
    set(ae_target_cpu "x64")
  else()
    set(ae_target_cpu "x86")
  endif()

  ADD_EXTERNAL_CSHARP_PROJECT(
    "${ae_name}"
    "${ActiVizDotNet_BINARY_DIR}/ExamplesBuild/${ae_dir}"
    "${ae_sln}"
    "${ae_target_cpu}"
    "${ae_output}"
    "${EXECUTABLE_OUTPUT_PATH}/${CMAKE_CFG_INTDIR}"
    "Kitware.VTK"
    "${ae_full_sources}"
    "${EXECUTABLE_OUTPUT_PATH}/${CMAKE_CFG_INTDIR}/Kitware.VTK.dll"
    )
endmacro()


message(STATUS "info: '${CMAKE_CURRENT_LIST_FILE}' included...")


set(AVDN_BUILD_SLN_FILES OFF)


if(MSVC80 OR MSVC90)
  message(STATUS "info: MSVC80 OR MSVC90")
  set(AVDN_BUILD_SLN_FILES ON)
endif()


if(AVDN_BUILD_SLN_FILES)
  # Force a clean build of "${ActiVizDotNet_BINARY_DIR}/ExamplesBuild"
  # after every CMake configure for now:
  #
  file(REMOVE_RECURSE "${ActiVizDotNet_BINARY_DIR}/ExamplesBuild")

  set(EXAMPLES
    #"BoxWidget"
    #"CubeAxes"
    #"Decimate"
    #"DelMesh"
    #"Dialog"
    "EventMonitor"
    "FileTree"
    #"HelloVTKConsole" # can't be built by loop below: different set of files
    #"HelloVTKForm"    # can't be built by loop below: different set of files
    #"SpherePuzzle"
    #"Streamline"
    #"VolumeRendering"
    #"Wikipedia"
    )

  # As happy good luck would have it, the source list for both of the uncommented
  # examples that build here is exactly the same:
  #
  foreach(ex ${EXAMPLES})
    set(s
      "Form1.cs"
      "Form1.Designer.cs"
      "Form1.resx"
      "Program.cs"
      "Properties/AssemblyInfo.cs"
      "Properties/Resources.Designer.cs"
      "Properties/Resources.resx"
      "Properties/Settings.Designer.cs"
      "Properties/Settings.settings"
      "${ex}.csproj"
      "${ex}.sln"
      )

    ADD_EXAMPLE("Build.Examples.${ex}" "${ex}/CS" "${ex}.sln" "${ex}.exe" "${s}")
  endforeach()

endif()
