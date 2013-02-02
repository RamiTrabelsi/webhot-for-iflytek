using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    [Serializable]
    public class City
    {
        public string DistrictID { get; set; }

        public string DistrictName { get; set; }

        public string LocationID { get; set; }

        private List<Village> _villages =new List<Village>();

        public List<Village> Villages
        {
            get { return _villages; }
            set { _villages = value; }
        }

        
        public City()
        {

        }

        public City(string id, string name, string locationID, List<Village> villages)
            : this()
        {
            DistrictID = id;
            DistrictName = name;
            LocationID = locationID;
            Villages = villages;
        }
    }
}
