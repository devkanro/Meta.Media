// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: UtilFunctionAttribute.cs
// Version: 20160507

using System;

namespace Meta.Media.Interop
{
    /// <summary>
    /// Function attribute for avutil.
    /// </summary>
    public class UtilFunctionAttribute : InteropFunctionAttribute
    {
        /// <summary>
        /// Create interop function with function name.
        /// </summary>
        /// <param name="name">function name</param>
        public UtilFunctionAttribute(String name) : base(name)
        {

        }

        /// <summary>
        /// Create delegate for this function.
        /// </summary>
        /// <returns></returns>
        public override Delegate CreateDelegate()
        {
            // TODO: Create delegate for avutil functions.
            throw new NotImplementedException();
        }
    }
}