namespace aoc_2024.Solvers;

public class SolverDay09 : ISolver {
    [PuzzleInput("09-01")]
    public void Solve(string[] input) {
        var line = input[0];
        var queue = new LinkedList<(int id, int count)>();
        for (var i = 0; i < line.Length; i+=2) {
            queue.AddLast((i / 2, line[i] - '0'));
        }
        
        var answer = 0L;
        var leftPos = 0;
        
        using var leftEnum = GetLeft().GetEnumerator();
        using var rightEnum = GetRight().GetEnumerator();
        
        var p = 0;
        while(queue.Count > 0 && p < line.Length-1) {
            for (var d = 0; d < line[p] - '0' && queue.Count >0; d++) {
                leftEnum.MoveNext();
                answer += leftPos * leftEnum.Current;
                leftPos++;
            }
            for (var d = 0; d < line[p+1] - '0' && queue.Count > 0; d++) {
                rightEnum.MoveNext();
                answer += leftPos * rightEnum.Current;
                leftPos++;
            }
            p+=2;

        }
        Console.WriteLine($"Answer: {answer}");

        IEnumerable<int> GetLeft() {
           while (queue.Count > 0) {
               var (id, count) = queue.First.Value;
               queue.RemoveFirst();
               if (count > 1) queue.AddFirst((id, count - 1));
               yield return id;
           }
        }
        
        IEnumerable<int> GetRight() {
            while (queue.Count > 0) {
                var (id, count) = queue.Last.Value;
                queue.RemoveLast();
                if (count > 1) queue.AddLast((id, count - 1));
                yield return id;
            }
        }
    }
}