using API_Cadastro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Cadastro.Interfaces
{
    public interface IPaisSingleton
    {
        Task<IEnumerable<PaisModel>> GetPaisAsync();
    }
}
