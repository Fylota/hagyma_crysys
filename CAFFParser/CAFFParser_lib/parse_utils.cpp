//
// Created by fupn26 on 2022.11.05..
//

#include <cstring>
#include <sstream>
#include "parse_utils.h"

std::string ParseUtils::parseString(std::vector<uint8_t> &bytes, uint64_t startIndex, uint64_t bytesCount) {
    std::ostringstream stringStream;

    for (uint64_t i = startIndex; i < startIndex + bytesCount; ++i) {
        stringStream << bytes[i];
    }

    return stringStream.str();
}

int64_t ParseUtils::parse8ByteNumber(std::vector<uint8_t> &bytes, uint64_t startIndex) {
    int64_t result = 0;

    std::memcpy(&result, bytes.data() + startIndex, sizeof(int64_t));

    return result;
}

int16_t ParseUtils::parse2ByteNumber(std::vector<uint8_t> &bytes, uint64_t startIndex) {
    int16_t result = 0;

    std::memcpy(&result, bytes.data() + startIndex, sizeof(int16_t));

    return result;
}
