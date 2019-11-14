using System;

namespace Hooke_Jeeves
{
    class Program
    {
        static double Sq(double x)
        {
            return (4+3*x);
        }
        delegate double Func(double x);
        static double User(double leftX, double rightX, double startStep, double endStep, Func func)
        {
            double step = startStep;
            double x = 0;
            double lastResult = func(x);
            while(Math.Abs(step) > endStep)
            {
                x += step;
                Console.WriteLine($"x={x} step={step} lr={lastResult} func={func(x)}");
                if(x >= rightX)
                {
                    x -= step;
                    step = -step;
                }
                else
                {
                    if(x > leftX)
                    {
                        if (func(x) > lastResult)
                        {
                            x -= step;
                            step = -step;
                            if (step > 0)
                            {
                                step = step / 2;
                            }
                        }
                        else
                        {
                            lastResult = func(x);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return x;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Func func = Sq;
            Console.WriteLine(User(-100, 100, 10, 0.01, Sq));
        }
    }
}
