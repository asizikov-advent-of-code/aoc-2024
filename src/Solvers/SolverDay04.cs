using System.Text;

namespace aoc_2024.Solvers;

public class SolverDay04 : ISolver {
    [PuzzleInput("04-01")]
    public void Solve(string[] input) {
        var xmas = "MAS";
        (int dr, int dc)[] dirs = [(1, 1), (-1, 1), (-1, -1), (1, -1)];
        var answer = 0;
        
        for (var r = 0; r < input.Length; r++) {
            for (var c = 0; c < input[r].Length; c++) {
                if (input[r][c] != 'A') continue;
                Check(r, c);
            }
        }

        Console.WriteLine($"Answer: {answer}");


        void Check(int r, int c) {
            var found = 0;
            foreach (var dir in dirs) {
                (int r, int c) pos1 = (r + dir.dr, c + dir.dc);
                (int r, int c) pos2 = (r - dir.dr, c - dir.dc);

                if (pos1.r < 0 || pos1.r >= input.Length || pos1.c < 0 || pos1.c >= input[pos1.r].Length) continue;
                if (pos2.r < 0 || pos2.r >= input.Length || pos2.c < 0 || pos2.c >= input[pos2.r].Length) continue;

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