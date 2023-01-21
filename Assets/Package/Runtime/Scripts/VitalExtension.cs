using System;

namespace HGS.RpgStats
{
  public static class VitalExtension
  {
    /// <summary>
    /// <inheritdoc cref="Vital.Set" />
    /// </summary>
    public static void Set(Vital vital, Stat stat) => vital.Set(stat.Current);
  }
}