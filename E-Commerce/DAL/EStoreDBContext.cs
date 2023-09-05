using E_Commerce.Helper;
using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;

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

            public static bool CheckForDuplicates(string email)
            {
                SqlConnection sqlCon = null;
                using (sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand Cmnd = new SqlCommand("SELECT * FROM dbo.Customers Where Email = @Email", sqlCon);
                    Cmnd.Parameters.AddWithValue("@Email", email);

                    Cmnd.CommandType = System.Data.CommandType.Text;
                    int customerId = Convert.ToInt32(Cmnd.ExecuteScalar());
                    sqlCon.Close();

                    if (customerId > 0 )
                    {
                        return true;
                    }

                    else
                    {
                        return false;
                    }
                }
            }

            public static int GetNumberOfUsers()
            {
                SqlConnection sqlCon = null;
                using (sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand Cmnd = new SqlCommand("SELECT COUNT(*) as UserCount FROM Customers;", sqlCon);
                    Cmnd.CommandType = System.Data.CommandType.Text;
                    int customerId = Convert.ToInt32(Cmnd.ExecuteScalar());
                    sqlCon.Close();

                    if (customerId > 0)
                    {
                        return customerId;
                    }

                    else
                    {
                        return 0;
                    }
                }
            }

            public static void AddCategory(Category category)
            {
                SqlConnection sqlCon = null;
                Customer person = new Customer();
                using (sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand Cmnd = new SqlCommand("INSERT INTO dbo.Categories (CategoryName) VALUES (@CategoryName); SELECT SCOPE_IDENTITY()", sqlCon);
                    DecryptionHelper decryptionHelper = new DecryptionHelper();
                    Cmnd.Parameters.AddWithValue("@CategoryName", category.CategoryName);
                    //Cmnd.CommandType = System.Data.CommandType.Text;
                    //Cmnd.ExecuteNonQuery();
                    // Execute the INSERT statement and retrieve the generated customer ID
                    int customerId = Convert.ToInt32(Cmnd.ExecuteScalar());
                    sqlCon.Close();
                }
            }

            public static List<Category> GetCategoryList()
            {
                SqlConnection sqlCon = null;
                List<Category> categories = new List<Category>();
                using (sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand Cmnd = new SqlCommand("SELECT * FROM dbo.Categories", sqlCon);
                    //Cmnd.Parameters.AddWithValue("@CustomerId", customerId);

                    Cmnd.CommandType = System.Data.CommandType.Text;
                    SqlDataReader rdr = Cmnd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Category cat = new Category();
                        cat.CategoryID = Convert.ToInt32(rdr["CategoryID"]);
                        cat.CategoryName = Convert.ToString(rdr["CategoryName"]);
                        categories.Add(cat);
                    }

                    sqlCon.Close();
                }
                return categories;
            }

            public static void AddProduct(Products product)
            {
                SqlConnection sqlCon = null;
                using (sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    //SqlCommand Cmnd = new SqlCommand("INSERT INTO dbo.Products (ProductName) VALUES (@ProductName); SELECT SCOPE_IDENTITY()", sqlCon);
                    SqlCommand Cmnd = new SqlCommand("INSERT INTO dbo.Products (ProductName, CategoryID, ProductDescription, Category) VALUES (@ProductName, @CategoryID, @ProductDescription, @Category); SELECT SCOPE_IDENTITY()", sqlCon);
                    Cmnd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    Cmnd.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                    Cmnd.Parameters.AddWithValue("@Category", GetCategoryName(product.CategoryID));
                    Cmnd.Parameters.AddWithValue("@ProductDescription", product.ProductDescription);
                    //Cmnd.CommandType = System.Data.CommandType.Text;
                    //Cmnd.ExecuteNonQuery();
                    // Execute the INSERT statement and retrieve the generated customer ID
                    int productID = Convert.ToInt32(Cmnd.ExecuteScalar());

                    // Create a new Inventory object with the generated ProductID and quantity
                    Inventory newInventory = new Inventory
                    {
                        ProductID = productID, // Pass the generated ProductID
                        ProductName = product.ProductName,
                        Quantity = 50 // Set the initial quantity in inventory accordingly
                    };

                    // Call AddInventory to insert the inventory data
                    AddInventory(newInventory);
                    sqlCon.Close();
                }
            }

            public static string GetCategoryName(int categoryID)
            {
                SqlConnection sqlCon = null;
                string categoryName = null;
                using (sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand Cmnd = new SqlCommand("SELECT CategoryName FROM dbo.Categories Where CategoryID = @CategoryID", sqlCon);
                    Cmnd.Parameters.AddWithValue("@CategoryID", categoryID);

                    Cmnd.CommandType = System.Data.CommandType.Text;
                    categoryName = Convert.ToString(Cmnd.ExecuteScalar());
                    sqlCon.Close();

                    if (categoryName != null)
                    {
                        return categoryName;
                    }

                    else
                    {
                        return null;
                    }
                }
            }

            public static List<Products> GetProductList()
            {
                SqlConnection sqlCon = null;
                List<Products> productList = new List<Products>();
                using (sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand Cmnd = new SqlCommand("SELECT * FROM dbo.Products", sqlCon);
                    //Cmnd.Parameters.AddWithValue("@CustomerId", customerId);

                    Cmnd.CommandType = System.Data.CommandType.Text;
                    SqlDataReader rdr = Cmnd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Products products = new Products();
                        products.CategoryID = Convert.ToInt32(rdr["CategoryID"]);
                        products.ProductName = Convert.ToString(rdr["ProductName"]);
                        products.ProductDescription = Convert.ToString(rdr["ProductDescription"]);
                        products.ProductID = Convert.ToInt32(rdr["ProductID"]);
                        products.Category = GetCategoryName(Convert.ToInt32(rdr["CategoryID"]));
                        productList.Add(products);
                    }

                    sqlCon.Close();
                }

                return productList;
            }

            public static List<Inventory> GetInventoryList()
            {
                SqlConnection sqlCon = null;
                List<Inventory> inventoryList = new List<Inventory>();
                using (sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand Cmnd = new SqlCommand("SELECT * FROM dbo.Inventory", sqlCon);
                    //Cmnd.Parameters.AddWithValue("@CustomerId", customerId);

                    Cmnd.CommandType = System.Data.CommandType.Text;
                    SqlDataReader rdr = Cmnd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Inventory inventory = new Inventory();
                        inventory.InventoryID = Convert.ToInt32(rdr["InventoryID"]);
                        inventory.ProductName = Convert.ToString(rdr["ProductName"]);
                        inventory.ProductID = Convert.ToInt32(rdr["ProductID"]);
                        inventory.Quantity = Convert.ToInt32(rdr["Quantity"]);
                        inventory.Price = Convert.ToDecimal(rdr["Price"]);
                        inventoryList.Add(inventory);
                    }

                    sqlCon.Close();
                }

                return inventoryList;
            }

            public static void AddInventory(Inventory inventory)
            {
                SqlConnection sqlCon = null;
                using (sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    //SqlCommand Cmnd = new SqlCommand("INSERT INTO dbo.Inventory (ProductName) VALUES (@ProductName); SELECT SCOPE_IDENTITY()", sqlCon);
                    SqlCommand Cmnd = new SqlCommand("INSERT INTO dbo.Inventory (ProductID, ProductName, Quantity, Price) VALUES (@ProductID, @ProductName, @Quantity, @Price); SELECT SCOPE_IDENTITY()", sqlCon);
                    Cmnd.Parameters.AddWithValue("@ProductName", inventory.ProductName);
                    Cmnd.Parameters.AddWithValue("@ProductID", inventory.ProductID);
                    Cmnd.Parameters.AddWithValue("@Quantity", inventory.Quantity);
                    Cmnd.Parameters.AddWithValue("@Price", inventory.Price);
                    //Cmnd.CommandType = System.Data.CommandType.Text;
                    //Cmnd.ExecuteNonQuery();
                    // Execute the INSERT statement and retrieve the generated customer ID
                    int customerId = Convert.ToInt32(Cmnd.ExecuteScalar());
                    sqlCon.Close();
                }
            }

            public static Inventory GetCurrentInventory(int productID)
            {
                SqlConnection sqlCon = null;
                //string categoryName = null;
                Inventory currentInventory = new Inventory();  
                using (sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand Cmnd = new SqlCommand("SELECT * FROM dbo.Inventory Where ProductID = @ProductID", sqlCon);
                    Cmnd.Parameters.AddWithValue("@ProductID", productID);

                    using (var reader = Cmnd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Map the data from the database to the Inventory model
                            currentInventory = new Inventory
                            {
                                InventoryID = Convert.ToInt32(reader["InventoryID"]),
                                ProductID = Convert.ToInt32(reader["ProductID"]),
                                ProductName = reader["ProductName"].ToString(),
                                Quantity = Convert.ToInt32(reader["Quantity"]),
                                Price = Convert.ToDecimal(reader["Price"])
                                // Map other properties as needed
                            };
                        }
                    }

                    sqlCon.Close();

                    return currentInventory;
                }
            }

            public static int UpdateInventory(Inventory inventory)
            {
                SqlConnection sqlCon = null;
                using (sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    //SqlCommand Cmnd = new SqlCommand("INSERT INTO dbo.Inventory (ProductName) VALUES (@ProductName); SELECT SCOPE_IDENTITY()", sqlCon);
                    SqlCommand Cmnd = new SqlCommand("UPDATE dbo.Inventory SET ProductName = @ProductName, Quantity = @Quantity, Price = @Price Where ProductID = @ProductID;", sqlCon);
                    Cmnd.Parameters.AddWithValue("@ProductName", inventory.ProductName);
                    Cmnd.Parameters.AddWithValue("@ProductID", inventory.ProductID);
                    Cmnd.Parameters.AddWithValue("@Quantity", inventory.Quantity);
                    Cmnd.Parameters.AddWithValue("@Price", inventory.Price);
                    //Cmnd.CommandType = System.Data.CommandType.Text;
                    //Cmnd.ExecuteNonQuery();
                    // Execute the INSERT statement and retrieve the generated customer ID
                    int rowsAffected = Cmnd.ExecuteNonQuery();  
                    sqlCon.Close();

                    return rowsAffected;
                }
            }

            public static List<Products> GetProductsByCategory(int categoryId)
            {
                SqlConnection sqlCon = null;
                List<Products> productList = new List<Products>();
                using (sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand Cmnd = new SqlCommand("SELECT * FROM dbo.Products where CategoryID = @CategoryID", sqlCon);
                    Cmnd.Parameters.AddWithValue("@CategoryID", categoryId);

                    Cmnd.CommandType = System.Data.CommandType.Text;
                    SqlDataReader rdr = Cmnd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Products products = new Products();
                        products.CategoryID = Convert.ToInt32(rdr["CategoryID"]);
                        products.ProductName = Convert.ToString(rdr["ProductName"]);
                        products.ProductDescription = Convert.ToString(rdr["ProductDescription"]);
                        products.ProductID = Convert.ToInt32(rdr["ProductID"]);
                        products.Category = GetCategoryName(Convert.ToInt32(rdr["CategoryID"]));
                        productList.Add(products);
                    }

                    sqlCon.Close();
                }

                return productList;
            }
        }
    }
}
