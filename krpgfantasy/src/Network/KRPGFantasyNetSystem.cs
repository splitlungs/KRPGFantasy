using System;
using Vintagestory.API.Net;
using ProtoBuf;
using Vintagestory.API.Common;
using Vintagestory.API.Server;
using Vintagestory.API.Client;

namespace KRPGLib.Net
{
    public class KRPGFantasyNetSystem : ModSystem
    {
        public const string ChannelID = "krpgfantasynet";
        ICoreClientAPI cApi;
        ICoreServerAPI sApi;
        public static IClientNetworkChannel cChannel;
        public static IServerNetworkChannel sChannel;
        public event Action<IPlayer, StatsPacket> C2S_OnStatsPacket;
        public override double ExecuteOrder()
        {
            // Ensure we load before the rest of the ModSystems
            return 0.05;
        }
        public override void StartClientSide(ICoreClientAPI api)
        {
            base.StartClientSide(api);
            cApi = api;
            cChannel = api.Network.RegisterChannel(ChannelID)
                .RegisterMessageType(typeof(StatsPacket));
        }
        public override void StartServerSide(ICoreServerAPI api)
        {
            base.StartServerSide(api);
            sApi = api;
            sChannel = api.Network.RegisterChannel(ChannelID)
                .RegisterMessageType(typeof(StatsPacket))
                .SetMessageHandler<StatsPacket>(C2S_OnReceiveStatsPacket);
        }
        private void C2S_OnReceiveStatsPacket(IPlayer player, StatsPacket pkt) => C2S_OnStatsPacket?.Invoke(player, pkt);

        /// <summary>Send a data accept/reject response to a specific player.</summary>
        // public void SendResponse(IPlayer player, ResponsePacket packet) => sChannel.SendPacket(player, packet);
    }
}