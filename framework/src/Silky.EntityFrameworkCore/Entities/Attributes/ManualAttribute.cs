using System;

namespace Silky.EntityFrameworkCore.Entities.Attributes
{
    /// <summary>
    /// Manually configure entity properties
    /// </summary>
    /// <remarks>Support classes and methods</remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ManualAttribute : Attribute
    {
    }
}