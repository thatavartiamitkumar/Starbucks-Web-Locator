using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Starbucks
{
    public class CmpDetails
    {
        
        public string Street { set; get; }
        public string City { set; get; }
        public int cityid { set; get; }
        public int addressid{set;get;}
        public string State { set; get; }
        public string Country { set; get; }
        public string zipcode { set; get; }
        public string phone { set; get; }
        public double longitude { set; get; }
        public double latitude { set; get; }
        public string ddlCompany { set; get; }
        public string ddlSort { set; get; }
        public string ddlOrder { set; get; }
        public int Id { set; get; }
    }
}