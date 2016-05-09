// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: BPrint.cs
// Version: 20160508

using System;
using System.Runtime.InteropServices;

namespace Meta.Media.Interop.Util
{
    /// <summary>
    ///     Buffer to print data progressively.
    ///     <para></para>
    ///     The string buffer grows as necessary and is always 0-terminated. The content of the string is never accessed, and
    ///     thus is encoding-agnostic and can even hold binary data.
    ///     <para></para>
    ///     Small buffers are kept in the structure itself, and thus require no memory allocation at all (unless the contents
    ///     of the buffer is needed after the structure goes out of scope). This is almost as lightweight as declaring a local
    ///     "char buf[512]".
    ///     <para></para>
    ///     The length of the string can go beyond the allocated size: the buffer is then truncated, but the functions still
    ///     keep account of the actual total length.
    ///     <para></para>
    ///     In other words, buf->len can be greater than buf->size and records the total length of what would have been to the
    ///     buffer if there had been enough memory.
    ///     <para></para>
    ///     Append operations do not need to be tested for failure: if a memory allocation fails, data stop being appended to
    ///     the buffer, but the length is still updated. This situation can be tested with av_bprint_is_complete().
    ///     <para></para>
    ///     The size_max field determines several possible behaviours:
    ///     <para></para>
    ///     size_max = -1 (= UINT_MAX) or any large value will let the buffer be reallocated as necessary, with an amortized
    ///     linear cost.
    ///     <para></para>
    ///     size_max = 0 prevents writing anything to the buffer: only the total length is computed. The write operations can
    ///     then possibly be repeated in a buffer with exactly the necessary size (using size_init = size_max = len + 1).
    ///     <para></para>
    ///     size_max = 1 is automatically replaced by the exact size available in the structure itself, thus ensuring no
    ///     dynamic memory allocation. The internal buffer is large enough to hold a reasonable paragraph of text, such as the
    ///     current paragraph.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 1024)]
    // TODO: Structure operation.
    public unsafe struct BPrint
    {
        private byte* _str;
        private uint _length;
        private uint _size;
        private uint _sizeMax;

        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeConst = 1)]
        private byte[] _reservedInternalBuffer;
    }

    public struct BPrintPtr
    {
        internal BPrintPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public BPrint Struct => Marshal.PtrToStructure<BPrint>(Value);

        public static bool operator ==(BPrintPtr value1, BPrintPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(BPrintPtr value1, BPrintPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(BPrintPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BPrintPtr))
            {
                return false;
            }

            return Equals((BPrintPtr) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(BPrint)";
        }
    }

    public struct TimePtr
    {
        internal TimePtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public static bool operator ==(TimePtr value1, TimePtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(TimePtr value1, TimePtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(TimePtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TimePtr))
            {
                return false;
            }

            return Equals((TimePtr) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(Time)";
        }
    }

    /// <summary>
    ///     Init a print buffer.
    /// </summary>
    /// <param name="bPrint">buffer to init</param>
    /// <param name="size">initial size (including the final 0)</param>
    /// <param name="sizeMax">
    ///     maximum size;
    ///     <para></para>
    ///     0 means do not write anything, just count the length;
    ///     <para></para>
    ///     1 is replaced by the maximum value for automatic storage;
    ///     <para></para>
    ///     any large value means that the internal buffer will be reallocated as needed up to that limit; -1 is converted to
    ///     UINT_MAX, the largest limit possible.
    /// </param>
    [UtilFunction("av_bprint_init")]
    public delegate void BPrintInitialize(BPrintPtr bPrint, uint size, uint sizeMax);

    /// <summary>
    ///     Init a print buffer using a pre-existing buffer.
    /// </summary>
    /// <param name="bPrint">buffer structure to init</param>
    /// <param name="buffer">byte buffer to use for the string data</param>
    /// <param name="size">size of buffer</param>
    [UtilFunction("av_bprint_init_for_buffer")]
    public unsafe delegate void BPrintInitializeForBuffer(BPrintPtr bPrint, byte* buffer, uint size);

    /// <summary>
    ///     Append a formatted string to a print buffer.
    /// </summary>
    /// <param name="bPrint"></param>
    /// <param name="fmt"></param>
    /// <param name="args"></param>
    [UtilFunction("av_vbprintf")]
    public unsafe delegate void BPrintf(BPrintPtr bPrint, byte* fmt, byte* args);

    /// <summary>
    ///     Append char c n times to a print buffer.
    /// </summary>
    /// <param name="bPrint"></param>
    /// <param name="c"></param>
    /// <param name="n"></param>
    [UtilFunction("av_bprint_chars")]
    public delegate void BPrintChars(BPrintPtr bPrint, byte c, uint n);

    /// <summary>
    ///     Append data to a print buffer.
    /// </summary>
    /// <param name="bPrint">bprint buffer to use</param>
    /// <param name="data">pointer to data</param>
    /// <param name="size">size of data</param>
    [UtilFunction("av_bprint_append_data")]
    public unsafe delegate void BPrintAppendData(BPrintPtr bPrint, byte* data, uint size);

    /// <summary>
    ///     Append a formatted date and time to a print buffer.
    /// </summary>
    /// <param name="bPrint">bprint buffer to use</param>
    /// <param name="fmt">date and time format string, see strftime()</param>
    /// <param name="time">broken-down time structure to translate</param>
    [UtilFunction("av_bprint_strftime")]
    public unsafe delegate void BPrintTime(BPrintPtr bPrint, byte* fmt, TimePtr time);

    /// <summary>
    ///     Allocate bytes in the buffer for external use.
    /// </summary>
    /// <param name="bPrint">buffer structure</param>
    /// <param name="size">required size</param>
    /// <param name="mem">pointer to the memory area</param>
    /// <param name="actualSize">size of the memory area after allocation; can be larger or smaller than size</param>
    [UtilFunction("av_bprint_get_buffer")]
    public unsafe delegate void BPrintGetBuffer(BPrintPtr bPrint, uint size, out byte* mem, out uint actualSize);

    /// <summary>
    ///     Reset the string to "" but keep internal allocated data.
    /// </summary>
    /// <param name="bPrint"></param>
    [UtilFunction("av_bprint_clear")]
    public delegate void BPrintClear(BPrintPtr bPrint);

    /// <summary>
    ///     Finalize a print buffer.
    ///     <para></para>
    ///     The print buffer can no longer be used afterwards, but the len and size fields are still valid.
    /// </summary>
    /// <param name="bPrint"></param>
    /// <param name="retStr">
    ///     if not NULL, used to return a permanent copy of the buffer contents, or NULL if memory allocation
    ///     fails; if NULL, the buffer is discarded and freed
    /// </param>
    /// <returns>0 for success or error code (probably ERROR(ENOMEM))</returns>
    [UtilFunction("av_bprint_finalize")]
    // TODO: Error Code.
    public unsafe delegate int BPrintFinalize(BPrintPtr bPrint, out byte* retStr);

    /// <summary>
    ///     Escape the content in src and append it to dstbuf.
    /// </summary>
    /// <param name="bPrint">already inited destination bprint buffer</param>
    /// <param name="src">string containing the text to escape</param>
    /// <param name="specialChars">string containing the special characters which need to be escaped, can be NULL</param>
    /// <param name="mode">
    ///     escape mode to employ, see <see cref="EscapeMode" />.
    ///     <para></para>
    ///     Any unknown value for mode will be considered equivalent to <see cref="EscapeMode.Backslash" />, but this behaviour
    ///     can change without notice.
    /// </param>
    /// <param name="flags">flags which control how to escape, see <see cref="EscapeFlags" /></param>
    [UtilFunction("av_bprint_escape")]
    public unsafe delegate void BPrintEscape(
        BPrintPtr bPrint, byte* src, byte* specialChars, EscapeMode mode, EscapeFlags flags);
}