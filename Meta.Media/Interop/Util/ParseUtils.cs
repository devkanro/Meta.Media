// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: ParseUtils.cs
// Version: 20160511

namespace Meta.Media.Interop.Util
{
    /// <summary>
    /// Parse str and store the parsed ratio in q.
    /// 
    /// Note that a ratio with infinite (1/0) or negative value is considered valid, so you should check on the returned value if you want to exclude those values.
    /// 
    /// The undefined value can be expressed using the "0:0" string.
    /// </summary>
    /// <param name="q">pointer to the AVRational which will contain the ratio</param>
    /// <param name="str">the string to parse: it has to be a string in the format num:den, a float number or an expression</param>
    /// <param name="max">the maximum allowed numerator and denominator</param>
    /// <param name="logOffset">log level offset which is applied to the log level of logCtx</param>
    /// <param name="logCtx">parent logging context</param>
    /// <returns>>= 0 on success, a negative error code otherwise</returns>
    [UtilFunction("av_parse_ratio")]
    // TODO: Error Code.
    public unsafe delegate int ParseRatio(ref Rational q, byte* str, int max, int logOffset, void* logCtx);

    /// <summary>
    /// Parse str and put in width_ptr and height_ptr the detected values.
    /// </summary>
    /// <param name="width">pointer to the variable which will contain the detected width value</param>
    /// <param name="height">pointer to the variable which will contain the detected height value</param>
    /// <param name="str">the string to parse: it has to be a string in the format width x height or a valid video size abbreviation.</param>
    /// <returns>>= 0 on success, a negative error code otherwise</returns>
    [UtilFunction("av_parse_video_size")]
    // TODO: Error Code.
    public unsafe delegate int ParseVideoSize(ref int width, ref int height, byte* str);

    /// <summary>
    /// Parse str and store the detected values in *rate.
    /// </summary>
    /// <param name="rate">rate pointer to the AVRational which will contain the detected frame rate</param>
    /// <param name="str">the string to parse: it has to be a string in the format rate_num / rate_den, a float number or a valid video rate abbreviation</param>
    /// <returns>>= 0 on success, a negative error code otherwise</returns>
    [UtilFunction("av_parse_video_rate")]
    // TODO: Error Code.
    public unsafe delegate int ParseVideoRate(ref Rational rate, byte* str);

    /// <summary>
    /// Put the RGBA values that correspond to str in rgbaColor.
    /// </summary>
    /// <param name="rgbaColor"></param>
    /// <param name="str">
    /// a string specifying a color. It can be the name of a color (case insensitive match) or a [0x|#]RRGGBB[AA] sequence, possibly followed by "@" and a string representing the alpha component.
    /// <para></para>
    /// The alpha component may be a string composed by "0x" followed by an hexadecimal number or a decimal number between 0.0 and 1.0, which represents the opacity value (0x00/0.0 means completely transparent, 0xff/1.0 completely opaque).
    /// <para></para>
    /// If the alpha component is not specified then 0xff is assumed. The string "random" will result in a random color.
    /// </param>
    /// <param name="slen">length of the initial part of str containing the color. It can be set to -1 if str is a null terminated string containing nothing else than the color.</param>
    /// <param name="logCtx"></param>
    /// <returns>>= 0 in case of success, a negative value in case of failure (for example if str cannot be parsed).</returns>
    [UtilFunction("av_parse_color")]
    // TODO: Error Code.
    public unsafe delegate int ParseColor(ref uint rgbaColor, byte* str, int slen, void* logCtx);

    /// <summary>
    /// Get the name of a color from the internal table of hard-coded named colors.
    /// <para></para>
    /// This function is meant to enumerate the color names recognized by <see cref="ParseColor"/>.
    /// </summary>
    /// <param name="colorIndex">index of the requested color, starting from 0</param>
    /// <param name="rgb">if not NULL, will point to a 3-elements array with the color value in RGB</param>
    /// <returns>the color name string or NULL if colorIndex is not in the array</returns>
    [UtilFunction("av_get_known_color_name")]
    // TODO: Error Code.
    public unsafe delegate int GetKnownColorName(int colorIndex, ref byte* rgb);


    /// <summary>
    /// Parse timestr and return in *time a corresponding number of microseconds.
    /// </summary>
    /// <param name="timeval">
    /// puts here the number of microseconds corresponding to the string in timestr. If the string represents a duration, it is the number of microseconds contained in the time interval.  If the string is a date, is the number of microseconds since 1st of January, 1970 up to the time of the parsed date.  If timestr cannot be successfully parsed, set *time to INT64_MIN.
    /// </param>
    /// <param name="timestr">
    /// a string representing a date or a duration.
    /// - If a date the syntax is:
    /// <code>
    /// [{YYYY-MM-DD|YYYYMMDD}[T|t| ]]{{HH:MM:SS[.m...]]]}|{HHMMSS[.m...]]]}}[Z]
    /// now
    /// </code>
    /// If the value is "now" it takes the current time.
    /// Time is local time unless Z is appended, in which case it is interpreted as UTC.
    /// If the year-month-day part is not specified it takes the current year-month-day.
    /// - If a duration the syntax is:
    /// <code>
    /// [-][HH:]MM:SS[.m...]
    /// [-]S+[.m...]
    /// </code>
    /// </param>
    /// <param name="duration">flag which tells how to interpret timestr, if not zero timestr is interpreted as a duration, otherwise as a date</param>
    /// <returns>>= 0 in case of success, a negative value corresponding to an ERROR code otherwise</returns>
    [UtilFunction("av_parse_time")]
    // TODO: Error Code.
    public unsafe delegate int ParseTime(ref long timeval, byte* timestr, int duration);

    /// <summary>
    /// Attempt to find a specific tag in a URL.
    /// <para></para>
    /// syntax: '?tag1=val1&amp;tag2=val2...'. Little URL decoding is done.
    /// </summary>
    /// <param name="arg"></param>
    /// <param name="argSize"></param>
    /// <param name="tag1"></param>
    /// <param name="info"></param>
    /// <returns>Return 1 if found.</returns>
    [UtilFunction("av_find_info_tag")]
    // TODO: Error Code.
    public unsafe delegate int FindInfoTag(byte* arg, int argSize, byte* tag1, byte* info);

    /// <summary>
    /// Simplified version of strptime
    /// <para></para>
    /// Parse the input string p according to the format string fmt and store its results in the structure dt.
    /// <para></para>
    /// This implementation supports only a subset of the formats supported by the standard strptime().
    /// <para></para>
    /// The supported input field descriptors are listed below.
    /// <para></para>
    /// - %H: the hour as a decimal number, using a 24-hour clock, in the
    ///   range '00' through '23'
    /// <para></para>
    /// - %J: hours as a decimal number, in the range '0' through INT_MAX
    /// <para></para>
    /// - %M: the minute as a decimal number, using a 24-hour clock, in the
    ///   range '00' through '59'
    /// <para></para>
    /// - %S: the second as a decimal number, using a 24-hour clock, in the
    ///   range '00' through '59'
    /// <para></para>
    /// - %Y: the year as a decimal number, using the Gregorian calendar
    /// <para></para>
    /// - %m: the month as a decimal number, in the range '1' through '12'
    /// <para></para>
    /// - %d: the day of the month as a decimal number, in the range '1'
    ///   through '31'
    /// <para></para>
    /// - %T: alias for '%H:%M:%S'
    /// <para></para>
    /// - %%: a literal '%'
    /// </summary>
    /// <param name="p"></param>
    /// <param name="fmt"></param>
    /// <param name="dt"></param>
    /// <returns>
    /// a pointer to the first character not processed in this function
    /// call. In case the input string contains more characters than
    /// required by the format string the return value points right after
    /// the last consumed input character. In case the whole input string
    /// is consumed the return value points to the null byte at the end of
    /// the string. On failure NULL is returned.
    /// </returns>
    [UtilFunction("av_small_strptime")]
    // TODO: Error Code.
    public unsafe delegate int SmallStrptime(byte* p, byte* fmt, TimePtr dt);
}