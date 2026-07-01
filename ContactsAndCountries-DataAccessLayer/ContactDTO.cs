using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsAndCountries_DataAccessLayer
{
    public class clsContactDTO
    {
        public int ID { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public DateTime DateOfBirth { set; get; }
        public int CountryID { set; get; }
        public string ImagePath { set; get; }
    }
}
