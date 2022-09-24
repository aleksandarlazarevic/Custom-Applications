using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    public class Utilities
    {
        public static void PrintCurrentSudoku(int[,] sudoku)
        {
            Console.WriteLine();
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 10; j++)
                    if (j % 3 == 0)
                    {
                        Console.Write(" {0} ", sudoku[i - 1, j - 1]);
                    }
                    else
                    {
                        Console.Write(" {0}", sudoku[i - 1, j - 1]);
                    }

                Console.WriteLine(" ");
                if (i % 3 == 0) Console.WriteLine();
            }
        }
    }
}
