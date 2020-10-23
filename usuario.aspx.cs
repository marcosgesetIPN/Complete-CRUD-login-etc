using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyecto02
{
    public partial class usuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Bienvenido: " + Session["usuario"]);
        }

        public void consulta()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["empresaConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SPSbeneficiario";
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = txtBuscar.Text.Trim();
                cmd.Connection = con;
                con.Open();
                grid1.DataSourceID = "";
                grid1.DataSource = cmd.ExecuteReader();
                grid1.DataBind();
            }
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            consulta();
        }

    }
}