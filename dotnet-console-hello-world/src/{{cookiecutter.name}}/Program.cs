using System;

namespace HelloWorldTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Component ID: {{cookiecutter.name}}");
            Console.WriteLine("Description: {{cookiecutter.description}}");
        }
    }
}
