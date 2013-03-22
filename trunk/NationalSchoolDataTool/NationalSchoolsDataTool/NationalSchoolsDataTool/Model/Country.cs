using System;
using System.Collections.Generic;
using System.Text;

namespace NationalSchoolsDataTool
{
    class Country
    {
        public string ContryID { get; set; }

        public string CountryName { get; set; }

        private List<Province> _citys = new List<Province>();

        public List<Province> Provinces
        {
            get { return _citys; }
            set { _citys = value; }
        }

        public Country()
        {

        }

        public Country(string id, string name, List<Province> provinces)
            : this()
        {
            ContryID = id;
            CountryName = name;
            Provinces = provinces;
        }
    }
}
