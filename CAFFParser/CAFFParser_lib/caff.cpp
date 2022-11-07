//
// Created by fupn26 on 04/11/2022.
//

#include <iostream>
#include <sstream>
#include "caff.h"
#include "parse_utils.h"
#include "date_validator.h"

const std::string CAFF::magicChars = "CAFF";
const uint8_t CAFF::headerId = 0x1;
const uint8_t CAFF::creditsId = 0x2;
const uint8_t CAFF::animationId = 0x3;

CAFF::CAFF() {
    creator = "";
    numberOfAnimations = 0;
    valid = true;
}

CAFF CAFF::parseCAFF(std::vector<uint8_t> bytes) {
    CAFF caff;
    uint64_t dataSize = bytes.size();
    uint64_t bytesRead = 0;
    uint64_t animationBlocksRead = 0;
    bool isCreditsRead = false;

    bytesRead += parseHeaderBlock(caff, bytes);

    if (!caff.valid) {
        return caff;
    }

    while (bytesRead < dataSize) {
        switch (bytes[bytesRead]) {
            case CAFF::headerId:
                std::cout << "Multiple header block found" << std::endl;
                caff.valid = false;
                break;
            case CAFF::creditsId:
                if (isCreditsRead) {
                    std::cout << "Multiple credits block found" << std::endl;
                    caff.valid = false;
                    break;
                }
                bytesRead += parseCreditsBlock(caff, bytes, bytesRead);
                isCreditsRead = true;
                break;
            case CAFF::animationId:
                bytesRead += parseAnimationBlock(caff, bytes, bytesRead);
                animationBlocksRead++;
                if (animationBlocksRead > caff.numberOfAnimations) {
                    std::cout << "Too many animation block found" << std::endl;
                }
                break;
            default:
                caff.valid = false;
                break;
        }
        if (!caff.valid)
            break;
    }

    if (caff.numberOfAnimations != animationBlocksRead) {
        std::cout << "Animation blocks are missing" << std::endl;
        caff.valid = false;
    }

    return caff;
}

uint64_t CAFF::parseHeaderBlock(CAFF &caff, std::vector<uint8_t> &bytes) {
    uint64_t bytesRead = 0;
    const uint64_t dataSize = bytes.size();
    const uint64_t headerSize = 20;
    const uint64_t headerBlockSize = headerSize + 9;

    if (bytesRead >= dataSize || dataSize < headerBlockSize) {
        std::cout << "Can't read CAFF file header" << std::endl;
        caff.valid = false;
        return bytesRead;
    }

    // Read ID
    uint8_t id = bytes[bytesRead];
    bytesRead++;

    if (id != CAFF::headerId) { // The first block of all CAFF files is the CAFF header.
        std::cout << "CAFF file not starting with header block" << std::endl;
        caff.valid = false;
        return bytesRead;
    }

    // Read length
    int64_t length = ParseUtils::parse8ByteNumber(bytes, bytesRead);
    bytesRead += 8;

    if (length != headerSize) {
        std::cout << "Invalid CAFF header block size" << std::endl;
        caff.valid = false;
        return bytesRead;
    }

    // Read magic
    std::string magic = ParseUtils::parseString(bytes, bytesRead, 4);
    bytesRead += 4;

    if (magic != CAFF::magicChars) {
        std::cout << "Invalid CAFF magic chars" << std::endl;
        caff.valid = false;
        return bytesRead;
    }

    // Read header size
    int64_t givenHeaderSize = ParseUtils::parse8ByteNumber(bytes, bytesRead);
    bytesRead += 8;

    if (givenHeaderSize != headerSize) {
        std::cout << "Invalid CAFF header size" << std::endl;
        caff.valid = false;
        return bytesRead;
    }

    // Read number of animations
    int64_t numberOfAnimations = ParseUtils::parse8ByteNumber(bytes, bytesRead);
    bytesRead += 8;

    if (numberOfAnimations < 0) {
        std::cout << "Invalid number of animations" << std::endl;
        caff.valid = false;
        return bytesRead;
    }

    caff.numberOfAnimations = numberOfAnimations;

    return bytesRead;
}

uint64_t CAFF::parseCreditsBlock(CAFF &caff, std::vector<uint8_t> &bytes, uint64_t startIndex) {
    const uint64_t dataSize = bytes.size();
    const uint64_t creditsMinimumSize =  14;
    const uint64_t creditsBlockMinimumSize =  creditsMinimumSize + 9;
    uint64_t bytesRead = startIndex;

    if (bytesRead >= dataSize || dataSize < bytesRead + creditsBlockMinimumSize) {
        std::cout << "Can't read credits block" << std::endl;
        caff.valid = false;
        return bytesRead - startIndex;
    }

    // Read id
    uint8_t id = bytes[bytesRead];
    bytesRead++;

    if (id != CAFF::creditsId) {
        std::cout << "Given block is not a credits block" << std::endl;
        caff.valid = false;
        return bytesRead - startIndex;
    }

    // Read length
    int64_t length = ParseUtils::parse8ByteNumber(bytes, bytesRead);
    bytesRead += 8;

    if (length < creditsMinimumSize) {
        std::cout << "Invalid credits block size" << std::endl;
        caff.valid = false;
        return bytesRead - startIndex;
    }

    // Read creation date and time
    caff.creationDate.year = ParseUtils::parse2ByteNumber(bytes, bytesRead);
    bytesRead += 2;
    caff.creationDate.month = bytes[bytesRead++];
    caff.creationDate.day = bytes[bytesRead++];
    caff.creationDate.hour = bytes[bytesRead++];
    caff.creationDate.minute = bytes[bytesRead++];

    if (!DateValidator::isValidDate(caff.creationDate)) {
        std::cout << "Invalid creation date" << std::endl;
        caff.valid = false;
        return bytesRead - startIndex;
    }

    // Read creator length
    int64_t creatorLen = ParseUtils::parse8ByteNumber(bytes, bytesRead);
    bytesRead += 8;

    if (creatorLen < 0 || dataSize < bytesRead + creatorLen || length != creditsMinimumSize + creatorLen) {
        std::cout << "Can't read creator" << std::endl;
        caff.valid = false;
        return bytesRead - startIndex;
    }

    // Read creator
    caff.creator = ParseUtils::parseString(bytes, bytesRead, creatorLen);
    bytesRead += creatorLen;

    return bytesRead - startIndex;
}

uint64_t CAFF::parseAnimationBlock(CAFF &caff, std::vector<uint8_t> &bytes, uint64_t startIndex) {
    const uint64_t dataSize = bytes.size();
    const uint64_t animationMinimumSize = 8;
    const uint64_t animationBlockMinimumSize = animationMinimumSize + 9;
    uint64_t bytesRead = startIndex;

    if (bytesRead >= dataSize || dataSize < bytesRead + animationBlockMinimumSize) {
        std::cout << "Can't read animations block" << std::endl;
        caff.valid = false;
        return bytesRead - startIndex;
    }

    // Read id
    uint8_t id = bytes[bytesRead];
    bytesRead++;

    if (id != CAFF::animationId) {
        std::cout << "Given block is not an animation block" << std::endl;
        caff.valid = false;
        return bytesRead - startIndex;
    }

    // Read length
    int64_t length = ParseUtils::parse8ByteNumber(bytes, bytesRead);
    bytesRead += 8;

    if (dataSize < bytesRead + length || length < animationMinimumSize) {
        std::cout << "Can't read animation block" << std::endl;
        caff.valid = false;
        return bytesRead - startIndex;
    }

    // Read duration
    int64_t duration = ParseUtils::parse8ByteNumber(bytes, bytesRead);
    bytesRead += 8;

    if (duration < 0) {
        std::cout << "Invalid animation duration" << std::endl;
        caff.valid = false;
        return bytesRead - startIndex;
    }

    // Read CIFF
    uint64_t ciffSize = length - 8; // length - 8 bytes (duration)
    std::vector<uint8_t> ciffBytes(bytes.begin() + bytesRead, bytes.begin() + bytesRead + ciffSize);
    CIFF ciff = CIFF::parseCIFF(ciffBytes);
    bytesRead += ciffSize;

    if (!ciff.isValid()) {
        std::cout << "Invalid ciff" << std::endl;
        caff.valid = false;
        return bytesRead - startIndex;
    }

    caff.ciffsWithDuration.emplace_back(duration, ciff);

    return bytesRead - startIndex;
}

uint64_t CAFF::getNumberOfAnimations() const {
    return numberOfAnimations;
}

const Date &CAFF::getCreationDate() const {
    return creationDate;
}

const std::string &CAFF::getCreator() const {
    return creator;
}

const std::vector<std::pair<int64_t, CIFF>> &CAFF::getCiffsWithDuration() const {
    return ciffsWithDuration;
}

bool CAFF::isValid() const {
    return valid;
}

/**
 * Generates a preview of the CAFF file from the first CIFF in .ppm format
 * @return ppm file in bytes
 */
std::vector<uint8_t> CAFF::generatePpmPreview() {
    CIFF ciffToConvert = ciffsWithDuration[0].second;

    std::ostringstream ppm;

    ppm << "P6\n" << ciffToConvert.getImageWidth() << " " << ciffToConvert.getImageHeight()
    << "\n255\n";

    ppm.write(reinterpret_cast<const char *>(ciffToConvert.getPixels().data()), ciffToConvert.getPixels().size());

    std::string result = ppm.str();
    return {result.begin(), result.end()};
}
