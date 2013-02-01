using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace NationalSchoolsDataTool
{
    class Province : EntityBase
    { 
        public List<City> Citys { get; set; }
    }
}
