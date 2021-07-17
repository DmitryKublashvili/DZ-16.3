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

        /// <summary>
        /// Сhecks the equivalence of matrices 
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <returns></returns>
        static bool AreMatrixcesEquivalent(int[,] array1, int[,] array2)
        {
            int rows1 = array1.GetLength(0);
            int columns1 = array1.GetLength(1);
            int rows2 = array2.GetLength(0);
            int columns2 = array2.GetLength(1);

            if (rows1 != rows2 || columns1 != columns2)
            {
                return false;
            }

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < columns1; j++)
                {
                    if (array1[i, j] != array2[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        static void Main(string[] args)
        {
            int[,] array1 = MatrixCreate(100, 2000);
            int[,] array2 = MatrixCreate(2000, 3000);

            Console.WriteLine("Initial matrices are formed\n");
            //PrintArray(array1);
            //PrintArray(array2);

            //////////////////////////////////// 1 поток \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            //Console.WriteLine("Matrix multiplication in method 1 (without multithreading)...");

            //MatrixMultiplier matrixMultiplier = new MatrixMultiplier(array1, array2);

            //matrixMultiplier.Calculate();

            //Console.ForegroundColor = ConsoleColor.Cyan;
            //Console.WriteLine($"\nThe resulting matrix is built in method 1 (one thread) in {matrixMultiplier.GetRunninTime()} seconds.\n");
            //Console.ResetColor();

            //matrixMultiplier.PrintResult();

            //////////////////////////////////// Многопоточность \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\

            Console.WriteLine("\nMultiply matrices in method 2 (using multithreading)...");

            MatrixMultiplier_UsingMultiTasks multiplier_UsingMultiTasks = new MatrixMultiplier_UsingMultiTasks(array1, array2);

            multiplier_UsingMultiTasks.Calculate();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nThe resulting matrix is constructed in method 2 (using multithreading) in {multiplier_UsingMultiTasks.GetRunninTime()} seconds.\n");
            Console.ResetColor();

            //multiplier_UsingMultiTasks.PrintResult();

            Console.WriteLine("Calculations completed");

            Console.WriteLine("Equivalence check ...");

            //Console.WriteLine(AreMatrixcesEquivalent(matrixMultiplier.GetMatrixResult(), multiplier_UsingMultiTasks.GetMatrixResult()));

            Console.ReadKey();
        }
    }
}
