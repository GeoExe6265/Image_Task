using System;
namespace Recognizer;

public static class ThresholdFilterTask
{
	public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
	{
		var width = original.GetLength(0);
		var height = original.GetLength(1);
		double[,] brightness = new double[width, height];
		if (whitePixelsFraction == 0)
		{
			return brightness;
		}

		if (whitePixelsFraction == 1)
		{
			for (var x = 0; x < width; x++)
			{
				for (var y = 0; y < height; y++)
				{
					brightness[x, y] = 1.0;
				}
			}
			return brightness;
		}

		double[] brightnessValue = new double[width * height];
		var count = 0;
		for (var x = 0; x < width; x++)
		{
			for (var y = 0; y < height; y++)
			{
				brightnessValue[count] = original[x, y];
				count += 1;
			}
		}

		Array.Sort(brightnessValue);
		var tCount = (int)(brightnessValue.Length * (1 - whitePixelsFraction));
		var t = brightnessValue[tCount];

		for (var x = 0; x < width; x++)
		{
			for (var y = 0; y < height; y++)
			{
				if (original[x,y] >= t)
				{
					brightness[x,y] = 1.0;
				}
				else
				{
					brightness[x,y] = 0.0;
				}
			}
		}
		return brightness;
	}
}
