using System.Security.Cryptography.X509Certificates;
using aoc_2024.Utils;

namespace aoc_2024.Solvers;

public class SolverDay12: ISolver {
        
        [PuzzleInput("12-01")]
        public void Solve(string[] input) {
            var grid = input.Select(l => l.ToCharArray()).ToArray();
            var seen = new HashSet<(int r, int c)>();
            var answer = 0L;
            foreach (var (r, c, v) in input.Scan()) {
                if(seen.Contains((r, c))) continue;
                var (a, p) = Visit(r, c, v);
                answer += a * p;
                Console.WriteLine($"Type: {v}, Area: {a}, perimeter: {p}");
                
            }
            
            
            Console.WriteLine($"Answer: {answer}");
            
            
            (int area, int perimeter) Visit(int r, int c, char type) {
                var queue = new Queue<(int r, int c)>();
                queue.Enqueue((r, c));
                
                var (area, perimeter) = (0, 0);
                while (queue.Count > 0) {
                    var current = queue.Dequeue();
                    if (!input.IsInBounds(current.r, current.c)) continue;
                    if (grid[current.r][current.c] != type) continue;
                    if (!seen.Add(current)) continue;
                    area++;
                    foreach (var (dr, dc) in new[] {(-1, 0), (1, 0), (0, -1), (0, 1)}) {
                        (int r, int c) next = (current.r + dr, current.c + dc);
                        if (!input.IsInBounds(next.r, next.c) || grid[next.r][next.c] != type) {
                            perimeter++;
                        } else {
                            queue.Enqueue(next);
                        }
                    }
                }
                
                return (area, perimeter);
            }
        }
}