using System.Collections.Generic;
using WeirdBot.Models;

namespace WeirdBot.DataAccess
{
    public interface IComponentRepository
    {
        List<Component> GetAllProducts();
        Component GetComponentByPriceAndPowerRank(ComponentType componentType, Quality powerRank, decimal priceTarget);
    }
}