macro(APPEND_FLAGS var flags)
  if(NOT "${${var}}" MATCHES "${flags}")
    set(${var} "${${var}} ${flags}")
    #message(STATUS "INFO: ${var} changed to '${${var}}'")
  endif()
endmacro()


macro(REPLACE_FLAGS var these those)
  if("${${var}}" MATCHES "${these}")
    string(REGEX REPLACE "${these}" "${those}" ${var} "${${var}}")
    #message(STATUS "INFO: ${var} changed to '${${var}}'")
  endif()
endmacro()


macro(MSVC_FORCE_WARNING_LEVEL level)
  if(MSVC)
    foreach(lang C CXX)
    foreach(suffix "" _DEBUG _MINSIZEREL _RELEASE _RELWITHDEBINFO)
      REPLACE_FLAGS("CMAKE_${lang}_FLAGS${suffix}" "/W[1-4]" "/W${level}")
    endforeach()
    endforeach()
  endif()
endmacro()


macro(MSVC_TREAT_WARNINGS_AS_ERRORS)
  if(MSVC)
    APPEND_FLAGS("CMAKE_C_FLAGS" "/WX")
    APPEND_FLAGS("CMAKE_CXX_FLAGS" "/WX")
  endif()
endmacro()


macro(MSVC_LINK_TO_STATIC_CRT)
  if(MSVC)
    foreach(lang C CXX)
    foreach(suffix "" _DEBUG _MINSIZEREL _RELEASE _RELWITHDEBINFO)
      REPLACE_FLAGS("CMAKE_${lang}_FLAGS${suffix}" "/MD" "/MT")
    endforeach()
    endforeach()
  endif()
endmacro()


macro(MSVC_LINK_TO_DLL_CRT)
  if(MSVC)
    foreach(lang C CXX)
    foreach(suffix "" _DEBUG _MINSIZEREL _RELEASE _RELWITHDEBINFO)
      REPLACE_FLAGS("CMAKE_${lang}_FLAGS${suffix}" "/MT" "/MD")
    endforeach()
    endforeach()
  endif()
endmacro()


macro(MSVC80_FORCE_MANIFEST_LINKER_FLAG)
  if(MSVC80)
    APPEND_FLAGS("CMAKE_EXE_LINKER_FLAGS" "/MANIFEST")
    APPEND_FLAGS("CMAKE_MODULE_LINKER_FLAGS" "/MANIFEST")
    APPEND_FLAGS("CMAKE_SHARED_LINKER_FLAGS" "/MANIFEST")
  endif()
endmacro()


macro(MSVC80_SUPPRESS_CRT_DEPRECATED_WARNINGS)
  if(MSVC80)
    add_definitions("-D_CRT_NONSTDC_NO_DEPRECATE")
    add_definitions("-D_CRT_SECURE_NO_DEPRECATE")
    add_definitions("-D_SCL_SECURE_NO_DEPRECATE")
  endif()
endmacro()
