using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace InfoPCMS.Services
{
    class LocationService
    {
        private String locationname;

        public String LocationName
        {
            get { return locationname; }
            set { locationname = value; }
        }

        public Boolean addLocation() {

            try {

                var parameters = new Dictionary<String, String>() { { "LocationName", LocationName } };
                InfoPCMS.db.executeInsertOrUpdate("spInsertLocation", parameters);
                return true;
            
            }
            catch (Exception ex) {

                throw ex;
            
            }

            return false;
        
        }


        public DataTable getAllLocations() {

            try {

                DataTable dt = InfoPCMS.db.executeSelectQuery("select * from Location");
                return dt;
            
            }
            catch (Exception ex) {

                throw ex;
            
            }
        
        }

    }
}
