//
// Created by fupn26 on 04/11/2022.
//

#include <iostream>
#include <fstream>
#include <cstring>
#include "CAFFParser_lib/ciff.h"
#include "CAFFParser_lib/caff.h"

std::vector<uint8_t> readVectorFromDisk(const std::string& filePath)
{
    std::ifstream inStream(filePath, std::ios::in | std::ios::binary);

    if (!inStream.is_open()) {
        std::cerr << "Can't open input file: " << filePath << std::endl;
        exit(-3);
    }

    std::vector<uint8_t> data((std::istreambuf_iterator<char>(inStream)), std::istreambuf_iterator<char>());
    inStream.close();
    return data;
}

void writeVectorToDisk(const std::string& filePath, const std::vector<uint8_t> &data)
{
    std::ofstream outStream(filePath, std::ios::out | std::ios::binary);

    if (!outStream.is_open()) {
        std::cerr << "Can't create output file: " << filePath << std::endl;
        exit(-3);
    }

    for (const uint8_t &e : data) {
        outStream << e;
    }

    outStream.close();
}

void writeStringToFile(const std::string& filePath, const std::string &data) {
    std::ofstream outStream(filePath, std::ios::out | std::ios::binary);

    if (!outStream.is_open()) {
        std::cerr << "Can't create output file: " << filePath << std::endl;
        exit(-3);
    }

    outStream << data;

    outStream.close();
}

void printHelp() {
    std::cerr << "Usage: ./CAFFParser_run <caff file> -o <ppm file to generate> <json file to generate>" << std::endl;
    exit(-1);
}

int main(int argc, char *argv[]) {

    if (argc != 5) {
        printHelp();
    }

    if (strcmp(argv[2], "-o") != 0) {
        printHelp();
    }

    CAFF caff = CAFF::parseCAFF(readVectorFromDisk(argv[1]));
    std::cout << argv[1] << '\t' << (caff.isValid() ? "VALID" : "INVALID") << std::endl;

    writeStringToFile(argv[4], caff.generateMetaDataJson());

    if (!caff.isValid())
        return -2;

    writeVectorToDisk(argv[3], caff.generatePpmPreview());

    return 0;
}

