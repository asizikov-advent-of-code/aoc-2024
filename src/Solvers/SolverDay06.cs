using System.Security;

namespace aoc_2024.Solvers;

public class SolverDay06 : ISolver {
    [PuzzleInput("06-01")]
    public void Solve(string[] input) {
        (int dr, int dc)[] dirs = [(1,0), (-1, 0), (0, 1), (0, -1)];
        var path = new HashSet<(int r, int c)>();
        for (var r = 0; r < input.Length; r++) {
            for (var c = 0; c < input[r].Length; c++) {
                if (input[r][c] != '^') continue;
                Walk(r, c, (-1, 0));
            }
        }
        var answer = path.Count;
        Console.WriteLine($"Answer: {answer}");


        void Walk(int r, int c, (int dr, int dc) dir) {
            path.Add((r, c));
            (int r, int c) nextPos = (r + dir.dr, c + dir.dc);
            var nextDir = dir;
            
            if (nextPos.r < 0 || nextPos.r >= input.Length || nextPos.c < 0 || nextPos.c >= input[nextPos.r].Length) {
                return;
            }
            if (input[nextPos.r][nextPos.c] == '#') {
                nextDir = (dir.dc, -dir.dr);
                nextPos = (r + dir.dc, c - dir.dr);
            }


            Walk(nextPos.r, nextPos.c, nextDir);
        }
    }
}

