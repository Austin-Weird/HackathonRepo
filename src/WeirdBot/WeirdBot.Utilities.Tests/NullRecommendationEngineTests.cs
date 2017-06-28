using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeirdBot.Models;

namespace WeirdBot.Utilities.Tests
{
    [TestClass]
    public class NullRecommendationEngineTests
    {
        [TestMethod]
        public void NullEngine_GetRecommendedComponent_ShouldAlwaysReturnNull()
        {
            var sut = new NullRecommendationEngine(null);
            var result = sut.GetRecommendedComponent(null, decimal.MinValue);

            Assert.IsNull(result);

            result = sut.GetRecommendedComponent(new Usage[] { Usage.Gaming }, 600.00M);

            Assert.IsNull(result);
        }
    }
}
