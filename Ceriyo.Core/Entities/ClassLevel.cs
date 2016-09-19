using Artemis.Interface;

namespace Ceriyo.Core.Entities
{
    public class ClassLevel: IComponent
    {
        public int Level { get; set; }
        public int ExperienceRequired { get; set; }
    }
}
