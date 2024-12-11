namespace aoc_2024.Solvers;

public class SolverDay11 : ISolver {
    [PuzzleInput("11-01")]
    public void Solve(string[] input) {
        
        var stones = input[0].Split(" ").ToList();
        for (var i = 0; i < 25; i++) {
            var next = new List<string>();
            foreach(var stone in stones) {
                if(stone is "0") next.Add("1");
                else if (stone.Length % 2 == 0) {
                    var half = stone.Length / 2;
                    next.Add(stone.Substring(0, half));
                    var secondHalf = stone.Substring(half);
                    secondHalf = secondHalf.TrimStart('0');
                    next.Add(secondHalf is "" ? "0" : secondHalf);
                }
                else {
                    var val = long.Parse(stone);
                    next.Add((val * 2024).ToString());
                }
            }
            stones = next;
        }
        
        var answer = stones.Count;
        Console.WriteLine($"Answer: {answer}");
    }
}