// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: AES.cs
// Version: 20160508

using System;

namespace Meta.Media.Interop.Util
{
    /// <summary>
    ///     A pointer of AES context.
    /// </summary>
    public struct AesContextPtr
    {
        internal AesContextPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public static bool operator ==(AesContextPtr value1, AesContextPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(AesContextPtr value1, AesContextPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(AesContextPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is AesContextPtr))
            {
                return false;
            }

            return Equals((AesContextPtr) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(AesContext)";
        }
    }

    /// <summary>
    ///     AES key bits type.
    /// </summary>
    public enum AesKeyBits
    {
        /// <summary>
        ///     128 bits AES key.
        /// </summary>
        Aes128 = 128,

        /// <summary>
        ///     192 bits AES key.
        /// </summary>
        Aes192 = 192,

        /// <summary>
        ///     256 bits AES key.
        /// </summary>
        Aes256 = 256
    }

    /// <summary>
    ///     AES mode.
    /// </summary>
    public enum AesMode
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
    ///     Allocate an AES context.
    /// </summary>
    /// <returns></returns>
    [UtilFunction("av_aes_alloc")]
    public delegate AesContextPtr AesAlloc();

    /// <summary>
    ///     Initialize an AES context.
    /// </summary>
    /// <param name="context">AES context</param>
    /// <param name="key">AES key</param>
    /// <param name="keyBits">128, 192 or 256</param>
    /// <param name="mode">AES mode</param>
    /// <returns></returns>
    [UtilFunction("av_aes_init")]
    public unsafe delegate int AesInitialize(AesContextPtr context, byte* key, AesKeyBits keyBits, AesMode mode);

    /// <summary>
    ///     Encrypt or decrypt a buffer using a previously initialized context.
    /// </summary>
    /// <param name="context">AES context</param>
    /// <param name="dst">destination array, can be equal to src</param>
    /// <param name="src">source array, can be equal to dst</param>
    /// <param name="count">number of 16 byte blocks</param>
    /// <param name="iv">initialization vector for CBC mode, if NULL then ECB will be used</param>
    /// <param name="mode">AES mode</param>
    [UtilFunction("av_aes_crypt")]
    public unsafe delegate void AesCrypt(AesContextPtr context, byte* dst, byte* src, int count, byte* iv, AesMode mode);
}