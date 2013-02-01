using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    class Country:EntityBase
    {  
        public List<Province> Provinces { get; set; }
    }
}
