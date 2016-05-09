// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: ImageUtil.cs
// Version: 20160509

namespace Meta.Media.Interop.Util
{
    /// <summary>
    ///     Compute the max pixel step for each plane of an image with a format described by pixdesc.
    ///     <para></para>
    ///     The pixel step is the distance in bytes between the first byte of the group of bytes which describe a pixel
    ///     component and the first byte of the successive group in the same plane for the same component.
    /// </summary>
    /// <param name="maxPixsteps">
    ///     an array which is filled with the max pixel step for each plane. Since a plane may contain
    ///     different pixel components, the computed max_pixsteps[plane] is relative to the component in the plane with the max
    ///     pixel step, 4 items.
    /// </param>
    /// <param name="maxPixstepComps">
    ///     an array which is filled with the component for each plane which has the max pixel step.
    ///     May be NULL, 4 items.
    /// </param>
    /// <param name="pixdesc"></param>
    [UtilFunction("av_image_fill_max_pixsteps")]
    public delegate void ImageFillMaxPixsteps(int[] maxPixsteps, int[] maxPixstepComps, PixelFormatDescriptorPtr pixdesc
        );

    /// <summary>
    ///     Compute the size of an image line with format and width for the plane.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="width"></param>
    /// <param name="plane"></param>
    /// <returns>the computed size in bytes</returns>
    [UtilFunction("av_image_get_linesize")]
    public delegate int ImageGetLineSize(PixelFormat format, int width, int plane);

    /// <summary>
    ///     Fill plane lineSizes for an image with pixel format and width.
    /// </summary>
    /// <param name="lineSizes">linesizes array to be filled with the linesize for each plane, 4 items</param>
    /// <param name="format"></param>
    /// <param name="width"></param>
    /// <returns>>= 0 in case of success, a negative error code otherwise</returns>
    [UtilFunction("av_image_fill_linesizes")]
    // TODO: Error Code.
    public delegate int ImageFillLineSizes(int[] lineSizes, PixelFormat format, int width);

    /// <summary>
    ///     Fill plane data pointers for an image with pixel format and height.
    /// </summary>
    /// <param name="data">pointers array to be filled with the pointer for each image plane, 4 items</param>
    /// <param name="format"></param>
    /// <param name="height"></param>
    /// <param name="ptr">the pointer to a buffer which will contain the image</param>
    /// <param name="linesizes">
    ///     the array containing the linesize for each plane, should be filled by
    ///     <see cref="ImageFillLineSizes" />, 4 items
    /// </param>
    /// <returns>the size in bytes required for the image buffer, a negative error code in case of failure</returns>
    [UtilFunction("av_image_fill_pointers")]
    // TODO: Error Code.
    public unsafe delegate int ImageFillPointers(
        byte*[] data, PixelFormat format, int height, byte* ptr, int[] linesizes);

    /// <summary>
    ///     Allocate an image with size w and h and pixel format, and fill pointers and lineSizes accordingly.
    ///     <para></para>
    ///     The allocated image buffer has to be freed by using av_freep(&pointers[0]).
    /// </summary>
    /// <param name="pointers"></param>
    /// <param name="linesizes"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="format"></param>
    /// <param name="align">the value to use for buffer size alignment</param>
    /// <returns>the size in bytes required for the image buffer, a negative error code in case of failure</returns>
    [UtilFunction("av_image_alloc")]
    // TODO: Link to av_freep().
    // TODO: Error Code.
    public unsafe delegate int ImageAlloc(
        byte*[] pointers, int[] linesizes, int width, int height, PixelFormat format, int align);

    /// <summary>
    ///     Copy image plane from src to dst.
    ///     <para></para>
    ///     That is, copy "height" number of lines of "bytewidth" bytes each.
    ///     <para></para>
    ///     The first byte of each successive line is separated by *_linesize bytes.
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="dstLineSize">linesize for the image plane in dst</param>
    /// <param name="src"></param>
    /// <param name="srcLineSize">linesize for the image plane in src</param>
    /// <param name="byteWidth"></param>
    /// <param name="height"></param>
    [UtilFunction("av_image_copy_plane")]
    public unsafe delegate void ImageCopyPlane(
        byte* dst, int dstLineSize, byte* src, int srcLineSize, int byteWidth, int height);

    /// <summary>
    ///     Copy image in srcData to dstData.
    /// </summary>
    /// <param name="dstData"></param>
    /// <param name="dstLineSizes">lineSizes for the image in dstData</param>
    /// <param name="srcData"></param>
    /// <param name="srcLineSizes">lineSizes for the image in srcData</param>
    /// <param name="format"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    [UtilFunction("av_image_copy")]
    public unsafe delegate void ImageCopy(
        byte*[] dstData, int[] dstLineSizes, byte*[] srcData, int[] srcLineSizes, PixelFormat format, int width,
        int height);

    /// <summary>
    ///     Setup the data pointers and lineSizes based on the specified image parameters and the provided array.
    ///     <para></para>
    ///     The fields of the given image are filled in by using the src address which points to the image data buffer.
    ///     Depending on the specified pixel format, one or multiple image data pointers and line sizes will be set.  If a
    ///     planar format is specified, several pointers will be set pointing to the different picture planes and the line
    ///     sizes of the different planes will be stored in the linesSizes array. Call with src == NULL to get the required
    ///     size for the src buffer.
    ///     <para></para>
    ///     To allocate the buffer and fill in the dstData and dstLineSizes in one call, use <see cref="ImageAlloc" />.
    /// </summary>
    /// <param name="dstData">data pointers to be filled in</param>
    /// <param name="dstLineSizes">lineSizes for the image in dst_data to be filled in</param>
    /// <param name="src">buffer which will contain or contains the actual image data, can be NULL</param>
    /// <param name="format">the pixel format of the image</param>
    /// <param name="width">the width of the image in pixels</param>
    /// <param name="height">the height of the image in pixels</param>
    /// <param name="align">the value used in src for lineSize alignment</param>
    /// <returns>the size in bytes required for src, a negative error code in case of failure</returns>
    [UtilFunction("av_image_fill_arrays")]
    // TODO: Error Code.
    public unsafe delegate int ImageFillArrays(
        byte*[] dstData, int[] dstLineSizes, byte* src, PixelFormat format, int width, int height, int align);

    /// <summary>
    ///     Return the size in bytes of the amount of data required to store an image with the given parameters.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="align">align the assumed linesize alignment</param>
    /// <returns></returns>
    [UtilFunction("av_image_fill_arrays")]
    public delegate int ImageGetBufferSize(PixelFormat format, int width, int height, int align);

    /// <summary>
    ///     Copy image data from an image into a buffer.
    ///     <para></para>
    ///     <see cref="ImageGetBufferSize" /> can be used to compute the required size for the buffer to fill.
    /// </summary>
    /// <param name="dst">a buffer into which picture data will be copied</param>
    /// <param name="dstSize">the size in bytes of dst</param>
    /// <param name="srcData">pointers containing the source image data</param>
    /// <param name="srcLineSizes">lineSizes for the image in srcData</param>
    /// <param name="format">the pixel format of the source image</param>
    /// <param name="width">the width of the source image in pixels</param>
    /// <param name="height">the height of the source image in pixels</param>
    /// <param name="align">the assumed linesize alignment for dst</param>
    /// <returns>the number of bytes written to dst, or a negative value (error code) on error</returns>
    [UtilFunction("av_image_copy_to_buffer")]
    // TODO: Error Code.
    public unsafe delegate int ImageCopyToBuffer(
        byte* dst, int dstSize, byte*[] srcData, int[] srcLineSizes, PixelFormat format, int width, int height,
        int align);

    /// <summary>
    ///     Check if the given dimension of an image is valid, meaning that all bytes of the image can be addressed with a
    ///     signed int.
    /// </summary>
    /// <param name="width">the width of the picture</param>
    /// <param name="height">the height of the picture</param>
    /// <param name="logOffset">the offset to sum to the log level for logging with logContext</param>
    /// <param name="logContext">the parent logging context, it may be NULL</param>
    /// <returns>>= 0 if valid, a negative error code otherwise</returns>
    [UtilFunction("av_image_check_size")]
    // TODO: Error Code.
    public unsafe delegate int ImageCheckSize(uint width, uint height, int logOffset, void* logContext);

    /// <summary>
    ///     Check if the given sample aspect ratio of an image is valid.
    ///     <para></para>
    ///     It is considered invalid if the denominator is 0 or if applying the ratio to the image size would make the smaller
    ///     dimension less than 1. If the sar numerator is 0, it is considered unknown and will return as valid.
    /// </summary>
    /// <param name="width">width of the image</param>
    /// <param name="height">height of the image</param>
    /// <param name="sar">sample aspect ratio of the image</param>
    /// <returns>0 if valid, a negative ERROR code otherwise</returns>
    [UtilFunction("av_image_check_sar")]
    // TODO: Error Code.
    public delegate int ImageCheckSar(uint width, uint height, Rational sar);
}