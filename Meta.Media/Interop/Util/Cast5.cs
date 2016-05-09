// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: Cast5.cs
// Version: 20160508

using System;

namespace Meta.Media.Interop.Util
{
    /// <summary>
    ///     A pointer of Cast5 context.
    /// </summary>
    public struct Cast5ContextPtr
    {
        internal Cast5ContextPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public static bool operator ==(Cast5ContextPtr value1, Cast5ContextPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(Cast5ContextPtr value1, Cast5ContextPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(Cast5ContextPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Cast5ContextPtr))
            {
                return false;
            }

            return Equals((Cast5ContextPtr) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(Cast5Context)";
        }
    }

    /// <summary>
    ///     Cast5 mode.
    /// </summary>
    public enum Cast5Mode
    {
        /// <summary>
        ///     For encryption.
        /// </summary>
        Encryption,

        /// <summary>
        ///     For decryption.
        /// </summary>
        Decryption
    }

    /// <summary>
    ///     Allocate an Cast5 context.
    /// </summary>
    /// <returns></returns>
    [UtilFunction("av_cast5_alloc")]
    public delegate Cast5ContextPtr Cast5Alloc();

    /// <summary>
    ///     Initialize an Cast5 context.
    /// </summary>
    /// <param name="context">Cast5 context</param>
    /// <param name="key">a key of 5,6,...16 bytes used for encryption/decryption</param>
    /// <param name="keyBits">number of keybits: possible are 40,48,...,128</param>
    /// <returns></returns>
    [UtilFunction("av_cast5_init")]
    public unsafe delegate int Cast5Initialize(Cast5ContextPtr context, byte* key, int keyBits);

    /// <summary>
    ///     Encrypt or decrypt a buffer using a previously initialized context.
    /// </summary>
    /// <param name="context">Cast5 context</param>
    /// <param name="dst">destination array, can be equal to src</param>
    /// <param name="src">source array, can be equal to dst</param>
    /// <param name="count"> umber of 8 byte blocks</param>
    /// <param name="mode">Cast5 mode</param>
    [UtilFunction("av_cast5_crypt")]
    public unsafe delegate void Cast5Crypt(Cast5ContextPtr context, byte* dst, byte* src, int count, Cast5Mode mode);

    /// <summary>
    ///     Encrypt or decrypt a buffer using a previously initialized context.
    /// </summary>
    /// <param name="context">Cast5 context</param>
    /// <param name="dst">destination array, can be equal to src</param>
    /// <param name="src">source array, can be equal to dst</param>
    /// <param name="count"> umber of 8 byte blocks</param>
    /// <param name="iv">initialization vector for CBC mode, if NULL then ECB will be used</param>
    /// <param name="mode">Cast5 mode</param>
    [UtilFunction("av_cast5_crypt2")]
    public unsafe delegate void Cast5Crypt2(
        Cast5ContextPtr context, byte* dst, byte* src, int count, byte* iv, Cast5Mode mode);
}