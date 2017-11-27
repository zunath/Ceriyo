using Artemis.Interface;

namespace Ceriyo.Core.Components
{
    /// <summary>
    /// Tracks a class's level and experience required to next level.
    /// </summary>
    public class ClassLevel: IComponent
    {
        /// <summary>
        /// The current level of the class.
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// The amount of experience required for the next level.
        /// </summary>
        public int ExperienceRequired { get; set; }
    }
}
