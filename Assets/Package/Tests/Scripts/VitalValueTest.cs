using NUnit.Framework;

namespace HGS.RpgStats
{
  public class VitalValueTest
  {
    [Test]
    public void Set()
    {
      var maxHealth = new Stat();
      var health = new Vital();

      maxHealth.Set(100);

      health.Max = maxHealth;

      health.Set(200);
      Assert.AreEqual(100, health.Current);
      Assert.AreEqual(true, health.IsFull);
      Assert.AreEqual(false, health.IsEmpty);

      health.Set(-100);
      Assert.AreEqual(0, health.Current);
      Assert.AreEqual(false, health.IsFull);
      Assert.AreEqual(true, health.IsEmpty);
    }
  }
}