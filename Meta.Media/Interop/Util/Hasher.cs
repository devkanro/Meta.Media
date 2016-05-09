// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: Hasher.cs
// Version: 20160509

using System;

namespace Meta.Media.Interop.Util
{
    public enum CrcId
    {
        Crc8Atm,
        Crc16Ansi,
        Crc16Ccitt,
        Crc32Ieee,

        /// <summary>
        ///     reversed bitorder version of <see cref="Crc32Ieee" />
        /// </summary>
        Crc32IeeeLe,

        /// <summary>
        ///     reversed bitorder version of <see cref="Crc16Ansi" />
        /// </summary>
        Crc16AnsiLe,

        Crc24Ieee = 12
    }

    public struct HashContextPtr
    {
        internal HashContextPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public static bool operator ==(HashContextPtr value1, HashContextPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(HashContextPtr value1, HashContextPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(HashContextPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is HashContextPtr))
            {
                return false;
            }

            return Equals((HashContextPtr) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(HashContext)";
        }
    }

    public struct MD5ContextPtr
    {
        internal MD5ContextPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public static bool operator ==(MD5ContextPtr value1, MD5ContextPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(MD5ContextPtr value1, MD5ContextPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(MD5ContextPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MD5ContextPtr))
            {
                return false;
            }

            return Equals((MD5ContextPtr)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(MD5Context)";
        }
    }

    public struct MurMur3ContextPtr
    {
        internal MurMur3ContextPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public static bool operator ==(MurMur3ContextPtr value1, MurMur3ContextPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(MurMur3ContextPtr value1, MurMur3ContextPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(MurMur3ContextPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MurMur3ContextPtr))
            {
                return false;
            }

            return Equals((MurMur3ContextPtr)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(MurMur3Context)";
        }
    }

    /// <summary>
    ///     Calculate the Adler32 checksum of a buffer.
    ///     <para />
    ///     Passing the return value to a subsequent <see cref="Adler32Update" /> call allows the checksum of multiple buffers
    ///     to be calculated as though they were concatenated.
    /// </summary>
    /// <param name="adler">initial checksum value</param>
    /// <param name="buffer">pointer to input buffer</param>
    /// <param name="lenght">size of input buffer</param>
    /// <returns>updated checksum</returns>
    [UtilFunction("av_adler32_update")]
    public unsafe delegate uint Adler32Update(uint adler, byte* buffer, uint lenght);

    /// <summary>
    ///     Initialize a CRC table.
    /// </summary>
    /// <param name="context">must be an array of size sizeof(uint)*257 or sizeof(uint)*1024</param>
    /// <param name="le">
    ///     If 1, the lowest bit represents the coefficient for the highest exponent of the corresponding polynomial (both for
    ///     poly and actual CRC).
    ///     <para></para>
    ///     If 0, you must swap the CRC parameter and the result of <see cref="Crc" /> if you need the standard representation
    ///     (can be simplified in most cases to e.g. bswap16): av_bswap32(crc &lt;&lt; (32-bits))
    /// </param>
    /// <param name="bits">number of bits for the CRC</param>
    /// <param name="poly">generator polynomial without the x**bits coefficient, in the representation as specified by le</param>
    /// <param name="contextSize">size of ctx in bytes</param>
    /// <returns>negative on failure</returns>
    [UtilFunction("av_crc_init")]
    public unsafe delegate int CrcInitialize(uint* context, int le, int bits, uint poly, int contextSize);

    /// <summary>
    ///     Get an initialized standard CRC table.
    /// </summary>
    /// <param name="id">ID of a standard CRC</param>
    /// <returns>a pointer to the CRC table or NULL on failure</returns>
    [UtilFunction("av_crc_get_table")]
    public unsafe delegate uint* CrcGetTable(CrcId id);

    /// <summary>
    ///     Calculate the CRC of a block.
    /// </summary>
    /// <param name="context">CRC of previous blocks if any or initial value for CRC</param>
    /// <param name="crc"></param>
    /// <param name="buffer"></param>
    /// <param name="length"></param>
    /// <returns>updated with the data from the given block</returns>
    [UtilFunction("av_crc")]
    public unsafe delegate uint Crc(uint* context, uint crc,
        byte* buffer, uint length);

    /// <summary>
    /// Allocate an AVMD5 context.
    /// </summary>
    /// <returns></returns>
    [UtilFunction("av_md5_alloc")]
    public delegate MD5ContextPtr MD5Alloc();

    /// <summary>
    /// Initialize MD5 hashing.
    /// </summary>
    /// <param name="context">pointer to the function context (of size av_md5_size)</param>
    [UtilFunction("av_md5_init")]
    public delegate void MD5Initialize(MD5ContextPtr context);

    /// <summary>
    /// Update hash value.
    /// </summary>
    /// <param name="context">hash function context</param>
    /// <param name="src">input data to update hash with</param>
    /// <param name="len">input data length</param>
    [UtilFunction("av_md5_update")]
    public unsafe delegate void MD5Update(MD5ContextPtr context, byte* src, int len);

    /// <summary>
    /// Finish hashing and output digest value.
    /// </summary>
    /// <param name="context">hash function context</param>
    /// <param name="dst">buffer where output digest value is stored</param>
    [UtilFunction("av_md5_final")]
    public unsafe delegate void MD5Final(MD5ContextPtr context, byte* dst);
    
    /// <summary>
    /// Hash an array of data.
    /// </summary>
    /// <param name="dst">The output buffer to write the digest into</param>
    /// <param name="src">The data to hash</param>
    /// <param name="len">The length of the data, in bytes</param>
    [UtilFunction("av_md5_sum")]
    public unsafe delegate void MD5Sum(byte* dst, byte* src, int len);
    
    [UtilFunction("av_murmur3_alloc")]
    public delegate MurMur3ContextPtr MurMur3Alloc();
    [UtilFunction("av_murmur3_init")]
    public delegate void MurMur3Initialize(MurMur3ContextPtr context);
    [UtilFunction("av_murmur3_init_seeded")]
    public delegate void MurMur3InitializeSeeded(MurMur3ContextPtr context, ulong seed);
    [UtilFunction("av_murmur3_update")]
    public unsafe delegate void MurMur3Update(MurMur3ContextPtr context, byte* src, int len);
    [UtilFunction("av_murmur3_final")]
    public delegate void MurMur3Final(MurMur3ContextPtr context, byte[] dst);

    /// <summary>
    ///     Allocate a hash context for the algorithm specified by name.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="name"></param>
    /// <returns>>= 0 for success, a negative error code for failure</returns>
    /// <remarks>The context is not initialized, you must call <see cref="HashInitialize"/>.</remarks>
    [UtilFunction("av_hash_alloc")]
    public unsafe delegate int HashAlloc(out HashContextPtr context, byte* name);

    /// <summary>
    ///     Get the names of available hash algorithms.
    ///     <para></para>
    ///     This function can be used to enumerate the algorithms.
    /// </summary>
    /// <param name="index">index of the hash algorithm, starting from 0</param>
    /// <returns>a pointer to a static string or NULL if i is out of range</returns>
    [UtilFunction("av_hash_names")]
    public unsafe delegate byte* HashNames(int index);

    /// <summary>
    ///     Get the name of the algorithm corresponding to the given hash context.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    [UtilFunction("av_hash_get_name")]
    public unsafe delegate byte* HashGetName(HashContextPtr context);

    /// <summary>
    ///     Get the size of the resulting hash value in bytes.
    ///     <para></para>
    ///     The pointer passed to av_hash_final have space for at least this many bytes.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    [UtilFunction("av_hash_get_size")]
    public delegate int HashGetSize(HashContextPtr context);

    /// <summary>
    ///     Initialize or reset a hash context.
    /// </summary>
    /// <param name="context"></param>
    [UtilFunction("av_hash_init")]
    public delegate void HashInitialize(HashContextPtr context);

    /// <summary>
    ///     Update a hash context with additional data.
    /// </summary>
    [UtilFunction("av_hash_update")]
    public unsafe delegate void HashUpdate(HashContextPtr context, byte* src, int len);

    /// <summary>
    ///     Finalize a hash context and compute the actual hash value.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="dst"></param>
    [UtilFunction("av_hash_final")]
    public unsafe delegate void HashFinal(HashContextPtr context, byte* dst);

    /// <summary>
    ///     Finalize a hash context and compute the actual hash value.
    ///     If size is smaller than the hash size, the hash is truncated;
    ///     if size is larger, the buffer is padded with 0.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="dst"></param>
    /// <param name="size"></param>
    [UtilFunction("av_hash_final_bin")]
    public unsafe delegate void HashFinalBin(HashContextPtr context, byte* dst, int size);

    /// <summary>
    ///     Finalize a hash context and compute the actual hash value as a hex string. The string is always 0-terminated. If
    ///     size is smaller than 2 * hash_size + 1, the hex string is truncated.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="dst"></param>
    /// <param name="size"></param>
    [UtilFunction("av_hash_final_hex")]
    public unsafe delegate void HashFinalHex(HashContextPtr context, byte* dst, int size);

    /// <summary>
    ///     Finalize a hash context and compute the actual hash value as a base64 string. The string is always 0-terminated.
    ///     If size is smaller than _BASE64_SIZE(hash_size), the base64 string is truncated.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="dst"></param>
    /// <param name="size"></param>
    [UtilFunction("av_hash_final_b64")]
    public unsafe delegate void HashFinalBase64(HashContextPtr context, byte* dst, int size);

    /// <summary>
    ///     Free hash context.
    /// </summary>
    /// <param name="context"></param>
    [UtilFunction("av_hash_freep")]
    public delegate void HashFinalFreeP(ref HashContextPtr context);
}