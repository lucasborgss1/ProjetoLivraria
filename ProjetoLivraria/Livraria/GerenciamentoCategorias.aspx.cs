using DevExpress.Web.Data;
using ProjetoLivraria.DAO;
using ProjetoLivraria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoLivraria.Livraria
{
    public partial class GerenciamentoCategorias : System.Web.UI.Page
    {
        TiposLivroDAO ioTiposLivro = new TiposLivroDAO();

        public TiposLivro TiposLivroSessao
        {
            get { return (TiposLivro)Session["SessionTiposLivroSelecionado"]; }
            set { Session["SessionTiposLivroSelecionado"] = value; }
        }
            
        public BindingList<TiposLivro> ListaTiposLivro
        {
            get
            {
                if ((BindingList<TiposLivro>)ViewState["ViewStateListaTiposLivro"] == null) 
                    this.CarregaDados();
                return (BindingList<TiposLivro>)ViewState["ViewStateListaTiposLivro"];
            }

            set
            {
                ViewState["ViewStateListaTiposLivro"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CarregaDados();
        }

        private void CarregaDados()
        {
            try
            {
                this.ListaTiposLivro = ioTiposLivro.BuscaTiposLivro();
                this.gvGerenciamentoTiposLivro.DataSource = this.ListaTiposLivro.OrderBy(loTiposLivro => loTiposLivro.til_ds_descricao);
                this.gvGerenciamentoTiposLivro.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar categorias.')</script>");
            }
        }

        protected void BtnNovoTipoLivro_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid) return;

                decimal ldcIdTipoLivro = this.ListaTiposLivro.OrderByDescending(t => t.til_id_tipo_livro).First().til_id_tipo_livro + 1;
                string lsDescricaoTipoLivro = this.tbxDescricaoTipoLivro.Text;

                TiposLivro loTipoLivro = new TiposLivro(ldcIdTipoLivro, lsDescricaoTipoLivro);

                this.ioTiposLivro.InsereTipoLivro(loTipoLivro);

                HttpContext.Current.Response.Write("<script> alert('Categoria cadastrada com sucesso!'); </script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script> alert('Erro no cadastrado da categoria!'); </script>");
            }
            this.tbxDescricaoTipoLivro.Text = String.Empty;
        }


        protected void gvGerenciamentoTiposLivro_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                decimal tiposLivroId = Convert.ToDecimal(e.Keys["til_id_tipo_livro"]);
                string descricao = e.NewValues["til_ds_descricao"].ToString();

                if (string.IsNullOrEmpty(descricao))
                {
                    HttpContext.Current.Response.Write("<script> alert('Informe a descricao da categoria.'); </script>");
                    return;
                }

                TiposLivro tiposLivro = new TiposLivro(tiposLivroId, descricao);

                int qtdLinhasAfetadas = ioTiposLivro.AtualizaTipoLivro(tiposLivro);

                e.Cancel = true;
                this.gvGerenciamentoTiposLivro.CancelEdit();
                CarregaDados();
            }
            catch
            {
                e.Cancel = true;
                HttpContext.Current.Response.Write("<script>alert('Erro na atualização da categoria.');</script>");
            }
        }

        protected void gvGerenciamentoTiposLivro_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                decimal tiposLivroId = Convert.ToDecimal(e.Keys["til_id_tipo_livro"]);
                TiposLivro loTiposLivro = this.ioTiposLivro.BuscaTiposLivro(tiposLivroId).FirstOrDefault();

                if (loTiposLivro != null)
                {
                    LivrosDAO loLivrosDAO = new LivrosDAO();
                    if (loLivrosDAO.BuscarLivrosPorCategoria(loTiposLivro).Count != 0)
                    {
                        HttpContext.Current.Response.Write("<script>alert('Não é possível remover a categoria selecionada pois existem livros associados a ela.');</script>");
                        e.Cancel = true;
                    }
                    else
                    {
                        this.ioTiposLivro.RemoveTipoLivro(loTiposLivro);
                        e.Cancel = true;
                        CarregaDados();
                    }
                }
            }
            catch
            {
                e.Cancel = true;
                HttpContext.Current.Response.Write("<script>alert('Erro na remoção da categoria selecionada.')</script>");
            }
        }

        protected void gvGerenciamentoTiposLivro_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            decimal tiposLivroId = Convert.ToDecimal(gvGerenciamentoTiposLivro.GetRowValues(e.VisibleIndex, "til_id_tipo_livro"));
            var loTiposLivro = ioTiposLivro.BuscaTiposLivro(tiposLivroId).FirstOrDefault();

            if (e.ButtonID == "btnLivros")
            {
                Session["SessionTiposLivroSelecionado"] = loTiposLivro;

                gvGerenciamentoTiposLivro.JSProperties["cpRedirectToLivros"] = true;
            }
        }

        private void RedirectLivros(String idTiposLivroString, string controlID)
        {
            switch (controlID)
            {
                case "btnLivros":
                    decimal id = Convert.ToDecimal(idTiposLivroString);
                    TiposLivroSessao = this.ioTiposLivro.BuscaTiposLivro(id).First();

                    Response.Redirect("/Livraria/GerenciamentoLivros.aspx");
                    break;
                default: break;
            }
        }
    }
}