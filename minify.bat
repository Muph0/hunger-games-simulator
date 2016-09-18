@echo off
setlocal EnableDelayedExpansion

cd game
for /r %%x in (*.js) do (

    if "%files%" == "" (
        set files=%%x
    ) else (
        set files=%files% %%x
    )
)
cd ..

set level=SIMPLE_OPTIMIZATIONS
set level=ADVANCED_OPTIMIZATIONS

echo Compressing...
java -jar compiler.jar CanvasConsole/*.js game/*/*.js game/*.js --compilation_level %level% --js_output_file hgs.min.js
echo Done.