namespace TimeLib;
/// <summary>
/// An immutable class representing the time of day in hours, minutes and seconds.
/// </summary>
public record Time
{
    public int Hours { get; init; }
    public int Minutes { get; init; }
    public int Seconds { get; init; }

    /// <summary>
    /// Create a new <see cref="Time"/>. 
    /// </summary>
    /// <param name="hours"></param>
    /// <param name="minutes"></param>
    /// <param name="seconds"></param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="hours"/> must be non-negative and less than 24.
    /// <para>
    /// <paramref name="minutes"/> and <paramref name="seconds"/> must be between non-negative and less than 60.
    /// </para>
    /// </exception>
    public Time(int hours, int minutes, int seconds)
    {
        Hours = hours is >= 0 and <= 23
                ? hours
                : throw new ArgumentOutOfRangeException(nameof(hours), "Must be non-negative and less than 24");
        Minutes = minutes is >= 0 and <= 59
                ? minutes
                : throw new ArgumentOutOfRangeException(nameof(minutes), "Must be non-negative and less than 60");
        Seconds = seconds is >= 0 and <= 59
                ? seconds
                : throw new ArgumentOutOfRangeException(nameof(seconds), "Must be non-negative and less than 24");
    }

    /// <summary>
    /// Add a delay to the time maxing out at one seond before midnight (i.e., 23:59:59).
    /// </summary>
    /// <param name="delta">The time delay.</param>
    /// <returns>
    /// The <see cref="Time"/> calculated by adding <paramref name="delta"/> to the current <see cref="Time"/>.
    /// </returns>
    public Time AddNoWrapping(Time delta) => CalculateDelta(delta.Hours, delta.Minutes, delta.Seconds, shouldWrap: false);

    /// <summary>
    /// Add a delay to the time wrapping around at midnight.
    /// </summary>
    /// <param name="delta">The time delay.</param>
    /// <returns>
    /// The <see cref="Time"/> calculated by adding <paramref name="delta"/> to the current <see cref="Time"/>.
    /// </returns>
    public Time AddAndWrapAround(Time delta) => CalculateDelta(delta.Hours, delta.Minutes, delta.Seconds, shouldWrap: true);

    /// <summary>
    /// Subtract a delay to the time stoping at midnight (i.e., 00:00:00).
    /// </summary>
    /// <param name="delta">The time delay.</param>
    /// <returns>
    /// The <see cref="Time"/> calculated by subtracting <paramref name="delta"/> to the current <see cref="Time"/>.
    /// </returns>
    public Time SubtractNoWrapping(Time delta)
        => CalculateDelta(-delta.Hours, -delta.Minutes, -delta.Seconds, shouldWrap: false);

    /// <summary>
    /// Subtract a delay to the time wrapping around at midnight.
    /// </summary>
    /// <param name="delta">The time delay.</param>
    /// <returns>
    /// The <see cref="Time"/> calculated by subtracting <paramref name="delta"/> to the current <see cref="Time"/>.
    /// </returns>
    public Time SubtractAndWrapAround(Time delta)
        => CalculateDelta(-delta.Hours, -delta.Minutes, -delta.Seconds, shouldWrap: true);

    private Time CalculateDelta(int deltaHours, int deltaMinutes, int deltaSeconds, bool shouldWrap)
    {
        int hours = Hours;
        int minutes = Minutes;
        int seconds = Seconds;

        CaclulateSeconds(deltaSeconds, ref minutes, ref seconds);
        CalculateMinutes(deltaMinutes, ref hours, ref minutes);
        CalculateHours(deltaHours, ref hours, ref minutes, ref seconds, shouldWrap);

        return new Time(hours, minutes, seconds);
    }

    private static void CalculateHours(int deltaHours, ref int hours, ref int minutes, ref int seconds, bool shouldWrap)
    {
        hours += deltaHours;
        switch (hours)
        {
            case >= 24:
                if (shouldWrap)
                {
                    hours = 0;
                }
                else
                {
                    hours = 23;
                    minutes = 59;
                    seconds = 59;
                }
                break;
            case < 0:
                if (shouldWrap)
                {
                    hours = 23;
                }
                else
                {
                    hours = 0;
                    minutes = 0;
                    seconds = 0;
                }
                break;
        }
    }

    private static void CalculateMinutes(int deltaMinutes, ref int hours, ref int minutes)
    {
        minutes += deltaMinutes;
        switch (minutes)
        {
            case >= 60:
                hours += 1;
                minutes %= 60;
                break;
            case < 0:
                hours -= 1;
                minutes += 60;
                break;
        }
    }

    private static void CaclulateSeconds(int deltaSeconds, ref int minutes, ref int seconds)
    {
        seconds += deltaSeconds;
        switch (seconds)
        {
            case >= 60:
                minutes += 1;
                seconds %= 60;
                break;
            case < 0:
                minutes -= 1;
                seconds += 60;
                break;
        }
    }
}
