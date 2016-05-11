// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: RandomSeed.cs
// Version: 20160511

namespace Meta.Media.Interop.Util
{
    /// <summary>
    ///     Get a seed to use in conjunction with random functions.
    ///     <para></para>
    ///     This function tries to provide a good seed at a best effort bases.
    ///     <para></para>
    ///     Its possible to call this function multiple times if more bits are needed.
    ///     <para></para>
    ///     It can be quite slow, which is why it should only be used as seed for a faster PRNG. The quality of the seed
    ///     depends on the platform.
    /// </summary>
    /// <returns></returns>
    [UtilFunction("av_get_random_seed")]
    public delegate uint GetRandomSpeed();
}