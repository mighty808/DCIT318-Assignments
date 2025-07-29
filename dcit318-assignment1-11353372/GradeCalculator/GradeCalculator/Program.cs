using System;

class Program
{
    static void Main()
    {
        bool keepRunning = true;

        while (keepRunning)
        {
            Console.WriteLine("Welcome to the Grade Calculator. Please enter your score (0–100):");
            string input = Console.ReadLine();

            bool isValid = int.TryParse(input, out int innerInput);

            if (isValid && innerInput >= 0 && innerInput <= 100)
            {
                Console.WriteLine($"{innerInput} is your Grade");

                if (innerInput >= 90)
                    Console.WriteLine("You get an A");
                else if (innerInput >= 80)
                    Console.WriteLine("You get a B");
                else if (innerInput >= 70)
                    Console.WriteLine("You get a C");
                else if (innerInput >= 60)
                    Console.WriteLine("You get a D");
                else
                    Console.WriteLine("You get an F");
            }
            else
            {
                Console.WriteLine("InValid Number");
                Console.WriteLine("Enter a valid number between 0 and 100.");
            }

            Console.WriteLine("Do you want to enter another grade? (y/n)");
            string checkAgain = Console.ReadLine();

            if (checkAgain.ToLower() != "y")
            {
                keepRunning = false;
            }
        }

        Console.WriteLine("Thank you for using the Grade Calculator!");
    }
}
