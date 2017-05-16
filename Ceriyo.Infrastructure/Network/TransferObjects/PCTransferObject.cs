using Ceriyo.Core.Data;
using EmptyKeys.UserInterface.Mvvm;
using ProtoBuf;

namespace Ceriyo.Infrastructure.Network.TransferObjects
{
    [ProtoContract]
    public class PCTransferObject: BindableBase
    {
        private string _firstName;

        [ProtoMember(1)]
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;

        [ProtoMember(2)]
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private string _globalID;

        [ProtoMember(3)]
        public string GlobalID
        {
            get { return _globalID; }
            set { SetProperty(ref _globalID, value); }
        }

        private int _level;

        [ProtoMember(4)]
        public int Level
        {
            get { return _level; }
            set { SetProperty(ref _level, value); }
        }

        private string _description;

        [ProtoMember(5)]
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

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
