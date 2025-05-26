namespace Shared.Models;

public class RequestModel<T>
{
    public T Request { get; set; } = default!;
}