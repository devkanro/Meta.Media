// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: PixelUtil.cs
// Version: 20160511

namespace Meta.Media.Interop.Util
{
    public unsafe delegate int PixelSADFunction(byte* src1, int stride1, byte* src2, int stride2);

    /// <summary>
    /// Get a potentially optimized pointer to a Sum-of-absolute-differences function (see the <see cref="PixelSADFunction"/> prototype).
    /// </summary>
    /// <param name="widthBits">1&lt;&lt;w_bits is the requested width of the block size</param>
    /// <param name="heightBits">1&lt;&lt;h_bits is the requested height of the block size</param>
    /// <param name="aligned">
    /// If set to 2, the returned sad function will assume src1 and src2 addresses are aligned on the block size.
    /// <para></para>
    /// If set to 1, the returned sad function will assume src1 is aligned on the block size.
    /// <para></para>
    /// If set to 0, the returned sad function assume no particular alignment.
    /// </param>
    /// <param name="logCtx">context used for logging, can be NULL</param>
    /// <returns>a pointer to the SAD function or NULL in case of error (because of invalid parameters)</returns>
    public unsafe delegate PixelSADFunction GetSADFunction(int widthBits, int heightBits, int aligned, void* logCtx);
}