# CMake -P script that builds all examples in the ActiViz .NET install directory

IF(NOT DEVENV_EXE)
  SET(DEVENV_EXE "C:/Program Files (x86)/Microsoft Visual Studio 8/Common7/IDE/devenv.com")
ENDIF(NOT DEVENV_EXE)

IF(NOT INSTALL_DIR)
  SET(INSTALL_DIR "C:/Documents and Settings/david/My Documents/ActiViz.NET 5.2.0")
ENDIF(NOT INSTALL_DIR)

IF(NOT CPU_TYPE)
  SET(CPU_TYPE "x64")
ENDIF(NOT CPU_TYPE)

FILE(GLOB EXAMPLE_NAMES ${INSTALL_DIR}/Examples/*)

SET(upgrade_error_count 0)
SET(build_error_count 0)
SET(build_count 0)

FOREACH(EXAMPLE_PATH ${EXAMPLE_NAMES})
  GET_FILENAME_COMPONENT(EXAMPLE ${EXAMPLE_PATH} NAME)

  FOREACH(LANG CS VB)
  FOREACH(CONFIG Debug Release)
    SET(PROJECT_PATH "${INSTALL_DIR}/Examples/${EXAMPLE}/${LANG}")
    SET(SLN "${EXAMPLE}.sln")

    MESSAGE("-- Upgrading and building ${EXAMPLE}-${LANG}-${CONFIG}")

    EXECUTE_PROCESS(
      WORKING_DIRECTORY ${PROJECT_PATH}
      COMMAND ${DEVENV_EXE} ${SLN} /Upgrade
      RESULT_VARIABLE rvUpgrade
      )
    IF(NOT rvUpgrade EQUAL 0)
      MATH(EXPR upgrade_error_count "${upgrade_error_count} + 1")
    ENDIF(NOT rvUpgrade EQUAL 0)

    EXECUTE_PROCESS(
      WORKING_DIRECTORY ${PROJECT_PATH}
      COMMAND ${DEVENV_EXE} ${SLN} /Build "${CONFIG}|${CPU_TYPE}"
      RESULT_VARIABLE rvBuild
      )
    IF(NOT rvBuild EQUAL 0)
      MATH(EXPR build_error_count "${build_error_count} + 1")
    ENDIF(NOT rvBuild EQUAL 0)

    MATH(EXPR build_count "${build_count} + 1")
  ENDFOREACH(CONFIG)
  ENDFOREACH(LANG)
ENDFOREACH(EXAMPLE_PATH)

MESSAGE("")
MESSAGE("info: build_count='${build_count}'")
MESSAGE("")
MESSAGE("")

IF(NOT upgrade_error_count EQUAL 0)
  MESSAGE(FATAL_ERROR "error: upgrade_error_count='${upgrade_error_count}'")
ENDIF(NOT upgrade_error_count EQUAL 0)

IF(NOT build_error_count EQUAL 0)
  MESSAGE(FATAL_ERROR "error: build_error_count='${build_error_count}'")
ENDIF(NOT build_error_count EQUAL 0)
