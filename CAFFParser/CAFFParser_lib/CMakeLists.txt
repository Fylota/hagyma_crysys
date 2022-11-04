project(CAFFParser_lib)

add_library(CAFFParser_lib STATIC library.cpp CAFF.cpp CAFF.h CIFF.cpp CIFF.h parse_exception.h)
