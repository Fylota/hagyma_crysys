//
// Created by fupn26 on 04/11/2022.
//

#include <iostream>
#include <fstream>
#include "CIFF.h"

std::vector<uint8_t> read_vector_from_disk(std::string file_path)
{
    std::ifstream instream(file_path, std::ios::in | std::ios::binary);
    std::vector<uint8_t> data((std::istreambuf_iterator<char>(instream)), std::istreambuf_iterator<char>());
    return data;
}

int main(int argc, char *argv[]) {
    std::cout << "Hello world!" << std::endl;

    for (int i = 1; i < argc; ++i) {
        CIFF ciff = CIFF::parseCIFF(read_vector_from_disk(argv[i]));
        std::cout << argv[i] << '\t' << (ciff.isValid() ? "VALID" : "INVALID") << std::endl;
    }

    return 0;
}

