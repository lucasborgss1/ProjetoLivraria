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
    public class LivrosEAutoresDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<LivrosEAutores> BuscaLivrosEAutores(decimal? adcIdLivro, decimal? adcIdAutor)
        {
            BindingList<LivrosEAutores> loListLivrosEAutores = new BindingList<LivrosEAutores>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (adcIdLivro != null && adcIdAutor != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIA_LIVRO_AUTOR WHERE LIA_ID_AUTOR = @idAutor AND LIA_ID_LIVRO = @idLivro", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idAutor", adcIdAutor));
                        ioQuery.Parameters.Add(new SqlParameter("@idLivro", adcIdLivro));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM LIA_LIVRO_AUTOR", ioConexao);
                    } 

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            LivrosEAutores loNovoLivroEAutor = new LivrosEAutores(loReader.GetDecimal(0), loReader.GetDecimal(1), loReader.GetDecimal(2));
                            loListLivrosEAutores.Add(loNovoLivroEAutor);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar o(s) registro(s) de livro(s) e autor(es).");
                }
            }
            return loListLivrosEAutores;
        }

        public int InsereLivroEAutores(LivrosEAutores aoNovoLivroEAutor)
        {
            if (aoNovoLivroEAutor == null) throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO LIA_LIVRO_AUTOR(LIA_ID_AUTOR, LIA_ID_LIVRO, LIA_PC_ROYALTY)" +
                        " VALUES (@idAutor, @idLivro, @royalty)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoNovoLivroEAutor.lia_id_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoNovoLivroEAutor.lia_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@royalty", aoNovoLivroEAutor.lia_pc_royalty));

                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar cadastrar registro de livro e autor.");
                }
            }
            return liQtdRegistrosInseridos;
        }

        public int RemoveLivroEAutor(decimal asIdAutor, decimal asIdLivro)
        {
            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM LIA_LIVRO_AUTOR WHERE LIA_ID_AUTOR = @idAutor AND LIA_ID_LIVRO = @idLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", asIdAutor));
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", asIdLivro));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao tentar excluir registro de livro e autor.", ex);
                }
            }
            return liQtdRegistrosInseridos;
        }

        public int AtualizaLivroEAutor(LivrosEAutores aoLivroEAutor)
        {
            if (aoLivroEAutor == null) throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("UPDATE LIA_LIVRO_AUTOR SET LIA_ID_LIVRO = @idlivro,LIA_ID_AUTOR = @idAutor,LIA_PC_ROYALTY = @royalty WHERE LIA_ID_LIVRO = @idLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idAutor", aoLivroEAutor.lia_id_autor));
                    ioQuery.Parameters.Add(new SqlParameter("@idLivro", aoLivroEAutor.lia_id_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@royalty", aoLivroEAutor.lia_pc_royalty));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar atualizar informações do registro de livro e autor.");
                }
            }
            return liQtdRegistrosInseridos;
        }
    }
}