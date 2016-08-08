
function main(body)
{
	var img = document.getElementById('ascii');

	var Console = new CanvasConsole(80, 25, img);
	Console.CreateCanvas();

	var game = new GameBase(Console);
	game.Start();
}