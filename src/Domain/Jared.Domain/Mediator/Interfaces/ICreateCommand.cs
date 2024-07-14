namespace Jared.Domain.Mediator.Interfaces;

public interface ICreateCommand : IDataCommand
{
}

public interface ICreateCommand<out T> : ICreateCommand, IDataCommand<T>
{
}
