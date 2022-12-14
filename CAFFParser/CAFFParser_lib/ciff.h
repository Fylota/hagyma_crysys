//
// Created by fupn26 on 04/11/2022.
//

#ifndef CAFFPARSER_CIFF_H
#define CAFFPARSER_CIFF_H

#ifndef SWIG
    #include <string>
    #include <vector>
#endif

#include "library.h"
#include "endianess.h"

class CIFF {
public:
    CAFF_PARSER_LIBRARY_EXPORT static CIFF parseCIFF(std::vector<uint8_t> bytes, Endianess endianess);

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
    void handleError(const std::string &message);

    explicit CIFF(Endianess endianess);

    static const std::string magicChars;
    int64_t headerSize = 0;
    int64_t contentSize = 0;
    int64_t imageWidth = 0;
    int64_t imageHeight = 0;
    std::string caption;
    std::vector<std::string> tags;
    std::vector<uint8_t> pixels;
    bool valid = true;
    Endianess endianess;
    std::vector<std::string> parseFails;
public:
    const std::vector<std::string> &getParseFails() const;
};


#endif //CAFFPARSER_CIFF_H
