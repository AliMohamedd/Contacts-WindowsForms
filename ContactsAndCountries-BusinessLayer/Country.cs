using ContactsAndCountries_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ContactsAndCountries_BusinessLayer
{
    public class clsCountry
    {
        public enum enMode { AddNew = 0, Update = 1}
        public enMode Mode { private set; get; }

        public int ID { private set; get; }
        public string CountryName{ set; get; }
        public string Code { set; get; }
        public string PhoneCode { set; get; }

        public clsCountry()
        {
            this.ID = -1;
            this.CountryName = String.Empty;
            this.Code = String.Empty;
            this.PhoneCode = String.Empty;
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

            return CountryDTO == null ? null : new clsCountry(CountryDTO);
        }

        public static clsCountry Find(string CountryName)
        {
            clsCountryDTO CountryDTO = clsCountryDataAccess.GetCountryByName(CountryName);

            return CountryDTO == null ? null : new clsCountry(CountryDTO);
        }

        public static bool IsCountryExist(int ID)
        {
            return clsCountryDataAccess.IsCountryExist(ID);
        }

        public static bool IsCountryExist(string FirstName)
        {
            return clsCountryDataAccess.IsCountryExist(FirstName);
        }

        private clsCountryDTO _ToDo()
        {
            return new clsCountryDTO
            {
                ID = this.ID,
                CountryName = this.CountryName,
                Code = this.Code,
                PhoneCode = this.PhoneCode,
            };
        }

        private bool _AddNewCountry()
        {
            clsCountryDTO CountryDTO = _ToDo();

            this.ID = clsCountryDataAccess.AddNewCountry(CountryDTO);

            return (this.ID != -1);
        }

        private bool _UpdateCountry()
        {
            clsCountryDTO CountryDTO = _ToDo();

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
