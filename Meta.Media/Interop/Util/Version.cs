// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: Version.cs
// Version: 20160509

namespace Meta.Media.Interop.Util
{
    /// <summary>
    /// This class define some version macros for FFMPEG.
    /// You can check those macros here, but we don't use them in C# code.
    /// We have add macros for v54.27.100 in this project.
    /// But if you want use Meta.Media in other version FFMPEG, you should check those macros.
    /// </summary>
    public static class UtilVersion
    {
        public static int LIBUTIL_VERSION_MAJOR => 54;
        public static int LIBUTIL_VERSION_MINOR => 27;
        public static int LIBUTIL_VERSION_MICRO => 100;
        public static int LIBUTIL_VERSION_INT => _VERSION_INT(LIBUTIL_VERSION_MAJOR, LIBUTIL_VERSION_MINOR, LIBUTIL_VERSION_MICRO);
        public static string LIBUTIL_VERSION => _VERSION(LIBUTIL_VERSION_MAJOR, LIBUTIL_VERSION_MINOR, LIBUTIL_VERSION_MICRO);
        public static int LIBUTIL_BUILD => LIBUTIL_VERSION_INT;
        public static string LIBUTIL_IDENT => "Lavu" + LIBUTIL_VERSION;
        public static bool FF_API_OLD_OPTIONS => LIBUTIL_VERSION_MAJOR < 55;
        public static bool FF_API_PIX_FMT => LIBUTIL_VERSION_MAJOR < 55;
        public static bool FF_API_CONTEXT_SIZE => LIBUTIL_VERSION_MAJOR < 55;
        public static bool FF_API_PIX_FMT_DESC => LIBUTIL_VERSION_MAJOR < 55;
        public static bool FF_API__REVERSE => LIBUTIL_VERSION_MAJOR < 55;
        public static bool FF_API_AUDIOCONVERT => LIBUTIL_VERSION_MAJOR < 55;
        public static bool FF_API_CPU_FLAG_MMX2 => LIBUTIL_VERSION_MAJOR < 55;
        public static bool FF_API_LLS_PRIVATE => LIBUTIL_VERSION_MAJOR < 55;
        public static bool FF_API_FRAME_LC => LIBUTIL_VERSION_MAJOR < 55;
        public static bool FF_API_VDPAU => LIBUTIL_VERSION_MAJOR < 55;
        public static bool FF_API_GET_CHANNEL_LAYOUT_COMPAT => LIBUTIL_VERSION_MAJOR < 55;
        public static bool FF_API_XVMC => LIBUTIL_VERSION_MAJOR < 55;
        public static bool FF_API_OPT_TYPE_METADATA => LIBUTIL_VERSION_MAJOR < 55;
        public static bool FF_API_DLOG => LIBUTIL_VERSION_MAJOR < 55;


        public static int _VERSION_INT(int a, int b, int c)
        {
            return a << 16 | b << 8 | c;
        }

        public static string _VERSION(int a, int b, int c)
        {
            return $"{a}.{b}.{c}";
        }
    }
}