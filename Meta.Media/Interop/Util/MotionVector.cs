// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: MotionVector.cs
// Version: 20160509

namespace Meta.Media.Interop.Util
{
    public struct MotionVector
    {
        /// <summary>
        /// Where the current macroblock comes from; negative value when it comes from the past, positive value when it comes from the future. XXX: set exact relative ref frame reference instead of a +/- 1 "direction".
        /// </summary>
        private int _source;
        /// <summary>
        /// Width of the block.
        /// </summary>
        private byte _width;
        /// <summary>
        /// Height of the block.
        /// </summary>
        private byte _height;
        /// <summary>
        /// Absolute source position. Can be outside the frame area.
        /// </summary>
        private short _srcX;
        /// <summary>
        /// Absolute source position. Can be outside the frame area.
        /// </summary>
        private short _srcY;
        /// <summary>
        /// Absolute destination position. Can be outside the frame area.
        /// </summary>
        private short _dstX;
        /// <summary>
        /// Absolute destination position. Can be outside the frame area.
        /// </summary>
        private short _dstY;
        /// <summary>
        /// Extra flag information. Currently unused.
        /// </summary>
        private ulong _flags;
    }
}