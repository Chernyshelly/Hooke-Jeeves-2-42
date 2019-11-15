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
        static double[] HookJeeves(double[] leftX, double[] rightX, double[] startCoords, double[] startStep, double[] endStep, Func func, int count)
        {
            double step = startStep[0];
            double[] coords = startCoords;
            for (int i = 0; i < count; i++)
            {
                double lastResult = func(coords);
                step = startStep[i];
                while (Math.Abs(step) > endStep[i])
                {
                    coords[i] += step;
                    Console.WriteLine($"coords[{i}]={coords[i]} step={step} lr={lastResult} func={func(coords)}");
                    if (coords[i] >= rightX[i])
                    {
                        coords[i] -= step;
                        step = -step;
                    }
                    else
                    {
                        if (coords[i] > leftX[i])
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
                            step = endStep[i];
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
            double[] ms = new double[2] { 0, 0};
            double[] startSteps = new double[2] { 10, 10 };
            double[] endSteps = new double[2] { 0.001, 0.001 };
            double[] LeftX = new double[2] { -100, -100 };
            double[] RightX = new double[2] { 100, 100 };
            double[] oout = HookJeeves(LeftX, RightX, ms, startSteps, endSteps, Xy, 2);
            Console.WriteLine($"x={oout[0]} y={oout[1]} f={Xy(oout)}");
        }
    }
}
