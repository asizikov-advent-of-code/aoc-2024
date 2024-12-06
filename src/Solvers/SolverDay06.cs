using System.Security;

namespace aoc_2024.Solvers;

public class SolverDay06 : ISolver {
    [PuzzleInput("06-01")]
    public void Solve(string[] input) {
        var grid = input.Select(x => x.ToCharArray()).ToArray();
        var path = new HashSet<(int r, int c)>();
        var positions = new HashSet<((int r, int c)pos, (int dr, int dc) dir)>();
        var obstacles = new HashSet<(int r, int c)>();
        var start = (0, 0);
        for (var r = 0; r < input.Length; r++) {
            for (var c = 0; c < input[r].Length; c++) {
                if (input[r][c] != '^') continue;
                start = (r, c);
                Walk(r, c, (-1, 0));
            }
        }

        obstacles.Remove(start);
        foreach (var row in grid) {
            Console.WriteLine(row);
        }
        
        Console.WriteLine($"Answer: {path.Count}, obstacles: {obstacles.Count}");


        void Walk(int r, int c, (int dr, int dc) dir) {
            positions.Add(((r, c), dir));
            path.Add((r, c));
            if (grid[r][c] == '.') grid[r][c] = 'X';
            
            (int r, int c) nextPos = (r + dir.dr, c + dir.dc);
            var nextDir = dir;
            var nextDirRotated = (dir.dc, -dir.dr);
            (int r, int c) nextPosRotated = (r + dir.dc, c - dir.dr);
            
            if (nextPos.r < 0 || nextPos.r >= input.Length || nextPos.c < 0 || nextPos.c >= input[nextPos.r].Length) {
                return;
            }
            
            if (Trace(nextPosRotated.r, nextPosRotated.c, nextDirRotated)) {
                if (grid[nextPos.r][nextPos.c] == 'X') {
                    grid[nextPos.r][nextPos.c] = '*';
                } else {
                    grid[nextPos.r][nextPos.c] = 'O';
                }
                
                if (start != nextPos) obstacles.Add(nextPos);
            }
            
            if (input[nextPos.r][nextPos.c] == '#') Walk(nextPosRotated.r, nextPosRotated.c, nextDirRotated);
            else Walk(nextPos.r, nextPos.c, nextDir);

            bool Trace(int r, int c, (int dr, int dc) dir) {
                while (r >= 0 && r < input.Length && c >= 0 && c < input[r].Length) {
                    if (input[r][c] == '#') return false;
                    if (positions.Contains(((r, c), dir))) return true;
                    r += dir.dr;
                    c += dir.dc;
                }

                return false;
            }
        }
    }
}

