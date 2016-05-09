// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: Memory.cs
// Version: 20160509

namespace Meta.Media.Interop.Util
{
    /// <summary>
    ///     Allocate a block of size bytes with alignment suitable for all memory accesses (including vectors if available on
    ///     the CPU).
    /// </summary>
    /// <param name="size">Size in bytes for the memory block to be allocated.</param>
    /// <returns>Pointer to the allocated block, NULL if the block cannot be allocated.</returns>
    /// <remarks>
    ///     Seealso:
    ///     <seealso cref="MallocZ" />
    /// </remarks>
    [UtilFunction("av_malloc")]
    public unsafe delegate void* Malloc(uint size);

    /// <summary>
    ///     Allocate or reallocate a block of memory. If ptr is NULL and size > 0, allocate a new block. If size is zero, free
    ///     the memory block pointed to by ptr.
    /// </summary>
    /// <param name="ptr">Pointer to a memory block already allocated with <see cref="Realloc" /> or NULL.</param>
    /// <param name="size">Size in bytes of the memory block to be allocated or reallocated.</param>
    /// <returns>
    ///     Pointer to a newly-reallocated block or NULL if the block cannot be reallocated or the function is used to
    ///     free the memory block.
    /// </returns>
    /// <remarks>
    ///     Warning:
    ///     Pointers originating from the <see cref="Malloc" /> family of functions must not be passed to av_realloc(). The
    ///     former can be implemented using memalign() (or other functions), and there is no guarantee that pointers from such
    ///     functions can be passed to realloc() at all. The situation is undefined according to POSIX and may crash with some
    ///     libc implementations.
    ///     <para></para>
    ///     Seealso:
    ///     <seealso cref="FastRealloc" />
    /// </remarks>
    [UtilFunction("av_realloc")]
    public unsafe delegate void* Realloc(void* ptr, uint size);

    /// <summary>
    ///     Allocate or reallocate a block of memory.
    ///     <para></para>
    ///     This function does the same thing as av_realloc, except:
    ///     <para></para>
    ///     - It takes two arguments and checks the result of the multiplication for integer overflow.
    ///     <para></para>
    ///     - It frees the input block in case of failure, thus avoiding the memory leak with the classic "buf = realloc(buf);
    ///     if (!buf) return -1;".
    /// </summary>
    /// <param name="ptr"></param>
    /// <param name="elemmentCount">Number of elements</param>
    /// <param name="elementSize">Size of the single element</param>
    /// <returns></returns>
    [UtilFunction("av_realloc_f")]
    public unsafe delegate void* ReallocF(void* ptr, uint elemmentCount, uint elementSize);

    /// <summary>
    ///     Allocate or reallocate a block of memory.
    ///     If *ptr is NULL and size > 0, allocate a new block. If size is zero, free the memory block pointed to by ptr.
    /// </summary>
    /// <param name="ptr">
    ///     Pointer to a pointer to a memory block already allocated with <see cref="Realloc" />, or pointer to a
    ///     pointer to NULL. The pointer is updated on success, or freed on failure.
    /// </param>
    /// <param name="size">Size in bytes for the memory block to be allocated or reallocated</param>
    /// <returns>Zero on success, an AVERROR error code on failure.</returns>
    /// <remarks>
    ///     Warning:
    ///     Pointers originating from the av_malloc() family of functions must not be passed to <see cref="Realloc" />. The
    ///     former can be implemented using memalign() (or other functions), and there is no guarantee that pointers from such
    ///     functions can be passed to realloc() at all. The situation is undefined according to POSIX and may crash with some
    ///     libc implementations.
    ///     <para></para>
    /// </remarks>
    [UtilFunction("av_realloc_cp")]
    // TODO: Error Code.
    public unsafe delegate int ReallocCp(void* ptr, uint size);

    /// <summary>
    ///     Allocate or reallocate an array.
    ///     If ptr is NULL and nmemb > 0, allocate a new block. If nmemb is zero, free the memory block pointed to by ptr.
    /// </summary>
    /// <param name="ptr">Pointer to a memory block already allocated with <see cref="Realloc" /> or NULL.</param>
    /// <param name="elemmentCount">Number of elements</param>
    /// <param name="elementSize">Size of the single element</param>
    /// <returns>
    ///     Pointer to a newly-reallocated block or NULL if the block cannot be reallocated or the function is used to
    ///     free the memory block.
    /// </returns>
    /// <remarks>
    ///     Warning:
    ///     Pointers originating from the av_malloc() family of functions must not be passed to <see cref="Realloc" />. The
    ///     former can be implemented using memalign() (or other functions), and there is no guarantee that pointers from such
    ///     functions can be passed to realloc() at all. The situation is undefined according to POSIX and may crash with some
    ///     libc implementations.
    ///     <para></para>
    /// </remarks>
    [UtilFunction("av_realloc_array")]
    public unsafe delegate void* ReallocArray(void* ptr, uint elemmentCount, uint elementSize);

    /// <summary>
    ///     Allocate or reallocate an array through a pointer to a pointer.
    ///     If *ptr is NULL and nmemb > 0, allocate a new block. If nmemb is zero, free the memory block pointed to by ptr.
    /// </summary>
    /// <param name="ptr">
    ///     Pointer to a memory block already allocated with <see cref="Realloc" /> or NULL. The pointer is
    ///     updated on success, or freed on failure.
    /// </param>
    /// <param name="elemmentCount">Number of elements</param>
    /// <param name="elementSize">Size of the single element</param>
    /// <returns>Zero on success, an AVERROR error code on failure.</returns>
    /// <remarks>
    ///     Warning:
    ///     Pointers originating from the av_malloc() family of functions must not be passed to <see cref="Realloc" />. The
    ///     former can be implemented using memalign() (or other functions), and there is no guarantee that pointers from such
    ///     functions can be passed to realloc() at all. The situation is undefined according to POSIX and may crash with some
    ///     libc implementations.
    ///     <para></para>
    /// </remarks>
    [UtilFunction("av_reallocp_array")]
    // TODO: Error Code.
    public unsafe delegate int ReallocArrayCp(void* ptr, uint elemmentCount, uint elementSize);

    /// <summary>
    ///     Free a memory block which has been allocated with <see cref="Malloc" /> or <see cref="Realloc" />.
    /// </summary>
    /// <param name="ptr">Pointer to the memory block which should be freed.</param>
    /// <remarks>
    ///     Note:
    ///     ptr = NULL is explicitly allowed.
    ///     <para></para>
    ///     It is recommended that you use <see cref="FreeP" /> instead.
    ///     <para></para>
    ///     Seealso:
    ///     <seealso cref="FreeP" />
    /// </remarks>
    [UtilFunction("av_free")]
    public unsafe delegate void Free(void* ptr);

    /// <summary>
    ///     Allocate a block of size bytes with alignment suitable for all memory accesses (including vectors if available on
    ///     the CPU) and zero all the bytes of the block.
    /// </summary>
    /// <param name="size">Size in bytes for the memory block to be allocated.</param>
    /// <returns>Pointer to the allocated block, NULL if it cannot be allocated.</returns>
    /// <remarks>
    ///     Seealso:
    ///     <seealso cref="Malloc" />
    /// </remarks>
    [UtilFunction("av_mallocz")]
    public unsafe delegate void* MallocZ(uint size);

    /// <summary>
    ///     Allocate a block of elemmentCount * size bytes with alignment suitable for all memory accesses (including vectors
    ///     if available on the CPU) and zero all the bytes of the block.
    ///     <para></para>
    ///     The allocation will fail if nmemb * size is greater than or equal to INT_MAX.
    /// </summary>
    /// <param name="size"></param>
    /// <returns>Pointer to the allocated block, NULL if it cannot be allocated.</returns>
    [UtilFunction("av_calloc")]
    public unsafe delegate void* Calloc(uint elemmentCount, uint size);

    /// <summary>
    ///     Duplicate the string s.
    /// </summary>
    /// <param name="str">string to be duplicated</param>
    /// <returns>Pointer to a newly-allocated string containing a copy of s or NULL if the string cannot be allocated.</returns>
    [UtilFunction("av_strdup")]
    public unsafe delegate byte* StrDup(byte* str);

    /// <summary>
    ///     Duplicate a substring of the string s.
    /// </summary>
    /// <param name="str">string to be duplicated</param>
    /// <param name="len">the maximum length of the resulting string (not counting the terminating byte).</param>
    /// <returns>Pointer to a newly-allocated string containing a copy of s or NULL if the string cannot be allocated.</returns>
    [UtilFunction("av_strndup")]
    public unsafe delegate byte* StrNDup(byte* str, uint len);

    /// <summary>
    ///     Duplicate the buffer p.
    /// </summary>
    /// <param name="p">buffer to be duplicated</param>
    /// <param name="size"></param>
    /// <returns>Pointer to a newly allocated buffer containing a copy of p or NULL if the buffer cannot be allocated.</returns>
    [UtilFunction("av_memdup")]
    public unsafe delegate void* MemDup(void* p, uint size);

    /// <summary>
    ///     Free a memory block which has been allocated with <see cref="Malloc" /> or <see cref="Realloc" /> and set the
    ///     pointer pointing to it to NULL.
    /// </summary>
    /// <param name="ptr">Pointer to the pointer to the memory block which should be freed.</param>
    /// <remarks>
    ///     Note:
    ///     passing a pointer to a NULL pointer is safe and leads to no action.
    ///     <para></para>
    ///     Seealso:
    ///     <seealso cref="Free" />
    /// </remarks>
    [UtilFunction("av_freep")]
    public unsafe delegate void FreeP(ref void* ptr);

    /// <summary>
    ///     Add an element to a dynamic array.
    ///     <para></para>
    ///     he array to grow is supposed to be an array of pointers to structures, and the element to add must be a pointer to
    ///     an already allocated structure.
    ///     <para></para>
    ///     The array is reallocated when its size reaches powers of 2. Therefore, the amortized cost of adding an element is
    ///     constant.
    ///     <para></para>
    ///     In case of success, the pointer to the array is updated in order to point to the new grown array, and the number
    ///     pointed to by nb_ptr is incremented.
    ///     <para></para>
    ///     In case of failure, the array is freed, *tab_ptr is set to NULL and *nb_ptr is set to 0.
    /// </summary>
    /// <param name="tabPtr">pointer to the array to grow</param>
    /// <param name="nbPtr">pointer to the number of elements in the array</param>
    /// <param name="element">element to add</param>
    /// <remarks>
    ///     Seealso:
    ///     <seealso cref="DynamicArrayAddNoFree" />
    ///     <para></para>
    ///     Seealso:
    ///     <seealso cref="DynamicArrayAdd2" />
    /// </remarks>
    [UtilFunction("av_dynarray_add")]
    public unsafe delegate void DynamicArrayAdd(void* tabPtr, int* nbPtr, void* element);

    /// <summary>
    ///     Add an element to a dynamic array.
    ///     <para></para>
    ///     Function has the same functionality as <see cref="DynamicArrayAdd" />, but it doesn't free memory on fails. It
    ///     returns error code instead and leave current buffer untouched.
    /// </summary>
    /// <param name="tabPtr">pointer to the array to grow</param>
    /// <param name="nbPtr">pointer to the number of elements in the array</param>
    /// <param name="element">element to add</param>
    /// <returns>return >=0 on success, negative otherwise.</returns>
    /// <remarks>
    ///     Seealso:
    ///     <seealso cref="DynamicArrayAdd" />
    ///     <para></para>
    ///     Seealso:
    ///     <seealso cref="DynamicArrayAdd2" />
    /// </remarks>
    [UtilFunction("av_dynarray_add_nofree")]
    public unsafe delegate int DynamicArrayAddNoFree(void* tabPtr, int* nbPtr, void* element);

    /// <summary>
    ///     Add an element of size elem_size to a dynamic array.
    ///     <para></para>
    ///     The array is reallocated when its number of elements reaches powers of 2.
    ///     Therefore, the amortized cost of adding an element is constant.
    ///     <para></para>
    ///     In case of success, the pointer to the array is updated in order to point to the new grown array, and the number
    ///     pointed to by nb_ptr is incremented.
    ///     <para></para>
    ///     In case of failure, the array is freed, *tab_ptr is set to NULL and *nb_ptr is set to 0.
    /// </summary>
    /// <param name="tabPtr"></param>
    /// <param name="nbPtr"></param>
    /// <param name="elementSize">size in bytes of the elements in the array</param>
    /// <param name="elementData">
    ///     pointer to the data of the element to add. If NULL, the space of the new added element is not
    ///     filled.
    /// </param>
    /// <returns>
    ///     pointer to the data of the element to copy in the new allocated space. If NULL, the new allocated space is
    ///     left uninitialized."
    /// </returns>
    /// <remarks>
    ///     Seealso:
    ///     <seealso cref="DynamicArrayAdd" />
    ///     <para></para>
    ///     Seealso:
    ///     <seealso cref="DynamicArrayAddNoFree" />
    /// </remarks>
    [UtilFunction("av_dynarray2_add")]
    public unsafe delegate void* DynamicArrayAdd2(ref void* tabPtr, int* nbPtr, uint elementSize, byte* elementData);

    /// <summary>
    ///     Set the maximum size that may me allocated in one block.
    /// </summary>
    /// <param name="max"></param>
    [UtilFunction("av_max_alloc")]
    public delegate void MacAlloc(uint max);

    /// <summary>
    ///     deliberately overlapping memcpy implementation
    /// </summary>
    /// <param name="dst">destination buffer</param>
    /// <param name="back">how many bytes back we start (the initial size of the overlapping window), must be > 0</param>
    /// <param name="cnt">number of bytes to copy, must be >= 0</param>
    /// <returns></returns>
    /// <remarks>
    ///     Note:
    ///     cnt > back is valid, this will copy the bytes we just copied, thus creating a repeating pattern with a period
    ///     length of back.
    /// </remarks>
    [UtilFunction("av_memcpy_backptr")]
    public unsafe delegate void MemcpyBackPtr(byte* dst, int back, int cnt);

    /// <summary>
    ///     Reallocate the given block if it is not large enough, otherwise do nothing.
    /// </summary>
    /// <param name="ptr"></param>
    /// <param name="size"></param>
    /// <param name="minSize"></param>
    /// <returns></returns>
    /// <remarks>
    ///     Seealso:
    ///     <seealso cref="Realloc" />
    /// </remarks>
    [UtilFunction("av_fast_realloc")]
    public unsafe delegate void* FastRealloc(void* ptr, int* size, uint minSize);

    /// <summary>
    ///     Allocate a buffer, reusing the given one if large enough.
    ///     <para></para>
    ///     Contrary to av_fast_realloc the current buffer contents might not be preserved and on error the old buffer is
    ///     freed, thus no special handling to avoid memleaks is necessary.
    /// </summary>
    /// <param name="ptr">pointer to pointer to already allocated buffer, overwritten with pointer to new buffer</param>
    /// <param name="size">size of the buffer *ptr points to</param>
    /// <param name="minSize">minimum size of *ptr buffer after returning, *ptr will be NULL and *size 0 if an error occurred.</param>
    /// <returns></returns>
    [UtilFunction("av_fast_malloc")]
    public unsafe delegate void* FastMalloc(void* ptr, int* size, uint minSize);
}