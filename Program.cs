using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _16._3
{
    public class Program
    {
        static Random random = new Random();

        static void PrintArray(int[,] array)
        {
            if (array == null)
            {
                Console.WriteLine("Матрицы не существует");
                return;
            }

            int rows = array.GetLength(0);
            int columns = array.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{array[i, j],3}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static int[,] MatrixCreate(int rows, int columns)
        {
            int[,] createdArray = new int[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    createdArray[i, j] = random.Next(4);
                }
            }
            return createdArray;
        }


        static int[,] MatrixMultyplay(int[,] array1, int[,] array2)
        {
            int rows1 = array1.GetLength(0);
            int columns1 = array1.GetLength(1);

            int rows2 = array2.GetLength(0);
            int columns2 = array2.GetLength(1);

            if (columns1 != rows2)
            {
                Console.WriteLine("Матрицы не сопоставимы");
                return null;
            }

            int[,] resultArray = new int[rows1, columns2];

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < columns2; j++)
                {
                    for (int k = 0; k < columns1; k++)
                    {
                        resultArray[i, j] += array1[i, k] * array2[k, j];
                    }
                }
            }
            return resultArray;
        }

        static void Main(string[] args)
        {
            int[,] array1 = MatrixCreate(1000, 2000);
            int[,] array2 = MatrixCreate(2000, 30000);


            Console.WriteLine("Исходные матрицы сформированы\n");
            //PrintArray(array1);
            //PrintArray(array2);

            Console.WriteLine("Умножаем матрицы...");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int[,] multArray = MatrixMultyplay(array1, array2);

            stopwatch.Stop();

            Console.WriteLine($"Результирующая матрица построена за {stopwatch.ElapsedMilliseconds / 1000} секунд\n");
            //PrintArray(multArray);

            Console.WriteLine("Вычисления завершены");

            Console.ReadKey();
        }
    }
}
