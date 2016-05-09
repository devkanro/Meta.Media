// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: ChannelLayout.cs
// Version: 20160508

using System;

namespace Meta.Media.Interop.Util
{
    [Flags]
    internal enum ChannelLayoutMask : UInt64
    {
        FrontLeft = 0x00000001,
        FrontRight = 0x00000002,
        FrontCenter = 0x00000004,
        LowFrequency = 0x00000008,
        BackLeft = 0x00000010,
        BackRight = 0x00000020,
        FrontLeftOfCenter = 0x00000040,
        FrontRightOfCenter = 0x00000080,
        BackCenter = 0x00000100,
        SideLeft = 0x00000200,
        SideRight = 0x00000400,
        TopCenter = 0x00000800,
        TopFrontLeft = 0x00001000,
        TopFrontCenter = 0x00002000,
        TopFrontRight = 0x00004000,
        TopBackLeft = 0x00008000,
        TopBackCenter = 0x00010000,
        TopBackRight = 0x00020000,

        /// <summary>
        ///     Stereo downmix.
        /// </summary>
        StereoLeft = 0x20000000,

        /// <summary>
        ///     See <see cref="StereoLeft" />.
        /// </summary>
        StereoRight = 0x40000000,

        WideLeft = 0x0000000080000000UL,
        WideRight = 0x0000000100000000UL,
        SurroundDirectLeft = 0x0000000200000000UL,
        SurroundDirectRight = 0x0000000400000000UL,
        LowFrequency2 = 0x0000000800000000UL,
        LayoutNative = 0x8000000000000000UL
    }

    /// <summary>
    ///     A channel layout is a 64-bits integer with a bit set for every channel. The number of bits set must be equal to the
    ///     number of channels. The value 0 means that the channel layout is not known.
    /// </summary>
    /// <remarks>
    ///     this data structure is not powerful enough to handle channels combinations that have the same channel multiple
    ///     times, such as dual-mono.
    /// </remarks>
    [Flags]
    public enum ChannelLayout : UInt64
    {
        LayoutMono = (ChannelLayoutMask.FrontCenter),
        LayoutStereo = (ChannelLayoutMask.FrontLeft | ChannelLayoutMask.FrontRight),
        Layout2Point1 = (LayoutStereo | ChannelLayoutMask.LowFrequency),
        Layout21 = (LayoutStereo | ChannelLayoutMask.BackCenter),
        LayoutSurround = (LayoutStereo | ChannelLayoutMask.FrontCenter),
        Layout3Point1 = (LayoutSurround | ChannelLayoutMask.LowFrequency),
        Layout4Point0 = (LayoutSurround | ChannelLayoutMask.BackCenter),
        Layout4Point1 = (Layout4Point0 | ChannelLayoutMask.LowFrequency),
        Layout22 = (LayoutStereo | ChannelLayoutMask.SideLeft | ChannelLayoutMask.SideRight),
        LayoutQuad = (LayoutStereo | ChannelLayoutMask.BackLeft | ChannelLayoutMask.BackRight),
        Layout5Point0 = (LayoutSurround | ChannelLayoutMask.SideLeft | ChannelLayoutMask.SideRight),
        Layout5Point1 = (Layout5Point0 | ChannelLayoutMask.LowFrequency),
        Layout5Point0Back = (LayoutSurround | ChannelLayoutMask.BackLeft | ChannelLayoutMask.BackRight),
        Layout5Point1Back = (Layout5Point0Back | ChannelLayoutMask.LowFrequency),
        Layout6Point0 = (Layout5Point0 | ChannelLayoutMask.BackCenter),
        Layout6Point0Front = (Layout22 | ChannelLayoutMask.FrontLeftOfCenter | ChannelLayoutMask.FrontRightOfCenter),
        LayoutHexagonal = (Layout5Point0Back | ChannelLayoutMask.BackCenter),
        Layout6Point1 = (Layout5Point1 | ChannelLayoutMask.BackCenter),
        Layout6Point1Back = (Layout5Point1Back | ChannelLayoutMask.BackCenter),
        Layout6Point1Front = (Layout6Point0Front | ChannelLayoutMask.LowFrequency),
        Layout7Point0 = (Layout5Point0 | ChannelLayoutMask.BackLeft | ChannelLayoutMask.BackRight),
        Layout7Point0Front = (Layout5Point0 | ChannelLayoutMask.FrontLeftOfCenter | ChannelLayoutMask.FrontRightOfCenter),
        Layout7Point1 = (Layout5Point1 | ChannelLayoutMask.BackLeft | ChannelLayoutMask.BackRight),
        Layout7Point1Wide = (Layout5Point1 | ChannelLayoutMask.FrontLeftOfCenter | ChannelLayoutMask.FrontRightOfCenter),

        Layout7Point1WideBack =
            (Layout5Point1Back | ChannelLayoutMask.FrontLeftOfCenter | ChannelLayoutMask.FrontRightOfCenter),

        LayoutOctagonal =
            (Layout5Point0 | ChannelLayoutMask.BackLeft | ChannelLayoutMask.BackCenter | ChannelLayoutMask.BackRight),

        LayoutStereoDownmix = (ChannelLayoutMask.StereoLeft | ChannelLayoutMask.StereoRight)
    }

    public enum MatrixEncoding
    {
        None,
        Dolby,
        Dplii,
        DpliiX,
        DpliiZ,
        DolbyEx,
        DolbyHeadphone
    }
    
    /// <summary>
    ///     Return a channel layout id that matches name, or 0 if no match is found.
    /// </summary>
    /// <param name="name">channel layout name</param>
    /// <returns>channel layout id</returns>
    /// <remarks>
    ///     name can be one or several of the following notations, separated by '+' or '|':
    ///     <para></para>
    ///     - the name of an usual channel layout (mono, stereo, 4.0, quad, 5.0, 5.0(side), 5.1, 5.1(side), 7.1, 7.1(wide),
    ///     downmix);
    ///     <para></para>
    ///     - the name of a single channel(FL, FR, FC, LFE, BL, BR, FLC, FRC, BC, SL, SR, TC, TFL, TFC, TFR, TBL, TBC, TBR, DL,
    ///     DR);
    ///     <para></para>
    ///     - a number of channels, in decimal, optionally followed by 'c', yielding the default channel layout for that number
    ///     of channels(<see cref="GetDefaultChannelLayout"/>);
    ///     <para></para>
    ///     - a channel layout mask, in hexadecimal starting with "0x" (<see cref="ChannelLayout"/>).
    ///     <para></para>
    ///     Example: "stereo+FC" = "2c+FC" = "2c+1c" = "0x7"
    ///     <para></para>
    ///     Warning: Starting from the next major bump the trailing character 'c' to specify a number of channels will be
    ///     required, while a channel layout mask could also be specified as a decimal number (if and only if not followed by
    ///     "c").
    /// </remarks>
    [UtilFunction("av_get_channel_layout")]
    public unsafe delegate ChannelLayout GetChannelLayout(byte* name);

    /// <summary>
    /// Return a description of a channel layout.
    ///     <para></para>
    /// If channelCount is &lt;= 0, it is guessed from the channel_layout.
    /// </summary>
    /// <param name="buffer">put here the string containing the channel layout</param>
    /// <param name="bufferSize">size in bytes of the buffer</param>
    /// <param name="channelCount"></param>
    /// <param name="channelLayout"></param>
    [UtilFunction("av_get_channel_layout_string")]
    public unsafe delegate void GetChannelLayoutString(byte* buffer, int bufferSize, int channelCount, ChannelLayout channelLayout);

    /// <summary>
    /// Append a description of a channel layout to a bPrint buffer.
    /// </summary>
    /// <param name="bPrint"></param>
    /// <param name="channelCount"></param>
    /// <param name="channelLayout"></param>
    [UtilFunction("av_bprint_channel_layout")]
    public delegate void BPrintChannelLayout(BPrintPtr bPrint, int channelCount, ChannelLayout channelLayout);

    /// <summary>
    /// Return the number of channels in the channel layout.
    /// </summary>
    /// <param name="channelLayout"></param>
    /// <returns></returns>
    [UtilFunction("av_get_channel_layout_nb_channels")]
    public delegate int GetChannelLayoutChannelCount(ChannelLayout channelLayout);

    /// <summary>
    /// Return default channel layout for a given number of channels.
    /// </summary>
    /// <param name="channelCount"></param>
    /// <returns></returns>
    [UtilFunction("av_get_default_channel_layout")]
    public delegate ChannelLayout GetDefaultChannelLayout(int channelCount);

    /// <summary>
    /// Get the index of a channel in channelLayout.
    /// </summary>
    /// <param name="channelLayout"></param>
    /// <param name="channel">a channel layout describing exactly one channel which must be present in channel_layout.</param>
    /// <returns>index of channel in channel_layout on success, a negative ERROR on error.</returns>
    [UtilFunction("av_get_channel_layout_channel_index")]
    // TODO: Error Code.
    public delegate int GetChannelLayoutChannelIndex(ChannelLayout channelLayout, ChannelLayout channel);

    /// <summary>
    /// Get the channel with the given index in channel_layout.
    /// </summary>
    /// <param name="channelLayout"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    [UtilFunction("av_channel_layout_extract_channel")]
    public delegate ChannelLayout GetChannelLayoutExtractChannel(ChannelLayout channelLayout, int index);

    /// <summary>
    /// Get the name of a given channel.
    /// </summary>
    /// <param name="channel"></param>
    /// <returns>channel name on success, NULL on error.</returns>
    [UtilFunction("av_get_channel_name")]
    public unsafe delegate byte* GetChannelName(ChannelLayout channel);

    /// <summary>
    /// Get the description of a given channel.
    /// </summary>
    /// <param name="channel">a channel layout with a single channel</param>
    /// <returns>channel description on success, NULL on error</returns>
    [UtilFunction("av_get_channel_description")]
    public unsafe delegate byte* GetChannelDescription(ChannelLayout channel);

    /// <summary>
    /// Get the value and name of a standard channel layout.
    /// </summary>
    /// <param name="index">[IN]index in an internal list, starting at 0</param>
    /// <param name="layout">[OUT]channel layout mask</param>
    /// <param name="name">[OUT]name of the layout</param>
    /// <returns>0  if the layout exists, &lt;0 if index is beyond the limits</returns>
    [UtilFunction("av_get_standard_channel_layout")]
    public unsafe delegate int GetStandardChannelLayout(uint index, out ulong layout, out byte* name);
}