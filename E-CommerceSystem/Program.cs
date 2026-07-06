namespace E_CommerceSystem
{
    internal class Program
    {

        public static ECommerceContext context = new ECommerceContext();

        static void DisplayHeader(string header)
        {
            Console.Clear();
            Console.ResetColor();
            Console.WriteLine("==================================================");
            Console.WriteLine($"       {header}      ");
            Console.WriteLine("==================================================");
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
                        //RegisterNewUser();
                        break;
                    case "2":
                        //AddProductToCategory();
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
