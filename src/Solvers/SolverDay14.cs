using System.Text.RegularExpressions;

namespace aoc_2024.Solvers;

public class SolverDay14 : ISolver {
    [PuzzleInput("14-01")]
    public void Solve(string[] input) {
        var robots = new List<(int c, int r, int dc, int dr)>();
        var positionRegex = new Regex("p=([0-9]+,[0-9]+)");
        var velocityRegex = new Regex("v=(-?[0-9]+,-?[0-9]+)");
        foreach (var line in input) {
            var velocity = velocityRegex.Match(line).Groups[1].Value.Split(",").Select(int.Parse).ToArray();
            var position = positionRegex.Match(line).Groups[1].Value.Split(",").Select(int.Parse).ToArray();
            robots.Add((position[0], position[1], velocity[0], velocity[1]));
        }

        // var (roomR, roomC) = (7, 11);
        var (roomR, roomC) = (103, 101);
        var iterations = 100;
        var next = new List<(int c, int r, int dc, int dr)>();
        var quadrants = new[] { 0L, 0L, 0L, 0L };
        foreach (var robot in robots) {
            var r = (robot.r + robot.dr * iterations) % roomR;
            if (r < 0) r += roomR;
            var c = (robot.c + robot.dc * iterations) % roomC;
            if (c < 0) c += roomC;
            next.Add((c, r, robot.dc, robot.dr));
            var midleRow = roomR / 2;
            var midleCol = roomC / 2;
            if (r < midleRow && c < midleCol) quadrants[0]++;
            if (r < midleRow && c > midleCol) quadrants[1]++;
            if (r > midleRow && c < midleCol) quadrants[2]++;
            if (r > midleRow && c > midleCol) quadrants[3]++;
        }
        robots = next;
        
        var finalRoom = new char[roomR, roomC];
        for (var r = 0; r < roomR; r++) {
            for (var c = 0; c < roomC; c++) {
                finalRoom[r, c] = '.';
            }
        }
        foreach (var robot in robots) {
            if (finalRoom[robot.r, robot.c] == '.') finalRoom[robot.r, robot.c] = '1';
            else finalRoom[robot.r, robot.c] = (char)(finalRoom[robot.r, robot.c]+ 1);
        }
        
        for (var r = 0; r < roomR; r++) {
            for (var c = 0; c < roomC; c++) {
                Console.Write(finalRoom[r, c]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
        
        for (var r = 0; r < roomR; r++) {
            if (r == roomR /2) {
                for (var c = 0; c < roomC; c++) {
                    if (c == roomC / 2) Console.Write("+");
                    else Console.Write("-");
                }
            }
            else {
                for (var c = 0; c < roomC; c++) {
                    if (c == roomC / 2) Console.Write("|");
                    else
                    Console.Write(finalRoom[r, c]);
                }                    
            }
            
            Console.WriteLine();
        }
        
        Console.WriteLine($"Quadrants: {string.Join(", ", quadrants)}");
        
        var answer = quadrants[0] * quadrants[1] * quadrants[2] * quadrants[3];

        Console.WriteLine($"Answer: {answer}");
    }
}