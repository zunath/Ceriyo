using System;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores class level data.
    /// </summary>
    public class ClassLevelData: IDataDomainObject
    {
        /// <inheritdoc />
        public string GlobalID { get; set; }

        /// <inheritdoc />
        [SerializationIgnore]
        public string DirectoryName => null;

        /// <summary>
        /// The level 
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Amount of experience needed for the next level.
        /// </summary>
        public int ExperienceRequired { get; set; }

        /// <summary>
        /// Constructs a new class level data object.
        /// </summary>
        public ClassLevelData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
