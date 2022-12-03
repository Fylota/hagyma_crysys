using System.Runtime.Serialization;

namespace Backend.Exceptions;

[Serializable]
public class NotAllowedException : Exception
{
    public NotAllowedException()
    {
        
    }
    protected NotAllowedException(SerializationInfo info, StreamingContext context) : base(info, context)
    { }
}