using System;
using System.Collections.Generic;
using UnityEngine;

namespace HGS.RpgStats
{
  public struct ChangeInfo
  {
    public float delta;
    public float current;
    public bool IsChanged => delta != 0;
  }

  [Serializable]
  /// <summary>
  /// Stat defines an attribute, like Damage, Armor, MaxHealth...
  /// </summary>
  public class Stat
  {
    public float Current { get; protected set; }

    public Action<ChangeInfo> onChange = null;


    private Dictionary<string, Modifier> _modifiers = new Dictionary<string, Modifier>();


    protected void NotifyChanges(float delta = 0)
    {
      onChange?.Invoke(new ChangeInfo
      {
        delta = delta,
        current = Current
      });
    }

    /// <summary>
    /// Set clamping between from 0 to value.
    /// </summary>
    /// <param name="value">Nova a ser definido</param>
    public virtual void Set(float value)
    {
      var previous = Current;
      Current = Mathf.Max(value, 0);
      var delta = value - previous;

      NotifyChanges(delta);
    }

    /// <summary>
    /// Returns true if modifier id is applied to this stat value
    /// </summary>
    public bool ContainsModifier(Modifier modifier)
    {
      return ContainsModifier(modifier.id);
    }

    /// <summary>
    /// Returns true if modifier id is applied to this stat value
    /// </summary>
    public bool ContainsModifier(string modifierId)
    {
      return _modifiers.ContainsKey(modifierId);
    }

    /// <summary>
    /// Add a modifier and apply the effect in stat value
    /// </summary>
    /// <param name="modifier"></param>
    public void AddModifier(Modifier modifier)
    {
      var isPersistent = modifier.usage == ModifierUsage.Persistent;
      var exists = ContainsModifier(modifier);

      if (isPersistent && exists) return;
      if (isPersistent) _modifiers.Add(modifier.id, modifier);

      switch (modifier.operation)
      {
        case ModifierOperation.Percentage: IncreasePercentile(modifier.value); break;
        case ModifierOperation.Sum: Increase(modifier.value); break;
      }
    }

    /// <summary>
    /// Remove a existent modifier, and remove effect.
    /// </summary>
    public void RemoveModifier(string modifierId)
    {
      if (!ContainsModifier(modifierId)) return;
      var modifier = _modifiers[modifierId];

      switch (modifier.operation)
      {
        case ModifierOperation.Percentage: DecreasePercentile(modifier.value); break;
        case ModifierOperation.Sum: Decrease(modifier.value); break;
      }
    }

    /// <summary>
    /// Remove a existent modifier, and remove effect.
    /// </summary>
    public void RemoveModifier(Modifier modifier)
    {
      RemoveModifier(modifier.id);
    }

    /// <summary>
    /// Increase stat amount by percentage, sample: 
    /// <para>
    /// Considering <see cref="percentage"/> = 0.1f;
    /// </para> 
    /// <code>
    /// 100 + 100 * <see cref="percentage"/> = 110;
    /// </code> 
    /// </summary>
    public void IncreasePercentile(float percentage) => Set(Current + Current * percentage);

    /// <summary>
    /// Reduce stat amount by percentage, sample: 
    /// <para>
    /// Considering <see cref="percentage"/> = 0.1f;
    /// </para> 
    /// <code>
    /// 100 - 100 * <see cref="percentage"/> = 90;
    /// </code> 
    /// </summary>
    public void DecreasePercentile(float percentage) => Set(Current - Current * percentage);

    /// <summary>
    /// Increase stat amount by raw number, sample: 
    /// <para>
    /// Considering <see cref="value"/> = 10;
    /// </para> 
    /// <code>
    /// 100 + 100 * <see cref="value"/> = 110;
    /// </code> 
    /// </summary>
    public void Increase(float value) => Set(Current + value);

    /// <summary>
    /// Reduce stat amount by raw number, sample: 
    /// <para>
    /// Considering <see cref="value"/> = 10;
    /// </para> 
    /// <code>
    /// 100 - 100 * <see cref="value"/> = 90;
    /// </code> 
    /// </summary>
    public void Decrease(float value) => Set(Current - value);
  }
}