using DevExpress.Web;
using DevExpress.Web.Data;
using ProjetoLivraria.DAO;
using ProjetoLivraria.DTO;
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

        public TiposLivro TiposLivroSessao
        {
            get { return (TiposLivro)Session["SessionTiposLivroSelecionado"]; }
            set { Session["SessionTiposLivroSelecionado"] = value; }
        }

        public Autores AutoresSessao
        {
            get { return (Autores)Session["SessionAutorSelecionado"]; }
            set { Session["SessionAutorSelecionado"] = value; }
        }

        public Editores EditoresSessao
        {
            get { return (Editores)Session["SessionEditorSelecionado"]; }
            set { Session["SessionEditorSelecionado"] = value; }
        }

        public BindingList<LivrosDTO> ListaLivros
        {
            get
            {
                if ((BindingList<LivrosDTO>)ViewState["ViewStateListaLivros"] == null)
                    CarregaDados();
                return (BindingList<LivrosDTO>)ViewState["ViewStateListaLivros"];
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

        public BindingList<LivrosEAutores> ListaLivrosEAutores
        {
            get
            {
                if ((BindingList<LivrosEAutores>)ViewState["ViewStateListaLivrosEAutores"] == null)
                    this.CarregaDados();
                return (BindingList<LivrosEAutores>)ViewState["ViewStateListaLivrosEAutores"];
            }

            set
            {
                ViewState["ViewStateListaLivrosEAutores"] = value;
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
                CarregaListas();
                CarregarComboBoxes();
                CarregaGridView();
            }
            catch (Exception e)
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar recuperar dados.')</script>" + e.Message);
            }
        }

        private void CarregaGridView()
        {
            this.gvGerenciamentoLivros.DataSource = this.ListaLivros.OrderBy(loLivro => loLivro.liv_nm_titulo);

            GridViewDataComboBoxColumn autorCol = (GridViewDataComboBoxColumn)gvGerenciamentoLivros.Columns["aut_id_autor"];
            autorCol.PropertiesComboBox.DataSource = this.ListaAutores.OrderBy(loAutor => loAutor.aut_nome_completo);
            autorCol.PropertiesComboBox.ValueField = "aut_id_autor";
            autorCol.PropertiesComboBox.TextField = "aut_nome_completo";

            GridViewDataComboBoxColumn editorCol = (GridViewDataComboBoxColumn)gvGerenciamentoLivros.Columns["edi_id_editor"];
            editorCol.PropertiesComboBox.DataSource = this.ListaEditores.OrderBy(loEditor => loEditor.edi_nm_editor);
            editorCol.PropertiesComboBox.ValueField = "edi_id_editor";
            editorCol.PropertiesComboBox.TextField = "edi_nm_editor";

            GridViewDataComboBoxColumn categoriaCol = (GridViewDataComboBoxColumn)gvGerenciamentoLivros.Columns["til_id_tipo_livro"];
            categoriaCol.PropertiesComboBox.DataSource = this.ListaTiposLivro.OrderBy(loTipoLivro => loTipoLivro.til_ds_descricao);
            categoriaCol.PropertiesComboBox.ValueField = "til_id_tipo_livro";
            categoriaCol.PropertiesComboBox.TextField = "til_ds_descricao";
            this.gvGerenciamentoLivros.DataBind();
        }

        private void CarregarComboBoxes()
        {
            this.cbCadastroCategoria.DataSource = this.ListaTiposLivro.OrderBy(loTipoLivro => loTipoLivro.til_ds_descricao);
            this.cbCadastroCategoria.TextField = "til_ds_descricao";
            this.cbCadastroCategoria.ValueField = "til_id_tipo_livro";
            this.cbCadastroCategoria.DataBind();

            this.cbCadastroAutorLivro.DataSource = this.ListaAutores.OrderBy(loAutor => loAutor.aut_nome_completo);
            this.cbCadastroAutorLivro.TextField = "aut_nome_completo";
            this.cbCadastroAutorLivro.ValueField = "aut_id_autor";
            this.cbCadastroAutorLivro.DataBind();

            this.cbCadastroEditorLivro.DataSource = this.ListaEditores.OrderBy(loEditor => loEditor.edi_nm_editor);
            this.cbCadastroEditorLivro.TextField = "edi_nm_editor";
            this.cbCadastroEditorLivro.ValueField = "edi_id_editor";
            this.cbCadastroEditorLivro.DataBind();
        }

        private void CarregaListas()
        {
            this.ListaTiposLivro = ioTiposLivroDAO.BuscaTiposLivro();
            this.ListaAutores = ioAutoresDAO.BuscaAutores();
            this.ListaEditores = ioEditoresDAO.BuscaEditores();
            this.ListaLivros = ioLivrosDAO.BuscaLivrosDTO();
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

                CarregaDados();

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
            try
            {
                decimal idLivro = Convert.ToDecimal(e.Keys["liv_id_livro"]);
                decimal idAutor = Convert.ToDecimal(e.NewValues["aut_id_autor"]);
                decimal idCategoria = Convert.ToDecimal(e.NewValues["til_id_tipo_livro"]);
                decimal idEditor = Convert.ToDecimal(e.NewValues["edi_id_editor"]);
                string titulo = e.NewValues["liv_nm_titulo"].ToString();
                decimal preco = Convert.ToDecimal(e.NewValues["liv_vl_preco"]);
                decimal royalty = Convert.ToDecimal(e.NewValues["liv_pc_royalty"]);
                string resumo = e.NewValues["liv_ds_resumo"].ToString();
                int edicao = Convert.ToInt32(e.NewValues["liv_nu_edicao"]);

                if (string.IsNullOrEmpty(idCategoria.ToString()))
                {
                    HttpContext.Current.Response.Write("<script>alert('Informe a categoria do livro.');</script>");
                    return;
                }
                else if (string.IsNullOrEmpty(idEditor.ToString()))
                {
                    HttpContext.Current.Response.Write("<script>alert('Informe o editor do livro.');</script>");
                    return;
                }
                else if (string.IsNullOrEmpty(titulo))
                {
                    HttpContext.Current.Response.Write("<script>alert('Informe o titulo do livro.');</script>");
                    return;
                }
                else if (string.IsNullOrEmpty(preco.ToString()))
                {
                    HttpContext.Current.Response.Write("<script>alert('Informe o preço do livro.');</script>");
                    return;
                }
                else if (string.IsNullOrEmpty(royalty.ToString()))
                {
                    HttpContext.Current.Response.Write("<script>alert('Informe o royalty do livro.');</script>");
                    return;
                }
                else if (string.IsNullOrEmpty(resumo))
                {
                    HttpContext.Current.Response.Write("<script>alert('Informe o resumo do livro.');</script>");
                    return;
                }
                else if (string.IsNullOrEmpty(edicao.ToString()))
                {
                    HttpContext.Current.Response.Write("<script>alert('Informe a edicao do livro.');</script>");
                    return;
                }
                else if (string.IsNullOrEmpty(idAutor.ToString()))
                {
                    HttpContext.Current.Response.Write("<script>alert('Informe o autor do livro.');</script>");
                    return;
                }

                Livros livro = new Livros(idLivro, idCategoria, idEditor, titulo, preco, royalty, resumo, edicao);
                LivrosEAutores lEA = new LivrosEAutores(idAutor, idLivro, royalty);

                int qtdLinhasAfetadas = ioLivrosDAO.AtualizaLivro(livro);
                qtdLinhasAfetadas += ioLivrosEAutoresDAO.AtualizaLivroEAutor(lEA);

                e.Cancel = true;
                this.gvGerenciamentoLivros.CancelEdit();
                CarregaDados();
            }
            catch
            {
                e.Cancel = true;
                HttpContext.Current.Response.Write("<script>alert('Erro na atualização do livro.');</script>");
            }
        }

        protected void gvGerenciamentoLivros_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            try
            {
                decimal ldcIdLivro = Convert.ToDecimal(e.Keys["liv_id_livro"]);
                int visibleIndex = gvGerenciamentoLivros.FindVisibleIndexByKeyValue(ldcIdLivro);
                decimal ldcIdAutor = Convert.ToDecimal(gvGerenciamentoLivros.GetRowValues(visibleIndex, "aut_id_autor"));
                Livros loLivro = this.ioLivrosDAO.BuscaLivros(ldcIdLivro).FirstOrDefault();
            

                this.ioLivrosDAO.RemoveLivro(loLivro);
                this.ioLivrosEAutoresDAO.RemoveLivroEAutor(ldcIdAutor, ldcIdLivro);
                e.Cancel = true;
                CarregaDados();
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script> alert('Erro no cadastrado do Autor!'); </script>" + ex.Message);
            }
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