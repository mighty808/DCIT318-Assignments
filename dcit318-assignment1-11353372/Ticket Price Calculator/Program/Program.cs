using System;

class Program
{
    static void Main()
{
    bool keepRunning = true;

    while (keepRunning) {
        
        Console.WriteLine("What is your Age: ");
    string age = Console.ReadLine();     

    bool isValid = int.TryParse(age, out int input);
    Console.WriteLine(input);


    if (isValid){
        int ticket = 10;
    
    {
        if (input > 64 && input < 130 || input < 13) {
        Console.WriteLine("You have received a discounted price of GHC7");
        int values = ticket - 7;
        Console.WriteLine("Total Amount: " + "GHC" + values);

    }

    else if (input > 12 && input < 65){
        Console.WriteLine("You do not qualify for a discount");
        Console.WriteLine("Total Amount: " + "GHC" + ticket);

    }

    else{
        Console.WriteLine("Enter a Valid Age");
    }


}
    }

        Console.WriteLine("Do you want to enter Again? (y/n)");
        string checkAgain = Console.ReadLine();

        if (checkAgain.ToLower() != "y"){
            keepRunning = false;
        }
    }

    Console.WriteLine("Thank you Bye");

    
}}