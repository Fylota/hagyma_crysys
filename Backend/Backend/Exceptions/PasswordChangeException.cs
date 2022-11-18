namespace Backend.Exceptions;

public class PasswordChangeException : Exception
{
    public PasswordChangeException() : base()
    {
        
    }

    public PasswordChangeException(string message) : base(message)
    {
        
    }
}