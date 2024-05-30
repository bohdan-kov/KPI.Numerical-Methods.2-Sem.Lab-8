using System;
using System.Linq;
using System.Reflection.Emit;

namespace Lab_8
{
    internal class Program
    {

        static public double calc_fun(double x, double y)
        {
            return Math.Cos(1.4*x+y) + (x - y);
        }

        static void PrintSep()
        {
            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 50)));
        }

        static public void RungeKuttMethod(double a, double b, double x0, double y0)
        {
            double h = 0.1; //Задамо початковий крок 0.1
            int M = 10;

            double k1 = 0;
            double k2 = 0;
            double k3 = 0;
            double k4 = 0;

            double del_y = 0;

            for (int i = 0; i < M; i++)
            {
                k1 = h * calc_fun(x0, y0);
                k2 = h * calc_fun(x0 + (h / 2),y0 + k1 / 2);
                k3 = h * calc_fun(x0 + (h / 2),y0 + k2 / 2);
                k4 = h * calc_fun(x0 + h, y0 + k3);

                Console.WriteLine(Math.Abs((k2 - k3) / (k1 / k2)));
                if (Math.Abs((k2 - k3)/(k1 / k2)) > Math.Pow(10, -2))
                {
                    h /= 2;
                    M *= 2; 
                }

                del_y = (k1 + (2*k2) + (2*k3) + k4) * (1.0 / 6.0);
                y0 += del_y; 
                x0 += h;
                Console.WriteLine($"{i} - x {x0} y {y0}");
            }
            
        }
        static void Main(string[] args)
        {
            PrintSep();
            Console.WriteLine("\tЛабораторна робота #7");
            Console.WriteLine("Виконав студент групи IC-31 Коваль Богдан");
            PrintSep();

            double a = 0;
            double b = 1;

            double x0 = 0;
            double y0 = 0;
            
            RungeKuttMethod(a , b, x0, y0);


        }
    }
}
