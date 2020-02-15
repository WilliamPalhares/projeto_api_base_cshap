using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_Cadastro.Models
{
    [Table("Pais")]
    public class PaisModel : BaseModel
    {
        public PaisModel()
        {
            Clientes = new HashSet<ClienteModel>();
        }
        
        [Column("Descricao")]
        [MaxLength(150, ErrorMessage = "Nome do Pais superior a 150 caracteres")]
        [Required(ErrorMessage = "Nome do Pais é obrigatório")]
        [JsonProperty(PropertyName = "descricao")]
        public string Descricao { get; set; }

        [JsonProperty(PropertyName = "clientes")]
        public virtual ICollection<ClienteModel> Clientes { get; set; }
    }
}
