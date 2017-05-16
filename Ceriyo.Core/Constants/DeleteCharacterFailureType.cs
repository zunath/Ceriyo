namespace Ceriyo.Core.Constants
{
    public enum DeleteCharacterFailureType: byte
    {
        Unknown = 0,
        Success = 1,
        FileDoesNotExist = 2,
        ServerDoesNotAllowDeletion = 3,

    }
}
