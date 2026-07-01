using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsAndCountries_DataAccessLayer
{
    public class clsCountryDTO
    {
        public int ID { set; get; }
        public string CountryName { set; get; }
        public string Code { set; get; }
        public string PhoneCode { set; get; }
    }
}
