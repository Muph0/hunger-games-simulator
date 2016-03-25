using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace hunger_games_simulator.core.networking
{
    [Serializable]
    class ServerResponse
    {
        public ResponseType Purpose;
        public object[] Data;

        public ServerResponse()
        {
            
        }
        
        public static ServerResponse ReceiveFrom(Stream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            ServerResponse packet = (ServerResponse)formatter.Deserialize(stream);
            return packet;
        }
        public void SendTo(Stream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
        }

        public enum ResponseType
        {
            Handshake = 0, // (int)ID
            Surroundings,
            ActionDenied,
            ActionAllowed,
            NewTurn,
        }
    }
}
