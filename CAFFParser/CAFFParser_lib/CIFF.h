//
// Created by fupn26 on 04/11/2022.
//

#ifndef CAFFPARSER_CIFF_H
#define CAFFPARSER_CIFF_H


#include <string>
#include <vector>

class CIFF {
public:
    static CIFF parseCIFF(std::vector<uint8_t> bytes);

    const std::string &getMagicChars() const;

    int64_t getHeaderSize() const;

    void setHeaderSize(int64_t headerSize);

    int64_t getContentSize() const;

    void setContentSize(int64_t contentSize);

    int64_t getImageWidth() const;

    void setImageWidth(int64_t imageWidth);

    int64_t getImageHeight() const;

    void setImageHeight(int64_t imageHeight);

    const std::string &getCaption() const;

    void setCaption(const std::string &caption);

    const std::vector<std::string> &getTags() const;

    void setTags(const std::vector<std::string> &tags);

    const std::vector<uint8_t> &getPixels() const;

    void setPixels(const std::vector<uint8_t> &pixels);

    bool isValid() const;

    void setIsValid(bool isValid);
private:
    static std::string parseString(std::vector<uint8_t> &bytes, uint64_t startIndex, uint64_t bytesCount);
    static int64_t parse8ByteNumber(std::vector<uint8_t> &bytes, uint64_t startIndex);
    static uint64_t parseHeader(CIFF &ciff, std::vector<uint8_t> &bytes);
    static uint64_t parseCaption(CIFF &ciff, std::vector<uint8_t> &bytes, uint64_t startIndex, uint64_t headerSize);
    static uint64_t parseTags(CIFF &ciff, std::vector<uint8_t> &bytes, uint64_t startIndex, uint64_t headerSize);

    CIFF();

    const std::string magicChars = "CIFF";
    int64_t headerSize;
    int64_t contentSize;
    int64_t imageWidth;
    int64_t imageHeight;
    std::string caption;
    std::vector<std::string> tags;
    std::vector<uint8_t> pixels;
    bool valid;
};


#endif //CAFFPARSER_CIFF_H
