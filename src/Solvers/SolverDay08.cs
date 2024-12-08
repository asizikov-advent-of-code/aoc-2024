namespace aoc_2024.Solvers;

public class SolverDay08 : ISolver {
    [PuzzleInput("08-01")]
    public void Solve(string[] input) {
        var antennas = new Dictionary<char, List<(int r, int c)>>();
        for (var r = 0; r < input.Length; r++) {
            for (var c = 0; c < input[r].Length; c++) {
                if (input[r][c] == '.') continue;
                antennas.TryAdd(input[r][c], []);
                antennas[input[r][c]].Add((r, c));
            }
        }

        var antinodes = new HashSet<(int r, int c)>();
        foreach (var frequency in antennas.Keys) {
            var positions = antennas[frequency];
            for (var i = 0; i < positions.Count - 1; i++) {
                for (var j = i + 1; j < positions.Count; j++) {
                    var (first, second) = (positions[i], positions[j]);
                    var dr = second.r - first.r;
                    var dc = second.c - first.c;

                    (int r, int c) antinode1 = (first.r - dr, first.c - dc);
                    (int r, int c) antinode2 = (second.r + dr, second.c + dc);

                    if (antinode1.r >= 0 && antinode1.r < input.Length && antinode1.c >= 0 && antinode1.c < input[antinode1.r].Length) {
                        antinodes.Add(antinode1);
                    }

                    if (antinode2.r >= 0 && antinode2.r < input.Length && antinode2.c >= 0 && antinode2.c < input[antinode2.r].Length) {
                        antinodes.Add(antinode2);
                    }
                }
            }
        }

        var answer = antinodes.Count;
        Console.WriteLine($"Answer: {answer}");
    }
}