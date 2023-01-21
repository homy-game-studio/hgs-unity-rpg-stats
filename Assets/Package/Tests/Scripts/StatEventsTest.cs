using NUnit.Framework;

namespace HGS.RpgStats
{
  public class StatEventsTest
  {
    [Test]
    public void OnChangeEvent()
    {
      var stat = new Stat();

      stat.onChange += (change) =>
      {
        Assert.AreEqual(100, change.delta);
        Assert.AreEqual(true, change.IsChanged);
      };

      stat.Set(100);
    }

    [Test]
    public void OnNotChangeEvent()
    {
      var stat = new Stat();

      stat.onChange += (change) =>
      {
        Assert.AreEqual(false, change.IsChanged);
      };

      stat.Set(stat.Current);
    }
  }
}