using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API_Cadastro.Models
{
    [Table("Cliente")]
    public class ClienteModel : BaseModel
    {
        [Column("Nome")]
        [MaxLength(100, ErrorMessage = "Nome do cliente superior a 100 caracteres")]
        [Required(ErrorMessage = "Nome do cliente é obrigatório")]
        [JsonProperty(PropertyName = "nome")]
        public String Nome { get; set; }

        [Column("PaisId")]
        [Required(ErrorMessage = "A Pais é obrigatório.")]
        [JsonProperty(PropertyName = "paisId")]
        public Int64 PaisId { get; set; }

        [JsonProperty(PropertyName = "pais")]
        public virtual PaisModel Pais { get; set; }
    }
}
