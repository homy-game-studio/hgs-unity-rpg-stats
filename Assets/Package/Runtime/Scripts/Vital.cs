using System;
using UnityEngine;

namespace HGS.RpgStats
{
  [Serializable]
  /// <summary>
  /// Vital defines a flexible attribute, like Health and Mana.
  /// </summary>
  public class Vital : Stat
  {
    private Stat _max;

    public Stat Max
    {
      get
      {
        if (_max == null) throw new ArithmeticException("Max value is null, you need set Stat to max first");
        return _max;
      }
      set
      {
        // Remove old max changes binds
        if (_max != null) _max.onChange -= OnMaxChange;

        _max = value;

        // Apply onChange listener to new max value
        _max.onChange += OnMaxChange;

        NotifyChanges();
      }
    }

    public bool IsFull { get => Current > 0; }
    public bool IsEmpty { get => Current == 0; }
    public float Normalized { get => Current / Max.Current; }

    public Action onEmpty = null;
    public Action onFull = null;

    private void OnMaxChange(ChangeInfo _)
    {
      NotifyChanges();
    }

    /// <summary>
    /// Set the new vital value, clamping between 0 and Max.
    /// When value == 0 raise onEmpty event
    /// When value == Max raise onFull event
    /// </summary>
    /// <param name="value">Nova vida a ser definida</param>
    public override void Set(float value)
    {
      var previous = Current;
      Current = Mathf.Clamp(value, 0, Max.Current);
      var delta = value - previous;

      if (Current == Max.Current) onFull?.Invoke();
      if (Current == 0) onEmpty?.Invoke();

      NotifyChanges(delta);
    }

    /// <summary>
    /// Refill vital amount, setting Max as value
    /// </summary>
    public void Refill() => Set(Max.Current);
  }
}