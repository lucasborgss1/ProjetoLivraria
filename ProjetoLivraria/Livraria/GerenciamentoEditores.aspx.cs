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
                decimal ldcIdEditor = this.ListaEditores.OrderByDescending(ed => ed.edi_id_editor).First().edi_id_editor + 1;

                string lsNomeEditor = this.tbxCadastroNomeEditor.Text;
                string lsEmailEditor = this.tbxCadastroEmailEditor.Text;
                string lsUrlEditor = this.tbxCadastroUrlEditor.Text;

                Editores loEditor = new Editores(ldcIdEditor, lsNomeEditor, lsEmailEditor, lsUrlEditor);

                this.ioEditoresDAO.InsereEditor(loEditor);

                Response.Redirect(Request.RawUrl, false);
            }
            catch
            {
                HttpContext.Current.Response.Write("<script> alert('Erro no cadastrado do Editor!'); </script>");
            }
        }


        protected void gvGerenciamentoEditores_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {

        }

        protected void gvGerenciamentoEditores_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {

        }

        protected void gvGerenciamentoEditores_RowDeleting(object sender, DevExpress.Web.ASPxGridViewCustomButtonCallbackEventArgs e)
        {

        }
    }
}