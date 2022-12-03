using System.Runtime.Serialization;

namespace Backend.Exceptions;

[Serializable]
public class InvalidCaffException : Exception
{
    public InvalidCaffException()
    {
        
    }
    protected InvalidCaffException(SerializationInfo info, StreamingContext context) : base(info, context)
    { }
}