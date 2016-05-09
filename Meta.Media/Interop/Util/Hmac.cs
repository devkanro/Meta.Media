// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: Hmac.cs
// Version: 20160509

using System;

namespace Meta.Media.Interop.Util
{
    public enum HmacType
    {
        MD5,
        SHA1,
        SHA224 = 10,
        SHA256,
        SHA384,
        SHA512
    }

    public struct HmacContextPtr
    {
        internal HmacContextPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public static bool operator ==(HmacContextPtr value1, HmacContextPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(HmacContextPtr value1, HmacContextPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(HmacContextPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is HmacContextPtr))
            {
                return false;
            }

            return Equals((HmacContextPtr) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(HmacContext)";
        }
    }

    /// <summary>
    ///     Allocate an Hmac context.
    /// </summary>
    /// <param name="type">type The hash function used for the Hmac.</param>
    /// <returns></returns>
    [UtilFunction("av_hmac_alloc")]
    public delegate HashContextPtr HmacAlloc(HmacType type);

    /// <summary>
    ///     Free an Hmac context.
    /// </summary>
    /// <param name="context">The context to free, may be NULL</param>
    [UtilFunction("av_hmac_free")]
    public delegate void HmacFree(HashContextPtr context);

    /// <summary>
    ///     Initialize an Hmac context with an authentication key.
    /// </summary>
    /// <param name="context">The Hmac context</param>
    /// <param name="key">The authentication key</param>
    /// <param name="keyLength">The length of the key, in bytes</param>
    [UtilFunction("av_hmac_init")]
    public unsafe delegate void HmacInitialize(HashContextPtr context, byte* key, uint keyLength);

    /// <summary>
    ///     Hash data with the Hmac.
    /// </summary>
    /// <param name="context">The Hmac context</param>
    /// <param name="data">The data to hash</param>
    /// <param name="len">The length of the data, in bytes</param>
    [UtilFunction("av_hmac_update")]
    public unsafe delegate void HmacUpdate(HashContextPtr context, byte* data, uint len);

    /// <summary>
    ///     Finish hashing and output the Hmac digest.
    /// </summary>
    /// <param name="context">The Hmac context</param>
    /// <param name="out">The output buffer to write the digest into</param>
    /// <param name="outLength">The length of the out buffer, in bytes</param>
    /// <returns>The number of bytes written to out, or a negative error code.</returns>
    [UtilFunction("av_hmac_final")]
    // TODO: Error Code.
    public unsafe delegate int HmacFinal(HashContextPtr context, byte* @out, uint outLength);

    /// <summary>
    ///     Hash an array of data with a key.
    /// </summary>
    /// <param name="context">The Hmac context</param>
    /// <param name="data">The data to hash</param>
    /// <param name="len">The length of the data, in bytes</param>
    /// <param name="key">The authentication key</param>
    /// <param name="keyLength">The length of the key, in bytes</param>
    /// <param name="out">The output buffer to write the digest into</param>
    /// <param name="outLength">The length of the out buffer, in bytes</param>
    /// <returns>The number of bytes written to out, or a negative error code.</returns>
    [UtilFunction("av_hmac_calc")]
    // TODO: Error Code.
    public unsafe delegate int HmacCalc(
        HashContextPtr context, byte* data, uint len, byte* key, uint keyLength, byte* @out, uint outLength);
}