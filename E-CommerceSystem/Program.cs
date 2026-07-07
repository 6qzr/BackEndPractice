using E_CommerceSystem.Models;
using Isopoh.Cryptography.Argon2;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace E_CommerceSystem
{
    internal class Program
    {

        public static ECommerceContext context = new ECommerceContext();

        static void DisplayHeader(string header)
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine($"       {header.ToUpper()}      ");
            Console.WriteLine("==================================================");
        }

        /* 
         * ADD Operations --------------------------------------------------------------------
        */

        // 01 Register a New User
        static void RegisterNewUser()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            DisplayHeader("Register New User");
            Console.ResetColor();

            Console.Write("\nEnter a Username: ");
            string userName = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(userName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Invalid username. Press Enter");
                Console.ReadLine();
                Console.ResetColor();
                return;
            }

            // Check for uniqueness in the database
            bool usernameExists = context.Users.Any(u => u.username == userName);
            if (usernameExists)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  This username is already taken. Please choose another one. Press Enter");
                Console.ReadLine();
                Console.ResetColor();
                return;
            }

            Console.Write("\nEnter User Email: ");
            string email = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(email))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Invalid user email. Press Enter");
                Console.ReadLine();
                Console.ResetColor();
                return;
            }
            
            // Validate structural format(e.g., text@domain.com)
            if (!System.Net.Mail.MailAddress.TryCreate(email, out _))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Invalid email format. Please enter a valid email. Press Enter");
                Console.ReadLine();
                Console.ResetColor();
                return;
            }

            // Validate uniqueness against the database using EF Core
            bool emailExists = context.Users.Any(u => u.email == email);
            if (emailExists)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  This email is already registered to an account. Press Enter");
                Console.ReadLine();
                Console.ResetColor();
                return;
            }

            Console.Write("\nEnter a Password: ");
            string password = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(password))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Invalid password. Press Enter");
                Console.ReadLine();
                Console.ResetColor();
                return;
            }

            // Hash the plain-text password using BCrypt
            //string securePasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

            // Hash the plain-text password using Argon2id (Golden Standard)
            string securePasswordHash = Argon2.Hash(password);

            Console.Write("\nEnter User Full Name: ");
            string fullName = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(fullName))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Invalid user full name. Press Enter");
                Console.ReadLine();
                Console.ResetColor();
                return;
            }

            // Phone Number and Address can be null
            Console.Write("\nEnter User Phone Number: ");
            string phone = Console.ReadLine().Trim();

            Console.Write("\nEnter User Address: ");
            string address = Console.ReadLine().Trim();

            User newUser = new User
            {
                username = userName,
                email = email,
                passwordHash = securePasswordHash,
                fullName = fullName,
                phoneNumber = phone,
                address = address
                // registerationDate and isActive are default
            };

            context.Users.Add(newUser);
            context.SaveChanges();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nNew User Registered Successfully.");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"User ID = {newUser.userId}");
            Console.ResetColor();
        }

        // 02 Add a New Product to a Category
        static void AddProductToCategory()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                DisplayHeader("Add a New Product to a Category");
                Console.ResetColor();

                var categories = context.Categories.ToList();

                Console.WriteLine("\nSelect Category: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (var category in categories)
                {
                    Console.WriteLine($"[{category.categoryId}] {category.categoryName}");
                }
                Console.ResetColor();

                Console.Write("\nEnter Category ID: ");
                if (!int.TryParse(Console.ReadLine(), out int inputId) || !categories.Any(c => c.categoryId == inputId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid Category ID. Press Enter.");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                int targetCategoryId = inputId;

                // Product Name Input (Required)
                Console.Write("\nEnter Product Name: ");
                string productName = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(productName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  Error: Product name is required. Press Enter.");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                // Description Input (Optional, Max Length 1000)
                Console.Write("\nEnter Description (Optional): ");
                string description = Console.ReadLine().Trim();
                if (description.Length > 1000)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  Error: Description cannot exceed 1000 characters. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }
                string? finalDescription = string.IsNullOrEmpty(description) ? null : description;

                // Price Input (Required, Range 0.01 to Max)
                Console.Write("\nEnter Price: ");
                if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0.01m)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  Error: Invalid price. Must be a positive decimal value (min 0.01). Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                // Stock Quantity Input (Optional, Defaults to 0, Min 0)
                Console.Write("\nEnter Stock Quantity (Press Enter for 0): ");
                string stockInput = Console.ReadLine().Trim();
                int stockQuantity = 0; // Baseline default match
                if (!int.TryParse(stockInput, out stockQuantity) || stockQuantity < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  Error: Invalid quantity. Must be a whole number 0 or greater. Press Enter.");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                // Image URL Input (Optional, Max Length 300)
                Console.Write("\nEnter Image URL (Optional): ");
                string imageUrl = Console.ReadLine().Trim();
                if (imageUrl.Length > 300)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("  Error: Image URL cannot exceed 300 characters.");
                    Console.ResetColor();
                    return;
                }
                string? finalImageUrl = string.IsNullOrEmpty(imageUrl) ? null : imageUrl;

                Product newProduct = new Product
                {
                    productName = productName,
                    description = description,
                    price = price,
                    stockQuantity = stockQuantity,
                    imageUrl = finalImageUrl,
                    categoryId = targetCategoryId
                };

                context.Products.Add(newProduct);
                context.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nSuccess: '{newProduct.productName}' Product ID {newProduct.productId}.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }


        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                DisplayHeader("E-COMMERCE SYSTEM EF CORE - MAIN MENU");
                Console.WriteLine("Select an operation to perform:\n");

                // Easy Operations
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  1. Register a New User");
                Console.WriteLine("  2. Add a New Product to a Category");
                Console.WriteLine("  4. Write a Product Review");
                Console.WriteLine("  5. Update Product Price and Availability");
                Console.WriteLine("  7. Delete a Review");
                Console.WriteLine("  8. View All Products");
                Console.WriteLine("  9. Filter Products by Category and Price Range\n");

                // Medium Operations
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  3. Place an Order");
                Console.WriteLine("  6. Cancel an Order");
                Console.WriteLine(" 10. Get Category with All Its Products\n");

                // Hard Operations
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" 11. View Order History with Full Details");
                Console.WriteLine(" 12. Product Summary Report\n");

                Console.ResetColor();
                Console.WriteLine("  0. Exit");
                Console.WriteLine("==================================================");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                Console.Clear();
                Console.ResetColor();

                switch (choice)
                {
                    case "1":
                        RegisterNewUser();
                        break;
                    case "2":
                        AddProductToCategory();
                        break;
                    case "3":
                        //PlaceOrder();
                        break;
                    case "4":
                        //WriteProductReview();
                        break;
                    case "5":
                        //UpdateProductPriceAndAvailability();
                        break;
                    case "6":
                        //CancelOrder();
                        break;
                    case "7":
                        //DeleteReview();
                        break;
                    case "8":
                        //ViewAllProducts();
                        break;
                    case "9":
                        //FilterProducts();
                        break;
                    case "10":
                        //GetCategoryWithProducts();
                        break;
                    case "11":
                        //ViewOrderHistory();
                        break;
                    case "12":
                        //GenerateProductSummaryReport();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                if (!exit)
                {
                    Console.ResetColor();
                    Console.WriteLine("\nPress any key to return to the main menu...");
                    Console.ReadKey();
                }
            }
        }
    }
}
