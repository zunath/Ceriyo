using System.ComponentModel;

namespace Ceriyo.Core.Constants
{
    /// <summary>
    /// The type of Player-Versus-Player rules
    /// </summary>
    public enum PVPType
    {
        /// <summary>
        /// No player-versus-player
        /// </summary>
        [Description("None")]
        None = 1,

        /// <summary>
        /// Players within the same party cannot attack each other.
        /// </summary>
        [Description("Party")]
        Party = 2,

        /// <summary>
        /// All players can attack all other players, even if in the same party.
        /// </summary>
        [Description("Full")]
        Full = 3
    }
}
