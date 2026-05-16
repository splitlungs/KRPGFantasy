using ProtoBuf;

namespace KRPGLib.Net
{
    /// <summary>
    /// Generic acknowledgement / rejection response for data channel requests.
    /// WatchedAttributes are auto-synced by the engine so no data payload is needed.
    /// </summary>
    [ProtoContract]
    public class ResponsePacket
    {
        public enum RequestType : int
        {
            Stats = 0,
            Class = 1,
            FeatUnlock = 2
        }

        [ProtoMember(1)]
        public int Request { get; set; }   // cast from RequestKind

        [ProtoMember(2)]
        public bool Success { get; set; }

        [ProtoMember(3)]
        public string Message { get; set; }
    }
}