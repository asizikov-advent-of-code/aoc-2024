namespace aoc_2024;

public static class PuzzleInputReader {
    public static string[] ReadLines(string fileName) {
        var path = $"InputFiles/{fileName}.txt";
        if (!File.Exists(path)) {
            Console.WriteLine($"Couldn't find {fileName}");
            throw new Exception();
        }

        return File.ReadAllLines(path);
    }
}