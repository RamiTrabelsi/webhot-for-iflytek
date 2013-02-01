using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    [Serializable]
    class Province
    {
        public int LocationID { get; set; }

        public string Name { get; set; }

        public List<City> Citys { get; set; }
    }
}
