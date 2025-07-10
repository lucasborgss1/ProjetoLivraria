using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.Models
{
    [Serializable]
    public class TiposLivro
    {
        public decimal til_id_tipo_livro { get; set; }
        public string til_ds_descricao { get; set; }    

        public TiposLivro(decimal adcIdTipoLivro, string asDescricao)
        {
            this.til_id_tipo_livro = adcIdTipoLivro;
            this.til_ds_descricao = asDescricao;
        }
    }
}