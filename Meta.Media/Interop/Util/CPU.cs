// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: CPU.cs
// Version: 20160508

using System;

namespace Meta.Media.Interop.Util
{
    [Flags]
    public enum CpuFlags
    {
        /// <summary>
        ///     standard MMX
        /// </summary>
        MMX = 0x0001,

        /// <summary>
        ///     SSE integer functions or AMD MMX ext
        /// </summary>
        MMXEXT = 0x0002,

        /// <summary>
        ///     SSE integer functions or AMD MMX ext
        /// </summary>
        MMX2 = 0x0002,

        /// <summary>
        ///     AMD 3DNOW
        /// </summary>
        _3DNOW = 0x0004,

        /// <summary>
        ///     SSE functions
        /// </summary>
        SSE = 0x0008,

        /// <summary>
        ///     PIV SSE2 functions
        /// </summary>
        SSE2 = 0x0010,

        /// <summary>
        ///     SSE2 supported, but usually not faster
        /// </summary>
        SSE2SLOW = 0x40000000,

        /// <summary>
        ///     AMD 3DNowExt
        /// </summary>
        _3DNOWEXT = 0x0020,

        /// <summary>
        ///     Prescott SSE3 functions
        /// </summary>
        SSE3 = 0x0040,

        /// <summary>
        ///     SSE3 supported, but usually not faster
        /// </summary>
        SSE3SLOW = 0x20000000,

        /// <summary>
        ///     Conroe SSSE3 functions
        /// </summary>
        SSSE3 = 0x0080,

        /// <summary>
        ///     Atom processor, some SSSE3 instructions are slower
        /// </summary>
        ATOM = 0x10000000,

        /// <summary>
        ///     Penryn SSE4.1 functions
        /// </summary>
        SSE4 = 0x0100,

        /// <summary>
        ///     Nehalem SSE4.2 functions
        /// </summary>
        SSE42 = 0x0200,

        /// <summary>
        ///     X functions: requires OS support even if YMM registers aren't used
        /// </summary>
        X = 0x4000,

        /// <summary>
        ///     X supported, but slow when using YMM registers (e.g. Bulldozer)
        /// </summary>
        XSLOW = 0x8000000,

        /// <summary>
        ///     Bulldozer XOP functions
        /// </summary>
        XOP = 0x0400,

        /// <summary>
        ///     Bulldozer FMA4 functions
        /// </summary>
        FMA4 = 0x0800,

        /// <summary>
        ///     supports cmov instruction
        /// </summary>
        CMOV = 0x1001000,

        /// <summary>
        ///     X2 functions: requires OS support even if YMM registers aren't used
        /// </summary>
        X2 = 0x8000,

        /// <summary>
        ///     Haswell FMA3 functions
        /// </summary>
        FMA3 = 0x10000,

        /// <summary>
        ///     Bit Manipulation Instruction Set 1
        /// </summary>
        BMI1 = 0x20000,

        /// <summary>
        ///     Bit Manipulation Instruction Set 2
        /// </summary>
        BMI2 = 0x40000,

        /// <summary>
        ///     standard
        /// </summary>
        ALTIVEC = 0x0001,

        /// <summary>
        ///     ISA 2.06
        /// </summary>
        VSX = 0x0002,

        /// <summary>
        ///     ISA 2.07
        /// </summary>
        POWER8 = 0x0004,

        ARMV5TE = (1 << 0),
        ARMV6 = (1 << 1),
        ARMV6T2 = (1 << 2),
        VFP = (1 << 3),
        VFPV3 = (1 << 4),
        NEON = (1 << 5),
        ARMV8 = (1 << 6),
        SETEND = (1 << 16)
    }

    /// <summary>
    /// Return the flags which specify extensions supported by the CPU. The returned value is affected by <see cref="ForceCpuFlags"/> if that was used before. So <see cref="GetCpuFlags"/> can easily be used in a application to detect the enabled cpu flags.
    /// </summary>
    /// <returns></returns>
    [UtilFunction("av_get_cpu_flags")]
    public delegate CpuFlags GetCpuFlags();

    /// <summary>
    /// Disables cpu detection and forces the specified flags. -1 is a special case that disables forcing of specific flags.
    /// </summary>
    /// <param name="flags"></param>
    [UtilFunction("av_force_cpu_flags")]
    public delegate void ForceCpuFlags(int flags);

    /// <summary>
    /// Parse CPU caps from a string and update the given _CPU_* flags based on that.
    /// </summary>
    /// <param name="flags"></param>
    /// <param name="str"></param>
    /// <returns>negative on error.</returns>
    [UtilFunction("av_parse_cpu_caps")]
    public unsafe delegate CpuFlags ParseCpuCaps(out CpuFlags flags, byte* str);

    /// <summary>
    /// the number of logical CPU cores present.
    /// </summary>
    /// <returns></returns>
    [UtilFunction("av_cpu_count")]
    public delegate int GetCpuCount();
}