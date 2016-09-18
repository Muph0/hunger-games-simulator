@echo off
setlocal EnableDelayedExpansion

set level=SIMPLE_OPTIMIZATIONS
set level=ADVANCED_OPTIMIZATIONS

echo Wrapping...
jswrapper CanvasConsole/*.js game/*.js -o hgs.wrapped.js
echo Compressing...
java -jar compiler.jar hgs.wrapped.js --assume_function_wrapper --compilation_level %level% --js_output_file hgs.min.js
echo Done.
rm hgs.wrapped.js
