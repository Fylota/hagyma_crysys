//
// Created by fupn26 on 04/11/2022.
//

#ifndef CAFFPARSER_CIFF_H
#define CAFFPARSER_CIFF_H


#include <string>
#include <vector>
#include "library.h"
#include "endianess.h"

class CIFF {
public:
    CAFF_PARSER_LIBRARY_EXPORT static CIFF parseCIFF(std::vector<uint8_t> bytes, Endianess endianess = Endianess::LITTLE_ENDIAN);

    CAFF_PARSER_LIBRARY_EXPORT int64_t getHeaderSize() const;

    CAFF_PARSER_LIBRARY_EXPORT int64_t getContentSize() const;

    CAFF_PARSER_LIBRARY_EXPORT int64_t getImageWidth() const;

    CAFF_PARSER_LIBRARY_EXPORT int64_t getImageHeight() const;

    CAFF_PARSER_LIBRARY_EXPORT const std::string &getCaption() const;

    CAFF_PARSER_LIBRARY_EXPORT const std::vector<std::string> &getTags() const;

    CAFF_PARSER_LIBRARY_EXPORT const std::vector<uint8_t> &getPixels() const;

    CAFF_PARSER_LIBRARY_EXPORT bool isValid() const;

private:
    static uint64_t parseHeader(CIFF &ciff, std::vector<uint8_t> &bytes);
    static uint64_t parseCaption(CIFF &ciff, std::vector<uint8_t> &bytes, uint64_t startIndex, uint64_t headerSize);
    static uint64_t parseTags(CIFF &ciff, std::vector<uint8_t> &bytes, uint64_t startIndex, uint64_t headerSize);

    CIFF(Endianess endianess);

    static const std::string magicChars;
    int64_t headerSize;
    int64_t contentSize;
    int64_t imageWidth;
    int64_t imageHeight;
    std::string caption;
    std::vector<std::string> tags;
    std::vector<uint8_t> pixels;
    bool valid;
    Endianess endianess;
};


#endif //CAFFPARSER_CIFF_H
