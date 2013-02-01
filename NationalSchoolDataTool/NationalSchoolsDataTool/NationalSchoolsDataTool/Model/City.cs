using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    class City : EntityBase
    {  
        public List<Village> Villages { get; set; }
    }
}
