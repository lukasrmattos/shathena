using Database.DAO.Objs;
using DTO;
using DTO.Objs;
using Network;
using Network.Methods;
using Network.Methods.AC;
using Shared;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Text;
using YamlDotNet.Core.Tokens;

namespace Emulator.Login_Server
{
    public class LoginPacketHandler
    {
        public static uint ConvertFromIpAddressToInteger(string ipAddress)
        {
            var address = IPAddress.Parse(ipAddress);
            byte[] bytes = address.GetAddressBytes();

            // flip big-endian(network order) to little-endian
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToUInt32(bytes, 0);
        }

        public static string ConvertFromIntegerToIpAddress(uint ipAddress)
        {
            byte[] bytes = BitConverter.GetBytes(ipAddress);

            // flip little-endian to big-endian(network order)
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return new IPAddress(bytes).ToString();
        }

        public int HandlePacket(string ipPort, uint id, dynamic data)
        {
            if(data == null)
            {
                Display.Show(ShowType.Warn, "Received null packet from " + ipPort + " client!", ServerType.LOGIN);
                return 0;
            }

            DTOBase dto;

            try
            {
                switch (id)
                {
                    case (uint)PacketHeader.HEADER_CA_LOGIN:
                        Display.Show(ShowType.Info, string.Format("{0} is trying to connect...", data.Username), ServerType.LOGIN);

                        dto = GetPlayerSession(data);

                        if(dto == null)
                            Core.LoginServer.Svr.Send(ipPort, Login_RefuseLogin.Build(1));
                        else
                        {
                            var ps = (PlayerSessionDTO)dto;

                            if (Core.LoginServer.PlayersSession.ContainsKey(ps.AccountId))
                            {
                                Display.Show(ShowType.Warn, string.Format("User with account {0}({1}) is already connected!", ps.Username, ps.AccountId), ServerType.LOGIN);
                                Core.LoginServer.Svr.Send(ipPort, Login_RefuseLogin.Build(8));
                            }
                            else
                            {
                                Core.LoginServer.PlayersSession.TryAdd(ps.AccountId, ps);

                                Random rnd = new Random();
                                var tokens = new Tuple<uint, uint>((uint)rnd.Next(0, 999999), (uint)rnd.Next(0, 999999));

                                Core.LoginServer.Tokens.TryRemove(ps.AccountId, out var oldToken);
                                Core.LoginServer.Tokens.TryAdd(ps.AccountId, tokens);

                                Core.LoginServer.Svr.Send(ipPort, Char_AcceptLogin.Build(ps.AccountId, "127.0.0.1", (ushort)ServerPort.CharPort, tokens));
                            }
                        }

                        break;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return 1;
        }

        public PlayerSessionDTO GetPlayerSession(dynamic login)
        {
            using var dao = new PlayerSessionDAO();
            var dto = dao.GetDto(login);

            return dto;
        }
    }
}
