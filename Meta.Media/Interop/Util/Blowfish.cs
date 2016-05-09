// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: Blowfish.cs
// Version: 20160508

using System;
using System.Runtime.InteropServices;

namespace Meta.Media.Interop.Util
{
    public struct Blowfish
    {
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4, SizeConst = 18)]
        public uint[] P;
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U4, SizeConst = 4 * 256)]
        public uint[][] S;
    }

    public struct BlowfishPtr
    {
        internal BlowfishPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public Blowfish Struct => Marshal.PtrToStructure<Blowfish>(Value);

        public static bool operator ==(BlowfishPtr value1, BlowfishPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(BlowfishPtr value1, BlowfishPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(BlowfishPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BlowfishPtr))
            {
                return false;
            }

            return Equals((BlowfishPtr)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(Blowfish)";
        }
    }

    public enum BlowfishMode
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
    /// Initialize an Blowfish context.
    /// </summary>
    /// <param name="context">an Blowfish context</param>
    /// <param name="key">a key</param>
    /// <param name="keyLength">length of the key</param>
    [UtilFunction("av_blowfish_init")]
    public unsafe delegate void BlowfishInitialize(BlowfishPtr context, byte* key, int keyLength);

    /// <summary>
    /// Encrypt or decrypt a buffer using a previously initialized context.
    /// </summary>
    /// <param name="context">an Blowfish context</param>
    /// <param name="dst">destination array, can be equal to src</param>
    /// <param name="src">source array, can be equal to dst</param>
    /// <param name="count">count number of 8 byte blocks</param>
    /// <param name="iv">initialization vector for CBC mode, if NULL ECB will be used</param>
    /// <param name="mode">crypt mode</param>
    [UtilFunction("av_blowfish_crypt")]
    public unsafe delegate void BlowfishCrypt(BlowfishPtr context, byte* dst, byte* src, int count, byte* iv, BlowfishMode mode);

    /// <summary>
    /// Encrypt or decrypt a buffer using a previously initialized context.
    /// </summary>
    /// <param name="context">an Blowfish context</param>
    /// <param name="xl">left four bytes halves of input to be encrypted</param>
    /// <param name="xr">right four bytes halves of input to be encrypted</param>
    /// <param name="mode">crypt mode</param>
    [UtilFunction("av_blowfish_crypt_ecb")]
    public unsafe delegate void BlowfishCryptEcb(BlowfishPtr context, uint* xl, uint* xr, BlowfishMode mode);
}