
WebSocketServer = require('ws').Server;
perlin = require('perlin-noise');
fs = require('fs');

var rl = require('readline').createInterface({
  input: process.stdin,
  output: process.stdout,
  terminal: false
});

var server;

rl.on('line', function(line){
    if (line === 'restart')
    {
        server.Stop();
        init();
    }
});

var init = function()
{
    // include files the old way
    eval(fs.readFileSync('Server.js')+'');
    eval(fs.readFileSync('Random.js')+'');
    eval(fs.readFileSync('HaltonSet.js')+'');
    eval(fs.readFileSync('level/LevelGenerator.js')+'');

    eval(fs.readFileSync('../game/level/Arena.js')+'');
    eval(fs.readFileSync('../game/level/Tile.js')+'');

    server = new Server();
    server.Start(12321);
}

init();