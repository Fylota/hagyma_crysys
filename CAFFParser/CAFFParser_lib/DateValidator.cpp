//
// Created by fupn26 on 2022.11.05..
//

#include <iomanip>
#include <iostream>
#include <chrono>
#include "DateValidator.h"

bool DateValidator::isValidDate(Date &date) {
    std::tm t = {};
    std::string dateString = mapDateToString(date);
    std::istringstream ss(dateString);
    ss >> std::get_time(&t, "%Y.%m.%d %H:%M");

    if (ss.fail()) {
        return false;
    }

    std::chrono::system_clock::time_point givenTimePoint = std::chrono::system_clock::from_time_t(std::mktime(&t));
    std::chrono::system_clock::time_point now = std::chrono::system_clock::now();

    if (givenTimePoint > now) {
        return false;
    }

    return true;
}

std::string DateValidator::mapDateToString(Date &date) {
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


