using Artemis.Interface;

namespace Ceriyo.Core.Components
{
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
