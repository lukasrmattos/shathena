using DTO;
using Network;
using Network.Methods.AC;
using Network.Methods.HC;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Emulator.PacketHandler
{
   public class CharPacketHandler
    {
        public int HandlePacket(string ipPort, uint id, dynamic data)
        {
            if (data == null)
            {
                Display.Show(ShowType.Warn, "Received null packet from " + ipPort + " client!", ServerType.LOGIN);
                return 0;
            }

            DTOBase dto;

            try
            {
                switch (id)
                {
                    case (uint)PacketHeader.HEADER_CH_LOGIN:
                        {
                            Display.Show(ShowType.Info, string.Format("account id {0} is trying to connect...", data.AccountId), ServerType.LOGIN);

                            if (!Core.LoginServer.Tokens.ContainsKey(data.AccountId))
                            {
                                Display.Show(ShowType.Warn, string.Format("Account id {0} tried to login in char-server without a login-token generated.", data.AccountId), ServerType.CHAR);
                                Core.CharServer.Svr.DisconnectClient(ipPort);
                            }
                            else
                            {
                                Tuple<uint, uint> tokens = Core.LoginServer.Tokens[data.AccountId];

                                if (tokens.Item1 == data.Token1 && tokens.Item2 == data.Token2)
                                {
                                    // Why this only works with delay? Some one can help me understand client logic here?
                                    new Thread(delegate ()
                                    {
                                        // send back account_id
                                        Core.CharServer.Svr.Send(ipPort, SendCharacter.Build_099d(null));
                                        Thread.Sleep(10);
                                        Core.CharServer.Svr.Send(ipPort, SendCharacter.Build_Slots(data.AccountId));
                                        Thread.Sleep(10);
                                        Core.CharServer.Svr.Send(ipPort, SendCharacter.Build_Chars(10, 3, 3));
                                        Thread.Sleep(10);

                                        Core.CharServer.Svr.Send(ipPort, SendCharacter.Build_Pages(10));
                                        Thread.Sleep(10);

                                        Core.CharServer.Svr.Send(ipPort, SendCharacter.Build_BlockCharacter());
                                        Thread.Sleep(10);

                                        Core.CharServer.Svr.Send(ipPort, SendCharacter.Build_Pincode(data.AccountId, 0));
                                        Thread.Sleep(10);
                                    }).Start();                                  
                                }
                                else
                                {
                                    Display.Show(ShowType.Warn, string.Format("Account id {0} tried to login in char-server with a invalid token.", data.AccountId), ServerType.CHAR);
                                    Core.CharServer.Svr.DisconnectClient(ipPort);
                                }
                            }
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return 1;
        }
    }
}
