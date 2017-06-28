using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeirdBot.DataAccess;
using WeirdBot.Models;

namespace WeirdBot.Utilities.Tests
{
    [TestClass]
    public class RecommendationEngineTests
    {

        [TestInitialize]
        public void SetUp()
        {
        }

        [TestMethod]
        public void RecommendationEngine_GetRecommendedComponent_ShouldReturnMatchingHardDriveComponent()
        {
            var targetPrice = 250.00M;
            var quality = Quality.Better;
            var usage = new Usage[] { Usage.Programming, Usage.Business };
            var testRepo = new Mock<IComponentRepository>();
            testRepo
                .Setup(x => x.GetComponentByPriceAndPowerRank(ComponentType.HardDrive, 
                                                                It.IsAny<Quality>(), It.IsAny<decimal>()))
                .Returns(new Component { Category = ComponentType.HardDrive, Quality = Quality.Best, Price = 175.00M });

            var sut = new HardDriveRecommendationEngine(testRepo.Object);
            var result = sut.GetRecommendedComponent(usage, 800.00M);

            Assert.AreEqual(ComponentType.HardDrive, result.Category);
            Assert.IsTrue(result.Price <= targetPrice);
            Assert.IsTrue(result.Quality >= quality);
        }


    }
}
