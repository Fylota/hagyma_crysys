//
// Created by fupn26 on 2022.11.08..
//

#include "gtest/gtest.h"
#include "date_validator.h"

TEST(DateValidatorTest, InvalidDayInMonth) {
    //given
    DateTime date;
    date.year = 1999; //not leap year
    date.month = 2;
    date.day = 29;
    date.hour = 23;
    date.minute = 56;

    //when
    bool actual = DateValidator::isValidDateTime(date);

    //then
    ASSERT_FALSE(actual);
}

TEST(DateValidatorTest, ValidDateTime) {
    //given
    DateTime date;
    date.year = 1999;
    date.month = 2;
    date.day = 28;
    date.hour = 23;
    date.minute = 56;

    //when
    bool actual = DateValidator::isValidDateTime(date);

    //then
    ASSERT_TRUE(actual);
}

TEST(DateValidatorTest, YearValidation) {
    //given
    DateTime invalidDate;
    invalidDate.year = 1979;
    invalidDate.month = 2;
    invalidDate.day = 28;
    invalidDate.hour = 23;
    invalidDate.minute = 56;

    DateTime validDate;
    validDate.year = 1980;
    validDate.month = 2;
    validDate.day = 28;
    validDate.hour = 23;
    validDate.minute = 56;


    //when
    bool actualInvalid = DateValidator::isValidDateTime(invalidDate);
    bool actualValid = DateValidator::isValidDateTime(validDate);

    //then
    ASSERT_FALSE(actualInvalid);
    ASSERT_TRUE(actualValid);
}

TEST(DateValidatorTest, InvalidFutureDate) {
    //given
    DateTime date;
    date.year = 2300;
    date.month = 2;
    date.day = 28;
    date.hour = 23;
    date.minute = 56;

    //when
    bool actual = DateValidator::isValidDateTime(date);

    //then
    ASSERT_FALSE(actual);
}

TEST(DateValidatorTest, InvalidOutOfRangeValues) {
    //given
    DateTime date;
    date.year = 2300;
    date.month = 13;
    date.day = 28;
    date.hour = 25;
    date.minute = 62;

    //when
    bool actual = DateValidator::isValidDateTime(date);

    //then
    ASSERT_FALSE(actual);
}

TEST(DateValidatorTest, Valid31DaysMonth) {
    //given
    DateTime date;
    date.year = 2021;
    date.month = 8;
    date.day = 31;
    date.hour = 22;
    date.minute = 21;

    //when
    bool actual = DateValidator::isValidDateTime(date);

    //then
    ASSERT_TRUE(actual);
}

TEST(DateValidatorTest, Valid30DaysMonth) {
    //given
    DateTime date;
    date.year = 2021;
    date.month = 6;
    date.day = 30;
    date.hour = 22;
    date.minute = 21;

    //when
    bool actual = DateValidator::isValidDateTime(date);

    //then
    ASSERT_TRUE(actual);
}