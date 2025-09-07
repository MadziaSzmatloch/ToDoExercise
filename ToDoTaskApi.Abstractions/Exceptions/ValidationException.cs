namespace ToDoTaskApi.Abstractions.Exceptions
{
    public class ValidationException(string message) : Exception(message)
    {
    }
}
