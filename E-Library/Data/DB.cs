using MySql.Data.MySqlClient;
using System.Xml.Linq;



namespace E_Library.Data
{
    public class DB
    {
        public string Test()
        {
            string str;
            string connectionString = "server=localhost;database=e-library;uid=root;password=idjN42069;port=3306";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT BID, title, PDF FROM book;";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();

                        str = reader.GetString(0);

                    }
                }
            }
            return str;

        }

        public List<DataPoints> GetBooks()
        {
            List<DataPoints> list = new List<DataPoints>();
            string connectionString = "server=localhost;database=e-library;uid=root;password=idjN42069;port=3306";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT BID, title, PDF, JPG FROM book WHERE borrower is NULL;";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new DataPoints { bid = reader.GetInt32(0), Title = reader.GetString(1), PDF = reader.GetString(2), JPG = reader.GetString(3) });
                        }

                    }
                }
            }
            return list;

        }

        public List<DataPoints> GetMyBooks(int account)
        {
            List<DataPoints> list = new List<DataPoints>();
            string connectionString = "server=localhost;database=e-library;uid=root;password=idjN42069;port=3306";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT BID, title, PDF, JPG FROM book WHERE borrower = @account;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@account", account);
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            list.Add(new DataPoints { bid = reader.GetInt32(0), Title = reader.GetString(1), PDF = reader.GetString(2), JPG = reader.GetString(3) });
                        }

                    }
                }
            }
            return list;

        }

        public void CreateAccount(string name, string email, string password, string securityQuestion, string securityAnswer)
        {
            string str;
            string connectionString = "server=localhost;database=e-library;uid=root;password=idjN42069;port=3306";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = "CALL createAccount(@name, @email, @password, @secQ, @secA)";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            
                            command.Parameters.AddWithValue("@name", name);
                            command.Parameters.AddWithValue("@email", email);
                            command.Parameters.AddWithValue("@password", password);
                            command.Parameters.AddWithValue("@secQ", securityQuestion);
                            command.Parameters.AddWithValue("@secA", securityAnswer);

                            
                            command.Transaction = transaction;
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Transaction failed: {ex.Message}");

                    }
                }

            }
        }
        public void ChangePassword(string email, string password)
        {
            string str;
            string connectionString = "server=localhost;database=e-library;uid=root;password=idjN42069;port=3306";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = "CALL changePassword(@email, @password)";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            
                            command.Parameters.AddWithValue("@email", email);
                            command.Parameters.AddWithValue("@password", password);

                            
                            command.Transaction = transaction;
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Transaction failed: {ex.Message}");

                    }
                }

            }
        }

        public void CheckOut(int bid, int userID)
        {
            string str;
            string connectionString = "server=localhost;database=e-library;uid=root;password=idjN42069;port=3306";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = "CALL checkOut(@bid, @userID)";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {

                            command.Parameters.AddWithValue("@bid", bid);
                            command.Parameters.AddWithValue("@userID", userID);


                            command.Transaction = transaction;
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Transaction failed: {ex.Message}");

                    }
                }

            }
        }

        public void CheckIn(int bid)
        {
            string str;
            string connectionString = "server=localhost;database=e-library;uid=root;password=idjN42069;port=3306";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        string query = "CALL checkIn(@bid)";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {

                            command.Parameters.AddWithValue("@bid", bid);
                            


                            command.Transaction = transaction;
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Transaction failed: {ex.Message}");

                    }
                }

            }
        }

        public bool EntryExist(string email)
        {
            bool exists = false;
            string connectionString = "server=localhost;database=e-library;uid=root;password=idjN42069;port=3306";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM patron WHERE email = @Email";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    
                    command.Parameters.AddWithValue("@Email", email);

                    
                    object result = command.ExecuteScalar();

                    
                    if (result != null && Convert.ToInt32(result) > 0)
                    {
                        exists = true;
                    }
                }
            }

            return exists;

        }

        public int GetAccount(string email)
        {
            int id;
            string connectionString = "server=localhost;database=e-library;uid=root;password=idjN42069;port=3306";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT userID FROM patron WHERE email = @Email;";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();

                        id = reader.GetInt32(0);

                    }
                }
            }
            return id;

        }
        public string GetName(string email)
        {
            string str;
            string connectionString = "server=localhost;database=e-library;uid=root;password=idjN42069;port=3306";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT name FROM patron WHERE email = '" + email + "';";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();

                        str = reader.GetString(0);

                    }
                }
            }
            return str;

        }
        public string[] GetSec(string email)
        {
            string[] str = new string[] {"", "" };
            string connectionString = "server=localhost;database=e-library;uid=root;password=idjN42069;port=3306";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT securityQuestion, securityAnswer FROM patron WHERE email = '" + email + "';";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        reader.Read();

                        str[0] = reader.GetString(0);
                        str[1] = reader.GetString(1);
                    }
                }
            }
            return str;

        }

        public  string logIn(string email, string password)
        {

            string loginVal = "l";
            string connectionString = "server=localhost;database=e-library;uid=root;password=idjN42069;port=3306";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT p.email, p.password FROM patron p WHERE p.email = '" + email + "';";
                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        string newEmail;
                        string newPassword;

                        reader.Read();

                        try
                        {
                            newEmail = reader.GetValue(0).ToString();
                            newPassword = reader.GetValue(1).ToString();

                            reader.Close();

                            if (password == newPassword)
                            {
                                
                                return password;
                            }
                            else
                            {
                                return null;
                            }

                        }
                        catch
                        {

                        }



                    }
                }
            }



            return null;
        }




    }
}
