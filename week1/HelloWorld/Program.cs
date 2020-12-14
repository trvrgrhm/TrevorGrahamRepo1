using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Please enter a word.");
            string word = Console.ReadLine();
            Console.WriteLine("You said \""+word+"\"!!!");
        }
    }
}
