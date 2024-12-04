using System.Text;

namespace aoc_2024.Solvers;

public class SolverDay04 : ISolver {
    [PuzzleInput("04-00")]
    public void Solve(string[] input) {
        var xmas = "XMAS";
        (int dr, int dc)[] dirs = [(0, 1), (1, 0), (0, -1), (-1, 0), (1, 1), (-1, 1), (-1, -1), (1, -1) ];
        var answer = 0;
        for (var r = 0; r < input.Length; r++) {
            for (var c = 0; c < input[r].Length; c++) {
                if (input[r][c] != 'X') continue;
                foreach (var dir in dirs) {
                    Visit(r, c, 0, dir);
                }
            }
        }
        
        Console.WriteLine($"Answer: {answer}");


        void Visit(int r, int c, int i, (int dr, int dc) dir) {
            if (r < 0 || r >= input.Length || c < 0 || c >= input[r].Length || input[r][c] != xmas[i]) {
                return;
            }
            if (i == xmas.Length - 1) {
                answer++;
                return;
            } 
            Visit(r + dir.dr, c + dir.dc, i + 1, dir);
        }
    }
}