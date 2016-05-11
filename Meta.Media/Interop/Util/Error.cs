// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: Error.cs
// Version: 20160508

namespace Meta.Media.Interop.Util
{
    public enum ErrorCode
    {
        /// <summary>
        /// Operation not permitted 
        /// </summary>
        PERM = -1,
        /// <summary>
        /// No such file or directory 
        /// </summary>
        NOENT = -2,
        /// <summary>
        /// No such process 
        /// </summary>
        SRCH = -3,
        /// <summary>
        /// Interrupted system call 
        /// </summary>
        INTR = -4,
        /// <summary>
        ///  I/O error 
        /// </summary>
        IO = -5,
        /// <summary>
        /// No such device or address 
        /// </summary>
        NXIO = -6,
        /// <summary>
        /// Arg list too long 
        /// </summary>
        TOOBIG = -7,
        /// <summary>
        /// Exec format error 
        /// </summary>
        NOEXEC = -8,
        /// <summary>
        /// Bad file number 
        /// </summary>
        BADF = -9,
        /// <summary>
        /// No child processes 
        /// </summary>
        CHILD = -10,
        /// <summary>
        /// No child processes 
        /// </summary>
        AGAIN = -11,
        /// <summary>
        /// No child processes 
        /// </summary>
        NOMEM = -12,
        /// <summary>
        /// No child processes 
        /// </summary>
        ACCES = -13,
        /// <summary>
        /// No child processes 
        /// </summary>
        FAULT = -14,
        /// <summary>
        /// Device or resource busy 
        /// </summary>
        BUSY = -16,
        /// <summary>
        /// Device or resource busy 
        /// </summary>
        EXIST = -17,
        /// <summary>
        /// Cross-device link 
        /// </summary>
        XDEV = -18,
        /// <summary>
        /// No such device 
        /// </summary>
        NODEV = -19,
        /// <summary>
        /// Not a directory 
        /// </summary>
        NOTDIR = -20,
        /// <summary>
        /// Is a directory 
        /// </summary>
        ISDIR = -21,
        /// <summary>
        /// File table overflow 
        /// </summary>
        NFILE = -23,
        /// <summary>
        /// Too many open files 
        /// </summary>
        MFILE = -24,
        /// <summary>
        /// Not a typewriter 
        /// </summary>
        NOTTY = -25,
        /// <summary>
        /// File too large 
        /// </summary>
        FBIG = -27,
        /// <summary>
        /// No space left on device 
        /// </summary>
        NOSPC = -28,
        /// <summary>
        ///  Illegal seek 
        /// </summary>
        SPIPE = -29,
        /// <summary>
        /// Read-only file system 
        /// </summary>
        ROFS = -30,
        /// <summary>
        /// Too many links 
        /// </summary>
        MLINK = -31,
        /// <summary>
        /// Broken pipe 
        /// </summary>
        PIPE = -32,
        /// <summary>
        /// Math argument out of domain of func 
        /// </summary>
        DOM = -33,
        /// <summary>
        /// Math result not representable 
        /// </summary>
        DEADLK = -36,
        /// <summary>
        /// File name too long 
        /// </summary>
        NAMETOOLONG = -38,
        /// <summary>
        /// No record locks available 
        /// </summary>
        NOLCK = -39,
        /// <summary>
        /// Function not implemented 
        /// </summary>
        NOSYS = -40,
        /// <summary>
        /// Directory not empty 
        /// </summary>
        NOTEMPTY = -41,
        /// <summary>
        /// Invalid argument 
        /// </summary>
        INVAL = 22,
        /// <summary>
        /// Math result not representable 
        /// </summary>
        RANGE = 34,
        /// <summary>
        /// 
        /// </summary>
        ILSEQ = 42,
        STRUNCATE = 80,




        /// <summary>
        /// Bitstream filter not found
        /// </summary>
        BsfNotFound = -(int)(0xF8 | 'B' << 8 | 'S' << 16 | (uint)('F' << 24)),
        /// <summary>
        /// Internal bug
        /// <seealso cref="Bug2"/>
        /// </summary>
        Bug = -(int)('B' | 'U' << 8 | 'G' << 16 | (uint)('!' << 24)),
        /// <summary>
        /// Buffer too small
        /// </summary>
        BufferTooSmall = -(int)('B' | 'U' << 8 | 'F' << 16 | (uint)('S' << 24)),
        /// <summary>
        /// Decoder not found
        /// </summary>
        DecoderNotFound = -(int)(0xF8 | 'D' << 8 | 'E' << 16 | (uint)('C' << 24)),
        /// <summary>
        /// Demuxer not found
        /// </summary>
        DemuxerNotFound = -(int)(0xF8 | 'D' << 8 | 'E' << 16 | (uint)('M' << 24)),
        /// <summary>
        /// Encoder not found
        /// </summary>
        EncoderNotFound = -(int)(0xF8 | 'E' << 8 | 'N' << 16 | (uint)('C' << 24)),
        /// <summary>
        /// End of file
        /// </summary>
        Eof = -(int)('E' | 'O' << 8 | 'F' << 16 | (uint)(' ' << 24)),
        /// <summary>
        /// Immediate exit was requested; the called function should not be restarted
        /// </summary>
        Exit = -(int)('E' | 'X' << 8 | 'I' << 16 | (uint)('T' << 24)),
        /// <summary>
        /// Generic error in an external library
        /// </summary>
        External = -(int)('E' | 'X' << 8 | 'T' << 16 | (uint)(' ' << 24)),
        /// <summary>
        /// Filter not found
        /// </summary>
        FilterNotFound = -(int)(0xF8 | 'F' << 8 | 'I' << 16 | (uint)('L' << 24)),
        /// <summary>
        /// Invalid data found when processing input
        /// </summary>
        Invaliddata = -(int)('I' | 'N' << 8 | 'D' << 16 | (uint)('A' << 24)),
        /// <summary>
        /// Muxer not found
        /// </summary>
        MuxerNotFound = -(int)(0xF8 | 'M' << 8 | 'U' << 16 | (uint)('X' << 24)),
        /// <summary>
        /// Option not found
        /// </summary>
        OptionNotFound = -(int)(0xF8 | 'O' << 8 | 'P' << 16 | (uint)('T' << 24)),
        /// <summary>
        /// Not yet implemented in FFmpeg, patches welcome
        /// </summary>
        PatchWelcome = -(int)('P' | 'A' << 8 | 'W' << 16 | (uint)('E' << 24)),
        /// <summary>
        /// Protocol not found
        /// </summary>
        ProtocolNotFound = -(int)(0xF8 | 'P' << 8 | 'R' << 16 | (uint)('O' << 24)),
        /// <summary>
        /// Stream not found
        /// </summary>
        StreamNotFound = -(int)(0xF8 | 'S' << 8 | 'T' << 16 | (uint)('R' << 24)),
        Bug2 = -(int)('B' | 'U' << 8 | 'G' << 16 | (uint)(' ' << 24)),
        /// <summary>
        /// Unknown error, typically from an external library
        /// </summary>
        Unknown = -(int)('U' | 'N' << 8 | 'K' << 16 | (uint)('N' << 24)),
        /// <summary>
        /// 
        /// </summary>
        HttpBadRequest = -(int)(0xF8 | '4' << 8 | '0' << 16 | (uint)('0' << 24)),
        HttpUnauthorized = -(int)(0xF8 | '4' << 8 | '0' << 16 | (uint)('1' << 24)),
        HttpForbidden = -(int)(0xF8 | '4' << 8 | '0' << 16 | (uint)('3' << 24)),
        HttpNotFound = -(int)(0xF8 | '4' << 8 | '0' << 16 | (uint)('4' << 24)),
        HttpOther_4Xx = -(int)(0xF8 | '4' << 8 | 'X' << 16 | (uint)('X' << 24)),
        HttpServerError = -(int)(0xF8 | '5' << 8 | 'X' << 16 | (uint)('X' << 24)),
        /// <summary>
        /// Requested feature is flagged experimental. Set strict_std_compliance if you really want to use it.
        /// </summary>
        Experimental = (-0x2bb2afa8),
        /// <summary>
        /// Input changed between calls. Reconfiguration is required. (can be OR-ed with ERROR_OUTPUT_CHANGED)
        /// </summary>
        InputChanged = (-0x636e6701),
        /// <summary>
        /// Output changed between calls. Reconfiguration is required. (can be OR-ed with ERROR_INPUT_CHANGED)
        /// </summary>
        OutputChanged = (-0x636e6702)
    }

    /// <summary>
    /// Put a description of the ERROR code err in errbuf.
    /// <para></para>
    /// In case of failure the global variable errno is set to indicate the error. Even in case of failure av_strerror() will print a generic error message indicating the errnum provided to errbuf.
    /// </summary>
    /// <param name="err">error code to describe</param>
    /// <param name="errbuf">buffer to which description is written</param>
    /// <param name="errbufSize">the size in bytes of errbuf</param>
    /// <returns>0 on success, a negative value if a description for err cannot be found</returns>
    [UtilFunction("av_strerror")]
    public unsafe delegate int StrError(ErrorCode err, byte* errbuf, uint errbufSize);
}