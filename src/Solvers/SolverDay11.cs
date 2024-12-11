namespace aoc_2024.Solvers;

public class SolverDay11 : ISolver {
    
    [PuzzleInput("11-01")]
    public void Solve(string[] input) {
        var stones = input[0].Split(" ").ToList();
        var cache = new Dictionary<(int, string), long>();
        var answer = stones.Sum(stone => Build(75, stone));
        Console.WriteLine($"Answer: {answer}");
        
        long Build(int iteration, string stone) {
            if (cache.ContainsKey((iteration, stone))) return cache[(iteration, stone)];
            if (iteration is 0) { return 1; }

            long res;
            if (stone is "0") {
                res = Build(iteration - 1, "1");
            } else if (stone.Length % 2 == 0) {
                var half = stone.Length / 2;
                var firstHalf = stone.Substring(0, half);
                var secondHalf = stone.Substring(half);
                secondHalf = secondHalf.TrimStart('0');
                secondHalf = secondHalf is "" ? "0" : secondHalf;

                res = Build(iteration - 1, firstHalf) + Build(iteration - 1, secondHalf);
            } else {
                var val = long.Parse(stone);
                res = Build(iteration - 1, (val * 2024).ToString());
            }

            cache[(iteration, stone)] = res;
            return res;
        }
    }
}