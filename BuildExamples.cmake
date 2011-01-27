MACRO(ADD_EXTERNAL_CSHARP_PROJECT aecp_name aecp_dir aecp_sln aecp_target_cpu aecp_out aecp_out_dir aecp_dependencies aecp_sources aecp_dependent_dlls)
  ADD_CUSTOM_COMMAND(
    OUTPUT "${aecp_out_dir}/${aecp_out}"
    DEPENDS ${aecp_dir}/${aecp_sln} ${aecp_sources} ${aecp_dependent_dlls}
    WORKING_DIRECTORY ${aecp_dir}
    COMMAND ${CMAKE_MAKE_PROGRAM}
    ARGS "${aecp_sln}" /Build "\"${CMAKE_CFG_INTDIR}|${aecp_target_cpu}\""
    )

  ADD_CUSTOM_TARGET(
    "${aecp_name}" ALL
    DEPENDS "${aecp_out_dir}/${aecp_out}"
    )

  ADD_DEPENDENCIES("${aecp_name}" ${aecp_dependencies})
ENDMACRO(ADD_EXTERNAL_CSHARP_PROJECT)


MACRO(MODIFY_CSPROJFILE mc_csprojfile)
  EXECUTE_PROCESS(COMMAND ${CMAKE_COMMAND} -E copy
    "${mc_csprojfile}"
    "${mc_csprojfile}.backup"
    )

  # Read in the .csproj file:
  #
  FILE(READ "${mc_csprojfile}" contents)
  STRING(REGEX REPLACE ";" "\\\\;" contents "${contents}")
  STRING(REGEX REPLACE "\n" "E;" contents "${contents}")

  # Write a new empty file:
  #
  FILE(WRITE "${mc_csprojfile}.new" "")

  # Scan for "<OutputPath>.*</OutputPath>" matches:
  #
  FOREACH(line ${contents})
    IF(line MATCHES "^(.*)<OutputPath>.*</OutputPath>(.*)E$")
      STRING(REGEX REPLACE "^(.*)<OutputPath>.*</OutputPath>(.*)E$" "\\1<OutputPath>..\\\\..\\\\..\\\\bin\\\\$\(Configuration\)</OutputPath>\\2" newline "${line}")
    ELSE(line MATCHES "^(.*)<OutputPath>.*</OutputPath>(.*)E$")
      STRING(REGEX REPLACE "^(.*)E$" "\\1" newline "${line}")
    ENDIF(line MATCHES "^(.*)<OutputPath>.*</OutputPath>(.*)E$")

    # Append to the new file:
    #
    FILE(APPEND "${mc_csprojfile}.new" "${newline}\n")
  ENDFOREACH(line)

  # Copy the new one on top of the original input (in-place replacement)
  # and then get rid of the new one:
  #
  EXECUTE_PROCESS(COMMAND ${CMAKE_COMMAND} -E copy
    "${mc_csprojfile}.new"
    "${mc_csprojfile}"
    )
  EXECUTE_PROCESS(COMMAND ${CMAKE_COMMAND} -E remove
    "${mc_csprojfile}.new"
    )
ENDMACRO(MODIFY_CSPROJFILE)


MACRO(ADD_EXAMPLE ae_name ae_dir ae_sln ae_output ae_sources)
  # Copy the whole example directory given to its parallel in the build tree:
  #
  EXECUTE_PROCESS(COMMAND ${CMAKE_COMMAND} -E copy_directory
    "${ActiVizDotNet_BINARY_DIR}/Examples/${ae_dir}"
    "${ActiVizDotNet_BINARY_DIR}/ExamplesBuild/${ae_dir}"
    )


  # ae_sources is a list of source files relative to
  # "${ActiVizDotNet_BINARY_DIR}/ExamplesBuild/${ae_dir}"
  #
  # Create a list of full path sources.
  #
  SET(ae_full_sources "")
  FOREACH(s ${ae_sources})
    SET(ae_full_sources ${ae_full_sources} "${ActiVizDotNet_BINARY_DIR}/ExamplesBuild/${ae_dir}/${s}")

    IF(s MATCHES "\\.csproj$")
      MESSAGE(STATUS "Modifying .csproj file: '${ActiVizDotNet_BINARY_DIR}/ExamplesBuild/${ae_dir}/${s}'")
      MODIFY_CSPROJFILE("${ActiVizDotNet_BINARY_DIR}/ExamplesBuild/${ae_dir}/${s}")
    ENDIF(s MATCHES "\\.csproj$")
  ENDFOREACH(s)

  # Upgrade the project to the latest version of visual studio
  #
  EXECUTE_PROCESS(COMMAND 
    "${CMAKE_MAKE_PROGRAM}" "${ActiVizDotNet_BINARY_DIR}/ExamplesBuild/${ae_dir}/${ae_sln}" "/upgrade"
    )

  # Add a custom target that builds the example via its .sln file:
  #
  IF(CMAKE_SIZEOF_VOID_P EQUAL 8)
    SET(ae_target_cpu "x64")
  ELSE(CMAKE_SIZEOF_VOID_P EQUAL 8)
    SET(ae_target_cpu "x86")
  ENDIF(CMAKE_SIZEOF_VOID_P EQUAL 8)

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
ENDMACRO(ADD_EXAMPLE)


MESSAGE(STATUS "info: '${CMAKE_CURRENT_LIST_FILE}' included...")


SET(AVDN_BUILD_SLN_FILES OFF)


IF(MSVC80 OR MSVC90)
  MESSAGE(STATUS "info: MSVC80 OR MSVC90")
  SET(AVDN_BUILD_SLN_FILES ON)
ENDIF(MSVC80 OR MSVC90)


IF(AVDN_BUILD_SLN_FILES)
  # Force a clean build of "${ActiVizDotNet_BINARY_DIR}/ExamplesBuild"
  # after every CMake configure for now:
  #
  FILE(REMOVE_RECURSE "${ActiVizDotNet_BINARY_DIR}/ExamplesBuild")

  SET(EXAMPLES
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
  FOREACH(ex ${EXAMPLES})
    SET(s
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
  ENDFOREACH(ex)

ENDIF(AVDN_BUILD_SLN_FILES)
