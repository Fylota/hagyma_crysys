//
// Created by fupn26 on 2022.11.05..
//

#ifndef CAFFPARSER_PARSE_UTILS_H
#define CAFFPARSER_PARSE_UTILS_H


#include <cstdint>
#include <vector>
#include <string>

class ParseUtils {
public:
    static std::string parseString(std::vector<uint8_t> &bytes, uint64_t startIndex, uint64_t bytesCount);
    static int64_t parse8ByteNumber(std::vector<uint8_t> &bytes, uint64_t startIndex);
    static int16_t parse2ByteNumber(std::vector<uint8_t> &bytes, uint64_t startIndex);
};


#endif //CAFFPARSER_PARSE_UTILS_H
