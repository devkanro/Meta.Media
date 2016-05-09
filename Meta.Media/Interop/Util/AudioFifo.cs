// Project: Meta.Media (https://github.com/higankanshi/Meta.Media)
// Filename: AudioFifo.cs
// Version: 20160508

using System;

namespace Meta.Media.Interop.Util
{
    /// <summary>
    ///     Context for an Audio FIFO Buffer.
    ///     <para></para>
    ///     - Operates at the sample level rather than the byte level.
    ///     <para></para>
    ///     - Supports multiple channels with either planar or packed sample format.
    ///     <para></para>
    ///     - Automatic reallocation when writing to a full buffer.
    /// </summary>
    public struct AudioFifoPtr
    {
        internal AudioFifoPtr(IntPtr value)
        {
            Value = value;
        }

        /// <summary>
        ///     Get pointer value.
        /// </summary>
        public IntPtr Value { get; internal set; }

        public static bool operator ==(AudioFifoPtr value1, AudioFifoPtr value2)
        {
            return value1.Equals(value2);
        }

        public static bool operator !=(AudioFifoPtr value1, AudioFifoPtr value2)
        {
            return !(value1 == value2);
        }

        public bool Equals(AudioFifoPtr pointer)
        {
            return Value == pointer.Value;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is AudioFifoPtr))
            {
                return false;
            }

            return Equals((AudioFifoPtr) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value}(AudioFifo)";
        }
    }

    /// <summary>
    ///     Free an <see cref="AudioFifoPtr" />.
    /// </summary>
    /// <param name="context">AudioFifo to free</param>
    [UtilFunction("av_audio_fifo_free")]
    public delegate void AudioFifoFree(AudioFifoPtr context);

    /// <summary>
    ///     Allocate an AudioFifo.
    /// </summary>
    /// <param name="sampleFormat">sample format</param>
    /// <param name="channelCount">number of channels</param>
    /// <param name="sampleCount">initial allocation size, in samples</param>
    /// <returns>newly allocated AudioFifo, or NULL on error</returns>
    [UtilFunction("av_audio_fifo_alloc")]
    public delegate AudioFifoPtr AudioFifoAlloc(SampleFormat sampleFormat, int channelCount, int sampleCount);

    /// <summary>
    ///     Reallocate an AudioFifo.
    /// </summary>
    /// <param name="context">AudioFifo to reallocate</param>
    /// <param name="sampleCount">new allocation size, in samples</param>
    /// <returns>0 if OK, or negative ERROR code on failure</returns>
    [UtilFunction("av_audio_fifo_realloc")]
    // TODO: Error Code.
    public delegate int AudioFifoRealloc(AudioFifoPtr context, int sampleCount);

    /// <summary>
    ///     Write data to an AudioFifo.
    ///     <para></para>
    ///     The AudioFifo will be reallocated automatically if the available space is less than sampleCount.
    /// </summary>
    /// <param name="context">AudioFifo to write to</param>
    /// <param name="data">audio data plane pointers</param>
    /// <param name="sampleCount">number of samples to write</param>
    /// <returns>
    ///     number of samples actually written, or negative ERROR code on failure. If successful, the number of samples
    ///     actually written will always be sampleCount.
    /// </returns>
    /// <seealso cref="SampleFormat" />
    [UtilFunction("av_audio_fifo_write")]
    // TODO: Error Code.
    public unsafe delegate int AudioFifoWrite(AudioFifoPtr context, void** data, int sampleCount);

    /// <summary>
    ///     Read data from an AudioFifo.
    /// </summary>
    /// <param name="context">AudioFifo to read from</param>
    /// <param name="data">audio data plane pointers</param>
    /// <param name="sampleCount">number of samples to read</param>
    /// <returns>
    ///     number of samples actually written, or negative ERROR code on failure. If successful, the number of samples
    ///     actually read will always be greater than sampleCount, and will only be less than sampleCount if
    ///     <seealso cref="AudioFifoGetSize" /> is less than sampleCount.
    /// </returns>
    /// <seealso cref="SampleFormat" />
    [UtilFunction("av_audio_fifo_read")]
    // TODO: Error Code.
    public unsafe delegate int AudioFifoRead(AudioFifoPtr context, void** data, int sampleCount);

    /// <summary>
    ///     Drain data from an AudioFifo.
    ///     <para></para>
    ///     Removes the data without reading it.
    /// </summary>
    /// <param name="context">AudioFifo to drain</param>
    /// <param name="sampleCount">number of samples to drain</param>
    /// <returns>0 if OK, or negative ERROR code on failure</returns>
    [UtilFunction("av_audio_fifo_drain")]
    // TODO: Error Code.
    public delegate int AudioFifoDrain(AudioFifoPtr context, int sampleCount);

    /// <summary>
    ///     Reset the AudioFifo buffer.
    ///     <para></para>
    ///     This empties all data in the buffer.
    /// </summary>
    /// <param name="context">AudioFifo to reset</param>
    [UtilFunction("av_audio_fifo_reset")]
    public delegate void AudioFifoReset(AudioFifoPtr context);

    /// <summary>
    ///     Get the current number of samples in the AudioFifo available for reading.
    /// </summary>
    /// <param name="context">the AudioFifo to query</param>
    /// <returns>number of samples available for reading</returns>
    [UtilFunction("av_audio_fifo_size")]
    public delegate int AudioFifoGetSize(AudioFifoPtr context);

    /// <summary>
    ///     Get the current number of samples in the AudioFifo available for writing.
    /// </summary>
    /// <param name="context">the AudioFifo to query</param>
    /// <returns>number of samples available for writing</returns>
    [UtilFunction("av_audio_fifo_space")]
    public delegate int AudioFifoGetSpace(AudioFifoPtr context);
}