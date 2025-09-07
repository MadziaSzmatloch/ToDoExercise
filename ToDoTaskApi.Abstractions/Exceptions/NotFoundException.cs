namespace ToDoTaskApi.Abstractions.Exceptions
{
    public class NotFoundException(string message) : Exception(message){ }
}
