namespace aoc_2024.Solvers;

public class SolverDay19 : ISolver {
    [PuzzleInput("19-01")]
    public void Solve(string[] input) {
        var colors = new List<string>(input[0].Split(", ")).OrderBy(l => -l.Length).ToList();
        var patterns = new List<string>();
        for (var i = 2; i < input.Length; i++) {
            patterns.Add(input[i]);
        }

        var answer = 0;
        foreach (var pattern in patterns) {
            Console.WriteLine(pattern);
            if (TryCompose(pattern, 0,new Dictionary<int, bool>())) answer++;
        }
        Console.WriteLine(answer);
        
        bool TryCompose(string pattern, int pos, Dictionary<int, bool> memo) {
            if (pos >= pattern.Length) return true;
            if (memo.ContainsKey(pos)) return memo[pos];

            foreach (var substring in colors) {
                if (substring.Length + pos > pattern.Length) continue;
                if (pattern.Substring(pos, substring.Length) == substring) {
                    if (TryCompose(pattern, pos + substring.Length, memo)) {
                        memo[pos] = true;
                        return true;
                    }
                }
            }
            memo[pos] = false;
            return false;
        }
    }
}