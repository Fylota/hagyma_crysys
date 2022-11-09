//
// Created by fupn26 on 2022.11.05..
//

#ifndef CAFFPARSER_PARSE_UTILS_H
#define CAFFPARSER_PARSE_UTILS_H


#include <cstdint>
#include <vector>
#include <string>
#include "endianess.h"

class ParseUtils {
public:
    static std::string parseString(std::vector<uint8_t> &bytes, uint64_t startIndex, uint64_t bytesCount);
    static int64_t parse8ByteNumber(std::vector<uint8_t> &bytes, uint64_t startIndex, Endianess endianess);
    static int16_t parse2ByteNumber(std::vector<uint8_t> &bytes, uint64_t startIndex, Endianess endianess);
private:
    ParseUtils();

    static const Endianess systemEndianess;
};


#endif //CAFFPARSER_PARSE_UTILS_H
