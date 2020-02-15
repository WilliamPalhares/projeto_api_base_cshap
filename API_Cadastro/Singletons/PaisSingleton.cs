using API_Cadastro.Interfaces;
using API_Cadastro.Models;
using API_Cadastro.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Cadastro.Singletons
{
    public class PaisSingleton : IPaisSingleton
    {
        private readonly ILogger logger;
        private readonly IPaisRepository paisRepository;

        public PaisSingleton(ILogger<PaisSingleton> logger, IPaisRepository paisRepository)
        {
            this.logger = logger;
            this.paisRepository = paisRepository;
        }

        public async Task<IEnumerable<PaisModel>> GetPaisAsync()
        {
            try
            {
                return await paisRepository.GetListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
