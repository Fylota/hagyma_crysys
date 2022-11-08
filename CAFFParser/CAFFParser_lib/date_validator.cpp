//
// Created by fupn26 on 2022.11.05..
//

#include <iomanip>
#include <iostream>
#include <sstream>
#include <chrono>
#include "date_validator.h"

bool DateValidator::isValidDateTime(DateTime &date) {
    std::tm t = {};
    std::string dateString = mapDateToString(date);
    std::istringstream ss(dateString);
    ss >> std::get_time(&t, "%Y.%m.%d %H:%M");

    if (ss.fail()) {
        return false;
    }

    if (!isValidYear(date.year) || !isValidDayInMonth(date.year, date.month, date.day)) {
        return false;
    }

    std::chrono::system_clock::time_point givenTimePoint = std::chrono::system_clock::from_time_t(std::mktime(&t));
    std::chrono::system_clock::time_point now = std::chrono::system_clock::now();

    if ((uint64_t)givenTimePoint.time_since_epoch().count() > (uint64_t)now.time_since_epoch().count()) {
        return false;
    }

    return true;
}

std::string DateValidator::mapDateToString(DateTime &date) {
    std::ostringstream stringStream;
    stringStream << date.year << "." << mapIntToDatePart(date.month)
    << "." << mapIntToDatePart(date.day)
    << " " << mapIntToDatePart(date.hour) << ":" << mapIntToDatePart(date.minute);
    return stringStream.str();
}

std::string DateValidator::mapIntToDatePart(uint8_t intToMap) {
    std::ostringstream stringStream;
    if (intToMap < 10)
        stringStream << '0';
    stringStream << (int)intToMap;
    return stringStream.str();
}

bool DateValidator::isLeapYear(uint16_t year) {
    return (((year % 4 == 0) &&
             (year % 100 != 0)) ||
            (year % 400 == 0));
}

bool DateValidator::isValidDayInMonth(uint16_t year, uint8_t month, uint8_t day) {
    if (month == 2)
    {
        if (isLeapYear(year))
            return (day <= 29);
        else
            return (day <= 28);
    }

    if (month == 4 || month == 6 ||
            month == 9 || month == 11)
        return (day <= 30);

    return true;
}

bool DateValidator::isValidYear(uint16_t year) {
    return year >= 1980;
}


