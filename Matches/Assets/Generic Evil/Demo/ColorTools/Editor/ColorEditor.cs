using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

// This is an example extention to Unitys Color inspector that adds a small + box to colors in the inspector.
// Clicking this will expand a dropdown revealing some buttons that can be used to quickly tweak a colors 
// saturation and ligtness values.

[CustomPropertyDrawer(typeof(Color))]
public class ColorEditor : PropertyDrawer
{
    private static List<string> expandedCache = new List<string>();

    // Here you must define the height of your property drawer. Called by Unity.
    public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
    {
        if (IsExpanded(prop))
            return base.GetPropertyHeight(prop, label) + 16;
        else
            return base.GetPropertyHeight(prop, label);
    }

    // Here you can define the GUI for your property drawer. Called by Unity.
    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {
        EditorGUI.BeginChangeCheck();

        bool isExpanded = IsExpanded(prop);

        Rect colorRect = position;
        colorRect.width -= 20;
        colorRect.height = 16;
        var color = EditorGUI.ColorField(colorRect, label, prop.colorValue);

        Rect expandRect = position;
        expandRect.width = 20;
        expandRect.height = 16;
        expandRect.x += colorRect.width;
        if (GUI.Button(expandRect, isExpanded ? "-" : "+"))
        {
            isExpanded = ToggleExpand(prop);
        }

        if (isExpanded)
        {
            expandRect = position;
            expandRect.width = 80;
            expandRect.height = 16;
            expandRect.x += 80 * 0;
            expandRect.y += 16;
            if (GUI.Button(expandRect, "Saturate"))
            {
                color.Desaturate(0.2f);
            }

            expandRect = position;
            expandRect.width = 80;
            expandRect.height = 16;
            expandRect.x += 80 * 1;
            expandRect.y += 16;
            if (GUI.Button(expandRect, "Desaturate"))
            {
                color.Desaturate(0.2f);
            }

            expandRect = position;
            expandRect.width = 80;
            expandRect.height = 16;
            expandRect.x += 80 * 2;
            expandRect.y += 16;
            if (GUI.Button(expandRect, "Lighten"))
            {
                color = color.Lighten(0.2f);
            }

            expandRect = position;
            expandRect.width = 80;
            expandRect.height = 16;
            expandRect.x += 80 * 3;
            expandRect.y += 16;
            if (GUI.Button(expandRect, "Darken"))
            {
                color = color.Darken(0.2f);
            }
        }

        if (EditorGUI.EndChangeCheck())
            prop.colorValue = color;
    }


    void DrawTextField(Rect position, SerializedProperty prop, GUIContent label)
    {
        // Draw the text field control GUI.
        EditorGUI.BeginChangeCheck();
        string value = EditorGUI.TextField(position, label, prop.stringValue);
        if (EditorGUI.EndChangeCheck())
            prop.stringValue = value;
    }

    public bool ToggleExpand(SerializedProperty prop)
    {
        // is this property currently expanded?
        bool isExpanded = expandedCache.Contains(prop.propertyPath);

        if (isExpanded)
        {
            // close it down!
            expandedCache.Remove(prop.propertyPath);
            return false; // return for conviniance
        }
        else
        {
            // make sure the cache isnt tooo bloted
            if (expandedCache.Count > 50)
                expandedCache.RemoveAt(0);
            expandedCache.Add(prop.propertyPath);
            return true; // return for conviniance
        }
    }

    public bool IsExpanded(SerializedProperty prop)
    {
        return expandedCache.Contains(prop.propertyPath);
    }
}
