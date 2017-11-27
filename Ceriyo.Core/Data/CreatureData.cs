using System;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores creature data.
    /// </summary>
    public class CreatureData : IDataDomainObject
    {
        /// <inheritdoc />
        public string GlobalID { get; set; }

        /// <inheritdoc />
        [SerializationIgnore]
        public string DirectoryName => "Creature";

        /// <summary>
        /// The name of the creature.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// The tag of the creature.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// The resref of the creature.
        /// </summary>
        public string Resref { get; set; }

        /// <summary>
        /// The class resref of the class used by the creature.
        /// </summary>
        public string ClassResref { get; set; }

        /// <summary>
        /// The level of the creature.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The resref of the dialog used by the creature.
        /// </summary>
        public string DialogResref { get; set; }

        /// <summary>
        /// The script resref fired when the creature is conversed with.
        /// </summary>
        public string OnConversation { get; set; }

        /// <summary>
        /// The script resref fired when the creature is attacked.
        /// </summary>
        public string OnAttacked { get; set; }

        /// <summary>
        /// The script resref fired when the creature is damaged.
        /// </summary>
        public string OnDamaged { get; set; }

        /// <summary>
        /// The script resref fired when the creature dies.
        /// </summary>
        public string OnDeath { get; set; }

        /// <summary>
        /// The script resref fired when the creature's inventory is disturbed.
        /// </summary>
        public string OnDisturbed { get; set; }

        /// <summary>
        /// The script resref fired every heartbeat.
        /// </summary>
        public string OnHeartbeat { get; set; }

        /// <summary>
        /// The script resref fired when the creature is spawned.
        /// </summary>
        public string OnSpawned { get; set; }

        /// <summary>
        /// The local variables stored on the creature.
        /// </summary>
        public LocalVariableData LocalVariables { get; set; }

        /// <summary>
        /// The description of the creature.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The comment of the creature.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Constructs a new creature data object.
        /// </summary>
        public CreatureData()
        {
            GlobalID = Guid.NewGuid().ToString();
            LocalVariables = new LocalVariableData();
            Level = 1;
        }
    }
}
