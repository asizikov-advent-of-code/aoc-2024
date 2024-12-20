using aoc_2024.Utils;

namespace aoc_2024.Solvers;

public class SolverDay20 : ISolver {
    [PuzzleInput("20-01")]
    public void Solve(string[] input) {
        var path = new List<(int r, int c)>();
        foreach (var start in input.ScanFor('S')) {
            path= Run((start.r, start.c));
        }
        Console.WriteLine(path.Count - 1);
        
        var allPaths = new List<int>();
        allPaths = RunWithCheats(path.First());
        
        allPaths.GroupBy(p => path.Count - 1 - p)
            .OrderBy(g => g.Key)
            .ToList().ForEach(g => Console.WriteLine($"{g.Count()} cheats saved {g.Key}  "));
        
        Console.WriteLine(allPaths.Select(p => path.Count-1 - p >= 100).Count());

        List<int> RunWithCheats((int r, int c) start) {
            var saves = new Dictionary<((int r, int c)s, (int r, int c)e), int>();
            var pathLengths = new List<int>();
            var stack = new Stack<((int r, int c) p, HashSet<(int r, int c)> visited, int steps, List<(int r, int c)> cheats)>();
            stack.Push((start, new(), 0, new()));

            while (stack.Count > 0) {
                var current = stack.Pop();
                if (!current.visited.Add(current.p)) continue;
                if (current.cheats.Count == 2 && input[current.p.r][current.p.c] == '#') continue;
                if (current.steps >= path.Count - 1) continue;
                if (input[current.p.r][current.p.c] == 'E') {
                    if (current.steps >= path.Count - 1) continue;
                    saves.TryAdd((current.cheats[0], current.cheats[1]), int.MaxValue);
                    saves[(current.cheats[0], current.cheats[1])] = Math.Min(saves[(current.cheats[0], current.cheats[1])], current.steps);
                    Console.WriteLine("Cheats: " + string.Join(", ", current.cheats) + " saved: " + (path.Count - current.steps - 1));
                    pathLengths.Add(current.steps);
                    continue;
                }
                foreach ((int dr, int dc) dir in new[] { (0, 1), (0, -1), (1, 0), (-1, 0) }) {
                    (int r, int c) next = (current.p.r + dir.dr, current.p.c + dir.dc);
                    if (!input.IsInBounds(next) || input[next.r][next.c] == '#' && current.cheats.Count == 2) {
                        continue;
                    }

                    if (current.cheats.Count == 1) {
                        var newCheats = new List<(int r, int c)>(current.cheats) { current.p };
                        stack.Push((next, [..current.visited], current.steps + 1, newCheats));
                        continue;
                    }
                    
                    if (input.IsInBounds(next) && input[next.r][next.c] != '#') {
                        stack.Push((next, [..current.visited], current.steps + 1, current.cheats));
                    } else if (input.IsInBounds(next) && input[next.r][next.c] == '#') {
                        var newCheats = new List<(int r, int c)>(current.cheats) { current.p };
                        stack.Push((next, [..current.visited], current.steps + 1, newCheats));
                    }
                }
            }
            
            return saves.Keys.Select(k => saves[k]).ToList();
        }

        List<(int r, int c)> Run((int r, int c) start) {
            var path = new List<(int r, int c)>();
            var visited = new HashSet<(int r, int c)>();
            var queue = new Queue<(int r, int c)>();
            queue.Enqueue(start);
            
            while (queue.Count != 0) {
                var current = queue.Dequeue();
                if (!visited.Add(current)) continue;
                path.Add(current);
                foreach ((int dr, int dc) dir in new[] { (0, 1), (0, -1), (1, 0), (-1, 0) }) {
                    (int r, int c) next = (current.r + dir.dr, current.c + dir.dc);
                    if (input.IsInBounds(next) && input[next.r][next.c] != '#') {
                        queue.Enqueue(next);
                    }
                }
            }
            return path;
        }
    }
}