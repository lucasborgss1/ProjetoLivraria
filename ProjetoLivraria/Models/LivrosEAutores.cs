using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.Models
{
    [Serializable]
    public class LivrosEAutores
    {
        public decimal lia_id_autor { get; set; }
        public decimal lia_id_livro { get; set; }
        public decimal lia_pc_royalty { get; set; }

        public LivrosEAutores(decimal asIdAutor, decimal asIdLivro, decimal asRoyalty)
        {
            this.lia_id_autor = asIdAutor;
            this.lia_id_livro = asIdLivro;
            this.lia_pc_royalty = asRoyalty;
        }
    }
}