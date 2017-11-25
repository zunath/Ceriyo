using System;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    public class PlaceableData : IDataDomainObject
    {
        public string GlobalID { get; set; }
        [SerializationIgnore]
        public string DirectoryName => "Placeable";

        public string Name { get; set; }

        public string Tag { get; set; }

        public string Resref { get; set; }

        public string Description { get; set; }

        public string Comment { get; set; }

        public bool IsStatic { get; set; }

        public bool IsPlot { get; set; }

        public bool IsUseable { get; set; }

        public bool IsLocked { get; set; }

        public bool IsKeyRequired { get; set; }

        public bool AutoRemoveKey { get; set; }

        public string KeyTag { get; set; }

        public string OnClosed { get; set; }

        public string OnOpened { get; set; }

        public string OnDamaged { get; set; }

        public string OnDeath { get; set; }

        public string OnHeartbeat { get; set; }

        public string OnDisturbed { get; set; }

        public string OnLocked { get; set; }

        public string OnUnlocked { get; set; }

        public string OnAttacked { get; set; }

        public string OnUsed { get; set; }

        public LocalVariableData LocalVariables { get; set; }


        public PlaceableData()
        {
            GlobalID = Guid.NewGuid().ToString();
            LocalVariables = new LocalVariableData();
        }
    }
}
