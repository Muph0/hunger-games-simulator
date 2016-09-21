<!DOCTYPE html>
<html>
<head>
    <title>Hunger Games Sim</title>

    <script type="text/javascript" src="./jquery.min.js"></script>
    <script type="text/javascript" src="./hgs.min.js"></script>
    <meta charset="utf-8">

    <style type="text/css">
        * {
            margin: 0;
            padding: 0;
        }
        body {
            background-color: #111;
        }
    </style>
</head>
<body onload="main(this)" onkeydown="KEYBOARD.keydown(event)" onkeyup="KEYBOARD.keyup(event)" onkeypress="KEYBOARD.keypress(event)" onclick="document.getElementById('keyconsumer').focus();">
    <input type="text" style="opacity: 0; position: absolute; top: 0; left: 0;" autofocus>
    <img id="ascii" src="CanvasConsole/ascii.png" style="display: none;">
</body>
</html>
