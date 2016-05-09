// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: Display.cs
// Version: 20160508

namespace Meta.Media.Interop.Util
{
    /// <summary>
    ///     Extract the rotation component of the transformation matrix.
    /// </summary>
    /// <param name="matrix">the transformation matrix</param>
    /// <returns>
    ///     the angle (in degrees) by which the transformation rotates the frame counterclockwise. The angle will be in
    ///     range [-180.0, 180.0], or NaN if the matrix is singular.
    /// </returns>
    /// <remarks>
    ///     Note: floating point numbers are inherently inexact, so callers are recommended to round the return value to
    ///     nearest integer before use.
    /// </remarks>
    [UtilFunction("av_display_rotation_get")]
    public delegate double DisplayGetRotation(int[] matrix);

    /// <summary>
    ///     Initialize a transformation matrix describing a pure counterclockwise rotation by the specified angle (in degrees).
    /// </summary>
    /// <param name="matrix">an allocated transformation matrix (will be fully overwritten by this function)</param>
    /// <param name="angle">rotation angle in degrees.</param>
    [UtilFunction("av_display_rotation_set")]
    public delegate void DisplaySetRotation(int[] matrix, double angle);

    /// <summary>
    ///     Flip the input matrix horizontally and/or vertically.
    /// </summary>
    /// <param name="matrix">an allocated transformation matrix</param>
    /// <param name="hFlip">whether the matrix should be flipped horizontally</param>
    /// <param name="vFlip">whether the matrix should be flipped vertically</param>
    [UtilFunction("av_display_matrix_flip")]
    public delegate void DisplayMatrixFlip(int[] matrix, int hFlip, int vFlip);
}