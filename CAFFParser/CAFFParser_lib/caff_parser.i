%module CAFFParser

%include <std_string.i>
%include <typemaps.i>
%include <std_vector.i>
%include <std_pair.i>
%include <stdint.i>

%include ciff.i
%include date_time.i
%include caff.i
%include endianess.i

%apply const std::string & {std::string &};
%template(BytesVector) std::vector<uint8_t>;
%template(StringVector) std::vector<std::string>;
%feature("valuewrapper") std::pair<int64_t, CIFF>;
%ignore std::pair<int64_t, CIFF>::pair();
%template(CIFFAndDuration) std::pair<int64_t, CIFF>;
%template(CIFFAndDurationVector) std::vector<std::pair<int64_t, CIFF>>;
