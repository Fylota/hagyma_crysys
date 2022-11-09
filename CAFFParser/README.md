# CAFF parser program/library

## Building
1. The Google Test submodule has to be loaded by the following command:
```batch
$> git submodule update --init --recursive
```

2. The project can be built by the following command:
```
$> cmake . -D=CAFF_PARSER_LIBRARY_STATIC=ON && make
```

## Running
The program cen be run:
```
$> ./CAFFParser_run <caff file> -o <ppm file to generate> <json file to generate>
```