// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: Camellia.cs
// Version: 20160508

using System;

namespace Meta.Media.Interop.Util
{
    /// <summary>
    ///     A pointer of Camellia context.
    /// </summary>
    public struct CamelliaContextPtr
    {
        internal CamelliaContextPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public static bool operator ==(CamelliaContextPtr value1, CamelliaContextPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(CamelliaContextPtr value1, CamelliaContextPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(CamelliaContextPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CamelliaContextPtr))
            {
                return false;
            }

            return Equals((CamelliaContextPtr) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(CamelliaContext)";
        }
    }

    /// <summary>
    ///     Camellia key bits type.
    /// </summary>
    public enum CamelliaKeyBits
    {
        /// <summary>
        ///     128 bits Camellia key.
        /// </summary>
        Camellia128 = 128,

        /// <summary>
        ///     192 bits Camellia key.
        /// </summary>
        Camellia192 = 192,

        /// <summary>
        ///     256 bits Camellia key.
        /// </summary>
        Camellia256 = 256
    }

    /// <summary>
    ///     Camellia mode.
    /// </summary>
    public enum CamelliaMode
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
    ///     Initialize a Camellia context.
    /// </summary>
    /// <param name="context">a Camellia context</param>
    /// <param name="key">a key of 16, 24, 32 bytes used for encryption/decryption</param>
    /// <param name="keyBits">number of keybits: possible are 128, 192, 256</param>
    /// <returns></returns>
    [UtilFunction("av_camellia_init")]
    public unsafe delegate int CamelliaInitialize(CamelliaContextPtr context, byte* key, CamelliaKeyBits keyBits);

    /// <summary>
    ///     Encrypt or decrypt a buffer using a previously initialized context
    /// </summary>
    /// <param name="context">a Camellia context</param>
    /// <param name="dst">destination array, can be equal to src</param>
    /// <param name="src">source array, can be equal to dst</param>
    /// <param name="count">count number of 16 byte blocks</param>
    /// <param name="iv">initialization vector for CBC mode, NULL for ECB mode</param>
    /// <param name="mode">0 for encryption, 1 for decryption</param>
    [UtilFunction("av_camellia_crypt")]
    public unsafe delegate void CamelliaCrypt(
        CamelliaContextPtr context, byte* dst, byte src, int count, byte iv, CamelliaMode mode);
}