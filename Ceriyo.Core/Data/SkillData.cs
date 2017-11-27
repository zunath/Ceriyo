using System;
using Ceriyo.Core.Attributes;
using Ceriyo.Core.Data.Contracts;

namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores skill data
    /// </summary>
    public class SkillData : IDataDomainObject
    {
        /// <inheritdoc />
        public string GlobalID { get; set; }

        /// <inheritdoc />
        [SerializationIgnore]
        public string DirectoryName => "Skill";

        /// <summary>
        /// The name of the skill.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The tag of the skill.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// The resref of the skill.
        /// </summary>
        public string Resref { get; set; }

        /// <summary>
        /// The description of the skill.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The comment of the skill.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Whether or not this skill is passive.
        /// Passive skills cannot be activated.
        /// </summary>
        public bool IsPassive { get; set; }

        /// <summary>
        /// The script resref fired when the skill is activated.
        /// </summary>
        public string OnActivated { get; set; }

        /// <summary>
        /// Constructs a new skill data object.
        /// </summary>
        public SkillData()
        {
            GlobalID = Guid.NewGuid().ToString();
        }
    }
}
