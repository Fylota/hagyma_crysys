LIB             = libcaff_parser.a
DLIB            = libcaff_parser.so
CLI             = caff_run

SRCS            = CAFFParser_lib/caff.cpp CAFFParser_lib/ciff.cpp CAFFParser_lib/parse_utils.cpp CAFFParser_lib/date_validator.cpp main.cpp
HDRS            = CAFFParser_lib/caff.h CAFFParser_lib/ciff.h CAFFParser_lib/parse_exception.h CAFFParser_lib/parse_utils.h CAFFParser_lib/date_validator.h CAFFParser_lib/date_time.h CAFFParser_lib/library.h CAFFParser_lib/endianess.h
OBJS            = ${SRCS:.cpp=.o}

CXXFLAGS         += -O2 -pipe -pedantic -std=c++14
CXXFLAGS         += -Wall -Wextra
CXXFLAGS         += -Wmissing-declarations
CXXFLAGS         += -Wshadow -Wpointer-arith
CXXFLAGS         += -Wsign-compare -Wcast-qual

CXXFLAGS         += -fPIC

CXXFLAGS         += -DMAGICKCORE_QUANTUM_DEPTH=16
CXXFLAGS         += -DMAGICKCORE_HDRI_ENABLE=0

LDFLAGS        += ${LIBS}

PREFIX         ?= /usr/local


all: ${LIB} ${DLIB} ${CLI}

${LIB}: ${OBJS}
	${AR} rcs ${LIB} ${OBJS}

${DLIB}: ${OBJS}
	${CC} -shared -o ${DLIB} ${CXXFLAGS} ${OBJS}

${CLI}: ${OBJS}
	${CC} -o ${CLI} ${CXXFLAGS} ${OBJS} ${LDFLAGS}

install-lib: ${LIB}
	install ${LIB} ${PREFIX}/lib/
	install ${DLIB} ${PREFIX}/lib/
	install ${HDRS} ${PREFIX}/include/

install-cli: ${CLI}
	install ${CLI} ${PREFIX}/bin/

deinstall-lib:
	rm -f ${PREFIX}/lib/${LIB} ${PREFIX}/lib/${DLIB}
	@sh -xc \
	    'for h in ${HDRS}; do rm -f "${PREFIX}/include/$$h"; done'

deinstall-cli:
	rm -f ${PREFIX}/bin/${CLI}


install: install-lib install-cli

deinstall: deinstall-lib deinstall-cli

clean:
	rm -f ${LIB} ${CLI} ${OBJS}

.PHONY: clean
.PHONY: install deinstall
.PHONY: install-lib deinstall-lib
.PHONY: install-cli deinstall-cli