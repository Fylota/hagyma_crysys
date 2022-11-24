cmake_minimum_required(VERSION 3.20)
project(CAFFParser)

set(CMAKE_CXX_STANDARD 14)

option(CAFF_PARSER_LIBRARY_STATIC "Compile CAFFParser_lib as static library" ON)
option(CAFF_PARSER_LIBRARY_SWIG "Compile only CAFFParser_lib as SWIG library" OFF)

if (CAFF_PARSER_LIBRARY_SWIG)
	add_subdirectory(CAFFParser_lib)
else()
	set(SOURCE_FILES main.cpp)

	include_directories(CAFFParser_lib)
	add_subdirectory(CAFFParser_lib)

	if (CAFF_PARSER_LIBRARY_STATIC)
		add_subdirectory(Tests)
	endif()

	add_executable(CAFFParser_run ${SOURCE_FILES})
	target_link_libraries(CAFFParser_run PRIVATE CAFFParser_lib)
endif()