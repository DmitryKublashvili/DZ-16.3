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

        /// <summary>
        /// Outputs to the console a 2-dimensional matrix
        /// </summary>
        /// <param name="array"></param>
        static void PrintArray(int[,] array)
        {
            if (array == null)
            {
                Console.WriteLine("There is no matrix");
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

        /// <summary>
        /// Creates a 2-dimensional matrix of the dimensions specified in the parameters
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
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


        static void Main(string[] args)
        {
            int[,] array1 = MatrixCreate(1000, 2001);
            int[,] array2 = MatrixCreate(2000, 20000);

            Console.WriteLine("Исходные матрицы сформированы\n");
            //PrintArray(array1);
            //PrintArray(array2);

            //////////////////////////////////// 1 поток \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            Console.WriteLine("Умножаем матрицы 1 способом...");

            MatrixMultiplier matrixMultiplier = new MatrixMultiplier(array1, array2);

            matrixMultiplier.Calculate();

            Console.WriteLine($"Результирующая матрица построена 1 способом (один поток) за {matrixMultiplier.GetRunninTime()} секунд\n");

            //matrixMultiplier.PrintResult();

            //////////////////////////////////// Многопоточность \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

            Console.WriteLine("\nУмножаем матрицы 2 способом...");

            MatrixMultiplier_UsingMultiTasks multiplier_UsingMultiTasks = new MatrixMultiplier_UsingMultiTasks(array1, array2);

            multiplier_UsingMultiTasks.Calculate();

            Console.WriteLine($"\nРезультирующая матрица построена 2 способом (многопоточность) за {multiplier_UsingMultiTasks.GetRunninTime()} секунд\n");

            // multiplier_UsingMultiTasks.PrintResult();

            Console.WriteLine("Вычисления завершены");
            //Console.WriteLine("Выполняется запись массивов в файлы...");

            //ResultsRecording resultsRecording1 = new ResultsRecording("Matrix-Result-1.txt", matrixMultiplier.GetMatrixResult());
            //ResultsRecording resultsRecording2 = new ResultsRecording("Matrix-Result-2.txt", multiplier_UsingMultiTasks.GetMatrixResult());

            Console.ReadKey();
        }
    }
}
