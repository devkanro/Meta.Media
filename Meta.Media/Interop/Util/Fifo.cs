// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: Fifo.cs
// Version: 20160508

using System;
using System.Runtime.InteropServices;

namespace Meta.Media.Interop.Util
{
    // TODO: Structure operation.
    public unsafe struct FifoBuffer
    {
        private byte* _buffer;
        private byte* _rptr;
        private byte* _wptr;
        private byte* _end;
        private uint _rndx;
        private uint _wndx;
    }

    public struct FifoBufferPtr
    {
        internal FifoBufferPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public FifoBuffer Struct => Marshal.PtrToStructure<FifoBuffer>(Value);

        public static bool operator ==(FifoBufferPtr value1, FifoBufferPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(FifoBufferPtr value1, FifoBufferPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(FifoBufferPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is FifoBufferPtr))
            {
                return false;
            }

            return Equals((FifoBufferPtr)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(FifoBuffer)";
        }
    }

    /// <summary>
    /// Initialize an FifoBuffer.
    /// </summary>
    /// <param name="size">size of FIFO</param>
    /// <returns>FifoBuffer or NULL in case of memory allocation failure</returns>
    [UtilFunction("av_fifo_alloc")]
    public delegate FifoBufferPtr FifoAlloc(uint size);

    /// <summary>
    /// Initialize FifoBuffers.
    /// </summary>
    /// <param name="itemCount">number of elements</param>
    /// <param name="size">size of the single element</param>
    /// <returns>FifoBuffer or NULL in case of memory allocation failure</returns>
    [UtilFunction("av_fifo_alloc_array")]
    public delegate FifoBufferPtr FifoAllocArray(uint itemCount, uint size);

    /// <summary>
    /// Free an FifoBuffer.
    /// </summary>
    /// <param name="f">FifoBuffer to free</param>
    [UtilFunction("av_fifo_free")]
    public delegate void FifoFree(FifoBufferPtr f);
    
    /// <summary>
    /// Free an FifoBuffer and reset pointer to NULL.
    /// </summary>
    /// <param name="f">FifoBuffer to free</param>
    [UtilFunction("av_fifo_freep")]
    public delegate void FifoFreeP(ref FifoBufferPtr f);

    /// <summary>
    /// Reset the FifoBuffer to the state right after av_fifo_alloc, in particular it is emptied.
    /// </summary>
    /// <param name="f">FifoBuffer to reset</param>
    [UtilFunction("av_fifo_reset")]
    public delegate void FifoReset(FifoBufferPtr f);

    /// <summary>
    /// Return the amount of data in bytes in the FifoBuffer, that is the amount of data you can read from it.
    /// </summary>
    /// <param name="f">FifoBuffer to read from</param>
    /// <returns>size</returns>
    [UtilFunction("av_fifo_size")]
    public delegate int FifoSize(FifoBufferPtr f);

    /// <summary>
    /// Return the amount of space in bytes in the FifoBuffer, that is the amount of data you can write into it.
    /// </summary>
    /// <param name="f">FifoBuffer to write into</param>
    /// <returns>size</returns>
    [UtilFunction("av_fifo_space")]
    public delegate int FifoSpace(FifoBufferPtr f);

    public unsafe delegate void FifoGenericFunc(void* src, void* destBuf, int destBufSize);

    /// <summary>
    /// Feed data from an FifoBuffer to a user-supplied callback.
    /// </summary>
    /// <param name="f">FifoBuffer to read from</param>
    /// <param name="dest">data destination</param>
    /// <param name="bufSize">number of bytes to read</param>
    /// <param name="func">generic read function</param>
    /// <returns></returns>
    [UtilFunction("av_fifo_generic_read")]
    public unsafe delegate int FifoGenericRead(FifoBufferPtr f, void* dest, int bufSize, FifoGenericFunc func);

    /// <summary>
    /// Feed data from a user-supplied callback to an FifoBuffer.
    /// </summary>
    /// <param name="f">FifoBuffer to write to</param>
    /// <param name="src">data source; non-const since it may be used as a modifiable context by the function defined in func</param>
    /// <param name="size">number of bytes to write</param>
    /// <param name="func">generic write function. func must return the number of bytes written to dest_buf, or &lt;= 0 to indicate no more data available to write. If func is NULL, src is interpreted as a simple byte array for source data.</param>
    /// <returns>the number of bytes written to the FIFO</returns>
    [UtilFunction("av_fifo_generic_write")]
    public unsafe delegate int FifoGenericWrite(FifoBufferPtr f, void* src, int size, FifoGenericFunc func);

    /// <summary>
    /// Resize an FifoBuffer. In case of reallocation failure, the old FIFO is kept unchanged.
    /// </summary>
    /// <param name="f">FifoBuffer to resize</param>
    /// <param name="size">new FifoBuffer size in bytes</param>
    /// <returns>&lt;0 for failure, >=0 otherwise</returns>
    [UtilFunction("av_fifo_realloc2")]
    public delegate int FifoRealloc(FifoBufferPtr f, uint size);

    /// <summary>
    /// Enlarge an FifoBuffer. In case of reallocation failure, the old FIFO is kept unchanged. The new fifo size may be larger than the requested size.
    /// </summary>
    /// <param name="f">FifoBuffer to resize</param>
    /// <param name="additionalSpace">the amount of space in bytes to allocate in addition to <see cref="FifoSize"/></param>
    /// <returns>&lt;0 for failure, >=0 otherwise</returns>
    [UtilFunction("av_fifo_grow")]
    public delegate int FifoGrow(FifoBufferPtr f, uint additionalSpace);

    /// <summary>
    /// Read and discard the specified amount of data from an FifoBuffer.
    /// </summary>
    /// <param name="f">FifoBuffer to read from</param>
    /// <param name="size">amount of data to read in bytes</param>
    /// <returns></returns>
    [UtilFunction("av_fifo_drain")]
    public delegate int FifoDrain(FifoBufferPtr f, int size);

    // TODO: Inline function av_fifo_peek2().
}