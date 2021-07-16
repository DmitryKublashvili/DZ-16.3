using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _16._3
{
    class MatrixMultiplier
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="matrixA"></param>
        /// <param name="matrixB"></param>
        public MatrixMultiplier(int[,] matrixA, int[,] matrixB)
        {
            MatrixA = matrixA;
            MatrixB = matrixB;

            MatrixA_Rows = matrixA.GetLength(0);
            MatrixA_Columns = matrixA.GetLength(1);

            MatrixB_Rows = matrixB.GetLength(0);
            MatrixB_Columns = matrixB.GetLength(1);

            Rows = MatrixA_Rows;
            Columns = MatrixB_Columns;

            MatrixResult = new int[Rows, Columns];

            if (MatrixA_Columns != MatrixB_Rows)
            {
                IsComparable = false;
            }
        }

        private int[,] MatrixA { get; set; }
        private int[,] MatrixB { get; set; }
        private int[,] MatrixResult { get; set; }

        private int MatrixA_Rows { get; set; }
        private int MatrixA_Columns { get; set; }

        private int MatrixB_Rows { get; set; }
        private int MatrixB_Columns { get; set; }

        private int Rows { get; set; }
        private int Columns { get; set; }

        private bool IsComparable { get; set; } = true;
        private bool IsCalculationComplete { get; set; } = false;

        private long RunningTime { get; set; }

        /// <summary>
        /// Performs matrix multiplication
        /// </summary>
        public void Calculate()
        {
            if (!IsComparable)
            {
                Console.WriteLine("Matrixes is not comparable. Multiplication is not possible");
                return;
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.WriteLine("Calculation in process...");

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    for (int k = 0; k < MatrixA_Columns; k++)
                    {
                        MatrixResult[i, j] += MatrixA[i, k] * MatrixB[k, j];
                    }
                }
            }
            IsCalculationComplete = true;

            stopwatch.Stop();

            RunningTime = stopwatch.ElapsedMilliseconds / 1000;

            Console.WriteLine("Calculation completed");
        }

        /// <summary>
        /// Outputs the result to the console
        /// </summary>
        public void PrintResult()
        {
            if (!IsComparable)
            {
                Console.WriteLine("Matrixes is not comparable. Printing is not possible");
                return;
            }

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Console.Write($" {MatrixResult[i, j],3} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Returns the result of matrix multiplication or null if no multiplication was performed
        /// </summary>
        /// <returns>int[,] or null</returns>
        public int[,] GetMatrixResult()
        {
            if (!IsCalculationComplete)
            {
                Console.WriteLine("Calculations are not performed. Perform the calculations using the method Calculate()");
                return null;
            }
            return MatrixResult;
        }

        public long GetRunninTime()
        {
            return RunningTime;
        }
    }
}
