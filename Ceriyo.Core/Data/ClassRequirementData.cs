namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores class requirement data.
    /// </summary>
    public class ClassRequirementData
    {
        /// <summary>
        /// The resref of the class which this requirement is attached to.
        /// </summary>
        public string ClassResref { get; set; }

        /// <summary>
        /// The character level required 
        /// </summary>
        public int LevelRequired { get; set; }
    }
}
