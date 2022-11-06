//
// Created by fupn26 on 04/11/2022.
//

#include <sstream>
#include <iostream>
#include "ciff.h"
#include "parse_utils.h"

CIFF::CIFF() {
    headerSize = 0;
    contentSize = 0;
    imageHeight = 0;
    imageWidth = 0;
    caption = "";
    valid = true;
}

CIFF CIFF::parseCIFF(std::vector<uint8_t> bytes) {
    CIFF ciff;

    uint64_t dataSize = bytes.size();

    uint64_t bytesRead = 0;

    bytesRead += parseHeader(ciff, bytes);

    if (bytesRead == dataSize || !ciff.isValid())
        return ciff;

    std::vector<uint8_t> pixels;
    std::copy(bytes.begin() + bytesRead, bytes.end(), std::back_inserter(pixels));

    ciff.setPixels(pixels);

    return ciff;
}

const std::string &CIFF::getMagicChars() const {
    return magicChars;
}


int64_t CIFF::getHeaderSize() const {
    return headerSize;
}

void CIFF::setHeaderSize(int64_t headerSize) {
    CIFF::headerSize = headerSize;
}


int64_t CIFF::getContentSize() const {
    return contentSize;
}

void CIFF::setContentSize(int64_t contentSize) {
    CIFF::contentSize = contentSize;
}


int64_t CIFF::getImageWidth() const {
    return imageWidth;
}

void CIFF::setImageWidth(int64_t imageWidth) {
    CIFF::imageWidth = imageWidth;
}


int64_t CIFF::getImageHeight() const {
    return imageHeight;
}

void CIFF::setImageHeight(int64_t imageHeight) {
    CIFF::imageHeight = imageHeight;
}

const std::string &CIFF::getCaption() const {
    return caption;
}

void CIFF::setCaption(const std::string &caption) {
    CIFF::caption = caption;
}

const std::vector<std::string> &CIFF::getTags() const {
    return tags;
}

void CIFF::setTags(const std::vector<std::string> &tags) {
    CIFF::tags = tags;
}

const std::vector<uint8_t> &CIFF::getPixels() const {
    return pixels;
}

void CIFF::setPixels(const std::vector<uint8_t> &pixels) {
    CIFF::pixels = pixels;
}

bool CIFF::isValid() const {
    return valid;
}

void CIFF::setIsValid(bool isValid) {
    CIFF::valid = isValid;
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
        ciff.setIsValid(false);
        return bytesRead;
    }

    std::string magic = ParseUtils::parseString(bytes, bytesRead, 4);
    bytesRead += 4;

    if (magic != ciff.getMagicChars()) {
        std::cout << "Magic chars are invalid" << std::endl;
        ciff.setIsValid(false);
        return bytesRead;
    }

    ciff.setHeaderSize(ParseUtils::parse8ByteNumber(bytes, bytesRead));
    bytesRead += 8;

    if (ciff.getHeaderSize() < (int64_t)minimumHeaderSize) {
        std::cout << "Invalid header size" << std::endl;
        ciff.setIsValid(false);
        return bytesRead;
    }

    ciff.setContentSize(ParseUtils::parse8ByteNumber(bytes, bytesRead));
    bytesRead += 8;

    if (ciff.getContentSize() < 0) {
        std::cout << "Invalid content size" << std::endl;
        ciff.setIsValid(false);
        return bytesRead;
    }

    if (ciff.getContentSize() + ciff.getHeaderSize() != dataSize) {
        std::cout << "CIFF file size invalid" << std::endl;
        ciff.setIsValid(false);
        return bytesRead;
    }

    ciff.setImageWidth(ParseUtils::parse8ByteNumber(bytes, bytesRead));
    bytesRead += 8;
    ciff.setImageHeight(ParseUtils::parse8ByteNumber(bytes, bytesRead));
    bytesRead += 8;

    if (ciff.getImageHeight() < 0 || ciff.getImageWidth() < 0
    || ciff.contentSize == 0 && (ciff.imageHeight != 0 || ciff.imageWidth != 0)
    || ciff.getContentSize() != ciff.getImageWidth() * ciff.getImageHeight() * 3) {
        std::cout << "Width and height are invalid" << std::endl;
        ciff.setIsValid(false);
        return bytesRead;
    }

    if (dataSize == bytesRead) {
        return bytesRead;
    }

    bytesRead += parseCaption(ciff, bytes, bytesRead, ciff.getHeaderSize());

    if (dataSize == bytesRead) {
        return bytesRead;
    }

    bytesRead += parseTags(ciff, bytes, bytesRead, ciff.getHeaderSize());

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

    ciff.setCaption(stringStream.str());

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
