<%@ Page Title="Gerenciamento de Editores" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="GerenciamentoEditores.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoEditores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function OnEndCallback(s, e) {
            if (s.cpRedirectToLivros) {
                window.location.href = '/Livraria/GerenciamentoLivros.aspx';
            }
        }

        function OnSalvarEditorClick(s, e) {
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
                    <dx:LayoutItem Caption="Nome" ColumnSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxCadastroNomeEditor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="true" ErrorText="Digite o nome do Editor!" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="E-mail"  ColumnSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxTextBox ID="tbxCadastroEmailEditor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="true" ErrorText="Digite o email do Editor!" />
                                        <RegularExpression ErrorText="Email inválido" ValidationExpression="[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="URL"  ColumnSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxCadastroUrlEditor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="true" ErrorText="Digite a URL do Editor!" />
                                        <RegularExpression ErrorText="URL inválida" ValidationExpression="^https?:\/\/[\w\-]+(\.[\w\-]+)+[/#?]?.*$"/>
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
                                    OnClick="BtnNovoEditor_Click"
                                    ClientSideEvents-Click="OnSalvarEditorClick"/>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>

    <dx:ASPxGridView ID="gvGerenciamentoEditores" runat="server" ShowInsert="True" AllowEditing="True" Width="100%" KeyFieldName="edi_id_editor"
        OnRowUpdating="gvGerenciamentoEditores_RowUpdating"
        OnRowDeleting="gvGerenciamentoEditores_RowDeleting"
        OnCustomButtonCallback="gvGerenciamentoEditores_CustomButtonCallback">
        <ClientSideEvents EndCallback="OnEndCallback" />
        <Settings ShowFilterRow="True"/>
        

        <Columns>
            <dx:GridViewDataTextColumn FieldName="edi_id_editor" Caption="Id" Visible="false" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="15" FieldName="edi_nm_editor" Caption="Nome" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="50" FieldName="edi_ds_email" Caption="Email" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="50" FieldName="edi_ds_url" Caption="URL" />

            <dx:GridViewCommandColumn ShowEditButton="true" ShowDeleteButton="true">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="btnLivros" Text="Livros" />
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
        <SettingsEditing Mode="Batch" />
    </dx:ASPxGridView>
</asp:Content>
