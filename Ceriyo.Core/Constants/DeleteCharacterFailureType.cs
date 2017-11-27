namespace Ceriyo.Core.Constants
{
    /// <summary>
    /// When a character is deleted, the server will return one of these values.
    /// </summary>
    public enum DeleteCharacterFailureType: byte
    {
        /// <summary>
        /// An unknown error occurred.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// The delete was successful.
        /// </summary>
        Success = 1,
        /// <summary>
        /// The character file does not exist on the server.
        /// </summary>
        FileDoesNotExist = 2,
        /// <summary>
        /// The server does not allow character deletion.
        /// </summary>
        ServerDoesNotAllowDeletion = 3,

    }
}
