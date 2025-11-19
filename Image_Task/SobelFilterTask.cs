using System;

namespace Recognizer;
internal static class SobelFilterTask
{
    public static double[,] SobelFilter(double[,] g, double[,] sx)
    {
        var width = g.GetLength(0);
        var height = g.GetLength(1);
        var result = new double[width, height];
        var sy = Transpose(sx);

        for (var x = 1; x < width - 1; x++)
            for (var y = 1; y < height - 1; y++)
            {
                var gx = Convolve(g, sx, x, y);
                var gy = Convolve(g, sy, x, y);
                result[x, y] = Math.Sqrt(gx * gx + gy * gy);
            }
        return result;
    }

    private static double[,] Transpose(double[,] matrix)
    {
        var rows = matrix.GetLength(0);
        var cols = matrix.GetLength(1);
        var result = new double[cols, rows];

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                result[j, i] = matrix[i, j];
            }
        }
        return result;
    }

    private static double Convolve(double[,] g, double[,] filter, int x, int y)
    {
        var sum = 0.0;
        var filterSize = filter.GetLength(0);
        var offset = filterSize / 2;

        for (var i = 0; i < filterSize; i++)
        {
            for (var j = 0; j < filterSize; j++)
            {
                sum += g[x - offset + i, y - offset + j] * filter[i, j];
            }
        }
        return sum;
    }
}