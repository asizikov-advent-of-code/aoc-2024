namespace aoc_2024.Solvers;

public class SolverDay09 : ISolver {
    [PuzzleInput("09-01")]
    public void Solve(string[] input) {
        var line = input[0];
        var files = new LinkedList<(int id, int count, int size)>();
        var spaces = new LinkedList<(int size, int start)>();
        var start = 0;
        for (var i = 0; i < line.Length; i++) {
            var value = line[i] - '0';
            if (i % 2 == 0) files.AddLast((i / 2, value, start));
            else spaces.AddLast((value, start));
            start += value;
        }

        var result = new PriorityQueue<(int id, int pos), int>();
        var right = files.Last;

        while (right is not null) {
            var (id, size, pos) = right.Value;

            var space = spaces.First;
            for (; space is {} && space.Value.size < size && space.Value.start < pos; space = space.Next);
            
            if (space is not null && space.Value.start < pos) {
                var (spaceSize, spacePos) = space.Value;
                var newStart = spacePos;
                while (size --> 0) {
                    result.Enqueue((id, newStart), newStart++);
                    spaceSize--;
                }

                var tmp = right.Previous;
                files.Remove(right);
                right = tmp;

                if (spaceSize is 0) spaces.Remove(space);
                else space.Value = (spaceSize, newStart);
            }
            else right = right.Previous;
        }
        
        while (files.Count > 0) {
            var (id, count, pos) = files.First.Value;
            files.RemoveFirst();
            while (count --> 0) result.Enqueue((id, pos), pos++);
        }

        var (answer, p) = (0L, 0);
        while (result.Count > 0) {
            var (id, pos) = result.Dequeue();
            while (p < pos) { p++; }
            answer += id * p++;
        }

        Console.WriteLine($"Answer: {answer}");
    }
}