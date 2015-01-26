using UnityEngine;
using System;
using System.Collections;

public static class ColorExtentions
{
    /// <summary>
    /// Returns a color that is Lighter. Lightness is incresed aboslutly by difference.
    /// </summary>
    public static Color Lighten(this Color c, float difference)
    {
        ColorHSL c2 = c;
        c2.l += difference;

        // clamp it!
        if (c2.l < 0f)
        {
            c2.l = 0f;
        }
        if (c2.l > 1f)
        {
            c2.l = 1f;
        }

        return c2;
    }

    /// <summary>
    /// Returns a color that is Darker. Lightness is decresed aboslutly by difference.
    /// </summary>
    public static Color Darken(this Color c, float difference)
    {
        ColorHSL c2 = c;
        c2.l -= difference;

        if (c2.l < 0f)
        {
            c2.l = 0f;
        }
        if (c2.l > 1f)
        {
            c2.l = 1f;
        }

        return c2;
    }

    /// <summary>
    /// Returns a color that is Lighter. Lightness is incresed reletivly by percentage.
    /// </summary>
    public static Color LightenRelative(this Color c, float percent)
    {
        ColorHSL c2 = c;
        c2.l += percent * (1 - c2.l);

        // clamp it!
        if (c2.l < 0f)
        {
            c2.l = 0f;
        }
        if (c2.l > 1f)
        {
            c2.l = 1f;
        }

        return c2;
    }

    /// <summary>
    /// Returns a color that is Darker. Lightness is decresed reletivly by percentage.
    /// </summary>
    public static Color DarkenRelative(this Color c, float percent)
    {
        ColorHSL c2 = c;
        c2.l -= percent * c2.l;

        if (c2.l < 0f)
        {
            c2.l = 0f;
        }
        if (c2.l > 1f)
        {
            c2.l = 1f;
        }

        return c2;
    }

    /// <summary>
    /// Returns a color that is Desaturated. Saturation is decresed aboslutly by difference.
    /// </summary>
    public static Color Desaturate(this Color c, float difference)
    {
        ColorHSL c2 = c;
        c2.s -= difference;

        if (c2.s < 0f)
        {
            c2.s = 0f;
        }
        if (c2.s > 1f)
        {
            c2.s = 1f;
        }

        return c2;
    }

    /// <summary>
    /// Returns a color that is Saturated. Saturation is incresed aboslutly by difference.
    /// </summary>
    public static Color Saturate(this Color c, float difference)
    {
        ColorHSL c2 = c;
        c2.s += difference;

        if (c2.s < 0f)
        {
            c2.s = 0f;
        }
        if (c2.s > 1f)
        {
            c2.s = 1f;
        }

        return c2;
    }

    /// <summary>
    /// Returns a color that is Desaturated. Saturation is decresed reletivly by percentage.
    /// </summary>
    public static Color DesaturateRelative(this Color c, float percent)
    {
        ColorHSL c2 = c;
        c2.s -= percent * c2.s;

        if (c2.s < 0f)
        {
            c2.s = 0f;
        }
        if (c2.s > 1f)
        {
            c2.s = 1f;
        }

        return c2;
    }

    /// <summary>
    /// Returns a color that is Saturated. Saturation is incresed reletivly by percentage.
    /// </summary>
    public static Color SaturateRelative(this Color c, float percent)
    {
        ColorHSL c2 = c;
        c2.s += percent * c2.s;

        if (c2.s < 0f)
        {
            c2.s = 0f;
        }
        if (c2.s > 1f)
        {
            c2.s = 1f;
        }

        return c2;
    }

    /// <summary>
    /// Convert this color to its grayscale representation.
    /// </summary>
    public static Color Grayscale(this Color c)
    {
        ColorHSL c2 = c;
        c2.s = 0;

        return c2;
    }

    /// <summary>
    /// Gets the relative brightness of a color, 0 for total black and 1 for complete white.
    /// </summary>
    public static float Luminosity(this Color c)
    {
        return ((ColorHSL)c).l;
    }

    /// <summary>
    /// Gets the color that contrasts the most with this, based on Luminosity/brigthness
    /// </summary>
    public static Color SelectContrasting(this Color c, Color a, Color b)
    {
        var l = c.Luminosity();
        var al = a.Luminosity();
        var bl = b.Luminosity();

        var diffA = Mathf.Abs(l - al);
        var diffB = Mathf.Abs(l - bl);
        if (diffA > diffB)
            return a;
        return b;
    }

    /// <summary>
    /// Returns true if the other color is visually similar. (have close by Hue values)
    /// </summary>
    public static bool IsSimilar(this Color c, Color other)
    {
        var areSimilar = false;
        var h1 = ((ColorHSL)c).h;
        var h2 = ((ColorHSL)other).h;
        float num = Mathf.Repeat(h1 - h2, 1f);
        if (num >= 0.5f) num -= 1f;
        if (Mathf.Abs(num) <= 0.15f)
            areSimilar = true;
        return areSimilar;
    }

    /// <summary>
    /// Returns how similar another color is to this, in a range of 1 to 0.
    /// If the Hues are the same, this will return 0. If they are opposite 
    /// this will return 1.
    /// </summary>
    public static float HowSimilar(this Color c, Color other)
    {
        var h1 = ((ColorHSL)c).h;
        var h2 = ((ColorHSL)other).h;
        float num = Mathf.Repeat(h1 - h2, 1f);
        if (num >= 0.5f) num -= 1f;
        
        return Mathf.Abs(num * 2f);
    }

    /// <summary>
    /// Returns a color with a flipped hue
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public static Color InvertHue(this Color c)
    {
        ((ColorHSL)c).InvertHue();
        return c;
    }
}

