namespace aoc_2024.Solvers;

public class SolverDay07 : ISolver {
    [PuzzleInput("07-01")]
    public void Solve(string[] input) {
        var answer = 0L;

        foreach (var line in input) {
            var parts = line.Split(": ");
            var target = long.Parse(parts[0]);
            var values = parts[1].Split(" ").Select(long.Parse).ToArray();
            if (CanBuild(target,values.AsSpan() , 0L)) {
                answer+= target;
            }
        }

        Console.WriteLine($"Answer: {answer}");


        bool CanBuild(long target, Span<long> values, long aggregate) {
            if (values.Length is 0) return target == aggregate;
            if (aggregate is 0) return CanBuild(target, values[1..], values[0]);
            if (aggregate > target) return false;
            return
                CanBuild(target, values[1..], checked(aggregate + values[0]))
                || CanBuild(target, values[1..], checked(aggregate * values[0]))
                || CanBuild(target, values[1..], checked(aggregate * (long)Math.Pow(10, (int)Math.Log10(values[0]) + 1) + values[0]));
        }
    }
}