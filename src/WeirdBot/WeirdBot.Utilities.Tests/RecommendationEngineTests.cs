using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeirdBot.DataAccess;
using Moq;
using WeirdBot.Utilities;
using WeirdBot.Models;

namespace WeirdBot.Utilities.Tests
{
    [TestClass]
    public class RecommendationFactoryTests
    {
        [TestMethod]
        public void GetRecommendation_WithValidPriceRangeAndUsage_ShouldReturnRecommendation()
        {
            var usage = new[] { Usage.General, Usage.Business };
            var lowPrice = 150.00M;
            var highPrice = 400.00M;
            var fakeRecommendation = new Recommendation
            {
                Processor = new Component() { Category = ComponentType.Processor },
                HardDiskDrive = new Component() { Category = ComponentType.HardDrive },
                RamKit = new Component() { Category = ComponentType.RAM },
                VideoCard = new Component() { Category = ComponentType.VideoCard },
                SoundCard = new Component() { Category = ComponentType.SoundCard }
            };
            var repository = new Mock<ComponentRepository>();

            var sut = new RecommendationFactory(repository.Object);
            var result = sut.GetRecommendation(usage, lowPrice, highPrice);

            Assert.IsInstanceOfType(result, typeof(Recommendation));

            Assert.IsTrue(result.Processor.Category == ComponentType.Processor);
            Assert.IsTrue(result.HardDiskDrive.Category == ComponentType.Processor);
            Assert.IsTrue(result.RamKit.Category == ComponentType.Processor);
            Assert.IsTrue(result.VideoCard.Category == ComponentType.Processor);
            Assert.IsTrue(result.SoundCard.Category == ComponentType.Processor);
        }

        [TestMethod]
        public void GetRecommendation_WithInvalidPriceRange_ShouldReturnNullRecommendation()
        {
            Assert.Inconclusive("Not Implemented");
        }
    }
}
