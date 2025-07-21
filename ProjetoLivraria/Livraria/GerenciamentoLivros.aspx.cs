using DevExpress.Web;
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
    public partial class GerenciamentoLivros : System.Web.UI.Page
    {
        LivrosDAO ioLivrosDAO = new LivrosDAO();
        AutoresDAO ioAutoresDAO = new AutoresDAO();
        TiposLivroDAO ioTiposLivroDAO = new TiposLivroDAO();
        EditoresDAO ioEditoresDAO = new EditoresDAO();
        LivrosEAutoresDAO ioLivrosEAutoresDAO = new LivrosEAutoresDAO();

        public BindingList<Livros> ListaLivros
        {
            get
            {
                if ((BindingList<Livros>)ViewState["ViewStateListaLivros"] == null)
                    CarregaDados();
                return (BindingList<Livros>)ViewState["ViewStateListaLivros"];
            }

            set
            {
                ViewState["ViewStateListaLivros"] = value;
            }
        }

        public BindingList<Autores> ListaAutores
        {
            get
            {
                if ((BindingList<Autores>)ViewState["ViewStateListaAutores"] == null)
                    CarregaDados();
                return (BindingList<Autores>)ViewState["ViewStateListaAutores"];
            }

            set
            {
                ViewState["ViewStateListaAutores"] = value;
            }
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

        public BindingList<Editores> ListaEditores
        {
            get
            {
                if ((BindingList<Editores>)ViewState["ViewStateListaEditores"] == null)
                    this.CarregaDados();
                return (BindingList<Editores>)ViewState["ViewStateListaEditores"];
            }

            set
            {
                ViewState["ViewStateListaEditores"] = value;
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
                this.ListaLivros = ioLivrosDAO.BuscaLivros();
                this.gvGerenciamentoLivros.DataSource = this.ListaLivros.OrderBy(loLivro => loLivro.liv_nm_titulo);
                this.gvGerenciamentoLivros.DataBind();

                this.ListaTiposLivro = ioTiposLivroDAO.BuscaTiposLivro();
                this.cbCadastroCategoria.DataSource = this.ListaTiposLivro.OrderBy(loTipoLivro => loTipoLivro.til_ds_descricao);
                this.cbCadastroCategoria.TextField = "til_ds_descricao";
                this.cbCadastroCategoria.ValueField = "til_id_tipo_livro";
                this.cbCadastroCategoria.DataBind();


                this.ListaAutores = ioAutoresDAO.BuscaAutores();
                this.cbCadastroAutorLivro.DataSource = this.ListaAutores.OrderBy(loAutor => loAutor.aut_nome_completo);
                this.cbCadastroAutorLivro.TextField = "aut_nome_completo";
                this.cbCadastroAutorLivro.ValueField = "aut_id_autor";
                this.cbCadastroAutorLivro.DataBind();

                this.ListaEditores = ioEditoresDAO.BuscaEditores();
                this.cbCadastroEditorLivro.DataSource = this.ListaEditores.OrderBy(loEditor => loEditor.edi_nm_editor);
                this.cbCadastroEditorLivro.TextField = "edi_nm_editor";
                this.cbCadastroEditorLivro.ValueField = "edi_id_editor";
                this.cbCadastroEditorLivro.DataBind();
            }
            catch (Exception e)
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar dados.')</script>" + e);
            }
        }

        





        protected void BtnNovoLivro_Click(object sender, EventArgs e)
        {
            try
            {

                if (!Page.IsValid) return;
                decimal ldcIdLivro = this.ListaLivros.OrderByDescending(l => l.liv_id_livro).First().liv_id_livro + 1;
                decimal lsIdCategoria = Convert.ToDecimal(this.cbCadastroCategoria.Value);
                decimal lsIdEditor = Convert.ToDecimal(this.cbCadastroEditorLivro.Value);
                string lsTitulo = this.tbxCadastroTituloLivro.Text;
                decimal lsPreco = Convert.ToDecimal(this.seCasdastroPrecoLivro.Text);
                decimal lsRoyalty = Convert.ToDecimal(this.seCadastroRoyaltyLivro.Text);
                string lsResumo = this.tbxCadastroResumoLivro.Text;
                int lsEdicao = Convert.ToInt32(this.seCadastroEdicaoLivro.Text);
                decimal ldcIdAutor = Convert.ToDecimal(this.cbCadastroAutorLivro.Value);


                Livros loLivro = new Livros(ldcIdLivro, lsIdCategoria, lsIdEditor, lsTitulo, lsPreco, lsRoyalty, lsResumo, lsEdicao);
                this.ioLivrosDAO.InsereLivro(loLivro);

                LivrosEAutores loLeA = new LivrosEAutores(ldcIdAutor, ldcIdLivro, lsRoyalty);
                this.ioLivrosEAutoresDAO.InsereLivroEAutores(loLeA);

                HttpContext.Current.Response.Write("<script> alert('Livro cadastrado com sucesso!'); </script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script> alert('Erro no cadastrado do Autor!'); </script>");
            }

            LimparFormulario();
        }

        protected void gvGerenciamentoLivros_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            //try
            //{
            //    decimal autorId = Convert.ToDecimal(e.Keys["aut_id_autor"]);
            //    string nome = e.NewValues["aut_nm_nome"].ToString();
            //    string sobrenome = e.NewValues["aut_nm_sobrenome"].ToString();
            //    string email = e.NewValues["aut_ds_email"].ToString();

            //    if (string.IsNullOrEmpty(nome))
            //    {
            //        HttpContext.Current.Response.Write("<script> alert('Informe o nome do autor.'); </script>");
            //        return;
            //    }
            //    else if (string.IsNullOrEmpty(sobrenome))
            //    {
            //        HttpContext.Current.Response.Write("<script>alert('Informe o sobrenome do autor.');</script>");
            //        return;
            //    }
            //    else if (string.IsNullOrEmpty(email))
            //    {
            //        HttpContext.Current.Response.Write("<script>alert('Informe o email do autor.');</script>");
            //        return;
            //    }
            //    Autores autor = new Autores(autorId, nome, sobrenome, email);

            //    int qtdLinhasAfetadas = ioAutoresDAO.AtualizaAutor(autor);

            //    e.Cancel = true;
            //    this.gvGerenciamentoAutores.CancelEdit();
            //    CarregaDados();
            //}
            //catch
            //{
            //    e.Cancel = true;
            //    HttpContext.Current.Response.Write("<script>alert('Erro na atualização do autor.');</script>");
            //}
        }

        protected void gvGerenciamentoLivros_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            //try
            //{
            //    decimal autorId = Convert.ToDecimal(e.Keys["aut_id_autor"]);
            //    Autores loAutor = this.ioAutoresDAO.BuscaAutores(autorId).FirstOrDefault();
            //    if (loAutor != null)
            //    {
            //        LivrosDAO loLivrosDAO = new LivrosDAO();
            //        if (loLivrosDAO.BuscarLivrosPorAutor(loAutor).Count != 0)
            //        {
            //            HttpContext.Current.Response.Write("<script>alert('Não é possível remover o autor selecionado pois existem livros associados a ele.');</script>");
            //            e.Cancel = true;
            //        }
            //        else
            //        {
            //            this.ioAutoresDAO.RemoveAutor(loAutor);
            //            e.Cancel = true;
            //            CarregaDados();
            //        }
            //    }
            //}
            //catch
            //{
            //    e.Cancel = true;
            //    HttpContext.Current.Response.Write("<script>alert('Erro na remoção do autor selecionado.')</script>");
            //}
        }

        private void LimparFormulario()
        {
            this.cbCadastroCategoria.SelectedIndex = 0;
            this.cbCadastroAutorLivro.SelectedIndex = 0;
            this.cbCadastroEditorLivro.SelectedIndex = 0;
            this.tbxCadastroTituloLivro.Text = String.Empty;
            this.seCasdastroPrecoLivro.Value = null;
            this.seCadastroRoyaltyLivro.Value = null;
            this.tbxCadastroResumoLivro.Text = String.Empty;
            this.seCadastroEdicaoLivro.Value = null;
        }

    }
}