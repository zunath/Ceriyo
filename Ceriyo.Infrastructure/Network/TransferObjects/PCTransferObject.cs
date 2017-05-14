using Ceriyo.Core.Data;
using ProtoBuf;

namespace Ceriyo.Infrastructure.Network.TransferObjects
{
    [ProtoContract]
    public class PCTransferObject
    {
        [ProtoMember(1)]
        public string FirstName { get; set; }
        [ProtoMember(2)]
        public string LastName { get; set; }
        [ProtoMember(3)]
        public string GlobalID { get; set; }
        [ProtoMember(4)]
        public int Level { get; set; }
        [ProtoMember(5)]
        public string Description { get; set; }

        public string Name => FirstName + " " + LastName;

        public static PCTransferObject Load(PCData data)
        {
            PCTransferObject pcTO = new PCTransferObject
            {
                FirstName = data.FirstName,
                LastName = data.LastName,
                GlobalID = data.GlobalID,
                Level = data.Level,
                Description = data.Description

            };

            return pcTO;
        }
    }
}
