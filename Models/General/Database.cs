using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//doc file Web.Config
using System.Configuration;
//su dung duoc doi tuong DataTable
using System.Data;
//su dung duoc doi tuong SqlConnection
using System.Data.SqlClient;

namespace Add_TemplateAdmin.Models.General
{
    public class Database
    {
        public static string strConnection = "connection";
        //lay danh sach cac ban ghi -> tra ve DataTable
        public static DataTable ListDataTable(string sql)
        {
            DataTable dt = new DataTable();
            //try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[strConnection].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            //catch
            //{
            //    HttpContext.Current.Response.Write("<div>ListDataTable(): Lỗi thực thi câu truy vấn</div>");
            //    HttpContext.Current.Response.End();
            //}
            return dt;
        }
        //Lay mot ban ghi -> tra ve DataRow (la mot row trong DataTable)
        public static DataRow FirstDataTable(string sql)
        {
            DataTable dt = new DataTable();
            DataRow dr = dt.NewRow();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[strConnection].ToString()))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                        dr = dt.Rows[0];
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<div>FirstDataTable(): Lỗi thực thi câu truy vấn</div>");
                HttpContext.Current.Response.End();
            }
            return dr;
        }
        //thuc thi cau lenh sql
        public static void execute(string sql)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[strConnection].ToString()))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //---
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    //---
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<div>Execute(): Lỗi thực thi câu truy vấn</div>");
                HttpContext.Current.Response.End();
            }
        }
        //tra ve so luong ban ghi truy van
        public static int Count(string sql)
        {
            DataTable dt = new DataTable(sql);
            //try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[strConnection].ToString()))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                                return dt.Rows.Count;
                        }
                    }
                }
            }
            //catch
            //{
            //    HttpContext.Current.Response.Write("<div>ListDataTable(): Lỗi thực thi câu truy vấn</div>");
            //    HttpContext.Current.Response.End();
            //}
            return 0;
        }
    }
}