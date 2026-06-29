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
    public class clsContactDataAccess
    {
        public static clsContactDTO GetContactInfoByID(int ID)
        {
            clsContactDTO ContactDTO = null;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = "SELECT * FROM Contacts WHERE ContactID = @ContactID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ContactID", ID);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //The record was found.
                                ContactDTO = new clsContactDTO();

                                ContactDTO.FirstName = reader["FirstName"].ToString();
                                ContactDTO.LastName = reader["LastName"].ToString();
                                ContactDTO.Email = reader["Email"].ToString();
                                ContactDTO.Phone = reader["Phone"].ToString();
                                ContactDTO.Address = reader["Address"].ToString();
                                ContactDTO.DateOfBirth = (DateTime)reader["DateOfBirth"];
                                ContactDTO.CountryID = (int)reader["CountryID"];


                                //ImagePath: allows null in database so we shold handle null.
                                if (reader["ImagePath"] != DBNull.Value)
                                {
                                    ContactDTO.ImagePath = (string)reader["ImagePath"];
                                }
                                else
                                {
                                    ContactDTO.ImagePath = "";
                                }

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
            
            return ContactDTO;
        }

        public static int AddNewContact(clsContactDTO ContactDTO)
        {
            ContactDTO.ID = -1;

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = @"INSERT INTO Contacts (FirstName, LastName, Email, Phone, Address, DateOfBirth, CountryID, ImagePath)
                                  VALUES (@FirstName, @LastName, @Email, @Phone, @Address, @DateOfBirth, @CountryID, @ImagePath);
                                  SELECT SCOPE_IDENTITY();";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FirstName", ContactDTO.FirstName);
                        command.Parameters.AddWithValue("@LastName", ContactDTO.LastName);
                        command.Parameters.AddWithValue("@Email", ContactDTO.Email);
                        command.Parameters.AddWithValue("@Phone", ContactDTO.Phone);
                        command.Parameters.AddWithValue("@Address", ContactDTO.Address);
                        command.Parameters.AddWithValue("@DateOfBirth", ContactDTO.DateOfBirth);
                        command.Parameters.AddWithValue("@CountryID", ContactDTO.CountryID);

                        if (!string.IsNullOrWhiteSpace(ContactDTO.ImagePath))
                        {
                            command.Parameters.AddWithValue("@ImagePath", ContactDTO.ImagePath);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
                        }

                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        {
                            //The Contact inserted Successfully.
                            ContactDTO.ID = insertedID;
                        }
                    }
                }
                
            }
            catch (Exception Error)
            {
                // Here we can add error to Logs.
                throw;
            }

            return ContactDTO.ID;
        }

        public static bool UpdateContact(clsContactDTO ContactDTO)
        {
            int affectedRows = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = @"UPDATE Contacts
                                SET FirstName = @FirstName
                                   ,LastName = @LastName
                                   ,Email = @Email
                                   ,Phone = @Phone
                                   ,Address = @Address
                                   ,DateOfBirth = @DateOfBirth
                                   ,CountryID = @CountryID
                                   ,ImagePath = @ImagePath
                               WHERE ContactID = @ContactID;";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ContactID", ContactDTO.ID);
                        command.Parameters.AddWithValue("@FirstName", ContactDTO.FirstName);
                        command.Parameters.AddWithValue("@LastName", ContactDTO.LastName);
                        command.Parameters.AddWithValue("@Email", ContactDTO.Email);
                        command.Parameters.AddWithValue("@Phone", ContactDTO.Phone);
                        command.Parameters.AddWithValue("@Address", ContactDTO.Address);
                        command.Parameters.AddWithValue("@DateOfBirth", ContactDTO.DateOfBirth);
                        command.Parameters.AddWithValue("@CountryID", ContactDTO.CountryID);

                        if (!string.IsNullOrWhiteSpace(ContactDTO.ImagePath))
                        {
                            command.Parameters.AddWithValue("@ImagePath", ContactDTO.ImagePath);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
                        }

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

        public static bool DeleteContact(int ID)
        {
            int affectedRows = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = "DELETE FROM Contacts WHERE ContactID = @ContactID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ContactID", ID);

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

        public static DataTable GetAllContacts()
        {
            DataTable dt = new DataTable();

            try
            {
                using(SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = "SELECT * FROM Contacts";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // Load All Rows.
                                dt.Load(reader);
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

            return dt;
        }

        public static bool IsContactExist(int ID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.connectionString))
                {
                    string query = "SELECT 1 FROM Contacts WHERE ContactID = @ContactID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ContactID", ID);

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
    }
}
