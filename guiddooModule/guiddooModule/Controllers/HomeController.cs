using guiddooModule.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace guiddooModule.Controllers
{
    public class HomeController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage GetFriendList(int fromID )
        {
           Generic gn = new Generic();
            DataSet ds = new DataSet();
            DataTable result = new DataTable();
            result.TableName = "result";
            result.Columns.Add("status");
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Select FromID as 'Friend', Status from Friends where ToID= @fromID and Status=1 ", con))
                {
                    if (con.State != ConnectionState.Open)
                        con.Open();
                    cmd.Parameters.AddWithValue("@FromID", fromID);

                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            result.Rows.Add("1");
                            ds.Tables[0].TableName = "Friend List";
                            result.AcceptChanges();
                            ds.Tables.Add(result.Copy());

                        }
                        else
                        {
                            result.Rows.Add("0");
                            result.AcceptChanges();
                            ds.Tables.Add(result.Copy());
                        }
                    }

                }

            }
            
            return new HttpResponseMessage()
            {
                Content = new StringContent(
            gn.createJSON(ds),
            Encoding.UTF8,
            "text/html")
            };
        }
    }
}
