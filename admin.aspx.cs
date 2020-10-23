using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.Record;
using NPOI.SS.Util;
using System.ComponentModel;

namespace proyecto02
{
    public partial class admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Bienvenido: " + Session["usuario"]);
            fecha();
        }

        public void fecha()
        {
            DataTable dt = new DataTable();
            DateTime fecha_hoy = DateTime.Today;
            DateTime vence;
            

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["empresaConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT fecha_pago FROM beneficiario WHERE DATEDIFF(day, GetDate(), fecha_pago) in (0, 1, 6, 13)";
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                /*
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dt.Columns.Count - 1; j++)
                    {
                        vence = Convert.ToDateTime(dt.Rows[i][j].ToString());

                        if (vence != fecha_hoy)
                        {
                            Response.Write("<Script> alert('Tu pago es en 6 dias') </script>");
                        }
                    }
                } */

                foreach (DataRow row in dt.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        vence = Convert.ToDateTime(item);
                        if (vence == fecha_hoy.AddDays(6))
                        {
                            Response.Write("<Script> alert('Tu pago es en 6 dias') </script>");
                        }
                        else if (vence == fecha_hoy.AddDays(3))
                        {
                            Response.Write("<Script> alert('Tu pago es en 3 dias') </script>");
                        }
                        else if (vence == fecha_hoy)
                        {
                            Response.Write("<Script> alert('Tu pago vence hoy') </script>");
                        }
                    }
                }

            }

        }


        public DataTable dtBeneficiario()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["empresaConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SPSbeneficiarioConsulta";
                cmd.Connection = con;
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                da.Dispose();
            }
            return dt;
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

        protected void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            Document documento = new Document();
            PdfWriter escribir = PdfWriter.GetInstance(documento, HttpContext.Current.Response.OutputStream);
            dt = dtBeneficiario();
            if (dt.Rows.Count > 0)
            {
                documento.Open();
                Font fontTitle = FontFactory.GetFont(FontFactory.COURIER_BOLD, 25);
                Font font9 = FontFactory.GetFont(FontFactory.TIMES, 9);


                PdfPTable table = new PdfPTable(dt.Columns.Count);
                documento.Add(new Paragraph(20, "Beneficiarios", fontTitle));
                documento.Add(new Chunk("\n"));

                float[] widths = new float[dt.Columns.Count];
                for (int i = 0; i < dt.Columns.Count; i++)
                    widths[i] = 4f;

                table.SetWidths(widths);

                table.WidthPercentage = 90;

                PdfPCell cell = new PdfPCell(new Phrase("columns"));
                cell.Colspan = dt.Columns.Count;

                foreach (DataColumn c in dt.Columns)
                {
                    table.AddCell(new Phrase(c.ColumnName, font9));
                }

                foreach (DataRow r in dt.Rows)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int h = 0; h < dt.Columns.Count; h++)
                        {
                            table.AddCell(new Phrase(r[h].ToString(), font9));
                        }
                    }
                }
                documento.Add(table);
            }
            documento.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=beneficiarios" + ".pdf");
            HttpContext.Current.Response.Write(documento);
            Response.Flush();
            Response.End();
        }

 
        protected void btnGenerarExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = dtBeneficiario();
            Stream s = DataTableToExcel(dt);
            if (s != null)
            {
                MemoryStream ms = s as MemoryStream;
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=" + HttpUtility.UrlEncode("ejemplo") + ".xlsx"));
                Response.AddHeader("Content-Length", ms.ToArray().Length.ToString());
                Response.BinaryWrite(ms.ToArray());
                Response.Flush();
                ms.Close();
                ms.Dispose();
            }
        }

        //excel
        public Stream DataTableToExcel(DataTable dt)
        {
            XSSFWorkbook workbook = new XSSFWorkbook(); //genera el archivo de excel
            MemoryStream ms = new MemoryStream();
            ISheet sheet = workbook.CreateSheet("beneficiario"); //nombrando la hoja
            XSSFRow headerRow = headerRow = (XSSFRow)sheet.CreateRow(0);
            try
            {
                foreach (DataColumn column in dt.Columns)
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                int rowIndex = 1;
                foreach (DataRow row in dt.Rows)
                {
                    XSSFRow dataRow = (XSSFRow)sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in dt.Columns)
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    ++rowIndex;
                }
                for (int i = 0; i <= dt.Columns.Count; ++i)
                    sheet.AutoSizeColumn(i);
                workbook.Write(ms);
                ms.Flush();
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                ms.Close();
                sheet = null;
                headerRow = null;
                workbook = null;
            }
            return ms;
        }

        /*
        protected void btnGenerarWord_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = dtBeneficiario();
            MemoryStream ms = s as MemoryStream;
            Stream s = DataTableToExcel(dt);
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; file=datos.doc");
            Response.AddHeader("Content-Length", ms.ToArray().Length.ToString());  
            Response.BinaryWrite(ms.ToArray());
            Response.Flush();
            ms.Close();
            ms.Dispose();
        } */


    }
}