using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.Models
{
    [Serializable]
    public class Livros
    {
        public decimal liv_id_livro { get; set; }
        public decimal liv_id_tipo_livro { get; set; }
        public decimal liv_id_editor { get; set; }
        public string liv_nm_titulo { get; set; }
        public decimal liv_vl_preco { get; set; }
        public decimal liv_pc_royalty { get; set; }
        public string liv_ds_resumo { get; set; }
        public int liv_nu_edicao { get; set; }

        public Livros(decimal adcIdLivro, decimal asIdTipoLivro, decimal asIdEditor, string asNomeTitulo, decimal asValorPreco,
            decimal asRoyalty, string asDescricao, int asNumeroEdicao)
        {
            this.liv_id_livro = adcIdLivro;
            this.liv_id_tipo_livro = asIdTipoLivro;
            this.liv_id_editor = asIdEditor;
            this.liv_nm_titulo = asNomeTitulo;
            this.liv_vl_preco = asValorPreco;
            this.liv_pc_royalty = asRoyalty;
            this.liv_ds_resumo = asDescricao;
            this.liv_nu_edicao = asNumeroEdicao;
        }
    }
}