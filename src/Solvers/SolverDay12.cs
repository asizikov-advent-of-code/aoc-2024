using aoc_2024.Utils;

namespace aoc_2024.Solvers;

public class SolverDay12: ISolver {

        [PuzzleInput("12-01")]
        public void Solve(string[] input) {
            var seen = new HashSet<(int r, int c)>();
            var answer = 0L;
            foreach (var (r, c, v) in input.Scan()) {
                if(seen.Contains((r, c))) continue;
                var (area, _, sides) = Area(r, c, v);
                answer += area * sides;
            }


            Console.WriteLine($"Answer: {answer}");

            int Sides(HashSet<(int r, int c)> region) {
                var sides = 0;
                foreach (var (dr, dc) in new[] {(0, 1), (0, -1), (1, 0), (-1, 0)}) {
                    var fences = new HashSet<(int r, int c)>();
                    foreach (var (cellR, cellC) in region) {
                        var nr = cellR + dr;
                        var nc = cellC + dc;
                        if (!region.Contains((nr, nc))) {
                            fences.Add((nr, nc));
                        }
                    }

                    foreach (var (fenceR, fenceC) in fences) {
                        if (!fences.Contains((fenceR - dc, fenceC + dr))) {
                            sides++;
                        }
                    }
                }

                return sides;
            }

            (int area, int perimeter, int sides) Area(int r, int c, char type) {
                var queue = new Queue<(int r, int c)>();
                var region = new HashSet<(int r, int c)>();
                queue.Enqueue((r, c));

                var (area, perimeter) = (0, 0);
                while (queue.Count > 0) {
                    var current = queue.Dequeue();
                    if (!input.IsInBounds(current.r, current.c)) continue;
                    if (input[current.r][current.c] != type) continue;
                    region.Add(current);
                    if (!seen.Add(current)) continue;
                    area++;
                    foreach (var (dr, dc) in new[] {(-1, 0), (1, 0), (0, -1), (0, 1)}) {
                        (int r, int c) next = (current.r + dr, current.c + dc);
                        if (!input.IsInBounds(next.r, next.c) || input[next.r][next.c] != type) {
                            perimeter++;
                        } else {
                            queue.Enqueue(next);
                        }
                    }
                }
                var sides = Sides(region);
                return (area, perimeter, sides);
            }
        }
}