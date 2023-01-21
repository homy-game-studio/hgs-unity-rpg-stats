using NUnit.Framework;

namespace HGS.RpgStats
{
  public class StatValueTest
  {
    [Test]
    public void Set()
    {
      var stat = new Stat();

      stat.Set(-100);
      Assert.AreEqual(0, stat.Current);

      stat.Set(15);
      Assert.AreEqual(15, stat.Current);
    }

    [Test]
    public void Increase()
    {
      var stat = new Stat();
      stat.Set(100);
      stat.Increase(25);
      Assert.AreEqual(125, stat.Current);
    }

    [Test]
    public void Decrease()
    {
      var stat = new Stat();
      stat.Set(100);
      stat.Decrease(25);
      Assert.AreEqual(75, stat.Current);
    }

    [Test]
    public void IncreasePercentile()
    {
      var stat = new Stat();
      stat.Set(100);
      stat.IncreasePercentile(0.1f);
      Assert.AreEqual(110, stat.Current);
    }

    [Test]
    public void DecreasePercentile()
    {
      var stat = new Stat();
      stat.Set(100);
      stat.DecreasePercentile(0.1f);
      Assert.AreEqual(90, stat.Current);
    }
  }
}