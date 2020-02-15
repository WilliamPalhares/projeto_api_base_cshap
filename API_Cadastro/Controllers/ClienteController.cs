using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_Cadastro.Interfaces;
using API_Cadastro.Models;
using API_Cadastro.ViewModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_Cadastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigins")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IClienteRepository clienteRepository;
        private readonly IPaisRepository paisRepository;

        public ClienteController(ILogger<ClienteController> logger, IClienteRepository clienteRepository, IPaisRepository paisRepository)
        {
            this.logger = logger;
            this.clienteRepository = clienteRepository;
            this.paisRepository = paisRepository;
        }

        // GET api/cliente
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string page)
        {
            try
            {
                var list = await clienteRepository.GetListAsync();

                foreach (var item in list)
                {
                    item.Pais = await this.paisRepository.GetObjectAsync(item.PaisId);
                }

                int count = list.Count();
                int CurrentPage = Convert.ToInt32(page);
                int PageSize = 1;
                int TotalCount = count;
                int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

                var items = list.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

                var paginationMetadata = new
                {
                    docs = items,
                    total = count,
                    limit = PageSize,
                    page = CurrentPage,
                    pages = TotalPages
                };

                return Ok(paginationMetadata);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }

        // GET api/cliente/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await clienteRepository.GetObjectAsync(id));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }

        // POST api/cliente
        [HttpPost]
        public async Task<IActionResult> Post(ClienteModel model)
        {
            try
            {
                return Ok(await clienteRepository.InsertAsync(model));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }

        // PUT api/cliente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ClienteModel model)
        {
            try
            {
                await clienteRepository.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }

        // DELETE api/cliente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                ClienteModel model = await clienteRepository.GetObjectAsync(id);
                await clienteRepository.RemoveAsync(model);

                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex);
            }
        }
    }
}
