using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Diagnostics;
using GMaps;
using Subgurim.Controles;
using System.Data;
using System.Data.SqlClient;
using Subgurim.Controles.GoogleChartIconMaker;
using System.Drawing;


namespace Starbucks
{
    public partial class Decision : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["CompanyData"] = null;
                InsertButton.Visible = false;
                VisualizeButton.Visible = false;

                Session["VisLocation"] = null;
                GLatLng Location = new GLatLng(37.09024, -95.712891);// 37.09024, -95.712891
                GMap1.setCenter(Location, 6);
                XPinLetter xpinLetter = new XPinLetter(PinShapes.pin_star, "USA", Color.Red, Color.White, Color.Chocolate);
                GMap1.Add(new GMarker(Location, new GMarkerOptions(new GIcon(xpinLetter.ToString(), xpinLetter.Shadow()))));
                GControl gc = new GControl(GControl.preBuilt.MapTypeControl);
                GMap1.Add(gc);

                LabelMap.Visible = false;
                GMap1.Visible = false;

            }




        }



        protected void Decide_Click(object sender, EventArgs e)
        {
            //make previous error messages null
            DecidelblMessage.Text = null;
            ErrorMessageID.Text = null;
            CmpDetails cmpObj = new CmpDetails();


            if (!String.IsNullOrEmpty(StreetTextBox.Text))
            {
                cmpObj.Street = Convert.ToString(StreetTextBox.Text);

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

            }
            if (!String.IsNullOrEmpty(zipTextBox.Text))
            {
                cmpObj.zipcode = Convert.ToString(zipTextBox.Text);

            }

            try
            {
                string key = ConfigurationManager.AppSettings.Get("googlemaps.subgurim.net");
                String Address = cmpObj.Street + "," + cmpObj.City + "," + cmpObj.State;
                GeoCode geocode = GMap1.getGeoCodeRequest(Address);
                Subgurim.Controles.GMap.geoCodeRequest(Address, key);
                int countOfPlaceMark = geocode.Placemarks.Count;

                if (countOfPlaceMark == 1)
                {
                    GLatLng glatlng = new Subgurim.Controles.GLatLng(geocode.Placemark.coordinates.lat, geocode.Placemark.coordinates.lng);
                    cmpObj.latitude = glatlng.lat;
                    cmpObj.longitude = glatlng.lng;

                    // find the decision
                    int decision = 1;
                    string modelPath = "C:\\Users\\amitkumarthatavarti\\Documents\\ProcessedData_model.model";
                    ClassifyClass makeDecision = new ClassifyClass(cmpObj.longitude, cmpObj.latitude, modelPath);
                    decision = makeDecision.makeDecision();


                    if (decision == 4)
                    {
                        DecidelblMessage.Text = "You can setup Starbuck";
                        InsertButton.Visible = true;
                        VisualizeButton.Visible = true;
                    }
                    else if (decision == 5)
                    {
                        ErrorMessageID.Text = "Not a preferable location to setup Starbucks";
                        InsertButton.Visible = false;
                        VisualizeButton.Visible = false;
                    }
                    else if (decision == 2 || decision == 0)
                    {
                        ErrorMessageID.Text = "Error in Fetching JSON Object";
                        InsertButton.Visible = false;
                        VisualizeButton.Visible = false;
                    }
                    else if (decision == 3)
                    {
                        ErrorMessageID.Text = "Error in Reading JSON Object";
                        InsertButton.Visible = false;
                        VisualizeButton.Visible = false;
                    }
                }
                else
                {
                    ErrorMessageID.Text = "Invalid Address";
                    InsertButton.Visible = false;
                    VisualizeButton.Visible = false;

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
            DecidelblMessage.Text = null;
            ErrorMessageID.Text = null;
            InsertButton.Visible = false;
            VisualizeButton.Visible = false;
            LabelMap.Visible = false;
            GMap1.resetMarkers();
            GMap1.reset();
            GMap1.Visible = false;

        }
        protected void Clear_Click(object sender, EventArgs e)
        {
            FieldsClear();

            // SaveButton.Visible = true;
            Session["CompanyData"] = null;


        }

        //protected string GMap1_Click(object s, GAjaxServerEventArgs e)
        //{
        //  return default(string);
        // }

        protected void InsertButton_Click(object sender, EventArgs e)
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

                    SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLDbConnection"].ConnectionString);
                    cnn.Open();

                    //Find cityid from Location table
                    string locationSelectQuery = " Select cityid from Location where city=@city and state=@state";
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
                            string selectquery = "Select cityid from Location where city=@city and state=@state";
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
                    string addressquery = " Select addressid from Address where street=@street and zipcode=@zipcode";
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
                        //FieldsClear();
                        Session["CompanyData"] = null;
                        DecidelblMessage.Text = "Company Details Successfully Saved";
                        InsertButton.Visible = false;


                    }
                    else
                    {
                        ErrorMessageID.Text = "Company Details Not Saved,Please Try Again!";
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

        protected void VisualizeButton_Click(object sender, EventArgs e)
        {
            LabelMap.Visible = true;
            GMap1.Visible = true;

            VisLocation vis = new VisLocation();

            if (ddlState.SelectedIndex != 0)
            {
                vis.state = Convert.ToString(ddlState.SelectedItem.Value);
            }
            vis.city = Convert.ToString(CityTextBox.Text);
            vis.zipcode = Convert.ToString(zipTextBox.Text);
            SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQLDbConnection"].ConnectionString);
            cnn.Open();
            string subquery = "";
            if (!String.IsNullOrEmpty(vis.state))
            {

                subquery += " and state='" + vis.state.ToUpper() + "'";
            }
            if (!String.IsNullOrEmpty(vis.city))
            {
                String s = vis.city;
                string a = s.Substring(0, 1);
                string b = s.Substring(1, (s.Length - 1));
                string x = a.ToUpper() + b.ToLower();
                subquery += " and city='" + x + "'";

            }
            if (!String.IsNullOrEmpty(vis.zipcode))
            {
                subquery += " and zipcode='" + vis.zipcode + "'";

            }
            string query = " select latitude, longitude, street, city, state from Store s, Address addr, Location loc where addr.cityid=loc.cityid and s.addressid= addr.addressid " + subquery;
            SqlCommand cmd = new SqlCommand(query, cnn);
            List<VisLocation> lstvis = new List<VisLocation>();
            try
            {
                SqlDataReader dr1 = cmd.ExecuteReader();
                if (!dr1.HasRows)
                {
                    GMap1.reset();
                    GMap1.resetMarkers();
                    LabelMap.Text = " NO STRABUCKS AT THIS LOCATION";
                    GLatLng Location = new GLatLng(37.09024, -95.712891);
                    GMap1.setCenter(Location, 6);
                    XPinLetter xpinLetter = new XPinLetter(PinShapes.pin_star, "USA", Color.Red, Color.White, Color.Chocolate);
                    GMap1.Add(new GMarker(Location, new GMarkerOptions(new GIcon(xpinLetter.ToString(), xpinLetter.Shadow()))));
                    GControl gc = new GControl(GControl.preBuilt.MapTypeControl);
                    GMap1.Add(gc);
                }
                else if (dr1.HasRows)
                {
                    dr1.Close();

                    SqlDataReader dr = cmd.ExecuteReader();


                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            VisLocation vislist = new VisLocation();
                            vislist.LocLat = Convert.ToDouble(dr["latitude"]);
                            vislist.LocLng = Convert.ToDouble(dr["longitude"]);
                            vislist.street = Convert.ToString(dr["street"]);
                            vislist.state = Convert.ToString(dr["state"]);
                            vislist.city = Convert.ToString(dr["city"]);
                            lstvis.Add(vislist);
                        }

                    }

                    // LabelMap.Text = "STARBUCKS LOCATIONS";
                    GLatLng mainLocation = new GLatLng(37.09024, -95.712891);
                    GMap1.setCenter(mainLocation, 6);

                    PinIcon p;
                    GMarker gm;
                    GInfoWindow win;
                    List<Subgurim.Controles.GLatLng> glatln = new List<Subgurim.Controles.GLatLng>();
                    GMap1.reset();
                    GMap1.resetMarkers();
                    foreach (var i in lstvis)
                    {
                        p = new PinIcon(PinIcons.home, Color.Chocolate);
                        gm = new GMarker(new GLatLng(Convert.ToDouble(i.LocLat), Convert.ToDouble(i.LocLng)),
                            new GMarkerOptions(new GIcon(p.ToString(), p.Shadow())));
                        GLatLng loc = new GLatLng(Convert.ToDouble(i.LocLat), Convert.ToDouble(i.LocLng));
                        glatln.Add(loc);
                        GControl gc = new GControl(GControl.preBuilt.MapTypeControl);
                        GMap1.Add(gc);
                        win = new GInfoWindow(gm, i.street + " " + i.city + " " + i.state, false, GListener.Event.mouseover);
                        GMap1.setCenter(loc, 8);
                        GMap1.Add(win);

                    }
                    GControlPosition gcpos = new GControlPosition(GControlPosition.position.Top_Left);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }




    }
}