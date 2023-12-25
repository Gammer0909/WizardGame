using System;


public class NotEnoughException : Exception {
    public NotEnoughException() { }
    public NotEnoughException(string message) : base(message) { }
    public NotEnoughException(string message, Exception inner) : base(message, inner) { }
    protected NotEnoughException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}