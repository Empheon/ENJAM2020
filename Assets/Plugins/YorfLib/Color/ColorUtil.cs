using UnityEngine;

namespace YorfLib
{
	public static class ColorUtil
	{
		public static Color LerpHSV(Color a, Color b, float t)
		{
			return LerpHSV(a.ToHSV(), b.ToHSV(), t);
		}

		public static Color LerpHSV(ColorHSV a, ColorHSV b, float t)
		{
			// Hue interpolation
			float h;
			float d = b.h - a.h;
			if (a.h > b.h)
			{
				// Swap (a.h, b.h)
				var h3 = b.h;
				b.h = a.h;
				a.h = h3;

				d = -d;
				t = 1 - t;
			}

			if (d > 0.5) // 180deg
			{
				a.h = a.h + 1; // 360deg
				h = (a.h + t * (b.h - a.h)) % 1; // 360deg
			}
			else
			{
				h = a.h + t * d;
			}

			// Interpolates the rest
			return new ColorHSV
			(
				h,          // H
				a.s + t * (b.s - a.s),  // S
				a.v + t * (b.v - a.v),  // V
				a.a + t * (b.a - a.a)   // A
			);
		}

		public static Color EvaluateHSV(this Gradient gradient, float time)
		{
			time = Mathf.Repeat(time, 1.0F);

			GradientColorKey[] keys = gradient.colorKeys;
			GradientColorKey lastColor = keys[0];
			GradientColorKey nextColor = keys[1];

			for (int i = 0; i < keys.Length - 1; i++)
			{
				if (time <= keys[i + 1].time)
				{
					nextColor = keys[i + 1];
					break;
				}

				lastColor = keys[i + 1];
			}

			return LerpHSV(lastColor.color, nextColor.color, (time - lastColor.time) / (nextColor.time - lastColor.time));
		}

		public static Color Negative(this Color color)
		{
			return new Color(1.0F - color.r, 1.0F - color.g, 1.0F - color.b, color.a);
		}

        public static Texture2D CreateTexture(this Gradient gradient, int width = 128)
        {
            Texture2D texture2D = new Texture2D(width, 1, TextureFormat.RGBA32, false);
            texture2D.wrapMode = TextureWrapMode.Clamp;

            for (int x = 0; x < width; x++)
            {
                texture2D.SetPixel(x, 0, gradient.Evaluate(x / (float) width));
            }

            texture2D.Apply();

            return texture2D;
        }

        public static Color Lighter(this Color color, float value)
        {
            return new Color(color.r + value, color.b + value, color.g + value, color.a);
        }

        public static Color Darker(this Color color, float value)
        {
            return new Color(color.r - value, color.b - value, color.g - value, color.a);
        }
    }
}
