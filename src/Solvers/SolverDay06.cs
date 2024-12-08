namespace aoc_2024.Solvers;

public class SolverDay06 : ISolver {
    [PuzzleInput("06-01")]
    public void Solve(string[] input) {
        var grid = input.Select(x => x.ToCharArray()).ToArray();
        var path = new HashSet<(int r, int c)>();

        (int r, int c) start = (0, 0);
        for (var r = 0; r < input.Length; r++) {
            for (var c = 0; c < input[r].Length; c++) {
                if (input[r][c] != '^') continue;
                start = (r, c);
                Walk(r, c, (-1, 0));
            }
        }

        var obstacles = new HashSet<(int r, int c)>();
        var positions = new HashSet<(int r, int c, int dr, int dc)>();
        (int r, int c) obstacle = (-1, -1);
        foreach (var pos in path) {
            obstacle = pos;
            positions.Clear();
            if (IsLoop(start.r, start.c, (-1, 0))) obstacles.Add(obstacle);
        }

        
        Console.WriteLine($"Answer: {path.Count}, obstacles: {obstacles.Count}");

        bool IsLoop(int r, int c, (int dr, int dc) dir) {
            if (!positions.Add((r, c, dir.dr, dir.dc))) return true;
            (int r, int c) nextPos = (r + dir.dr, c + dir.dc);
            if (nextPos.r < 0 || nextPos.r >= input.Length || nextPos.c < 0 || nextPos.c >= input[nextPos.r].Length) {
                return false;
            }

            if (input[nextPos.r][nextPos.c] == '#' || obstacle == nextPos) {
                var nextDir = (dir.dc, -dir.dr);
                return IsLoop(r, c, nextDir);
            }
            return IsLoop(nextPos.r, nextPos.c, dir);
        }

        void Walk(int r, int c, (int dr, int dc) dir) {
            path.Add((r, c));
            (int r, int c) nextPos = (r + dir.dr, c + dir.dc);
            var nextDirRotated = (dir.dc, -dir.dr);
            (int r, int c) nextPosRotated = (r + dir.dc, c - dir.dr);

            if (nextPos.r < 0 || nextPos.r >= input.Length || nextPos.c < 0 || nextPos.c >= input[nextPos.r].Length) {
                return;
            }

            if (input[nextPos.r][nextPos.c] == '#') {
                Walk(nextPosRotated.r, nextPosRotated.c, nextDirRotated);
            }
            else Walk(nextPos.r, nextPos.c, dir);
        }
    }
}