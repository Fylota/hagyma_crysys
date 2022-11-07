//
// Created by fupn26 on 06/11/2022.
//

#include "file_reader.h"

std::vector<uint8_t> readVectorFromDisk(const std::string& file_path)
{
    std::ifstream instream(file_path, std::ios::in | std::ios::binary);
    std::vector<uint8_t> data((std::istreambuf_iterator<char>(instream)), std::istreambuf_iterator<char>());
    return data;
}