using aoc_2024.Utils;

namespace aoc_2024.Solvers;

public class SolverDay16 : ISolver {
    [PuzzleInput("16-01")]
    public void Solve(string[] input) {
        var (r, c, __) = input.ScanFor('S').First();
        var grid = input.Select(l => l.ToCharArray()).ToArray();
        var minScore = int.MaxValue;
        var minPath = new List<(int r, int c, char d)>();
        Walk2((r, c), (0, 1));

        Console.WriteLine($"Minscore {minScore}");

        void Print(List<(int r, int c, char d)> path) {
            for (var r = 0; r < grid.Length; r++) {
                for (var c = 0; c < grid[r].Length; c++) {
                    if (path.Any(p => p.r == r && p.c == c)) {
                        Console.Write(path.First(p => p.r == r && p.c == c).d);
                    } 
                    else {
                        Console.Write(grid[r][c]);
                    }
                }
                Console.WriteLine();
            }
        }
        
        void Walk2((int r, int c) pos, (int dr, int dc) dir) {
            var stack = new Stack<((int r, int c)pos, (int dr, int dc) dir, int score, List<(int r, int c, char d)> path)>();
            stack.Push((pos, dir, 0, new()));
            var visited = new Dictionary<((int r, int c)p, (int dr, int dc) d), int>();
            while (stack.Any()) {
                var ((r, c), (dr, dc), score, path) = stack.Pop();
                if (visited.ContainsKey(((r, c), (dr, dc)))) {
                    if (visited[((r, c), (dr, dc))] <= score) continue;
                    visited[((r, c), (dr, dc))] = score;
                } else {
                    visited.Add(((r, c), (dr, dc)), score);
                }

                var ch = (dr, dc) switch {
                    (0, 1) => '>',
                    (0, -1) => '<',
                    (1, 0) => 'v',
                    (-1, 0) => '^',
                };
                path.Add(((r, c, ch)));

                if (score > minScore) continue;
                if (input[r][c] == 'E') {
                    if (minScore > score) {
                        Console.WriteLine($"New min score: {score}");
                        minPath = path;
                        Console.Clear();
                        Print(path);
                        Console.WriteLine($"Score: {score} ; MinScore: {minScore}");
                        Thread.Sleep(100);
                    }
                    minScore = Math.Min(minScore, score);
                    continue;
                }

                (int dr, int dc)[] dirs = [
                    (dr, dc),
                    (dc, -dr),
                    (-dc, dr)
                ];
                for (var i = 0; i < dirs.Length; i++) {
                    (int r, int c) next = (r + dirs[i].dr, c + dirs[i].dc);
                    if (input.IsInBounds(next) && input[next.r][next.c] != '#' && input[next.r][next.c] != 'S') {
                        var newPath = new List<(int r, int c, char d)>(path);
                        if (i != 0) {
                            newPath.RemoveAt(newPath.Count - 1);
                            newPath.Add((r, c, (dirs[i].dr, dirs[i].dc) switch {
                                (0, 1) => '>',
                                (0, -1) => '<',
                                (1, 0) => 'v',
                                (-1, 0) => '^',
                            }));
                        }
                        stack.Push((next, (dirs[i].dr,  dirs[i].dc), i == 0 ? score + 1 : score + 1001, newPath));
                    }
                }
                grid[r][c] = '.';
            }
        }
    }
}