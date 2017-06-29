using System.Collections.Generic;
using WeirdBot.Models;

namespace WeirdBot.DataAccess
{
    public interface IComponentRepository
    {
        List<Component> GetAllComponents();
        Component GetComponentByPriceAndQuality(ComponentType componentType, Quality powerRank, decimal priceTarget);
    }
}