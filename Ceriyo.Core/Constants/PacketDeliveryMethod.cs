namespace Ceriyo.Core.Constants
{
    public enum PacketDeliveryMethod
    {
        /// <summary>
        /// This is just UDP. Messages can be lost or received more than once. 
        /// Messages may not be received in the same order as they were sent.
        /// </summary>
        Unreliable,
        /// <summary>
        /// Using this delivery method messages can still be lost; but you're protected against duplicated messages. 
        /// If a message arrives late; that is, if a message sent after this one has already been received - it will be dropped. 
        /// This means you will never receive "older" data than what you already have received.
        /// </summary>
        UnreliableSequenced,
        /// <summary>
        /// This delivery method ensures that every message sent will be eventually received. 
        /// It does not however guarantee what order they will be received in; late messages may be delivered before newer ones.
        /// </summary>
        ReliableUnordered,
        /// <summary>
        /// This delivery method is similar to UnreliableSequenced; except that it guarantees that SOME messages will be received - 
        /// if you only send one message - it will be received. 
        /// If you sent two messages quickly, and they get reordered in transit, 
        /// only the newest message will be received - but at least ONE of them will be received.
        /// </summary>
        ReliableSequenced,
        /// <summary>
        /// This delivery method guarantees that messages will always be received in the exact order they were sent.
        /// </summary>
        ReliableOrdered
    }
}
