using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    [Serializable]
    class City
    {
        public string DistrictID { get; set; }

        public string DistrictName { get; set; }

        public string LocationID { get; set; }

        public City()
        {

        }

        public City(string id, string name, string locationID)
            : this()
        {
            DistrictID = id;
            DistrictName = name;
            LocationID = locationID;
        }
    }
}
