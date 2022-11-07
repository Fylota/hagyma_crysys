//
// Created by fupn26 on 04/11/2022.
//

#include <iostream>
#include <fstream>
#include "ciff.h"
#include "caff.h"

std::vector<uint8_t> read_vector_from_disk(const std::string& file_path)
{
    std::ifstream instream(file_path, std::ios::in | std::ios::binary);
    std::vector<uint8_t> data((std::istreambuf_iterator<char>(instream)), std::istreambuf_iterator<char>());
    return data;
}

int main(int argc, char *argv[]) {

    if (argc < 1)
        std::cout << "Give one or more file as argument" << std::endl;
    else {
        for (int i = 1; i < argc; ++i) {
            CAFF caff = CAFF::parseCAFF(read_vector_from_disk(argv[i]));
            std::cout << argv[i] << '\t' << (caff.isValid() ? "VALID" : "INVALID") << std::endl;
        }
    }

    return 0;
}

