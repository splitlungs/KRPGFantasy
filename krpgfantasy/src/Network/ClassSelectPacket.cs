using System.IO;
using ProtoBuf;
using Vintagestory.API.Common;

namespace KRPGLib.Fantasy.Stats
{
    /// <summary>
    /// Simple packet that only carries the name of the class the player selected.
    /// </summary>
    [ProtoContract]
    public class ClassSelectPacket : IByteSerializable
    {
        [ProtoMember(1)]
        public string ClassName { get; set; }
        public void ToBytes(BinaryWriter writer)
        {
            writer.Write(ClassName);
        }
        public void FromBytes(BinaryReader reader, IWorldAccessor world)
        {
            reader.ReadString();
        }
    }
}