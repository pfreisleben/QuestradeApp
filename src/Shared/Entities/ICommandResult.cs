namespace Shared.Entities
{
    public interface ICommandResult {
    
        List<string> Messages { get; set; }
        bool Succeeded { get; set; }
    }

    public interface ICommandResult<T> : ICommandResult
    {
        T Data { get; set; }
    }
}