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

        //private Recommendation fakeRecommendation;

        [TestInitialize]
        public void SetUp()
        {
            //fakeRecommendation = new Recommendation
            //{
            //    Processor = new Component() { Category = ComponentType.Processor },
            //    HardDiskDrive = new Component() { Category = ComponentType.HardDrive },
            //    RamKit = new Component() { Category = ComponentType.RAM },
            //    VideoCard = new Component() { Category = ComponentType.VideoCard },
            //    SoundCard = new Component() { Category = ComponentType.SoundCard }
            //};
        }

        [TestMethod]
        public void RecommendationFactory_GetRecommendation_WithValidPriceRangeAndUsage_ShouldReturnRecommendation()
        {
            var usage = new[] { Usage.General, Usage.Business };
            var highPrice = 400.00M;
            var repository = new Mock<ComponentRepository>();
            var engineSource = new Mock<RecommendationEngineSupplier>();
            engineSource
                .Setup(x => x.GetComponentRecommendationEngine(It.IsAny<ComponentRepository>(), It.IsAny<ComponentType>()))
                .Returns((ComponentRepository r, ComponentType t) => GetFakeEngineFor(t));

            var sut = new RecommendationFactory(repository.Object, engineSource.Object);
            var result = sut.GetRecommendation(usage, highPrice);

            Assert.IsInstanceOfType(result, typeof(Recommendation));

            Assert.IsTrue(result.Processor.Category == ComponentType.Processor);
            Assert.IsTrue(result.HardDiskDrive.Category == ComponentType.HardDrive);
            Assert.IsTrue(result.RamKit.Category == ComponentType.RAM);
            Assert.IsTrue(result.VideoCard.Category == ComponentType.VideoCard);
            Assert.IsTrue(result.SoundCard.Category == ComponentType.SoundCard);
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
            Assert.IsNull(result.SoundCard);
            Assert.IsNull(result.VideoCard);
        }


        //private Mock<ComponentRepository> GetMockRepository()
        //{
        //    var fakeCloudTableClient = new Mock<CloudTableClient>();
        //    var fakeCloudTable = new Mock<CloudTable>();
        //    fakeCloudTable
        //        .Setup(t => t.ExecuteQuery(It.IsAny<TableQuery<ComponentEntity>>(),
        //                                It.IsAny<TableRequestOptions>(),
        //                                It.IsAny<OperationContext>()))
        //         .Returns((TableQuery q, TableRequestOptions o, OperationContext c)
        //            => TestData.GetFakeComponentEntities(q.FilterString));

        //    fakeCloudTableClient
        //        .Setup(x => x.GetTableReference("components"))
        //        .Returns(fakeCloudTable.Object);

        //    return new Mock<ComponentRepository>(fakeCloudTableClient.Object);
        //}

    }
}
