@echo off

node debug app.js

if not "%ERRORLEVEL%" == "0" (
    echo.
    pause
)