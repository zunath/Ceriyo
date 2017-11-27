namespace Ceriyo.Core.Data
{
    /// <summary>
    /// Stores Player Character data.
    /// </summary>
    public class PCData: CreatureData
    {
        /// <summary>
        /// The first name of the PC.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the PC.
        /// </summary>
        public string LastName { get; set; }
    }
}
