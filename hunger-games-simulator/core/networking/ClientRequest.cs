using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace hunger_games_simulator.core.networking
{
    class ClientRequest
    {
        public RequestType Purpose;
        public int ClientID;
        public object[] Data;

        public ClientRequest(int ClientID)
        {
            this.ClientID = ClientID;
        }

        public enum RequestType
        {
            Connect = 0, // { (string)PlayerName }
            Ping, // { }
            SendMeSurroundings,
            Action,
        }
        public static ClientRequest ReceiveFrom(Stream stream)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                ClientRequest packet = (ClientRequest)formatter.Deserialize(stream);
                return packet;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void SendTo(Stream stream)
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
