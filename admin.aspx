<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="proyecto02.admin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <asp:EntityDataSource ID="EntityDataSource1" runat="server" ConnectionString="name=empresaEntities" DefaultContainerName="empresaEntities" EnableDelete="True" EnableFlattening="False" EnableInsert="True" EnableUpdate="True" EntitySetName="beneficiario">
        </asp:EntityDataSource>

        <div>
            <asp:Label Text="Buscar" runat="server" />
            <asp:TextBox runat="server" id="txtBuscar"/>
            <asp:Button Text="Buscar" runat="server" id="btnBuscar" OnClick="btnBuscar_Click"/>
        </div>
        <br />

        <div>
                    <asp:GridView ID="grid1" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" DataKeyNames="id" DataSourceID="EntityDataSource1" ForeColor="Black" GridLines="None">
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
            <Columns>
                <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" SortExpression="id" />
                <asp:BoundField DataField="nombre" HeaderText="nombre" SortExpression="nombre" />
                <asp:BoundField DataField="apellido_pa" HeaderText="apellido_pa" SortExpression="apellido_pa" />
                <asp:BoundField DataField="apellido_ma" HeaderText="apellido_ma" SortExpression="apellido_ma" />
                <asp:BoundField DataField="telefono" HeaderText="telefono" SortExpression="telefono" />
                <asp:BoundField DataField="correo" HeaderText="correo" SortExpression="correo" />
                <asp:BoundField DataField="fecha_pago" HeaderText="fecha_pago" SortExpression="fecha_pago" />
                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
            </Columns>
            <FooterStyle BackColor="Tan" />
            <HeaderStyle BackColor="Tan" Font-Bold="True" />
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <SortedAscendingCellStyle BackColor="#FAFAE7" />
            <SortedAscendingHeaderStyle BackColor="#DAC09E" />
            <SortedDescendingCellStyle BackColor="#E1DB9C" />
            <SortedDescendingHeaderStyle BackColor="#C2A47B" />
        </asp:GridView>
        </div>

        <br />
        <div>
              <asp:Button Text="Descargar PDF" runat="server" ID ="btnGenerarPDF" OnClick="btnGenerarPDF_Click"/>
        </div>

        <br />
        <div>
              <asp:Button Text="Descargar Excel" runat="server" ID ="btnGenerarExcel" OnClick ="btnGenerarExcel_Click"/>
        </div>

        <div>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="cerrar.aspx">Cerrar Sesion</asp:HyperLink>
        </div>



    </form>
</body>
</html>
