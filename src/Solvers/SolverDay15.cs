using System.Text;
using aoc_2024.Utils;

namespace aoc_2024.Solvers;

public class SolverDay15 : ISolver {
    [PuzzleInput("15-01")]
    public void Solve(string[] input) {
        var visualize = false;
        var grid = new List<char[]>();
        var movements = new StringBuilder();
        var gridComplete = false;
        foreach (var line in input) {
            if (line == string.Empty) {
                gridComplete = true;
                continue;
            }

            if (!gridComplete) {
                var lineBuilder = new StringBuilder();
                foreach (var tile in line) {
                    lineBuilder.Append(tile switch {
                        '#' => "##",
                        '@' => "@.",
                        'O' => "[]",
                        _ => ".."
                    });
                }
                grid.Add(lineBuilder.ToString().ToCharArray());
            }
            else movements.Append(line);
        }

        Print();
        var instructions = movements.ToString();
        var robotStart = grid.ScanFor('@').First();
        Start(robotStart.r, robotStart.c);

        var answer = grid.ScanFor('[').Aggregate(0L, (acc, pos) => acc + (pos.r * 100 + pos.c));
        
        Console.WriteLine("answer: " + answer);


        void Start(int r, int c) {
            (int r, int c) robotPos = (r, c);
            foreach (var (i,instruction) in instructions.Index()) {
                if (visualize) {
                    Console.Clear();
                    Console.WriteLine($"Instruction: {instruction}, {i} out of {instructions.Length}");    
                }
                
                var rollback = CloneGrid();
                (int dr, int dc) dir = instruction switch {
                    '^' => (-1, 0),
                    'v' => (1, 0),
                    '<' => (0, -1),
                    '>' => (0, 1),
                    _ => (0, 0)
                };
                
                if (TryMove(robotPos, dir))
                    robotPos = (robotPos.r + dir.dr, robotPos.c + dir.dc);
                else grid = rollback;

                if (visualize) {
                    Print();
                    Thread.Sleep(100);    
                }
                
            }
        }

        void Print() {
            foreach (var row in grid) {
                Console.WriteLine(new string(row));
            }

            Console.WriteLine();
        }

        List<char[]> CloneGrid() {
            var clone = new List<char[]>();
            foreach (var row in grid) {
                clone.Add(row.Clone() as char[]);
            }
            return clone;
        }
        
        bool TryMove((int r, int c) p, (int dr, int dc) dir)
        {
            (int r, int c) next = (p.r + dir.dr, p.c + dir.dc);
            switch (grid[next.r][next.c])
            {
                case '#':
                    return false;
                case '[':
                    if (TryMove((next.r, next.c + 1), dir) && TryMove( next, dir)) return TryMove(p, dir);
                    return false;
                case ']':
                    if (TryMove((next.r,  next.c - 1), dir) && TryMove( next, dir)) return TryMove( p, dir);
                    return false;
                case '.':                  
                    grid[next.r][ next.c] = grid[p.r][p.c];
                    grid[p.r][ p.c] = '.';                   
                    return true;
            }

            return false;
        }   
    }
}