using aoc_2024.Utils;

namespace aoc_2024.Solvers;

public class SolverDay16 : ISolver {
    [PuzzleInput("16-01")]
    public void Solve(string[] input) {
        var (r, c, __) = input.ScanFor('S').First();
        var minScore = int.MaxValue;
        var minPath = new HashSet<(int r, int c)>();
        Walk((r, c), (0, 1));
        
        Console.WriteLine($"Minscore {minScore}, path length: {minPath.Count}");
        
        
        void Walk((int r, int c) pos, (int dr, int dc) dir) {
            var stack = new Stack<((int r, int c)pos, (int dr, int dc) dir, int score, List<(int r, int c)> path)>();
            stack.Push((pos, dir, 0, new()));
            var visited = new Dictionary<((int r, int c)p, (int dr, int dc) d), int>();
            while (stack.Any()) {
                var ((r, c), (dr, dc), score, path) = stack.Pop();
                if (visited.ContainsKey(((r, c), (dr, dc)))) {
                    if (visited[((r, c), (dr, dc))] < score) continue;
                    visited[((r, c), (dr, dc))] = score;
                } else {
                    visited.Add(((r, c), (dr, dc)), score);
                }

                path.Add((r, c));

                if (score > 160624) continue;
                if (input[r][c] == 'E') {
                    minScore = Math.Min(minScore, score);
                    if (minScore == 160624) {
                        foreach (var p in path) {
                            minPath.Add((p.r, p.c));
                        }
                    }
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
                        var newPath = new List<(int r, int c)>(path);
                        if (i != 0) {
                            newPath.RemoveAt(newPath.Count - 1);
                            newPath.Add((r, c));
                        }
                        stack.Push((next, (dirs[i].dr,  dirs[i].dc), i == 0 ? score + 1 : score + 1001, newPath));
                    }
                }
            }
        }
    }
}