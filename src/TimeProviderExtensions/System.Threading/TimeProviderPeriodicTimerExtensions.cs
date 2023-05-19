namespace System.Threading;

/// <summary>
/// PeriodicTimer extensions for <see cref="TimeProvider"/>.
/// </summary>
public static class TimeProviderPeriodicTimerExtensions
{
#if NET6_0_OR_GREATER && !NET8_0_OR_GREATER
    /// <summary>
    /// Factory method that creates a periodic timer that enables waiting asynchronously for timer ticks.
    /// Use this factory method as a replacement for instantiating a <see cref="System.Threading.PeriodicTimer"/>.
    /// </summary>
    /// <remarks>
    /// This timer is intended to be used only by a single consumer at a time: only one call to <see cref="System.Threading.PeriodicTimer.WaitForNextTickAsync" />
    /// may be in flight at any given moment. <see cref="System.Threading.PeriodicTimer.Dispose()"/> may be used concurrently with an active <see cref="System.Threading.PeriodicTimer.WaitForNextTickAsync"/>
    /// to interrupt it and cause it to return false.
    /// </remarks>
    /// <returns>
    /// A new <see cref="TimeProviderExtensions.PeriodicTimer"/>.
    /// Note, this is a wrapper around a <see cref="System.Threading.PeriodicTimer"/>,
    /// and will behave exactly the same as the original.
    /// </returns>
    public static TimeProviderExtensions.PeriodicTimer CreatePeriodicTimer(this TimeProvider timeProvider, TimeSpan period)
    {
        if (timeProvider == TimeProvider.System)
        {
            return new PeriodicTimerWrapper(period);
        }

        ArgumentNullException.ThrowIfNull(timeProvider);

        return new ManualPeriodicTimer(period, timeProvider);
    }
#endif
#if NET8_0_OR_GREATER
    /// <summary>
    /// Factory method that creates a periodic timer that enables waiting asynchronously for timer ticks.
    /// Use this factory method as a replacement for instantiating a <see cref="System.Threading.PeriodicTimer"/>.
    /// </summary>
    /// <remarks>
    /// This timer is intended to be used only by a single consumer at a time: only one call to <see cref="System.Threading.PeriodicTimer.WaitForNextTickAsync" />
    /// may be in flight at any given moment. <see cref="System.Threading.PeriodicTimer.Dispose()"/> may be used concurrently with an active <see cref="System.Threading.PeriodicTimer.WaitForNextTickAsync"/>
    /// to interrupt it and cause it to return false.
    /// </remarks>
    /// <returns>A new <see cref="TimeProviderExtensions.PeriodicTimer"/>. Note, this is a wrapper around a <see cref="System.Threading.PeriodicTimer"/>.</returns>
    public static System.Threading.PeriodicTimer CreatePeriodicTimer(this TimeProvider timeProvider, TimeSpan period)
        => new PeriodicTimer(period, timeProvider);
#endif
}