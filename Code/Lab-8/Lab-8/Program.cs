using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_8
{
    internal class Program
    {
        /// <summary>
        /// Функція обчислення заданої функції
        /// </summary>
        static double calc_fun(double x, double y)
        {
            return Math.Cos(1.4 * x + y) + (x - y);
        }

        /// <summary>
        /// Вивід розділювальної лінії
        /// </summary>
        static void PrintSep()
        {
            Console.WriteLine(string.Concat(Enumerable.Repeat("-", 75)));
        }

        static void RungeKuttMethod(double a, double b, double x0, double y0)
        {
            double h = 0.1; //Задамо початковий крок 0.1
            int M = 10;

            double k1 = 0;
            double k2 = 0;
            double k3 = 0;
            double k4 = 0;

            double k1_r = 0;
            double k2_r = 0;
            double k3_r = 0;
            double k4_r = 0;

            double del_y = 0;
            double del_y_r = 0;

            Console.WriteLine("Номер iтерацiї\tx\ty\t\t\tdelta_y");

            for (int i = 0; i < M; i++)
            {
                k1 = h * calc_fun(x0, y0);
                k1_r = (h / 2) * calc_fun(x0, y0);

                k2 = h * calc_fun(x0 + (h / 2), y0 + k1 / 2);
                k2_r = (h / 2) * calc_fun(x0 + (h / 4), y0 + k1 / 2);


                k3 = h * calc_fun(x0 + (h / 2), y0 + k2 / 2);
                k3_r = (h / 2) * calc_fun(x0 + (h / 4), y0 + k2 / 2);

                k4 = h * calc_fun(x0 + h, y0 + k3);
                k4_r = (h / 2) * calc_fun(x0 + (h / 2), y0 + k3);


                if (Math.Abs((k2 - k3) / (k1 / k2)) > Math.Pow(10, -2))
                {
                    h /= 2;
                    M *= 2;
                }

                del_y = (k1 + (2 * k2) + (2 * k3) + k4) * (1.0 / 6.0);
                del_y_r = (k1_r + (2 * k2_r) + (2 * k3_r) + k4_r) * (1.0 / 6.0);

                y0 += del_y;
                x0 += h;
                Console.WriteLine($"{i + 1}\t\t{Math.Round(x0, 5)}\t{y0}\t{del_y}\t{(del_y - del_y_r) / 15}");
                PrintSep();
            }

        }

        static void AdamsMethod(double x0, double y0)
        {
            int k = 3; // Початок ітерацій
            double h = 0.1; // Початковий крок

            // Початковий список, перші взяті перші 3 знач. із минулого метода
            List<double> y = new List<double> { 0, 0.09907029408912865, 0.19290313803369596, 0.2774320639668538 };
            List<double> x = new List<double> { 0, 0.1, 0.2, 0.3 };
            List<double> error = new List<double> {0, 0, 0, 0};

            double y_predicted, y_adjusted, y_predicted_half, y_adjusted_half;

            Console.WriteLine("Номер iтерацiї\tx\ty\t\t\tпомилка");

            do
            {
                // Прогнозоване значення з кроком h. За формулою (4)
                y_predicted = y[k] + (h / 24) * (55 * calc_fun(x[k], y[k]) - 59 * calc_fun(x[k - 1], y[k - 1]) + 37 * calc_fun(x[k - 2], y[k - 2]) - 9 * calc_fun(x[k - 3], y[k - 3]));

                // Скориговане значення з кроком h. За формулою (5)
                y_adjusted = y[k] + (h / 24) * (9 * y_predicted + 19 * calc_fun(x[k], y[k]) - 5 * calc_fun(x[k - 1], y[k - 1]) + calc_fun(x[k - 2], y[k - 2]));

                // Прогнозоване значення з кроком h/2
                double h_half = h / 2;
                y_predicted_half = y[k] + (h_half / 24) * (55 * calc_fun(x[k], y[k]) - 59 * calc_fun(x[k - 1], y[k - 1]) + 37 * calc_fun(x[k - 2], y[k - 2]) - 9 * calc_fun(x[k - 3], y[k - 3]));

                // Скориговане значення з кроком h/2
                y_adjusted_half = y[k] + (h_half / 24) * (9 * y_predicted_half + 19 * calc_fun(x[k], y[k]) - 5 * calc_fun(x[k - 1], y[k - 1]) + calc_fun(x[k - 2], y[k - 2]));

                // Оцінка похибки методом Рунте за правилом Рунге (8)
                error.Add(Math.Abs((y_predicted - y_predicted_half) / 15.0));

                if (Math.Abs(y_adjusted - y_predicted) <= Math.Pow(10, -4))
                {
                    h /= 2; // Перевірка на похибку 
                    y_predicted = y_adjusted;
                }

                x.Add(x[k] + h); // Запис у масив
                y.Add(y_adjusted);
                k++;
            } while (Math.Round(x[k], 5) <= 1);

            for (int i = 0; i < k; i++)
            {
                Console.WriteLine($"{i}\t\t{Math.Round(x[i], 5)}\t{y[i]}\t{error[i]}");
                PrintSep();
            }
        }

        static void Main(string[] args)
        {
            PrintSep();
            Console.WriteLine("\tЛабораторна робота #8");
            Console.WriteLine("Виконав студент групи IC-31 Коваль Богдан");
            PrintSep();

            double a = 0;
            double b = 1;

            double x0 = 0;
            double y0 = 0;

            Console.WriteLine("Метод Рунге-Кутта:");
            PrintSep();
            RungeKuttMethod(a, b, x0, y0);

            Console.WriteLine("\n");

            Console.WriteLine("Метод Адамса:");
            AdamsMethod(x0, y0);
        }
    }
}
