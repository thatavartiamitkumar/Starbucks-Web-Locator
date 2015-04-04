using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using WebApplication2;

namespace Starbucks
{
    public partial class DataTransfer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLDbConnection"].ConnectionString);
                cnn.Open();
                string query = "Select * from Address";
                SqlCommand cmd = new SqlCommand(query, cnn);
                List<addressCls> lstAddr = new List<addressCls>();
                try
                {
                    // addressCls addr = new addressCls();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            addressCls addr = new addressCls();
                            addr.addressid = Convert.ToInt32(dr["addressid"]);
                            addr.city = Convert.ToString(dr["city"]);
                            addr.street = Convert.ToString(dr["street"]);
                            addr.zipcode = Convert.ToString(dr["zipcode"]);
                            addr.state = Convert.ToString(dr["state"]);

                            lstAddr.Add(addr);
                        }
                    }
                    dr.Close();

                    for (int i = 0; i < lstAddr.Count; i++)
                    {
                        string cityquery = "select loc.cityid from Location loc  where loc.city= @city and loc.state=@state";
                        SqlCommand cmdcity = new SqlCommand(cityquery, cnn);
                        cmdcity.Parameters.AddWithValue("@city", lstAddr[i].city);
                        cmdcity.Parameters.AddWithValue("@state", lstAddr[i].state);


                        SqlDataReader citydr = cmdcity.ExecuteReader();
                        if (citydr.HasRows)
                        {
                            while (citydr.Read())
                            {
                                lstAddr[i].cityid = Convert.ToInt32(citydr[0]);
                                //addr.cityid = Convert.ToInt32(citydr["cityid"]);

                            }
                        }
                        citydr.Close();
                        string updatecity = " update Address set cityid=@cityid where addressid=@aid";
                        SqlCommand cmdupdate = new SqlCommand(updatecity, cnn);
                        cmdupdate.Parameters.AddWithValue("@cityid", lstAddr[i].cityid);
                        cmdupdate.Parameters.AddWithValue("@aid", lstAddr[i].addressid);

                        int up = cmdupdate.ExecuteNonQuery();
                        if (up > 0)
                        {

                        }

                    }

                }


                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}