//
// Created by fupn26 on 2022.11.05..
//

#ifndef CAFFPARSER_DATE_H
#define CAFFPARSER_DATE_H

#include <cstdint>

struct Date {
    uint16_t year = 0;
    uint8_t month = 0;
    uint8_t day = 0;
    uint8_t hour = 0;
    uint8_t minute = 0;
};

#endif //CAFFPARSER_DATE_H
