using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HungerGames.world;

namespace HungerGames
{
    public class ServerOptions
    {
        public ServerOptions()
        {
            Port = 14253;
            Arena = new Arena(10, 10);
            AiSetup = "<WIP>";
            Nazev = "Hunger Server";
        }

        public short Port { get; set; }
        public Arena Arena { get; set; }
        public string AiSetup { get; set; }
        public string Nazev { get; set; }
    }
}
