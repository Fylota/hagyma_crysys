project(Tests)

add_subdirectory(lib)
include_directories(${gtest_SOURCE_DIR}/include ${gtest_SOURCE_DIR})

add_executable(Tests_run ciff_test.cpp caff_test.cpp file_reader.h file_reader.cpp)
target_link_libraries(Tests_run gtest gtest_main)
target_link_libraries(Tests_run CAFFParser_lib)