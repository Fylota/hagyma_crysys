project(CAFFParser_lib)

if (NOT DEFINED CAFF_PARSER_LIBRARY_STATIC)
    set(CAFF_PARSER_LIBRARY_STATIC FALSE)
endif()

set(CAFF_PARSER_LIBRARY_SOURCES
        caff.cpp
        caff.h
        ciff.cpp
        ciff.h
        parse_exception.h
        parse_utils.cpp
        parse_utils.h
        date_validator.cpp
        date_validator.h
        date.h
        library.h
)

if (CAFF_PARSER_LIBRARY_STATIC)
    add_library(CAFFParser_lib STATIC ${CAFF_PARSER_LIBRARY_SOURCES})

    target_compile_definitions(CAFFParser_lib
            PUBLIC
                CAFF_PARSER_LIBRARY_STATIC
    )
else()
    add_library(CAFFParser_lib SHARED ${CAFF_PARSER_LIBRARY_SOURCES})

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