using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Starbucks
{
    public partial class SortDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        
            if (!IsPostBack)
            {
                Session["CompanyData"] = null;
            }

        }

        protected void Sort_Click(object sender, EventArgs e)
        {
            CmpDetails cmpObj = new CmpDetails();
           
            cmpObj.Street = Convert.ToString(StreetTextBox.Text);
            cmpObj.City = Convert.ToString(CityTextBox.Text);
            cmpObj.State = Convert.ToString(StateTextBox.Text);
            cmpObj.Country = Convert.ToString(countryTextBox.Text);
            cmpObj.zipcode = Convert.ToString(zipTextBox.Text);
            string sort="";
            if (ddlSort.SelectedItem.Value != "0")
                cmpObj.ddlSort = Convert.ToString(ddlSort.SelectedItem.Value);
            else
            {
                cmpObj.ddlSort = null;
            }
            if (ddlOrder.SelectedItem.Value != "0")
            {
                cmpObj.ddlOrder = Convert.ToString(ddlOrder.SelectedItem.Value);
                if (cmpObj.ddlOrder == "Ascending")
                {
                    sort = "asc";
                }
                else
                {
                     sort = "desc";
                }
            
            }
            else
            {
                cmpObj.ddlOrder = null;
            }
            string subquery = "";
            string subquery1 = "";

            //int count = 0;
            if (!String.IsNullOrEmpty(cmpObj.Street))
            {
                subquery += " and street='" + cmpObj.Street + "'";

            }
            if (!String.IsNullOrEmpty(cmpObj.City))
            {
                subquery += " and city='" + cmpObj.City + "'";

            }
            if (!String.IsNullOrEmpty(cmpObj.State))
            {
                subquery += " and state='" + cmpObj.State + "'";

            }
            if (!String.IsNullOrEmpty(cmpObj.Country))
            {
                subquery += " and country='" + cmpObj.Country + "'";

            }
            if (!String.IsNullOrEmpty(cmpObj.ddlSort))
            {
                subquery1 += " order by '" + cmpObj.ddlSort + " '";

            }
            
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLDbConnection"].ConnectionString);
            cnn.Open();
            string spName = "Select * from CompanyDetails where (Company=@Company and zipcode=@Zipcode " + subquery + ")" + subquery1 + sort ;
            SqlCommand cmd = new SqlCommand(spName, cnn);
            
            cmd.Parameters.AddWithValue("@zipcode", cmpObj.zipcode);
            List<CmpDetails> lstCompany = new List<CmpDetails>();
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CmpDetails cmpData = new CmpDetails();
                       
                        cmpData.Street = Convert.ToString(dr["street"]);
                        cmpData.City = Convert.ToString(dr["city"]);
                        cmpData.State = Convert.ToString(dr["state"]);
                        cmpData.Country = Convert.ToString(dr["country"]);
                        cmpData.zipcode = Convert.ToString(dr["zipcode"]);
                        cmpData.phone = Convert.ToString(dr["phone"]);
                        cmpData.longitude = Convert.ToDouble(dr["longitutde"]);
                        cmpData.latitude = Convert.ToDouble(dr["latitude"]);
                        lstCompany.Add(cmpData);
                    }
                }
                cnn.Close();
                if (lstCompany.Count > 0)
                {

                    GridViewSort.DataSource = lstCompany;
                    GridViewSort.DataBind();
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Dispose();
                cnn.Close();
                cnn.Dispose();
            }
        }


        public void FieldsClear()
        {
            ddlCompany.SelectedIndex = 0;
            StreetTextBox.Text = string.Empty;
            CityTextBox.Text = string.Empty;
            StateTextBox.Text = string.Empty;
            countryTextBox.Text = string.Empty;
            zipTextBox.Text = string.Empty;
            ddlSort.SelectedIndex = 0;
            ddlOrder.SelectedIndex = 0;

        }
        protected void Clear_Click(object sender, EventArgs e)
        {
            FieldsClear();
        }
    }
}