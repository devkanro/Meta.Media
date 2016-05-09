// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: Downmix.cs
// Version: 20160508

using System;
using System.Runtime.InteropServices;

namespace Meta.Media.Interop.Util
{
    /// <summary>
    /// Possible downmix types.
    /// </summary>
    public enum DownmixType
    {
        /// <summary>
        /// Not indicated.
        /// </summary>
        Unknown,
        /// <summary>
        /// Lo/Ro 2-channel downmix (Stereo).
        /// </summary>
        Loro,
        /// <summary>
        /// Lt/Rt 2-channel downmix, Dolby Surround compatible.
        /// </summary>
        Ltrt,
        /// <summary>
        /// Lt/Rt 2-channel downmix, Dolby Pro Logic II compatible.
        /// </summary>
        Dplii,
    };

    /// <summary>
    /// This structure describes optional metadata relevant to a downmix procedure.
    /// <para></para>
    /// All fields are set by the decoder to the value indicated in the audio bitstream (if present), or to a "sane" default otherwise.
    /// </summary>
    public struct DownmixInfo
    {
        /// <summary>
        /// Type of downmix preferred by the mastering engineer.
        /// </summary>
        private DownmixType _preferredDownmixType;

        /// <summary>
        /// Absolute scale factor representing the nominal level of the center channel during a regular downmix.
        /// </summary>
        private double _centerMixLevel;

        /// <summary>
        /// Absolute scale factor representing the nominal level of the center channel during an Lt/Rt compatible downmix.
        /// </summary>
        private double _centerMixLevelLtrt;

        /// <summary>
        /// Absolute scale factor representing the nominal level of the surround channels during a regular downmix.
        /// </summary>
        private double _surroundMixLevel;

        /// <summary>
        /// Absolute scale factor representing the nominal level of the surround channels during an Lt/Rt compatible downmix.
        /// </summary>
        private double _surroundMixLevelLtrt;

        /// <summary>
        /// Absolute scale factor representing the level at which the LFE data is mixed into L/R channels during downmixing.
        /// </summary>
        private double _lfeMixLevel;
    }

    public struct DownmixInfoPtr
    {
        internal DownmixInfoPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public DownmixInfo Struct => Marshal.PtrToStructure<DownmixInfo>(Value);

        public static bool operator ==(DownmixInfoPtr value1, DownmixInfoPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(DownmixInfoPtr value1, DownmixInfoPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(DownmixInfoPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DownmixInfoPtr))
            {
                return false;
            }

            return Equals((DownmixInfoPtr)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(DownmixInfo)";
        }
    }

    /// <summary>
    /// Get a frame's DownmixInfo side data for editing.
    /// <para></para>
    /// If the side data is absent, it is created and added to the frame.
    /// </summary>
    /// <param name="frame">the frame for which the side data is to be obtained or created</param>
    /// <returns>the DownmixInfo structure to be edited by the caller, or NULL if the structure cannot be allocated.</returns>
    [UtilFunction("av_downmix_info_update_side_data")]
    // TODO: IntPtr is FramePtr(Frame).
    public delegate DownmixInfoPtr DownmixInfoUpdateSideData(IntPtr frame);
}