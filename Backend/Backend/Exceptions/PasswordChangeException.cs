using System.Runtime.Serialization;

namespace Backend.Exceptions;

[Serializable]
public class PasswordChangeException : Exception
{
    public PasswordChangeException(string message) : base(message)
    {
    }

    protected PasswordChangeException(SerializationInfo info, StreamingContext context) : base(info, context)
    { }
}