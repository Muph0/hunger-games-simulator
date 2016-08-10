@echo off

node app.js

if not "%ERRORLEVEL%" == "0" (
    echo.
    pause
)