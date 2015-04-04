using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using GMaps;
using Subgurim.Controles;
namespace Starbucks
{
    public partial class CompanyDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FieldsClear();
                if (Session["CompanyData"] != null)
                {
                    if (((List<CmpDetails>)Session["CompanyData"]).Count > 0)
                    {
                        List<CmpDetails> lstCompanyData = (List<CmpDetails>)Session["CompanyData"];
                        CmpDetails cmpObj = lstCompanyData[0];
                        StreetTextBox.Text = Convert.ToString(cmpObj.Street);
                        CityTextBox.Text = Convert.ToString(cmpObj.City);
                        ddlState.SelectedItem.Value = Convert.ToString(cmpObj.State);
                        CountryTextBox.Text = Convert.ToString(cmpObj.Country);
                        zipTextBox.Text = Convert.ToString(cmpObj.zipcode);
                        PhoneTextBox.Text = Convert.ToString(cmpObj.phone);
                       // LongTextBox.Text = Convert.ToString(cmpObj.longitude);
                        //LatTextBox.Text = Convert.ToString(cmpObj.latitude);

                        Session["CompanyData"] = null;
                        if (Request.UrlReferrer != null &&
                                 Request.UrlReferrer.ToString().Contains("UpdateDetails.aspx"))
                        {
                            
                            SaveButton.Visible = false;
                        }
                    }
                }
                else
                {
                   
                    SaveButton.Visible = true;
                }
            }
        }

        public void Save_Click(object sender, EventArgs e)
        {
            
                CmpDetails cmpObj = new CmpDetails();
                string storeIntoQuery = "";
                string addresIntoQuery = "";
                string storeValueQuery = "";
                string addressValueQuery = "";


                if (!String.IsNullOrEmpty(StreetTextBox.Text))
                {
                    cmpObj.Street = Convert.ToString(StreetTextBox.Text);
                    addresIntoQuery += "street,";
                    addressValueQuery += "'" + cmpObj.Street + "'";
                    addressValueQuery += ",";
                }
                if (!String.IsNullOrEmpty(CityTextBox.Text))
                {
                    cmpObj.City = Convert.ToString(CityTextBox.Text);
                }
                if (ddlState.SelectedIndex != 0)
                {
                    cmpObj.State = Convert.ToString(ddlState.SelectedItem.Value);
                } 
                if (!String.IsNullOrEmpty(CountryTextBox.Text))
                {
                    cmpObj.Country = Convert.ToString(CountryTextBox.Text);
                }
                if (!String.IsNullOrEmpty(PhoneTextBox.Text))
                {
                    cmpObj.phone = Convert.ToString(PhoneTextBox.Text);
                    storeIntoQuery += "phone,";
                    storeValueQuery += "'" + cmpObj.phone + "'";
                    storeValueQuery += ",";
                }
                if (!String.IsNullOrEmpty(zipTextBox.Text))
                {
                    cmpObj.zipcode = Convert.ToString(zipTextBox.Text);
                    addresIntoQuery += "zipcode,";
                    addressValueQuery += "'" + cmpObj.zipcode + "'";
                    addressValueQuery += ",";
                }
                //if (!String.IsNullOrEmpty(LatTextBox.Text))
                //{
                //    cmpObj.latitude = Convert.ToDouble(LatTextBox.Text);
                //    storeIntoQuery += "latitude,";
                //    storeValueQuery += cmpObj.latitude;
                //    storeValueQuery += ",";

                //}
                //if (!String.IsNullOrEmpty(LongTextBox.Text))
                //{
                //    cmpObj.longitude = Convert.ToDouble(LongTextBox.Text);
                //    storeIntoQuery += "longitude,";
                //    storeValueQuery += cmpObj.longitude;
                //    storeValueQuery += ",";
                //}
                try
                {
                    
                        //Fetch Longitude and Latitude for given address

                        string key = ConfigurationManager.AppSettings.Get("googlemaps.subgurim.net");
                        String Address = cmpObj.Street + "," + cmpObj.City + "," + cmpObj.State;
                        GeoCode geocode = GMap1.getGeoCodeRequest(Address);
                        Subgurim.Controles.GMap.geoCodeRequest(Address, key);
                        GLatLng glatlng = new Subgurim.Controles.GLatLng(geocode.Placemark.coordinates.lat, geocode.Placemark.coordinates.lng);
                        if (geocode.Placemarks.Count == 1)
                        {

                            cmpObj.latitude = glatlng.lat;
                            cmpObj.longitude = glatlng.lng;


                            storeIntoQuery += "longitude,";
                            storeValueQuery += cmpObj.longitude + ",";
                            storeIntoQuery += "latitude,";
                            storeValueQuery += cmpObj.latitude + ",";


                            // If we get Longitude and Latitude then it is proper addres


                            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLDbConnection"].ConnectionString);
                            cnn.Open();

                            //check if the location is already there or not.
                            string storeCheckQuery = "select storeid from Store,Address where longitude=@longitude and latitude=@latitude and Store.delete_flag=0 and Store.addressid=Address.addressid and Address.delete_flag=0";
                            SqlCommand storeCheckCmd = new SqlCommand(storeCheckQuery, cnn);
                            storeCheckCmd.Parameters.AddWithValue("@longitude", cmpObj.longitude);
                            storeCheckCmd.Parameters.AddWithValue("@latitude", cmpObj.latitude);
                            SqlDataReader storeResult = storeCheckCmd.ExecuteReader();
                           
                            if (!storeResult.HasRows)
                            {
                                storeResult.Close();
                                //Find cityid from Location table
                                string locationSelectQuery = " Select cityid from Location where city=@city and state=@state and delete_flag=0";
                                SqlCommand selectcmd = new SqlCommand(locationSelectQuery, cnn);
                                selectcmd.Parameters.AddWithValue("@city", cmpObj.City);
                                selectcmd.Parameters.AddWithValue("@state", cmpObj.State);
                                SqlDataReader locationResultSet = selectcmd.ExecuteReader();
                                List<CmpDetails> lstCompany = new List<CmpDetails>();
                                //If there are no such cities insert this city
                                if (!locationResultSet.HasRows)
                                {
                                    locationResultSet.Close();
                                    string insertcity = " INSERT INTO city(city,state,country) values(" + cmpObj.City + "," + cmpObj.State + "," + cmpObj.Country + ")";
                                    SqlCommand citycmd = new SqlCommand(insertcity, cnn);
                                    citycmd.CommandType = CommandType.Text;
                                    int insertresult = citycmd.ExecuteNonQuery();
                                    if (insertresult == 1)
                                    {
                                        //Again select from the location for cityid
                                        string selectquery = "Select cityid from Location where city=@city and state=@state and delete_flag=0";
                                        SqlCommand selectcmd1 = new SqlCommand(selectquery, cnn);
                                        selectcmd1.Parameters.AddWithValue("@city", cmpObj.City);
                                        selectcmd1.Parameters.AddWithValue("@state", cmpObj.State);
                                        SqlDataReader locationReader = selectcmd1.ExecuteReader();
                                        if (locationReader.HasRows)
                                        {
                                            while (locationReader.Read())
                                            {
                                                CmpDetails cmpData = new CmpDetails();
                                                cmpData.cityid = Convert.ToInt32(locationReader["cityid"]);
                                                lstCompany.Add(cmpData);
                                            }
                                        }
                                        locationReader.Close();


                                    }
                                }
                                else
                                {
                                    while (locationResultSet.Read())
                                    {
                                        CmpDetails cmpData = new CmpDetails();
                                        cmpData.cityid = Convert.ToInt32(locationResultSet["cityid"]);
                                        lstCompany.Add(cmpData);
                                    }
                                }
                                locationResultSet.Close();

                                //Insert data into Address table
                                string insertAddress = "INSERT INTO Address(" + addresIntoQuery + "cityid) values(" + addressValueQuery + "@cityid)";
                                SqlCommand insertAddressCommand = new SqlCommand(insertAddress, cnn);
                                insertAddressCommand.CommandType = CommandType.Text;
                                insertAddressCommand.Parameters.AddWithValue("@cityid", lstCompany[0].cityid);
                                int intResult = insertAddressCommand.ExecuteNonQuery();

                                //Select the address id         
                                string addressquery = " Select addressid from Address where street=@street and zipcode=@zipcode and delete_flag=0";
                                SqlCommand selectAddressCommand = new SqlCommand(addressquery, cnn);
                                selectAddressCommand.Parameters.AddWithValue("@street", cmpObj.Street);
                                selectAddressCommand.Parameters.AddWithValue("@zipcode", cmpObj.zipcode);
                                List<CmpDetails> AddressCompany = new List<CmpDetails>();

                                SqlDataReader addressReader = selectAddressCommand.ExecuteReader();
                                if (addressReader.HasRows)
                                {
                                    while (addressReader.Read())
                                    {
                                        CmpDetails cmpData = new CmpDetails();
                                        cmpData.addressid = Convert.ToInt32(addressReader["addressid"]);
                                        AddressCompany.Add(cmpData);
                                    }
                                }
                                addressReader.Close();

                                //Insert into store table
                                string storeInsert = "INSERT INTO Store(" + storeIntoQuery + "addressid) values(" + storeValueQuery + "@addressid)";
                                SqlCommand storeInsertCommand = new SqlCommand(storeInsert, cnn);
                                storeInsertCommand.CommandType = CommandType.Text;
                                storeInsertCommand.Parameters.AddWithValue("@addressid", AddressCompany[0].addressid);
                                int intResult1 = storeInsertCommand.ExecuteNonQuery();
                                if ((Convert.ToInt32(intResult) > 0) && (Convert.ToInt32(intResult1)) > 0)
                                {
                                    FieldsClear();
                                    Session["CompanyData"] = null;
                                    InsertlblMessage.Text = "Company Details Successfully Saved";
                                }
                                else
                                {
                                    ErrorMessageID.Text = "Company Details Not Saved,Please Try Again!";
                                }
                            }
                            else
                            {
                                ErrorMessageID.Text = "Location Already Exists";
                            }
                        }
                        else
                        {
                            ErrorMessageID.Text = "Invalid Address";
                        }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error", e.ToString());
                    throw ex;
                }
                finally
                {

                }
            
        }

        public void FieldsClear()
        {

            StreetTextBox.Text = string.Empty;
            CityTextBox.Text = string.Empty;
            ddlState.SelectedIndex = 0;
            CountryTextBox.Text = string.Empty;
            zipTextBox.Text = string.Empty;
            PhoneTextBox.Text = string.Empty;
            InsertlblMessage.Text = null;
            ErrorMessageID.Text = null;
        }
        protected void Clear_Click(object sender, EventArgs e)
        {
            FieldsClear();
           
            SaveButton.Visible = true;
            Session["CompanyData"] = null;


        }


       
        protected string GMap1_Click(object s, GAjaxServerEventArgs e)
        {
            return default(string);
        }

        protected bool ErrorValidation()
        {
            int count = 0;
            if (!String.IsNullOrEmpty(StreetTextBox.Text))
            {
                InsertlblMessage.Text = "Please Enter Street";
                count++;
            }
            if (String.IsNullOrEmpty(CityTextBox.Text))
            {
                InsertlblMessage.Text = "Please Enter City";
                count++;
            }

            if (ddlState.SelectedIndex == 0)
            {
               
                InsertlblMessage.Text = "Please Enter State";
                count++;
            }
            if (String.IsNullOrEmpty(CountryTextBox.Text))
            {
                InsertlblMessage.Text = "Please Enter Country";
                count++;
            }
            if (String.IsNullOrEmpty(zipLabel.Text))
            {
                InsertlblMessage.Text = "Please Enter Zipcode";
                count++;
            }

            if (count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
                

        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {

        }



    }
}
