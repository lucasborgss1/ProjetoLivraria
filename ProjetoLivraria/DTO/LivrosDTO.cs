using System;

namespace ProjetoLivraria.DTO
{
    [Serializable]
    public class LivrosDTO
    {
        public decimal liv_id_livro { get; set; }
        public decimal til_id_tipo_livro { get; set; }
        public decimal edi_id_editor { get; set; }
        public string liv_nm_titulo { get; set; }
        public decimal liv_vl_preco { get; set; }
        public decimal liv_pc_royalty { get; set; }
        public string liv_ds_resumo { get; set; }
        public int liv_nu_edicao { get; set; }
        public decimal aut_id_autor { get; set; }
        public string aut_nm_nome { get; set; }
        public string aut_nm_sobrenome { get; set; }
        public string aut_nome_completo
        {
            get { return $"{aut_nm_nome} {aut_nm_sobrenome}"; }
        }

        public string edi_nm_editor { get; set; }

        public string til_ds_descricao { get; set; }

        public LivrosDTO(decimal idLivro, decimal idTipoLivro, decimal idEditor, string nomeTitulo, decimal valorPreco,
            decimal royalty, string descricao, int numeroEdicao, decimal idAutor, string nomeAutor, string sobrenomeAutor, string nomeEditor, string descCategoria)
        {
            this.liv_id_livro = idLivro;
            this.til_id_tipo_livro = idTipoLivro;
            this.edi_id_editor = idEditor;
            this.liv_nm_titulo = nomeTitulo;
            this.liv_vl_preco = valorPreco;
            this.liv_pc_royalty = royalty;
            this.liv_ds_resumo = descricao;
            this.liv_nu_edicao = numeroEdicao;
            this.aut_id_autor = idAutor;
            this.aut_nm_nome = nomeAutor;
            this.aut_nm_sobrenome = sobrenomeAutor;
            this.edi_nm_editor = nomeEditor;
            this.til_ds_descricao = descCategoria;
        }
    }
}
