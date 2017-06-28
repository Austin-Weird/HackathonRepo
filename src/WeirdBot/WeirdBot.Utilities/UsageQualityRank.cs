using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeirdBot.Models;

namespace WeirdBot.Utilities
{
    public class UsageQualityRank
    {
        public static Quality GetRankOf(Usage item)
        {
            switch (item)
            {
                case Usage.Gaming:
                case Usage.Media:
                    return Quality.Best;
                case Usage.Programming:
                case Usage.Business:
                    return Quality.Better;
                case Usage.General:
                default:
                    return Quality.Good;
            }
        }

        public static Quality GetHighetstRankOf(Usage[] usage)
        {
            Quality highestRank = Quality.Good;
            foreach (var item in usage)
            {
                var thisItemRank = GetRankOf(item);
                highestRank = (int)thisItemRank > (int)highestRank ? thisItemRank : highestRank;
            }
            return highestRank;
        }
    }
}
