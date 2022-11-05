//
// Created by fupn26 on 2022.11.05..
//

#ifndef CAFFPARSER_DATEVALIDATOR_H
#define CAFFPARSER_DATEVALIDATOR_H


#include "Date.h"

class DateValidator {
public:
    static bool isValidDate(Date &date);

private:
    static std::string mapDateToString(Date &date);
    static std::string mapIntToDatePart(uint8_t intToMap);
};


#endif //CAFFPARSER_DATEVALIDATOR_H
