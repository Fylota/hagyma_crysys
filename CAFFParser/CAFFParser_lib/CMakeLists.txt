project(CAFFParser_lib)

add_library(CAFFParser_lib STATIC CAFF.cpp CAFF.h CIFF.cpp CIFF.h parse_exception.h ParseUtils.cpp ParseUtils.h DateValidator.cpp DateValidator.h Date.h)
