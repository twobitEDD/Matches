using UnityEngine;
using System;
using System.Collections;

[Serializable]
public struct ColorHSL 
{
    public float h;
    public float s;
    public float l;
    public float a;

    #region Construtors
    public ColorHSL(float h, float s, float l, float a = 1)
    {
        this.h = h;
        this.s = s;
        this.l = l;
        this.a = a;
    }

    public ColorHSL(Color c)
    {
        ColorHSL temp = FromColor(c);
        h = temp.h;
        s = temp.s;
        l = temp.l;
        a = temp.a;
    }
    #endregion

    #region Conversion
    // Based on the algorithms published in the CSS 3 spec. http://www.w3.org/TR/css3-color/#hsl-color

    public static ColorHSL FromColor(Color color)
    {
        float h = 0, s = 0, l = 0, a = color.a;

        float max = Mathf.Max(color.r, Mathf.Max(color.g, color.b));
        float min = Mathf.Min(color.r, Mathf.Min(color.g, color.b));
        l = (max + min) / 2f;
        
        if (min != max)
        {
            float delta = max - min;
            s = l > 0.5f ? delta / (2f - max - min) : delta / (min + max);

            if (max == color.r)
            {
                h = (color.g - color.b) / delta + (color.g < color.b ? 6f : 0f);
            }
            else if (max == color.g)
            {
                h = (color.b - color.r) / delta + 2f;
            }
            else if (max == color.b)
            {
                h = (color.r - color.g) / delta + 4f;
            }

            h /= 6f;
        }

        return new ColorHSL(h, s, l, a);
    }

    public static Color ToColor(ColorHSL color)
    {
        float r, g, b, a = color.a;
        r = g = b = color.l;

        if (color.l <= 0)
            color.l = 0.001f;
        if (color.l >= 1)
            color.l = 0.999f;

        if (color.s != 0f)
        {
            var m2 = color.l < 0.5f ? color.l * (color.s + 1f) : color.l + color.s - color.l * color.s;
            var m1 = color.l * 2 - m2;
            r = ExtractRGB(m1, m2, color.h + 1f / 3f);
            g = ExtractRGB(m1, m2, color.h);
            b = ExtractRGB(m1, m2, color.h - 1f / 3f);
        }

        return new Color(r, g, b, a);
    }

    private static float ExtractRGB(float m1, float m2, float hue)
    {
        if (hue < 0) hue += 1;
        if (hue > 1f) hue -= 1;
        if (hue * 6f < 1f) return m1 + (m2 - m1) * hue * 6f;
        if (hue * 2f < 1f) return m2;
        if (hue * 3f < 2f) return m1 + (m2 - m1) * (2f / 3f - hue) * 6f;
        return m1;
    }
    #endregion

    #region Operators and overrides
    public static implicit operator ColorHSL(Color src)
    {
        return FromColor(src);
    }

    public static implicit operator Color(ColorHSL src)
    {
        return ToColor(src);
    }

    public override string ToString()
    {
        return string.Format("H: {0} S: {1} L: {2} A: {3}", h, s, l, a);
    }
    #endregion

    #region Helpers
    public void Invert()
    {
        // Hue acts like an angle, a hue of 0 is essentally the same as a hue of 1.
        h += 0.5f;
        if (h > 1f)
        {
            h -= 1f;
        }

        // Lightness and Saturation are much simpler to rotate
        s = 1 - s;
        l = 1 - l;
    }

    public void InvertHue()
    {

        h += 0.5f;
        if (h > 1f)
        {
            h -= 1f; 
        }
    }

    public void InvertLightness()
    {
        l = 1 - l;
    }

    public void InvertSaturation()
    {
        s = 1 - s;
    }
    #endregion
}
