// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: PixelDescriptor.cs
// Version: 20160509

using System;
using System.Runtime.InteropServices;

namespace Meta.Media.Interop.Util
{
    // TODO: Structure operation.
    public struct ComponentDescriptor
    {
        private ushort _data;
    }

    // TODO: Structure operation.
    public unsafe struct PixelFormatDescriptor
    {
        private byte* _name;

        /// <summary>
        ///     The number of components each pixel has, (1-4)
        /// </summary>
        private byte _componentCount;

        /// <summary>
        ///     Amount to shift the luma width right to find the chroma width.
        ///     <para></para>
        ///     For YV12 this is 1 for example.
        ///     <para></para>
        ///     chroma_width = -((-luma_width) >> log2_chroma_w)
        ///     <para></para>
        ///     The note above is needed to ensure rounding up.
        ///     <para></para>
        ///     This value only refers to the chroma components.
        /// </summary>
        private byte _log2ChromaW;

        /// <summary>
        ///     Amount to shift the luma height right to find the chroma height.
        ///     <para></para>
        ///     For YV12 this is 1 for example.
        ///     <para></para>
        ///     chroma_height= -((-luma_height) >> log2_chroma_h)
        ///     <para></para>
        ///     The note above is needed to ensure rounding up.
        ///     <para></para>
        ///     This value only refers to the chroma components.
        /// </summary>
        private byte _log2ChromaH;

        private PixelFormatFlags _flags;

        /// <summary>
        ///     Parameters that describe how pixels are packed.
        ///     <para></para>
        ///     If the format has 2 or 4 components, then alpha is last.
        ///     <para></para>
        ///     If the format has 1 or 2 components, then luma is 0.
        ///     <para></para>
        ///     If the format has 3 or 4 components:
        ///     <para></para>
        ///     - if the RGB flag is set then 0 is red, 1 is green and 2 is blue;
        ///     <para></para>
        ///     - otherwise 0 is luma, 1 is chroma-U and 2 is chroma-V.
        /// </summary>
        private ComponentDescriptor[] _comp;

        /// <summary>
        ///     Alternative comma-separated names.
        /// </summary>
        private byte* _alias;
    }

    public struct PixelFormatDescriptorPtr
    {
        internal PixelFormatDescriptorPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public PixelFormatDescriptor Struct => Marshal.PtrToStructure<PixelFormatDescriptor>(Value);

        public static bool operator ==(PixelFormatDescriptorPtr value1, PixelFormatDescriptorPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(PixelFormatDescriptorPtr value1, PixelFormatDescriptorPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(PixelFormatDescriptorPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PixelFormatDescriptorPtr))
            {
                return false;
            }

            return Equals((PixelFormatDescriptorPtr) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(PixelFormatDescriptor)";
        }
    }

    [Flags]
    public enum PixelFormatFlags : byte
    {
        /// <summary>
        ///     Pixel format is big-endian.
        /// </summary>
        BigEndian = (1 << 0),

        /// <summary>
        ///     Pixel format has a palette in data[1], values are indexes in this palette.
        /// </summary>
        PAL = (1 << 1),

        /// <summary>
        ///     All values of a component are bit-wise packed end to end.
        /// </summary>
        Bitstream = (1 << 2),

        /// <summary>
        ///     Pixel format is an HW accelerated format.
        /// </summary>
        HWAcceLerated = (1 << 3),

        /// <summary>
        ///     At least one pixel component is not in the first data plane.
        /// </summary>
        Planar = (1 << 4),

        /// <summary>
        ///     The pixel format contains RGB-like data (as opposed to YUV/grayscale).
        /// </summary>
        RGB = (1 << 5),

        /// <summary>
        ///     The pixel format is "pseudo-paletted". This means that it contains a
        ///     fixed palette in the 2nd plane but the palette is fixed/constant for each
        ///     PIX_FMT. This allows interpreting the data as if it was PAL8, which can
        ///     in some cases be simpler. Or the data can be interpreted purely based on
        ///     the pixel format without using the palette.
        ///     <para></para>
        ///     An example of a pseudo-paletted format is <see cref="PixelFormat.Gray8" />
        /// </summary>
        PseudoPAL = (1 << 6),

        /// <summary>
        ///     The pixel format has an alpha channel. This is set on all formats that
        ///     support alpha in some way. The exception is <see cref="PixelFormat.PAL8" />, which can
        ///     carry alpha as part of the palette. Details are explained in the
        ///     PixelFormat enum, and are also encoded in the corresponding
        ///     PixFmtDescriptor.
        ///     <para></para>
        ///     The alpha is always straight, never pre-multiplied.
        ///     <para></para>
        ///     If a codec or a filter does not support alpha, it should set all alpha to
        ///     opaque, or use the equivalent pixel formats without alpha component, e.g.
        ///     <see cref="PixelFormat.RGBX" /> (or <see cref="PixelFormat.RGB24" /> etc.) instead of
        ///     <see cref="PixelFormat.RGBA" />.
        /// </summary>
        Alpha = (1 << 7)
    }

    [Flags]
    public enum LossType
    {
        /// <summary>
        ///     loss due to resolution change
        /// </summary>
        Resolution = 0x0001,

        /// <summary>
        ///     loss due to color depth change
        /// </summary>
        Depth = 0x0002,

        /// <summary>
        ///     loss due to color space conversion
        /// </summary>
        ColorSpace = 0x0004,

        /// <summary>
        ///     loss of alpha bits
        /// </summary>
        Alpha = 0x0008,

        /// <summary>
        ///     loss due to color quantization
        /// </summary>
        ColorQuant = 0x0010,

        /// <summary>
        ///     loss of chroma (e.g. RGB to gray conversion)
        /// </summary>
        Chroma = 0x0020
    }

    /// <summary>
    ///     Read a line from an image, and write the values of the pixel format component c to dst.
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="data">the array containing the pointers to the planes of the image, 4 items.</param>
    /// <param name="lineSize">the array containing the line sizes of the image, 4 items.</param>
    /// <param name="desc">the pixel format descriptor for the image</param>
    /// <param name="x">the horizontal coordinate of the first pixel to read</param>
    /// <param name="y">the vertical coordinate of the first pixel to read</param>
    /// <param name="c"></param>
    /// <param name="w">the width of the line to read, that is the number of values to write to dst</param>
    /// <param name="readPalComponent">
    ///     if not zero and the format is a paletted format writes the values corresponding to the
    ///     palette component c in data[1] to dst, rather than the palette indexes in data[0]. The behavior is undefined if the
    ///     format is not paletted.
    /// </param>
    [UtilFunction("av_read_image_line")]
    public unsafe delegate void ReadImageLine(
        ushort* dst, byte*[] data, int[] lineSize, PixelFormatDescriptorPtr desc, int x, int y, int c, int w,
        int readPalComponent);

    /// <summary>
    ///     Write the values from src to the pixel format component c of an image line.
    /// </summary>
    /// <param name="src">array containing the values to write</param>
    /// <param name="data">the array containing the pointers to the planes of the image, 4 items.</param>
    /// <param name="lineSize">the array containing the line sizes of the image, 4 items.</param>
    /// <param name="desc">the pixel format descriptor for the image</param>
    /// <param name="x">the horizontal coordinate of the first pixel to write</param>
    /// <param name="y">the vertical coordinate of the first pixel to write</param>
    /// <param name="c"></param>
    /// <param name="w">the width of the line to write, that is the number of values to write to dst</param>
    [UtilFunction("av_write_image_line")]
    public unsafe delegate void WriteImageLine(
        ushort* src, byte*[] data, int[] lineSize, PixelFormatDescriptorPtr desc, int x, int y, int c, int w);

    /// <summary>
    ///     Return the pixel format corresponding to name.
    ///     <para></para>
    ///     If there is no pixel format with name name, then looks for a pixel format with the name corresponding to the native
    ///     endian format of name.
    ///     <para></para>
    ///     For example in a little-endian system, first looks for "gray16", then for "gray16le".
    ///     Finally if no pixel format has been found, returns <see cref="PixelFormat.None" />.
    /// </summary>
    /// <param name="name"></param>
    [UtilFunction("av_get_pix_fmt")]
    public unsafe delegate PixelFormat GetPixelFormat(byte* name);

    /// <summary>
    ///     Return the short name for a pixel format, NULL in case format is unknown.
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    /// <remarks>
    ///     Seealso:
    ///     <seealso cref="GetPixelFormat" />
    ///     <seealso cref="GetPixelFormatString" />
    /// </remarks>
    [UtilFunction("av_get_pix_fmt_name")]
    public unsafe delegate byte* GetPixelFormatName(PixelFormat format);

    /// <summary>
    ///     Print in buf the string corresponding to the pixel format with number format, or a header if format is negative.
    /// </summary>
    /// <param name="buffer">the buffer where to write the string</param>
    /// <param name="bufferSize">the size of buffer</param>
    /// <param name="format">
    ///     the number of the pixel format to print the corresponding info string, or a negative value to
    ///     print the corresponding header.
    /// </param>
    /// <returns></returns>
    [UtilFunction("av_get_pix_fmt_string")]
    public unsafe delegate byte* GetPixelFormatString(byte* buffer, int bufferSize, PixelFormat format);

    /// <summary>
    ///     Return the number of bits per pixel used by the pixel format described by pixdesc. Note that this is not the same
    ///     as the number of bits per sample.
    ///     <para></para>
    ///     The returned number of bits refers to the number of bits actually used for storing the pixel information, that is
    ///     padding bits are not counted.
    /// </summary>
    /// <param name="pixdesc"></param>
    /// <returns></returns>
    [UtilFunction("av_get_bits_per_pixel")]
    public delegate int GetBitsPerPixel(PixelFormatDescriptorPtr pixdesc);

    /// <summary>
    ///     Return the number of bits per pixel for the pixel format described by pixdesc, including any padding or unused
    ///     bits.
    /// </summary>
    /// <param name="pixdesc"></param>
    /// <returns></returns>
    [UtilFunction("av_get_padded_bits_per_pixel")]
    public delegate int GetPaddedBitsPerPixel(PixelFormatDescriptorPtr pixdesc);

    /// <summary>
    ///     Return a pixel format descriptor for provided pixel format or NULL if this pixel format is unknown.
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    [UtilFunction("av_pix_fmt_desc_get")]
    public delegate PixelFormatDescriptorPtr GetPixelFormatDescriptor(PixelFormat format);

    /// <summary>
    ///     Iterate over all pixel format descriptors known to libavutil.
    /// </summary>
    /// <param name="prev">previous descriptor. NULL to get the first descriptor.</param>
    /// <returns>next descriptor or NULL after the last descriptor</returns>
    [UtilFunction("av_pix_fmt_desc_next")]
    public delegate PixelFormatDescriptorPtr GetNextPixelFormatDescriptor(PixelFormatDescriptorPtr prev);

    /// <summary>
    ///     Return an PixelFormat id described by desc, or <see cref="PixelFormat.None" /> if desc is not a valid pointer to
    ///     a pixel format descriptor.
    /// </summary>
    /// <returns></returns>
    [UtilFunction("av_pix_fmt_desc_get_id")]
    public delegate PixelFormat GetPixelFormatDescriptorId(PixelFormatDescriptorPtr desc);

    /// <summary>
    ///     Utility function to access log2_chroma_w log2_chroma_h from the pixel format PixFmtDescriptor.
    ///     See av_get_chroma_sub_sample() for a function that asserts a valid pixel format instead of returning an error code.
    ///     Its recommended that you use avcodec_get_chroma_sub_sample unless you do check the return code!
    /// </summary>
    /// <param name="format">the pixel format</param>
    /// <param name="hShift">store log2_chroma_w</param>
    /// <param name="vShift">store log2_chroma_h</param>
    /// <returns>0 on success, ERROR(ENOSYS) on invalid or unknown pixel format</returns>
    [UtilFunction("av_pix_fmt_get_chroma_sub_sample")]
    // TODO: Link to av_get_chroma_sub_sample().
    // TODO: Error Code.
    public unsafe delegate int PixelFormatGetChromaSubSample(PixelFormat format, int* hShift, int* vShift);

    /// <summary>
    ///     Return number of planes in format, a negative ERROR if format is not a valid pixel format.
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    [UtilFunction("av_pix_fmt_count_planes")]
    // TODO: Error Code.
    public delegate int PixelFormatGetPlanesCount(PixelFormat format);

    /// <summary>
    ///     Utility function to swap the endianness of a pixel format.
    /// </summary>
    /// <param name="format">the pixel format</param>
    /// <returns>pixel format with swapped endianness if it exists, otherwise _PIX_FMT_NONE</returns>
    [UtilFunction("av_pix_fmt_swap_endianness")]
    public delegate PixelFormat SwapPixelFormatEndianness(PixelFormat format);

    /// <summary>
    ///     Compute what kind of losses will occur when converting from one specific pixel format to another.
    ///     <para></para>
    ///     When converting from one pixel format to another, information loss may occur.
    ///     <para></para>
    ///     For example, when converting from RGB24 to GRAY, the color information will be lost. Similarly, other losses occur
    ///     when converting from some formats to other formats. These losses can involve loss of chroma, but also loss of
    ///     resolution, loss of color depth, loss due to the color space conversion, loss of the alpha bits or loss due to
    ///     color quantization.
    ///     <para></para>
    ///     <see cref="GetPixelFormatLossType" /> informs you about the various types of losses which will occur when
    ///     converting from one pixel format to another.
    /// </summary>
    /// <param name="dstFormat">destination pixel format</param>
    /// <param name="srcFormat">source pixel format</param>
    /// <param name="hasAlpha">Whether the source pixel format alpha channel is used.</param>
    /// <returns>Combination of flags informing you what kind of losses will occur (maximum loss for an invalid dstFormat).</returns>
    [UtilFunction("av_get_pix_fmt_loss")]
    public delegate LossType GetPixelFormatLossType(PixelFormat dstFormat, PixelFormat srcFormat, int hasAlpha);

    [UtilFunction("av_find_best_pix_fmt_of_2")]
    public delegate PixelFormat FindBestPixelFormatOf(
        PixelFormat dstFormat1, PixelFormat dstFormat2, PixelFormat srcFormat, int hasAlpha, ref LossType lossPtr);

    /// <summary>
    ///     Return the name for provided color range or NULL if unknown.
    /// </summary>
    /// <param name="range"></param>
    /// <returns></returns>
    [UtilFunction("av_color_range_name")]
    public unsafe delegate byte* GetColorRangeName(ColorRange range);

    /// <summary>
    ///     Return the name for provided color primaries or NULL if unknown.
    /// </summary>
    /// <param name="primaries"></param>
    /// <returns></returns>
    [UtilFunction("av_color_primaries_name")]
    public unsafe delegate byte* GetColorPrimariesName(ColorPrimaries primaries);

    /// <summary>
    ///     Return the name for provided color transfer or NULL if unknown.
    /// </summary>
    /// <param name="transfer"></param>
    /// <returns></returns>
    [UtilFunction("av_color_transfer_name")]
    public unsafe delegate byte* GetColorTransferCharacteristicName(ColorTransferCharacteristic transfer);

    /// <summary>
    ///     Return the name for provided color space or NULL if unknown.
    /// </summary>
    /// <param name="space"></param>
    /// <returns></returns>
    [UtilFunction("av_color_space_name")]
    public unsafe delegate byte* GetColorSpaceName(ColorSpace space);

    /// <summary>
    ///     Return the name for provided chroma location or NULL if unknown.
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    [UtilFunction("av_chroma_location_name")]
    public unsafe delegate byte* GetChromaLocationName(ChromaLocation location);
}