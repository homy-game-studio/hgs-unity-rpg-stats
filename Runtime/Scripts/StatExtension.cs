using System;

namespace HGS.RpgStats
{
  public static class StatExtension
  {
    public static bool InRange(this Stat myStat, float min, float max)
    {
      return myStat.Current >= min && myStat.Current <= max;
    }

    public static bool IsPercentile(this Stat myStat)
    {
      return InRange(myStat, 0f, 1f);
    }

    /// <summary>
    /// <inheritdoc cref="Stat.IncreasePercentile" />
    /// </summary>
    public static void IncreasePercentile(Stat myStat, Stat stat)
    {
      if (!IsPercentile(stat))
      {
        throw new ArgumentException($"Failed to increase stat value because received stat value is not between 0f and 1f");
      }

      myStat.IncreasePercentile(stat.Current);
    }

    /// <summary>
    /// <inheritdoc cref="Stat.DecreasePercentile" />
    /// </summary>
    public static void DecreasePercentile(this Stat myStat, Stat stat)
    {
      if (!IsPercentile(stat))
      {
        throw new ArgumentException($"Failed to decrease stat value because received stat value is not between 0f and 1f");
      }

      myStat.DecreasePercentile(stat.Current);
    }

    /// <summary>
    /// <inheritdoc cref="Stat.Set" />
    /// </summary>
    public static void Set(Stat myStat, Stat stat) => myStat.Set(stat.Current);

    /// <summary>
    /// <inheritdoc cref="Stat.Increase" />
    /// </summary>
    public static void Increase(this Stat myStat, Stat stat) => myStat.Increase(stat.Current);

    /// <summary>
    /// <inheritdoc cref="Stat.Decrease" />
    /// </summary>
    public static void Decrease(this Stat myStat, Stat stat) => myStat.Decrease(stat.Current);
  }
}