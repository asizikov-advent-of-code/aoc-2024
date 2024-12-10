using aoc_2024.Utils;

namespace aoc_2024.Solvers;

public class SolverDay04 : ISolver {
    [PuzzleInput("04-01")]
    public void Solve(string[] input) {
        var xmas = "MAS";
        (int dr, int dc)[] dirs = [(1, 1), (-1, 1), (-1, -1), (1, -1)];
        var answer = 0;

        foreach (var (r, c, val) in input.ScanFor('A')) {
            Check(r, c);
        }

        Console.WriteLine($"Answer: {answer}");
        
        void Check(int r, int c) {
            var found = 0;
            foreach (var dir in dirs) {
                (int r, int c) pos1 = (r + dir.dr, c + dir.dc);
                (int r, int c) pos2 = (r - dir.dr, c - dir.dc);
                if(!input.IsInBounds(pos1) || !input.IsInBounds(pos2)) continue;
                if (input[pos1.r][pos1.c] == xmas[0] && input[pos2.r][pos2.c] == xmas[2]) {
                    found++;
                }
            }

            if (found >= 2) {
                answer++;
            }
        }
    }
}