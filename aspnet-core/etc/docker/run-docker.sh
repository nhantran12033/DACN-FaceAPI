#!/bin/bash

if [[ ! -d certs ]]
then
    mkdir certs
    cd certs/
    if [[ ! -f localhost.pfx ]]
    then
        dotnet dev-certs https -v -ep localhost.pfx -p 07051b0d-1ec9-40bb-ac2d-57fdc6c1fd15 -t
    fi
    cd ../
fi

docker-compose up -d
