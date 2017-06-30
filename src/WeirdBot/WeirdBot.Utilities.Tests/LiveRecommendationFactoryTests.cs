using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage;
using WeirdBot.Utilities;
using WeirdBot.Models;

namespace WeirdBot.DataAccess.Tests
{
    [TestClass]
    public class LiveRecommendationFactoryTests
    {
        IComponentRepository liveRepo;

        public LiveRecommendationFactoryTests()
        {
            var storageAccount = CloudStorageAccount
                 .Parse("DefaultEndpointsProtocol=https;AccountName=weirdbotdb;AccountKey=9Z+nS1VRMNQ2AqeMXpLO0aNrsEDtIQDHUQTiqHLVGgD6I34u5JcokVvb1+PuRck+WnBfV7r9ALtWaBse1UqcBQ==;EndpointSuffix=core.windows.net");
            liveRepo = new ComponentRepository(storageAccount.CreateCloudTableClient());
        }

        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void Live_GetComponentByPriceAndQuality_ForGamingBestPossible()
        {
            var priceCap = 5000.00M;

            var sut = new RecommendationFactory(liveRepo, new RecommendationEngineSupplier());
            var result = sut.GetRecommendation(new Usage[] { Usage.Gaming }, priceCap);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Processor);
            Assert.AreEqual(Quality.Best, result.Processor.Quality);
            Assert.IsNotNull(result.RamKit);
            Assert.AreEqual(Quality.Best, result.RamKit.Quality);
            Assert.IsNotNull(result.HardDiskDrive);
            Assert.AreEqual(Quality.Best, result.HardDiskDrive.Quality);
            Assert.IsNotNull(result.VideoCard);
            Assert.AreEqual(Quality.Best, result.VideoCard.Quality);
            Assert.IsTrue(result.TotalPrice <= priceCap);
        }

        [TestMethod]
        public void Live_GetComponentByPriceAndQuality_ForGamingMidRange()
        {
            var priceCap = 1000.00M;

            var sut = new RecommendationFactory(liveRepo, new RecommendationEngineSupplier());
            var result = sut.GetRecommendation(new Usage[] { Usage.Gaming }, priceCap);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Processor);
            Assert.IsNotNull(result.RamKit);
            Assert.IsNotNull(result.HardDiskDrive);
            Assert.IsNotNull(result.VideoCard);
            Assert.IsTrue(result.TotalPrice <= priceCap);
        }

        [TestMethod]
        public void Live_GetComponentByPriceAndQuality_ForGaming_ImpossiblePrice_ShouldReturnNullRecommendation()
        {
            var priceCap = 10.00M;

            var sut = new RecommendationFactory(liveRepo, new RecommendationEngineSupplier());
            var result = sut.GetRecommendation(new Usage[] { Usage.Gaming }, priceCap);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Processor);
            Assert.IsNull(result.RamKit);
            Assert.IsNull(result.HardDiskDrive);
            Assert.IsNull(result.VideoCard);
            Assert.IsTrue(result.TotalPrice == 0.00M);
        }

        [TestMethod]
        public void Live_GetComponentByPriceAndQuality_ForGeneral()
        {
            var priceCap = 500.00M;

            var sut = new RecommendationFactory(liveRepo, new RecommendationEngineSupplier());
            var result = sut.GetRecommendation(new Usage[] { Usage.General }, priceCap);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Processor);
            Assert.IsNotNull(result.RamKit);
            Assert.IsNotNull(result.HardDiskDrive);
            Assert.IsNotNull(result.VideoCard);
            Assert.IsTrue(result.TotalPrice <= priceCap);
        }

        [TestMethod]
        public void Live_GetComponentByPriceAndQuality_ForGeneralAndBusiness_ShouldUseBusinessQuality()
        {
            var priceCap = 760.00M;
            var usage = new Usage[] { Usage.General, Usage.Business };

            var sut = new RecommendationFactory(liveRepo, new RecommendationEngineSupplier());
            var result = sut.GetRecommendation(usage, priceCap);

            Assert.IsNotNull(result);

            Assert.IsNotNull(result.Processor);
            Assert.IsNotNull(result.RamKit);
            Assert.IsNotNull(result.HardDiskDrive);
            Assert.IsNotNull(result.VideoCard);
            Assert.IsTrue(result.TotalPrice <= priceCap);
        }



    }
}
