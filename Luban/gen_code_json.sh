#!/bin/zsh
WORKSPACE=..

GEN_CLIENT=${WORKSPACE}/Luban/Luban.ClientServer/Luban.ClientServer.dll
CONF_ROOT=${WORKSPACE}/Luban/Config


dotnet ${GEN_CLIENT} -h ${LUBAN_SERVER_IP} -j cfg --\
 -d ${CONF_ROOT}/Defines/__root__.xml \
 --input_data_dir ${CONF_ROOT}/Datas \
 --output_code_dir ${WORKSPACE}/Assets/Gen \
 --output_data_dir ${WORKSPACE}/Assets/GenerateDatas/json \
 --gen_types code_cs_unity_json,data_json \
 -s all 
