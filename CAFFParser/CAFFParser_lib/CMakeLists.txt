project(CAFFParser_lib)

add_library(CAFFParser_lib STATIC caff.cpp caff.h ciff.cpp ciff.h parse_exception.h parse_utils.cpp parse_utils.h date_validator.cpp date_validator.h date.h)
