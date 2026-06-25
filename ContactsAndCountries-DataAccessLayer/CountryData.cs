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
        public static bool GetCountryByID(int ID, ref string CountryName, ref string Code,
            ref string PhoneCode)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM Countries WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryID", ID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    //The record was found.
                    isFound = true;

                    if (reader["CountryName"] != System.DBNull.Value)
                        CountryName = (string)reader["CountryName"];
                    if (reader["Code"] != System.DBNull.Value)
                        Code = (string)reader["Code"];
                    if (reader["PhoneCode"] != System.DBNull.Value)
                        PhoneCode = (string)reader["PhoneCode"];
                }
                else
                {
                    // The record was not found.
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception Error)
            {
                // Here we can add error to Logs.
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetCountryByName(string CountryName, ref int ID, ref string Code,
            ref string PhoneCode)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM Countries WHERE CountryName = @CountryName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    //The record was found.
                    isFound = true;

                    ID = (int)reader["CountryID"];
                    if (reader["Code"] != System.DBNull.Value)
                        Code = (string)reader["Code"];
                    if (reader["PhoneCode"] != System.DBNull.Value)
                        PhoneCode = (string)reader["PhoneCode"];
                }
                else
                {
                    // The record was not found.
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception Error)
            {
                // Here we can add error to Logs.
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool IsCountryExist(int ID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT 1 FROM Countries WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryID", ID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    return true;
                }
            }
            catch (Exception Error)
            {
                // Here we can add error to Logs.
                return false;
            }
            finally
            {
                connection.Close();
            }

            return false;
        }

        public static bool IsCountryExist(string CountryName)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT 1 FROM Countries WHERE CountryName = @CountryName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    return true;
                }
            }
            catch (Exception Error)
            {
                // Here we can add error to Logs.
                return false;
            }
            finally
            {
                connection.Close();
            }

            return false;
        }

        public static int AddNewCountry(string CountryName, string Code, string PhoneCode)
        {
            int CountryID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"INSERT INTO Countries (CountryName, Code, PhoneCode)
                                  VALUES (@CountryName, @Code, @PhoneCode);
                                  SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                if (CountryName != "")
                    if (CountryName.Length < 51)
                        command.Parameters.AddWithValue("@CountryName", CountryName);
                    else
                        throw new Exception("The value assigned to CountryName exceeded the limit of 50 ");
                else
                    command.Parameters.AddWithValue("@CountryName", System.DBNull.Value);

                if (Code != "")
                    if (Code.Length < 4)
                        command.Parameters.AddWithValue("@Code", Code);
                    else
                        throw new Exception("The value assigned to Code exceeded the limit of 3 ");
                else
                    command.Parameters.AddWithValue("@Code", System.DBNull.Value);

                if (PhoneCode != "")
                    if (PhoneCode.Length < 4)
                        command.Parameters.AddWithValue("@PhoneCode", PhoneCode);
                    else
                        throw new Exception("The value assigned to PhoneCode exceeded the limit of 3 ");
                else
                    command.Parameters.AddWithValue("@PhoneCode", System.DBNull.Value);

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    //The Contact inserted Successfully.
                    CountryID = insertedID;
                }
            }
            catch (Exception Error)
            {
                // Here we can add error to Logs.
            }
            finally
            {
                connection.Close();
            }

            return CountryID;
        }

        public static bool UpdateCountry(int ID, string CountryName, string Code, string PhoneCode)
        {
            int affectedRows = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = @"UPDATE Countries
                                SET CountryName = @CountryName,
                                    Code = @Code,
                                    PhoneCode = @PhoneCode
                               WHERE CountryID = @CountryID;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryID", ID);

            try
            {
                connection.Open();

                if (CountryName != "")
                    if (CountryName.Length < 51)
                        command.Parameters.AddWithValue("@CountryName", CountryName);
                    else
                        throw new Exception("The value assigned to CountryName exceeded the limit of 50 ");
                else
                    command.Parameters.AddWithValue("@CountryName", System.DBNull.Value);

                if (Code != "")
                    if (Code.Length < 4)
                        command.Parameters.AddWithValue("@Code", Code);
                    else
                        throw new Exception("The value assigned to Code exceeded the limit of 3 ");
                else
                    command.Parameters.AddWithValue("@Code", System.DBNull.Value);

                if (PhoneCode != "")
                    if (PhoneCode.Length < 4)
                        command.Parameters.AddWithValue("@PhoneCode", PhoneCode);
                    else
                        throw new Exception("The value assigned to PhoneCode exceeded the limit of 3 ");
                else
                    command.Parameters.AddWithValue("@PhoneCode", System.DBNull.Value);

                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception Error)
            {
                // Here we can add error to Logs.
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (affectedRows > 0);
        }

        public static bool DeleteCountry(int ID)
        {
            int affectedRows = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "DELETE FROM Countries WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryID", ID);

            try
            {
                connection.Open();

                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception Error)
            {
                // Here we can add error to Logs.
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (affectedRows > 0);
        }

        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString);

            string query = "SELECT * FROM Countries order by CountryName";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    // Load All Rows.
                    dt.Load(reader);
                }

                reader.Close();
            }
            catch (Exception Error)
            {
                // Here we can add error to Logs.
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
    }
}
