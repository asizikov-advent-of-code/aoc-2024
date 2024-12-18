namespace aoc_2024.Utils;

public static class InputExtensions {
    public static IEnumerable<(int r, int c, char val)> Scan(this string[] grid) {
        for (var r = 0; r < grid.Length; r++) {
            for (var c = 0; c < grid[r].Length; c++) {
                yield return (r, c, grid[r][c]);
            }
        }
    }
    
    public static IEnumerable<(int r, int c, char val)> ScanFor(this string[] grid, char val) {
        for (var r = 0; r < grid.Length; r++) {
            for (var c = 0; c < grid[r].Length; c++) {
                if (grid[r][c] == val) yield return (r, c, grid[r][c]);
            }
        }
    }
    
    public static IEnumerable<(int r, int c, char val)> ScanFor(this IList<char[]> grid, char val) {
        for (var r = 0; r < grid.Count; r++) {
            for (var c = 0; c < grid[r].Length; c++) {
                if (grid[r][c] == val) yield return (r, c, grid[r][c]);
            }
        }
    }
    
    public static IEnumerable<(int r, int c, char val)> ScanExcept(this string[] grid, params char[] vals) {
        for (var r = 0; r < grid.Length; r++) {
            for (var c = 0; c < grid[r].Length; c++) {
                if (!vals.Contains(grid[r][c])) yield return (r, c, grid[r][c]);
            }
        }
    }
    
    public static bool IsInBounds(this char[][] grid, (int r, int c) pos) {
        return pos.r >= 0 && pos.r < grid.Length && pos.c >= 0 && pos.c < grid[pos.r].Length;
    }
    public static bool IsInBounds(this string[] grid, int r, int c) {
        return r >= 0 && r < grid.Length && c >= 0 && c < grid[r].Length;
    }
    
    public static bool IsInBounds(this string[] grid, (int r, int c) pos) {
        return pos.r >= 0 && pos.r < grid.Length && pos.c >= 0 && pos.c < grid[pos.r].Length;
    }
}