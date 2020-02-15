using API_Cadastro.Interfaces;
using API_Cadastro.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Cadastro.Repositories
{
    public class PaisRepository : BaseRepository<PaisModel>, IPaisRepository
    {
        public PaisRepository(ApplicationContext context, ILogger<PaisRepository> logger) : base(context, logger)
        {
        }
    }
}
