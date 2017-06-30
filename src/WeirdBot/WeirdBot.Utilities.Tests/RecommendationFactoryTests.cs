using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeirdBot.DataAccess;
using Moq;
using WeirdBot.Utilities;
using WeirdBot.Models;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using WeirdBot.DataAccess.DataObjects;
using WeirdBot.Testing;

namespace WeirdBot.Utilities.Tests
{
    [TestClass]
    public class RecommendationFactoryTests
    {
        [TestInitialize]
        public void SetUp() { }

        [TestMethod]
        public void RecommendationFactory_GetRecommendation_WithValidPriceRangeAndUsage_ShouldReturnRecommendation()
        {
            var usage = new[] { Usage.General, Usage.Business };
            var priceCap = 400.00M;
            var repository = new Mock<ComponentRepository>();
            var engineSource = new Mock<RecommendationEngineSupplier>();
            engineSource
                .Setup(x => x.GetComponentRecommendationEngine(
                                It.IsAny<IComponentRepository>(), 
                                It.IsAny<ComponentType>()))
                .Returns((IComponentRepository r, ComponentType t) => 
                            GetFakeEngineFor(t, usage, priceCap));

            var sut = new RecommendationFactory(repository.Object, engineSource.Object);
            var result = sut.GetRecommendation(usage, priceCap);

            engineSource.VerifyAll();

            Assert.IsInstanceOfType(result, typeof(Recommendation));

            Assert.IsTrue(result.Processor.Category == ComponentType.Processor);
            Assert.IsTrue(result.HardDiskDrive.Category == ComponentType.HardDrive);
            Assert.IsTrue(result.RamKit.Category == ComponentType.RAM);
            Assert.IsTrue(result.VideoCard.Category == ComponentType.VideoCard);
            Assert.IsTrue(result.TotalPrice <= priceCap);
        }

        private IComponentRecommendationEngine GetFakeEngineFor(ComponentType type, Usage[] usage, decimal highPrice)
        {
            var quality = UsageQualityRank.GetHighetstRankOf(usage, type);
            var componentHighPrice = UsageProfiles.GetPricePercentage(type, usage) * highPrice;
            var fakeEngine = new Mock<IComponentRecommendationEngine>();
            fakeEngine
                .Setup(x => x.GetRecommendedComponent(
                            It.IsAny<Usage[]>(),
                            It.IsAny<decimal>()))
                .Returns((Usage[] u, decimal d) => 
                    new Component {
                        Category = type,
                        Quality = quality,
                        Price = componentHighPrice - 50.00M
                    });

            return fakeEngine.Object;
        }

        private IComponentRecommendationEngine GetFakeEngineFor(ComponentType t)
        {
            var fakeEngine = new Mock<IComponentRecommendationEngine>();
            fakeEngine
                .Setup(x => x.GetRecommendedComponent(It.IsAny<Usage[]>(), It.IsAny<decimal>()))
                .Returns(new Component { Category = t });

            return fakeEngine.Object;
        }

        [TestMethod]
        public void RecommendationFactory_GetRecommendation_WithInvalidPriceRange_ShouldReturnNullRecommendation()
        {
            var usage = new[] { Usage.General, Usage.Business };
            var highPrice = 10.00M;
            var repository = new Mock<ComponentRepository>();
            var engineSource = new Mock<RecommendationEngineSupplier>();
            engineSource
                .Setup(x => x.GetComponentRecommendationEngine(It.IsAny<ComponentRepository>(), It.IsAny<ComponentType>()))
                .Returns(new NullRecommendationEngine(repository.Object));

            var sut = new RecommendationFactory(repository.Object, engineSource.Object);
            var result = sut.GetRecommendation(usage, highPrice);

            Assert.IsInstanceOfType(result, typeof(Recommendation));

            Assert.IsNull(result.HardDiskDrive);
            Assert.IsNull(result.Processor);
            Assert.IsNull(result.RamKit);
            Assert.IsNull(result.VideoCard);
        }

    }
}
