using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android;
using Android.Content;
using Android.Runtime;

/*
 * This class is a bunch of calls to a backend service in PHP that controls all the db interaction
 * Or Python I Still don't know
 */
namespace MyAquarium
{
    public class BackEndServices
    {
        public int UserExist(string email, string password) {
            int found;

            //First step: Call to the DB, maybe as POST

            //Second case: Check return code
            return 1;
        }

        public string GetAquariumData(string email, string name)
        {
            //First step: Call to the DB, maybe as POST

            //Second step: Check value of string

            //Third step: return string
            return "";
        }

        public int AddAquarium(string name, string email, string lts, string type)
        {
            return 1;
        }

        public int AddParameters(string email, string name, string date, string salinity, string amonia, string nitrite, string nitrate)
        {
            return 1;
        }

    }
}