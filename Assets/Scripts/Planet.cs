using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Planet class, containing attributes for the appearances of planets
public class Planet : ScriptableObject
{
    //Private Attributes
    private Color color;
    private Material pattern;
    private float sizeModifier;
    private Mesh landscape;

    //Names of attributes, used for HUD
    private string colorName;
    private string patternName;
    private string landscapeName;

    //Constructor
    public Planet(Color inputColor, Material inputPattern, Mesh inputLandscape, float inputSizeModifier, string inputColorName, string inputPatternName, string inputLandscapeName)
    {
        //Passed in values are assigned to attribute variables in the class
        color = inputColor;
        pattern = inputPattern;
        landscape = inputLandscape;
        sizeModifier = inputSizeModifier;

        //Names of attributes, used for HUD
        colorName = inputColorName;
        patternName = inputPatternName;
        landscapeName = inputLandscapeName;
    }

    //Public getters for each attribute
    public Color GetColor() { return color; }
    public Material GetPattern() { return pattern; }
    public float GetSizeModifier() { return sizeModifier; }
    public Mesh GetLandscape() { return landscape; }
    public string GetColorName() { return colorName; }
    public string GetPatternName() { return patternName; }
    public string GetLandscapeName() { return landscapeName; }
}
