using NUnit.Framework;
using Assets.Scripts.World.Model;
using Assets.Scripts.Model;

namespace Tests
{
    public class ResourceLayoutManagerTest
    {
        ResourceLayoutManager manager;
        private static readonly int SIZE = 500;
        private static readonly int SEED = 1000;
        private static readonly int HAB = 90;
        [SetUp]
        public void Init()
        {
            manager = new ResourceLayoutManager(SIZE, SEED, HAB);
        }
        [Test]
        public void TestLayoutResources()
        {
            var fields = manager.LayoutResources();
            Assert.AreEqual(fields[45].Feature, EResource.Iron);
            Assert.AreEqual(fields[33].Feature, EResource.Water);
        }
    }
}
