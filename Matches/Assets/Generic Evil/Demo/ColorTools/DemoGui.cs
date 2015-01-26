using UnityEngine;
using System.Collections;

public class DemoGui : MonoBehaviour 
{
    public ColorHSL color;
    private Texture2D previewTexture; 
    private GUIStyle previewStyle;
    private float yPos;
	// Use this for initialization
	void Start () {
        color = ColorHelper.RandomBrightColor();

        // Make a tiny 1x1 texture, were going to use this to preview the color we have picked
        previewTexture = new Texture2D(1, 1);
        previewStyle = new GUIStyle();
        previewStyle.normal.background = previewTexture;

        StartCoroutine(AnimateWindow());
	}

    public IEnumerator AnimateWindow()
    {
        float timer = 0;
        float time = 2;
        while (timer <= time)
        {
            timer += Time.deltaTime;
            yPos = Mathf.Lerp(Screen.height + 100, (Screen.height / 2) - (height / 2), timer / time);
            yield return null;
        }
    }

    int width = 500;
    int height = 270;
    void OnGUI()
    {
        // Draw a centered window
        GUI.Window(0, new Rect((Screen.width/2f)-(width/2f), yPos, width, height), DrawWindow, "ColorTools for Unity");
    }

    private void DrawWindow(int windowID)
    {
        // Layout three sliders as a simple color picker
        GUILayout.BeginHorizontal();
        GUILayout.Label("Hue", GUILayout.MaxWidth(70));
        color.h = GUILayout.HorizontalSlider(color.h, 0, 1);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Saturation", GUILayout.MaxWidth(70));
        color.s = GUILayout.HorizontalSlider(color.s, 0, 1);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Lightness", GUILayout.MaxWidth(70));
        color.l = GUILayout.HorizontalSlider(color.l, 0, 1);
        GUILayout.EndHorizontal();

        // Draw a nice big box showing off the current color
        DrawColorPreview(color);

        // Finally, add some buttons to test various features 
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Random Bright Color"))
        {
            color = ColorHelper.RandomBrightColor();
        }
        if (GUILayout.Button("Random Pastel Color"))
        {
            color = ColorHelper.RandomPastelColor();
        }
        if (GUILayout.Button("Random Dark Color"))
        {
            color = ColorHelper.RandomDarkColor();
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Invert Hue"))
        {
            color.InvertHue();
        }
        if (GUILayout.Button("Invert Saturation"))
        {
            color.InvertSaturation();
        }
        if (GUILayout.Button("Invert Lightness"))
        {
            color.InvertLightness();
        }
        if (GUILayout.Button("Invert"))
        {
            color.Invert();
        }
        GUILayout.EndHorizontal();
    }

    private void DrawColorPreview(Color color)
    {
        previewTexture.SetPixel(0, 0, color);
        previewTexture.Apply();
        previewStyle.normal.textColor = color.SelectContrasting(Colors.White, Colors.Black); // Use select contrasting to make sure we can always read this text
        GUILayout.Box(((ColorHSL)color).ToString() + "\n\n\n\n\n\n\n", previewStyle);
    }
}
