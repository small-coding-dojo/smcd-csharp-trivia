#!/usr/bin/env bash

dotnet run --project ../Trivia 0 > actual.txt

# todo: use git line ending conversion instead 
# https://docs.github.com/en/get-started/getting-started-with-git/configuring-git-to-handle-line-endings 
diff golden.txt actual.txt
STATUS=$?
if [ $STATUS -ne 0 ]; then
    echo "error"
else
    echo "all fine"
fi