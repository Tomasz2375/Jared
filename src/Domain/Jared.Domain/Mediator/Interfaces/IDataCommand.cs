namespace Jared.Domain.Mediator.Interfaces;

public interface IDataCommand
{
}

public interface IDataCommand<out T> : IDataCommand
{
    public T Data { get; }
}
