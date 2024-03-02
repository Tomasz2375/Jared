namespace Jared.Domain.Mediator.Interfaces;

public interface IUpdateCommand : IDataCommand
{
}

public interface IUpdateCommand<out T> : ICreateCommand, IDataCommand<T>
{
}
