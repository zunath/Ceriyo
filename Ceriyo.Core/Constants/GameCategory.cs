using System.ComponentModel;

namespace Ceriyo.Core.Constants
{
    /// <summary>
    /// Standard game category listings.
    /// </summary>
    public enum GameCategory
    {
        /// <summary>
        /// Action server category
        /// </summary>
        [Description("Action")]
        Action = 1,

        /// <summary>
        /// Story server category
        /// </summary>
        [Description("Story")]
        Story = 2,

        /// <summary>
        /// Role Play server category
        /// </summary>
        [Description("Role Play")]
        RolePlay = 3,

        /// <summary>
        /// Team server category
        /// </summary>
        [Description("Team")]
        Team = 4,

        /// <summary>
        /// Melee server category
        /// </summary>
        [Description("Melee")]
        Melee = 5,

        /// <summary>
        /// Social server category
        /// </summary>
        [Description("Social")]
        Social = 6,

        /// <summary>
        /// Alternative server category
        /// </summary>
        [Description("Alternative")]
        Alternative = 7,

        /// <summary>
        /// Persistent World Action server category
        /// </summary>
        [Description("PW Action")]
        PWAction = 8,

        /// <summary>
        /// Persistent World Story server category
        /// </summary>
        [Description("PW Story")]
        PWStory = 9,

        /// <summary>
        /// Solo server category
        /// </summary>
        [Description("Solo")]
        Solo = 10
    }
}
