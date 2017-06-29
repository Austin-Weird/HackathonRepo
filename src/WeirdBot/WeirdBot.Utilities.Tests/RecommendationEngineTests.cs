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
            var targetPrice = 501.00M;
            var quality = Quality.Better;
            var usage = new Usage[] { Usage.Programming, Usage.Business };
            var testRepo = new Mock<IComponentRepository>();
            testRepo
                .Setup(x => x.GetComponentByPriceAndQuality(ComponentType.HardDrive, 
                                                                It.IsAny<Quality>(), It.IsAny<decimal>()))
                .Returns(new Component { Category = ComponentType.HardDrive, Quality = Quality.Best, Price = 175.00M });

            var sut = new HardDriveRecommendationEngine(testRepo.Object);
            var result = sut.GetRecommendedComponent(usage, targetPrice);

            Assert.AreEqual(ComponentType.HardDrive, result.Category);
            Assert.IsTrue(result.Price <= targetPrice);
            Assert.IsTrue(result.Quality >= quality);
        }

        [TestMethod]
        public void RecommendationEngine_GetRecommendedComponent_WithInvalidPrice_ShouldReturnNull()
        {
            //Gaming price should be > $600
            var targetPrice = 500.00M;
            var usage = new Usage[] { Usage.Gaming };
            var testRepo = new Mock<IComponentRepository>();

            var sut = new HardDriveRecommendationEngine(testRepo.Object);
            var result = sut.GetRecommendedComponent(usage, targetPrice);

            Assert.IsNull(result, "{0} <= {1}", Enum.GetName(typeof(Usage), usage[0]), targetPrice);


            //Media/Programming Price should be > $500
            targetPrice = 250.00M;
            usage = new Usage[] { Usage.Programming };

            result = sut.GetRecommendedComponent(usage, targetPrice);

            Assert.IsNull(result, "{0} <= {1}", Enum.GetName(typeof(Usage), usage[0]), targetPrice);


            //General/Business/Default Price should be > $200
            targetPrice = 50.00M;
            usage = new Usage[] { Usage.Business };

            result = sut.GetRecommendedComponent(usage, targetPrice);

            Assert.IsNull(result, "{0} <= {1}", Enum.GetName(typeof(Usage), usage[0]), targetPrice);


        }

    }
}
