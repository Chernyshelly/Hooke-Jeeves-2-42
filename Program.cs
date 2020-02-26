using System;

namespace Hooke_Jeeves
{
    class Program
    {
        static bool IsLesser(double[] a, double[] b)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if(b[i] <= a[i])
                {
                    return false;
                }
            }

            return true;
        }
        static double Sq(double x)
        {
            return (Math.Cos(x));
        }
        static double Tryy(double[] coords)
        {
            double x = coords[0];
            double y = coords[1];
            return (8 * x * x + 4 * x * y + 5 * y * y);
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
            bool quit = false;
            while(quit == false)
            {
                double[] dot1 = startCoords;
                double[] curStep = startStep;
                while (IsLesser(endStep, curStep))
                {
                    for (int i = 0; i < count; i++)
                    {
                            double[] tempDot = dot1;
                            tempDot[i] = dot1[i] + curStep[i];
                            double plusFunc = func(tempDot);
                            tempDot[i] = dot1[i] - curStep[i];
                            double minusFunc = func(tempDot);
                            if ((plusFunc < func(dot1)) && (plusFunc < minusFunc))
                            {
                                dot1[i] = dot1[i] + curStep[i];
                            }
                            else
                            {
                                if ((minusFunc < func(dot1)) && (minusFunc < plusFunc))
                                {
                                    dot1[i] = dot1[i] - curStep[i];
                                }
                                else
                                {
                                    if (curStep[i] >= endStep[i])
                                    {
                                        curStep[i] = curStep[i] / 2;
                                    }
                                }
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
            double[] ms = new double[2] { -4, -4};
            double[] startSteps = new double[2] { 10, 10 };
            double[] endSteps = new double[2] { 0.1, 0.1 };
            double[] LeftX = new double[2] { -100, -100 };
            double[] RightX = new double[2] { 100, 100 };
            double[] oout = HookJeeves(LeftX, RightX, ms, startSteps, endSteps, Tryy, 2);
            Console.WriteLine($"x={oout[0]} y={oout[1]} f={Xy(oout)}");
        }
    }
}
