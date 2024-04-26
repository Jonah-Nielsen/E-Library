using Microsoft.AspNetCore.Components;
using System;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;

namespace E_Library.Pages
{
    public partial class Index : ComponentBase
    {
        private string incorrect = "";
        private string Username { get; set; }
        private string Password { get; set; }


        private void click()
        {
            string value = db.logIn(Username, Password);

            string connectionString = "server=localhost;database=e-library;uid=root;password=idjN42069;port=3306";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT p.email, p.password FROM patron p WHERE p.email = '" + Username + "';";
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

                            if (Password == newPassword)
                            {
                                user.Email=Username;
                                this.Login();
                                
                            }
                            else
                            {
                                incorrect = "Invalid Username or Password";
                            }
                            

                        }
                        catch
                        {
                            incorrect = "Invalid Username or Password";
                        }



                    }
                }
            }


        }

        async Task Login() => NavigationManager.NavigateTo("/Account");



    }
}
