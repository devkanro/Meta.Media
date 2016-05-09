// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: Buffer.cs
// Version: 20160508

using System;
using System.Runtime.InteropServices;

namespace Meta.Media.Interop.Util
{
    /// <summary>
    ///     A reference counted buffer type. It is opaque and is meant to be used through references (BufferRef).
    ///     <para></para>
    ///     Buffer is an API for reference-counted data buffers.
    ///     <para></para>
    ///     There are two core objects in this API -- Buffer and BufferRef. Buffer represents the data buffer itself; it is
    ///     opaque and not meant to be accessed by the caller directly, but only through BufferRef. However, the caller may
    ///     e.g. compare two Buffer pointers to check whether two different references are describing the same data buffer.
    ///     BufferRef represents a single reference to an Buffer and it is the object that may be manipulated by the caller
    ///     directly.
    ///     <para></para>
    ///     There are two functions provided for creating a new Buffer with a single reference -- <see cref="BufferAlloc" /> to
    ///     just allocate a new buffer, and <see cref="BufferCreate" /> to wrap an existing array in an Buffer.From an existing
    ///     reference, additional references may be created with <see cref="BufferCreateReference" />. Use
    ///     <see cref="BufferReleaseReference" /> to free a reference(this will automatically free the data once all the
    ///     references are freed).
    ///     <para></para>
    ///     The convention throughout this API and the rest of FFmpeg is such that the buffer is considered writable if there
    ///     exists only one reference to it (and it has not been marked as read-only). The <see cref="BufferIsWritable" />
    ///     function is provided to check whether this is true and av_buffer_make_writable() will automatically create a new
    ///     writable buffer when necessary.Of course nothing prevents the calling code from violating this convention, however
    ///     that is safe only when all the existing references are under its control.
    ///     <para></para>
    /// </summary>
    /// <remarks>
    ///     Note: Referencing and unreferencing the buffers is thread-safe and thus may be done from multiple threads
    ///     simultaneously without any need for additional locking.
    ///     <para></para>
    ///     Note: Two different references to the same buffer can point to different parts of the buffer (i.e.their
    ///     BufferRef.data will not be equal).
    /// </remarks>
    public struct BufferPtr
    {
        internal BufferPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public static bool operator ==(BufferPtr value1, BufferPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(BufferPtr value1, BufferPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(BufferPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BufferPtr))
            {
                return false;
            }

            return Equals((BufferPtr) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(Buffer)";
        }
    }

    /// <summary>
    ///     A reference to a data buffer.
    ///     <para></para>
    ///     The size of this struct is not a part of the public ABI and it is not meant to be allocated directly.
    /// </summary>
    public unsafe struct BufferRef
    {
        private BufferPtr _buffer;

        /// <summary>
        ///     The data buffer. It is considered writable if and only if this is the only reference to the buffer, in which case
        ///     av_buffer_is_writable() returns 1.
        /// </summary>
        // TODO: Link to av_buffer_is_writable().
        private byte* _data;

        /// <summary>
        ///     Size of data in bytes.
        /// </summary>
        private int _size;
    }

    public struct BufferRefPtr
    {
        internal BufferRefPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public BufferRef Struct => Marshal.PtrToStructure<BufferRef>(Value);

        public static bool operator ==(BufferRefPtr value1, BufferRefPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(BufferRefPtr value1, BufferRefPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(BufferRefPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BufferRefPtr))
            {
                return false;
            }

            return Equals((BufferRefPtr) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(BufferRef)";
        }
    }

    public struct BufferPoolPtr
    {
        internal BufferPoolPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public static bool operator ==(BufferPoolPtr value1, BufferPoolPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(BufferPoolPtr value1, BufferPoolPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(BufferPoolPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BufferPoolPtr))
            {
                return false;
            }

            return Equals((BufferPoolPtr) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(BufferPool)";
        }
    }

    public enum BufferFlags
    {
        None,
        ReadOnly = 1 << 0
    }

    /// <summary>
    ///     Allocate an Buffer of the given size using av_malloc().
    /// </summary>
    /// <param name="size"></param>
    /// <returns>an BufferRef of given size or NULL when out of memory</returns>
    [UtilFunction("av_buffer_alloc")]
    // TODO: Link to av_malloc().
    public delegate BufferRefPtr BufferAlloc(int size);

    /// <summary>
    ///     Same as <see cref="BufferAlloc" />, except the returned buffer will be initialized to zero.
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    [UtilFunction("av_buffer_allocz")]
    public delegate BufferRefPtr BufferAllocZ(int size);

    [UtilFunction("av_buffer_default_free")]
    public unsafe delegate void BufferFreeCallback(void* opaque, byte* data);

    /// <summary>
    ///     Create an Buffer from an existing array.
    ///     <para></para>
    ///     If this function is successful, data is owned by the Buffer. The caller may only access data through the returned
    ///     BufferRef and references derived from it.
    ///     <para></para>
    ///     If this function fails, data is left untouched.
    /// </summary>
    /// <param name="data">data array</param>
    /// <param name="size">size of data in bytes</param>
    /// <param name="freeCallback">a callback for freeing this buffer's data</param>
    /// <param name="opaque">parameter to be got for processing or passed to free</param>
    /// <param name="flags">see <see cref="BufferFlags" /></param>
    /// <returns>an BufferRef referring to data on success, NULL on failure.</returns>
    [UtilFunction("av_buffer_create")]
    public unsafe delegate BufferRefPtr BufferCreate(
        byte* data, int size, BufferFreeCallback freeCallback, void* opaque, BufferFlags flags);

    /// <summary>
    ///     Create a new reference to an Buffer.
    /// </summary>
    /// <param name="buf"></param>
    /// <returns>a new BufferRef referring to the same Buffer as buf or NULL on failure.</returns>
    [UtilFunction("av_buffer_ref")]
    public delegate BufferRefPtr BufferCreateReference(BufferRefPtr buf);

    /// <summary>
    ///     Free a given reference and automatically free the buffer if there are no more references to it.
    /// </summary>
    /// <param name="buf">the reference to be freed. The pointer is set to NULL on return.</param>
    [UtilFunction("av_buffer_unref")]
    public unsafe delegate void BufferReleaseReference(BufferRefPtr* buf);

    /// <summary>
    /// </summary>
    /// <param name="buf"></param>
    /// <returns>
    ///     1 if the caller may write to the data referred to by buf (which is true if and only if buf is the only reference to
    ///     the underlying Buffer). Return 0 otherwise.
    ///     <para></para>
    ///     A positive answer is valid until <see cref="BufferCreateReference" /> is called on buf.
    /// </returns>
    [UtilFunction("av_buffer_is_writable")]
    public delegate int BufferIsWritable(BufferRefPtr buf);

    /// <summary>
    /// </summary>
    /// <param name="buf"></param>
    /// <returns>the opaque parameter set by av_buffer_create.</returns>
    [UtilFunction("av_buffer_get_opaque")]
    public unsafe delegate void* BufferGetOpaque(BufferRefPtr buf);

    [UtilFunction("av_buffer_get_ref_count")]
    public delegate int BufferGetReferenceCount(BufferRefPtr buf);

    /// <summary>
    ///     Create a writable reference from a given buffer reference, avoiding data copy if possible.
    /// </summary>
    /// <param name="buf">
    ///     buffer reference to make writable. On success, buf is either left untouched, or it is unreferenced
    ///     and a new writable BufferRef is written in its place. On failure, buf is left untouched.
    /// </param>
    /// <returns>0 on success, a negative ERROR on failure.</returns>
    [UtilFunction("av_buffer_make_writable")]
    // TODO: Error Code.
    public unsafe delegate int BufferMakeWritable(BufferRefPtr* buf);

    /// <summary>
    ///     Reallocate a given buffer.
    /// </summary>
    /// <param name="buf">
    ///     a buffer reference to reallocate. On success, buf will be unreferenced and a new reference with the
    ///     required size will be written in its place. On failure buf will be left untouched. *buf may be NULL, then a new
    ///     buffer is allocated.
    /// </param>
    /// <param name="size">required new buffer size.</param>
    /// <returns>0 on success, a negative ERROR on failure.</returns>
    /// <remarks>
    ///     the buffer is actually reallocated with av_realloc() only if it was initially allocated through BufferRealloc(NULL)
    ///     and there is only one reference to it (i.e. the one passed to this function). In all other cases a new buffer is
    ///     allocated and the data is copied.
    /// </remarks>
    [UtilFunction("av_buffer_realloc")]
    // TODO: Link to av_realloc().
    // TODO: Error Code.
    public unsafe delegate int BufferRealloc(BufferRefPtr* buf, int size);

    /// <summary>
    ///     Allocate and initialize a buffer pool.
    /// </summary>
    /// <param name="size">size of each buffer in this pool</param>
    /// <param name="alloc">
    ///     a function that will be used to allocate new buffers when the pool is empty. May be NULL, then the
    ///     default allocator will be used (<see cref="BufferAlloc" />).
    /// </param>
    /// <returns>newly created buffer pool on success, NULL on error.</returns>
    [UtilFunction("av_buffer_pool_init")]
    public delegate BufferPoolPtr BufferPoolInitialize(int size, BufferAlloc alloc);

    /// <summary>
    ///     Mark the pool as being available for freeing. It will actually be freed only once all the allocated buffers
    ///     associated with the pool are released. Thus it is safe to call this function while some of the allocated buffers
    ///     are still in use.
    /// </summary>
    /// <param name="pool">pointer to the pool to be freed. It will be set to NULL.</param>
    [UtilFunction("av_buffer_pool_uninit")]
    // TODO: See av_buffer_pool_can_uninit().
    public unsafe delegate void BufferPoolUninitialize(BufferPoolPtr* pool);

    /// <summary>
    ///     Allocate a new Buffer, reusing an old buffer from the pool when available.
    ///     <para></para>
    ///     This function may be called simultaneously from multiple threads.
    /// </summary>
    /// <param name="pool"></param>
    /// <returns>a reference to the new buffer on success, NULL on error.</returns>
    [UtilFunction("av_buffer_pool_get")]
    public delegate BufferRefPtr BufferPoolGet(BufferPoolPtr pool);
}