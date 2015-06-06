using System.Collections.Generic;
using System.Linq;
using Ceriyo.Data.GameObjects;
using Ceriyo.Library.Processing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ceriyo.Library.Tests.Processing
{
    [TestClass]
    public class GameResourceProcessorTests
    {
        [TestMethod]
        public void GenerateUniqueResref_FromList_IsEqualToArea2()
        {
            IList<Area> list = new List<Area>();
            list.Add(new Area("Area0", "Area0", "Area0", 0, 0, 0));
            list.Add(new Area("Area1", "Area1", "Area1", 0, 0, 0));

            string resref = GameResourceProcessor.GenerateUniqueResref(list.Cast<IGameObject>().ToList(), "Area");

            Assert.AreEqual("Area2", resref);
        }
    }
}
