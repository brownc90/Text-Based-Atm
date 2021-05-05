using System;
using System.Collections.Generic;
using System.Text;

namespace ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            // Customize console
            // Set the title of the console window
            Console.Title = "ATM Machine";

            // Set colors
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.DarkGray;

            // Declare variables
            string pin;
            int menuSel;
            double balance;
            Dictionary<string, double> accounts = new Dictionary<string, double>();
            bool contLoop = true;

            // Add user accounts to the bank accounts dictionary set
            // The key-value pair consists of the user's pin and account balance, respectively
            accounts.Add("4554", 1000.05);
            accounts.Add("1235", 2800.00);
            accounts.Add("6541", 12288.50);

            Console.Write("Welcome! ");

            do
            {
                contLoop = true;

                // Ask user for input
                Console.WriteLine("Please enter 4-digit pin:");

                // Call processPin method and set returned string to user's entered pin
                pin = ProcessPin();
                Console.WriteLine();

                // Validate pin input
                // If pin not found, display an error message
                if (!(accounts.ContainsKey(pin)))
                {
                    Console.WriteLine("ERROR: That pin is not valid for an existing account.");
                    continue;
                }
                
                // Display main user menu
                Console.WriteLine("What would you like to do?"
                                    + "\n\n"
                                    + " 1. View Balance\n"
                                    + " 2. Deposit\n"
                                    + " 3. Withdraw\n"
                                    + " 4. Exit");
                menuSel = Convert.ToInt32(Console.ReadLine());

                // Branch based on user's menu selection
                switch (menuSel)
                {
                    case 1:             // View Balance
                        Console.WriteLine("Balance");
                        break;
                    case 2:             // Deposit
                        Console.WriteLine("Deposit");
                        break;
                    case 3:             // Withdraw
                        Console.WriteLine("Withdraw");
                        break;
                    case 4:             // Exit
                    case 4:             // Exit -- will break out and fall to exit check
                        break;
                    default:
                        Console.WriteLine("ERROR: That is not a valid selection.");
                        break;
                }

                // Check if user wants to exit
                Console.WriteLine("Exit? Y/N: ");
                if (Console.ReadLine().Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                    contLoop = false;

            } while (contLoop);

            Console.ReadKey();
        }


        // Function to manage user keystrokes when entering pin
        // and return the pin as a string
        public static string ProcessPin()
        {
            StringBuilder sb = new StringBuilder();
            ConsoleKeyInfo keyInfo;

            // Loop thru each keystroke and replace w/ '*', if not a control character
            // and until 'Enter' key is pressed
            do
            {
                // Elect not to display character
                keyInfo = Console.ReadKey(true);

                if (!char.IsControl(keyInfo.KeyChar))
                {
                    sb.Append(keyInfo.KeyChar);
                    Console.Write("*");
                }

            } while (keyInfo.Key != ConsoleKey.Enter);

            return sb.ToString();
        }
    }
}
