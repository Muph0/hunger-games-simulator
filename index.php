<!DOCTYPE html>
<html>
<head>
    <title>Hunger Games Sim</title>

    <script type="text/javascript" src="./jquery.min.js"></script>
    <script type="text/javascript" src="./CanvasConsole/CanvasConsole.js"></script>
    <meta charset="utf-8">

<?php
    // TEMPORARY:
    function scandir_r($dir, &$result = array())
    {
        $files = scandir($dir);
        foreach ($files as $f) {
            if (!is_dir("$dir/$f")) {
                $result[] = "$dir/$f";
            } else if ($f != '.' && $f != '..') {
                scandir_r("$dir/$f", $result);
            }
        }
        return $result;
    }

    foreach (scandir_r('./game') as $f) {
        if (pathinfo($f, PATHINFO_EXTENSION) === 'js')
            echo '    <script type="text/javascript" src="'.$f.'"></script>'."\n";
    }
    ?>
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
    <input id="keyconsumer" type="text" style="opacity: 0; position: absolute; top: 0; left: 0;" autofocus>
    <img id="ascii" src="CanvasConsole/ascii.png" style="display: none;">
</body>
</html>
