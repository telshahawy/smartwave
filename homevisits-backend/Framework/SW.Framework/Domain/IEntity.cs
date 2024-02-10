using System;

namespace SW.Framework.Domain
{
    /// <summary>
    ///     Interface IEntity
    /// </summary>
    /// <typeparam name="TId">The type of the primary identifier.</typeparam>
    public interface IEntity<TId> : ICloneable, IEquatable<IEntity<TId>> where TId : struct
    {
        TId Id { get; }
    }
}