using E_Commerce.Helper;
using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace E_Commerce.DAL
{
    public class EStoreDBContext
    {
        public static class EStoreDB
        {
            private static string connectionString = ConfigurationManager.ConnectionStrings["E-STORE"].ToString();

            public static List<Customer> GetCustomers()
            {
                SqlConnection sqlCon = null;
                List<Customer> customer = new List<Customer>();
                using (sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand Cmnd = new SqlCommand("SELECT * FROM dbo.Customers", sqlCon);
                    //Cmnd.Parameters.AddWithValue("@CustomerId", customerId);

                    Cmnd.CommandType = System.Data.CommandType.Text;
                    SqlDataReader rdr = Cmnd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Customer person = new Customer();
                        person.CustomerID = Convert.ToInt32(rdr["CustomerID"]);
                        person.FirstName = Convert.ToString(rdr["FirstName"]);
                        person.LastName = Convert.ToString(rdr["LastName"]);
                        person.Email = Convert.ToString(rdr["Email"]);
                        person.Password = Convert.ToString(rdr["Password"]);
                        person.Phone = Convert.ToString(rdr["Phone"]);
                        person.Address = Convert.ToString(rdr["Address"]);
                        customer.Add(person);
                    }

                    sqlCon.Close();
                }
                return customer;
            }

            public static Customer AddCustomer(Customer customer)
            {
                SqlConnection sqlCon = null;
                Customer person = new Customer();
                using (sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand Cmnd = new SqlCommand("INSERT INTO Customers (FirstName, LastName, Email, Password, Phone, Address) VALUES (@FirstName, @LastName, @Email, @Password, @Phone, @Address); SELECT SCOPE_IDENTITY()", sqlCon);
                    DecryptionHelper decryptionHelper = new DecryptionHelper();
                    customer.Password = decryptionHelper.EncryptedPassword(customer.Password);
                    customer.ConfirmPassword = (customer.Password);
                    Cmnd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    Cmnd.Parameters.AddWithValue("@LastName", customer.LastName);
                    Cmnd.Parameters.AddWithValue("@Email", customer.Email);
                    Cmnd.Parameters.AddWithValue("@Password", customer.Password);
                    Cmnd.Parameters.AddWithValue("@Phone", customer.Phone);
                    Cmnd.Parameters.AddWithValue("@Address", customer.Address);
                    //Cmnd.CommandType = System.Data.CommandType.Text;
                    //Cmnd.ExecuteNonQuery();
                    // Execute the INSERT statement and retrieve the generated customer ID
                    int customerId = Convert.ToInt32(Cmnd.ExecuteScalar());

                    // Create a new Customer object with the generated ID
                    Customer addedCustomer = new Customer
                    {
                        CustomerID = customerId,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Email = customer.Email,
                        Password = customer.Password,
                        ConfirmPassword = customer.ConfirmPassword,
                        Phone = customer.Phone,
                        Address = customer.Address
                    };

                    sqlCon.Close();

                    return addedCustomer;
                }
                
            }

            public static Customer LoginUser(string userName, string password)
            {
                SqlConnection sqlCon = null;
                List<Customer> customer = new List<Customer>();
                using (sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand Cmnd = new SqlCommand("SELECT * FROM dbo.Customers Where Email = @Email and Password = @Password", sqlCon);
                    Cmnd.Parameters.AddWithValue("@Email", userName);
                    Cmnd.Parameters.AddWithValue("@Password", password);

                    Cmnd.CommandType = System.Data.CommandType.Text;
                    SqlDataReader rdr = Cmnd.ExecuteReader();
                    Customer person = new Customer();
                    while (rdr.Read())
                    {
                        person.CustomerID = Convert.ToInt32(rdr["CustomerID"]);
                        person.FirstName = Convert.ToString(rdr["FirstName"]);
                        person.LastName = Convert.ToString(rdr["LastName"]);
                        person.Email = Convert.ToString(rdr["Email"]);
                        person.Password = Convert.ToString(rdr["Password"]);
                        person.Phone = Convert.ToString(rdr["Phone"]);
                        person.Address = Convert.ToString(rdr["Address"]);
                    }

                    
                    sqlCon.Close();
                    return person;
                }
               
            }
        }
    }
}
