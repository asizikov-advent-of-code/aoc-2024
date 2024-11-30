namespace aoc_2024.Solvers;

[AttributeUsage(AttributeTargets.Method)]
public class PuzzleInputAttribute(string fileName) : Attribute {
    public string FileName { get; } = fileName;
}