using API_Cadastro.Interfaces;
using API_Cadastro.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API_Cadastro.Service
{
    public class DataService : IDataService
    {
        private readonly ILogger logger;
        private readonly ApplicationContext context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IPaisRepository paisRepository;

        public DataService(ILogger<DataService> logger
                         , ApplicationContext context
                         , IHostingEnvironment hostingEnvironment
                         , IPaisRepository paisRepository
            )
        {
            this.logger = logger;
            this.hostingEnvironment = hostingEnvironment;
            this.context = context;
            this.paisRepository = paisRepository;
        }

        public void InicializaDB()
        {
            try
            {
                //context.Database.Migrate();

                IList<PaisModel> paises = GetListObject<PaisModel>("paises.json");

                Task<IEnumerable<PaisModel>> TaskPaisModels = this.paisRepository.GetListAsync();
                TaskPaisModels.Wait();
                IEnumerable<PaisModel> PaisModels = TaskPaisModels.Result;
                
                if (Equals(PaisModels, null) || Equals(PaisModels.Count(), 0))
                {
                    foreach (var item in paises)
                    {
                        this.paisRepository.Insert(item);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<T> GetListObject<T>(string arq)
        {
            try
            {
                string path = Path.Combine(this.hostingEnvironment.ContentRootPath, "Data");

                logger.LogInformation($"Obtendo arquivo {arq} no path {path} das lista a serem gravadas");
                var json = File.ReadAllText(Path.Combine(path, arq));

                logger.LogInformation("Deserializando Objeto");
                var list = JsonConvert.DeserializeObject<List<T>>(json);

                logger.LogInformation("Retornando objeto");
                return list;
            }
            catch (Exception ex)
            {
                logger.LogError($"Erro ao obter arquivo - {ex.Message}");
                throw ex;
            }
        }
    }
}
