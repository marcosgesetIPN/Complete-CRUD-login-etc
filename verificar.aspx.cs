using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyecto02
{
    public partial class verificar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"].Equals("marcos"))
            {
                Response.Redirect("admin.aspx");
            }
            else
            {
                Response.Redirect("usuario.aspx");
            }
        }
    }
}