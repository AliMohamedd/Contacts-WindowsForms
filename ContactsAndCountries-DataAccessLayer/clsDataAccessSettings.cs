using System;
using System.Configuration;

namespace ContactsAndCountries_DataAccessLayer
{
    static class clsDataAccessSettings
    {
        public static readonly string connectionString =
            ConfigurationManager.ConnectionStrings["ContactsDB"].ConnectionString
        ?? throw new Exception(
            "Environment variable CONTACTS_DB_CONNECTION was not found.");
    }
}