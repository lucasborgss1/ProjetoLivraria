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
    public class TiposLivroDAO
    {
        SqlCommand ioQuery;
        SqlConnection ioConexao;

        public BindingList<TiposLivro> BuscaTiposLivro(decimal? adcTipoLivro = null)
        {
            BindingList<TiposLivro> loListTiposLivro = new BindingList<TiposLivro>();

            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    if (adcTipoLivro != null)
                    {
                        ioQuery = new SqlCommand("SELECT * FROM TIL_TIPO_LIVRO WHERE TIL_ID_TIPO_LIVRO = @idTipoLivro", ioConexao);
                        ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", adcTipoLivro));
                    }
                    else
                    {
                        ioQuery = new SqlCommand("SELECT * FROM TIL_TIPO_LIVRO", ioConexao);
                    }

                    using (SqlDataReader loReader = ioQuery.ExecuteReader())
                    {
                        while (loReader.Read())
                        {
                            TiposLivro loNovoTipoLivro = new TiposLivro(loReader.GetDecimal(0), loReader.GetString(1));
                            loListTiposLivro.Add(loNovoTipoLivro);
                        }
                        loReader.Close();
                    }
                }
                catch
                {
                    throw new Exception("Erro ao tentar buscar o(s) tipos(s) de livro(s).");
                }
            }
            return loListTiposLivro;
        }

        public int InsereTipoLivro(TiposLivro aoNovoTipoLivro)
        {
            if (aoNovoTipoLivro == null) throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("INSERT INTO TIL_TIPO_LIVRO(TIL_ID_TIPO_LIVRO, TIL_DS_DESCRICAO) VALUES (@idTipoLivro, @descricaoTipoLivro)", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoNovoTipoLivro.til_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@descricaoTipoLivro", aoNovoTipoLivro.til_ds_descricao));

                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar cadastrar novo tipo de livro.");
                }
            }
            return liQtdRegistrosInseridos;
        }

        public int RemoveTipoLivro(TiposLivro aoTipoLivro)
        {
            if (aoTipoLivro == null) throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("DELETE FROM TIL_TIPO_LIVRO WHERE TIL_ID_TIPO_LIVRO = @idTipoLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoTipoLivro.til_id_tipo_livro));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar excluir tipo de livro.");
                }
            }
            return liQtdRegistrosInseridos;
        }

        public int AtualizaTipoLivro(TiposLivro aoTipoLivro)
        {
            if (aoTipoLivro == null) throw new NullReferenceException();
            int liQtdRegistrosInseridos = 0;
            using (ioConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                try
                {
                    ioConexao.Open();
                    ioQuery = new SqlCommand("UPDATE TIL_TIPO_LIVRO SET TIL_DS_DESCRICAO = @descricaoTipoLivro WHERE TIL_ID_TIPO_LIVRO = @idTipoLivro", ioConexao);
                    ioQuery.Parameters.Add(new SqlParameter("@idTipoLivro", aoTipoLivro.til_id_tipo_livro));
                    ioQuery.Parameters.Add(new SqlParameter("@descricaoTipoLivro", aoTipoLivro.til_ds_descricao));
                    liQtdRegistrosInseridos = ioQuery.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception("Erro ao tentar atualizar informações do tipo de livro.");
                }
            }
            return liQtdRegistrosInseridos;
        }

    }
}