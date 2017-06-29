using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeirdBot.Models;

namespace WeirdBot.Models.Tests
{
    [TestClass]
    public class RecommendationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Recommendation_PropertySet_WithInvalidComponent_ShouldThrowException()
        {
            var sut = new Recommendation();
            sut.HardDiskDrive = new Component { Category = ComponentType.RAM };
        }

        [TestMethod]
        public void Recommendation_PropertySetToNull_ShouldSetBackingVariableAndPreserveTotalPrice()
        {
            var hddPrice = 200.00M;
            var sut = new Recommendation();

            Assert.IsNull(sut.HardDiskDrive);
            Assert.IsNull(sut.Processor);
            Assert.AreEqual(0.00M, sut.TotalPrice);

            sut.HardDiskDrive = new Component { Category = ComponentType.HardDrive, Price = hddPrice };

            Assert.IsNotNull(sut.HardDiskDrive);
            Assert.AreEqual(hddPrice, sut.TotalPrice);

            sut.Processor = null;

            Assert.IsNull(sut.Processor);
            Assert.IsNotNull(sut.HardDiskDrive);
            Assert.AreEqual(hddPrice, sut.TotalPrice);
        }

        [TestMethod]
        public void Recommendation_PropertySet_ShouldSetBackingVariableAndUpdateTotalPrice()
        {
            var hddPrice = 200.00M;
            var sut = new Recommendation();

            Assert.IsNull(sut.HardDiskDrive);
            Assert.IsNull(sut.Processor);
            Assert.AreEqual(0.00M, sut.TotalPrice);

            sut.HardDiskDrive = new Component { Category = ComponentType.HardDrive, Price = hddPrice };

            Assert.IsNotNull(sut.HardDiskDrive);
            Assert.AreEqual(hddPrice, sut.TotalPrice);

            var cpuPrice = 350.00M;

            sut.Processor = new Component { Category = ComponentType.Processor, Price = cpuPrice };

            Assert.IsNotNull(sut.Processor);
            Assert.AreEqual(cpuPrice + hddPrice, sut.TotalPrice);
        }
    }
}
