using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GMap;
using Subgurim.Controles;
using System.Data.SqlClient;
using Subgurim.Controles.GoogleChartIconMaker;
using System.Drawing;
using System.Configuration;

namespace Starbucks
{
    public partial class VisualizeLocation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["VisLocation"] = null;
                GLatLng Location = new GLatLng(37.09024, -95.712891);// 37.09024, -95.712891
                GMap1.setCenter(Location, 6);
                XPinLetter xpinLetter = new XPinLetter(PinShapes.pin_star, "USA", Color.Red, Color.White, Color.Chocolate);
                GMap1.Add(new GMarker(Location, new GMarkerOptions(new GIcon(xpinLetter.ToString(), xpinLetter.Shadow()))));
                GControl gc = new GControl(GControl.preBuilt.MapTypeControl);
                GMap1.Add(gc);
            }

        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            VisLocation vis = new VisLocation();
            if (ddlState.SelectedIndex != 0)
            {
                vis.state = Convert.ToString(ddlState.SelectedItem.Value);
            }
            vis.city = Convert.ToString(CityTextBox.Text);
            vis.zipcode = Convert.ToString(ZipTextBox.Text);
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

                    LabelMap.Text = "STARBUCKS LOCATIONS";
                    GLatLng mainLocation = new GLatLng(37.09024, -95.712891);
                    GMap1.setCenter(mainLocation, 12);

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
                        GMap1.setCenter(loc, 10);
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

        protected void ClearButton_Click(object sender, EventArgs e)
        {
            LabelMap.Text = "";
            CityTextBox.Text = string.Empty;
            ddlState.SelectedIndex = 0;
            ZipTextBox.Text = string.Empty;
            GLatLng Location = new GLatLng(37.09024, -95.712891);// 37.09024, -95.712891
            GMap1.setCenter(Location, 6);
            XPinLetter xpinLetter = new XPinLetter(PinShapes.pin_star, "USA", Color.Red, Color.White, Color.Chocolate);
            GMap1.Add(new GMarker(Location, new GMarkerOptions(new GIcon(xpinLetter.ToString(), xpinLetter.Shadow()))));
        }

    }
}
