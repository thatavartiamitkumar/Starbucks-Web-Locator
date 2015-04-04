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
    public partial class UpdateDetails : System.Web.UI.Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["CompanyData"] = null;
            }

        }

        protected void Search_Click(object sender, EventArgs e)
        {
            ErrorMessageID.Text = null;
            UpdatelblMessage.Text = null;
            CmpDetails resultSetObj = new CmpDetails();

            resultSetObj.Street = Convert.ToString(StreetTextBox.Text);
            resultSetObj.City = Convert.ToString(CityTextBox.Text);
            if (ddlState.SelectedIndex != 0)
            {
                resultSetObj.State = Convert.ToString(ddlState.SelectedItem.Value);
            }
            resultSetObj.Country = Convert.ToString(countryTextBox.Text);
            resultSetObj.zipcode = Convert.ToString(zipTextBox.Text);

            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLDbConnection"].ConnectionString);
            cnn.Open();
            string mainQuery1 = "select storeid,Store.addressid,cityid,street,city,state,country,zipcode,phone,longitude,latitude from Store,(select Address.addressid,Address.cityid,Address.street,Address.zipcode,Location.city,Location.state,Location.country from Address,Location where Address.cityid=Location.cityid and Location.delete_flag=0 and Address.delete_flag=0 ";
            string mainQuery2 = ") as temp where temp.addressid=Store.addressid and Store.delete_flag=0;";
            //   Location.state='OH'


            string subQuery = "";


            //int count = 0;
            if (!String.IsNullOrEmpty(resultSetObj.Street))
            {
                subQuery += " and street='" + resultSetObj.Street + "'";

            }
            if (!String.IsNullOrEmpty(resultSetObj.zipcode))
            {
                subQuery += " and zipcode='" + resultSetObj.zipcode + "'";

            }
            if (!String.IsNullOrEmpty(resultSetObj.City))
            {
                subQuery += " and city='" + resultSetObj.City + "'";

            }
            if (!String.IsNullOrEmpty(resultSetObj.State))
            {
                subQuery += " and state='" + resultSetObj.State + "'";

            }
            if (!String.IsNullOrEmpty(resultSetObj.Country))
            {
                subQuery += " and country='" + resultSetObj.Country + "'";

            }

            string spName = mainQuery1 + subQuery + mainQuery2;
            SqlCommand cmd = new SqlCommand(spName, cnn);

            // List<ResultSet> lstCompany = new List<ResultSet>();
            DataTable dt = new DataTable();
            dt.Columns.Add("storeid");
            dt.Columns.Add("addressid");
            dt.Columns.Add("cityid");
            dt.Columns.Add("Street");
            dt.Columns.Add("City");
            dt.Columns.Add("State");
            dt.Columns.Add("Country");
            dt.Columns.Add("Zipcode");
            dt.Columns.Add("Phone");
            dt.Columns.Add("Longitude");
            dt.Columns.Add("Latitude");


            try
            {

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        DataRow row = dt.NewRow();
                        // ResultSet resultSet = new ResultSet();

                        row[0] = Convert.ToInt32(dr["storeid"]);
                        row[1] = Convert.ToInt32(dr["addressid"]);
                        row[2] = Convert.ToInt32(dr["cityid"]);
                        row[3] = Convert.ToString(dr["street"]);
                        row[4] = Convert.ToString(dr["city"]);
                        row[5] = Convert.ToString(dr["state"]);
                        row[6] = Convert.ToString(dr["country"]);
                        row[7] = Convert.ToString(dr["zipcode"]);
                        row[8] = Convert.ToString(dr["phone"]);
                        row[9] = Convert.ToDouble(dr["longitude"]);
                        row[10] = Convert.ToDouble(dr["latitude"]);

                        dt.Rows.Add(row);

                        // lstCompany.Add(resultSet);
                    }
                }
                else
                {
                    ErrorMessageID.Text = "No Records Found !";
                }
                cnn.Close();
                if (dt != null)
                {
                    //DeleteButton.Visible = true;
                    GridViewUpdate.DataSource = dt;
                    GridViewUpdate.DataBind();
                   
                    Session["dataTable"] = dt;

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

        protected void GridViewSearch_Sorting(object sender, GridViewSortEventArgs e)
        {
            string expression = "";
            expression = e.SortExpression;
            string direction = GetSortDirection(expression);
            DataTable dt = (DataTable)Session["dataTable"];

            if (dt != null)
            {

                dt.DefaultView.Sort = expression + " " + direction;
                GridViewUpdate.DataSource = dt;
                GridViewUpdate.DataBind();
               
            }


        }

        private string GetSortDirection(string column)
        {

            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }
        public void FieldsClear()
        {
            
            StreetTextBox.Text=string.Empty;
            CityTextBox.Text=string.Empty;
            ddlState.SelectedIndex = 0;
            countryTextBox.Text=string.Empty;
            zipTextBox.Text=string.Empty;
            UpdatelblMessage.Text = null;
            ErrorMessageID.Text = null;
            
        }
       
        protected void Clear_Click(object sender, EventArgs e)
        {
            FieldsClear();
        }
       

       protected void EditRow(object sender, GridViewEditEventArgs e)
       {

           GridViewUpdate.EditIndex = e.NewEditIndex;
           DataTable dt = (DataTable)Session["dataTable"];
           GridViewUpdate.DataSource = dt;
           GridViewUpdate.DataBind();
           
          

       }

       protected void CancelEditRow(object sender, GridViewCancelEditEventArgs e)
       {

           GridViewUpdate.EditIndex = -1;
           DataTable dt = (DataTable)Session["dataTable"];
           GridViewUpdate.DataSource = dt;
           GridViewUpdate.DataBind();
          

       }

       protected void GridViewUpdate_RowUpdating(object sender, GridViewUpdateEventArgs e)
       {
           var autoid = GridViewUpdate.DataKeys[e.RowIndex].Value;
          
           GridViewRow row = GridViewUpdate.Rows[e.RowIndex] as GridViewRow;
           int storeid = Convert.ToInt32(row.Cells[1].Text.Trim());
           int addressid = Convert.ToInt32(((TextBox)(row.Cells[2].Controls[0])).Text);
           int cityid = Convert.ToInt32(((TextBox)(row.Cells[3].Controls[0])).Text);

           string street = ((TextBox)(row.Cells[4].Controls[0])).Text;
           string city = ((TextBox)(row.Cells[5].Controls[0])).Text;

           string state = ((TextBox)(row.Cells[6].Controls[0])).Text;
           string country = ((TextBox)(row.Cells[7].Controls[0])).Text;
           string zipcode = ((TextBox)(row.Cells[8].Controls[0])).Text;
           string phone = ((TextBox)(row.Cells[9].Controls[0])).Text;
           double longitude = Convert.ToDouble(((TextBox)(row.Cells[10].Controls[0])).Text);
           double latitude = Convert.ToDouble(((TextBox)(row.Cells[11].Controls[0])).Text);

           SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLDbConnection"].ConnectionString);
           cnn.Open();

           //update Store table
           string updateStore = "update store set longitude=@longitude, latitude=@latitude,phone=@phone where storeid=@storeid";
           SqlCommand storeCmd = new SqlCommand(updateStore, cnn);

           storeCmd.CommandType = CommandType.Text;
           storeCmd.Parameters.AddWithValue("@longitude", longitude);
           storeCmd.Parameters.AddWithValue("@latitude", latitude);
           storeCmd.Parameters.AddWithValue("@phone", phone);
           storeCmd.Parameters.AddWithValue("@storeid", storeid);



           string updateAddress = "update Address set street=@street, zipcode=@zipcode where addressid=@addressid";
           SqlCommand addressCmd = new SqlCommand(updateAddress, cnn);

           addressCmd.CommandType = CommandType.Text;
           addressCmd.Parameters.AddWithValue("@street", street);
           addressCmd.Parameters.AddWithValue("@zipcode", zipcode);
           addressCmd.Parameters.AddWithValue("@addressid", addressid);


           int store_cmpId = storeCmd.ExecuteNonQuery();
           int address_cmpId = addressCmd.ExecuteNonQuery();
           
           storeCmd.Dispose();
           addressCmd.Dispose();
           cnn.Close();
           
           
           cnn.Close();

           if (store_cmpId == 1 && address_cmpId == 1)
           {
               UpdatelblMessage.Text = "Successfully Updated";

               //take the datatable from session
               DataTable dt = (DataTable)Session["dataTable"];

               //update the edited row in the data table

               dt.Rows[e.RowIndex]["storeid"] = storeid;
               dt.Rows[e.RowIndex]["addressid"] = addressid;
               dt.Rows[e.RowIndex]["cityid"] = cityid;
               dt.Rows[e.RowIndex]["Street"] = street;
               dt.Rows[e.RowIndex]["City"] = city;
               dt.Rows[e.RowIndex]["State"] = state;
               dt.Rows[e.RowIndex]["Country"] = country;
               dt.Rows[e.RowIndex]["Zipcode"] = zipcode;
               dt.Rows[e.RowIndex]["Phone"] = phone;
               dt.Rows[e.RowIndex]["Longitude"] = longitude;
               dt.Rows[e.RowIndex]["Latitude"] = latitude;
              
               GridViewUpdate.EditIndex = -1;
               GridViewUpdate.DataSource = dt;
               GridViewUpdate.DataBind();

               Session["dataTable"] = dt;
              
           }
           else
           {
               ErrorMessageID.Text = "Update Failed";
           }
          

       }

       protected void GridViewUpdate_RowDataBound(object sender, GridViewRowEventArgs e)
       {
           int indexOfColumn = 3;
           if (e.Row.Cells.Count > indexOfColumn)
           {
               e.Row.Cells[2].Visible = false;
               e.Row.Cells[3].Visible = false;
           } 
       }

      
       
    }
}