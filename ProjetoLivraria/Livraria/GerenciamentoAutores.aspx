<%@ Page Title="Gerenciamento de Autores" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="GerenciamentoAutores.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoAutores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script> 
        function OnEndCallback(s, e) {
            if (s.cpRedirectToLivros) {
                window.location.href = '/Livraria/GerenciamentoLivros.aspx';
            }
        }

        function OnSalvarAutorClick(s, e) {
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
                    <dx:LayoutItem Caption="Nome">
                        <ParentContainerStyle Paddings-PaddingRight="12"></ParentContainerStyle>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxCadastroNomeAutor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="true" ErrorText="Digite o nome do Autor!" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="Sobrenome">
                        <ParentContainerStyle Paddings-PaddingLeft="0" Paddings-PaddingRight="12"></ParentContainerStyle>
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer>
                                <dx:ASPxTextBox ID="tbxCadastroSobrenomeAutor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="true" ErrorText="Digite o sobrenome do Autor!" />
                                    </ValidationSettings>
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="E-mail" ColumnSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxTextBox ID="tbxCadastroEmailAutor" runat="server" Width="100%">
                                    <ValidationSettings ValidationGroup="MyGroup" ValidateOnLeave="true" Display="Dynamic">
                                        <RequiredField IsRequired="true" ErrorText="Digite o email do Autor!" />
                                        <RegularExpression ErrorText="Email inválido" ValidationExpression="[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" />
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
                                    OnClick="BtnNovoAutor_Click"
                                    ClientSideEvents-Click ="OnSalvarAutorClick"/>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>

    <dx:ASPxGridView ID="gvGerenciamentoAutores" runat="server" ShowInsert="True" AllowEditing="True" Width="100%" KeyFieldName="aut_id_autor"
        OnRowUpdating="gvGerenciamentoAutores_RowUpdating"
        OnRowDeleting="gvGerenciamentoAutores_RowDeleting"
        OnCustomButtonCallback="gvGerenciamentoAutores_CustomButtonCallback">
        <ClientSideEvents EndCallback="OnEndCallback" />
        <Settings ShowFilterRow="True"/>
        

        <Columns>
            <dx:GridViewDataTextColumn FieldName="aut_id_autor" Caption="Id" Visible="false" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="15" FieldName="aut_nm_nome" Caption="Nome" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="50" FieldName="aut_nm_sobrenome" Caption="Sobrenome" />
            <dx:GridViewDataTextColumn PropertiesTextEdit-MaxLength="50" FieldName="aut_ds_email" Caption="Email" />

            <dx:GridViewCommandColumn ShowEditButton="true" ShowDeleteButton="true">
                <CustomButtons>
                    <dx:GridViewCommandColumnCustomButton ID="btnLivros" Text="Livros" />
                    <dx:GridViewCommandColumnCustomButton ID="btnAutorInfo" Text="Informação" />
                </CustomButtons>
            </dx:GridViewCommandColumn>
        </Columns>
        <SettingsEditing Mode="Batch" />
    </dx:ASPxGridView>

    
</asp:Content>

