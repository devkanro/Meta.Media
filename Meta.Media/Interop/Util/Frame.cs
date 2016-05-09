// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: Frame.cs
// Version: 20160508

using System;
using System.Runtime.InteropServices;

namespace Meta.Media.Interop.Util
{
    public enum FrameSideDataType
    {
        /// <summary>
        ///     The data is the PanScan struct defined in libavcodec.
        /// </summary>
        Panscan,

        /// <summary>
        ///     ATSC A53 Part 4 Closed Captions.
        ///     A53 CC bitstream is stored as uint8_t in <see cref="Frame" />SideData.data.
        ///     The number of bytes of CC data is <see cref="Frame" />SideData.size.
        /// </summary>
        A53Cc,

        /// <summary>
        ///     Stereoscopic 3d metadata.
        ///     The data is the Stereo3D struct defined in libavutil/stereo3d.h.
        /// </summary>
        Stereo3D,

        /// <summary>
        ///     The data is the MatrixEncoding enum defined in libavutil/channel_layout.h.
        /// </summary>
        Matrixencoding,

        /// <summary>
        ///     Metadata relevant to a downmix procedure.
        ///     The data is the DownmixInfo struct defined in libavutil/downmix_info.h.
        /// </summary>
        DownmixInfo,

        /// <summary>
        ///     ReplayGain information in the form of the ReplayGain struct.
        /// </summary>
        Replaygain,

        /// <summary>
        ///     This side data contains a 3x3 transformation matrix describing an affine
        ///     transformation that needs to be applied to the frame for correct
        ///     presentation.
        ///     <para></para>
        ///     See libavutil/display.h for a detailed description of the data.
        /// </summary>
        Displaymatrix,

        /// <summary>
        ///     Active Format Description data consisting of a single byte as specified
        ///     in ETSI TS 101 154 using ActiveFormatDescription enum.
        /// </summary>
        Afd,

        /// <summary>
        ///     Motion vectors exported by some codecs (on demand through the export_mvs
        ///     flag set in the libavcodec CodecContext flags2 option).
        ///     The data is the MotionVector struct defined in
        ///     libavutil/motion_vector.h.
        /// </summary>
        MotionVectors,

        /// <summary>
        ///     Recommmends skipping the specified number of samples. This is exported
        ///     only if the "skip_manual" Option is set in libavcodec.
        ///     This has the same format as _PKT_DATA_SKIP_SAMPLES.
        ///     <code>
        /// u32le number of samples to skip from start of this packet
        /// u32le number of samples to skip from end of this packet
        /// u8    reason for start skip
        /// u8    reason for end   skip (0=padding silence, 1=convergence)
        /// </code>
        /// </summary>
        SkipSamples,

        /// <summary>
        ///     This side data must be associated with an audio frame and corresponds to
        ///     enum AudioServiceType defined in avcodec.h.
        /// </summary>
        AudioServiceType
    }

    public enum ActiveFormatDescription
    {
        FormatSame = 8,
        Format43 = 9,
        Format169 = 10,
        Format149 = 11,
        Format43Sp149 = 13,
        Format169Sp149 = 14,
        FormatSp43 = 15
    }

    /// <summary>
    ///     Structure to hold side data for an <see cref="Frame" />.
    ///     <para></para>
    ///     sizeof(<see cref="Frame" />SideData) is not a part of the public ABI, so new fields may be added to the end with a
    ///     minor bump.
    /// </summary>
    // TODO: Structure operation.
    public unsafe struct FrameSideData
    {
        private FrameSideDataType _type;
        private byte* _data;
        private int _size;
        private DictionaryPtr _metaData;
        private BufferRefPtr _buffer;
    }

    /// <summary>
    ///     This structure describes decoded (raw) audio or video data.
    ///     <para></para>
    ///     Frame must be allocated using av_frame_alloc(). Note that this only allocates the <see cref="Frame" /> itself, the
    ///     buffers for the data must be managed  through other means (see below). <see cref="Frame" /> must be freed with
    ///     av_frame_free().
    ///     <para></para>
    ///     <see cref="Frame" /> is typically allocated once and then reused multiple times to hold different data(e.g.a single
    ///     <see cref="Frame" /> to hold frames received from a decoder). In such a case, av_frame_unref() will free any
    ///     references held by the frame and reset it to its original clean state before it is reused again.
    ///     <para></para>
    ///     The data described by an <see cref="Frame" /> is usually reference counted through the <see cref="BufferPtr" />
    ///     API.The underlying buffer references are stored in <see cref="Frame" />.buf <see cref="Frame" />.extended_buf.An
    ///     <see cref="Frame" /> is considered to be reference counted if at least one reference is set, i.e. if
    ///     <see cref="Frame" />.buf[0] != NULL.In such a case, every single data plane must be contained in one of the buffers
    ///     in <see cref="Frame" />.buf or <see cref="Frame" />.extended_buf.There may be a single buffer for all the data, or
    ///     one separate buffer for each plane, or anything in between.
    ///     <para></para>
    ///     sizeof(<see cref="Frame" />) is not a part of the public ABI, so new fields may be added to the end with a minor
    ///     bump.Similarly fields that are marked as to be only accessed by av_opt_ptr() can be reordered.This allows 2 forks
    ///     to add fields without breaking compatibility with each other.
    /// </summary>
    // TODO: Structure operation.
    // TODO: Link to av_frame_alloc().
    // TODO: Link to av_frame_free().
    // TODO: Link to av_frame_unref().
    // TODO: Link to av_opt_ptr().
    public unsafe struct Frame
    {
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPArray, SizeConst = 8)]
        private byte*[] _data;
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeConst = 8)]
        private int[] _lineSize;
        private byte** _extendedData;
        private int _width;
        private int _height;
        private int _sampleCount;
        private int _format;
        private int _keyFrame;
        private PictureType _pictureType;
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPArray, SizeConst = 8)]
        [Obsolete]
        private byte*[] _base;
        private Rational _sampleAspectRatio;
        private long _pts;
        private long _pktPts;
        private long _pktDts;
        private int _codedPictureNumber;
        private int _displayPictureNumber;
        private int _quality;
        [Obsolete]
        private int _reference;
        [Obsolete]
        private byte* _qscaleTable;
        [Obsolete]
        private int _qstride;
        [Obsolete]
        private int _qscaleType;
        [Obsolete]
        private byte* _mbskipTable;
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPArray, SizeConst = 4)]
        private short*[][] _motionVal;
        [Obsolete]
        private uint _macroblockType;
        [Obsolete]
        private short _dctCoeff;
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPArray, SizeConst = 2)]
        [Obsolete]
        private byte*[] _refIndex;
        private void* _opaque;
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I8, SizeConst = 8)]
        private long[] _error;
        [Obsolete]
        private int _type;
        private int _repeatPict;
        private int _interlacedFrame;
        private int _topFieldFirst;
        private int _paletteHasChanged;
        [Obsolete]
        private int _bufferHints;
        [Obsolete]
        // TODO: IntPtr is PanScanPtr(PanScan).
        // TODO: PanScan structure.
        private IntPtr _panScan;
        private long _reorderedOpaque;
        [Obsolete]
        private void* _hwaccelPicturePrivate;
        [Obsolete]
        // TODO: IntPtr is CodecContextPtr(CodecContext).
        // TODO: CodecContext structure.
        private IntPtr _owner;
        [Obsolete]
        private void* _threadOpaque;
        private byte _motionSubsampleLog2;
        private int _sampleRate;
        private ChannelLayout _channelLayout;
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPArray, SizeConst = 8)]
        private BufferRefPtr[] _buf;
        private BufferRefPtr* _extendedBuf;
        private int _extendedBufCount;
        private FrameSideData** _sideData;
        private int _sideDataCount;
        private int _flags;
        // TODO: Other Fleids.
    }
}