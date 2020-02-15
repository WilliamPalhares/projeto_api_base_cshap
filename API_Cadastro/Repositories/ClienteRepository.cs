using API_Cadastro.Interfaces;
using API_Cadastro.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Cadastro.Repositories
{
    public class ClienteRepository : BaseRepository<ClienteModel>, IClienteRepository
    {
        public ClienteRepository(ApplicationContext context, ILogger<ClienteRepository> logger) : base(context, logger)
        {
        }
    }
}
