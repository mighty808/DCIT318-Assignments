using System;

class Program
{
    static void Main()
    {
        bool keepRunning = true;
        
        while (keepRunning) {
            Console.WriteLine("Enter The Value for the first Side: ");
        string firstSide = Console.ReadLine();

        Console.WriteLine("Enter The Value for the Second Side: ");
        string secondSide = Console.ReadLine();

        Console.WriteLine("Enter The Value for the Third Side: ");
        string thirdSide = Console.ReadLine();

        Console.WriteLine(firstSide + " " + secondSide + " " + thirdSide);


        if ((firstSide == secondSide && firstSide == thirdSide) && secondSide == thirdSide){
            Console.WriteLine("All three sides are equal");
            Console.WriteLine("This is an Equilateral Triangle");
        }
        
        else if ((firstSide != secondSide && firstSide != thirdSide) && secondSide != thirdSide){
            Console.WriteLine("No equal sides");
            Console.WriteLine("This is an Scalene Triangle");
        }
       
        else if (
                (firstSide == secondSide && firstSide != thirdSide) ||
                (firstSide == thirdSide && firstSide != secondSide) ||
                (secondSide == thirdSide && secondSide != firstSide))   
        {
            Console.WriteLine("Two sides equal sides");
            Console.WriteLine("This is an Isosceles Triangle");
        }

        Console.WriteLine("Do you want to enter Again? (y/n)");
        string checkAgain = Console.ReadLine();

        if (checkAgain.ToLower() != "y"){
            keepRunning = false;
        }
        
        }
            Console.WriteLine("Thank you Bye");

    }
}