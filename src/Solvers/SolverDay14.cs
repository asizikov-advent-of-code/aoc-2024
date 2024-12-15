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
        for (var iterations = 1; iterations < 8913; iterations++) {
            var next = new List<(int c, int r, int dc, int dr)>();
            foreach (var robot in robots) {
                var r = (robot.r + robot.dr * iterations) % roomR;
                if (r < 0) r += roomR;
                var c = (robot.c + robot.dc * iterations) % roomC;
                if (c < 0) c += roomC;
                next.Add((c, r, robot.dc, robot.dr));
            }

            robots = next;

            var finalRoom = new char[roomR, roomC];
            for (var r = 0; r < roomR; r++) {
                for (var c = 0; c < roomC; c++) {
                    finalRoom[r, c] = '.';
                }
            }

            foreach (var robot in robots) {
                finalRoom[robot.r, robot.c] = '#';
            }
            
            // check if we have triangle share
            /*
             *     .#.
             *    .###.
             *   .#####.
             */
            
            var count = 0;
            for (var r = 0; r < roomR - 2; r++) {
                for (var c = 0; c < roomC - 2; c++) {
                    if (r + 2 >= roomR || c + 2 >= roomC || r - 2 < 0 || c - 2 < 0) {
                        continue;
                    }
                    if (finalRoom[r, c] == '#' && finalRoom[r + 1, c - 1] == '#' && finalRoom[r + 1, c + 1] == '#' && finalRoom[r + 2, c - 2] == '#' && finalRoom[r + 2, c - 1] == '#' && finalRoom[r + 2, c] == '#' && finalRoom[r + 2, c + 1] == '#' && finalRoom[r + 2, c + 2] == '#') {
                        count++;
                    }
                }
            }
            
            if (count == 0 ) {
                continue;
            }
            
            Console.WriteLine($"Iteration: {iterations}");
            Console.WriteLine("====================================");
            for (var r = 0; r < roomR; r++) {
                for (var c = 0; c < roomC; c++) {
                    Console.Write(finalRoom[r, c]);
                }

                Console.WriteLine();
            }

            Console.WriteLine("====================================");

        }
    }
}