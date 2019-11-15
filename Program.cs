using System;

namespace Hooke_Jeeves
{
    class Program
    {
        static double Sq(double x)
        {
            return (Math.Cos(x));
        }
        static double Xy(double[] coords)
        {
            if((1 - Math.Pow(coords[0], 2) - Math.Pow(coords[1], 2))<0)
            {
                return 999999;
            }
            return (Math.Sqrt(1 - Math.Pow(coords[0], 2) - Math.Pow(coords[1], 2)));
        }
        delegate double Func(double[] coords);
        static double[] HookJeeves(double leftX, double rightX, double startStep, double endStep, Func func, int count)
        {
            double step = startStep;
            double[] coords = new double[count];
            for (int i = 0; i < count; i++)
            {
                coords[i] = 0;
            }
            for (int i = 0; i < count; i++)
            {
                double lastResult = func(coords);
                step = startStep;
                while (Math.Abs(step) > endStep)
                {
                    coords[i] += step;
                    Console.WriteLine($"coords[{i}]={coords[i]} step={step} lr={lastResult} func={func(coords)}");
                    if (coords[i] >= rightX)
                    {
                        coords[i] -= step;
                        step = -step;
                    }
                    else
                    {
                        if (coords[i] > leftX)
                        {
                            if (func(coords) > lastResult)
                            {
                                coords[i] -= step;
                                step = -step;
                                if (step > 0)
                                {
                                    step = step / 2;
                                }
                            }
                            else
                            {
                                if(func(coords)!=Math.Sqrt(-1))
                                lastResult = func(coords);
                            }
                        }
                        else
                        {
                            step = endStep;
                        }
                    }
                }
            }
            return coords;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Func func = Sq;
            double[] oout = HookJeeves(-100, 100, 10, 0.01, Xy, 2);
            Console.WriteLine($"x={oout[0]} y={oout[1]} f={Xy(oout)}");
        }
    }
}
