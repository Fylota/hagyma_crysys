//
// Created by fupn26 on 06/11/2022.
//

//
// Created by fupn26 on 05/11/2022.
//

#include "gtest/gtest.h"
#include "caff.h"
#include "file_reader.h"

TEST(CAFFTest, ValidCAFF1){
    //given
    std::vector<uint8_t> caffBytes = read_vector_from_disk("../../Tests/test_files/CAFF/1.caff");

    //when
    CAFF caff = CAFF::parseCAFF(caffBytes);

    //then
    EXPECT_TRUE(caff.isValid());
}

TEST(CAFFTest, ValidCAFF2){
    //given
    std::vector<uint8_t> caffBytes = read_vector_from_disk("../../Tests/test_files/CAFF/2.caff");

    //when
    CAFF caff = CAFF::parseCAFF(caffBytes);

    //then
    EXPECT_TRUE(caff.isValid());
}

TEST(CAFFTest, ValidCAFF3){
    //given
    std::vector<uint8_t> caffBytes = read_vector_from_disk("../../Tests/test_files/CAFF/3.caff");

    //when
    CAFF caff = CAFF::parseCAFF(caffBytes);

    //then
    EXPECT_TRUE(caff.isValid());
}