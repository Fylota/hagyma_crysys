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

int64_t ParseUtils::parse8ByteNumber(std::vector<uint8_t> &bytes, uint64_t startIndex, Endianess endianess) {
    int64_t result = 0;

    if (endianess == LITTLE_ENDIAN_MODE) {
        int i = 0;
        startIndex = startIndex + 7;
        while (i < 8) {
            result = (result << 8) | bytes[startIndex];
            startIndex--;
            i++;
        }
    } else if (endianess == BIG_ENDIAN_MODE) {
        int i = 0;
        while (i < 8) {
            result = (result << 8) | bytes[startIndex];
            startIndex++;
            i++;
        }
    } else {
        throw std::runtime_error("Invalid endianess specified");
    }

    return result;
}

int16_t ParseUtils::parse2ByteNumber(std::vector<uint8_t> &bytes, uint64_t startIndex, Endianess endianess) {
    int16_t result = 0;

    if (endianess == LITTLE_ENDIAN_MODE) {
        int i = 0;
        startIndex = startIndex + 1;
        while (i < 2) {
            result = (result << 8) | bytes[startIndex];
            startIndex--;
            i++;
        }
    } else if (endianess == BIG_ENDIAN_MODE) {
        int i = 0;
        while (i < 2) {
            result = (result << 8) | bytes[startIndex];
            startIndex++;
            i++;
        }
    } else {
        throw std::runtime_error("Invalid endianess specified");
    }

    return result;
}
