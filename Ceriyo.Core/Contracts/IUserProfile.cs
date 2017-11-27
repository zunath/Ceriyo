﻿namespace Ceriyo.Core.Contracts
{
    /// <summary>
    /// Maintains a player's user profile information.
    /// </summary>
    public interface IUserProfile
    {
        /// <summary>
        /// The master server token is received when the client logs in to their account.
        /// This token is necessary to make calls to the master server API.
        /// </summary>
        string MasterServerToken { get; set; }

    }
}
