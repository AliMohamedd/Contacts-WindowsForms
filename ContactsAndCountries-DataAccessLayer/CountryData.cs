using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ContactsAndCountries_DataAccessLayer
{
    public class clsCountryDataAccess
    {
        public static clsCountryDTO GetCountryByID(int ID)
        {
            clsCountryDTO CountryDTO = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = "SELECT * FROM Countries WHERE CountryID = @CountryID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CountryID", ID);

                        connection.Open();

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //The record was found.
                                CountryDTO = new clsCountryDTO();

                                CountryDTO.ID = ID;
                                if (reader["CountryName"] != System.DBNull.Value)
                                    CountryDTO.CountryName = (string)reader["CountryName"];
                                if (reader["Code"] != System.DBNull.Value)
                                    CountryDTO.Code = (string)reader["Code"];
                                if (reader["PhoneCode"] != System.DBNull.Value)
                                    CountryDTO.PhoneCode = (string)reader["PhoneCode"];
                            }
                        }
                    }
                }
            }
            catch (Exception Error)
            {
                // Here we can add error to Logs.
                throw;
            }

            return CountryDTO;
        }

        public static clsCountryDTO GetCountryByName(string CountryName)
        {
            clsCountryDTO CountryDTO = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = "SELECT * FROM Countries WHERE CountryName = @CountryName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CountryName", CountryName);

                        connection.Open();

                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //The record was found.
                                CountryDTO = new clsCountryDTO();

                                CountryDTO.ID = (int)reader["CountryID"];
                                if (reader["Code"] != System.DBNull.Value)
                                    CountryDTO.Code = (string)reader["Code"];
                                if (reader["CountryName"] != System.DBNull.Value)
                                    CountryDTO.CountryName = CountryName;
                                if (reader["PhoneCode"] != System.DBNull.Value)
                                    CountryDTO.PhoneCode = (string)reader["PhoneCode"];
                            }
                        }
                        
                    }
                }
            }
            catch (Exception Error)
            {
                // Here we can add error to Logs.
                throw;
            }

            return CountryDTO;
        }

        public static bool IsCountryExist(int ID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = "SELECT 1 FROM Countries WHERE CountryID = @CountryID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CountryID", ID);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception Error)
            {
                // Here we can add error to Logs.
                throw;
            }
  
            return false;
        }

        public static bool IsCountryExist(string CountryName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = "SELECT 1 FROM Countries WHERE CountryName = @CountryName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CountryName", CountryName);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            return true;
                        }
                    }
                }
                
            }
            catch (Exception Error)
            {
                // Here we can add error to Logs.
                throw;
            }

            return false;
        }

        public static int AddNewCountry(clsCountryDTO CountryDTO)
        {
            CountryDTO.ID = -1;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = @"INSERT INTO Countries (CountryName, Code, PhoneCode)
                                  VALUES (@CountryName, @Code, @PhoneCode);
                                  SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (CountryDTO.CountryName != "")
                            if (CountryDTO.CountryName.Length < 51)
                                command.Parameters.AddWithValue("@CountryName", CountryDTO.CountryName);
                            else
                                throw new Exception("The value assigned to CountryName exceeded the limit of 50 ");
                        else
                            command.Parameters.AddWithValue("@CountryName", System.DBNull.Value);

                        if (CountryDTO.Code != "")
                            if (CountryDTO.Code.Length < 4)
                                command.Parameters.AddWithValue("@Code", CountryDTO.Code);
                            else
                                throw new Exception("The value assigned to Code exceeded the limit of 3 ");
                        else
                            command.Parameters.AddWithValue("@Code", System.DBNull.Value);

                        if (CountryDTO.PhoneCode != "")
                            if (CountryDTO.PhoneCode.Length < 4)
                                command.Parameters.AddWithValue("@PhoneCode", CountryDTO.PhoneCode);
                            else
                                throw new Exception("The value assigned to PhoneCode exceeded the limit of 3 ");
                        else
                            command.Parameters.AddWithValue("@PhoneCode", System.DBNull.Value);

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            //The Contact inserted Successfully.
                            CountryDTO.ID = insertedID;
                        }
                    }
                }
            }
            catch (Exception Error)
            {
                // Here we can add error to Logs.
                throw;
            }

            return CountryDTO.ID;
        }

        public static bool UpdateCountry(clsCountryDTO CountryDTO)
        {
            int affectedRows = 0;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = @"UPDATE Countries
                                SET CountryName = @CountryName,
                                    Code = @Code,
                                    PhoneCode = @PhoneCode
                               WHERE CountryID = @CountryID;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CountryID", CountryDTO.ID);

                        if (CountryDTO.CountryName != "")
                            if (CountryDTO.CountryName.Length < 51)
                                command.Parameters.AddWithValue("@CountryName", CountryDTO.CountryName);
                            else
                                throw new Exception("The value assigned to CountryName exceeded the limit of 50 ");
                        else
                            command.Parameters.AddWithValue("@CountryName", System.DBNull.Value);

                        if (CountryDTO.Code != "")
                            if (CountryDTO.Code.Length < 4)
                                command.Parameters.AddWithValue("@Code", CountryDTO.Code);
                            else
                                throw new Exception("The value assigned to Code exceeded the limit of 3 ");
                        else
                            command.Parameters.AddWithValue("@Code", System.DBNull.Value);

                        if (CountryDTO.PhoneCode != "")
                            if (CountryDTO.PhoneCode.Length < 4)
                                command.Parameters.AddWithValue("@PhoneCode", CountryDTO.PhoneCode);
                            else
                                throw new Exception("The value assigned to PhoneCode exceeded the limit of 3 ");
                        else
                            command.Parameters.AddWithValue("@PhoneCode", System.DBNull.Value);

                        connection.Open();

                        affectedRows = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception Error)
            {
                // Here we can add error to Logs.
                throw;
            }

            return (affectedRows > 0);
        }

        public static bool DeleteCountry(int ID)
        {
            int affectedRows = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = "DELETE FROM Countries WHERE CountryID = @CountryID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CountryID", ID);
                        
                        connection.Open();

                        affectedRows = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception Error)
            {
                // Here we can add error to Logs.
                throw;
            }

            return (affectedRows > 0);
        }

        public static DataTable GetAllCountries()
        {
            DataTable dt = null;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = "SELECT * FROM Countries order by CountryName";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using(SqlDataReader reader = command.ExecuteReader())
                 
                        if (reader.HasRows)
                        {
                            // Load All Rows.
                            dt = new DataTable();
                            dt.Load(reader);
                        }
                    }
                }
            }
            catch (Exception Error)
            {
                // Here we can add error to Logs.
                throw;
            }

            return dt;
        }
    }
}
