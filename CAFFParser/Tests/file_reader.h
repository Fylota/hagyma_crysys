//
// Created by fupn26 on 06/11/2022.
//

#ifndef CAFFPARSER_FILE_READER_H
#define CAFFPARSER_FILE_READER_H

#include <fstream>
#include <vector>

std::vector<uint8_t> read_vector_from_disk(const std::string& file_path);

#endif //CAFFPARSER_FILE_READER_H
