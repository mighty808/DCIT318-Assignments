using System;

class Program
{
    static void Main ()
    {
        Console.WriteLine("Interfaces Running...");
        Car car = new Car();
        Bicycle bicycle = new Bicycle();

        car.Move();
        bicycle.Move();

    }
}

    interface IMovable
    {
        void Move();
    }


class Car : IMovable
{
    public void Move()
    {
        Console.WriteLine("Car is Moving...");
    }
}

class Bicycle : IMovable
{
    public void Move()
    {
        Console.WriteLine("Bicycle is moving");
    }
}
