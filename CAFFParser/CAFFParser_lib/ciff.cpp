//
// Created by fupn26 on 04/11/2022.
//

#include <sstream>
#include <iostream>
#include "ciff.h"
#include "parse_utils.h"

const std::string CIFF::magicChars = "CIFF";

CIFF::CIFF(Endianess endianess) {
    headerSize = 0;
    contentSize = 0;
    imageHeight = 0;
    imageWidth = 0;
    caption = "";
    valid = true;
    this->endianess = endianess;
}

CIFF CIFF::parseCIFF(std::vector<uint8_t> bytes, Endianess endianess) {
    CIFF ciff(endianess);

    uint64_t dataSize = bytes.size();

    uint64_t bytesRead = 0;

    bytesRead += parseHeader(ciff, bytes);

    if (bytesRead == dataSize || !ciff.isValid())
        return ciff;

    std::vector<uint8_t> pixels;
    std::copy(bytes.begin() + bytesRead, bytes.end(), std::back_inserter(pixels));

    ciff.pixels = pixels;

    return ciff;
}

int64_t CIFF::getHeaderSize() const {
    return headerSize;
}

int64_t CIFF::getContentSize() const {
    return contentSize;
}

int64_t CIFF::getImageWidth() const {
    return imageWidth;
}

const std::string &CIFF::getCaption() const {
    return caption;
}

const std::vector<std::string> &CIFF::getTags() const {
    return tags;
}

const std::vector<uint8_t> &CIFF::getPixels() const {
    return pixels;
}

bool CIFF::isValid() const {
    return valid;
}

int64_t CIFF::getImageHeight() const {
    return imageHeight;
}

/**
 * Parses the header in the given ciff file and validates the values
 * @param ciff ciff file which will contain the parsed values
 * @param bytes raw bytes which need to be parsed
 * @return the number of read bytes
 */
uint64_t CIFF::parseHeader(CIFF &ciff, std::vector<uint8_t> &bytes) {

    uint64_t dataSize = bytes.size();

    uint64_t bytesRead = 0;

    uint64_t minimumHeaderSize = 36;


    if (dataSize < minimumHeaderSize) {
        std::cout << "Can't parse header" << std::endl;
        ciff.valid = false;
        return bytesRead;
    }

    std::string magic = ParseUtils::parseString(bytes, bytesRead, 4);
    bytesRead += 4;

    if (magic != CIFF::magicChars) {
        std::cout << "Magic chars are invalid" << std::endl;
        ciff.valid = false;
        return bytesRead;
    }

    ciff.headerSize = ParseUtils::parse8ByteNumber(bytes, bytesRead, ciff.endianess);
    bytesRead += 8;

    if (ciff.headerSize < (int64_t)minimumHeaderSize) {
        std::cout << "Invalid header size" << std::endl;
        ciff.valid= false;
        return bytesRead;
    }

    ciff.contentSize = ParseUtils::parse8ByteNumber(bytes, bytesRead, ciff.endianess);
    bytesRead += 8;

    if (ciff.contentSize < 0) {
        std::cout << "Invalid content size" << std::endl;
        ciff.valid= false;
        return bytesRead;
    }

    if (ciff.contentSize + ciff.headerSize != dataSize) {
        std::cout << "CIFF file size invalid" << std::endl;
        ciff.valid= false;
        return bytesRead;
    }

    ciff.imageWidth = ParseUtils::parse8ByteNumber(bytes, bytesRead, ciff.endianess);
    bytesRead += 8;
    ciff.imageHeight = ParseUtils::parse8ByteNumber(bytes, bytesRead, ciff.endianess);
    bytesRead += 8;

    if (ciff.imageHeight < 0 || ciff.imageWidth < 0
    || ciff.contentSize == 0 && (ciff.imageHeight != 0 || ciff.imageWidth != 0)
    || ciff.contentSize != ciff.imageWidth * ciff.imageHeight * 3) {
        std::cout << "Width and height are invalid" << std::endl;
        ciff.valid= false;
        return bytesRead;
    }

    if (dataSize == bytesRead) {
        return bytesRead;
    }

    bytesRead += parseCaption(ciff, bytes, bytesRead, ciff.headerSize);

    if (dataSize == bytesRead) {
        return bytesRead;
    }

    bytesRead += parseTags(ciff, bytes, bytesRead, ciff.headerSize);

    return bytesRead;
}

uint64_t CIFF::parseCaption(CIFF &ciff, std::vector<uint8_t> &bytes, uint64_t startIndex, uint64_t headerSize) {
    std::ostringstream stringStream;
    uint64_t bytesRead = 0;

    for (uint64_t i = startIndex; i < headerSize; ++i) {
        ++bytesRead;
        if (bytes[i] == '\n')
            break;
        stringStream << bytes[i];
    }

    ciff.caption = stringStream.str();

    return bytesRead;
}

uint64_t CIFF::parseTags(CIFF &ciff, std::vector<uint8_t> &bytes, uint64_t startIndex, uint64_t headerSize) {
    std::ostringstream stringStream;
    uint64_t bytesRead = 0;
    std::vector<std::string> tags;

    for (uint64_t i = startIndex; i < headerSize; ++i) {
        if (bytes[i] == '\n') {
            std::cout << "Tags can't contain new line character" << std::endl;
            ciff.valid = false;
            stringStream.str("");
            stringStream.clear();
            break;
        }
        if (bytes[i] == '\0') {
            tags.push_back(stringStream.str());
            stringStream.str("");
            stringStream.clear();
        } else {
            stringStream << bytes[i];
        }
        ++bytesRead;
    }

    if (!stringStream.str().empty()) {
        std::cout << "Last '\\0' is missing after the last tag: " << stringStream.str() << std::endl;
        ciff.valid = false;
    }

    ciff.tags = tags;

    return bytesRead;
}
