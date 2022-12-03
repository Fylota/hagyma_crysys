using System.Runtime.Serialization;

namespace Backend.Exceptions;

[Serializable]
public class UserNotFoundException : Exception
{
    public UserNotFoundException()
    {

    }
    protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    { }
}