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
const uint8_t CAFF::minimumAnimationBlockRead = 1;

CAFF::CAFF() {
    creator = "";
    numberOfAnimations = 0;
    valid = true;
    endianess = Endianess::LITTLE_ENDIAN_MODE;
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
                caff.handleError("Multiple header block found");
                break;
            case CAFF::creditsId:
                if (isCreditsRead) {
                    caff.handleError("Multiple credits block found");
                    break;
                }
                bytesRead += parseCreditsBlock(caff, bytes, bytesRead);
                isCreditsRead = true;
                break;
            case CAFF::animationId:
                bytesRead += parseAnimationBlock(caff, bytes, bytesRead);
                animationBlocksRead++;
                if (animationBlocksRead > caff.numberOfAnimations) {
                    caff.handleError("Too many animation block found");
                }
                break;
            default:
                caff.valid = false;
                break;
        }
        if (!caff.valid)
            break;
    }

    if (caff.valid && caff.numberOfAnimations != animationBlocksRead || caff.numberOfAnimations < CAFF::minimumAnimationBlockRead) {
        caff.handleError("Animation blocks are missing");
    }

    return caff;
}

uint64_t CAFF::parseHeaderBlock(CAFF &caff, std::vector<uint8_t> &bytes) {
    uint64_t bytesRead = 0;
    const uint64_t dataSize = bytes.size();
    const uint64_t headerSize = 20;
    const uint64_t headerBlockSize = headerSize + 9;

    if (bytesRead >= dataSize || dataSize < headerBlockSize) {
        caff.handleError("Can't read file header");
        return bytesRead;
    }

    // Read ID
    uint8_t id = bytes[bytesRead];
    bytesRead++;

    if (id != CAFF::headerId) { // The first block of all CAFF files is the CAFF header.
        caff.handleError("File not starting with header block");
        return bytesRead;
    }

    // Read length
    int64_t lengthLittleEndian = ParseUtils::parse8ByteNumber(bytes, bytesRead, Endianess::LITTLE_ENDIAN_MODE);
    int64_t lengthBigEndian = ParseUtils::parse8ByteNumber(bytes, bytesRead, Endianess::BIG_ENDIAN_MODE);
    bytesRead += 8;

    if (lengthLittleEndian == headerSize)
        caff.endianess = Endianess::LITTLE_ENDIAN_MODE;
    else if (lengthBigEndian == headerSize)
        caff.endianess = Endianess::BIG_ENDIAN_MODE;
    else {
        caff.handleError("Invalid CAFF header block size");
        return bytesRead;
    }

    // Read magic
    std::string magic = ParseUtils::parseString(bytes, bytesRead, 4);
    bytesRead += 4;

    if (magic != CAFF::magicChars) {
        caff.handleError("Invalid CAFF magic chars");
        return bytesRead;
    }

    // Read header size
    int64_t givenHeaderSize = ParseUtils::parse8ByteNumber(bytes, bytesRead, caff.endianess);
    bytesRead += 8;

    if (givenHeaderSize != headerSize) {
        caff.handleError("Invalid CAFF header size");
        return bytesRead;
    }

    // Read number of animations
    int64_t numberOfAnimations = ParseUtils::parse8ByteNumber(bytes, bytesRead, caff.endianess);
    bytesRead += 8;

    if (numberOfAnimations < 0) {
        caff.handleError("Invalid number of animations");
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
        caff.handleError("Can't read credits block");
        return bytesRead - startIndex;
    }

    // Read id
    uint8_t id = bytes[bytesRead];
    bytesRead++;

    if (id != CAFF::creditsId) {
        caff.handleError("Given block is not a credits block");
        return bytesRead - startIndex;
    }

    // Read length
    int64_t length = ParseUtils::parse8ByteNumber(bytes, bytesRead, caff.endianess);
    bytesRead += 8;

    if (length < creditsMinimumSize) {
        caff.handleError("Invalid credits block size");
        return bytesRead - startIndex;
    }

    // Read creation date and time
    caff.creationDate.year = ParseUtils::parse2ByteNumber(bytes, bytesRead, caff.endianess);
    bytesRead += 2;
    caff.creationDate.month = bytes[bytesRead++];
    caff.creationDate.day = bytes[bytesRead++];
    caff.creationDate.hour = bytes[bytesRead++];
    caff.creationDate.minute = bytes[bytesRead++];

    if (!DateValidator::isValidDateTime(caff.creationDate)) {
        caff.handleError("Invalid creation date");
        return bytesRead - startIndex;
    }

    // Read creator length
    int64_t creatorLen = ParseUtils::parse8ByteNumber(bytes, bytesRead, caff.endianess);
    bytesRead += 8;

    if (creatorLen < 0 || dataSize < bytesRead + creatorLen || length != creditsMinimumSize + creatorLen) {
        caff.handleError("Can't read creator");
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
        caff.handleError("Can't read animations block");
        return bytesRead - startIndex;
    }

    // Read id
    uint8_t id = bytes[bytesRead];
    bytesRead++;

    if (id != CAFF::animationId) {
        caff.handleError("Given block is not an animation block");
        return bytesRead - startIndex;
    }

    // Read length
    int64_t length = ParseUtils::parse8ByteNumber(bytes, bytesRead, caff.endianess);
    bytesRead += 8;

    if (dataSize < bytesRead + length || length < (int64_t)animationMinimumSize) {
        caff.handleError("Can't read animation block");
        return bytesRead - startIndex;
    }

    // Read duration
    int64_t duration = ParseUtils::parse8ByteNumber(bytes, bytesRead, caff.endianess);
    bytesRead += 8;

    if (duration < 0) {
        caff.handleError("Invalid animation duration");
        return bytesRead - startIndex;
    }

    // Read CIFF
    uint64_t ciffSize = length - 8; // length - 8 bytes (duration)
    std::vector<uint8_t> ciffBytes(bytes.begin() + bytesRead, bytes.begin() + bytesRead + ciffSize);
    CIFF ciff = CIFF::parseCIFF(ciffBytes, caff.endianess);
    bytesRead += ciffSize;

    if (!ciff.isValid()) {
        caff.parseFails.insert(caff.parseFails.end(), ciff.getParseFails().begin(), ciff.getParseFails().end());
        caff.valid = false;
        return bytesRead - startIndex;
    }

    caff.ciffsWithDuration.emplace_back(duration, ciff);

    return bytesRead - startIndex;
}

uint64_t CAFF::getNumberOfAnimations() const {
    return numberOfAnimations;
}

const DateTime &CAFF::getCreationDate() const {
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
    if (!valid)
        throw std::runtime_error("Can't generate preview from invalid CAFF");

    CIFF ciffToConvert = ciffsWithDuration[0].second;

    std::ostringstream ppm;

    ppm << "P6\n" << ciffToConvert.getImageWidth() << " " << ciffToConvert.getImageHeight()
    << "\n255\n";

    ppm.write(reinterpret_cast<const char *>(ciffToConvert.getPixels().data()), ciffToConvert.getPixels().size());

    std::string result = ppm.str();
    return {result.begin(), result.end()};
}

/**
 * Generates a JSON object from CAFF and CIFF informations
 * @return
 */
std::string CAFF::generateMetaDataJson() {
    std::ostringstream json;

    json << "{"
        << "\"valid\":" << valid << ","
        << "\"errors\":[";
            for (int i = 0; i < parseFails.size(); ++i) {
                if (i != 0)
                    json << ",";
                json << "\"" << parseFails[i];
            }
        json << "]";

        if (valid) {
            json << ",\"caff\":{";
                json << "\"creator\":" << "\"" << creator << "\","
                << "\"creationDate\":{" << "\"year\":" << creationDate.year << ","
                << "\"month\":" << (int)creationDate.month << ","
                << "\"day\":" << (int)creationDate.day << ","
                << "\"hour\":" << (int)creationDate.hour << ","
                << "\"minute\":" << (int)creationDate.minute << "},"
                << "\"ciffs\":[";
                addCIFFsToStream(json);
            json << "]";
            json << "}";
        }
    json << "}";

    return json.str();
}

void CAFF::addCIFFsToStream(std::ostringstream &json) {
    for (int i = 0; i < ciffsWithDuration.size(); ++i) {
        if (i != 0)
            json << ",";

        json << "{" << "\"duration\":" << ciffsWithDuration[i].first << ","
             << "\"width\":" << ciffsWithDuration[i].second.getImageWidth() << ","
             << "\"height\":" << ciffsWithDuration[i].second.getImageHeight() << ","
             << "\"caption\":" << "\"" << ciffsWithDuration[i].second.getCaption() << "\","
             << "\"tags\":[";
                addTagsToStream(json, i);
        json << "]";
        json << "}";
    }
}

void CAFF::addTagsToStream(std::ostringstream &json, int i) {
    const std::vector<std::string> &tags = ciffsWithDuration[i].second.getTags();
    for (int j = 0; j < tags.size(); ++j) {
        if (j != 0)
            json << ",";

        json << "\"" << tags[j] << "\"";
    }
}

const std::vector<std::string> &CAFF::getParseFails() const {
    return parseFails;
}

void CAFF::handleError(const std::string &message) {
    std::cout << "CAFF: " << message << std::endl;
    parseFails.push_back(message);
    valid = false;
}
