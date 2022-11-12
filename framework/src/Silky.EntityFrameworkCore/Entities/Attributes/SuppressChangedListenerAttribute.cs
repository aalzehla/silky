using System;

namespace Silky.EntityFrameworkCore.Entities.Attributes
{
    /// <summary>
    /// No entity monitoring
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class SuppressChangedListenerAttribute : Attribute
    {
    }
}