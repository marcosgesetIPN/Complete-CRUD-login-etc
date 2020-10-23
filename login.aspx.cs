using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyecto02
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet1TableAdapters.UsuariosTableAdapter obj = new DataSet1TableAdapters.UsuariosTableAdapter();
                //tomando los valores del usuario y pass y mandarlos al dataset
                String usuarioContra = obj.login(txtUsuario.Text, txtContrasena.Text);

                if (usuarioContra != null)
                {
                    Session["usuario"] = usuarioContra;
                    Response.Redirect("verificar.aspx");
                }
            }
            catch
            {
                
            }
        }
    }
}