//
// Created by fupn26 on 05/11/2022.
//

#include "gtest/gtest.h"
#include "ciff.h"
#include "file_reader.h"

TEST(CIFFTest, ValidColor){
    //given
    std::vector<uint8_t> ciffBytes = readVectorFromDisk("../../Tests/test_files/CIFF/valid_color.ciff");

    //when
    CIFF ciff = CIFF::parseCIFF(ciffBytes, Endianess::LITTLE_ENDIAN_MODE);

    //then
    EXPECT_TRUE(ciff.isValid());
}

TEST(CIFFTest, InvalidMagicChar){
    //given
    std::vector<uint8_t> ciffBytes = readVectorFromDisk("../../Tests/test_files/CIFF/invalid_with_ciff_magic.ciff");

    //when
    CIFF ciff = CIFF::parseCIFF(ciffBytes, Endianess::LITTLE_ENDIAN_MODE);

    //then
    EXPECT_FALSE(ciff.isValid());
}

TEST(CIFFTest, InvalidMagicChar2){
    //given
    std::vector<uint8_t> ciffBytes = readVectorFromDisk("../../Tests/test_files/CIFF/invalid_with_CIFc_magic.ciff");

    //when
    CIFF ciff = CIFF::parseCIFF(ciffBytes, Endianess::LITTLE_ENDIAN_MODE);

    //then
    EXPECT_FALSE(ciff.isValid());
}

TEST(CIFFTest, InvalidZeroHeaderSize){
    //given
    std::vector<uint8_t> ciffBytes = readVectorFromDisk("../../Tests/test_files/CIFF/invalid_with_header_size_0.ciff");

    //when
    CIFF ciff = CIFF::parseCIFF(ciffBytes, Endianess::LITTLE_ENDIAN_MODE);

    //then
    EXPECT_FALSE(ciff.isValid());
}

TEST(CIFFTest, InvalidNegativeHeaderSize){
    //given
    std::vector<uint8_t> ciffBytes = readVectorFromDisk("../../Tests/test_files/CIFF/invalid_with_header_size_-1.ciff");

    //when
    CIFF ciff = CIFF::parseCIFF(ciffBytes, Endianess::LITTLE_ENDIAN_MODE);

    //then
    EXPECT_FALSE(ciff.isValid());
}

TEST(CIFFTest, ValidWithoutPixels){
    //given
    std::vector<uint8_t> ciffBytes = readVectorFromDisk("../../Tests/test_files/CIFF/valid_without_pixels.ciff");

    //when
    CIFF ciff = CIFF::parseCIFF(ciffBytes, Endianess::LITTLE_ENDIAN_MODE);

    //then
    EXPECT_TRUE(ciff.isValid());
}

TEST(CIFFTest, InvalidWidthAndHeight){
    //given
    std::vector<uint8_t> ciffBytes = readVectorFromDisk("../../Tests/test_files/CIFF/invalid_width_and_height.ciff");

    //when
    CIFF ciff = CIFF::parseCIFF(ciffBytes, Endianess::LITTLE_ENDIAN_MODE);

    //then
    EXPECT_FALSE(ciff.isValid());
}

TEST(CIFFTest, InvalidNegativeContentSize){
    //given
    std::vector<uint8_t> ciffBytes = readVectorFromDisk(
            "../../Tests/test_files/CIFF/invalid_with_negative_content_size.ciff");

    //when
    CIFF ciff = CIFF::parseCIFF(ciffBytes, Endianess::LITTLE_ENDIAN_MODE);

    //then
    EXPECT_FALSE(ciff.isValid());
}

TEST(CIFFTest, InvalidNonZeroImageHeight){
    //given
    std::vector<uint8_t> ciffBytes = readVectorFromDisk(
            "../../Tests/test_files/CIFF/invalid_with_non-zero_image_width.ciff");

    //when
    CIFF ciff = CIFF::parseCIFF(ciffBytes, Endianess::LITTLE_ENDIAN_MODE);

    //then
    EXPECT_FALSE(ciff.isValid());
}

TEST(CIFFTest, InvalidNegativeImageHeight){
    //given
    std::vector<uint8_t> ciffBytes = readVectorFromDisk(
            "../../Tests/test_files/CIFF/invalid_with_negative_image_height.ciff");

    //when
    CIFF ciff = CIFF::parseCIFF(ciffBytes, Endianess::LITTLE_ENDIAN_MODE);

    //then
    EXPECT_FALSE(ciff.isValid());
}

TEST(CIFFTest, ValidWithoutCaption){
    //given
    std::vector<uint8_t> ciffBytes = readVectorFromDisk("../../Tests/test_files/CIFF/valid_without_caption.ciff");

    //when
    CIFF ciff = CIFF::parseCIFF(ciffBytes, Endianess::LITTLE_ENDIAN_MODE);

    //then
    EXPECT_TRUE(ciff.isValid());
}

TEST(CIFFTest, InvalidWithTagsContainingNewLine){
    //given
    std::vector<uint8_t> ciffBytes = readVectorFromDisk(
            "../../Tests/test_files/CIFF/invalid_with_tags_containing_new_line.ciff");

    //when
    CIFF ciff = CIFF::parseCIFF(ciffBytes, Endianess::LITTLE_ENDIAN_MODE);

    //then
    EXPECT_FALSE(ciff.isValid());
}

TEST(CIFFTest, InvalidWithMisssingClosingCharacterAfterLastTag){
    //given
    std::vector<uint8_t> ciffBytes = readVectorFromDisk(
            "../../Tests/test_files/CIFF/invalid_with_missing_closing_character_after_last_tag.ciff");

    //when
    CIFF ciff = CIFF::parseCIFF(ciffBytes, Endianess::LITTLE_ENDIAN_MODE);

    //then
    EXPECT_FALSE(ciff.isValid());
}

TEST(CIFFTest, ValidWithoutTags){
    //given
    std::vector<uint8_t> ciffBytes = readVectorFromDisk("../../Tests/test_files/CIFF/valid_without_tags.ciff");

    //when
    CIFF ciff = CIFF::parseCIFF(ciffBytes, Endianess::LITTLE_ENDIAN_MODE);

    //then
    EXPECT_TRUE(ciff.isValid());
}

TEST(CIFFTest, InvalidWithZeroHeightAndWidth) {
    //given
    std::vector<uint8_t> ciffBytes = readVectorFromDisk(
            "../../Tests/test_files/CIFF/invalid_with_zero_height_and_width.ciff");

    //when
    CIFF ciff = CIFF::parseCIFF(ciffBytes, Endianess::LITTLE_ENDIAN_MODE);

    //then
    EXPECT_FALSE(ciff.isValid());
}

TEST(CIFFTest, ValidGraycsale) {
    //given
    std::vector<uint8_t> ciffBytes = readVectorFromDisk("../../Tests/test_files/CIFF/valid_grayscale.ciff");

    //when
    CIFF ciff = CIFF::parseCIFF(ciffBytes, Endianess::LITTLE_ENDIAN_MODE);

    //then
    EXPECT_TRUE(ciff.isValid());
}