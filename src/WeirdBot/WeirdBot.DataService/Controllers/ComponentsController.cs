using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using WeirdBot.Models;
using WeirdBot.Utilities;
using WeirdBot.DataAccess;
using Newtonsoft.Json;
using System.IO;

namespace WeirdBot.DataService.Controllers
{
    public class ComponentsController : ApiController
    {
        private IComponentRepository componentRepository;
        RecommendationFactory recommendationFactory;

        public ComponentsController(IComponentRepository repository, RecommendationFactory factory)
        {
            componentRepository = repository;
            recommendationFactory = factory;
        }

 
        // GET api/components
        [SwaggerOperation("GetAll")]
        public IEnumerable<Component> Get()
        {
            return componentRepository.GetAllComponents();
        }

        // GET api/category/3/components
        [SwaggerOperation("GetByCategory")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("api/category/{type}/components")]
        public IEnumerable<Component> GetByCategory(ComponentType type)
        {
            return componentRepository.GetAllComponents()
                .Where(p => p.Category == type);
        }

        // POST api/priceLimit/500/recommendation -> {"Usage": ["General","Gaming"]}
        [SwaggerOperation("GetRecommendation")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("api/priceLimit/{price}/recommendation")]
        [HttpPost()]
        public Recommendation GetRecommendation(decimal price, [FromBody]UsageParameter usage)
        {
            return recommendationFactory.GetRecommendation(usage.Category.ToArray(), price);
        }

        public class UsageParameter
        {
            public List<Usage> Category { get; set; }
        }

    }
}
