namespace aoc_2024.Solvers;

public class SolverDay05 : ISolver {
    [PuzzleInput("05-01")]
    public void Solve(string[] input) {
        var order = new Dictionary<int, HashSet<int>>();
        var answer = 0;
        foreach (var line in input) {
            if (line.Contains('|')) {
                var parts = line.Split('|');
                var before = int.Parse(parts[0]);
                var after = int.Parse(parts[1]);
                
                order.TryAdd(before, []);
                order.TryAdd(after, []);
                
                order[before].Add(after);
                
            } else if (line.Contains(',')) {
                var parts = line.Split(',').Select(int.Parse).ToList();
                if (ValidateOrder(parts)) {
                    var middleElement = parts[parts.Count / 2];
                    answer += middleElement;
                    Console.WriteLine($"Middle element: {middleElement}");
                }
            }
        }
        
        Console.WriteLine($"Answer: {answer}");
        
        bool ValidateOrder(List<int> sequence) {
            var sorted = new List<int>(sequence);
            sorted.Sort((a, b) => {
                if (order[a].Contains(b)) return -1;
                if (order[b].Contains(a)) return 1;
                return 0;
            });
            
            return sequence.SequenceEqual(sorted);
        }
    }
}