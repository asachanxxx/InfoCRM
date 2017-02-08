using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace InfoPCMS
{
    class ConversionService
    {

        public String convertDate(String date) {

            if(date!=null){
            DateTime dt = DateTime.ParseExact(date, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
            var txtFedCat = dt.ToString("dd/M/yyyy");

            String newdate = dt.ToString("dd/MM/yyyy");
            return newdate;
            }

            return null;

        }

    }
}
