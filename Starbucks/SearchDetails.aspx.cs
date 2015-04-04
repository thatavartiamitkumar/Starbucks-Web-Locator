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
    public partial class SearchDetails : System.Web.UI.Page
    {
       
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["CompanyData"] = null;
            }
            DeleteButton.Visible = false;
        }

        protected void Search_Click(object sender, EventArgs e)
        {
            ErrorMessageID.Text = null;
            DeletelblMessage.Text = null;
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
            string mainQuery1 = "select street,city,state,country,zipcode,phone,longitude,latitude from Store,(select Address.addressid,Address.cityid,Address.street,Address.zipcode,Location.city,Location.state,Location.country from Address,Location where Address.cityid=Location.cityid and Location.delete_flag=0 and Address.delete_flag=0 ";
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
                    //if the search results has rows the make delete button visible 
                    DeleteButton.Visible = true;
                    

                    while (dr.Read())
                    {
                        DataRow row = dt.NewRow();
                        // ResultSet resultSet = new ResultSet();


                        row[0] = Convert.ToString(dr["street"]);
                        row[1] = Convert.ToString(dr["city"]);
                        row[2] = Convert.ToString(dr["state"]);
                        row[3] = Convert.ToString(dr["country"]);
                        row[4] = Convert.ToString(dr["zipcode"]);
                        row[5] = Convert.ToString(dr["phone"]);
                        row[6] = Convert.ToDouble(dr["longitude"]);
                        row[7] = Convert.ToDouble(dr["latitude"]);

                        dt.Rows.Add(row);

                        // lstCompany.Add(resultSet);
                    }
                }
                else
                {
                    ErrorMessageID.Text = "No Records Found !";
                    DeleteButton.Visible = false;
                }
                cnn.Close();
                if (dt != null)
                {
                   
                    GridViewSearch.DataSource = dt;
                    GridViewSearch.DataBind();
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



        public void FieldsClear()
        {

            StreetTextBox.Text = string.Empty;
            CityTextBox.Text = string.Empty;
            ddlState.SelectedIndex = 0;
            countryTextBox.Text = string.Empty;
            zipTextBox.Text = string.Empty;
            DeleteButton.Visible = false;
           

            Session["dataTable"] = null;
            GridViewSearch.DataSource = null;
            GridViewSearch.DataBind();

            DeletelblMessage.Text = null;
            ErrorMessageID.Text = null;

        }



        protected void Clear_Click(object sender, EventArgs e)
        {
            FieldsClear();
        }


       /* protected void GridViewSearch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ResultSet cmpData = new ResultSet();

            if (e.CommandName == "Delete")
            {
                string arg = Convert.ToString(((System.Web.UI.WebControls.CommandEventArgs)(e)).CommandArgument);
                int index = Convert.ToInt32(arg);
                GridViewRow row = GridViewSearch.Rows[index];

                cmpData.Street = Convert.ToString(row.Cells[0].Text);
                cmpData.City = Convert.ToString(row.Cells[1].Text);
                cmpData.State = Convert.ToString(row.Cells[2].Text);
                cmpData.Country = Convert.ToString(row.Cells[3].Text);
                cmpData.zipcode = Convert.ToString(row.Cells[4].Text);
                cmpData.phone = Convert.ToString(row.Cells[5].Text);
                cmpData.longitude = Convert.ToString(row.Cells[6].Text);
                cmpData.latitude = Convert.ToString(row.Cells[7].Text);
                SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLDbConnection"].ConnectionString);
                cnn.Open();
                string spName = "Delete from CompanyDetails where (company=@Company and street=@Street and city=@City and state=@State and country=@Country and zipcode=@zipcode and phone=@phone and longitutde=@longitude and latitude=@latitude)";
                SqlCommand cmd = new SqlCommand(spName, cnn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Street ", cmpData.Street);
                cmd.Parameters.AddWithValue("@City ", cmpData.City);
                cmd.Parameters.AddWithValue("@State ", cmpData.State);
                cmd.Parameters.AddWithValue("@Country ", cmpData.Country);
                cmd.Parameters.AddWithValue("@zipcode ", cmpData.zipcode);
                cmd.Parameters.AddWithValue("@phone ", cmpData.phone);
                cmd.Parameters.AddWithValue("@longitude ", cmpData.longitude);
                cmd.Parameters.AddWithValue("@latitude ", cmpData.latitude);

                try
                {
                    int cmp_id = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cnn.Close();

                    if (cmp_id == 1)
                    {
                        DeletelblMessage.Text = "Successfully Deleted";
                       
                    }
                    else
                    {
                        ErrorMessageID.Text = "Delete Failed";
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


        }*/

        protected void GridViewSearch_Sorting(object sender, GridViewSortEventArgs e)
        {
            string expression = "";
            expression = e.SortExpression;
            string direction = GetSortDirection(expression);
            DataTable dt = (DataTable)Session["dataTable"];

            if (dt != null)
            {

                dt.DefaultView.Sort = expression + " " + direction;
                GridViewSearch.DataSource = dt;
                GridViewSearch.DataBind();
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

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            CmpDetails cmpData = new CmpDetails();

            foreach (GridViewRow gvrow in GridViewSearch.Rows)
            {
                CheckBox chkdelete = (CheckBox)gvrow.FindControl("CheckBoxDelete");

                if (chkdelete.Checked)
                {
                    int index = gvrow.RowIndex;
                    GridViewRow row = GridViewSearch.Rows[index];

                    cmpData.Street = Convert.ToString(row.Cells[1].Text);
                    cmpData.City = Convert.ToString(row.Cells[2].Text);
                    cmpData.State = Convert.ToString(row.Cells[3].Text);
                    cmpData.Country = Convert.ToString(row.Cells[4].Text);
                    cmpData.zipcode = Convert.ToString(row.Cells[5].Text);
                    cmpData.phone = Convert.ToString(row.Cells[6].Text);
                    cmpData.longitude = Convert.ToDouble(row.Cells[7].Text);
                    cmpData.latitude = Convert.ToDouble(row.Cells[8].Text);

                    SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLDbConnection"].ConnectionString);
                    cnn.Open();
                    //Need to update two tables Store and Address

                    string updateStore = "update store set delete_flag = 1 where longitude =@longitude and latitude=@latitude";
                    string updateAddress = "update Address set delete_flag=1 where street=@street and zipcode =@zipcode";

                    SqlCommand storeCmd = new SqlCommand(updateStore, cnn);

                    storeCmd.CommandType = CommandType.Text;
                    storeCmd.Parameters.AddWithValue("@longitude", cmpData.longitude);
                    storeCmd.Parameters.AddWithValue("@latitude", cmpData.latitude);

                    SqlCommand addressCmd = new SqlCommand(updateAddress, cnn);

                    addressCmd.CommandType = CommandType.Text;
                    addressCmd.Parameters.AddWithValue("@street", cmpData.Street);
                    addressCmd.Parameters.AddWithValue("@zipcode", cmpData.zipcode);


                    try
                    {
                        int store_cmpId = storeCmd.ExecuteNonQuery();
                        int address_cmpId = addressCmd.ExecuteNonQuery();
                        storeCmd.Dispose();
                        addressCmd.Dispose();
                        cnn.Close();

                        if (store_cmpId >= 1 && address_cmpId >= 1)
                        {
                            DeletelblMessage.Text = "Successfully Deleted";
                            //take the datatable from session
                            DataTable dt = (DataTable)Session["dataTable"];

                            //delete the row in the data table
                            dt.Rows.RemoveAt(index);
                            GridViewSearch.DataSource = dt;
                            GridViewSearch.DataBind();
                            Session["dataTable"] = dt;

                            if ((dt != null && dt.Rows.Count != 0))
                            {
                                DeleteButton.Visible = false;
                            }
                        }
                        else
                        {
                            ErrorMessageID.Text = "Delete Failed";
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        storeCmd.Dispose();
                        addressCmd.Dispose();
                        cnn.Close();
                        cnn.Dispose();
                        DeleteButton.Visible = true;
                    }


                }
            }






        }

       

    }
}

