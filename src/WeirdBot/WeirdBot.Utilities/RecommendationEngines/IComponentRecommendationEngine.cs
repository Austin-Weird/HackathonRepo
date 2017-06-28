using WeirdBot.Models;

namespace WeirdBot.Utilities
{
    public interface IComponentRecommendationEngine
    {
        Component GetRecommendedComponent(Usage[] usage, decimal highPrice);
    }
}