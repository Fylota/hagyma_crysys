﻿namespace Backend.Exceptions;

public class PasswordChangeException : Exception
{
    public PasswordChangeException(string message) : base(message)
    {
    }
}