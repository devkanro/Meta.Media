// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: Math.cs
// Version: 20160509

namespace Meta.Media.Interop.Util
{
    public enum Rounding
    {
        /// <summary>
        ///     Round toward zero.
        /// </summary>
        Zero = 0,

        /// <summary>
        ///     Round away from zero.
        /// </summary>
        Inf = 1,

        /// <summary>
        ///     Round toward -infinity.
        /// </summary>
        Down = 2,

        /// <summary>
        ///     Round toward +infinity.
        /// </summary>
        Up = 3,

        /// <summary>
        ///     Round to nearest and halfway cases away from zero.
        /// </summary>
        NearInf = 5,

        /// <summary>
        ///     Flag to pass INT64_MIN/MAX through instead of rescaling, this avoids special cases for AV_NOPTS_VALUE
        /// </summary>
        PassMinmax = 8192
    }

    /// <summary>
    ///     Return the greatest common divisor of a and b. If both a and b are 0 or either or both are &lt;0 then behavior is
    ///     undefined.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    [UtilFunction("av_gcd")]
    public delegate long GreatestCommonDivisor(long a, long b);

    /// <summary>
    ///     Rescale a 64-bit integer with rounding to nearest. A simple a*b/c isn't possible as it can overflow.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    [UtilFunction("av_rescale")]
    public delegate long Rescale(long a, long b, long c);

    /// <summary>
    ///     Rescale a 64-bit integer with specified rounding. A simple a*b/c isn't possible as it can overflow.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="c"></param>
    /// <param name="rounding"></param>
    /// <returns></returns>
    [UtilFunction("av_rescale_rnd")]
    public delegate long RescaleRounding(long a, long b, long c, Rounding rounding);

    /// <summary>
    ///     Rescale a 64-bit integer by 2 rational numbers.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="bq"></param>
    /// <param name="cq"></param>
    /// <returns></returns>
    [UtilFunction("av_rescale_q")]
    public delegate long RescaleQ(long a, Rational bq, Rational cq);

    /// <summary>
    ///     Rescale a 64-bit integer by 2 rational numbers with specified rounding.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="bq"></param>
    /// <param name="cq"></param>
    /// <param name="rounding"></param>
    /// <returns></returns>
    [UtilFunction("av_rescale_q_rnd")]
    public delegate long RescaleRoundingQ(long a, Rational bq, Rational cq, Rounding rounding);

    /// <summary>
    ///     Compare 2 timestamps each in its own timebases.
    ///     The result of the function is undefined if one of the timestamps is outside the int64_t range when represented in
    ///     the others timebase.
    /// </summary>
    /// <param name="tsA"></param>
    /// <param name="tbA"></param>
    /// <param name="tsB"></param>
    /// <param name="tbB"></param>
    /// <returns>-1 if tsA is before tsB, 1 if tsA is after tsB or 0 if they represent the same position</returns>
    [UtilFunction("av_compare_ts")]
    public delegate int CompareTimestamp(long tsA, Rational tbA, long tsB, Rational tbB);

    /// <summary>
    ///     Compare 2 integers modulo mod.
    ///     That is we compare integers a and b for which only the least significant log2(mod) bits are known.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="mod">must be a power of 2</param>
    /// <returns>
    ///     a negative value if a is smaller than b
    ///     <para></para>
    ///     a positive value if a is greater than b
    ///     <para></para>
    ///     0                if a equals          b
    /// </returns>
    [UtilFunction("av_compare_mod")]
    public delegate long CompareMod(long a, long b, long mod);

    /// <summary>
    ///     Rescale a timestamp while preserving known durations.
    /// </summary>
    /// <param name="inTb">Input timebase</param>
    /// <param name="inTs">Input timestamp</param>
    /// <param name="fsTb">Duration and *last timebase</param>
    /// <param name="duration">duration till the next call</param>
    /// <param name="last"></param>
    /// <param name="outTb">Output timebase</param>
    /// <returns></returns>
    [UtilFunction("av_rescale_delta")]
    public unsafe delegate long RescaleDelta(
        Rational inTb, long inTs, Rational fsTb, int duration, long* last, Rational outTb);

    /// <summary>
    ///     Add a value to a timestamp.
    ///     <para></para>
    ///     This function guarantees that when the same value is repeatly added that no accumulation of rounding errors occurs.
    /// </summary>
    /// <param name="tsTb">Input timestamp timebase</param>
    /// <param name="ts">Input timestamp</param>
    /// <param name="incTb">inc timebase</param>
    /// <param name="inc">value to add to ts</param>
    /// <returns></returns>
    [UtilFunction("av_add_stable")]
    public delegate long AvAddStable(Rational tsTb, long ts, Rational incTb, long inc);
}