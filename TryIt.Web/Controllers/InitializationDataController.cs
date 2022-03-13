using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TryIt.Core.Enums;
using TryIt.Core.Interfaces;
using TryIt.SharedKernel.Authorization;

namespace TryIt.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InitializationDataController : ControllerBase
    {
        private readonly IInitializationDataService _initializationDataService;

        public InitializationDataController(IInitializationDataService initializationDataService)
        {   
            _initializationDataService = initializationDataService;
        }

        [AllowAnonymous]
        [HttpGet("init_Array")]
        public ActionResult<string> Init_Array()
        {
            string result = _initializationDataService.InitDataStrucutre(DataStructuresTypesEnum.Array);
            return Ok(result);
        }


        [HttpGet("{type}")]
        [SwaggerOperation(
            Summary = "Initiating a new data structure",
            Description = @"Initiating a new data structure, 
            types : Array = 0,List = 1,HashSet = 2,Dictionary = 3,HashTable = 4 ",
            OperationId = "InitializationData.Init",
            Tags =null)
        ]
        public ActionResult<string> Init(DataStructuresTypesEnum type)
        {
            string result = _initializationDataService.InitDataStrucutre(type);
            return Ok(result);
        }

    }
}
