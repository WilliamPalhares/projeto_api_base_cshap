using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Cadastro.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_Cadastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigins")]
    public class PaisController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IPaisSingleton paisSingleton;

        public PaisController(ILogger<PaisController> logger, IPaisSingleton paisSingleton)
        {
            this.logger = logger;
            this.paisSingleton = paisSingleton;
        }

        [HttpGet]
        [ResponseCache(Duration = 3600)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await this.paisSingleton.GetPaisAsync());
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }
    }
}