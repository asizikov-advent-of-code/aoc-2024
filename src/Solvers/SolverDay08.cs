using aoc_2024.Utils;

namespace aoc_2024.Solvers;

public class SolverDay08 : ISolver {
    [PuzzleInput("08-01")]
    public void Solve(string[] input) {
        
        var antennas = new Dictionary<char, List<(int r, int c)>>();
        foreach (var (r, c, val) in input.ScanExcept('.')) {
            antennas.TryAdd(val, []);
            antennas[val].Add((r, c));
        }

        var antinodes = new HashSet<(int r, int c)>();
        foreach (var frequency in antennas.Keys) {
            var positions = antennas[frequency];
            
            for (var i = 0; i < positions.Count - 1; i++) {
                for (var j = i + 1; j < positions.Count; j++) {
                    
                    var (first, second) = (positions[i], positions[j]);
                    antinodes.Add(first);
                    antinodes.Add(second);
                    
                    (int dr, int dc)[] dirs = [(second.r - first.r, second.c - first.c), (first.r - second.r, first.c - second.c)];
                    foreach (var dir in dirs) {
                        for (var (r, c) = (first.r + dir.dr, first.c + dir.dc); r >= 0 && r < input.Length && c >= 0 && c < input[r].Length; r += dir.dr, c += dir.dc) {
                            antinodes.Add((r, c));
                        }
                    }
                }
            }
        }
        
        var answer = antinodes.Count;
        Console.WriteLine($"Answer: {answer}");
    }
}