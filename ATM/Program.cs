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
//            decimal balance;
            Dictionary<string, decimal> accounts = new Dictionary<string, decimal>();
            bool contLoop = true;

            // Add user accounts to the bank accounts dictionary set
            // The key-value pair consists of the user's pin and account balance, respectively
            accounts.Add("4554", 1000.05m);
            accounts.Add("1235", 2800m);
            accounts.Add("6541", 12288.5m);

            Console.Write("Welcome! ");

            // Main program loop
            do
            {
                contLoop = true;

                // Ask user for input
                Console.Write("Please enter 4-digit pin:   ");

                // Call ProcessPin method and set returned string to user's entered pin
                pin = ProcessPin();
                Console.WriteLine();

                // Validate pin input
                // If pin not found, display an error message
                if (!(accounts.ContainsKey(pin)))
                {
                    Console.WriteLine("ERROR: That pin is not valid for an existing account.");
                    Console.WriteLine();
                    continue;
                }

                Console.WriteLine();
                
                // Display main user menu
                Console.WriteLine("What would you like to do?\n"
                                    + " 1. View Balance\n"
                                    + " 2. Deposit\n"
                                    + " 3. Withdraw\n"
                                    + " 4. Exit");
                menuSel = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine();

                // Branch based on user's menu selection
                switch (menuSel)
                {
                    case 1:             // View Balance
                        Console.WriteLine(String.Format("Current account balance: {0:C2}", accounts[pin]));
                        break;
                    case 2:             // Deposit
                        Deposit(accounts, pin);
                        Console.WriteLine(String.Format("Updated account balance: {0:C2}", accounts[pin]));
                        break;
                    case 3:             // Withdraw
                        Withdraw(accounts, pin);
                        Console.WriteLine(String.Format("Updated account balance: {0:C2}", accounts[pin]));
                        break;
                    case 4:             // Exit -- will break out and fall to exit check
                        break;
                    default:
                        Console.WriteLine("ERROR: That is not a valid selection.");
                        break;
                }

                Console.WriteLine();

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

        // Function to deposit a user-specified amount into account
        public static void Deposit(Dictionary<string, decimal> accounts, string pin)
        {
            // Declare local variables
            decimal dep;
            bool contLoop = true;

            // Loop if specified amount to deposit is invalid
            do
            {
                Console.WriteLine("***MAX BALANCE = 99,999.99***");
                Console.Write("Deposit amount:   ");
                dep = Convert.ToDecimal(Console.ReadLine());
                if ((accounts[pin] + dep) >= 99999.99m)
                {
                    Console.WriteLine("ERROR: Total account balance cannot go over $99,999.99.\n"
                                        + "Please enter another amount...");
                    Console.WriteLine();
                    continue;
                }
                accounts[pin] += dep;

                // Allow for exit from loop
                contLoop = false;

            } while (contLoop);
        }

        // Function to withdraw a user-specified amount from account
        public static void Withdraw(Dictionary<string, decimal> accounts, string pin)
        {
            // Declare local variables
            decimal draw;
            bool contLoop = true;

            // Loop if specified amount to withdraw is invalid
            do
            {
                Console.WriteLine(String.Format("***CURRENT BALANCE = {0:N2}***", accounts[pin]));
                Console.Write("Withdrawal amount:   ");
                draw = Convert.ToDecimal(Console.ReadLine());
                if (draw > accounts[pin])
                {
                    Console.WriteLine("ERROR: Total account balance cannot be overdrawn.\n"
                                    + "Please enter another amount...");
                    Console.WriteLine();
                    continue;
                }
                accounts[pin] -= draw;

                Console.WriteLine();
                Console.WriteLine("Thank you! Please take your funds.");

                // Allow for exit from loop
                contLoop = false;

            } while (contLoop);

        }

    }
}
