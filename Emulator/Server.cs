
using DTO.Objs;
using Emulator.Login_Server;
using Emulator.PacketHandler;
using Network;
using Network.Methods;
using Shared;
using SimpleTcp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Emulator
{
    public class Server
    {
        public ServerType Type { get; set; }

        public ConcurrentDictionary<string, FileData> _clients { get; set; }
        public ConcurrentDictionary<ulong, PlayerSessionDTO> PlayersSession { get; set; } // Login ok
        public ConcurrentDictionary<uint, Tuple<uint, uint>> Tokens {get; set; }

        public TcpServer Svr { get; set; }
        public LoginPacketHandler LPH = new LoginPacketHandler();
        public CharPacketHandler CPH = new CharPacketHandler();

        public Server(ushort port, int max_connections, ServerType type)
        {
            Type = type;

            _clients = new ConcurrentDictionary<string, FileData>();
            PlayersSession = new ConcurrentDictionary<ulong, PlayerSessionDTO>();

            if (type != ServerType.MAP)
                Tokens = new ConcurrentDictionary<uint, Tuple<uint, uint>>();

            Svr = new TcpServer("127.0.0.1", port, false, null, null);

            Svr.ClientConnected += NewClientConnection;
            Svr.ClientDisconnected += ClientDisconnection;
            Svr.DataReceived += PacketReceived;

            Svr.Start();
        }

        public void NewClientConnection(object sender, SimpleTcp.ClientConnectedEventArgs e)
        {
            _clients.TryAdd(e.IpPort, new FileData(e.IpPort));

            Display.Show(ShowType.Info, string.Format("Client [{0}] is connected.", e.IpPort), Type);
        }

        public void ClientDisconnection(object sender, SimpleTcp.ClientDisconnectedEventArgs e)
        {
            if (_clients.ContainsKey(e.IpPort))
            {
                _clients.TryRemove(e.IpPort, out var fd);

                if (fd.AccountId > 0 && PlayersSession.ContainsKey(fd.AccountId))
                    PlayersSession.TryRemove(fd.AccountId, out var ps);
            }

            Display.Show(ShowType.Info, string.Format("Client [{0}] disconnected.", e.IpPort), Type);
        }

        public void PacketReceived(object sender, DataReceivedFromClientEventArgs e)
        {
            if(!_clients.ContainsKey(e.IpPort))
            {
                Console.WriteLine("Error!");
                Svr.DisconnectClient(e.IpPort);
                return;
            }

            var fd = _clients[e.IpPort];

            fd.Buffer.Append(e.Data);

            Console.WriteLine(Encoding.Default.GetString(e.Data));

            while (fd.Buffer.PacketAvaliable())
            {
                var x = fd.Buffer.GetPacketHeader();

                if (x.Size == -2)
                {
                    Display.Show(ShowType.Info, string.Format("Unknown Packet received 0x{0:x4} ({0})", x.MethodId), Type);
                    fd.Buffer.Clear();
                    continue;
                }

                var d = fd.Buffer.GetPacketData((int)x.Size);
                var m = Method.GetByID(x.MethodId);

                if (m != null)
                {
                    switch(this.Type)
                    {
                        case ServerType.LOGIN:
                            LPH.HandlePacket(e.IpPort, x.MethodId, m.Parse(x, d));
                            break;

                        case ServerType.CHAR:
                            CPH.HandlePacket(e.IpPort, x.MethodId, m.Parse(x, d));
                            break;
                    }
                }

                fd.Buffer.Consume();
            }
        }
    }
}
