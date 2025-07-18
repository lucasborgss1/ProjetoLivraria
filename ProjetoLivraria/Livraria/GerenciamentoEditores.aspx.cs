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
    public partial class GerenciamentoEditores : System.Web.UI.Page
    {
        EditoresDAO ioEditoresDAO = new EditoresDAO();

        public Editores EditoresSessao
        {
            get { return (Editores)Session["SessionEditorSelecionado"]; }
            set { Session["SessionEditorSelecionado"] = value; }
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
                this.ListaEditores = ioEditoresDAO.BuscaEditores();
                this.gvGerenciamentoEditores.DataSource = this.ListaEditores.OrderBy(loEditor => loEditor.edi_nm_editor);
                this.gvGerenciamentoEditores.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar Editores.')</script>");
            }
        }

        protected void BtnNovoEditor_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsValid) return;

                decimal ldcIdEditor = this.ListaEditores.OrderByDescending(ed => ed.edi_id_editor).First().edi_id_editor + 1;

                string lsNomeEditor = this.tbxCadastroNomeEditor.Text;
                string lsEmailEditor = this.tbxCadastroEmailEditor.Text;
                string lsUrlEditor = this.tbxCadastroUrlEditor.Text;

                Editores loEditor = new Editores(ldcIdEditor, lsNomeEditor, lsEmailEditor, lsUrlEditor);

                this.ioEditoresDAO.InsereEditor(loEditor);

                HttpContext.Current.Response.Write("<script> alert('Editor cadastrado com sucesso!'); </script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script> alert('Erro no cadastrado do Editor!'); </script>");
            }
            this.tbxCadastroNomeEditor.Text = String.Empty;
            this.tbxCadastroEmailEditor.Text = String.Empty;
            this.tbxCadastroUrlEditor.Text = String.Empty;
        }


        protected void gvGerenciamentoEditores_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            try
            {
                decimal editorId = Convert.ToDecimal(e.Keys["edi_id_editor"]);
                string nome = e.NewValues["edi_nm_editor"].ToString();
                string email = e.NewValues["edi_ds_email"].ToString();
                string url = e.NewValues["edi_ds_url"].ToString();

                if (string.IsNullOrEmpty(nome))
                {
                    HttpContext.Current.Response.Write("<script> alert('Informe o nome do editor.'); </script>");
                    return;
                }
                else if (string.IsNullOrEmpty(email))
                {
                    HttpContext.Current.Response.Write("<script>alert('Informe o email do editor.');</script>");
                    return;
                }
                else if (string.IsNullOrEmpty(url))
                {
                    HttpContext.Current.Response.Write("<script>alert('Informe a URL do editor.');</script>");
                    return;
                }
                Editores editor = new Editores(editorId, nome, email, url);

                int qtdLinhasAfetadas = ioEditoresDAO.AtualizaEditor(editor);

                e.Cancel = true;
                this.gvGerenciamentoEditores.CancelEdit();
                CarregaDados();
            }
            catch
            {
                e.Cancel = true;
                HttpContext.Current.Response.Write("<script>alert('Erro na atualização do editor.');</script>");
            }
        }

        protected void gvGerenciamentoEditores_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                decimal editorId = Convert.ToDecimal(e.Keys["edi_id_editor"]);
                Editores loEditor = this.ioEditoresDAO.BuscaEditores(editorId).FirstOrDefault();

                if(loEditor != null)
                {
                    LivrosDAO loLivrosDAO = new LivrosDAO();
                    if(loLivrosDAO.BuscarLivrosPorEditor(loEditor).Count != 0)
                    {
                        HttpContext.Current.Response.Write("<script>alert('Não é possível remover o editor selecionado pois existem livros associados a ele.');</script>");
                        e.Cancel = true;
                    }
                    else
                    {
                        this.ioEditoresDAO.RemoveEditor(loEditor);
                        e.Cancel = true;
                        CarregaDados();
                    }
                }
            }
            catch
            {
                e.Cancel = true;
                HttpContext.Current.Response.Write("<script>alert('Erro na remoção do editor selecionado.')</script>");
            }
        }

        protected void gvGerenciamentoEditores_CustomButtonCallback(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            decimal editorId = Convert.ToDecimal(gvGerenciamentoEditores.GetRowValues(e.VisibleIndex, "edi_id_editor"));
            var loEditor = ioEditoresDAO.BuscaEditores(editorId).FirstOrDefault();

            if (e.ButtonID == "btnLivros")
            {
                Session["SessionEditorSelecionado"] = loEditor;

                gvGerenciamentoEditores.JSProperties["cpRedirectToLivros"] = true;
            }
        }


        private void RedirectLivros(String idEditorString, string controlID)
        {
            switch (controlID)
            {
                case "btnLivros":
                    decimal id = Convert.ToDecimal(idEditorString);
                    EditoresSessao = this.ioEditoresDAO.BuscaEditores(id).First();

                    Response.Redirect("/Livraria/GerenciamentoLivros.aspx");
                    break;
                default: break;
            }
        }
    }
}