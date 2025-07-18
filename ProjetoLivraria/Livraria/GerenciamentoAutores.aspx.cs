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
    public partial class GerenciamentoAutores : System.Web.UI.Page
    {
        AutoresDAO ioAutoresDAO = new AutoresDAO();
        public Autores AutoresSessao
        {
            get { return (Autores)Session["SessionAutorSelecionado"]; }
            set { Session["SessionAutorSelecionado"] = value; }
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

        protected void Page_Load(object sender, EventArgs e)
        {  
            CarregaDados();
        }

        private void CarregaDados()
        {
            try
            {
                this.ListaAutores = ioAutoresDAO.BuscaAutores();
                this.gvGerenciamentoAutores.DataSource = this.ListaAutores.OrderBy(loAutor => loAutor.aut_nm_nome);
                this.gvGerenciamentoAutores.DataBind();
            }
            catch   
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar Autores.');</script>");
            }
        }

        protected void BtnNovoAutor_Click(object sender, EventArgs e)
        {
            try
            {
                // Se a valid~ção da página não foi corretamente feita, o código não é executado.
                if (!Page.IsValid) return;

                //Buscando nos autores já cadastrados o que possui o maior ID, e assim incrementando mais 1, evitando que a PrimaryKey se repita.(Esse campo não é auto-increment no banco.)
                decimal ldcIdAutor = this.ListaAutores.OrderByDescending(a => a.aut_id_autor).First().aut_id_autor + 1;

                string lsNomeAutor = this.tbxCadastroNomeAutor.Text;
                string lsSobrenomeAutor = this.tbxCadastroSobrenomeAutor.Text;
                string lsEmailAutor = this.tbxCadastroEmailAutor.Text;

                Autores loAutor = new Autores(ldcIdAutor, lsNomeAutor, lsSobrenomeAutor, lsEmailAutor);

                this.ioAutoresDAO.InsereAutor(loAutor);

                HttpContext.Current.Response.Write("<script> alert('Autor cadastrado com sucesso!'); </script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script> alert('Erro no cadastrado do Autor!'); </script>");
            }
            this.tbxCadastroNomeAutor.Text = String.Empty;
            this.tbxCadastroSobrenomeAutor.Text = String.Empty;
            this.tbxCadastroEmailAutor.Text = String.Empty;
        }

        protected void gvGerenciamentoAutores_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                decimal autorId = Convert.ToDecimal(e.Keys["aut_id_autor"]);
                string nome = e.NewValues["aut_nm_nome"].ToString();
                string sobrenome = e.NewValues["aut_nm_sobrenome"].ToString();
                string email = e.NewValues["aut_ds_email"].ToString();

                if (string.IsNullOrEmpty(nome))
                {
                    HttpContext.Current.Response.Write("<script> alert('Informe o nome do autor.'); </script>");
                    return;
                } 
                else if (string.IsNullOrEmpty(sobrenome))
                {
                    HttpContext.Current.Response.Write("<script>alert('Informe o sobrenome do autor.');</script>");
                    return;
                }
                else if (string.IsNullOrEmpty(email))
                {
                    HttpContext.Current.Response.Write("<script>alert('Informe o email do autor.');</script>");
                    return;
                }
                Autores autor = new Autores(autorId, nome, sobrenome, email);
                
                int qtdLinhasAfetadas = ioAutoresDAO.AtualizaAutor(autor);

                e.Cancel = true;
                this.gvGerenciamentoAutores.CancelEdit();
                CarregaDados();
            }
            catch
            {
                e.Cancel = true;
                HttpContext.Current.Response.Write("<script>alert('Erro na atualização do autor.');</script>");
            }
        }

        protected void gvGerenciamentoAutores_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                decimal autorId = Convert.ToDecimal(e.Keys["aut_id_autor"]);
                Autores loAutor = this.ioAutoresDAO.BuscaAutores(autorId).FirstOrDefault();
                if (loAutor != null)
                {
                    LivrosDAO loLivrosDAO = new LivrosDAO();
                    if (loLivrosDAO.BuscarLivrosPorAutor(loAutor).Count != 0)
                    {
                        HttpContext.Current.Response.Write("<script>alert('Não é possível remover o autor selecionado pois existem livros associados a ele.');</script>");
                        e.Cancel = true;
                    }
                    else
                    {
                        this.ioAutoresDAO.RemoveAutor(loAutor);
                        e.Cancel = true;
                        CarregaDados();
                    }
                }
            }
            catch
            {
                e.Cancel = true;
                HttpContext.Current.Response.Write("<script>alert('Erro na remoção do autor selecionado.')</script>");
            }
        }

        protected void gvGerenciamentoAutores_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            decimal autorId = Convert.ToDecimal(gvGerenciamentoAutores.GetRowValues(e.VisibleIndex, "aut_id_autor"));
            var loAutor = ioAutoresDAO.BuscaAutores(autorId).FirstOrDefault();

            if (e.ButtonID == "btnAutorInfo")
            {

            }
            else if (e.ButtonID == "btnLivros")
            {
                Session["SessionAutorSelecionado"] = loAutor;

                gvGerenciamentoAutores.JSProperties["cpRedirectToLivros"] = true;
            }
        }

        private void RedirectLivros(String idAutorString, string controlID)
        {
            switch (controlID)
            {
                case "btnLivros":
                    decimal id = Convert.ToDecimal(idAutorString);
                    AutoresSessao = this.ioAutoresDAO.BuscaAutores(id).First();

                    Response.Redirect("/Livraria/GerenciamentoLivros.aspx");
                    break;
                default: break;
            }
        }
    }
}