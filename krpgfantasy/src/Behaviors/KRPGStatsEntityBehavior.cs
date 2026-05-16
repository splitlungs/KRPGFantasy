using System;
using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;

namespace KRPGLib.Fantasy.Stats
{
    public class KRPGStatsEntityBehavior : EntityBehavior
    {
        ICoreAPI Api;
        public override string PropertyName()
        {
            return "KRPGStatsEntityBehavior";
        }
        public KRPGStatsEntityBehavior(Entity entity) : base(entity)
        {
            Api = entity.Api;
        }
        
    }
}