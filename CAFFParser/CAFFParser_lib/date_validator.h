//
// Created by fupn26 on 2022.11.05..
//

#ifndef CAFFPARSER_DATE_VALIDATOR_H
#define CAFFPARSER_DATE_VALIDATOR_H


#include "date.h"

class DateValidator {
public:
    DateValidator() = delete;
    static bool isValidDate(Date &date);

private:
    static std::string mapDateToString(Date &date);
    static std::string mapIntToDatePart(uint8_t intToMap);
};


#endif //CAFFPARSER_DATE_VALIDATOR_H
