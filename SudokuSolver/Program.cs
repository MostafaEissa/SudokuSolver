using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var puzzle = new SudokuPuzzle();

            puzzle[1, 2] = 8;
            puzzle[1, 3] = 1;
            puzzle[1, 4] = 7;
            puzzle[1, 5] = 9;
            puzzle[1, 7] = 3;
            puzzle[1, 9] = 4;

            puzzle[2, 5] = 4;
            puzzle[2, 8] = 1;
            puzzle[2, 9] = 6;


            puzzle[3, 3] = 6;
            puzzle[3, 4] = 1;
            puzzle[3, 6] = 3;
            puzzle[3, 8] = 5;


            puzzle[4, 6] = 8;
            puzzle[4, 7] = 6;
            puzzle[4, 8] = 4;


            puzzle[5, 3] = 8;
            puzzle[5, 4] = 9;
            puzzle[5, 6] = 4;
            puzzle[5, 7] = 1;


            puzzle[6, 2] = 4;
            puzzle[6, 3] = 9;
            puzzle[6, 4] = 2;


            puzzle[7, 2] = 9;
            puzzle[7, 4] = 6;
            puzzle[7, 6] = 5;
            puzzle[7, 7] = 2;

            puzzle[8, 1] = 8;
            puzzle[8, 2] = 7;
            puzzle[8, 5] = 2;


            puzzle[9, 1] = 2;
            puzzle[9, 3] = 5;
            puzzle[9, 5] = 1;
            puzzle[9, 6] = 7;
            puzzle[9, 7] = 4;
            puzzle[9, 8] = 9;

            //puzzle[1, 2] = 4;
            //puzzle[1, 5] = 1;
            //puzzle[1, 6] = 7;
            //puzzle[1, 8] = 6;


            //puzzle[2, 7] = 5;


            //puzzle[3, 1] = 7;
            //puzzle[3, 3] = 1;
            //puzzle[3, 6] = 8;
            //puzzle[3, 7] = 3;


            //puzzle[4, 1] = 2;
            //puzzle[4, 2] = 1;
            //puzzle[4, 3] = 9;            
            

            //puzzle[5, 2] = 6;
            //puzzle[5, 4] = 7;
            //puzzle[5, 6] = 2;
            //puzzle[5, 8] = 8;


            //puzzle[6, 7] = 6;
            //puzzle[6, 8] = 4;
            //puzzle[6, 9] = 2;


            //puzzle[7, 3] = 4;
            //puzzle[7, 4] = 3;
            //puzzle[7, 7] = 9;
            //puzzle[7, 9] = 7;


            //puzzle[8, 3] = 2;


            //puzzle[9, 2] = 5;
            //puzzle[9, 4] = 2;
            //puzzle[9, 5] = 8;
            //puzzle[9, 8] = 3;

 
            Console.WriteLine(puzzle);

            var stopWatch = Stopwatch.StartNew();
            var result = puzzle.Solve();
            stopWatch.Stop();

            if (result)
                Console.WriteLine(puzzle);
            else
                Console.WriteLine("Puzzle couldn't be solved");

            Console.WriteLine("Elapsed Time: {0}", stopWatch.ElapsedMilliseconds/1000.0);
        }

       
    }
}
