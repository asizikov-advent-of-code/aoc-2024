using System.Text;
using aoc_2024.Utils;

namespace aoc_2024.Solvers;

public class SolverDay15 :ISolver {
    [PuzzleInput("15-01")]
    public void Solve(string[] input) {
        var gridWidth = 0;
        var grid = new List<char[]>();
        var movements = new StringBuilder();
        var gridComplete = false;
        for (var i = 0; i < input.Length; i++) {
            var line = input[i];
            if (line == string.Empty) {
                gridComplete = true;
                continue;
            }

            if (!gridComplete) {
                grid.Add(line.ToCharArray());
            }
            else {
                movements.Append(line);
            }
        }
        
        var instructions = movements.ToString();
        foreach(var (r, c, _) in grid.ScanFor('@')) {
            Start(r, c);
        }
        
        foreach (var row in grid) {
            Console.WriteLine(new string(row));
        }
        Console.WriteLine();

        var answer = 0;
        foreach (var box in grid.ScanFor('O')) {
            answer += 100* box.r + box.c;
        }
        
        Console.WriteLine("answer: " + answer);
        
        
        void Start(int r, int c) {
            (int r, int c) robotPos = (r, c);
            foreach (var instruction in instructions) {
                grid[robotPos.r][robotPos.c] = '.';
                switch (instruction) {
                    case '^': robotPos = TryMove(robotPos, (-1, 0)); break;
                    case 'v': robotPos = TryMove(robotPos, (1, 0)); break;
                    case '<': robotPos = TryMove(robotPos, (0, -1)); break;
                    case '>': robotPos = TryMove(robotPos, (0, 1)); break;
                    default: continue;
                }
                grid[robotPos.r][robotPos.c] = '@';
            }
        }
        
        (int r, int c) TryMove((int r, int c) robot, (int dr, int dc) dir) {
            (int r, int c) newPos = (robot.r + dir.dr, robot.c + dir.dc);
            if (grid[newPos.r][newPos.c] is '.') return newPos;
            if (grid[newPos.r][newPos.c] is '#') return robot;
            if (grid[newPos.r][newPos.c] is 'O') {
                var (canMove, pos) = Trace(newPos, (dir.dr, dir.dc));
                if (!canMove) return robot;
                grid[pos.r][pos.c] = 'O';
                grid[newPos.r][newPos.c] = '.';
                return newPos;
            }
            return robot;
        }
        
        (bool canMove, (int r, int c) pos) Trace((int r, int c) start, (int dr, int dc) dir) {
            (int r, int c) current = start;
            while (true) {
                current = (current.r + dir.dr, current.c + dir.dc);
                if (grid[current.r][current.c] is '#') return (false, default);
                if (grid[current.r][current.c] is 'O') continue;
                if (grid[current.r][current.c] is '.') return (true, current);
            }
        }
    }


}