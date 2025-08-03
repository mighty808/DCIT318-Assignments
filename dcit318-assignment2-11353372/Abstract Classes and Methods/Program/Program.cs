using System;

class Program
{
    static void Main ()
    {
        Console.WriteLine("Abstract Working...");

        Circle circle = new Circle();
        Rectangle rectangle = new Rectangle();
        // Shape shape = new Shape();

        circle.GetArea();
        rectangle.GetArea();
        // shape.GetArea();
    }
}

abstract class Shape
{
    public void GetArea()
    {
        Console.WriteLine("Checking Area");
    }
}



class Circle : Shape
{
    public void GetArea()
    {
        Console.WriteLine("Abstract Circle");
    }
}



class Rectangle : Shape
{
    public void GetArea()
    {
        Console.WriteLine("Abstract Rectangle");
    }
}