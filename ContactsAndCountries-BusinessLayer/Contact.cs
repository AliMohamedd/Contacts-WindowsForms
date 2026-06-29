using ContactsAndCountries_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsAndCountries_BusinessLayer
{
    public class clsContact
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public int ID { private set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public DateTime DateOfBirth { set; get; }
        public int CountryID { set; get; }
        public string ImagePath { set; get; }

        // Used to add new contact.
        public clsContact()
        {
            this.ID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.CountryID = -1;
            this.ImagePath = "";
            Mode = enMode.AddNew;
        }

        // Used privatly by the find method to fill correct data form DB.
        private clsContact(clsContactDTO ContactDTO)
        {
            this.ID = ContactDTO.ID;
            this.FirstName = ContactDTO.FirstName;
            this.LastName = ContactDTO.LastName;
            this.Email = ContactDTO.Email;
            this.Phone = ContactDTO.Phone;
            this.Address = ContactDTO.Address;
            this.DateOfBirth = ContactDTO.DateOfBirth;
            this.CountryID = ContactDTO.CountryID;
            this.ImagePath = ContactDTO.ImagePath;
            Mode = enMode.Update;
        }

        public static clsContact Find(int ID)
        {
            clsContactDTO ContactDTO = clsContactDataAccess.GetContactInfoByID(ID);

            if (ContactDTO != null)
            {
                return new clsContact(ContactDTO);
            }
            else
            {
                return null;
            }
        }

        public static bool IsContactExist(int ID)
        {
            return (clsContactDataAccess.IsContactExist(ID));
        }

        private bool _AddNewContact()
        {
            clsContactDTO ContactDTO = new clsContactDTO();
            ContactDTO.FirstName = this.FirstName;
            ContactDTO.LastName = this.LastName;
            ContactDTO.Email = this.Email;
            ContactDTO.Phone = this.Phone;
            ContactDTO.Address = this.Address;
            ContactDTO.DateOfBirth = this.DateOfBirth;
            ContactDTO.CountryID = this.CountryID;
            ContactDTO.ImagePath = this.ImagePath;

            this.ID = clsContactDataAccess.AddNewContact(ContactDTO);

            return (this.ID != -1);
        }

        private bool _UpdateContact()
        {
            clsContactDTO ContactDTO = new clsContactDTO();
            ContactDTO.ID = this.ID;
            ContactDTO.FirstName = this.FirstName;
            ContactDTO.LastName = this.LastName;
            ContactDTO.Email = this.Email;
            ContactDTO.Phone = this.Phone;
            ContactDTO.Address = this.Address;
            ContactDTO.DateOfBirth = this.DateOfBirth;
            ContactDTO.CountryID = this.CountryID;
            ContactDTO.ImagePath = this.ImagePath;

            if (clsContactDataAccess.UpdateContact(ContactDTO)) return true;
            return false;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewContact())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateContact();
            }

            return false;
        }

        public static bool DeleteContact(int ID)
        {
            return (clsContactDataAccess.DeleteContact(ID));
        }

        public static DataTable GetAllContacts()
        {
            return clsContactDataAccess.GetAllContacts();
        }
    }
}
