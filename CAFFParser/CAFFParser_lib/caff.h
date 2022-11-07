//
// Created by fupn26 on 04/11/2022.
//

#ifndef CAFFPARSER_CAFF_H
#define CAFFPARSER_CAFF_H

#include <string>
#include "ciff.h"
#include "date.h"
#include "library.h"

class CAFF {
public:
    CAFF_PARSER_LIBRARY_EXPORT static CAFF parseCAFF(std::vector<uint8_t> bytes);

    CAFF_PARSER_LIBRARY_EXPORT uint64_t getNumberOfAnimations() const;

    CAFF_PARSER_LIBRARY_EXPORT const Date &getCreationDate() const;

    CAFF_PARSER_LIBRARY_EXPORT const std::string &getCreator() const;

    CAFF_PARSER_LIBRARY_EXPORT const std::vector<std::pair<int64_t, CIFF>> &getCiffsWithDuration() const;

    CAFF_PARSER_LIBRARY_EXPORT bool isValid() const;

private:
    static uint64_t parseHeaderBlock(CAFF &caff, std::vector<uint8_t> &bytes);
    static uint64_t parseCreditsBlock(CAFF &caff, std::vector<uint8_t> &bytes, uint64_t startIndex);
    static uint64_t parseAnimationBlock(CAFF &caff, std::vector<uint8_t> &bytes, uint64_t startIndex);

    CAFF();

    static const std::string magicChars;
    static const uint8_t headerId;
    static const uint8_t creditsId;
    static const uint8_t animationId;

    uint64_t numberOfAnimations;
    Date creationDate;
    std::string creator;
    std::vector<std::pair<int64_t, CIFF>> ciffsWithDuration;
    bool valid;
};


#endif //CAFFPARSER_CAFF_H
