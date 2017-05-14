using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class PCData: CreatureData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string Name => FirstName + " " + LastName;
    }
}
