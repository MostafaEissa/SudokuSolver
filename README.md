# Sudoku Solver


A library that tries to solve Sudoku puzzles using graph coloring. For more information you can check my article [A Sudoku Solver using Graph Coloring](http://www.codeproject.com/Articles/801268/A-Sudoku-Solver-using-Graph-Coloring).

## How does it work?


A Sudoku puzzle (9x9) can be thought of a graph with 81 vertices, one for each cell, and two vertices are connected by an edge if they cannot be assigned the same value. For example, all cells in the same row, column  or block will have edges between their corresponding vertices. 

Graph coloring tries to assign colors to the vertices such that no two adjacent vertices (two vertices connected by an edge) will have the same color. The goal is to find the minimum number of colors to solve such problem.

A solution is found for the Sudoku puzzle if the graph coloring algorithm can find a coloring of the graph used only 9 colors, since the possible values are 1 through 9.

## Sample

	//define a new puzzle
	var puzzle = new SudokuPuzzle();

	//fill in the known cells
	puzzle[1, 2] = 8;puzzle[1, 3] = 1;puzzle[1, 4] = 7;puzzle[1, 5] = 9;puzzle[1, 7] = 3;puzzle[1, 9] = 4;

	puzzle[2, 5] = 4;puzzle[2, 8] = 1;puzzle[2, 9] = 6;

	puzzle[3, 3] = 6;puzzle[3, 4] = 1;puzzle[3, 6] = 3;puzzle[3, 8] = 5;

	puzzle[4, 6] = 8;puzzle[4, 7] = 6;puzzle[4, 8] = 4;

	puzzle[5, 3] = 8;puzzle[5, 4] = 9;puzzle[5, 6] = 4;puzzle[5, 7] = 1;

	puzzle[6, 2] = 4;puzzle[6, 3] = 9;puzzle[6, 4] = 2;

	puzzle[7, 2] = 9;puzzle[7, 4] = 6;puzzle[7, 6] = 5;puzzle[7, 7] = 2;

	puzzle[8, 1] = 8;puzzle[8, 2] = 7;puzzle[8, 5] = 2;

	puzzle[9, 1] = 2;puzzle[9, 3] = 5;puzzle[9, 5] = 1;puzzle[9, 6] = 7;puzzle[9, 7] = 4;puzzle[9, 8] = 9;

	//print original puzzle
	Console.WriteLine(puzzle);

	var result = puzzle.Solve();

	if (result)
		Console.WriteLine(puzzle);
	else
		Console.WriteLine("Puzzle couldn't be solved");

## Known Issues

Currently not all puzzles can be solved; for some puzzles the algorithm cannot find a coloring that uses only nine colors. My guess is I should find a better algorithm that finds a solution is a fewer number of colors.

## External Links


### Graph Coloring

see [Graph coloring](http://en.wikipedia.org/wiki/Graph_coloring) for an explaination for graph coloring on [Wikipedia](http://www.wikipedia.org).

see [New Graph Coloring Algorithms](http://www.phys.ubbcluj.ro/~zneda/edu/mc/graphcolouring.pdf) for an explaination of the algorithm used.

