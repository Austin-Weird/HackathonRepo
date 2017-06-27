using WeirdBot.Models;

namespace WeirdBot.Utilities
{
    internal interface IComponentRecommendationEngine
    {
        Component GetRecommendedComponent(Usage[] usage, decimal lowPrice, decimal highPrice);
    }
}