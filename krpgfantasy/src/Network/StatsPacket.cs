using ProtoBuf;

namespace KRPGLib.Net
{
    [ProtoContract]
    public class StatsPacket
    {
        [ProtoMember(1)]
        public string PlayerUID;
        [ProtoMember(2)]
        public int[] Values;
        [ProtoMember(3)]
        public int PointsSpent;
    }
}