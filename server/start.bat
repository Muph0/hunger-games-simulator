@echo off

echo Starting node.js
node app.js

if not "%ERRORLEVEL%" == "0" (
    echo.
    pause
)