using System.Runtime.Serialization;

namespace Backend.Exceptions;

[Serializable]
public class ImageNotFoundException : Exception
{
    public ImageNotFoundException()
    {
    }

    protected ImageNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    { }
}