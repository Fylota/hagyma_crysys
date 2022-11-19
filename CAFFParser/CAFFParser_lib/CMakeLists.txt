cmake_minimum_required(VERSION 3.20)
project(CAFFParser_lib)

set(CMAKE_CXX_STANDARD 14)

if (NOT DEFINED CAFF_PARSER_LIBRARY_STATIC)
    set(CAFF_PARSER_LIBRARY_STATIC FALSE)
endif()

if (NOT DEFINED CAFF_PARSER_LIBRARY_SWIG)
    set(CAFF_PARSER_LIBRARY_SWIG FALSE)
endif()

set(CAFF_PARSER_LIBRARY_SOURCES
        caff.cpp
        ciff.cpp
        parse_utils.cpp
        date_validator.cpp
)

if (CAFF_PARSER_LIBRARY_SWIG)
    if (MSVC)
        if (NOT DEFINED ENV{SWIG_DIR} OR NOT DEFINED ENV{SWIG_EXECUTABLE})
            message( FATAL_ERROR "SWIG_DIR or SWIG_EXECUTABLE env variable not set!" )
        endif()
        set(SWIG_DIR $ENV{SWIG_DIR})
        set(SWIG_EXECUTABLE $ENV{SWIG_EXECUTABLE})
    endif(MSVC)

    find_package(SWIG REQUIRED)
    include(${SWIG_USE_FILE})

    set(NET_PROJECT_DIR "${CMAKE_CURRENT_SOURCE_DIR}/../../Backend/Backend")

    set(CMAKE_SWIG_FLAGS "")

    set_property(SOURCE caff_parser.i PROPERTY CPLUSPLUS ON)
    set_property(SOURCE caff_parser.i PROPERTY COMPILE_OPTIONS
        -namespace Backend.CAFFParser
    )
    set_source_files_properties(caff_parser.i PROPERTIES SWIG_FLAGS "-includeall")

    swig_add_library(CAFFParser_lib
            TYPE SHARED
            LANGUAGE CSharp
            SOURCES caff_parser.i ${CAFF_PARSER_LIBRARY_SOURCES}
            OUTPUT_DIR ${NET_PROJECT_DIR}/CAFFParser
            OUTFILE_DIR ${NET_PROJECT_DIR}/CAFFParser
    )

    target_compile_definitions(CAFFParser_lib
            PRIVATE
                CAFFPARSER_LIBRARY_BUILD
    )

    set_target_properties(CAFFParser_lib
            PROPERTIES
            # These copy animal.dll on Windows to .Net project directory
            RUNTIME_OUTPUT_DIRECTORY_RELEASE ${NET_PROJECT_DIR}
            RUNTIME_OUTPUT_DIRECTORY_DEBUG ${NET_PROJECT_DIR}

            # This copies animal.so on Linux to .Net project directory
            LIBRARY_OUTPUT_DIRECTORY ${NET_PROJECT_DIR}

            # Set address of C++ headers
            INCLUDE_DIRECTORIES ${CMAKE_CURRENT_SOURCE_DIR}
    )
else()
    set(CAFF_PARSER_LIBRARY_HEADERS
        caff.h
        ciff.h
        parse_exception.h
        parse_utils.h
        date_validator.h
        date_time.h
        library.h
        endianess.h
    )

    if (CAFF_PARSER_LIBRARY_STATIC)
        add_library(CAFFParser_lib STATIC ${CAFF_PARSER_LIBRARY_SOURCES} ${CAFF_PARSER_LIBRARY_HEADERS})

        target_compile_definitions(CAFFParser_lib
                PUBLIC
                    CAFF_PARSER_LIBRARY_STATIC
        )
    else()
        add_library(CAFFParser_lib SHARED ${CAFF_PARSER_LIBRARY_SOURCES} ${CAFF_PARSER_LIBRARY_HEADERS})

        set_target_properties(CAFFParser_lib
                PROPERTIES
                    C_VISIBILITY_PRESET hidden
                    CXX_VISIBILITY_PRESET hidden
        )

        if (WIN_32)
            add_custom_command(
                    TARGET CAFFParser_lib POST_BUILD
                    COMMAND ${CMAKE_COMMAND} -E copy_if_different
                    ${CMAKE_BINARY_DIR}/CAFFParser_lib/libCAFFParser_lib.dll ${CMAKE_BINARY_DIR}
                    COMMAND ${CMAKE_COMMAND} -E copy_if_different
                    ${CMAKE_BINARY_DIR}/CAFFParser_lib/libCAFFParser_lib.dll ${CMAKE_BINARY_DIR}
            )
        endif()
    endif()

    target_compile_definitions(CAFFParser_lib
            PRIVATE
                CAFFPARSER_LIBRARY_BUILD
    )

    target_include_directories(CAFFParser_lib
            INTERFACE
                ${CMAKE_CURRENT_SOURCE_DIR}
    )
endif()