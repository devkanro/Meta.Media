// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: Util.cs
// Version: 20160508

namespace Meta.Media.Interop.Util
{
    public enum MediaType
    {
        /// <summary>
        /// Usually treated as <see cref="DATA"/>
        /// </summary>
        UNKNOWN = -1,

        VIDEO,
        AUDIO,

        /// <summary>
        /// Opaque data information usually continuous
        /// </summary>
        DATA,

        SUBTITLE,

        /// <summary>
        /// Opaque data information usually sparse
        /// </summary>
        ATTACHMENT,
    };

    /// <summary>
    /// Picture types, pixel formats and basic image planes manipulation.
    /// </summary>
    public enum PictureType
    {
        /// <summary>
        /// Undefined
        /// </summary>
        NONE = 0,

        /// <summary>
        /// Intra
        /// </summary>
        I,

        /// <summary>
        /// Predicted
        /// </summary>
        P,

        /// <summary>
        /// Bi-dir predicted
        /// </summary>
        B,

        /// <summary>
        /// S(GMC)-VOP MPEG4
        /// </summary>
        S,

        /// <summary>
        /// Switching Intra
        /// </summary>
        SI,

        /// <summary>
        /// Switching Predicted
        /// </summary>
        SP,

        /// <summary>
        /// BI type
        /// </summary>
        BI,
    }

    /// <summary>
    /// Return the LIBUTIL_VERSION_INT constant.
    /// </summary>
    /// <returns></returns>
    [UtilFunction("avutil_version")]
    public delegate uint GetUtilVersion();

    /// <summary>
    /// Return the libavutil build-time configuration.
    /// </summary>
    /// <returns></returns>
    [UtilFunction("avutil_configuration")]
    public unsafe delegate byte* GetUtilConfiguration();

    /// <summary>
    /// Return the libavutil license.
    /// </summary>
    /// <returns></returns>
    [UtilFunction("avutil_license")]
    public unsafe delegate byte* GetUtilLicense();

    /// <summary>
    /// Return a string describing the mediaType enum, NULL if mediaType is unknown.
    /// </summary>
    /// <param name="mediaType"></param>
    /// <returns></returns>
    [UtilFunction("av_get_media_type_string")]
    public unsafe delegate byte* GetMediaTypeString(MediaType mediaType);

    /// <summary>
    /// Return a single letter to describe the given picture type.
    /// </summary>
    /// <param name="pictureType">the picture type @return a single character representing the picture type, '?' if pict_type is unknown</param>
    /// <returns></returns>
    [UtilFunction("av_get_picture_type_char")]
    public delegate byte GetPictureTypeChar(PictureType pictureType);
}