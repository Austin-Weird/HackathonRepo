using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeirdBot.Models;

namespace WeirdBot.Utilities.Tests
{
    [TestClass]
    public class UsageQualityRankTests
    {
        [TestMethod]
        public void QualityRank_GetHighetstRankOf_GivenUsageArray_ShouldReturnHighestQuality()
        {
            var use = new Usage[] { Usage.Business, Usage.Gaming };

            var result = UsageQualityRank.GetHighetstRankOf(use);

            Assert.AreEqual(result, Quality.Best);

            use = new Usage[] { Usage.General, Usage.Programming };
            result = UsageQualityRank.GetHighetstRankOf(use);

            Assert.AreEqual(result, Quality.Better);
        }

        [TestMethod]
        public void QualityRank_GetRankOf_ShouldReturnMatchingQuality()
        {
            var result = UsageQualityRank.GetRankOf(Usage.Business);

            Assert.AreEqual(result, Quality.Better);

            result = UsageQualityRank.GetRankOf(Usage.Gaming);

            Assert.AreEqual(result, Quality.Best);

            result = UsageQualityRank.GetRankOf(Usage.General);

            Assert.AreEqual(result, Quality.Good);

            result = UsageQualityRank.GetRankOf(Usage.Media);

            Assert.AreEqual(result, Quality.Best);

            result = UsageQualityRank.GetRankOf(Usage.Programming);

            Assert.AreEqual(result, Quality.Better);

        }
    }
}
