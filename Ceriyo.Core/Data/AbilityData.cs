using System;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores ability data.
    /// </summary>
    public class AbilityData: IDataDomainObject
    {
        /// <inheritdoc />
        public string GlobalID { get; set; }

        /// <inheritdoc />
        [SerializationIgnore]
        public string DirectoryName => "Area";

        /// <summary>
        /// The name of the ability
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The tag of the ability
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// The resource reference of the ability
        /// </summary>
        public string Resref { get; set; }

        /// <summary>
        /// Whether or not the ability can be manually triggered by players.
        /// </summary>
        public bool IsPassive { get; set; }

        /// <summary>
        /// The resref of the script located on the OnActivated script event.
        /// </summary>
        public string OnActivated { get; set; }

        /// <summary>
        /// The description of the ability.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The comments located on the ability. Not viewable in-game.
        /// </summary>
        public string Comment { get; set; }


        /// <summary>
        /// Constructs a new AbilityData object.
        /// </summary>
        public AbilityData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
