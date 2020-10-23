<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usuario.aspx.cs" Inherits="proyecto02.usuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <div>
            <asp:Label Text="Buscar" runat="server" />
            <asp:TextBox runat="server" id="txtBuscar"/>
            <asp:Button Text="Buscar" runat="server" id="btnBuscar" OnClick="btnBuscar_Click"/>
        </div>
        <br />
        <div>
            <asp:GridView ID="grid1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="id" DataSourceID="EntityDataSource1" ForeColor="Black" GridLines="Vertical">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" SortExpression="id" />
                    <asp:BoundField DataField="nombre" HeaderText="nombre" SortExpression="nombre" />
                    <asp:BoundField DataField="apellido_pa" HeaderText="apellido_pa" SortExpression="apellido_pa" />
                    <asp:BoundField DataField="apellido_ma" HeaderText="apellido_ma" SortExpression="apellido_ma" />
                    <asp:BoundField DataField="telefono" HeaderText="telefono" SortExpression="telefono" />
                    <asp:BoundField DataField="correo" HeaderText="correo" SortExpression="correo" />
                    <asp:BoundField DataField="fecha_pago" HeaderText="fecha_pago" SortExpression="fecha_pago" />
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <RowStyle BackColor="#F7F7DE" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                <SortedAscendingHeaderStyle BackColor="#848384" />
                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                <SortedDescendingHeaderStyle BackColor="#575357" />
            </asp:GridView>
        </div>
        <div>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="cerrar.aspx">Cerrar Sesion</asp:HyperLink>
            <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=empresaEntities" DefaultContainerName="empresaEntities" EnableFlattening="False" EntitySetName="beneficiario">
            </asp:EntityDataSource>
            
        </div>
    </form>
</body>
</html>
