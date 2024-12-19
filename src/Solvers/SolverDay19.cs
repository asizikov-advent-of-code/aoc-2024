namespace aoc_2024.Solvers;

public class SolverDay19 : ISolver {
    [PuzzleInput("19-01")]
    public void Solve(string[] input) {
        var colors = new List<string>(input[0].Split(", ")).OrderBy(l => -l.Length).ToList();
        var patterns = new List<string>();
        for (var i = 2; i < input.Length; i++) {
            patterns.Add(input[i]);
        }

        var answer = 0L;
        foreach (var pattern in patterns) {
            Console.WriteLine(pattern);
            answer += TryCompose(pattern, 0, new Dictionary<int, long>());
        }
        Console.WriteLine(answer);
        
        long TryCompose(string pattern, int pos, Dictionary<int, long> memo) {
            if (pos >= pattern.Length) return 1;
            if (memo.ContainsKey(pos)) return memo[pos];

            var ways = 0L;
            foreach (var substring in colors) {
                if (substring.Length + pos > pattern.Length) continue;
                if (pattern.Substring(pos, substring.Length) == substring) {
                    ways += TryCompose(pattern, pos + substring.Length, memo);
                }
            }
            memo[pos] = ways;
            return ways;
        }
    }
}