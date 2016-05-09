// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: PixelFormat.cs
// Version: 20160509

namespace Meta.Media.Interop.Util
{
    public enum PixelFormat
    {
        None = -1,

        /// <summary>
        ///     planar YUV 4:2:0, 12bpp, (1 Cr & Cb sample per 2x2 Y samples)
        /// </summary>
        YUV420P,

        /// <summary>
        ///     packed YUV 4:2:2, 16bpp, Y0 Cb Y1 Cr
        /// </summary>
        YUYV422,

        /// <summary>
        ///     packed RGB 8:8:8, 24bpp, RGBRGB...
        /// </summary>
        RGB24,

        /// <summary>
        ///     packed RGB 8:8:8, 24bpp, BGRBGR...
        /// </summary>
        BGR24,

        /// <summary>
        ///     planar YUV 4:2:2, 16bpp, (1 Cr & Cb sample per 2x1 Y samples)
        /// </summary>
        YUV422P,

        /// <summary>
        ///     planar YUV 4:4:4, 24bpp, (1 Cr & Cb sample per 1x1 Y samples)
        /// </summary>
        YUV444P,

        /// <summary>
        ///     planar YUV 4:1:0,  9bpp, (1 Cr & Cb sample per 4x4 Y samples)
        /// </summary>
        YUV410P,

        /// <summary>
        ///     planar YUV 4:1:1, 12bpp, (1 Cr & Cb sample per 4x1 Y samples)
        /// </summary>
        YUV411P,

        /// <summary>
        ///     Y        ,  8bpp
        /// </summary>
        Gray8,

        /// <summary>
        ///     Y        ,  1bpp, 0 is white, 1 is black, in each byte pixels are ordered from the msb to the lsb
        /// </summary>
        MonoWhite,

        /// <summary>
        ///     Y        ,  1bpp, 0 is black, 1 is white, in each byte pixels are ordered from the msb to the lsb
        /// </summary>
        MonoBlack,

        /// <summary>
        ///     8 bit with <see cref="RGB32" /> palette
        /// </summary>
        PAL8,

        /// <summary>
        ///     planar YUV 4:2:0, 12bpp, full scale (JPEG), deprecated in favor of <see cref="YUV420P" /> and setting color_range
        /// </summary>
        YUVJ420P,

        /// <summary>
        ///     planar YUV 4:2:2, 16bpp, full scale (JPEG), deprecated in favor of <see cref="YUV420P" /> and setting color_range
        /// </summary>
        YUVJ422P,

        /// <summary>
        ///     planar YUV 4:4:4, 24bpp, full scale (JPEG), deprecated in favor of <see cref="YUV420P" /> and setting color_range
        /// </summary>
        YUVJ444P,

#if FF_API_XVMC

        /// <summary>
        ///     XVideo Motion Acceleration via common packet passing
        /// </summary>
        XVMC_MPEG2_MC,

        XVMC_MPEG2_IDCT,
#endif

        /// <summary>
        ///     packed YUV 4:2:2, 16bpp, Cb Y0 Cr Y1
        /// </summary>
        UYVY422,

        /// <summary>
        ///     packed YUV 4:1:1, 12bpp, Cb Y0 Y1 Cr Y2 Y3
        /// </summary>
        UYYVYY411,

        /// <summary>
        ///     packed RGB 3:3:2,  8bpp, (msb)2B 3G 3R(lsb)
        /// </summary>
        BGR8,

        /// <summary>
        ///     packed RGB 1:2:1 bitstream,  4bpp, (msb)1B 2G 1R(lsb), a byte contains two pixels, the first pixel in the byte is
        ///     the one composed by the 4 msb bits
        /// </summary>
        BGR4,

        /// <summary>
        ///     packed RGB 1:2:1,  8bpp, (msb)1B 2G 1R(lsb)
        /// </summary>
        BGR4_BYTE,

        /// <summary>
        ///     packed RGB 3:3:2,  8bpp, (msb)2R 3G 3B(lsb)
        /// </summary>
        RGB8,

        /// <summary>
        ///     packed RGB 1:2:1 bitstream,  4bpp, (msb)1R 2G 1B(lsb), a byte contains two pixels, the first pixel in the byte is
        ///     the one composed by the 4 msb bits
        /// </summary>
        RGB4,

        /// <summary>
        ///     packed RGB 1:2:1,  8bpp, (msb)1R 2G 1B(lsb)
        /// </summary>
        RGB4_BYTE,

        /// <summary>
        ///     planar YUV 4:2:0, 12bpp, 1 plane for Y and 1 plane for the UV components, which are interleaved (first byte U and
        ///     the following byte V)
        /// </summary>
        NV12,

        /// <summary>
        ///     as above, but U and V bytes are swapped
        /// </summary>
        NV21,

        /// <summary>
        ///     packed ARGB 8:8:8:8, 32bpp, ARGBARGB...
        /// </summary>
        ARGB,

        /// <summary>
        ///     packed RGBA 8:8:8:8, 32bpp, RGBARGBA...
        /// </summary>
        RGBA,

        /// <summary>
        ///     packed ABGR 8:8:8:8, 32bpp, ABGRABGR...
        /// </summary>
        ABGR,

        /// <summary>
        ///     packed BGRA 8:8:8:8, 32bpp, BGRABGRA...
        /// </summary>
        BGRA,

        /// <summary>
        ///     Y        , 16bpp, big-endian
        /// </summary>
        Gray16BE,

        /// <summary>
        ///     Y        , 16bpp, little-endian
        /// </summary>
        Gray16LE,

        /// <summary>
        ///     planar YUV 4:4:0 (1 Cr & Cb sample per 1x2 Y samples)
        /// </summary>
        YUV440P,

        /// <summary>
        ///     planar YUV 4:4:0 full scale (JPEG), deprecated in favor of _PIX_FMT_YUV440P and setting color_range
        /// </summary>
        YUVJ440P,

        /// <summary>
        ///     planar YUV 4:2:0, 20bpp, (1 Cr & Cb sample per 2x2 Y & A samples)
        /// </summary>
        YUVA420P,

#if FF_API_VDPAU

        /// <summary>
        ///     H.264 HW decoding with VDPAU, data[0] contains a vdpau_render_state struct which contains the bitstream of the
        ///     slices as well as various fields extracted from headers
        /// </summary>
        VDPAU_H264,

        /// <summary>
        ///     MPEG-1 HW decoding with VDPAU, data[0] contains a vdpau_render_state struct which contains the bitstream of the
        ///     slices as well as various fields extracted from headers
        /// </summary>
        VDPAU_MPEG1,

        /// <summary>
        ///     MPEG-2 HW decoding with VDPAU, data[0] contains a vdpau_render_state struct which contains the bitstream of the
        ///     slices as well as various fields extracted from headers
        /// </summary>
        VDPAU_MPEG2,

        /// <summary>
        ///     WMV3 HW decoding with VDPAU, data[0] contains a vdpau_render_state struct which contains the bitstream of the
        ///     slices as well as various fields extracted from headers
        /// </summary>
        VDPAU_WMV3,

        /// <summary>
        ///     VC-1 HW decoding with VDPAU, data[0] contains a vdpau_render_state struct which contains the bitstream of the
        ///     slices as well as various fields extracted from headers
        /// </summary>
        VDPAU_VC1,

#endif

        /// <summary>
        ///     packed RGB 16:16:16, 48bpp, 16R, 16G, 16B, the 2-byte value for each R/G/B component is stored as big-endian
        /// </summary>
        RGB48BE,

        /// <summary>
        ///     packed RGB 16:16:16, 48bpp, 16R, 16G, 16B, the 2-byte value for each R/G/B component is stored as little-endian
        /// </summary>
        RGB48LE,

        /// <summary>
        ///     packed RGB 5:6:5, 16bpp, (msb)   5R 6G 5B(lsb), big-endian
        /// </summary>
        RGB565BE,

        /// <summary>
        ///     packed RGB 5:6:5, 16bpp, (msb)   5R 6G 5B(lsb), little-endian
        /// </summary>
        RGB565LE,

        /// <summary>
        ///     packed RGB 5:5:5, 16bpp, (msb)1X 5R 5G 5B(lsb), big-endian   , X=unused/undefined
        /// </summary>
        RGB555BE,

        /// <summary>
        ///     packed RGB 5:5:5, 16bpp, (msb)1X 5R 5G 5B(lsb), little-endian, X=unused/undefined
        /// </summary>
        RGB555LE,

        /// <summary>
        ///     packed BGR 5:6:5, 16bpp, (msb)   5B 6G 5R(lsb), big-endian
        /// </summary>
        BGR565BE,

        /// <summary>
        ///     packed BGR 5:6:5, 16bpp, (msb)   5B 6G 5R(lsb), little-endian
        /// </summary>
        BGR565LE,

        /// <summary>
        ///     packed BGR 5:5:5, 16bpp, (msb)1X 5B 5G 5R(lsb), big-endian   , X=unused/undefined
        /// </summary>
        BGR555BE,

        /// <summary>
        ///     packed BGR 5:5:5, 16bpp, (msb)1X 5B 5G 5R(lsb), little-endian, X=unused/undefined
        /// </summary>
        BGR555LE,

        /// <summary>
        ///     HW acceleration through VA API at motion compensation entry-point, Picture.data[3] contains a vaapi_render_state
        ///     struct which contains macroblocks as well as various fields extracted from headers
        /// </summary>
        VAAPI_MOCO,

        /// <summary>
        ///     HW acceleration through VA API at IDCT entry-point, Picture.data[3] contains a vaapi_render_state struct which
        ///     contains fields extracted from headers
        /// </summary>
        VAAPI_IDCT,

        /// <summary>
        ///     HW decoding through VA API, Picture.data[3] contains a vaapi_render_state struct which contains the bitstream of
        ///     the slices as well as various fields extracted from headers
        /// </summary>
        VAAPI_VLD,

        /// <summary>
        ///     planar YUV 4:2:0, 24bpp, (1 Cr & Cb sample per 2x2 Y samples), little-endian
        /// </summary>
        YUV420P16LE,

        /// <summary>
        ///     planar YUV 4:2:0, 24bpp, (1 Cr & Cb sample per 2x2 Y samples), big-endian
        /// </summary>
        YUV420P16BE,

        /// <summary>
        ///     planar YUV 4:2:2, 32bpp, (1 Cr & Cb sample per 2x1 Y samples), little-endian
        /// </summary>
        YUV422P16LE,

        /// <summary>
        ///     planar YUV 4:2:2, 32bpp, (1 Cr & Cb sample per 2x1 Y samples), big-endian
        /// </summary>
        YUV422P16BE,

        /// <summary>
        ///     planar YUV 4:4:4, 48bpp, (1 Cr & Cb sample per 1x1 Y samples), little-endian
        /// </summary>
        YUV444P16LE,

        /// <summary>
        ///     planar YUV 4:4:4, 48bpp, (1 Cr & Cb sample per 1x1 Y samples), big-endian
        /// </summary>
        YUV444P16BE,

#if FF_API_VDPAU

        /// <summary>
        ///     MPEG4 HW decoding with VDPAU, data[0] contains a vdpau_render_state struct which contains the bitstream of the
        ///     slices as well as various fields extracted from headers
        /// </summary>
        VDPAU_MPEG4,

#endif

        /// <summary>
        ///     HW decoding through DXVA2, Picture.data[3] contains a LPDIRECT3DSURFACE9 pointer
        /// </summary>
        DXVA2_VLD,

        /// <summary>
        ///     packed RGB 4:4:4, 16bpp, (msb)4X 4R 4G 4B(lsb), little-endian, X=unused/undefined
        /// </summary>
        RGB444LE,

        /// <summary>
        ///     packed RGB 4:4:4, 16bpp, (msb)4X 4R 4G 4B(lsb), big-endian,    X=unused/undefined
        /// </summary>
        RGB444BE,

        /// <summary>
        ///     packed BGR 4:4:4, 16bpp, (msb)4X 4B 4G 4R(lsb), little-endian, X=unused/undefined
        /// </summary>
        BGR444LE,

        /// <summary>
        ///     packed BGR 4:4:4, 16bpp, (msb)4X 4B 4G 4R(lsb), big-endian,    X=unused/undefined
        /// </summary>
        BGR444BE,

        /// <summary>
        ///     8bit gray, 8bit alpha
        /// </summary>
        YA8,

        /// <summary>
        ///     alias for <see cref="YA8" />
        /// </summary>
        Y400A = YA8,

        /// <summary>
        ///     alias for <see cref="YA8" />
        /// </summary>
        GRAY8A = YA8,

        /// <summary>
        ///     packed RGB 16:16:16, 48bpp, 16B, 16G, 16R, the 2-byte value for each R/G/B component is stored as big-endian
        /// </summary>
        BGR48BE,

        /// <summary>
        ///     packed RGB 16:16:16, 48bpp, 16B, 16G, 16R, the 2-byte value for each R/G/B component is stored as little-endian
        /// </summary>
        BGR48LE,

        // The following 12 formats have the disadvantage of needing 1 format for each bit depth.
        // Notice that each 9/10 bits sample is stored in 16 bits with extra padding.
        // If you want to support multiple bit depths, then using YUV420P16* with the bpp stored separately is better.
        /// <summary>
        ///     planar YUV 4:2:0, 13.5bpp, (1 Cr & Cb sample per 2x2 Y samples), big-endian
        /// </summary>
        YUV420P9BE,

        /// <summary>
        ///     planar YUV 4:2:0, 13.5bpp, (1 Cr & Cb sample per 2x2 Y samples), little-endian
        /// </summary>
        YUV420P9LE,

        /// <summary>
        ///     planar YUV 4:2:0, 15bpp, (1 Cr & Cb sample per 2x2 Y samples), big-endian
        /// </summary>
        YUV420P10BE,

        /// <summary>
        ///     planar YUV 4:2:0, 15bpp, (1 Cr & Cb sample per 2x2 Y samples), little-endian
        /// </summary>
        YUV420P10LE,

        /// <summary>
        ///     planar YUV 4:2:2, 20bpp, (1 Cr & Cb sample per 2x1 Y samples), big-endian
        /// </summary>
        YUV422P10BE,

        /// <summary>
        ///     planar YUV 4:2:2, 20bpp, (1 Cr & Cb sample per 2x1 Y samples), little-endian
        /// </summary>
        YUV422P10LE,

        /// <summary>
        ///     planar YUV 4:4:4, 27bpp, (1 Cr & Cb sample per 1x1 Y samples), big-endian
        /// </summary>
        YUV444P9BE,

        /// <summary>
        ///     planar YUV 4:4:4, 27bpp, (1 Cr & Cb sample per 1x1 Y samples), little-endian
        /// </summary>
        YUV444P9LE,

        /// <summary>
        ///     planar YUV 4:4:4, 30bpp, (1 Cr & Cb sample per 1x1 Y samples), big-endian
        /// </summary>
        YUV444P10BE,

        /// <summary>
        ///     planar YUV 4:4:4, 30bpp, (1 Cr & Cb sample per 1x1 Y samples), little-endian
        /// </summary>
        YUV444P10LE,

        /// <summary>
        ///     planar YUV 4:2:2, 18bpp, (1 Cr & Cb sample per 2x1 Y samples), big-endian
        /// </summary>
        YUV422P9BE,

        /// <summary>
        ///     planar YUV 4:2:2, 18bpp, (1 Cr & Cb sample per 2x1 Y samples), little-endian
        /// </summary>
        YUV422P9LE,

        /// <summary>
        ///     hardware decoding through VDA
        /// </summary>
        VDA_VLD,

#if _PIX_FMT_ABI_GIT_MASTER
    /// <summary>
    /// packed RGBA 16:16:16:16, 64bpp, 16R, 16G, 16B, 16A, the 2-byte value for each R/G/B/A component is stored as big-endian
    /// </summary>
        RGBA64BE,
        /// <summary>
        /// packed RGBA 16:16:16:16, 64bpp, 16R, 16G, 16B, 16A, the 2-byte value for each R/G/B/A component is stored as little-endian
        /// </summary>
        RGBA64LE,
        /// <summary>
        /// packed RGBA 16:16:16:16, 64bpp, 16B, 16G, 16R, 16A, the 2-byte value for each R/G/B/A component is stored as big-endian
        /// </summary>
        BGRA64BE,
        /// <summary>
        /// packed RGBA 16:16:16:16, 64bpp, 16B, 16G, 16R, 16A, the 2-byte value for each R/G/B/A component is stored as little-endian
        /// </summary>
        BGRA64LE,
#endif

        /// <summary>
        ///     planar GBR 4:4:4 24bpp
        /// </summary>
        GBRP,

        /// <summary>
        ///     planar GBR 4:4:4 27bpp, big-endian
        /// </summary>
        GBRP9BE,

        /// <summary>
        ///     planar GBR 4:4:4 27bpp, little-endian
        /// </summary>
        GBRP9LE,

        /// <summary>
        ///     planar GBR 4:4:4 30bpp, big-endian
        /// </summary>
        GBRP10BE,

        /// <summary>
        ///     planar GBR 4:4:4 30bpp, little-endian
        /// </summary>
        GBRP10LE,

        /// <summary>
        ///     planar GBR 4:4:4 48bpp, big-endian
        /// </summary>
        GBRP16BE,

        /// <summary>
        ///     planar GBR 4:4:4 48bpp, little-endian
        /// </summary>
        GBRP16LE,

        // duplicated pixel formats for compatibility with libav.
        // FFmpeg supports these formats since May 8 2012 and Jan 28 2012 (commits f9ca1ac7 and 143a5c55)
        // Libav added them Oct 12 2012 with incompatible values (commit 6d5600e85)
        /// <summary>
        ///     planar YUV 4:2:2 24bpp, (1 Cr & Cb sample per 2x1 Y & A samples)
        /// </summary>
        YUVA422P_LIB,

        /// <summary>
        ///     planar YUV 4:4:4 32bpp, (1 Cr & Cb sample per 1x1 Y & A samples)
        /// </summary>
        YUVA444P_LIB,

        /// <summary>
        ///     planar YUV 4:2:0 22.5bpp, (1 Cr & Cb sample per 2x2 Y & A samples), big-endian
        /// </summary>
        YUVA420P9BE,

        /// <summary>
        ///     planar YUV 4:2:0 22.5bpp, (1 Cr & Cb sample per 2x2 Y & A samples), little-endian
        /// </summary>
        YUVA420P9LE,

        /// <summary>
        ///     planar YUV 4:2:2 27bpp, (1 Cr & Cb sample per 2x1 Y & A samples), big-endian
        /// </summary>
        YUVA422P9BE,

        /// <summary>
        ///     planar YUV 4:2:2 27bpp, (1 Cr & Cb sample per 2x1 Y & A samples), little-endian
        /// </summary>
        YUVA422P9LE,

        /// <summary>
        ///     planar YUV 4:4:4 36bpp, (1 Cr & Cb sample per 1x1 Y & A samples), big-endian
        /// </summary>
        YUVA444P9BE,

        /// <summary>
        ///     planar YUV 4:4:4 36bpp, (1 Cr & Cb sample per 1x1 Y & A samples), little-endian
        /// </summary>
        YUVA444P9LE,

        /// <summary>
        ///     planar YUV 4:2:0 25bpp, (1 Cr & Cb sample per 2x2 Y & A samples, big-endian)
        /// </summary>
        YUVA420P10BE,

        /// <summary>
        ///     planar YUV 4:2:0 25bpp, (1 Cr & Cb sample per 2x2 Y & A samples, little-endian)
        /// </summary>
        YUVA420P10LE,

        /// <summary>
        ///     planar YUV 4:2:2 30bpp, (1 Cr & Cb sample per 2x1 Y & A samples, big-endian)
        /// </summary>
        YUVA422P10BE,

        /// <summary>
        ///     planar YUV 4:2:2 30bpp, (1 Cr & Cb sample per 2x1 Y & A samples, little-endian)
        /// </summary>
        YUVA422P10LE,

        /// <summary>
        ///     planar YUV 4:4:4 40bpp, (1 Cr & Cb sample per 1x1 Y & A samples, big-endian)
        /// </summary>
        YUVA444P10BE,

        /// <summary>
        ///     planar YUV 4:4:4 40bpp, (1 Cr & Cb sample per 1x1 Y & A samples, little-endian)
        /// </summary>
        YUVA444P10LE,

        /// <summary>
        ///     planar YUV 4:2:0 40bpp, (1 Cr & Cb sample per 2x2 Y & A samples, big-endian)
        /// </summary>
        YUVA420P16BE,

        /// <summary>
        ///     planar YUV 4:2:0 40bpp, (1 Cr & Cb sample per 2x2 Y & A samples, little-endian)
        /// </summary>
        YUVA420P16LE,

        /// <summary>
        ///     planar YUV 4:2:2 48bpp, (1 Cr & Cb sample per 2x1 Y & A samples, big-endian)
        /// </summary>
        YUVA422P16BE,

        /// <summary>
        ///     planar YUV 4:2:2 48bpp, (1 Cr & Cb sample per 2x1 Y & A samples, little-endian)
        /// </summary>
        YUVA422P16LE,

        /// <summary>
        ///     planar YUV 4:4:4 64bpp, (1 Cr & Cb sample per 1x1 Y & A samples, big-endian)
        /// </summary>
        YUVA444P16BE,

        /// <summary>
        ///     planar YUV 4:4:4 64bpp, (1 Cr & Cb sample per 1x1 Y & A samples, little-endian)
        /// </summary>
        YUVA444P16LE,

        /// <summary>
        ///     HW acceleration through VDPAU, Picture.data[3] contains a VdpVideoSurface
        /// </summary>
        VDPAU,

        /// <summary>
        ///     packed XYZ 4:4:4, 36 bpp, (msb) 12X, 12Y, 12Z (lsb), the 2-byte value for each X/Y/Z is stored as little-endian,
        ///     the 4 lower bits are set to 0
        /// </summary>
        XYZ12LE,

        /// <summary>
        ///     packed XYZ 4:4:4, 36 bpp, (msb) 12X, 12Y, 12Z (lsb), the 2-byte value for each X/Y/Z is stored as big-endian, the 4
        ///     lower bits are set to 0
        /// </summary>
        XYZ12BE,

        /// <summary>
        ///     interleaved chroma YUV 4:2:2, 16bpp, (1 Cr & Cb sample per 2x1 Y samples)
        /// </summary>
        NV16,

        /// <summary>
        ///     interleaved chroma YUV 4:2:2, 20bpp, (1 Cr & Cb sample per 2x1 Y samples), little-endian
        /// </summary>
        NV20LE,

        /// <summary>
        ///     interleaved chroma YUV 4:2:2, 20bpp, (1 Cr & Cb sample per 2x1 Y samples), big-endian
        /// </summary>
        NV20BE,

        // duplicated pixel formats for compatibility with libav.
        // FFmpeg supports these formats since Sat Sep 24 06:01:45 2011 +0200 (commits 9569a3c9f41387a8c7d1ce97d8693520477a66c3)
        // also see Fri Nov 25 01:38:21 2011 +0100 92afb431621c79155fcb7171d26f137eb1bee028
        // Libav added them Sun Mar 16 23:05:47 2014 +0100 with incompatible values (commit 1481d24c3a0abf81e1d7a514547bd5305232be30)
        /// <summary>
        ///     packed RGBA 16:16:16:16, 64bpp, 16R, 16G, 16B, 16A, the 2-byte value for each R/G/B/A component is stored as
        ///     big-endian
        /// </summary>
        RGBA64BE_LIB,

        /// <summary>
        ///     packed RGBA 16:16:16:16, 64bpp, 16R, 16G, 16B, 16A, the 2-byte value for each R/G/B/A component is stored as
        ///     little-endian
        /// </summary>
        RGBA64LE_LIB,

        /// <summary>
        ///     packed RGBA 16:16:16:16, 64bpp, 16B, 16G, 16R, 16A, the 2-byte value for each R/G/B/A component is stored as
        ///     big-endian
        /// </summary>
        BGRA64BE_LIB,

        /// <summary>
        ///     packed RGBA 16:16:16:16, 64bpp, 16B, 16G, 16R, 16A, the 2-byte value for each R/G/B/A component is stored as
        ///     little-endian
        /// </summary>
        BGRA64LE_LIB,

        /// <summary>
        ///     packed YUV 4:2:2, 16bpp, Y0 Cr Y1 Cb
        /// </summary>
        YVYU422,

        /// <summary>
        ///     HW acceleration through VDA, data[3] contains a CVPixelBufferRef
        /// </summary>
        VDA,

        /// <summary>
        ///     16bit gray, 16bit alpha (big-endian)
        /// </summary>
        YA16BE,

        /// <summary>
        ///     16bit gray, 16bit alpha (little-endian)
        /// </summary>
        YA16LE,

        // duplicated pixel formats for compatibility with libav.
        // FFmpeg supports these formats since May 3 2013 (commit e6d4e687558d08187e7a415a7725e4b1a416f782)
        // Libav added them Jan 14 2015 with incompatible values (commit 0e6c7dfa650e8b0497bfa7a06394b7a462ddc33a)
        /// <summary>
        ///     planar GBRA 4:4:4:4 32bpp
        /// </summary>
        GBRAP_LIB,

        /// <summary>
        ///     planar GBRA 4:4:4:4 64bpp, big-endian
        /// </summary>
        GBRAP16BE_LIB,

        /// <summary>
        ///     planar GBRA 4:4:4:4 64bpp, little-endian
        /// </summary>
        GBRAP16LE_LIB,

        /// <summary>
        ///     HW acceleration through QSV, data[3] contains a pointer to the mfxFrameSurface1 structure.
        /// </summary>
        QSV,

        /// <summary>
        ///     HW acceleration though MMAL, data[3] contains a pointer to the MMAL_BUFFER_HEADER_T structure.
        /// </summary>
        MMAL,

        /// <summary>
        ///     HW decoding through Direct3D11, Picture.data[3] contains a ID3D11VideoDecoderOutputView pointer
        /// </summary>
        D3D11VA_VLD,

#if !_PIX_FMT_ABI_GIT_MASTER

        /// <summary>
        ///     packed RGBA 16:16:16:16, 64bpp, 16R, 16G, 16B, 16A, the 2-byte value for each R/G/B/A component is stored as
        ///     big-endian
        /// </summary>
        _PIX_FMT_RGBA64BE = 0x123,

        /// <summary>
        ///     packed RGBA 16:16:16:16, 64bpp, 16R, 16G, 16B, 16A, the 2-byte value for each R/G/B/A component is stored as
        ///     little-endian
        /// </summary>
        _PIX_FMT_RGBA64LE,

        /// <summary>
        ///     packed RGBA 16:16:16:16, 64bpp, 16B, 16G, 16R, 16A, the 2-byte value for each R/G/B/A component is stored as
        ///     big-endian
        /// </summary>
        _PIX_FMT_BGRA64BE,

        /// <summary>
        ///     packed RGBA 16:16:16:16, 64bpp, 16B, 16G, 16R, 16A, the 2-byte value for each R/G/B/A component is stored as
        ///     little-endian
        /// </summary>
        _PIX_FMT_BGRA64LE,

#endif

        /// <summary>
        ///     packed RGB 8:8:8, 32bpp, XRGBXRGB...   X=unused/undefined
        /// </summary>
        XRGB = 0x123 + 4,

        /// <summary>
        ///     packed RGB 8:8:8, 32bpp, RGBXRGBX...   X=unused/undefined
        /// </summary>
        RGBX,

        /// <summary>
        ///     packed BGR 8:8:8, 32bpp, XBGRXBGR...   X=unused/undefined
        /// </summary>
        XBGR,

        /// <summary>
        ///     packed BGR 8:8:8, 32bpp, BGRXBGRX...   X=unused/undefined
        /// </summary>
        BGRX,

        /// <summary>
        ///     planar YUV 4:4:4 32bpp, (1 Cr & Cb sample per 1x1 Y & A samples)
        /// </summary>
        YUVA444P,

        /// <summary>
        ///     planar YUV 4:2:2 24bpp, (1 Cr & Cb sample per 2x1 Y & A samples)
        /// </summary>
        YUVA422P,

        /// <summary>
        ///     planar YUV 4:2:0,18bpp, (1 Cr & Cb sample per 2x2 Y samples), big-endian
        /// </summary>
        YUV420P12BE,

        /// <summary>
        ///     planar YUV 4:2:0,18bpp, (1 Cr & Cb sample per 2x2 Y samples), little-endian
        /// </summary>
        YUV420P12LE,

        /// <summary>
        ///     planar YUV 4:2:0,21bpp, (1 Cr & Cb sample per 2x2 Y samples), big-endian
        /// </summary>
        YUV420P14BE,

        /// <summary>
        ///     planar YUV 4:2:0,21bpp, (1 Cr & Cb sample per 2x2 Y samples), little-endian
        /// </summary>
        YUV420P14LE,

        /// <summary>
        ///     planar YUV 4:2:2,24bpp, (1 Cr & Cb sample per 2x1 Y samples), big-endian
        /// </summary>
        YUV422P12BE,

        /// <summary>
        ///     planar YUV 4:2:2,24bpp, (1 Cr & Cb sample per 2x1 Y samples), little-endian
        /// </summary>
        YUV422P12LE,

        /// <summary>
        ///     planar YUV 4:2:2,28bpp, (1 Cr & Cb sample per 2x1 Y samples), big-endian
        /// </summary>
        YUV422P14BE,

        /// <summary>
        ///     planar YUV 4:2:2,28bpp, (1 Cr & Cb sample per 2x1 Y samples), little-endian
        /// </summary>
        YUV422P14LE,

        /// <summary>
        ///     planar YUV 4:4:4,36bpp, (1 Cr & Cb sample per 1x1 Y samples), big-endian
        /// </summary>
        YUV444P12BE,

        /// <summary>
        ///     planar YUV 4:4:4,36bpp, (1 Cr & Cb sample per 1x1 Y samples), little-endian
        /// </summary>
        YUV444P12LE,

        /// <summary>
        ///     planar YUV 4:4:4,42bpp, (1 Cr & Cb sample per 1x1 Y samples), big-endian
        /// </summary>
        YUV444P14BE,

        /// <summary>
        ///     planar YUV 4:4:4,42bpp, (1 Cr & Cb sample per 1x1 Y samples), little-endian
        /// </summary>
        YUV444P14LE,

        /// <summary>
        ///     planar GBR 4:4:4 36bpp, big-endian
        /// </summary>
        GBRP12BE,

        /// <summary>
        ///     planar GBR 4:4:4 36bpp, little-endian
        /// </summary>
        GBRP12LE,

        /// <summary>
        ///     planar GBR 4:4:4 42bpp, big-endian
        /// </summary>
        GBRP14BE,

        /// <summary>
        ///     planar GBR 4:4:4 42bpp, little-endian
        /// </summary>
        GBRP14LE,

        /// <summary>
        ///     planar GBRA 4:4:4:4 32bpp
        /// </summary>
        GBRAP,

        /// <summary>
        ///     planar GBRA 4:4:4:4 64bpp, big-endian
        /// </summary>
        GBRAP16BE,

        /// <summary>
        ///     planar GBRA 4:4:4:4 64bpp, little-endian
        /// </summary>
        GBRAP16LE,

        /// <summary>
        ///     planar YUV 4:1:1, 12bpp, (1 Cr & Cb sample per 4x1 Y samples) full scale (JPEG), deprecated in favor of
        ///     _PIX_FMT_YUV411P and setting color_range
        /// </summary>
        YUVJ411P,

        /// <summary>
        ///     bayer, BGBG..(odd line), GRGR..(even line), 8-bit samples
        /// </summary>
        BAYER_BGGR8,

        /// <summary>
        ///     bayer, RGRG..(odd line), GBGB..(even line), 8-bit samples
        /// </summary>
        BAYER_RGGB8,

        /// <summary>
        ///     bayer, GBGB..(odd line), RGRG..(even line), 8-bit samples
        /// </summary>
        BAYER_GBRG8,

        /// <summary>
        ///     bayer, GRGR..(odd line), BGBG..(even line), 8-bit samples
        /// </summary>
        BAYER_GRBG8,

        /// <summary>
        ///     bayer, BGBG..(odd line), GRGR..(even line), 16-bit samples, little-endian
        /// </summary>
        BAYER_BGGR16LE,

        /// <summary>
        ///     bayer, BGBG..(odd line), GRGR..(even line), 16-bit samples, big-endian
        /// </summary>
        BAYER_BGGR16BE,

        /// <summary>
        ///     bayer, RGRG..(odd line), GBGB..(even line), 16-bit samples, little-endian
        /// </summary>
        BAYER_RGGB16LE,

        /// <summary>
        ///     bayer, RGRG..(odd line), GBGB..(even line), 16-bit samples, big-endian
        /// </summary>
        BAYER_RGGB16BE,

        /// <summary>
        ///     bayer, GBGB..(odd line), RGRG..(even line), 16-bit samples, little-endian
        /// </summary>
        BAYER_GBRG16LE,

        /// <summary>
        ///     bayer, GBGB..(odd line), RGRG..(even line), 16-bit samples, big-endian
        /// </summary>
        BAYER_GBRG16BE,

        /// <summary>
        ///     bayer, GRGR..(odd line), BGBG..(even line), 16-bit samples, little-endian
        /// </summary>
        BAYER_GRBG16LE,

        /// <summary>
        ///     bayer, GRGR..(odd line), BGBG..(even line), 16-bit samples, big-endian
        /// </summary>
        BAYER_GRBG16BE,

#if !FF_API_XVMC
    /// <summary>
    /// XVideo Motion Acceleration via common packet passing
    /// </summary>
        XVMC,
#endif

        /// <summary>
        ///     planar YUV 4:4:0,20bpp, (1 Cr & Cb sample per 1x2 Y samples), little-endian
        /// </summary>
        YUV440P10LE,

        /// <summary>
        ///     planar YUV 4:4:0,20bpp, (1 Cr & Cb sample per 1x2 Y samples), big-endian
        /// </summary>
        YUV440P10BE,

        /// <summary>
        ///     planar YUV 4:4:0,24bpp, (1 Cr & Cb sample per 1x2 Y samples), little-endian
        /// </summary>
        YUV440P12LE,

        /// <summary>
        ///     planar YUV 4:4:0,24bpp, (1 Cr & Cb sample per 1x2 Y samples), big-endian
        /// </summary>
        YUV440P12BE
    }

    /// <summary>
    ///     Chromaticity coordinates of the source primaries.
    /// </summary>
    public enum ColorPrimaries
    {
        Reserved0 = 0,

        /// <summary>
        ///     also ITU-R BT1361 / IEC 61966-2-4 / SMPTE RP177 Annex B
        /// </summary>
        BT709 = 1,

        Unspecified = 2,
        Reserved = 3,

        /// <summary>
        ///     also FCC Title 47 Code of Federal Regulations 73.682 (a)(20)
        /// </summary>
        BT470M = 4,

        /// <summary>
        ///     also ITU-R BT601-6 625 / ITU-R BT1358 625 / ITU-R BT1700 625 PAL & SECAM
        /// </summary>
        BT470BG = 5,

        /// <summary>
        ///     also ITU-R BT601-6 525 / ITU-R BT1358 525 / ITU-R BT1700 NTSC
        /// </summary>
        SMPTE170M = 6,

        /// <summary>
        ///     functionally identical to above
        /// </summary>
        SMPTE240M = 7,

        /// <summary>
        ///     colour filters using Illuminant C
        /// </summary>
        FILM = 8,

        /// <summary>
        ///     ITU-R BT2020
        /// </summary>
        BT2020 = 9
    }

    /// <summary>
    ///     Color Transfer Characteristic.
    /// </summary>
    public enum ColorTransferCharacteristic
    {
        RESERVED0 = 0,

        /// <summary>
        ///     also ITU-R BT1361
        /// </summary>
        BT709 = 1,

        UNSPECIFIED = 2,
        RESERVED = 3,

        /// <summary>
        ///     also ITU-R BT470M / ITU-R BT1700 625 PAL & SECAM
        /// </summary>
        GAMMA22 = 4,

        /// <summary>
        ///     also ITU-R BT470BG
        /// </summary>
        GAMMA28 = 5,

        /// <summary>
        ///     also ITU-R BT601-6 525 or 625 / ITU-R BT1358 525 or 625 / ITU-R BT1700 NTSC
        /// </summary>
        SMPTE170M = 6, //

        SMPTE240M = 7,

        /// <summary>
        ///     "Linear transfer characteristics"
        /// </summary>
        LINEAR = 8,

        /// <summary>
        ///     "Logarithmic transfer characteristic (100:1 range)"
        /// </summary>
        LOG = 9,

        /// <summary>
        ///     Logarithmic transfer characteristic (100 * Sqrt(10) : 1 range)"
        /// </summary>
        LOG_SQRT = 10,

        /// <summary>
        ///     IEC 61966-2-4
        /// </summary>
        IEC61966_2_4 = 11, //< IEC 61966-2-4

        /// <summary>
        ///     ITU-R BT1361 Extended Colour Gamut
        /// </summary>
        BT1361_ECG = 12,

        /// <summary>
        ///     IEC 61966-2-1 (sRGB or sYCC)
        /// </summary>
        IEC61966_2_1 = 13,

        /// <summary>
        ///     ITU-R BT2020 for 10 bit system
        /// </summary>
        BT2020_10 = 14,

        /// <summary>
        ///     ITU-R BT2020 for 12 bit system
        /// </summary>
        BT2020_12 = 15
    }

    /// <summary>
    ///     YUV colorspace type.
    /// </summary>
    public enum ColorSpace
    {
        /// <summary>
        ///     order of coefficients is actually GBR, also IEC 61966-2-1 (sRGB)
        /// </summary>
        RGB = 0,

        /// <summary>
        ///     also ITU-R BT1361 / IEC 61966-2-4 xvYCC709 / SMPTE RP177 Annex B
        /// </summary>
        BT709 = 1,

        Unspecified = 2,
        Reserved = 3,

        /// <summary>
        ///     FCC Title 47 Code of Federal Regulations 73.682 (a)(20)
        /// </summary>
        FCC = 4,

        /// <summary>
        ///     also ITU-R BT601-6 625 / ITU-R BT1358 625 / ITU-R BT1700 625 PAL & SECAM / IEC 61966-2-4 xvYCC601
        /// </summary>
        BT470BG = 5,

        /// <summary>
        ///     also ITU-R BT601-6 525 / ITU-R BT1358 525 / ITU-R BT1700 NTSC / functionally identical to above
        /// </summary>
        SMPTE170M = 6,

        SMPTE240M = 7,

        /// <summary>
        ///     Used by Dirac / VC-2 and H.264 FRext, see ITU-T SG16
        /// </summary>
        YCOCG = 8,

        /// <summary>
        ///     Used by Dirac / VC-2 and H.264 FRext, see ITU-T SG16
        /// </summary>
        /// <seealso cref="YCOCG" />
        YCGCO = YCOCG,

        /// <summary>
        ///     ITU-R BT2020 non-constant luminance system
        /// </summary>
        BT2020_NCL = 9,

        /// <summary>
        ///     ITU-R BT2020 constant luminance system
        /// </summary>
        BT2020_CL = 10
    }

    /// <summary>
    ///     MPEG vs JPEG YUV range.
    /// </summary>
    public enum ColorRange
    {
        Unspecified = 0,

        /// <summary>
        ///     the normal 219*2^(n-8) "MPEG" YUV ranges
        /// </summary>
        MPEG = 1,

        /// <summary>
        ///     the normal     2^n-1   "JPEG" YUV ranges
        /// </summary>
        JPEG = 2
    }

    /// <summary>
    ///     Location of chroma samples.
    ///     <para></para>
    ///     Illustration showing the location of the first (top left) chroma sample of the image, the left shows only luma, the
    ///     right shows the location of the chroma sample, the 2 could be imagined to overlay each other but are drawn
    ///     separately due to limitations of ASCII
    /// </summary>
    /// <code>
    ///                 1st 2nd       1st 2nd horizontal luma sample positions
    ///                  v   v         v   v
    ///                  ______        ______
    /// 1st luma line > |X   X ...    |3 4 X ...     X are luma samples,
    ///                 |             |1 2           1-6 are possible chroma positions
    /// 2nd luma line > |X   X ...    |5 6 X ...     0 is undefined/unknown position
    /// </code>
    public enum ChromaLocation
    {
        Unspecified = 0,

        /// <summary>
        ///     mpeg2/4 4:2:0, h264 default for 4:2:0
        /// </summary>
        Left = 1,

        /// <summary>
        ///     mpeg1 4:2:0, jpeg 4:2:0, h263 4:2:0
        /// </summary>
        Center = 2,

        /// <summary>
        ///     ITU-R 601, SMPTE 274M 296M S314M(DV 4:1:1), mpeg2 4:2:2
        /// </summary>
        TopLeft = 3,

        Top = 4,
        BottomLeft = 5,
        Bottom = 6
    }
}