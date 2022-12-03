//
// Created by fupn26 on 2022.11.08..
//
#include <cstring>
#include <algorithm>
#include "gtest/gtest.h"
#include "parse_utils.h"

TEST(ParseUtilsTest, Parse8ByteBigEndian) {
    //given
    int64_t expected = 300;
    std::vector<uint8_t> bytes(sizeof(int64_t));
    std::memcpy(bytes.data(), &expected, sizeof(int64_t));
    std::reverse(bytes.begin(), bytes.end());

    //when
    int64_t actual = ParseUtils::parse8ByteNumber(bytes, 0, Endianess::BIG_ENDIAN_MODE);

    //then
    ASSERT_EQ(expected, actual);
}

TEST(ParseUtilsTest, Parse8ByteLittleEndian) {
    //given
    int64_t expected = 300;
    std::vector<uint8_t> bytes(sizeof(int64_t));
    std::memcpy(bytes.data(), &expected, sizeof(int64_t));

    //when
    int64_t actual = ParseUtils::parse8ByteNumber(bytes, 0, Endianess::LITTLE_ENDIAN_MODE);

    //then
    ASSERT_EQ(expected, actual);
}

TEST(ParseUtilsTest, Parse8ByteLittleEndianNegative) {
    //given
    int64_t expected = -300;
    std::vector<uint8_t> bytes(sizeof(int64_t));
    std::memcpy(bytes.data(), &expected, sizeof(int64_t));

    //when
    int64_t actual = ParseUtils::parse8ByteNumber(bytes, 0, Endianess::LITTLE_ENDIAN_MODE);

    //then
    ASSERT_EQ(expected, actual);
}

TEST(ParseUtilsTest, Parse8ByteBigEndianNegative) {
    //given
    int64_t expected = -300;
    std::vector<uint8_t> bytes(sizeof(int64_t));
    std::memcpy(bytes.data(), &expected, sizeof(int64_t));
    std::reverse(bytes.begin(), bytes.end());

    //when
    int64_t actual = ParseUtils::parse8ByteNumber(bytes, 0, Endianess::BIG_ENDIAN_MODE);

    //then
    ASSERT_EQ(expected, actual);
}

TEST(ParseUtilsTest, Parse2ByteBigEndian) {
    //given
    int16_t expected = 300;
    std::vector<uint8_t> bytes(sizeof(int16_t));
    std::memcpy(bytes.data(), &expected, sizeof(int16_t));
    std::reverse(bytes.begin(), bytes.end());

    //when
    int16_t actual = ParseUtils::parse2ByteNumber(bytes, 0, Endianess::BIG_ENDIAN_MODE);

    //then
    ASSERT_EQ(expected, actual);
}

TEST(ParseUtilsTest, Parse2ByteLittleEndian) {
    //given
    int16_t expected = 300;
    std::vector<uint8_t> bytes(sizeof(int16_t));
    std::memcpy(bytes.data(), &expected, sizeof(int16_t));

    //when
    int16_t actual = ParseUtils::parse2ByteNumber(bytes, 0, Endianess::LITTLE_ENDIAN_MODE);

    //then
    ASSERT_EQ(expected, actual);
}

TEST(ParseUtilsTest, Parse2ByteLittleEndianNegative) {
    //given
    int16_t expected = -300;
    std::vector<uint8_t> bytes(sizeof(int16_t));
    std::memcpy(bytes.data(), &expected, sizeof(int16_t));

    //when
    int16_t actual = ParseUtils::parse2ByteNumber(bytes, 0, Endianess::LITTLE_ENDIAN_MODE);

    //then
    ASSERT_EQ(expected, actual);
}

TEST(ParseUtilsTest, Parse2ByteBigEndianNegative) {
    //given
    int16_t expected = -300;
    std::vector<uint8_t> bytes(sizeof(int16_t));
    std::memcpy(bytes.data(), &expected, sizeof(int16_t));
    std::reverse(bytes.begin(), bytes.end());

    //when
    int16_t actual = ParseUtils::parse2ByteNumber(bytes, 0, Endianess::BIG_ENDIAN_MODE);

    //then
    ASSERT_EQ(expected, actual);
}
