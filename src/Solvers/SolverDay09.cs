using System.Text;

namespace aoc_2024.Solvers;

public class SolverDay09 : ISolver {
    [PuzzleInput("09-01")]
    public void Solve(string[] input) {
        var line = input[0];
        var files = new LinkedList<(int id, int count, int size)>();
        var spaces = new LinkedList<(int size, int start)>();
        var start = 0;
        for (var i = 0; i < line.Length; i += 2) {
            files.AddLast((i / 2, line[i] - '0', start));
            start += line[i] - '0';
            if (i + 1 >= line.Length) break;
            spaces.AddLast((line[i + 1] - '0', start));
            start += line[i + 1] - '0';
        }

        var result = new PriorityQueue<(int id, int pos), int>();

        var right = files.Last;

        while (right is not null) {
            var (id, size, pos) = right.Value;

            var firstSpace = spaces.First;
            while (firstSpace is not null && firstSpace.Value.size < size && firstSpace.Value.start < pos) {
                firstSpace = firstSpace.Next;
            }

            if (firstSpace is not null && firstSpace.Value.start < pos) {
                var (spaceSize, spacePos) = firstSpace.Value;
                var newStart = spacePos;
                while (size-- > 0) {
                    result.Enqueue((id, newStart), newStart++);
                    spaceSize--;
                }

                var tmp = right.Previous;
                files.Remove(right);
                right = tmp;

                if (spaceSize == 0) {
                    spaces.Remove(firstSpace);
                }
                else {
                    firstSpace.Value = (spaceSize, newStart);
                }
            }
            else {
                right = right.Previous;
            }
        }
        
        while (files.Count > 0) {
            var (id, count, pos) = files.First.Value;
            files.RemoveFirst();
            while (count-- > 0) {
                result.Enqueue((id, pos), pos++);
            }
        }

        var answer = 0L;
        var p = 0;
        while (result.Count > 0) {
            var (id, pos) = result.Dequeue();
            while (p < pos) {
                p++;
            }

            answer += id * p;
            p++;
        }

        Console.WriteLine($"Answer: {answer}");
    }
}