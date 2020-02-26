using System;

namespace Hooke_Jeeves
{
    class Program
    {
        static bool AreAlmostEqual(double[] a, double[] b)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (Math.Round(a[i], 4) != Math.Round(b[i], 4))
                {
                    return false;
                }
            }
                
            return true;
        }
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

        static double[] dot3Founder(double[] coords1, double[] coords2, double lambda)
        {
            double[] coords = coords1;
            for (int i = 0; i < coords1.Length; i++)
            {
                coords[i] = coords[i] + ((coords2[i] - coords[i]) * lambda);
            }
            return coords;
        }
        static double Sq(double x)
        {
            return (x*x);
        }
        static double Tryy(double[] coords)
        {
            double x = coords[0];
            double y = coords[1];
            return (x*x+y*y*y);
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
            bool skipFirstStage = false;
            double[] dot1 = startCoords;
            double[] dot2 = startCoords;
            double[] dot3;
            double[] dot4;
            double[] curStep = startStep;
            while (true)
            {
                if (skipFirstStage == false)
                {
                    while (IsLesser(endStep, curStep))
                    {
                        for (int i = 0; i < count; i++)
                        {
                            double[] tempDot = new double[count];
                            Array.Copy(dot1, tempDot, count);
                            tempDot[i] = dot1[i] + curStep[i];
                            double plusFunc = func(tempDot);
                            tempDot[i] = dot1[i] - curStep[i];
                            double minusFunc = func(tempDot);
                            if ((plusFunc < func(dot1)) && (plusFunc < minusFunc))
                            {
                                dot2[i] = dot2[i] + curStep[i];
                            }
                            else
                            {
                                if ((minusFunc < func(dot1)) && (minusFunc < plusFunc))
                                {
                                    dot2[i] = dot2[i] - curStep[i];
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
                        // Console.WriteLine("zAZAZ");
                    }
                    Console.WriteLine("AzZAZ");
                    if (quit == true)
                    {
                        return dot2;
                    }
                }
                Console.WriteLine("AZAZa");
                dot3 = dot3Founder(dot1, dot2, 2);
                dot4 = new double[count];
                Array.Copy(dot3, dot4, count);
                for (int i = 0; i < count; i++)
                {
                    double[] tempDot = new double[count];
                    Array.Copy(dot1, tempDot, count);
                    tempDot[i] = dot3[i] + curStep[i];
                    double plusFunc = func(tempDot);
                    tempDot[i] = dot3[i] - curStep[i];
                    double minusFunc = func(tempDot);
                    if ((plusFunc < func(dot3)) && (plusFunc < minusFunc))
                    {
                        dot4[i] = dot4[i] + curStep[i];
                    }
                    else
                    {
                        if ((minusFunc < func(dot3)) && (minusFunc < plusFunc))
                        {
                            dot4[i] = dot4[i] - curStep[i];
                        }
                    }
                    if (quit == true)
                    {
                        return dot4;
                    }
                }
                Console.WriteLine("AZAZ");
                    if (AreAlmostEqual(dot3, dot4))
                    {
                        Array.Copy(dot2, dot1, count);
                        Array.Copy(dot4, dot2, count);
                        skipFirstStage = true;
                    }
                    else
                    {
                        Array.Copy(dot2, dot1, count);
                    }

                quit = true;
            }
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
            Console.WriteLine($"x={oout[0]} y={oout[1]} f={Tryy(oout)}");
        }
    }
}
