#!/usr/bin/env bash

dotnet run --project ../Trivia 0 > actual.txt

diff golden.txt actual.txt
STATUS=$?
if [ $STATUS -ne 0 ]; then
    echo "error"
else
    echo "all fine"
fi