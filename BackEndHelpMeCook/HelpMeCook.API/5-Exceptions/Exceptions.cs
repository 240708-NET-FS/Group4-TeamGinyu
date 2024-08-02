namespace HelpMeCook.API.Exceptions;

public class InvalidUserException : Exception
{
    public InvalidUserException(){}
    public InvalidUserException(string message) : base(message){}
    public InvalidUserException(string message, Exception inner) : base(message, inner){}
}

public class InvalidLoginException : Exception
{
    public InvalidLoginException(){}
    public InvalidLoginException(string message) : base(message){}
    public InvalidLoginException(string message, Exception inner) : base(message, inner){}
}

public class InvalidRecipeException : Exception
{
    public InvalidRecipeException(){}
    public InvalidRecipeException(string message) : base(message){}
    public InvalidRecipeException(string message, Exception inner) : base(message, inner){}
}