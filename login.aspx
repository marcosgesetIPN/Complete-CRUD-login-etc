<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="proyecto02.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Empresa</title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Iniciar sesion</h1>
        <div>
            <label>Usuario</label>
            <asp:TextBox runat="server" ID="txtUsuario" />
            <br />
            <br />
            <label>Contraseña</label>
            <asp:Textbox runat="server" ID="txtContrasena" type="password"/>
            <br /> 
            <br />
            <asp:Button Text="Iniciar sesion" runat="server" ID="btnEnviar" OnClick="btnEnviar_Click"/>
        </div>
    </form>
</body>
</html>
