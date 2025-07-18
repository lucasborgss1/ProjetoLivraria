using ProjetoLivraria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.DAO
{
    public class LivrosDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;
        public BindingList<Livros> BuscaLivros(decimal? adcIdLivro = null)
        {
            BindingList<Livros> loListLivros = new BindingList<Livros>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (adcIdLivro != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS WHERE LIV_ID_LIVRO = @idLivro", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idLivro", adcIdLivro));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS", ioConexao);
                    }

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livros loNovoLivro = new Livros(loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetDecimal(2), loReader.GetString(3), loReader.GetDecimal(4), loReader.GetDecimal(5), loReader.GetString(6), loReader.GetInt32(7));
                            loListLivros.Add(loNovoLivro);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar o(s) livro(s).");
                }
            }
            return loListLivros;
        }

        public BindingList<Livros> BuscarLivrosPorAutor(Autores adcAutor)
        {
            BindingList<Livros> loListLivros = new BindingList<Livros>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS liv INNER JOIN LIA_LIVRO_AUTOR lia ON liv.LIV_ID_LIVRO = lia.LIA_ID_LIVRO WHERE lia.LIA_ID_AUTOR = @idAutor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", adcAutor.aut_id_autor));

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livros loNovoLivro = new Livros(loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetDecimal(2), loReader.GetString(3), loReader.GetDecimal(4), loReader.GetDecimal(5), loReader.GetString(6), loReader.GetInt32(7));
                            loListLivros.Add(loNovoLivro);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar o(s) livro(s).");
                }
            }
            return loListLivros;
        }

        public BindingList<Livros> BuscarLivrosPorEditor(Editores adcEditor)
        {
            BindingList<Livros> loListLivros = new BindingList<Livros>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS  WHERE LIV_ID_EDITOR = @idEditor", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", adcEditor.edi_id_editor));

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livros loNovoLivro = new Livros(loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetDecimal(2), loReader.GetString(3), loReader.GetDecimal(4), loReader.GetDecimal(5), loReader.GetString(6), loReader.GetInt32(7));
                            loListLivros.Add(loNovoLivro);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar o(s) livro(s).");
                }
            }
            return loListLivros;
        }

        public BindingList<Livros> BuscarLivrosPorCategoria(TiposLivro adcTipoLivro)
        {
            BindingList<Livros> loListLivros = new BindingList<Livros>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("SELECT * FROM LIV_LIVROS WHERE LIV_ID_TIPO_LIVRO = @idTipoLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", adcTipoLivro.til_id_tipo_livro));

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            Livros loNovoLivro = new Livros(loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetDecimal(2), loReader.GetString(3), loReader.GetDecimal(4), loReader.GetDecimal(5), loReader.GetString(6), loReader.GetInt32(7));
                            loListLivros.Add(loNovoLivro);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar o(s) livro(s).");
                }
            }
            return loListLivros;
        }

        public int InsereLivro(Livros aoNovoLivro)
        {
            if (aoNovoLivro == null) throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO LIV_LIVROS(LIV_ID_LIVRO, LIV_ID_TIPO_LIVRO, LIV_ID_EDITOR, LIV_NM_TITULO, LIV_VL_PRECO, LIV_PC_ROYALTY, LIV_DS_RESUMO, LIV_NU_EDICAO)" +
                        " VALUES (@idLivro, @idTipoLivro, @idEditor, @nomeTitulo, @valorPreco, @royalty, @resumo, @numeroEdicao)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoNovoLivro.liv_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoNovoLivro.liv_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoNovoLivro.liv_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeTitulo", aoNovoLivro.liv_nm_titulo));
                    ioQuery.Parameters.Add(new SqlParameter("@valorPreco", aoNovoLivro.liv_vl_preco));
                    ioQuery.Parameters.Add(new SqlParameter("@royalty", aoNovoLivro.liv_pc_royalty));
                    ioQuery.Parameters.Add(new SqlParameter("@resumo", aoNovoLivro.liv_ds_resumo));
                    ioQuery.Parameters.Add(new SqlParameter("@numeroEdicao", aoNovoLivro.liv_nu_edicao));

                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar cadastrar novo livro.");
                }
            }
            return liQtdRegistrosInseridos;
        }

        public int RemoveLivro(Livros aoLivro)
        {
            if (aoLivro == null) throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM LIV_LIVROS WHERE LIV_ID_LIVRO = @idLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoLivro.liv_id_livro));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar excluir livro.");
                }
            }
            return liQtdRegistrosInseridos;
        }

        public int AtualizaLivro(Livros aoLivro)
        {
            if (aoLivro == null) throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("UPDATE LIV_LIVROS SET LIV_ID_TIPO_LIVRO = @idTipoLivro, LIV_ID_EDITOR = @idEditor, LIV_NM_TITULO = @nomeTitulo, " +
                        "LIV_VL_PRECO = @valorPreco, LIV_PC_ROYALTY = @royalty, LIV_DS_RESUMO = @resumo, LIV_NU_EDICAO = @numeroEdicao WHERE LIV_ID_LIVRO = @idLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoLivro.liv_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoLivro.liv_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@idEditor", aoLivro.liv_id_editor));
                    ioQuery.Parameters.Add(new SqlParameter("@nomeTitulo", aoLivro.liv_nm_titulo));
                    ioQuery.Parameters.Add(new SqlParameter("@valorPreco", aoLivro.liv_vl_preco));
                    ioQuery.Parameters.Add(new SqlParameter("@royalty", aoLivro.liv_pc_royalty));
                    ioQuery.Parameters.Add(new SqlParameter("@resumo", aoLivro.liv_ds_resumo));
                    ioQuery.Parameters.Add(new SqlParameter("@numeroEdicao", aoLivro.liv_nu_edicao));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar atualizar informações do livro.");
                }
            }
            return liQtdRegistrosInseridos;
        }

       
    }
}