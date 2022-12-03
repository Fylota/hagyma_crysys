using System.Runtime.Serialization;

namespace Backend.Exceptions;
[Serializable]
public class CommentNotFoundException : Exception
{
    public CommentNotFoundException()
    {
        
    }
    protected CommentNotFoundException(SerializationInfo info, StreamingContext context): base(info, context)
    { }
}