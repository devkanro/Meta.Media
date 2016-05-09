// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: Base64.cs
// Version: 20160508

namespace Meta.Media.Interop.Util
{
    /// <summary>
    /// Decode a base64-encoded string.
    /// </summary>
    /// <param name="out">buffer for decoded data</param>
    /// <param name="in">null-terminated input string</param>
    /// <param name="outSize">size in bytes of the out buffer, must be at least 3/4 of the length of in</param>
    /// <returns>number of bytes written, or a negative value in case of invalid input</returns>
    [UtilFunction("av_base64_decode")]
    public unsafe delegate int Base64Decode(byte* @out, byte* @in, int outSize);

    /// <summary>
    /// Encode data to base64 and null-terminate.
    /// </summary>
    /// <param name="out">buffer for encoded data</param>
    /// <param name="outSize">size in bytes of the out buffer (including the null terminator), must be at least (((inSize)+2) / 3 * 4 + 1)</param>
    /// <param name="in">input buffer containing the data to encode</param>
    /// <param name="inSize">size in bytes of the in buffer</param>
    /// <returns>out or NULL in case of error</returns>
    [UtilFunction("av_base64_encode")]
    public unsafe delegate byte* Base64Encode(byte* @out, int outSize, byte* @in, int inSize);
}