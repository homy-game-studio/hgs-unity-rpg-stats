using NUnit.Framework;

namespace HGS.RpgStats
{
  public class StatModifiersTest
  {
    Modifier HealthPotion = new Modifier
    {
      id = "HealthPotion",
      operation = ModifierOperation.Sum,
      usage = ModifierUsage.Consumable,
      value = 10,
    };

    Modifier MaxHealth = new Modifier
    {
      id = "IncreaseMaxHealth",
      operation = ModifierOperation.Sum,
      usage = ModifierUsage.Persistent,
      value = 100,
    };

    [Test]
    public void AddModifier()
    {
      var stat = new Stat();

      stat.Set(100);

      stat.AddModifier(HealthPotion);

      Assert.AreEqual(110, stat.Current);
    }

    [Test]
    public void RemoveModifier()
    {
      var stat = new Stat();

      stat.Set(100);

      stat.AddModifier(MaxHealth);
      Assert.AreEqual(200, stat.Current);

      stat.RemoveModifier(MaxHealth);
      Assert.AreEqual(100, stat.Current);
    }
  }
}