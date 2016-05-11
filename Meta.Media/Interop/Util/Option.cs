// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: Option.cs
// Version: 20160511

using System;
using System.Runtime.InteropServices;

namespace Meta.Media.Interop.Util
{
    public enum OptionType : uint
    {
        Flags,
        Int,
        Int64,
        Double,
        Float,
        String,
        Rational,

        /// <summary>
        ///     offset must point to a pointer immediately followed by an int for the length
        /// </summary>
        Binary,

        Dict,
        Const = 128,

        /// <summary>
        ///     offset must point to two consecutive integers
        /// </summary>
        ImageSize = (uint) ('S' | 'I' << 8 | 'Z' << 16 | 'E' << 24),

        PixelFmt = (uint) ('P' | 'F' << 8 | 'M' << 16 | 'T' << 24),
        SampleFmt = (uint) ('S' | 'F' << 8 | 'M' << 16 | 'T' << 24),

        /// <summary>
        ///     offset must point to Rational
        /// </summary>
        VideoRate = (uint) ('V' | 'R' << 8 | 'A' << 16 | 'T' << 24),

        Duration = (uint) ('D' | 'U' << 8 | 'R' << 16 | ' ' << 24),
        Color = (uint) ('C' | 'O' << 8 | 'L' << 16 | 'R' << 24),
        ChannelLayout = (uint) ('C' | 'H' << 8 | 'L' << 16 | 'A' << 24)
    }

    public enum OptionFlags
    {
        /// <summary>
        ///     a generic parameter which can be set by the user for muxing or encoding
        /// </summary>
        EncodingParam = 1,

        /// <summary>
        ///     a generic parameter which can be set by the user for demuxing or decoding
        /// </summary>
        DecodingParam = 2,

#if FF_API_OPT_TYPE_METADATA

        /// <summary>
        ///     some data extracted or inserted into the file like title, comment, ...
        /// </summary>
        Metadata = 4,

#endif
        AudioParam = 8,
        VideoParam = 16,
        SubtitleParam = 32,

        /// <summary>
        ///     The option is inteded for exporting values to the caller.
        /// </summary>
        Export = 64,

        /// <summary>
        ///     The option may not be set through the AVOptions API, only read.
        ///     This flag only makes sense when AV_OPT_FLAG_EXPORT is also set.
        /// </summary>
        Readonly = 128,

        /// <summary>
        ///     a generic parameter which can be set by the user for filtering
        /// </summary>
        FilteringParam = (1 << 16)
    }

    // TODO: Structure operation.
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public unsafe struct OptionValue
    {
        [FieldOffset(0)] private long _i64;

        [FieldOffset(0)] private double _dbl;

        [FieldOffset(0)] private byte* _str;

        [FieldOffset(0)] private Rational _q;
    }

    // TODO: Structure operation.
    public unsafe struct Option
    {
        private byte* _name;

        /// <summary>
        ///     short English help text
        /// </summary>
        private byte* _help;

        /// <summary>
        ///     The offset relative to the context structure where the option value is stored. It should be 0 for named ants.
        /// </summary>
        private int _offset;

        private OptionType _type;

        /// <summary>
        ///     the default value for scalar options
        /// </summary>
        private OptionValue _defaultValue;

        /// <summary>
        ///     minimum valid value for the option
        /// </summary>
        private double _min;

        /// <summary>
        ///     maximum valid value for the option
        /// </summary>
        private double _max;

        private OptionFlags _flags;

        /// <summary>
        ///     The logical unit to which the option belongs. Non-ant options and corresponding named ants share the same
        ///     unit. May be NULL.
        /// </summary>
        private byte* _unit;
    }

    public struct OptionPtr
    {
        internal OptionPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public Option Struct => Marshal.PtrToStructure<Option>(Value);

        public static bool operator ==(OptionPtr value1, OptionPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(OptionPtr value1, OptionPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(OptionPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is OptionPtr))
            {
                return false;
            }

            return Equals((OptionPtr) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(Option)";
        }
    }

    /// <summary>
    /// A single allowed range of values, or a single allowed value.
    /// </summary>
    public unsafe struct OptionRange
    {
        private byte* _str;
        /// <summary>
        /// Value range.
        /// <para></para>
        /// For string ranges this represents the min/max length.
        /// <para></para>
        /// For dimensions this represents the min/max pixel count or width/height in multi-component case.
        /// </summary>
        private double _valueMin;
        /// <summary>
        /// Value range.
        /// <para></para>
        /// For string ranges this represents the min/max length.
        /// <para></para>
        /// For dimensions this represents the min/max pixel count or width/height in multi-component case.
        /// </summary>
        private double _valueMax;
        /// <summary>
        /// Value's component range.
        /// <para></para>
        /// For string this represents the unicode range for chars, 0-127 limits to ASCII.
        /// </summary>
        private double _componentMin;
        /// <summary>
        /// Value's component range.
        /// <para></para>
        /// For string this represents the unicode range for chars, 0-127 limits to ASCII.
        /// </summary>
        private double _componentMax;
        /// <summary>
        /// Range flag.
        /// If set to 1 the struct encodes a range, if set to 0 a single value.
        /// </summary>
        private int _isRange;
    }

    /// <summary>
    /// List of <see cref="OptionRange"/> structs.
    /// </summary>
    public unsafe struct OptionRanges
    {
        //
        //Array of option ranges.
        //
        //Most of option types use just one component.
        //Following describes multi-component option types:
        //
        //AV_OPT_TYPE_IMAGE_SIZE:
        //component index 0: range of pixel count (width * height).
        //component index 1: range of width.
        //component index 2: range of height.
        //
        //@note To obtain multi-component version of this structure, user must
        //      provide AV_OPT_MULTI_COMPONENT_RANGE to av_opt_query_ranges or
        //      av_opt_query_ranges_default function.
        //
        //Multi-component range can be read as in following example:
        //
        //@code
        //int range_index, component_index;
        //AVOptionRanges *ranges;
        //AVOptionRange *range[3]; //may require more than 3 in the future.
        //av_opt_query_ranges(&ranges, obj, key, AV_OPT_MULTI_COMPONENT_RANGE);
        //for (range_index = 0; range_index < ranges->nb_ranges; range_index++) {
        //    for (component_index = 0; component_index < ranges->nb_components; component_index++)
        //        range[component_index] = ranges->range[ranges->nb_ranges * component_index + range_index];
        //    //do something with range here.
        //}
        //av_opt_freep_ranges(&ranges);
        //@endcode
        //
        /// <summary>
        /// Array of option ranges.
        /// <para></para>
        /// Most of option types use just one component.
        /// Following describes multi-component option types:
        /// <para></para>
        /// AV_OPT_TYPE_IMAGE_SIZE:
        /// <para></para>
        /// component index 0: range of pixel count (width * height).
        /// <para></para>
        /// component index 1: range of width.
        /// <para></para>
        /// component index 2: range of height.
        /// </summary>
        /// <remarks>
        /// Note:
        /// To obtain multi-component version of this structure, user must provide AV_OPT_MULTI_COMPONENT_RANGE to av_opt_query_ranges or av_opt_query_ranges_default function.
        /// <para></para>
        /// Multi-component range can be read as in following example:
        /// <code>
        /// int range_index, component_index;
        /// AVOptionRanges *ranges;
        /// AVOptionRange *range[3]; //may require more than 3 in the future.
        /// av_opt_query_ranges(&amp;ranges, obj, key, AV_OPT_MULTI_COMPONENT_RANGE);
        /// for (range_index = 0; range_index &lt; ranges->nb_ranges; range_index++) {
        ///     for (component_index = 0; component_index &lt; ranges->nb_components; component_index++)
        ///         range[component_index] = ranges->range[ranges->nb_ranges * component_index + range_index];
        ///     //do something with range here.
        /// }
        /// av_opt_freep_ranges(&amp;ranges);
        /// </code>
        /// </remarks>
        // TODO: Link to av_opt_query_ranges().
        // TODO: Link to av_opt_query_ranges_default().
        OptionRange** _range;
        /// <summary>
        /// Number of ranges per component.
        /// </summary>
        int _rangeCount;
        /// <summary>
        /// Number of componentes.
        /// </summary>
        int _componentCount;
    }

#if FF_API_OLD_AVOPTIONS
    [Obsolete]
    [UtilFunction("av_set_string3")]
    public unsafe delegate int SetString3(void* obj,  char* name,  char* val, int alloc, out OptionPtr option);
    [Obsolete]
    [UtilFunction("av_set_double")]
    public unsafe delegate Option* SetDouble(void* obj,  char* name, double n);
    [Obsolete]
    [UtilFunction("av_set_q")]
    public unsafe delegate Option* SetQ(void* obj,  char* name, Rational n);
    [Obsolete]
    [UtilFunction("av_set_int")]
    public unsafe delegate Option* SetInt(void* obj,  char* name, long n);

    [UtilFunction("av_get_double")]
    public unsafe delegate double GetDouble(void* obj, char* name, out OptionPtr option);
    [UtilFunction("av_get_q")]
    public unsafe delegate Rational GetQ(void* obj,  char* name,  out OptionPtr option);
    [UtilFunction("av_get_int")]
    public unsafe delegate long GetInt(void* obj,  char* name,  out OptionPtr option);

    [Obsolete]
    [UtilFunction("av_get_string")]
    public unsafe delegate char* GetString(void* obj,  char* name,  out OptionPtr option, char* buf, int bufLen);
    [Obsolete]
    [UtilFunction("av_next_option")]
    public unsafe delegate OptionPtr NextOption(void* obj,  OptionPtr last);
#endif
    
    /// <summary>
    /// Show the obj options.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="logObj">log context to use for showing the options</param>
    /// <param name="reqFlags">requested flags for the options to show. Show only the options for which it is opt->flags & req_flags.</param>
    /// <param name="rejFlags">rejected flags for the options to show. Show only the options for which it is !(opt->flags & req_flags).</param>
    /// <returns></returns>
    [UtilFunction("av_opt_show2")]
    public unsafe delegate int OptionShow(void* obj, void* logObj, int reqFlags, int rejFlags);
    
    /// <summary>
    /// Set the values of all AVOption fields to their default values.
    /// </summary>
    /// <param name="s">an AVOption-enabled struct (its first member must be a pointer to AVClass)</param>
    [UtilFunction("av_opt_set_defaults")]
    public unsafe delegate void OptionSetDefaults(void* s);
    
#if FF_API_OLD_AVOPTIONS
    [Obsolete]
    [UtilFunction("av_opt_set_defaults2")]
    public unsafe delegate void OptionSetDefaults2(void* s, int mask, int flags);
#endif

    // TODO: Comment.
    [UtilFunction("av_set_options_string")]
    public unsafe delegate int SetOptionsString(void* ctx, byte* opts, byte* keyValSep, byte* pairsSep);
    [UtilFunction("av_opt_set_from_string")]
    public unsafe delegate int OptionSetFromString(void* ctx, byte* opts, byte** shorthand, byte* keyValSep, byte* pairsSep);
    [UtilFunction("av_opt_free")]
    public unsafe delegate void OptionFree(void* obj);
    [UtilFunction("av_opt_flag_is_set")]
    public unsafe delegate int OptionFlagIsSet(void* obj, byte* fieldName, byte* flagName);
    [UtilFunction("av_opt_set_dict")]
    public unsafe delegate int OptionSetDict(void* obj, DictionaryPtr* options);
    [UtilFunction("av_opt_set_dict2")]
    public unsafe delegate int OptionSetDict2(void* obj, DictionaryPtr* options, int searchFlags);
    [UtilFunction("av_opt_get_key_value")]
    public unsafe delegate int OptionGetKeyValue(byte** ropts, byte* keyValSep, byte* pairsSep, uint flags, byte** rkey, byte** rval);
    [UtilFunction("av_opt_eval_flags")]
    public unsafe delegate int OptionEvalFlags(void* obj, Option* o, byte* val, int* flagsOut);
    [UtilFunction("av_opt_eval_int")]
    public unsafe delegate int OptionEvalInt(void* obj, Option* o, byte* val, int* intOut);
    [UtilFunction("av_opt_eval_int64")]
    public unsafe delegate int OptionEvalInt64(void* obj, Option* o, byte* val, long* int64Out);
    [UtilFunction("av_opt_eval_float")]
    public unsafe delegate int OptionEvalFloat(void* obj, Option* o, byte* val, float* floatOut);
    [UtilFunction("av_opt_eval_double")]
    public unsafe delegate int OptionEvalDouble(void* obj, Option* o, byte* val, double* doubleOut);
    [UtilFunction("av_opt_eval_q")]
    public unsafe delegate int OptionEvalQ(void* obj,  Option* o,  byte* val, Rational* qOut);
    [UtilFunction("av_opt_find")]
    public unsafe delegate  Option* OptionFind(void* obj, byte* name, byte* unit, int optFlags, int searchFlags);
    [UtilFunction("av_opt_find2")]
    public unsafe delegate  Option* OptionFind2(void* obj, byte* name, byte* unit, int optFlags, int searchFlags, void** targetObj);
    [UtilFunction("av_opt_next")]
    public unsafe delegate  Option* OptionNext(void* obj, Option* prev);
    [UtilFunction("av_opt_child_next")]
    public unsafe delegate void* OptionChildNext(void* obj, void* prev);
    [UtilFunction("av_opt_child_class_next")]
    public delegate  ClassPtr OptionChildClassNext(ClassPtr parent, ClassPtr prev);
    [UtilFunction("av_opt_set")]
    public unsafe delegate int OptionSet(void* obj, byte* name, byte* val, int searchFlags);
    [UtilFunction("av_opt_set_int")]
    public unsafe delegate int OptionSetInt(void* obj, byte* name, long val, int searchFlags);
    [UtilFunction("av_opt_set_double")]
    public unsafe delegate int OptionSetDouble(void* obj, byte* name, double val, int searchFlags);
    [UtilFunction("av_opt_set_q")]
    public unsafe delegate int OptionSetQ(void* obj,  byte* name, Rational val, int searchFlags);
    [UtilFunction("av_opt_set_bin")]
    public unsafe delegate int OptionSetBin(void* obj,  byte* name,  byte* val, int size, int searchFlags);
    [UtilFunction("av_opt_set_image_size")]
    public unsafe delegate int OptionSetImageSize(void* obj,  byte* name, int w, int h, int searchFlags);
    [UtilFunction("av_opt_set_pixel_fmt")]
    public unsafe delegate int OptionSetPixelFmt(void* obj,  byte* name, PixelFormat fmt, int searchFlags);
    [UtilFunction("av_opt_set_sample_fmt")]
    public unsafe delegate int OptionSetSampleFmt(void* obj,  byte* name, SampleFormat fmt, int searchFlags);
    [UtilFunction("av_opt_set_video_rate")]
    public unsafe delegate int OptionSetVideoRate(void* obj,  byte* name, Rational val, int searchFlags);
    [UtilFunction("av_opt_set_channel_layout")]
    public unsafe delegate int OptionSetChannelLayout(void* obj, byte* name, long chLayout, int searchFlags);
    [UtilFunction("av_opt_get")]
    public unsafe delegate int OptionGet(void* obj,  byte* name, int searchFlags, byte** outVal);
    [UtilFunction("av_opt_get_int")]
    public unsafe delegate int OptionGetInt(void* obj,  byte* name, int searchFlags, long* outVal);
    [UtilFunction("av_opt_get_double")]
    public unsafe delegate int OptionGetDouble(void* obj,  byte* name, int searchFlags, double* outVal);
    [UtilFunction("av_opt_get_q")]
    public unsafe delegate int OptionGetQ(void* obj,  byte* name, int searchFlags, Rational* outVal);
    [UtilFunction("av_opt_get_image_size")]
    public unsafe delegate int OptionGetImageSize(void* obj,  byte* name, int searchFlags, int* wOut, int* hOut);
    [UtilFunction("av_opt_get_pixel_fmt")]
    public unsafe delegate int OptionGetPixelFmt(void* obj,  byte* name, int searchFlags, PixelFormat * outFmt);
    [UtilFunction("av_opt_get_sample_fmt")]
    public unsafe delegate int OptionGetSampleFmt(void* obj,  byte* name, int searchFlags, SampleFormat * outFmt);
    [UtilFunction("av_opt_get_video_rate")]
    public unsafe delegate int OptionGetVideoRate(void* obj,  byte* name, int searchFlags, Rational* outVal);
    [UtilFunction("av_opt_get_channel_layout")]
    public unsafe delegate int OptionGetChannelLayout(void* obj,  byte* name, int searchFlags, long* chLayout);
    [UtilFunction("av_opt_get_dict_val")]
    public unsafe delegate int OptionGetDictVal(void* obj,  byte* name, int searchFlags, DictionaryPtr* outVal);
    [UtilFunction("av_opt_ptr")]
    public unsafe delegate void* option_ptr( ClassPtr avclass, void* obj,  byte* name);
    [UtilFunction("av_opt_freep_ranges")]
    public unsafe delegate void OptionFreepRanges(OptionRanges** ranges);
    [UtilFunction("av_opt_query_ranges")]
    public unsafe delegate int OptionQueryRanges(OptionRanges** ranges, void* obj, byte* key, int flags);
    [UtilFunction("av_opt_copy")]
    public unsafe delegate int OptionCopy(void* dest, void* src);
    [UtilFunction("av_opt_query_ranges_default")]
    public unsafe delegate int OptionQueryRangesDefault(OptionRanges** ranges, void* obj, byte* key, int flags);
    [UtilFunction("av_opt_is_set_to_default")]
    public unsafe delegate int OptionIsSetToDefault(void* obj,  Option* o);
    [UtilFunction("av_opt_is_set_to_default_by_name")]
    public unsafe delegate int OptionIsSetToDefaultByName(void* obj, byte* name, int searchFlags);
    [UtilFunction("av_opt_serialize")]
    public unsafe delegate int OptionSerialize(void* obj, int optFlags, int flags, byte** buffer,  byte keyValSep,  byte pairsSep);
}