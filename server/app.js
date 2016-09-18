
var fs = require('fs'),
    PATH = require('path'),
    ini = require('ini');
    WebSocketServer = require('ws').Server,
    perlin = require('perlin-noise'),
    window = {};

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
        process.exit(0);
    }
});

var scandir = function(dirname)
{
    if (dirname.match(/node_modules/)) return [];

    var files = [];

    fs.readdirSync(dirname).forEach(function(elem)
    {
        var file_path = PATH.resolve(dirname, elem);

        if (fs.statSync(file_path).isDirectory())
        {
            scandir(file_path).forEach(function(ff)
            {
                files.push(ff);
            });
        }
        else
        {
            files.push(PATH.relative(__dirname, file_path));
        }
    });

    return files.filter(function(elem){return elem.match(/.*\.js$/) && !elem.match(/app\.js/); });
}
var run = function(file)
{
    eval(fs.readFileSync(file)+'');
}

var init = function()
{
    var includes =  scandir('./')
            .concat(scandir('../game')
        );

    debugger;
    for (var i = 0; i < includes.length; i++)
    {
        includes[i] = fs.readFileSync(includes[i]);
    }

    eval(includes.join('\n'));
    debugger;

    SETTINGS = ini.parse(fs.readFileSync('./settings.ini', 'utf8'));

    Biome.RegisterBiomes();

    server = new Server();
    server.Start(SETTINGS.network.port);
}

init();