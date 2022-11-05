//
// Created by fupn26 on 04/11/2022.
//

#ifndef CAFFPARSER_CAFF_H
#define CAFFPARSER_CAFF_H


#include <string>
#include "CIFF.h"
#include "Date.h"

class CAFF {
public:
    static CAFF parseCAFF(std::vector<uint8_t> bytes);

    uint64_t getNumberOfAnimations() const;

    const Date &getCreationDate() const;

    const std::string &getCreator() const;

    const std::vector<std::pair<int64_t, CIFF>> &getCiffsWithDuration() const;

    bool isValid() const;

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
