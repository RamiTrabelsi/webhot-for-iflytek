using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    [Serializable]
    class Village
    {
        public string VillageID { get; set; }

        public string VillageName { get; set; }

        public string DistrictID { get; set; }

        public Village()
        {

        }

        public Village(string id, string name, string districtID)
            : this()
        {
            VillageID = id;
            VillageName = name;
            DistrictID = districtID;
        }
    }
}
