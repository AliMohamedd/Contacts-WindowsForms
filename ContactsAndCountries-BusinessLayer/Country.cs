using ContactsAndCountries_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsAndCountries_BusinessLayer
{
    public class clsCountry
    {
        private enum enMode { AddNew = 0, Update = 1}
        private enMode Mode = enMode.AddNew;

        public int ID { private set; get; }
        public string CountryName{ set; get; }
        public string Code { set; get; }
        public string PhoneCode { set; get; }

        public clsCountry()
        {
            this.ID = -1;
            this.CountryName = "";
            this.Code = "";
            this.PhoneCode = "";
            Mode = enMode.AddNew;
        }

        private clsCountry(int ID, string CountryName, string Code, string PhoneCode)
        {
            this.ID = ID;
            this.CountryName = CountryName;
            this.Code = Code;
            this.PhoneCode = Code;
            Mode = enMode.Update;
        }

        public static clsCountry Find(int ID)
        {
            string CountryName = "";
            string Code = "";
            string PhoneCode = "";

            if(clsCountryDataAccess.GetCountryByID(ID, ref CountryName, ref Code, ref PhoneCode))
            {
                return new clsCountry(ID, CountryName, Code, PhoneCode);
            }
            else
            {
                return null;
            }
        }

        public static clsCountry Find(string CountryName)
        {
            int ID = -1;
            string Code = "";
            string PhoneCode = "";

            if (clsCountryDataAccess.GetCountryByName(CountryName, ref ID, ref Code, ref PhoneCode))
            {
                return new clsCountry(ID, CountryName, Code, PhoneCode);
            }
            else
            {
                return null;
            }
        }

        public static bool IsCountryExist(int ID)
        {
            return clsCountryDataAccess.IsCountryExist(ID);
        }

        public static bool IsCountryExist(string FirstName)
        {
            return clsCountryDataAccess.IsCountryExist(FirstName);
        }

        private bool _AddNewCountry()
        {
            this.ID = clsCountryDataAccess.AddNewCountry(this.CountryName, this.Code, this.PhoneCode);
            return (this.ID != -1);
        }

        private bool _UpdateCountry()
        {
            return clsCountryDataAccess.UpdateCountry(this.ID, this.CountryName, this.Code, this.PhoneCode);
        }
        
        public bool Save()
        {
            switch(Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;

                case enMode.Update:
                    return (_UpdateCountry());
            }

            return false;
        }

        public static bool DeleteCountry(int ID)
        {
            return clsCountryDataAccess.DeleteCountry(ID);
        }

        public static DataTable GetAllCountries()
        {
            return clsCountryDataAccess.GetAllCountries();
        }
    }
}
