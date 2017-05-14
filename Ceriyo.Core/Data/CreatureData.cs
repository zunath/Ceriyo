using System;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class CreatureData : IDataDomainObject
    {
        public string GlobalID { get; set; }

        public virtual string Name { get; set; }

        public string Tag { get; set; }

        public string Resref { get; set; }

        public string ClassResref { get; set; }

        public int Level { get; set; }

        public string DialogResref { get; set; }

        public string OnConversation { get; set; }

        public string OnAttacked { get; set; }

        public string OnDamaged { get; set; }

        public string OnDeath { get; set; }

        public string OnDisturbed { get; set; }

        public string OnHeartbeat { get; set; }

        public string OnSpawned { get; set; }

        public LocalVariableData LocalVariables { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public CreatureData()
        {
            GlobalID = Guid.NewGuid().ToString();
            LocalVariables = new LocalVariableData();
            Level = 1;
        }
    }
}
