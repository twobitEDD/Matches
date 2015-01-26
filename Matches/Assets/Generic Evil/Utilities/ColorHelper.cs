using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class ColorHelper
{
    #region Random Colors
    private static System.Random Random = new System.Random();
    /// <summary>
    /// Returns a random bright color. A bright color is one that has a high saturation.
    /// </summary>
    public static Color RandomBrightColor()
    {
        // Generating a bright color is easy in a HSL!
        ColorHSL randomColor = new ColorHSL();
        randomColor.h = RandomValue(); // Generate a random hue. This defines what color we will have.
        randomColor.s = RandomRange(0.6f, 1f); // Set the saturation high to keep the color vivid.
        randomColor.l = RandomRange(0.45f, 0.6f); // Set lightness to the middle, so the color isnt dark or washed out
        randomColor.a = 1;

        return randomColor; // ColorTool's HSL implementation implicity converts back to a Unity Color!
    }

    /// <summary>
    /// Returns a random pastal color. A pastal color is one that has a low saturation and a medium lightness.
    /// </summary>
    public static Color RandomPastelColor()
    {
        ColorHSL randomColor = new ColorHSL();
        randomColor.h = RandomValue(); 
        randomColor.s = RandomRange(0.3f, 0.4f);
        randomColor.l = RandomRange(0.45f, 0.6f); 
        randomColor.a = 1;

        return randomColor; 
    }

    // The following Random color methods work in a similar way, but with different Satuation and lightness settings

    /// <summary>
    /// Returns a random dark color. 
    /// </summary>
    public static Color RandomDarkColor()
    {
        ColorHSL randomColor = new ColorHSL();
        randomColor.h = RandomValue(); 
        randomColor.s = 0.8f;
        randomColor.l = 0.1f; 
        randomColor.a = 1;

        return randomColor;
    }

    public static Color RandomColor(float saturation, float lightness)
    {
        ColorHSL randomColor = new ColorHSL();
        randomColor.h = RandomValue();
        randomColor.s = saturation;
        randomColor.l = lightness;
        randomColor.a = 1;

        return randomColor;
    }
    #endregion

    #region Blends
    public static Color Blend(Color a, Color b, BlendStyle style)
    {
        Color output = Colors.White;
        switch (style)
        {
            case BlendStyle.Multiply:
                output = Multiply(a, b);
                break;
            case BlendStyle.Screen:
                output = Screen(a, b);
                break;
            case BlendStyle.Overlay:
                output = Overlay(a, b);
                break;
            case BlendStyle.Difference:
                output = Difference(a, b);
                break;
            case BlendStyle.Average:
                output = Average(a, b);
                break;
            case BlendStyle.Negation:
                output = Negation(a, b);
                break;
        }
        return output;
    }

    public static Color Multiply(Color a, Color b)
    {
        return a*b;
    }

    public static Color Screen(Color a, Color b)
    {
        return a + b - a * b;
    }

    public static Color Overlay(Color a, Color b)
    {
        var ar = a.r; var br = b.r;
        var ag = a.g; var bg = b.g;
        var ab = a.b; var bb = b.b;
        ar = (ar <= 0.5f) ? (ar * 2f) * br : ar + br - ar * br;
        ag = (ag <= 0.5f) ? (ag * 2f) * bg : ag + bg - ag * bg;
        ab = (ab <= 0.5f) ? (ab * 2f) * bb : ab + bb - ab * bb;
        
        return new Color(ar, ag, ab, a.a);
    }

    public static Color Difference(Color a, Color b)
    {
        Color ret = new Color();
        ret.r = Math.Abs(a.r - b.r);
        ret.g = Math.Abs(a.g - b.g);
        ret.b = Math.Abs(a.b - b.b);
        ret.a = a.a;
        return ret;
    }

    public static Color Average(Color a, Color b)
    {
        Color ret = new Color();
        ret.r =(a.r + b.r) / 2f;
        ret.g =(a.g + b.g) / 2f;
        ret.b =(a.b + b.b) / 2f;
        ret.a = a.a;
        return ret;
    }

    public static Color Negation(Color a, Color b)
    {
        Color ret = new Color();
        ret.r = 1 - Math.Abs(a.r + b.r - 1);
        ret.g = 1 - Math.Abs(a.g + b.g - 1);
        ret.b = 1 - Math.Abs(a.b + b.b - 1);
        ret.a = a.a;
        return ret;
    }

    public static Color Mix(Color a, Color b)
    {
        return Color.Lerp(a, b, 0.5f);
    }
    #endregion

    #region Simple coroutine color mixers
    public static IEnumerator LerpOverTime(Action<Color> set, Color from, Color to, float time)
    {
        var handle = DoLerp(set, from, to, time);
        GetCoroutineManager().StartCoroutine(handle);
        return handle;
    }

    public static IEnumerator LerpOverTime(List<Color> target, List<Color> from, List<Color> to, float time)
    {
        if (from.Count != to.Count)
        {
            Debug.LogError("Lerp over time failed, the Lists are different sizes");
            return null;
        }
        var handle = DoLerp(target, from, to, time);
        GetCoroutineManager().StartCoroutine(handle);
        return handle;
    }

    public static IEnumerator LerpOverTime(Color[] target, Color[] from, Color[] to, float time)
    {
        if (from.Length != to.Length)
        {
            Debug.LogError("Lerp over time failed, the Arrays are different sizes");
            return null;
        }

        var handle = DoLerp(target, from, to, time);
        GetCoroutineManager().StartCoroutine(handle);
        return handle;
    }

 

    // Think of having a Color Class, then you could pass colors by reference and this would work?
    public static IEnumerator DoLerp(Action<Color> set, Color from, Color to, float time)
    {
        float timer = 0;
        while (timer <= time)
        {
            timer += Time.deltaTime;
            set(Color.Lerp(from, to, timer / time));
            yield return null;
        }
        set(to);
    }

    public static IEnumerator DoLerp(List<Color> target, List<Color> from, List<Color> to, float time)
    {
        float timer = 0;
        while (timer <= time)
        {
            timer += Time.deltaTime;
            float lerp = timer / time;
            for (int i = 0; i < from.Count; i++ )
            {
                target[i] = Color.Lerp(from[i], to[i], lerp);
            }
            
            yield return null;
        }

        for (int i = 0; i < from.Count; i++)
        {
            target[i] = to[i];
        }
    }

    public static IEnumerator DoLerp(Color[] target, Color[] from, Color[] to, float time)
    {
        float timer = 0;
        while (timer <= time)
        {
            timer += Time.deltaTime;
            float lerp = timer / time;
            for (int i = 0; i < from.Length; i++)
            {
                target[i] = Color.Lerp(from[i], to[i], lerp);
            }

            yield return null;
        }

        for (int i = 0; i < from.Length; i++)
        {
            target[i] = to[i];
        }
    }

	public static void CancelLerpOverTime(IEnumerator handle)
	{
		GetCoroutineManager ().StopCoroutine (handle);
	}
    #endregion

    #region Helper stuff
    private static CoroutineRunner coroutineManager;

    private static CoroutineRunner GetCoroutineManager()
    {
        if (coroutineManager == null)
        {
            var go = new GameObject();
            go.hideFlags = HideFlags.HideAndDontSave;
            coroutineManager = go.AddComponent<CoroutineRunner>();
        }
        return coroutineManager;
    }

    private static float RandomValue()
    {
        return (float)Random.NextDouble();
    }

    private static float RandomRange(float min, float max)
    {
        return min + ((float)Random.NextDouble() * (max - min));
    }
    #endregion
}

public enum BlendStyle
{
    Multiply,
    Screen,
    Overlay,
    Difference,
    Average,
    Negation
}
