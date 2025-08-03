using System;

class Program 
{
    static void Main() {
        Console.WriteLine("Working....");

        Dog dog = new Dog();
        Cat cat = new Cat();

        dog.MakeSound();
        cat.MakeSound();
    }
}

class Animal {
    public virtual void MakeSound()
    {
        Console.WriteLine("The Animal goes *rarrr*");
    }
}


class Dog : Animal {
    public override void MakeSound ()
    {
        Console.WriteLine("The dog goes *woof*");
        Console.WriteLine("The dog Barks");
    }
}

class Cat : Animal {
    public override void MakeSound ()
    {
        Console.WriteLine("The Cat goes *meow*");
        Console.WriteLine("The Cat *meows*");
    }
}