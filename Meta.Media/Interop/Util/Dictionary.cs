// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: Dictionary.cs
// Version: 20160508

using System;
using System.Runtime.InteropServices;

namespace Meta.Media.Interop.Util
{
    public enum DictionaryFlags
    {
        /// <summary>
        ///     Only get an entry with exact-case key match. Only relevant in av_dict_get().
        /// </summary>
        MatchCase = 1,

        /// <summary>
        ///     Return first entry in a dictionary whose first part corresponds to the search key, ignoring the suffix of the found
        ///     key string. Only relevant in av_dict_get().
        /// </summary>
        IgnoreSuffix = 2,

        /// <summary>
        ///     Take ownership of a key that's been allocated with av_malloc() or another memory allocation function.
        /// </summary>
        DontStrdupKey = 4,

        /// <summary>
        ///     Take ownership of a value that's been allocated with av_malloc() or another memory allocation function.
        /// </summary>
        DontStrdupVal = 8,

        /// <summary>
        ///     Don't overwrite existing entries.
        /// </summary>
        DontOverwrite = 16,

        /// <summary>
        ///     If the entry already exists, append to it.  Note that no delimiter is added, the strings are simply concatenated.
        /// </summary>
        Append = 32
    }

    // TODO: Structure operation.
    public unsafe struct DictionaryEntry
    {
        private byte* key;
        private byte* value;
    }

    public struct DictionaryEntryPtr
    {
        internal DictionaryEntryPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public DictionaryEntry Struct => Marshal.PtrToStructure<DictionaryEntry>(Value);

        public static bool operator ==(DictionaryEntryPtr value1, DictionaryEntryPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(DictionaryEntryPtr value1, DictionaryEntryPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(DictionaryEntryPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DictionaryEntryPtr))
            {
                return false;
            }

            return Equals((DictionaryEntryPtr) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(DictionaryEntry)";
        }
    }

    public struct DictionaryPtr
    {
        internal DictionaryPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public static bool operator ==(DictionaryPtr value1, DictionaryPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(DictionaryPtr value1, DictionaryPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(DictionaryPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DictionaryPtr))
            {
                return false;
            }

            return Equals((DictionaryPtr) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(Dictionary)";
        }
    }

    /// <summary>
    ///     Get a dictionary entry with matching key.
    ///     <para></para>
    ///     The returned entry key or value must not be changed, or it will cause undefined behavior.
    ///     <para></para>
    ///     To iterate through all the dictionary entries, you can set the matching key to the null string "" and set the
    ///     <see cref="DictionaryFlags.IgnoreSuffix" /> flag.
    /// </summary>
    /// <param name="dic"></param>
    /// <param name="key">matching key</param>
    /// <param name="prev">
    ///     Set to the previous matching element to find the next. If set to NULL the first matching element is
    ///     returned.
    /// </param>
    /// <param name="flags">a collection of <see cref="DictionaryFlags" /> flags controlling how the entry is retrieved</param>
    /// <returns>found entry or NULL in case no matching entry was found in the dictionary</returns>
    [UtilFunction("av_dict_get")]
    public unsafe delegate DictionaryEntryPtr DictionaryGet(
        DictionaryPtr dic, byte* key, DictionaryEntryPtr prev, DictionaryFlags flags);

    /// <summary>
    ///     Get number of entries in dictionary.
    /// </summary>
    /// <param name="dic">dictionary</param>
    /// <returns>number of entries in dictionary</returns>
    [UtilFunction("av_dict_count")]
    public delegate int DictionaryCount(DictionaryPtr dic);

    /// <summary>
    ///     Set the given entry in *pm, overwriting an existing entry.
    /// </summary>
    /// <param name="pm">
    ///     pointer to a pointer to a dictionary struct. If *pm is NULL a dictionary struct is allocated and put
    ///     in *pm.
    /// </param>
    /// <param name="key">entry key to add to *pm (will be av_strduped depending on flags)</param>
    /// <param name="value">
    ///     entry value to add to *pm (will be av_strduped depending on flags). Passing a NULL value will cause
    ///     an existing entry to be deleted.
    /// </param>
    /// <param name="flags"></param>
    /// <returns>>= 0 on success otherwise an error code &lt;0</returns>
    /// <remarks>
    ///     If <see cref="DictionaryFlags.DontStrdupKey" /> or <see cref="DictionaryFlags.DontStrdupVal" /> is set, key
    ///     will be freed on error.
    /// </remarks>
    [UtilFunction("av_dict_set")]
    public unsafe delegate int DictionarySet(DictionaryPtr* pm, byte* key, byte* value, DictionaryFlags flags);

    /// <summary>
    ///     Convenience wrapper for <see cref="DictionarySet" /> that converts the value to a string and stores it.
    /// </summary>
    /// <param name="pm"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="flags"></param>
    /// <returns></returns>
    /// <remarks>If <see cref="DictionaryFlags.DontStrdupKey" /> is set, key will be freed on error.</remarks>
    [UtilFunction("av_dict_set_int")]
    public unsafe delegate int DictionarySetInt(DictionaryPtr* pm, byte* key, long value, DictionaryFlags flags);

    /// <summary>
    ///     Parse the key/value pairs list and add the parsed entries to a dictionary.
    ///     <para></para>
    ///     In case of failure, all the successfully set entries are stored in *pm. You may need to manually free the created
    ///     dictionary.
    /// </summary>
    /// <param name="pm"></param>
    /// <param name="str"></param>
    /// <param name="keyValueSep">a 0-terminated list of characters used to separate key from value</param>
    /// <param name="pairsSep">a 0-terminated list of characters used to separate two pairs from each other</param>
    /// <param name="flags">
    ///     flags to use when adding to dictionary. <see cref="DictionaryFlags.DontStrdupKey" /> or
    ///     <see cref="DictionaryFlags.DontStrdupVal" /> are ignored since the key/value tokens will always be duplicated.
    /// </param>
    /// <returns>0 on success, negative ERROR code on failure</returns>
    [UtilFunction("av_dict_parse_string")]
    // TODO: Error Code.
    public unsafe delegate int DictionaryParseString(
        DictionaryPtr* pm, byte* str, byte* keyValueSep, byte* pairsSep, DictionaryFlags flags);

    /// <summary>
    ///     Copy entries from one Dictionary struct into another.
    /// </summary>
    /// <param name="dst">
    ///     pointer to a pointer to a Dictionary struct. If *dst is NULL, this function will allocate a struct
    ///     for you and put it in *dst
    /// </param>
    /// <param name="src">pointer to source Dictionary struct</param>
    /// <param name="flags">flags to use when setting entries in *dst</param>
    [UtilFunction("av_dict_copy")]
    public unsafe delegate void DictionaryCopy(DictionaryPtr* dst, DictionaryPtr src, DictionaryFlags flags);

    /// <summary>
    ///     Free all the memory allocated for an Dictionary struct and all keys and values.
    /// </summary>
    /// <param name="dst"></param>
    [UtilFunction("av_dict_free")]
    public unsafe delegate void DictionaryFree(DictionaryPtr* dst);

    /// <summary>
    ///     Get dictionary entries as a string.
    ///     <para></para>
    ///     Create a string containing dictionary's entries.
    ///     <para></para>
    ///     Such string may be passed back to <see cref="DictionaryParseString" />.
    /// </summary>
    /// <param name="dic">dictionary</param>
    /// <param name="buffer">
    ///     Pointer to buffer that will be allocated with string containg entries. Buffer must be freed by the
    ///     caller when is no longer needed.
    /// </param>
    /// <param name="keyValSep">character used to separate key from value</param>
    /// <param name="pairsSep">character used to separate two pairs from each other</param>
    /// <returns>>= 0 on success, negative on error</returns>
    /// <remarks>
    ///     Note: String is escaped with backslashes ('\').
    ///     <para></para>
    ///     Warning: Separators cannot be neither '\\' nor '\0'. They also cannot be the same.
    /// </remarks>
    [UtilFunction("av_dict_get_string")]
    public unsafe delegate int DictionaryGetString(DictionaryPtr dic, out byte* buffer, byte keyValSep, byte pairsSep);
}