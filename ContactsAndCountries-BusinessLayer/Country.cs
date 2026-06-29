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

        private clsCountry(clsCountryDTO CountryDTO)
        {
            this.ID = CountryDTO.ID;
            this.CountryName = CountryDTO.CountryName;
            this.Code = CountryDTO.Code;
            this.PhoneCode = CountryDTO.PhoneCode;
            Mode = enMode.Update;
        }

        public static clsCountry Find(int ID)
        {
            clsCountryDTO CountryDTO = clsCountryDataAccess.GetCountryByID(ID);

            if(CountryDTO != null)
            {
                return new clsCountry(CountryDTO);
            }
            else
            {
                return null;
            }
        }

        public static clsCountry Find(string CountryName)
        {
            clsCountryDTO CountryDTO = clsCountryDataAccess.GetCountryByName(CountryName);

            if (CountryDTO != null)
            {
                return new clsCountry(CountryDTO);
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
            clsCountryDTO CountryDTO = new clsCountryDTO();
            CountryDTO.CountryName = this.CountryName;
            CountryDTO.Code = this.Code;
            CountryDTO.PhoneCode = this.PhoneCode;
            this.ID = clsCountryDataAccess.AddNewCountry(CountryDTO);
            return (this.ID != -1);
        }

        private bool _UpdateCountry()
        {
            clsCountryDTO CountryDTO = new clsCountryDTO();
            CountryDTO.ID = this.ID;
            CountryDTO.CountryName = this.CountryName;
            CountryDTO.Code = this.Code;
            CountryDTO.PhoneCode = this.PhoneCode;
            return clsCountryDataAccess.UpdateCountry(CountryDTO);
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
