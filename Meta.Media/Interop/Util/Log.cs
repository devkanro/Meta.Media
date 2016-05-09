// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: Log.cs
// Version: 20160509

using System;
using System.Runtime.InteropServices;

namespace Meta.Media.Interop.Util
{
    public enum ClassCategory
    {
        Na = 0,
        Input,
        Output,
        Muxer,
        Demuxer,
        Encoder,
        Decoder,
        Filter,
        BitstreamFilter,
        Swscaler,
        Swresampler,
        DeviceVideoOutput = 40,
        DeviceVideoInput,
        DeviceAudioOutput,
        DeviceAudioInput,
        DeviceOutput,
        DeviceInput,
    }

    public enum LogLevel
    {
        /// <summary>
        /// Print no output.
        /// </summary>
        Quiet = -8,
        /// <summary>
        /// Something went really wrong and we will crash now.
        /// </summary>
        Panic = 0,
        /// <summary>
        /// Something went wrong and recovery is not possible. For example, no header was found for a format which depends on headers or an illegal combination of parameters is used.
        /// </summary>
        Fatal = 8,
        /// <summary>
        /// Something went wrong and cannot losslessly be recovered. However, not all future data is affected.
        /// </summary>
        Error = 16,
        /// <summary>
        /// Something somehow does not look correct. This may or may not lead to problems. An example would be the use of '-vstrict -2'.
        /// </summary>
        Warning = 24,
        /// <summary>
        /// Standard information.
        /// </summary>
        Info = 32,
        /// <summary>
        /// Detailed information.
        /// </summary>
        Verbose = 40,
        /// <summary>
        /// Stuff which is only useful for libav* developers.
        /// </summary>
        Debug = 48,
        MaxOffset = Debug - Quiet,
        /// <summary>
        /// Extremely verbose debugging, useful for libav* development.
        /// </summary>
        Trace = 56
    }

    public enum LogFlags
    {
        /// <summary>
        /// Skip repeated messages, this requires the user app to use av_log() instead of (f)printf as the 2 would otherwise interfere and lead to "Last message repeated x times" messages below (f)printf messages with some bad luck.
        /// <para></para>
        /// Also to receive the last, "last repeated" line if any, the user app must call av_log(NULL, _LOG_QUIET, "%s", ""); at the end
        /// </summary>
        SkipRepeated = 1,
        /// <summary>
        /// Include the log severity in messages originating from codecs.
        /// <para></para>
        /// Results in messages such as:
        /// <para></para>
        /// [rawvideo @ 0xDEADBEEF] [error] encode did not produce valid pts
        /// </summary>
        PrintLevel = 2
    }

    public struct OptionRangesPtr
    {
        internal OptionRangesPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public static bool operator ==(OptionRangesPtr value1, OptionRangesPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(OptionRangesPtr value1, OptionRangesPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(OptionRangesPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is OptionRangesPtr))
            {
                return false;
            }

            return Equals((OptionRangesPtr)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(OptionRanges)";
        }
    }

    /// <summary>
    /// a function which returns the name of a context instance associated with the class.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public unsafe delegate byte* ClassGetItemNameCallback(void* context);

    public unsafe delegate void* ClassNextChildCallback(void* obj, void* prev);

    public delegate ClassPtr ClassNextClassChildCallback(ClassPtr prev);

    public unsafe delegate ClassCategory ClassGetClassCategoryCallback(void* context);

    public unsafe delegate int ClassQueryRange(ref OptionRangesPtr ranges, void* obj, byte* key, int flags);


    // TODO: Structure operation.
    public unsafe struct Class
    {
        /// <summary>
        /// The name of the class; usually it is the same name as the context structure type to which the Class is associated.
        /// </summary>
        private byte* _className;

        /// <summary>
        /// A pointer to a function which returns the name of a context instance ctx associated with the class.
        /// </summary>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        private ClassGetItemNameCallback _itemName;

        /// <summary>
        /// a pointer to the first option specified in the class if any or NULL
        /// </summary>
        /// <remarks>
        /// Seealso:
        /// av_set_default_options()
        /// </remarks>
        // TODO: Link to av_set_default_options().
        private OptionPtr _option;

        /// <summary>
        /// LIBUTIL_VERSION with which this structure was created. This is used to allow fields to be added without requiring major version bumps everywhere.
        /// </summary>
        private int _version;

        /// <summary>
        /// Offset in the structure where log_level_offset is stored. 0 means there is no such variable
        /// </summary>
        private int _logLevelOffsetOffset;

        /// <summary>
        /// Offset in the structure where a pointer to the parent context for logging is stored. For example a decoder could pass its CodecContext to eval as such a parent context, which an av_log() implementation could then leverage to display the parent context. The offset can be NULL.
        /// </summary>
        private int _parentLogContextOffset;

        /// <summary>
        /// Return next Options-enabled child or NULL
        /// </summary>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        private ClassNextChildCallback _childNext;

        /// <summary>
        /// Return an Class corresponding to the next potential Options-enabled child.
        /// </summary>
        /// <remarks>
        /// The difference between _childNext and this is that _childNext iterates over _already existing_ objects, while _childClassNext iterates over _all possible_ children.
        /// </remarks>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        private ClassNextClassChildCallback _childClassNext;

        /// <summary>
        /// Category used for visualization (like color) This is only set if the category is equal for all objects using this class. available since version (51 &lt;&lt; 16 | 56 &lt;&lt; 8 | 100)
        /// </summary>
        private ClassCategory _category;

        /// <summary>
        /// Callback to return the category. available since version (51 &lt;&lt; 16 | 59 &lt;&lt; 8 | 100)
        /// </summary>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        private ClassGetClassCategoryCallback _getCategory;

        /// <summary>
        /// Callback to return the supported/allowed ranges. available since version (52.12)
        /// </summary>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        private ClassQueryRange _queryRanges;
    }

    public struct ClassPtr
    {
        internal ClassPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public Class Struct => Marshal.PtrToStructure<Class>(Value);

        public static bool operator ==(ClassPtr value1, ClassPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(ClassPtr value1, ClassPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(ClassPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ClassPtr))
            {
                return false;
            }

            return Equals((ClassPtr)obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(Class)";
        }
    }

    /// <summary>
    /// Send the specified message to the log if the level is less than or equal to the current av_log_level. By default, all logging messages are sent to stderr. This behavior can be altered by setting a different logging callback function.
    /// </summary>
    /// <param name="avcl">A pointer to an arbitrary struct of which the first field is a pointer to an <see cref="Class"/> struct.</param>
    /// <param name="level">level The importance level of the message expressed using a @ref lavu_log_constants "Logging Constant".</param>
    /// <param name="fmt">fmt The format string (printf-compatible) that specifies how subsequent arguments are converted to output.</param>
    /// <param name="vl">The arguments referenced by the format string.</param>
    /// <remarks>
    /// Seealso:
    /// <seealso cref="LogSetCallback"/>
    /// </remarks>
    [UtilFunction("av_vlog")]
    public unsafe delegate void Log(void* avcl, LogLevel level, byte* fmt, byte* vl);

    /// <summary>
    /// Get the current log level
    /// </summary>
    /// <returns>Current log level</returns>
    [UtilFunction("av_log_get_level")]
    public delegate LogLevel GetLogLevel();

    /// <summary>
    /// Set the log level
    /// </summary>
    /// <param name="level">Logging level</param>
    [UtilFunction("av_log_set_level")]
    public delegate void SetLogLevel(LogLevel level);

    /// <summary>
    /// Set the logging callback
    /// </summary>
    /// <param name="callback">A logging function with a compatible signature.</param>
    /// <remarks>
    /// Note:
    /// The callback must be thread safe, even if the application does not use threads itself as some codecs are multithreaded.
    /// <para></para>
    /// Seealso:
    /// <seealso cref="LogDefaultCallback"/>
    /// </remarks>
    [UtilFunction("av_log_set_callback")]
    public delegate void LogSetCallback(LogDefaultCallback callback);

    /// <summary>
    /// Default logging callback
    /// <para></para>
    /// It prints the message to stderr, optionally colorizing it.
    /// </summary>
    /// <param name="avcl">A pointer to an arbitrary struct of which the first field is a pointer to an <see cref="Class"/> struct.</param>
    /// <param name="level">level The importance level of the message expressed using a @ref lavu_log_constants "Logging Constant".</param>
    /// <param name="fmt">fmt The format string (printf-compatible) that specifies how subsequent arguments are converted to output.</param>
    /// <param name="vl">The arguments referenced by the format string.</param>
    [UtilFunction("av_log_default_callback")]
    public unsafe delegate void LogDefaultCallback(void* avcl, LogLevel level, byte* fmt, byte* vl);

    /// <summary>
    /// Return the context name
    /// </summary>
    /// <param name="context">The Class context</param>
    /// <returns>The Class class_name</returns>
    [UtilFunction("av_default_item_name")]
    public unsafe delegate byte* GetDefaultItemName(void* context);

    [UtilFunction("av_default_get_category")]
    public unsafe delegate ClassCategory GetDefaultClassCategory(void* ptr);

    /// <summary>
    /// Format a line of log the same way as the default callback.
    /// </summary>
    /// <param name="avcl"></param>
    /// <param name="level"></param>
    /// <param name="fmt"></param>
    /// <param name="vl"></param>
    /// <param name="line">buffer to receive the formatted line</param>
    /// <param name="lineSize">size of the buffer</param>
    /// <param name="printPrefix">used to store whether the prefix must be printed; must point to a persistent integer initially set to 1</param>
    [UtilFunction("av_log_format_line")]
    public unsafe delegate void LogFormatLine(void* avcl, LogLevel level, byte* fmt, byte* vl, char* line, int lineSize, int* printPrefix);

    [UtilFunction("av_log_set_flags")]
    public delegate void LogSetFlags(int arg);

    [UtilFunction("av_log_get_flags")]
    public delegate int LogGetFlags();
}