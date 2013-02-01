using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    [Serializable]
    class School
    {
        public int SchoolID { get; set; }

        public string Name { get; set; }

        public string SchoolProp1 { get; set; }

        public string SchoolProp2 { get; set; }
    }
}
