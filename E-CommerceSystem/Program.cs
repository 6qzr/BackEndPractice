using E_CommerceSystem.Models;
using Isopoh.Cryptography.Argon2;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
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

        static int GetUserID()
        {
            Console.Write("\nEnter User ID: ");
            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Invalid User ID. Press Enter.");
                Console.ReadLine();
                Console.ResetColor();
                return -1;
            }

            var user = context.Users.FirstOrDefault(u => u.userId == userId && u.isActive);
            if (user == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  User not found or inactive. Press Enter.");
                Console.ReadLine();
                Console.ResetColor();
                return -1;
            }

            return userId;
        }
        
        // 03 Place an Order
        static void PlaceOrder()
        {
            // Keep track of the newly initialized order entity out of local scopes to handle cleanup if needed
            Order newOrder = null;
            try
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                DisplayHeader("Place an Order");
                Console.ResetColor();

                // 1. Verify User ID
                int userId = GetUserID();
                if (userId == -1) return;

                // 2. Fetch Available Products (Early Check)
                List<Product> products = context.Products
                                                .Where(p => p.stockQuantity > 0 && p.isAvailable)
                                                .ToList();

                if (!products.Any())
                {
                    Console.WriteLine("\nNo products are currently available for purchase. Press Enter");
                    Console.ReadLine();
                    return;
                }

                // 3. Collect Shipping Information (Moved up to pass database requirement validation)
                Console.Write("\nEnter Shipping Address: ");
                string shippingAddress = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(shippingAddress) || shippingAddress.Length > 300)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Error: Shipping address cannot be empty or exceed 300 characters. Process Aborted. Press Enter");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                // 4. Collect Payment Method Options (Moved up to pass database requirement validation)
                Console.WriteLine("\nSelect a Payment Method: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" [1] Cash\n [2] Card\n [3] Apple Pay");
                Console.ResetColor();

                Console.Write("\nEnter option number (1-3): ");
                string input = Console.ReadLine().Trim();
                string paymentMethod;

                switch (input)
                {
                    case "1": paymentMethod = "Cash"; break;
                    case "2": paymentMethod = "Card"; break;
                    case "3": paymentMethod = "Apple Pay"; break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n  Error: Invalid choice. Process Aborted. Press Enter");
                        Console.ReadLine();
                        Console.ResetColor();
                        return;
                }

                // Create and save the Order record first to obtain the orderId (Now valid with required fields)
                newOrder = new Order
                {
                    userId = userId,
                    shippingAddress = shippingAddress,
                    paymentMethod = paymentMethod,
                };
                context.Orders.Add(newOrder);
                context.SaveChanges();

                // 5. Display Product Section
                Console.WriteLine("\nAvailable Products\n");
                Console.WriteLine(string.Format("{0,-6} | {1,-25} | {2,-12} | {3,-8}", "ID", "Product Name", "Price", "Stock"));
                Console.WriteLine("--------------------------------------------------------------------------");

                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (Product product in products)
                {
                    Console.WriteLine(string.Format(
                        "{0,-6} | {1,-25} | {2,-12:C} | {3,-8}",
                        product.productId,
                        product.productName.Length > 25 ? product.productName.Substring(0, 22) + "..." : product.productName,
                        product.price,
                        product.stockQuantity
                    ));
                }
                Console.ResetColor();
                Console.WriteLine("==========================================================================");

                decimal accumulatedTotal = 0;
                bool hasItemsInCart = false;

                // Let the user add multiple products via loop
                while (true)
                {
                    Console.Write("\nEnter Product ID (Press Enter to checkout): ");
                    Console.ResetColor();
                    string productInput = Console.ReadLine();

                    // Check if user pressed Enter on blank input to initiate checkout phase
                    if (string.IsNullOrWhiteSpace(productInput))
                    {
                        if (!hasItemsInCart)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n  Your order is empty. Add at least one item before checkout. Press Enter");
                            Console.ReadLine();
                            Console.ResetColor();
                            continue;
                        }
                        break; // Break selection loop and finalize changes
                    }

                    // Validate Product Selection
                    if (!int.TryParse(productInput, out int productId) || !products.Any(p => p.productId == productId))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n  Invalid Product ID. Please select a valid item from the list. Press Enter");
                        Console.ReadLine();
                        Console.ResetColor();
                        continue;
                    }

                    Product selectedProduct = products.First(f => f.productId == productId);

                    // Validate Quantity against remaining real-time stock limits
                    Console.Write($"Enter quantity for '{selectedProduct.productName}' (In Stock: {selectedProduct.stockQuantity}): ");
                    if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0 || selectedProduct.stockQuantity < quantity)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n  Invalid quantity. Must be greater than 0 and within stock bounds. Press Enter");
                        Console.ReadLine();
                        Console.ResetColor();
                        continue;
                    }

                    var existingOrderItem = context.OrderItems
                                                   .FirstOrDefault(oi => oi.orderId == newOrder.orderId && oi.productId == selectedProduct.productId);

                    if (existingOrderItem != null)
                    {
                        // If it already exists, just accumulate the quantity on the same row
                        existingOrderItem.quantity += quantity;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"  -> Updated existing item. New total quantity: {existingOrderItem.quantity}");
                        Console.ResetColor();
                    }
                    else
                    {
                        // If it's a new item, create a fresh OrderItem bridge record
                        OrderItem newOrderItem = new OrderItem
                        {
                            orderId = newOrder.orderId,
                            productId = selectedProduct.productId,
                            quantity = quantity,
                            unitPrice = selectedProduct.price
                        };
                        context.OrderItems.Add(newOrderItem);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"  -> Added {quantity} x '{selectedProduct.productName}' to checkout queue.");
                        Console.ResetColor();
                    }

                    // Accumulate totalAmount values
                    accumulatedTotal += (selectedProduct.price * quantity);
                    // Decrement stockQuantity on the product entity context references
                    selectedProduct.stockQuantity -= quantity;
                    // Auto-mark unavailable when stock hits zero
                    if (selectedProduct.stockQuantity == 0)
                        selectedProduct.isAvailable = false;
                    hasItemsInCart = true;
                }

                // Update Order total amount field 
                newOrder.totalAmount = accumulatedTotal;

                // Final collective SaveChanges database commitment for all items and product inventory metrics
                context.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nSuccess: New order finalized! Order ID: {newOrder.orderId}. Total: {newOrder.totalAmount:C}.");
                Console.ReadLine();
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                // Cleanup tracking artifact if database context faults out mid-operation
                if (newOrder != null && context.Orders.Contains(newOrder))
                {
                    context.Orders.Remove(newOrder);
                    context.SaveChanges();
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nAn unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }

        static List<User> DisplayAvailableUsers()
        {
            // Fetch active users from the database
            var users = context.Users.Where(u => u.isActive).ToList();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=====================================================================================");
            Console.WriteLine("                               AVAILABLE USERS SYSTEM                                ");
            Console.WriteLine("=====================================================================================");
            Console.ResetColor();

            if (!users.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  No active users found in the system.");
                Console.ResetColor();
                return users;
            }

            // Table Column Headers
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(string.Format("{0,-6} | {1,-18} | {2,-22} | {3,-30}", "ID", "Username", "Full Name", "Email Address"));
            Console.WriteLine("-------------------------------------------------------------------------------------");

            // Print rows safely with text truncation lengths
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var user in users)
            {
                string truncatedName = user.fullName?.Length > 22 ? user.fullName.Substring(0, 19) + "..." : user.fullName ?? "N/A";
                string truncatedEmail = user.email.Length > 30 ? user.email.Substring(0, 27) + "..." : user.email;

                Console.WriteLine(string.Format(
                    "{0,-6} | {1,-18} | {2,-22} | {3,-30}",
                    user.userId,
                    user.username,
                    truncatedName,
                    truncatedEmail
                ));
            }
            Console.ResetColor();
            Console.WriteLine("=====================================================================================");

            return users;
        }
        
        static int GetProductID()
        {
            Console.Write("\nEnter Product ID: ");
            if (!int.TryParse(Console.ReadLine(), out int productId) || !context.Products.Any(p => p.productId == productId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Invalid Product ID. Press Enter.");
                Console.ReadLine();
                Console.ResetColor();
                return -1;
            }
            return productId;
        }

        // 04 Write a Product Review 
        static void WriteProductReview()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                DisplayHeader("Write a Product Review");
                Console.ResetColor();

                // List all available users
                List<User> users = DisplayAvailableUsers();

                int userId = GetUserID();
                if (userId == -1) return;
             
                // List all products
                List<Product> products = DisplayProducts();

                int productId = GetProductID();
                if (productId == -1) return;

                Console.Write("\nEnter Product Rating (1-5): ");
                if(!int.TryParse(Console.ReadLine(), out int rating) || rating > 5 || rating < 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid product rating. Press Enter.");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }

                Console.Write("\nEnter a comment (optional): ");
                string? commentInput = Console.ReadLine().Trim();
                if (commentInput.Length > 1000)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Error: Review comment cannot exceed 500 characters. Press Enter.");
                    Console.ReadLine();
                    Console.ResetColor();
                    return;
                }
                string? finalComment = string.IsNullOrEmpty(commentInput) ? null : commentInput;

                Review newReview = new Review
                {
                    userId = userId,
                    productId = productId,
                    rating = rating,
                    comment = finalComment
                };

                context.Reviews.Add(newReview);
                context.SaveChanges();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nSuccess: New review submitted! Review ID: {newReview.reviewId}.");
                Console.ResetColor();

                Console.WriteLine("\nPress Enter to return...");
                Console.ReadLine();
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


        /* 
         * UPDATE Operations --------------------------------------------------------------------
        */

        // 05 Update product price and availability
        static void UpdateProductPriceAndAvailability()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            DisplayHeader("Update Product Price and Availability");
            Console.ResetColor();

            // List all products
            List<Product> products = DisplayProducts();

            int productId = GetProductID();
            if (productId == -1) return;

            Product selectedProduct = context.Products.FirstOrDefault(p => p.productId == productId);

            Console.Write($"\nEnter new Price (Current: {selectedProduct.price}): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0.01m)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  Error: Invalid price. Must be a positive decimal value (min 0.01). Press Enter");
                Console.ReadLine();
                Console.ResetColor();
                return;
            }
            selectedProduct.price = price;

            Console.Write($"\nUpdate Availability A/U (Current: {(selectedProduct.isAvailable ? "Available" : "Unavailable")}): ");
            string availInput = Console.ReadLine().Trim().ToLower();
            if (availInput != "a" && availInput != "u")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Invalid input. Press Enter.");
                Console.ReadLine();
                Console.ResetColor();
                return;
            }
            selectedProduct.isAvailable = availInput == "a";

            context.SaveChanges();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nSuccess: Product Updated! Price: {selectedProduct.price}  Availability: {(selectedProduct.isAvailable ? "Available" : "Unavailable")}.");
            Console.ResetColor();

            Console.WriteLine("\nPress Enter to return...");
            Console.ReadLine();
        }

        static int GetOrderID()
        {
            Console.Write("\nEnter Order ID: ");
            if (!int.TryParse(Console.ReadLine(), out int orderId) || !context.Orders.Any(o => o.orderId == orderId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Invalid Order ID. Press Enter.");
                Console.ReadLine();
                Console.ResetColor();
                return -1;
            }
            return orderId;
        }
        
        // 06 Cancel an Order
        static void CancelOrder()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            DisplayHeader("Cancel an Order");
            Console.ResetColor();

            int orderId = GetOrderID();
            if (orderId == -1) return;

            Order order = context.Orders.Include(o => o.OrderItems).ThenInclude(p => p.Product).FirstOrDefault(o => o.orderId == orderId);

            foreach(OrderItem item in order.OrderItems)
            {
                Product product = item.Product;
                product.stockQuantity += item.quantity;
                if (!product.isAvailable) product.isAvailable = true;
            }
            
            order.status = "Cancelled";

            context.SaveChanges();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nSuccess: Order {order.orderId} has been cancelled.");
            Console.ResetColor();

            Console.WriteLine("\nPress Enter to return...");
            Console.ReadLine();
        }


        /* 
         * DELETE Operations --------------------------------------------------------------------
        */

        static int GetReviewID()
        {
            Console.Write("\nEnter Review ID: ");
            if (!int.TryParse(Console.ReadLine(), out int reviewId) || !context.Reviews.Any(r => r.reviewId == reviewId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Invalid Review ID. Press Enter.");
                Console.ReadLine();
                Console.ResetColor();
                return -1;
            }
            return reviewId;
        }

        // Delete a Review
        static void DeleteReview()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            DisplayHeader("Delete a Review");
            Console.ResetColor();

            int reviewId = GetReviewID();
            if (reviewId == -1) return;

            Review review = context.Reviews.FirstOrDefault(r => r.reviewId == reviewId);

            context.Reviews.Remove(review);

            context.SaveChanges();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nSuccess: Review with ID {reviewId} has been deleted.");
            Console.ResetColor();

            Console.WriteLine("\nPress Enter to return...");
            Console.ReadLine();
        }


        /* 
         * Get Operations --------------------------------------------------------------------
        */

        // 08 View All Products
        static List<Product> DisplayProducts()
        {
            List<Product> products = context.Products.ToList();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n======================================================================================");
            Console.WriteLine("                                     PRODUCTS                                        ");
            Console.WriteLine("======================================================================================");
            Console.ResetColor();

            if (!products.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  No products found.");
                Console.ResetColor();
                return products;
            }

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(string.Format("{0,-6} | {1,-25} | {2,-12} | {3,-8} | {4,-12}", "ID", "Product Name", "Price", "Stock", "Availability"));
            Console.WriteLine("--------------------------------------------------------------------------------------");
            Console.ResetColor();

            foreach (Product product in products)
            {
                string truncatedProdName = product.productName.Length > 25
                    ? product.productName.Substring(0, 22) + "..."
                    : product.productName;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(string.Format("{0,-6} | {1,-25} | {2,-12:C} | {3,-8} | ",
                    product.productId,
                    truncatedProdName,
                    product.price,
                    product.stockQuantity));

                if (product.isAvailable)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Available");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unavailable");
                }
            }

            Console.ResetColor();
            Console.WriteLine("======================================================================================");

            return products;
        }

        static int GetCategoryID()
        {
            Console.Write("\nEnter Category ID: ");
            if (!int.TryParse(Console.ReadLine(), out int categoryId) || !context.Categories.Any(c => c.categoryId == categoryId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Invalid Category ID. Press Enter.");
                Console.ReadLine();
                Console.ResetColor();
                return -1;
            }
            return categoryId;
        }

        // 09 Filter Products by Category and Price Range
        static void FilterProducts()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            DisplayHeader("Filter Products by Category and Price Range");
            Console.ResetColor();

            int categoryId = GetCategoryID();
            if (categoryId == -1) return;

            Console.Write("\nEnter Maximum Price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal max) || max <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Invalid maximum price. Press Enter.");
                Console.ReadLine();
                Console.ResetColor();
                return;
            }

            Console.Write("\nEnter Minimum Price: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal min) || min < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Invalid minimum price. Press Enter.");
                Console.ReadLine();
                Console.ResetColor();
                return;
            }

            List<Product> products = context.Products
                                            .Where(p => p.categoryId == categoryId && p.price <= max && p.price >= min)
                                            .OrderBy(p => p.price)
                                            .ToList();

            if (!products.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  No products found.");
                Console.ResetColor();
                return;
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(string.Format("{0,-6} | {1,-25} | {2,-12} | {3,-8} | {4,-12}", "ID", "Product Name", "Price", "Stock", "Availability"));
            Console.WriteLine("--------------------------------------------------------------------------------------");
            Console.ResetColor();

            foreach (Product product in products)
            {
                string truncatedProdName = product.productName.Length > 25
                    ? product.productName.Substring(0, 22) + "..."
                    : product.productName;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(string.Format("{0,-6} | {1,-25} | {2,-12:C} | {3,-8} | ",
                    product.productId,
                    truncatedProdName,
                    product.price,
                    product.stockQuantity));

                if (product.isAvailable)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Available");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unavailable");
                }
            }

            Console.ResetColor();
            Console.WriteLine("======================================================================================");

        }

        // 10 Get Category with All Its Products (Include)
        static void GetCategoryWithProducts()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            DisplayHeader("Get Category with All Its Products");
            Console.ResetColor();

            int categoryId = GetCategoryID();
            if (categoryId == -1) return;

            Category category = context.Categories.FirstOrDefault(c => c.categoryId == categoryId);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n======================================================================================");
            Console.WriteLine("                                CATEGORY DETAILS                                     ");
            Console.WriteLine("======================================================================================");
            Console.ResetColor();

            Console.WriteLine(string.Format("{0,-20} {1}", "Category ID:", category.categoryId));
            Console.WriteLine(string.Format("{0,-20} {1}", "Name:", category.categoryName));
            Console.WriteLine(string.Format("{0,-20} {1}", "Description:", category.description ?? "N/A"));
            Console.WriteLine(string.Format("{0,-20} {1}", "Image URL:", category.imageUrl ?? "N/A"));

            Console.WriteLine("======================================================================================");

            if (category.Products.Count == 0) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n  Selected Category has no products yet! Press Enter.");
                Console.ReadLine();
                Console.ResetColor();
                return;
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(string.Format("{0,-6} | {1,-25} | {2,-12} | {3,-8} | {4,-12}", "ID", "Product Name", "Price", "Stock", "Availability"));
            Console.WriteLine("--------------------------------------------------------------------------------------");
            Console.ResetColor();

            foreach (Product product in category.Products)
            {
                string truncatedProdName = product.productName.Length > 25
                    ? product.productName.Substring(0, 22) + "..."
                    : product.productName;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(string.Format("{0,-6} | {1,-25} | {2,-12:C} | {3,-8} | ",
                    product.productId,
                    truncatedProdName,
                    product.price,
                    product.stockQuantity));

                if (product.isAvailable)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Available");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unavailable");
                }
            }

            Console.ResetColor();
            Console.WriteLine("======================================================================================");
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
                        PlaceOrder();
                        break;
                    case "4":
                        WriteProductReview();
                        break;
                    case "5":
                        UpdateProductPriceAndAvailability();
                        break;
                    case "6":
                        CancelOrder();
                        break;
                    case "7":
                        DeleteReview();
                        break;
                    case "8":
                        DisplayProducts();
                        break;
                    case "9":
                        FilterProducts();
                        break;
                    case "10":
                        GetCategoryWithProducts();
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
