using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookSite.Web.Methods
{
    public static class SeoHelper
    {

        public static string HeaderWriter(string siteHeader,string pageHeader)
        {
            return siteHeader + " | " + pageHeader;
        }

       public static string ShowPrice(double price,int number)
        {
            string finalPrice = "";
            try
            {
                string[] str = Convert.ToString(price).Split(',');
                finalPrice = str[0] + "," + str[1].Substring(0, number);
                return finalPrice;
            }
            catch (Exception)
            {
                //string[] str = Convert.ToString(price).Split(',');
                finalPrice = price.ToString();
                return finalPrice;
            }
        }

    }
}