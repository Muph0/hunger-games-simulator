
var WebSocketServer = require('ws').Server;
var perlin = require('perlin-noise');
var fs = require('fs');
var ini = require('ini');

var SETTINGS = {
    network: {
        port: 7887,
        max_clients: 100,
    },
    game: {
        arena_name: "level",
        entity_cap: 1000,
    },
};

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
    if (line === 'stop')
    {
        server.Stop();
        return 0;
    }
});

var init = function()
{
    // include files the old way
    eval(fs.readFileSync('Server.js')+'');
    eval(fs.readFileSync('Random.js')+'');
    eval( fs.readFileSync('HaltonSet.js') + '');
    eval( fs.readFileSync('level/LevelGenerator.js') + '');
    eval( fs.readFileSync('client_proc/ClientInfo.js') + '');

    // client processors
    eval( fs.readFileSync('client_proc/ClientVerifier.js') + '');
    // lobby mgr
    eval( fs.readFileSync('client_proc/LobbyManager.js') + '');

    eval(fs.readFileSync('../game/Main.js')+'');
    // load arena
    eval(fs.readFileSync('../game/level/Arena.js')+'');
    // load tile assets
    eval(fs.readFileSync('../game/tiles/Tile.js')+'');
    // load biome assets
    eval(fs.readFileSync('../game/biomes/Biome.js')+'');
    eval(fs.readFileSync('../game/biomes/DesertBiome.js')+'');
    eval(fs.readFileSync('../game/biomes/ForestBiome.js')+'');
    eval(fs.readFileSync('../game/biomes/PlainsBiome.js')+'');
    eval(fs.readFileSync('../game/biomes/TundraBiome.js')+'');

    // load character class
    eval(fs.readFileSync('../game/PlayerCharacter.js')+'');

    SETTINGS = ini.parse(fs.readFileSync('./settings.ini', 'utf8'));

    Biome.RegisterBiomes();

    server = new Server();
    server.Start(SETTINGS.network.port);
}

init();