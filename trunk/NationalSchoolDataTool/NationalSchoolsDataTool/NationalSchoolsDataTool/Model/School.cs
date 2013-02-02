using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    [Serializable]
    class School
    {
        public string SchoolID { get; set; }

        public string VilliageID { get; set; }

        public string DistrictID { get; set; }

        public string SchoolName { get; set; }

        public string SchoolProp1 { get; set; }

        public string SchoolProp2 { get; set; }

        
         
        public School()
        {

        }

        public School(string schoolID, string villiageID, string districtID, string name, string prop1, string prop2)
            : this()
        {

            SchoolID = schoolID;
            VilliageID = villiageID;
            DistrictID = districtID;
            SchoolName = name;
            SchoolProp1 = prop1;
            SchoolProp2 = prop2;
        }
    }
}
