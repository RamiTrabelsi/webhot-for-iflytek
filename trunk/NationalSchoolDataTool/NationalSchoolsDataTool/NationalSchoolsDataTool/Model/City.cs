using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    [Serializable]
    class City
    {
        public int DistrictID { get; set; }

        public string DistrictName { get; set; }

        public List<Village> Villages { get; set; }
    }
}
