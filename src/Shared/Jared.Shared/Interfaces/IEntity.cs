﻿namespace Jared.Shared.Interfaces;

public interface IEntity<TKey> : IEntity
{
    new TKey Id { get; set; }
}

public interface IEntity
{
    object Id { get; set; }
}
