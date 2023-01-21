using NUnit.Framework;

namespace HGS.RpgStats
{
  public class VitalEventsTest
  {
    [Test]
    public void OnChangeEvent()
    {
      var maxHealth = new Stat();
      var health = new Vital();

      maxHealth.Set(100);

      health.Max = maxHealth;

      health.onChange += (change) =>
      {
        Assert.AreEqual(20, change.delta);
        Assert.AreEqual(true, change.IsChanged);
      };

      health.Set(20);
    }
  }
}