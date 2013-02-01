using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    [Serializable]
    class Country
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public List<Province> Provinces { get; set; }
    }
}
