using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    [Serializable]
    public class Village
    {
        public string VillageID { get; set; }

        public string VillageName { get; set; }

        public string DistrictID { get; set; }

        private List<School> _schools = new List<School>();

        internal List<School> Schools
        {
            get { return _schools; }
            set { _schools = value; }
        }

       
        public Village()
        {

        }

        public Village(string id, string name, string districtID, List<School> schools)
            : this()
        {
            VillageID = id;
            VillageName = name;
            DistrictID = districtID;
            Schools = schools;
        }
    }
}
