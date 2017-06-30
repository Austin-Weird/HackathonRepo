using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeirdBot.Models;

namespace WeirdBot.Utilities.Tests
{
    [TestClass]
    public class UsageQualityRankTests
    {
        [TestMethod]
        public void QualityRank_GetHighetstRankOf_GivenUsageArrayAndComponent_ShouldReturnHighestQuality()
        {
            var use = new Usage[] { Usage.Business, Usage.Gaming };

            var result = UsageQualityRank.GetHighetstRankOf(use, ComponentType.HardDrive);

            Assert.AreEqual(result, Quality.Best);

            use = new Usage[] { Usage.General, Usage.Programming };
            result = UsageQualityRank.GetHighetstRankOf(use, ComponentType.VideoCard);

            Assert.AreEqual(result, Quality.Good);
        }

    }
}
