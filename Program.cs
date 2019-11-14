using System;

namespace Hooke_Jeeves
{
    class Program
    {
        static int Sq(int x)
        {
            return x * x;
        }
        delegate int Func(int x);
        static int User(int x, Func func)
        {
            return func(x);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Func func = Sq;
            Console.WriteLine(User(3, Sq));
        }
    }
}
