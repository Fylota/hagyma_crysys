//
// Created by fupn26 on 04/11/2022.
//

#ifndef CAFFPARSER_CAFF_H
#define CAFFPARSER_CAFF_H

#include "ciff.h"
#include "date_time.h"
#include "library.h"

class CAFF {
public:
    CAFF_PARSER_LIBRARY_EXPORT static CAFF parseCAFF(std::vector<uint8_t> bytes);

    CAFF_PARSER_LIBRARY_EXPORT uint64_t getNumberOfAnimations() const;

    CAFF_PARSER_LIBRARY_EXPORT const DateTime &getCreationDate() const;

    CAFF_PARSER_LIBRARY_EXPORT const std::string &getCreator() const;

    CAFF_PARSER_LIBRARY_EXPORT const std::vector<std::pair<int64_t, CIFF>> &getCiffsWithDuration() const;

    CAFF_PARSER_LIBRARY_EXPORT bool isValid() const;

    CAFF_PARSER_LIBRARY_EXPORT std::vector<uint8_t> generatePpmPreview();

    CAFF_PARSER_LIBRARY_EXPORT std::string generateMetaDataJson();

    CAFF_PARSER_LIBRARY_EXPORT const std::vector<std::string> &getParseFails() const;
private:
    static uint64_t parseHeaderBlock(CAFF &caff, std::vector<uint8_t> &bytes);
    static uint64_t parseCreditsBlock(CAFF &caff, std::vector<uint8_t> &bytes, uint64_t startIndex);
    static uint64_t parseAnimationBlock(CAFF &caff, std::vector<uint8_t> &bytes, uint64_t startIndex);
    void handleError(const std::string &message);

    CAFF();

    static const std::string magicChars;
    static const uint8_t headerId;
    static const uint8_t creditsId;
    static const uint8_t animationId;
    static const uint8_t minimumAnimationBlockRead;

    uint64_t numberOfAnimations = 0;
    DateTime creationDate;
    std::string creator;
    std::vector<std::pair<int64_t, CIFF>> ciffsWithDuration;
    bool valid = true;
    Endianess endianess;
    std::vector<std::string> parseFails;

    void addTagsToStream(std::ostringstream &json, int i);

    void addCIFFsToStream(std::ostringstream &json);
};


#endif //CAFFPARSER_CAFF_H
