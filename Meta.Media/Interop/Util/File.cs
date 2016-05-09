// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: File.cs
// Version: 20160508

namespace Meta.Media.Interop.Util
{
    /// <summary>
    ///     Read the file with name filename, and put its content in a newly allocated buffer or map it with mmap() when
    ///     available.
    ///     In case of success set *bufPtr to the read or mmapped buffer, and *size to the size in bytes of the buffer in
    ///     *bufPtr.
    ///     The returned buffer must be released with <see cref="FileUnmap" />.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="bufPtr"></param>
    /// <param name="size"></param>
    /// <param name="logOffset">loglevel offset used for logging</param>
    /// <param name="logContext">context used for logging</param>
    /// <returns>
    ///     a non negative number in case of success, a negative value corresponding to an ERROR error code in case of
    ///     failure
    /// </returns>
    [UtilFunction("av_file_map")]
    // TODO: Error Code.
    public unsafe delegate int FileMap(byte* fileName, byte** bufPtr, uint* size, int logOffset, void* logContext);

    /// <summary>
    ///     Unmap or free the buffer bufPtr created by <see cref="FileMap" />.
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="size">size in bytes of bufPtr, must be the same as returned by <see cref="FileMap" /></param>
    /// <returns></returns>
    [UtilFunction("av_file_unmap")]
    public unsafe delegate void FileUnmap(byte* fileName, uint size);

    /// <summary>
    ///     Wrapper to work around the lack of mkstemp() on mingw.
    ///     Also, tries to create file in /tmp first, if possible.
    ///     *prefix can be a character constant; *fileName will be allocated internally.
    /// </summary>
    /// <param name="prefix"></param>
    /// <param name="fileName"></param>
    /// <param name="logOffset"></param>
    /// <param name="logContext"></param>
    /// <returns>
    ///     file descriptor of opened file (or negative value corresponding to an ERROR code on error) and opened file
    ///     name in **fileName.
    /// </returns>
    /// <remarks>
    ///     On very old libcs it is necessary to set a secure umask before calling this, <see cref="TempFile" /> can't call
    ///     umask itself as it is used in libraries and could interfere with the calling application.
    /// </remarks>
    [UtilFunction("av_file_unmap")]
    // TODO: Error Code.
    public unsafe delegate int TempFile(byte* prefix, byte** fileName, int logOffset, void* logContext);
}