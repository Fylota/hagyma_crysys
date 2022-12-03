//
// Created by fupn26 on 2022.11.05..
//

#ifndef CAFFPARSER_DATE_VALIDATOR_H
#define CAFFPARSER_DATE_VALIDATOR_H


#include "date_time.h"

class DateValidator {
public:
    DateValidator() = delete;
    static bool isValidDateTime(const DateTime &date);

private:
    static std::string mapDateToString(const DateTime &date);
    static std::string mapIntToDatePart(uint8_t intToMap);
    static bool isLeapYear(uint16_t year);
    static bool isValidDayInMonth(uint16_t year, uint8_t month, uint8_t day);
    static bool isValidYear(uint16_t year);
};


#endif //CAFFPARSER_DATE_VALIDATOR_H
