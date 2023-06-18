using System;

namespace Bypass_Metods
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /* Создаем матрицу размером n на m
             * и заполняем ее рандомными целочисленными значениями от 0 до 100.
             * Полученную матрицу выводим в консоль.
             */
            int n = 5, m = 5;
            Random rnd = new Random();
            int[,] mas = new int[n, m];
            Console.Write("Матрица размером n:{0}; и m:{1};\n", n, m);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    mas[i, j] = rnd.Next(0, 100);
                    Console.Write(mas[i, j] + "\t");
                }
                Console.WriteLine();
            }


            /* С помощью метода спирального обхода создаем новый одномерный массив
             * где содержатся значения матрицы обойденные по спирали.
             * Полученный массив выводим в консоль.
             */
            Console.Write("\nМассив выпрямленных значений матрицы:\n");
            int[] new_mas = BypassMetods.Spiral(mas);
            for (int i = 0; i < new_mas.Length; i++)
            {
                Console.Write(new_mas[i] + "  ");
            }
            
            Console.Read();
        }
    }
}