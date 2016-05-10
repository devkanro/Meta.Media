// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: Frame.cs
// Version: 20160510

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

    public enum FrameFlags
    {
        /// <summary>
        ///     The frame data may be corrupted, e.g. due to decoding errors.
        /// </summary>
        Corrupt = (1 << 0)
    }

    public enum DecodeErrorFlag
    {
        InvalidBitstream = 1,
        MissingReference = 2
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
        /// <summary>
        ///     pointer to the picture/channel planes.
        ///     <para></para>
        ///     This might be different from the first allocated byte
        ///     <para></para>
        ///     Some decoders access areas outside 0,0 - width,height, please see avcodec_align_dimensions2(). Some filters and
        ///     swscale can read up to 16 bytes beyond the planes, if these filters are to be used, then 16 extra bytes must be
        ///     allocated.
        /// </summary>
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPArray, SizeConst = 8)]
        // TODO: Link to avcodec_align_dimensions2().
        private byte*[] _data;

        /// <summary>
        ///     For video, size in bytes of each picture line.
        ///     <para></para>
        ///     For audio, size in bytes of each plane.
        ///     <para></para>
        ///     For audio, only linesize[0] may be set. For planar audio, each channel
        ///     plane must be the same size.
        ///     <para></para>
        ///     For video the linesizes should be multiples of the CPUs alignment
        ///     preference, this is 16 or 32 for modern desktop CPUs.
        ///     Some code requires such alignment other code can be slower without
        ///     correct alignment, for yet other it makes no difference.
        /// </summary>
        /// <remarks>
        ///     Note:
        ///     The linesize may be larger than the size of usable data -- there may be extra padding present for performance
        ///     reasons.
        /// </remarks>
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I4, SizeConst = 8)] private int[] _lineSize;

        /// <summary>
        ///     pointers to the data planes/channels.
        ///     <para></para>
        ///     For video, this should simply point to data[].
        ///     <para></para>
        ///     For planar audio, each channel has a separate data pointer, and
        ///     linesize[0] contains the size of each channel buffer.
        ///     <para></para>
        ///     For packed audio, there is just one data pointer, and linesize[0]
        ///     contains the total size of the buffer for all channels.
        /// </summary>
        /// <remarks>
        ///     Note:
        ///     Both data and _extendedData should always be set in a valid frame, but for planar audio with more channels that can
        ///     fit in data, _extendedData must be used in order to access all channels.
        /// </remarks>
        private byte** _extendedData;

        /// <summary>
        ///     width of the video frame
        /// </summary>
        private int _width;

        /// <summary>
        ///     height of the video frame
        /// </summary>
        private int _height;

        /// <summary>
        ///     number of audio samples (per channel) described by this frame
        /// </summary>
        private int _sampleCount;

        /// <summary>
        ///     format of the frame, -1 if unknown or unset
        ///     <para></para>
        ///     Values correspond to <see cref="PixelFormat" /> for video frames, <see cref="SampleFormat" /> for audio)
        /// </summary>
        private int _format;

        /// <summary>
        ///     1 -> keyframe, 0-> not
        /// </summary>
        private int _keyFrame;

        /// <summary>
        ///     Picture type of the frame.
        /// </summary>
        private PictureType _pictureType;

#if FF_API_AVFRAME_LAVC

        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPArray, SizeConst = 8)] [Obsolete] private
            byte*[] _base;

#endif

        /// <summary>
        ///     Sample aspect ratio for the video frame, 0/1 if unknown/unspecified.
        /// </summary>
        private Rational _sampleAspectRatio;

        /// <summary>
        ///     Presentation timestamp in time_base units (time when frame should be shown to user).
        /// </summary>
        private long _pts;

        /// <summary>
        ///     PTS copied from the AVPacket that was decoded to produce this frame.
        /// </summary>
        private long _pktPts;

        /// <summary>
        ///     DTS copied from the AVPacket that triggered returning this frame. (if frame threading isn't used)
        ///     <para></para>
        ///     This is also the Presentation time of this Frame calculated from only AVPacket.dts values without pts values.
        /// </summary>
        private long _pktDts;

        /// <summary>
        ///     picture number in bitstream order
        /// </summary>
        private int _codedPictureNumber;

        /// <summary>
        ///     picture number in display order
        /// </summary>
        private int _displayPictureNumber;

        /// <summary>
        ///     quality (between 1 (good) and (256*128-1) (bad))
        /// </summary>
        private int _quality;

#if FF_API_AVFRAME_LAVC

        [Obsolete] private int _reference;

        /// <summary>
        ///     QP table
        /// </summary>
        [Obsolete] private byte* _qscaleTable;

        /// <summary>
        ///     QP store stride
        /// </summary>
        [Obsolete] private int _qstride;

        [Obsolete] private int _qscaleType;

        /// <summary>
        ///     mbskip_table[mb]>=1 if MB didn't change
        ///     <para></para>
        ///     stride= mb_width = (width+15)>>4
        /// </summary>
        [Obsolete] private byte* _mbskipTable;

        /// <summary>
        ///     motion vector table
        ///     <code>
        /// example:
        /// int mv_sample_log2= 4 - motion_subsample_log2;
        /// int mb_width= (width+15)>>4;
        /// int mv_stride= (mb_width &lt;&lt; mv_sample_log2) + 1;
        /// motion_val[direction][x + y*mv_stride][0->mv_x, 1->mv_y];
        /// </code>
        /// </summary>
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPArray, SizeConst = 4)] private short*[][]
            _motionVal;

        /// <summary>
        ///     macroBlock type table
        ///     <para></para>
        ///     mb_type_base + mb_width + 2
        /// </summary>
        [Obsolete] private uint _macroblockType;

        /// <summary>
        ///     DCT coefficients
        /// </summary>
        [Obsolete] private short _dctCoeff;

        /// <summary>
        ///     motion reference frame index the order in which these are stored can depend on the codec.
        /// </summary>
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPArray, SizeConst = 2)] [Obsolete] private
            byte*[] _refIndex;

#endif

        /// <summary>
        ///     for some private data of the user
        /// </summary>
        private void* _opaque;

        /// <summary>
        ///     error
        /// </summary>
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.I8, SizeConst = 8)] private long[] _error;

#if FF_API_AVFRAME_LAVC

        [Obsolete] private int _type;

#endif

        /// <summary>
        ///     When decoding, this signals how much the picture must be delayed.
        ///     <para></para>
        ///     extra_delay = repeat_pict / (2*fps)
        /// </summary>
        private int _repeatPict;

        /// <summary>
        ///     The content of the picture is interlaced.
        /// </summary>
        private int _interlacedFrame;

        /// <summary>
        ///     If the content is interlaced, is top field displayed first.
        /// </summary>
        private int _topFieldFirst;

        /// <summary>
        ///     Tell user application that palette has changed from previous frame.
        /// </summary>
        private int _paletteHasChanged;

#if FF_API_AVFRAME_LAVC

        [Obsolete] private int _bufferHints;

        /// <summary>
        ///     Pan scan.
        /// </summary>
        [Obsolete]
        // TODO: IntPtr is PanScanPtr(PanScan).
        // TODO: PanScan structure.
        private IntPtr _panScan;

#endif

        /// <summary>
        ///     reordered opaque 64bit (generally an integer or a double precision float
        ///     PTS but can be anything).
        ///     The user sets <see cref="CodecContext" />._reorderedOpaque to represent the input at
        ///     that time,
        ///     the decoder reorders values as needed and sets Frame._reorderedOpaque
        ///     to exactly one of the values provided by the user through <see cref="CodecContext_reorderedOpaque" />
        ///     <para></para>
        ///     Deprecated: in favor of pktPts
        /// </summary>
        [Obsolete] private long _reorderedOpaque;

#if FF_API_AVFRAME_LAVC

        /// <summary>
        ///     this field is unused
        /// </summary>
        [Obsolete] private void* _hwaccelPicturePrivate;

        [Obsolete]
        // TODO: IntPtr is CodecContextPtr(CodecContext).
        // TODO: CodecContext structure.
        private IntPtr _owner;

        [Obsolete] private void* _threadOpaque;

        /// <summary>
        ///     log2 of the size of the block which a single vector in motion_val represents:
        ///     (4->16x16, 3->8x8, 2-> 4x4, 1-> 2x2)
        /// </summary>
        private byte _motionSubsampleLog2;

#endif

        /// <summary>
        ///     Sample rate of the audio data.
        /// </summary>
        private int _sampleRate;

        /// <summary>
        ///     Channel layout of the audio data.
        /// </summary>
        private ChannelLayout _channelLayout;

        /// <summary>
        ///     Buffer references backing the data for this frame. If all elements of this array are NULL, then this frame is not
        ///     reference counted. This array must be filled contiguously -- if buf[i] is non-NULL then buf[j] must also be
        ///     non-NULL for all j &lt; i.
        ///     <para></para>
        ///     There may be at most one AVBuffer per data plane, so for video this array always contains all the references. For
        ///     planar audio with more than 8 channels, there may be more buffers than can fit in this array. Then the extra
        ///     AVBufferRefPtr pointers are stored in the extended_buf array.
        /// </summary>
        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPArray, SizeConst = 8)] private BufferRefPtr[]
            _buf;

        /// <summary>
        ///     For planar audio which requires more than 8 BufferRef pointers, this array will hold all the references which
        ///     cannot fit into Frame.buf.
        ///     <para></para>
        ///     Note that this is different from <see cref="_extendedData" />, which always contains all the pointers. This array
        ///     only contains the extra pointers, which cannot fit into Frame.buf.
        ///     <para></para>
        ///     This array is always allocated using <see cref="Malloc" /> by whoever ructs the frame. It is freed in
        ///     av_frame_unref().
        /// </summary>
        // TODO: Link to av_frame_unref().
        private BufferRefPtr* _extendedBuf;

        /// <summary>
        ///     Number of elements in _extendedBuf.
        /// </summary>
        private int _extendedBufCount;

        private FrameSideData** _sideData;
        private int _sideDataCount;

        /// <summary>
        ///     Frame flags, a combination of @ref lavu_frame_flags
        /// </summary>
        private FrameFlags _flags;

        /// <summary>
        ///     MPEG vs JPEG YUV range.
        ///     <para></para>
        ///     It must be accessed using av_frame_get_color_range() and av_frame_set_color_range().
        ///     <para></para>
        ///     - encoding: Set by user
        ///     <para></para>
        ///     - decoding: Set by libavcodec
        /// </summary>
        // TODO: Link to av_frame_get_color_range().
        // TODO: Link to av_frame_set_color_range().
        private ColorRange _colorRange;

        private ColorPrimaries _colorPrimaries;
        private ColorTransferCharacteristic _colorTrc;

        /// <summary>
        ///     YUV colorspace type.
        ///     <para></para>
        ///     It must be accessed using av_frame_get_colorspace() and av_frame_set_colorspace().
        ///     <para></para>
        ///     - encoding: Set by user
        ///     <para></para>
        ///     - decoding: Set by libavcodec
        /// </summary>
        // TODO: Link to av_frame_get_colorspace().
        // TODO: Link to av_frame_set_colorspace().
        private ColorSpace _colorspace;

        private ChromaLocation _chromaLocation;

        /// <summary>
        ///     frame timestamp estimated using various heuristics, in stream time base
        ///     <para></para>
        ///     Code outside libavcodec should access this field using: av_frame_get_best_effort_timestamp(frame)
        ///     <para></para>
        ///     - encoding: unused
        ///     <para></para>
        ///     - decoding: set by libavcodec, read by user.
        /// </summary>
        // TODO: Link to av_frame_get_best_effort_timestamp().
        private long _bestEffortTimestamp;

        /// <summary>
        ///     reordered pos from the last AVPacket that has been input into the decoder
        ///     <para></para>
        ///     Code outside libavcodec should access this field using: av_frame_get_pkt_pos(frame)
        ///     <para></para>
        ///     - encoding: unused
        ///     <para></para>
        ///     - decoding: Read by user.
        /// </summary>
        // TODO: Link to av_frame_get_pkt_pos().
        private long _pktPos;

        /// <summary>
        ///     duration of the corresponding packet, expressed in AVStream->time_base units, 0 if unknown.
        ///     <para></para>
        ///     Code outside libavcodec should access this field using: av_frame_get_pkt_duration(frame)
        ///     <para></para>
        ///     - encoding: unused
        ///     <para></para>
        ///     - decoding: Read by user.
        /// </summary>
        // TODO: Link to av_frame_get_pkt_duration().
        private long _pktDuration;

        /// <summary>
        ///     metadata.
        ///     <para></para>
        ///     Code outside libavcodec should access this field using: av_frame_get_metadata(frame)
        ///     <para></para>
        ///     - encoding: Set by user.
        ///     <para></para>
        ///     - decoding: Set by libavcodec.
        /// </summary>
        // TODO: Link to av_frame_get_metadata().
        private DictionaryPtr _metadata;

        /// <summary>
        ///     decode error flags of the frame, set to a combination of
        ///     <see cref="DecodeErrorFlag" /> flags if the decoder produced a frame, but there
        ///     were errors during the decoding.
        ///     <para></para>
        ///     Code outside libavcodec should access this field using: av_frame_get_decode_error_flags(frame)
        ///     <para></para>
        ///     - encoding: unused
        ///     <para></para>
        ///     - decoding: set by libavcodec, read by user.
        /// </summary>
        // TODO: Link to av_frame_get_decode_error_flags().
        private DecodeErrorFlag _decodeErrorFlags;

        /// <summary>
        ///     number of audio channels, only used for audio.
        ///     <para></para>
        ///     Code outside libavcodec should access this field using: av_frame_get_channels(frame)
        ///     <para></para>
        ///     - encoding: unused
        ///     <para></para>
        ///     - decoding: Read by user.
        /// </summary>
        // TODO: Link to av_frame_get_channels().
        private int _channelCount;

        /// <summary>
        ///     size of the corresponding packet containing the compressed frame. It must be accessed using av_frame_get_pkt_size()
        ///     and av_frame_set_pkt_size().
        ///     <para></para>
        ///     It is set to a negative value if unknown.
        ///     <para></para>
        ///     - encoding: unused
        ///     <para></para>
        ///     - decoding: set by libavcodec, read by user.
        /// </summary>
        // TODO: Link to av_frame_get_pkt_size().
        // TODO: Link to av_frame_set_pkt_size().
        private int _pktSize;

        /// <summary>
        ///     Not to be accessed directly from outside libavutil
        /// </summary>
        private BufferRefPtr _qpTableBuf;
    }

    public struct FramePtr
    {
        internal FramePtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public Frame Struct => Marshal.PtrToStructure<Frame>(Value);

        public static bool operator ==(FramePtr value1, FramePtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(FramePtr value1, FramePtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(FramePtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FramePtr))
            {
                return false;
            }

            return Equals((FramePtr) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(Frame)";
        }
    }

    [UtilFunction("av_frame_get_best_effort_timestamp")]
    public delegate long FrameGetBestEffortTimestamp(FramePtr frame);

    [UtilFunction("av_frame_set_best_effort_timestamp")]
    public delegate void FrameSetBestEffortTimestamp(FramePtr frame, long val);

    [UtilFunction("av_frame_get_pkt_duration")]
    public delegate long FrameGetPktDuration(FramePtr frame);

    [UtilFunction("av_frame_set_pkt_duration")]
    public delegate void FrameSetPktDuration(FramePtr frame, long val);

    [UtilFunction("av_frame_get_pkt_pos")]
    public delegate long FrameGetPktPos(FramePtr frame);

    [UtilFunction("av_frame_set_pkt_pos")]
    public delegate void FrameSetPktPos(FramePtr frame, long val);

    [UtilFunction("av_frame_get_channel_layout")]
    public delegate long FrameGetChannelLayout(FramePtr frame);

    [UtilFunction("av_frame_set_channel_layout")]
    public delegate void FrameSetChannelLayout(FramePtr frame, long val);

    [UtilFunction("av_frame_get_channels")]
    public delegate int FrameGetChannelCount(FramePtr frame);

    [UtilFunction("av_frame_set_channels")]
    public delegate void FrameSetChannelCount(FramePtr frame, int val);

    [UtilFunction("av_frame_get_sample_rate")]
    public delegate int FrameGetSampleRate(FramePtr frame);

    [UtilFunction("av_frame_set_sample_rate")]
    public delegate void FrameSetSampleRate(FramePtr frame, int val);

    [UtilFunction("av_frame_get_metadata")]
    public delegate DictionaryPtr FrameGetMetadata(FramePtr frame);

    [UtilFunction("av_frame_set_metadata")]
    public delegate void FrameSetMetadata(FramePtr frame, DictionaryPtr val);

    [UtilFunction("av_frame_get_decode_error_flags")]
    public delegate int FrameGetDecodeErrorFlags(FramePtr frame);

    [UtilFunction("av_frame_set_decode_error_flags")]
    public delegate void FrameSetDecodeErrorFlags(FramePtr frame, int val);

    [UtilFunction("av_frame_get_pkt_size")]
    public delegate int FrameGetPktSize(FramePtr frame);

    [UtilFunction("av_frame_set_pkt_size")]
    public delegate void FrameSetPktSize(FramePtr frame, int val);

    [UtilFunction("avpriv_frame_get_metadatap")]
    public unsafe delegate DictionaryPtr* AvprivFrameGetMetadatap(FramePtr frame);

    [UtilFunction("av_frame_get_qp_table")]
    public unsafe delegate byte* FrameGetQpTable(FramePtr f, ref int stride, ref int type);

    [UtilFunction("av_frame_set_qp_table")]
    public delegate int FrameSetQpTable(FramePtr f, BufferRefPtr buf, int stride, int type);

    [UtilFunction("av_frame_get_colorspace")]
    public delegate ColorSpace FrameGetColorspace(FramePtr frame);

    [UtilFunction("av_frame_set_colorspace")]
    public delegate void FrameSetColorspace(FramePtr frame, ColorSpace val);

    [UtilFunction("av_frame_get_color_range")]
    public delegate ColorRange FrameGetColorRange(FramePtr frame);

    [UtilFunction("av_frame_set_color_range")]
    public delegate void FrameSetColorRange(FramePtr frame, ColorRange val);

    /// <summary>
    ///     Get the name of a colorspace.
    /// </summary>
    /// <param name="val"></param>
    /// <returns>a static string identifying the colorspace; can be NULL.</returns>
    [UtilFunction("av_get_colorspace_name")]
    public unsafe delegate byte* GetColorspaceName(ColorSpace val);

    /// <summary>
    ///     Allocate an AVFrame and set its fields to default values.  The resulting
    ///     struct must be freed using frame_free().
    /// </summary>
    /// <returns> An AVFrame filled with default values or NULL on failure.</returns>
    /// <remarks>
    ///     Note:
    ///     this only allocates the AVFrame itself, not the data buffers. Those
    ///     must be allocated through other means, e.g. with frame_get_buffer() or
    ///     manually.
    /// </remarks>
    [UtilFunction("av_frame_alloc")]
    public delegate FramePtr FrameAlloc();

    /// <summary>
    ///     Free the frame and any dynamically allocated objects in it, e.g. _extendedData. If the frame is reference counted,
    ///     it will be unreferenced first.
    /// </summary>
    /// <param name="frame">frame to be freed. The pointer will be set to NULL.</param>
    [UtilFunction("av_frame_free")]
    public unsafe delegate void FrameFree(FramePtr* frame);

    /// <summary>
    ///     Set up a new reference to the data described by the source frame.
    ///     <para></para>
    ///     Copy frame properties from src to dst and create a new reference for each AVBufferRef from src.
    ///     <para></para>
    ///     If src is not reference counted, new buffers are allocated and the data is copied.
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="src"></param>
    /// <returns>0 on success, a negative AVERROR on error</returns>
    [UtilFunction("av_frame_ref")]
    // TODO: Error Code.
    public delegate int FrameRef(FramePtr dst, FramePtr src);

    /// <summary>
    ///     Create a new frame that references the same data as src.
    ///     <para></para>
    ///     This is a shortcut for frame_alloc()+av_frame_ref().
    /// </summary>
    /// <param name="src"></param>
    /// <returns>newly created AVFrame on success, NULL on error.</returns>
    [UtilFunction("av_frame_clone")]
    // TODO: Error Code.
    public delegate FramePtr FrameClone(FramePtr src);

    /// <summary>
    ///     Unreference all the buffers referenced by frame and reset the frame fields.
    /// </summary>
    /// <param name="frame"></param>
    [UtilFunction("av_frame_unref")]
    public delegate void FrameUnref(FramePtr frame);

    /// <summary>
    ///     Move everything contained in src to dst and reset src.
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="src"></param>
    [UtilFunction("av_frame_move_ref")]
    public delegate void FrameMoveRef(FramePtr dst, FramePtr src);

    /// <summary>
    ///     Allocate new buffer(s) for audio or video data.
    ///     <para></para>
    ///     The following fields must be set on frame before calling this function:
    ///     <para></para>
    ///     - format (pixel format for video, sample format for audio)
    ///     <para></para>
    ///     - width and height for video
    ///     <para></para>
    ///     - sampleCount and channelLayout for audio
    ///     <para></para>
    ///     This function will fill AVFrame.data and AVFrame.buf arrays and, if
    ///     necessary, allocate and fill AVFrame.extended_data and AVFrame.extended_buf.
    ///     For planar formats, one buffer will be allocated for each plane.
    /// </summary>
    /// <param name="frame">frame in which to store the new buffers.</param>
    /// <param name="align">required buffer size alignment</param>
    /// <returns>0 on success, a negative AVERROR on error.</returns>
    [UtilFunction("av_frame_get_buffer")]
    // TODO: Error Code.
    public delegate int FrameGetBuffer(FramePtr frame, int align);

    /// <summary>
    ///     Check if the frame data is writable.
    ///     <para></para>
    ///     If 1 is returned the answer is valid until buffer_ref() is called on any
    ///     of the underlying AVBufferRefs (e.g. through frame_ref() or directly).
    /// </summary>
    /// <param name="frame"></param>
    /// <returns>
    ///     A positive value if the frame data is writable (which is true if and  only if each of the underlying buffers
    ///     has only one reference, namely the one stored in this frame). Return 0 otherwise.
    /// </returns>
    /// <remarks>
    ///     Seealso:
    ///     <seealso cref="FrameMakeWritable" />
    ///     <para></para>
    ///     <seealso cref="BufferIsWritable" />
    /// </remarks>
    [UtilFunction("av_frame_is_writable")]
    public delegate int FrameIsWritable(FramePtr frame);

    /// <summary>
    ///     Ensure that the frame data is writable, avoiding data copy if possible.
    ///     <para></para>
    ///     Do nothing if the frame is writable, allocate new buffers and copy the data if it is not.
    /// </summary>
    /// <param name="frame"></param>
    /// <returns>0 on success, a negative AVERROR on error.</returns>
    /// <remarks>
    ///     Seealso:
    ///     <seealso cref="FrameIsWritable" />
    ///     <para></para>
    ///     <seealso cref="BufferMakeWritable" />
    ///     <para></para>
    ///     <seealso cref="BufferIsWritable" />
    /// </remarks>
    [UtilFunction("av_frame_make_writable")]
    // TODO: Error Code.
    public delegate int FrameMakeWritable(FramePtr frame);

    /// <summary>
    ///     Copy the frame data from src to dst.
    ///     <para></para>
    ///     This function does not allocate anything, dst must be already initialized and allocated with the same parameters as
    ///     src.
    ///     <para></para>
    ///     This function only copies the frame data (i.e. the contents of the data / extended data arrays), not any other
    ///     properties.
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="src"></param>
    /// <returns>>= 0 on success, a negative AVERROR on error.</returns>
    [UtilFunction("av_frame_copy")]
    // TODO: Error Code.
    public delegate int FrameCopy(FramePtr dst, FramePtr src);

    /// <summary>
    ///     Copy only "metadata" fields from src to dst.
    ///     <para></para>
    ///     Metadata for the purpose of this function are those fields that do not affect the data layout in the buffers.  E.g.
    ///     pts, sample rate (for audio) or sample aspect ratio (for video), but not width/height or channel layout. Side data
    ///     is also copied.
    /// </summary>
    /// <param name="dst"></param>
    /// <param name="src"></param>
    /// <returns></returns>
    [UtilFunction("av_frame_copy_props")]
    public delegate int FrameCopyProps(FramePtr dst, FramePtr src);

    /// <summary>
    ///     Get the buffer reference a given data plane is stored in.
    /// </summary>
    /// <param name="frame">index of the data plane of interest in frame->extended_data.</param>
    /// <param name="plane"></param>
    /// <returns>the buffer reference that contains the plane or NULL if the input frame is not valid.</returns>
    [UtilFunction("av_frame_get_plane_buffer")]
    public delegate BufferRefPtr FrameGetPlaneBuffer(FramePtr frame, int plane);

    /// <summary>
    ///     Add a new side data to a frame.
    /// </summary>
    /// <param name="frame">a frame to which the side data should be added</param>
    /// <param name="type">type of the added side data</param>
    /// <param name="size">size of the side data</param>
    /// <returns>newly added side data on success, NULL on error</returns>
    [UtilFunction("av_frame_new_side_data")]
    public unsafe delegate FrameSideData* FrameNewSideData(FramePtr frame,
        FrameSideDataType type,
        int size);

    /// <summary>
    ///     return a pointer to the side data of a given type on success, NULL if there is no side data with such type in this
    ///     frame.
    /// </summary>
    /// <param name="frame"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    [UtilFunction("av_frame_get_side_data")]
    public unsafe delegate FrameSideData* FrameGetSideData(FramePtr frame,
        FrameSideDataType type);

    /// <summary>
    ///     If side data of the supplied type exists in the frame, free it and remove it from the frame.
    /// </summary>
    /// <param name="frame"></param>
    /// <param name="type"></param>
    [UtilFunction("av_frame_remove_side_data")]
    public delegate void FrameRemoveSideData(FramePtr frame, FrameSideDataType type);

    /// <summary>
    ///     return a string identifying the side data type
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [UtilFunction("av_frame_side_data_name")]
    public unsafe delegate byte* FrameSideDataName(FrameSideDataType type);
}