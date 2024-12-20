using aoc_2024.Utils;

namespace aoc_2024.Solvers;

public class SolverDay20 : ISolver {
    [PuzzleInput("20-01")]
    public void Solve(string[] input) {

        var start = input.ScanFor('S').First();
        var path= Run((start.r, start.c));
        Console.WriteLine(path.Count);

        var answer = 0;
        var saves = new Dictionary<int, HashSet<((int r, int ), (int, int))>>();
        for (var s = 0; s < path.Count-1; s++) {
            for (var e = s + 1; e < path.Count; e++) {
                var dist = Math.Abs(path[s].r - path[e].r) + Math.Abs(path[s].c - path[e].c);
                if (dist <= 20) {
                    var savedDist = e - (s + dist) ;
                    if (savedDist == 0) continue;
                    if (savedDist >= 100) {
                        saves.TryAdd(savedDist, new HashSet<((int, int ), (int, int))>());
                        saves[savedDist].Add((path[s], path[e]));
                        answer++;
                    }    
                }
                
            }
        }
        
        Console.WriteLine(answer);

        List<(int r, int c)> Run((int r, int c) start) {
            var pathSoFar = new List<(int r, int c)>();
            var visited = new HashSet<(int r, int c)>();
            DFS(start);
            return pathSoFar;
            void DFS((int r, int c) current) {
                if (!visited.Add(current)) return;
                pathSoFar.Add(current);
                if (input[current.r][current.c] == 'E') return;
                foreach ((int dr, int dc) dir in new[] { (0, 1), (0, -1), (1, 0), (-1, 0) }) {
                    (int r, int c) next = (current.r + dir.dr, current.c + dir.dc);
                    if (!input.IsInBounds(next)|| input[next.r][next.c] == '#') continue;
                    DFS(next);
                }
            }
        }        
    }
}