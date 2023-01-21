using System;

namespace HGS.RpgStats
{
  public enum ModifierUsage
  {
    Consumable,
    Persistent
  }

  public enum ModifierOperation
  {
    Sum,
    Percentage,
  }

  [Serializable]
  public class Modifier
  {
    public string id;
    public float value;
    public ModifierUsage usage;
    public ModifierOperation operation;
  }
}