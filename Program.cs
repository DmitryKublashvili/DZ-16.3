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
            int[,] array1 = MatrixCreate(1000, 2000);
            int[,] array2 = MatrixCreate(2000, 20000);

            Console.WriteLine("Initial matrices are formed\n");
            //PrintArray(array1);
            //PrintArray(array2);

            //////////////////////////////////// 1 поток \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            Console.WriteLine("Matrix multiplication in method 1 (without multithreading)...");

            MatrixMultiplier matrixMultiplier = new MatrixMultiplier(array1, array2);

            matrixMultiplier.Calculate();

            Console.WriteLine($"\nThe resulting matrix is built 1 way (one thread) in {matrixMultiplier.GetRunninTime()} seconds.\n");

            //matrixMultiplier.PrintResult();

            //////////////////////////////////// Многопоточность \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

            Console.WriteLine("\nMultiply matrices in method 2 (using multithreading)...");

            MatrixMultiplier_UsingMultiTasks multiplier_UsingMultiTasks = new MatrixMultiplier_UsingMultiTasks(array1, array2);

            multiplier_UsingMultiTasks.Calculate();

            Console.WriteLine($"\nThe resulting matrix is constructed in method 2 in (using multithreading) за {multiplier_UsingMultiTasks.GetRunninTime()} seconds.\n");

            // multiplier_UsingMultiTasks.PrintResult();

            Console.WriteLine("Calculations completed");
            Console.ReadKey();
        }
    }
}
