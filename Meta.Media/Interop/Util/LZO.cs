// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: LZO.cs
// Version: 20160509

namespace Meta.Media.Interop.Util
{
    public enum LzoResult
    {
        InputDepleted = 1,
        OutputFull = 2,
        InvalidBackptr = 4,
        Error = 8
    }

    /// <summary>
    ///     Decodes LZO 1x compressed data.
    /// </summary>
    /// <param name="out">output buffer</param>
    /// <param name="outLen">size of output buffer, number of bytes left are returned here</param>
    /// <param name="in">input buffer</param>
    /// <param name="inLen">size of input buffer, number of bytes left are returned here</param>
    /// <returns>0 on success, otherwise a combination of the error flags above</returns>
    /// <remarks>
    ///     Make sure all buffers are appropriately padded, in must provide 8, out must provide 12 additional bytes.
    /// </remarks>
    [UtilFunction("av_lzo1x_decode")]
    public unsafe delegate LzoResult av_lzo1x_decode(void* @out, int* outLen, void* @in, int* inLen);
}