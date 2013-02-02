using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    [Serializable]
    class Province
    {
        public string LocationID { get; set; }

        public string LocationName { get; set; }

        public Province()
        {

        }

        public Province(string id, string name)
            : this()
        {
            LocationID = id;
            LocationName = name;
        }
    }
}
