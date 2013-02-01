using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    class Village:EntityBase
    { 
        public List<School> Schools { get; set; }
    }
}
