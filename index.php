<!DOCTYPE html>
<html>
<head>
	<title>Hunger games sim</title>

	<script type="text/javascript" src="./CanvasConsole/CanvasConsole.js"></script>

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
			margin: 0; padding: 0;
		}
	</style>
</head>
<body onload="main(this)">
	<img id="ascii" src="CanvasConsole/ascii.png" style="display: none;">
</body>
</html>
