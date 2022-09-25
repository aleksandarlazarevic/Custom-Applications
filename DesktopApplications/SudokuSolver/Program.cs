using System;

namespace SudokuSolver
{
    public class Program
    {
        static void Main(string[] args)
        {
            int[,] sudoku1 = {
                    { 0, 1, 3, 8, 0, 0, 4, 0, 5 },
                    { 0, 2, 4, 6, 0, 5, 0, 0, 0 },
                    { 0, 8, 7, 0, 0, 0, 9, 3, 0 },

                    { 4, 9, 0, 3, 0, 6, 0, 0, 0 },
                    { 0, 0, 1, 0, 0, 0, 5, 0, 0 },
                    { 0, 0, 0, 7, 0, 1, 0, 9, 3 },

                    { 0, 6, 9, 0, 0, 0, 7, 4, 0 },
                    { 0, 0, 0, 2, 0, 7, 6, 8, 0 },
                    { 1, 0, 2, 0, 0, 8, 3, 5, 0 }
            };
            int[,] sudoku2 = {
                    { 0, 0, 2, 0, 0, 0, 0, 4, 1 },
                    { 0, 0, 0, 0, 8, 2, 0, 7, 0 },
                    { 0, 0, 0, 0, 4, 0, 0, 0, 9 },

                    { 2, 0, 0, 0, 7, 9, 3, 0, 0 },
                    { 0, 1, 0, 0, 0, 0, 0, 8, 0 },
                    { 0, 0, 6, 8, 1, 0, 0, 0, 4 },

                    { 1, 0, 0, 0, 9, 0, 0, 0, 0 },
                    { 0, 6, 0, 4, 3, 0, 0, 0, 0 },
                    { 8, 5, 0, 0, 0, 0, 4, 0, 0 }
            };

            int[,] selectedSudoku = sudoku2;
            Console.WriteLine("Sudoku to be solved:");
            Utilities.PrintCurrentSudoku(selectedSudoku);

            var watch = System.Diagnostics.Stopwatch.StartNew();
            Solve(selectedSudoku);
            watch.Stop();
            Console.WriteLine($"Execution Time: {watch.Elapsed}");

            Console.WriteLine("Solved:");
            Utilities.PrintCurrentSudoku(selectedSudoku);
        }

        // sudoku1 resolution time: ~ 00.0006826
        // sudoku2 resolution time: ~ 00.0121960
        private static bool Solve(int[,] sudokuGrid, int row = 0, int column = 0)
        {
            // inspect the current cell
            if (row < Constants.MaxNumberOfRows && column < Constants.MaxNumberOfColumns)
            {
                // if the cell is not empty, inspect the next ones until you find one
                if (sudokuGrid[row, column] != 0)
                {
                    // iterate through columns of the same row
                    if ((column + 1) < Constants.MaxNumberOfColumns)
                    {
                        return Solve(sudokuGrid, row, column + 1);
                    }

                    // increment a row in order to iterate through it
                    else if ((row + 1) < Constants.MaxNumberOfRows)
                    {
                        return Solve(sudokuGrid, row + 1, 0);
                    }

                    // if none empty cells are found, the sudoku is solved
                    else
                    {
                        return true;
                    }
                }

                // when an empty cell is found, assign the first available value to it
                else
                {
                    // iterate through nubers 1..9 to check if those are valid candidates for the cell
                    for (int i = 0; i < 9; ++i)
                    {
                        // check if current number from the aforementioned range could be a solution for the cell's value
                        if (IsNumberAPossibleSolution(sudokuGrid, row, column, i + 1))
                        {
                            // choose the first available candidate number as a solution to the cell's value
                            sudokuGrid[row, column] = i + 1;

                            // if the last column is not reached, try solving the sudoku with the previously assumed value.
                            // if that's not possible, reset the cell's value to zero and try another number from the range 1..9
                            // as a possible solution
                            if ((column + 1) < Constants.MaxNumberOfColumns)
                            {
                                if (Solve(sudokuGrid, row, column + 1))
                                {
                                    return true;
                                }
                                else
                                {
                                    sudokuGrid[row, column] = 0;
                                }
                            }

                            // if the last column is reached, start from the next row, if it's not the last one
                            else if ((row + 1) < Constants.MaxNumberOfRows)
                            {
                                if (Solve(sudokuGrid, row + 1, 0))
                                {
                                    return true;
                                }
                                else
                                {
                                    sudokuGrid[row, column] = 0;
                                }
                            }

                            // if all the cells are filled, accept the chosen number as a solution
                            else
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }

            // exit the solver if a cell out of sudoku bounds is passed in
            else
            {
                return true;
            }
        }

        private static bool IsNumberAPossibleSolution(int[,] sudoku, int row, int column, int number)
        {
            int rowStart = (row / 3) * 3;
            int colStart = (column / 3) * 3;

            // disregard as a candidate if a number does not satisfy sudoku rules
            for (int i = 0; i < 9; ++i)
            {
                // check if the value is already present in the same row
                if (sudoku[row, i] == number)
                {
                    return false;
                }

                // check if the value is already present in the same column
                if (sudoku[i, column] == number)
                {
                    return false;
                }

                // check if the value is already present in the same sub-grid
                if (sudoku[rowStart + (i % 3), colStart + (i / 3)] == number)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
