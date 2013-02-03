using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    [Serializable]
    public class Province
    {
        public string LocationID { get; set; }

        public string LocationName { get; set; }

        private List<City> _citys = new List<City>();

        public List<City> Citys
        {
            get { return _citys; }
            set { _citys = value; }
        }

        public Province()
        {

        }

        public Province(string id, string name, List<City> citys)
            : this()
        {
            LocationID = id;
            LocationName = name;
            Citys = citys;
        }
    }
}
