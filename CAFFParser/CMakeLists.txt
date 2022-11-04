cmake_minimum_required(VERSION 3.23)
project(CAFFParser)

set(CMAKE_CXX_STANDARD 14)

set(SOURCE_FILES main.cpp)

include_directories(CAFFParser_lib)
add_subdirectory(CAFFParser_lib)

add_executable(CAFFParser_run ${SOURCE_FILES})
target_link_libraries(CAFFParser_run CAFFParser_lib)