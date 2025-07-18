<%@ Page Title="Gerenciamento de Categorias" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="GerenciamentoCategorias.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoCategorias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <script>
        function OnEndCallback(s, e) {
            if (s.cpRedirectToLivros) {
                window.location.href = '/Livraria/GerenciamentoLivros.aspx';
            }
        }

        function OnSalvarTipoLivroClick(s, e) {
            if (!ASPxClientEdit.ValidateGroup('MyGroup')) {
                e.processOnServer = false;
            } else {
                e.processOnServer = true;
            }
        }
     </script>

    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%" Theme="Office365">
        <Items>
            <dx:LayoutGroup Caption="" ColumnCount="2" SettingsItemCaptions-Location="Top">
                <Items>
                    <dx:LayoutItem Caption="Descrição" ColumnSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxDescricaoTipoLivro" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="true" ErrorText="Digite o nome da Categoria!" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="" ColumnSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton runat="server" Text="Salvar" AutoPostBack="false" ValidationGroup="MyGroup" 
                                    Width="100%" 
                                    UseSubmitBehavior="false"
                                    OnClick="BtnNovoTipoLivro_Click"
                                    ClientSideEvents-Click="OnSalvarTipoLivroClick"/>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>

     <dx:ASPxGridView ID="gvGerenciamentoTiposLivro" runat="server" ShowInsert="True" AllowEditing="True" Width="100%" KeyFieldName="til_id_tipo_livro"
        OnRowUpdating="gvGerenciamentoTiposLivro_RowUpdating"
        OnRowDeleting="gvGerenciamentoTiposLivro_RowDeleting"
        OnCustomButtonCallback="gvGerenciamentoTiposLivro_CustomButtonCallback">
        <ClientSideEvents EndCallback="OnEndCallback" />
        <Settings ShowFilterRow="True"/>
        

        <Columns>
            <dx:GridViewDataTextColumn FieldName="til_id_tipo_livro" Caption="Id" Visible="false" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="15" FieldName="til_ds_descricao" Caption="Descrição" />

            <dx:GridViewCommandColumn ShowEditButton="true" ShowDeleteButton="true">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="btnLivros" Text="Livros" />
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
        <SettingsEditing Mode="Batch" />
    </dx:ASPxGridView>
</asp:Content>
