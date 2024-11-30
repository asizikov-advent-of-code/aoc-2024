namespace aoc_2024.Solvers;

public class NullSolver(string number) : ISolver {
    [PuzzleInput("00-00")]
    public void Solve(string[] input) {
        Console.WriteLine($"Failed to find Solver for day {number}");
    }
}