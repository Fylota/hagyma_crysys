//
// Created by fupn2 on 2022. 11. 06..
//

#ifndef CAFFPARSER_LIBRARY_H
#define CAFFPARSER_LIBRARY_H

#ifndef CAFF_PARSER_LIBRARY_STATIC
    #ifdef CAFFPARSER_LIBRARY_BUILD
    // Building the library
        #ifdef _WIN32
            // Use the Windows-specific export attribute
            #define CAFF_PARSER_LIBRARY_EXPORT __declspec(dllexport)
        #elif __GNUC__ >= 4
            // Use the GCC-specific export attribute
            #define CAFF_PARSER_LIBRARY_EXPORT __attribute__((visibility("default")))
        #else
            // Assume that no export attributes are needed
            #define CAFF_PARSER_LIBRARY_EXPORT
        #endif
    #else
    // Using (including) the library
        #ifdef _WIN32
        // Use the Windows-specific import attribute
            #define CAFF_PARSER_LIBRARY_EXPORT __declspec(dllimport)
        #else
        // Assume that no import attributes are needed
            #define CAFF_PARSER_LIBRARY_EXPORT
        #endif
    #endif
#endif

#ifndef CAFF_PARSER_LIBRARY_EXPORT
    #define CAFF_PARSER_LIBRARY_EXPORT
#endif

#endif //CAFFPARSER_LIBRARY_H
