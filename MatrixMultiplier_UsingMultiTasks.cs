using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _16._3
{
    class MatrixMultiplier_UsingMultiTasks
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="matrixA"></param>
        /// <param name="matrixB"></param>
        public MatrixMultiplier_UsingMultiTasks(int[,] matrixA, int[,] matrixB)
        {
            MatrixA = matrixA;
            MatrixB = matrixB;

            MatrixA_Rows = matrixA.GetLength(0);
            MatrixA_Columns = matrixA.GetLength(1);

            MatrixB_Rows = matrixB.GetLength(0);
            MatrixB_Columns = matrixB.GetLength(1);

            Rows = MatrixA_Rows;
            Columns = MatrixB_Columns;

            CurrentRow = 0;

            obj = new object();

            MatrixResult = new int[Rows, Columns];

            if (MatrixA_Columns != MatrixB_Rows)
            {
                IsComparable = false;
            }

            TasksList = TasksCreating();
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

        // номер строки результирующей матрицы, начиная с которой поток выполняет вычисления
        private int CurrentRow { get; set; }

        public List<Task> TasksList { get; set; }

        private Object obj;

        private long RunningTime { get; set; }

        private bool IsComparable { get; set; } = true;

        private bool IsCalculationComplete { get; set; } = false;

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

            int taskIndex = 0;

            while (CurrentRow < Rows)
            {
                Thread.Sleep(10);

                try
                {
                    if (TasksList[taskIndex].Status.ToString() == "Created")
                    {
                        TasksList[taskIndex].Start();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Task - {TasksList[taskIndex].Id} threw an exception: {e.Message}. Task status: {TasksList[taskIndex].Status}");
                }

                taskIndex++;

                if (taskIndex == TasksList.Count())
                {
                    taskIndex = 0;
                }
            }

            Console.WriteLine("Checking the completion of all threads...");

            while (!IsCalculationComplete)
            {
                RunningTasksCheck();
            }

            stopwatch.Stop();
            RunningTime = stopwatch.ElapsedMilliseconds / 1000;
            Console.WriteLine("Calculation completed");
        }

        /// <summary>
        /// Checks if all threads are completed
        /// </summary>
        private void RunningTasksCheck()
        {

            foreach (var item in TasksList)
            {
                if (item.Status.ToString() == "Running")
                {
                    IsCalculationComplete = false;
                    //Console.WriteLine($"Поток {item.Id} все еще в стадии выполнения. {item.Status}");
                    return;
                }
            }
            IsCalculationComplete = true;
        }

        /// <summary>
        /// Creates tasks list
        /// </summary>
        /// <returns></returns>
        private List<Task> TasksCreating()
        {
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < 100; i++)
            {
                tasks.Add(new Task(TaskMethod));
            }
            return tasks;
        }

        /// <summary>
        /// Partially fills in the resulting matrix (method performed by the thread)
        /// </summary>
        private void TaskMethod()
        {
            int startIndex = CurrentRow;
            int finishIndex = CurrentRow + 30 < Rows ? CurrentRow + 30 : Rows;

            SetCurrentRow(finishIndex);

            for (int i = startIndex; i < finishIndex; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    for (int k = 0; k < MatrixA_Columns; k++)
                    {
                        MatrixResult[i, j] += MatrixA[i, k] * MatrixB[k, j];
                    }
                }
            }
        }

        /// <summary>
        /// Sets the value of the current row
        /// </summary>
        /// <param name="finishIndex"></param>
        private void SetCurrentRow(int finishIndex)
        {
            lock (obj)
            {
                CurrentRow = finishIndex;
            }
        }

        /// <summary>
        /// Outputs the result to the console
        /// </summary>
        public void PrintResult()
        {
            // если матрицы не сопоставимы, то ...
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

        /// <summary>
        /// Returns the time of calculation
        /// </summary>
        /// <returns></returns>
        public long GetRunninTime()
        {
            return RunningTime;
        }
    }
}
