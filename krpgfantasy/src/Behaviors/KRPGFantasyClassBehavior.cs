using System;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Server;

namespace KRPGLib.Fantasy
{
    public class KRPGFantasyClassBehavior : EntityBehavior
    {
        ICoreAPI Api;
        public override string PropertyName()
        {
            return "KRPGFantasyClassBehavior";
        }
        public KRPGFantasyClassBehavior(Entity entity) : base(entity)
        {
            Api = entity.Api;
        }
        public override void OnReceivedClientPacket(IServerPlayer player, int packetid, byte[] data, ref EnumHandling handled)
        {
            base.OnReceivedClientPacket(player, packetid, data, ref handled);

            if (packetid == 133716)
            {
            }
        }
    }
}