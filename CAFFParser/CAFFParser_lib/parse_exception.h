//
// Created by fupn26 on 04/11/2022.
//

#ifndef CAFFPARSER_PARSE_EXCEPTION_H
#define CAFFPARSER_PARSE_EXCEPTION_H

#include <exception>
#include <string>
#include <utility>

class ParseException : public std::exception {
public:
    explicit ParseException(std::string message) {
        this->message = std::move(message);
    }

    const char *what() const _GLIBCXX_TXN_SAFE_DYN _GLIBCXX_NOTHROW override {
        return message.c_str();
    }

private:
    std::string message;
};

#endif //CAFFPARSER_PARSE_EXCEPTION_H
