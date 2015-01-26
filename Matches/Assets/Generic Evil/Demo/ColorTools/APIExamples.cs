using UnityEngine;
using System.Collections;

public class APIExamples : MonoBehaviour 
{
    public int randomColorCount = 5;
    public Color[] RandomBrightColors;
    public Color[] RandomPastelColors;
    public Color[] RandomDarkColors;

    public Color[] LerpOverTimeTargets;
    public Color lerpTarget, lerpTargetMethod;
    
    public BlendStyle BlendStyle;
    public Color BlendA, BlendB, BlendResult;

	IEnumerator Start () 
    {
        // PART 1: NAMED COLORS
        // The new Colors class has lots of ready to use color definitions
        Color red = Colors.Crimson;
        Color green = Colors.Chartreuse;
        Color blue = Colors.CornflowerBlue;
        Color pink = Colors.Pink;
        
        // PART 2: HSL COLORS
        // ColorTools adds a new color structure that complements the Color and Color32
        // structs that ship with Unity. Its called ColorHSL and is an implementation
        // of the HSL color space. HSL works differently to the RGB colors you may be used
        // to, and makes reasoning about color much more simple!

        // A quick breakdown of the three values and their ranges
        ColorHSL lightBlueHSL = new ColorHSL();
        // The hue value represents the shade of our color. 
        // Values close to 0 are red, 0.3 are green and 0.6 are blue
        lightBlueHSL.h = 0.55f; 
        // Saturation represents how strong a color is. values close to 0 are totally 
        // desaturated to gray. Values close to 1 are totally vivid!
        lightBlueHSL.s = 1;
        // Finally we have Lightness. As it moves towards 0, the color will tend towards
        // black. Values closer to 1 tend towards white.
        lightBlueHSL.l = 0.8f;
        // It is recommended you run the demo scene and play with the color picker to 
        // familiarise yourself with these values!

        // Its easy to convert HSL colors to unity colors so we can uses them.
        Color lightBlue = ColorHSL.ToColor(lightBlueHSL);
        // We can also go the other way
        ColorHSL pinkHSL = ColorHSL.FromColor(pink);
        // ColorTools also implements implicit operators, which automatically converts
        // between Color and HSLColor
        ColorHSL greenHsl = Colors.Green;
        Color green2 = greenHsl;

        // PART 3: COLOR TOOLS
        Color target = Colors.Blue;

        target = target.Lighten(0.25f); // Get a lighter blue. This converts blue to a HSL
                                        // color and adds 0.25f to its Lightness

        target = target.Darken(0.25f);  // darken back to the origonal color

        target = target.LightenRelative(0.25f); // This converts blue to a HSL and lightens it
                                        // Rather than adding the value directly to lightness,
                                        // this adds a proportional amount. Has a stronger effect
                                        // on dark colors than already light colors.
        
        // There are Saturate/Desaturate versions of these methods.

        Color grayScale = target.Grayscale(); // Converts a color to its grayscale representation
                                              // by setting its Saturation to 0.

        float luminosity = target.Luminosity(); // Returns a float that tells us how bight the color is.
        // values close to 0 are dark and values close to 1 are light.

        var contrast = target.SelectContrasting(Colors.White, Colors.Black); // Returns which of the 
                                        // two provided colors contrasts the most. This can be 
                                        // useful for things like making sure text is readable
                                        // over a background color.

        if (target.IsSimilar(lightBlue)) // Returns true if the other color has a similar hue.
        {
            target = target.InvertHue(); // Inverts the colors Hue
        }

        // The static ColorHelper class has a bunch of methods for generating random colors
        // See DemoRandomColors() for some simple examples. Every second it randomizes the colors
        // in the RandomBrightColors, RandomPastelColors and RandomDarkColors arrays. Check their
        // contents in the inspector!
        InvokeRepeating("DemoRandomColors", 0, 1);

        // We can also use ColorHelper to animate a color over time. 
        // We have to provide LerpOverTime with a delegate so it knows how to update a color.
        ColorHelper.LerpOverTime(c => lerpTarget = c, Colors.CornflowerBlue, Colors.HotPink, 10);
        // If your unfamilier with the syntax show above, you can also use a regular method ()
        ColorHelper.LerpOverTime(UpdateLerpTerget, Colors.CornflowerBlue, Colors.HotPink, 10);
        // If you want to be able to cancel a call to LerpOverTime, keep a reference to the IEnumerator
        // it returns to you. This can be used to cancel it.
        var handle = ColorHelper.LerpOverTime(UpdateLerpTerget, Colors.CornflowerBlue, Colors.HotPink, 10);
        ColorHelper.CancelLerpOverTime(handle);

        // We can also lerp an entire  list or array of colors over time
        Color[] from = new Color[5]; for (int i = 0; i < 5; i++) from[i] = Colors.White;
        Color[] to = new Color[5]; for (int i = 0; i < 5; i++) to[i] = ColorHelper.RandomBrightColor();
        LerpOverTimeTargets = new Color[5];
        ColorHelper.LerpOverTime(LerpOverTimeTargets, from, to, 10);

        // Finally, ColorTools supports Photoshop like colour blending
        while (true)
        {
            // You can set colours in BlendA/BlendB and change BlendStyle in the inspector to
            // see how the blend effects the resulting color.
            BlendResult = ColorHelper.Blend(BlendA, BlendB, BlendStyle);

            yield return null;
        }
	}
    void UpdateLerpTerget(Color c)
    {
        lerpTargetMethod = c;
    }

    void DemoRandomColors()
    {
        // Make sure the arrays are the correct size first!
        if (RandomBrightColors.Length != randomColorCount)
        {
            RandomBrightColors = new Color[randomColorCount];
            RandomPastelColors = new Color[randomColorCount];
            RandomDarkColors = new Color[randomColorCount];
        }

        // Fill the arrays with random colors.
        for (int i = 0; i < randomColorCount; i++)
        {
            RandomBrightColors[i] = ColorHelper.RandomBrightColor();
            RandomPastelColors[i] = ColorHelper.RandomPastelColor();
            RandomDarkColors[i] = ColorHelper.RandomDarkColor();
        }
    }
}
