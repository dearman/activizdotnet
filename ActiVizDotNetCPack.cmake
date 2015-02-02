set(CPACK_PACKAGE_NAME "ActiViz.NET")
set(CPACK_PACKAGE_VERSION_MAJOR "${AVDN_MAJOR_VERSION}")
set(CPACK_PACKAGE_VERSION_MINOR "${AVDN_MINOR_VERSION}")
set(CPACK_PACKAGE_VERSION_PATCH "${AVDN_BUILD_VERSION}")
set(CPACK_PACKAGE_VERSION "${CPACK_PACKAGE_VERSION_MAJOR}.${CPACK_PACKAGE_VERSION_MINOR}.${CPACK_PACKAGE_VERSION_PATCH}")
set(CPACK_PACKAGE_INSTALL_DIRECTORY "${CPACK_PACKAGE_NAME} ${CPACK_PACKAGE_VERSION} ${AVDN_EDITION} Edition")
set(CPACK_PACKAGE_VENDOR "Kitware SAS")
set(CPACK_PACKAGE_DESCRIPTION_FILE "${ActiVizDotNet_SOURCE_DIR}/License${AVDN_EDITION_SUFFIX}.txt")
set(CPACK_RESOURCE_FILE_LICENSE "${ActiVizDotNet_SOURCE_DIR}/License${AVDN_EDITION_SUFFIX}.txt")

set(CPACK_SOURCE_PACKAGE_FILE_NAME
  "${CPACK_PACKAGE_NAME}-${AVDN_MAJOR_VERSION}.${AVDN_MINOR_VERSION}.${AVDN_BUILD_VERSION}.${AVDN_VERSION_SVN_REVISION}")

# Installers for 32- vs. 64-bit:
#  - Root install directory (displayed to end user at installer-run time)
#  - "NSIS package/display name" (text used in the installer GUI)
#  - Registry key used to store info about the installation
#
# This bit requires CPack 2.8.4 or later, because CPACK_NSIS_INSTALL_ROOT is
# ignored by previous versions of CPack.
#
if(CMAKE_CL_64)
  set(CPACK_NSIS_INSTALL_ROOT "$PROGRAMFILES64")
  set(CPACK_NSIS_PACKAGE_NAME "${CPACK_PACKAGE_INSTALL_DIRECTORY} (Win64)")
  set(CPACK_NSIS_DISPLAY_NAME "${CPACK_PACKAGE_INSTALL_DIRECTORY} (Win64)")
  set(CPACK_PACKAGE_INSTALL_REGISTRY_KEY "${CPACK_PACKAGE_INSTALL_DIRECTORY} (Win64)")
else()
  set(CPACK_NSIS_INSTALL_ROOT "$PROGRAMFILES")
  set(CPACK_NSIS_PACKAGE_NAME "${CPACK_PACKAGE_INSTALL_DIRECTORY}")
  set(CPACK_NSIS_DISPLAY_NAME "${CPACK_PACKAGE_INSTALL_DIRECTORY}")
  set(CPACK_PACKAGE_INSTALL_REGISTRY_KEY "${CPACK_PACKAGE_INSTALL_DIRECTORY}")
endif()

set(CPACK_PACKAGE_DESCRIPTION_SUMMARY "${CPACK_NSIS_DISPLAY_NAME} - the scientific data visualization power of VTK harnessed for C#, VB.NET or any other .NET Framework language.")

if(NOT DEFINED CPACK_SYSTEM_NAME)
  set(CPACK_SYSTEM_NAME ${CMAKE_SYSTEM_NAME}-${CMAKE_SYSTEM_PROCESSOR})
endif()
if(CPACK_SYSTEM_NAME MATCHES Windows)
  if(CMAKE_CL_64)
    set(CPACK_SYSTEM_NAME win64)
  else()
    set(CPACK_SYSTEM_NAME win32)
  endif()
endif()

if(NOT DEFINED CPACK_PACKAGE_FILE_NAME)
  set(CPACK_PACKAGE_FILE_NAME "${CPACK_SOURCE_PACKAGE_FILE_NAME}-${CPACK_SYSTEM_NAME}-${AVDN_EDITION}")
endif()

if(WIN32)
  # CPACK_PACKAGE_ICON: the installer banner bitmap (*.bmp file)
  set(icon_file "${ActiVizDotNet_SOURCE_DIR}/Logos/activizinstall.bmp")
  # Use Windows style path separator in NSIS script:
  string(REGEX REPLACE "/" "\\\\\\\\" icon_file "${icon_file}")
  set(CPACK_PACKAGE_ICON "${icon_file}")

  # CPACK_NSIS_MUI_ICON: the installer executable icon (*.ico file)
  set(icon_file "${ActiVizDotNet_SOURCE_DIR}/Logos/icon128x128 install.ico")
  # Use Windows style path separator in NSIS script:
  string(REGEX REPLACE "/" "\\\\\\\\" icon_file "${icon_file}")
  set(CPACK_NSIS_MUI_ICON "${icon_file}")

  # CPACK_NSIS_MUI_UNIICON: the uninstaller executable icon (*.ico file)
  set(icon_file "${ActiVizDotNet_SOURCE_DIR}/Logos/icon128x128 uninstall.ico")
  # Use Windows style path separator in NSIS script:
  string(REGEX REPLACE "/" "\\\\\\\\" icon_file "${icon_file}")
  set(CPACK_NSIS_MUI_UNIICON "${icon_file}")

  set(CPACK_NSIS_HELP_LINK "http://www.kitware.com/products/activiz.html")
  set(CPACK_NSIS_URL_INFO_ABOUT "http://www.kitware.com")
  set(CPACK_NSIS_CONTACT "kitware@kitware.com")

  set(CPACK_NSIS_EXTRA_INSTALL_COMMANDS "
  CreateShortCut \\\"$SMPROGRAMS\\\\$STARTMENU_FOLDER\\\\Examples.lnk\\\" \\\"$INSTDIR\\\\Examples\\\"
  CreateShortCut \\\"$SMPROGRAMS\\\\$STARTMENU_FOLDER\\\\License.lnk\\\" \\\"$INSTDIR\\\\License${AVDN_EDITION_SUFFIX}.txt\\\"
  CreateShortCut \\\"$SMPROGRAMS\\\\$STARTMENU_FOLDER\\\\Users Guide.lnk\\\" \\\"$INSTDIR\\\\UsersGuide.pdf\\\"
  CreateShortCut \\\"$SMPROGRAMS\\\\$STARTMENU_FOLDER\\\\Event Monitor.lnk\\\" \\\"$INSTDIR\\\\bin\\\\EventMonitor.exe\\\"
  CreateShortCut \\\"$SMPROGRAMS\\\\$STARTMENU_FOLDER\\\\File Browser.lnk\\\" \\\"$INSTDIR\\\\bin\\\\FileTree.exe\\\"
")

  set(CPACK_NSIS_EXTRA_UNINSTALL_COMMANDS "
  !insertmacro MUI_STARTMENU_GETFOLDER Application $MUI_TEMP
  Delete \\\"$SMPROGRAMS\\\\$MUI_TEMP\\\\File Browser.lnk\\\"
  Delete \\\"$SMPROGRAMS\\\\$MUI_TEMP\\\\Event Monitor.lnk\\\"
  Delete \\\"$SMPROGRAMS\\\\$MUI_TEMP\\\\Users Guide.lnk\\\"
  Delete \\\"$SMPROGRAMS\\\\$MUI_TEMP\\\\License.lnk\\\"
  Delete \\\"$SMPROGRAMS\\\\$MUI_TEMP\\\\Examples.lnk\\\"
")

  set(CPACK_GENERATOR "NSIS")
  set(CPACK_SOURCE_GENERATOR "${CPACK_GENERATOR}")
endif()

include(CPack)
