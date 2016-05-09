// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: InteropFunctionAttribute.cs
// Version: 20160507

using System;

namespace Meta.Media.Interop
{
    /// <summary>
    /// A base class for FFMPEG functions.
    /// </summary>
    [AttributeUsage(AttributeTargets.Delegate, AllowMultiple = false)]
    public abstract class InteropFunctionAttribute : InteropAttribute
    {
        /// <summary>
        /// Create interop function with function name.
        /// </summary>
        /// <param name="name">function name</param>
        public InteropFunctionAttribute(String name)
        {
            Name = name;
        }

        /// <summary>
        /// Get the name of this function.
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// Create delegate for this function.
        /// </summary>
        /// <returns></returns>
        public abstract Delegate CreateDelegate();
    }
}