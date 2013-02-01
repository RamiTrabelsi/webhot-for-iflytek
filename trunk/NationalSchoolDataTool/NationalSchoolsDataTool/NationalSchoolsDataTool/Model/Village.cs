using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    [Serializable]
    class Village
    {
        public int VillageID { get; set; }

        public string Name { get; set; }

        public List<School> Schools { get; set; }
    }
}
