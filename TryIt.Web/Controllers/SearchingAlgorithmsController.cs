using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TryIt.Core.Enums;
using TryIt.Core.Interfaces;

namespace TryIt.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SearchingAlgorithmsController : ControllerBase
    {
        private readonly ISearchingAlgorithmsService _searchingAlgorithmsService;

        public SearchingAlgorithmsController(ISearchingAlgorithmsService searchingAlgorithmsService)
        {
            _searchingAlgorithmsService = searchingAlgorithmsService;
        }

        [HttpGet("{type}")]
        [SwaggerOperation(
            Summary = "Searching Array using one of the algorthims",
            Description = @"Searching Array using one of the algorthims, 
            algorithms : Linear = 0,Greedy = 1 ,BinarySearchTree = 2",
            OperationId = null,
            Tags = null)
        ]
        public IActionResult GetAll(SearchAlgorithmTypesEnum type)
        {
            var result = _searchingAlgorithmsService.Search(type);
            return Ok(result);
        }
    }
}
