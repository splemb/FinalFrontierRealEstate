using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script that interfaces with the planet class to randomly generate new planet objects at runtime
public class CreatePlanet : MonoBehaviour
{
    //Reference to class, which stores given attributes for a generated planet
    public Planet planetAttributes;

    //Array of colours that can be assigned
    /* 0: Red
     * 1: Brown
     * 2: Yellow
     * 3: Green
     * 4: Blue
     * 5: Purple
     */
    public Color[] colorArray = new Color[] {
        Color.red,              //Red
        new Color(1,0.235f,0),  //Brown
        Color.yellow,           //Yellow
        Color.green,            //Green
        Color.blue,             //Blue
        new Color(1,0,1)        //Purple
    };
    public string[] colorNameArray = new string[] { "Red", "Brown", "Yellow", "Green", "Blue", "Purple" };

    //Array of values used as scale factors
    /* 0: Small
     * 1: Normal
     * 2: Large
     * 3: Very Large
     */
    public float[] sizeArray = new float[] { 1, 2, 3, 4 };

    //Array of materials for the patterns of the planets (assigned in editor)
    /* 0: Plain
     * 1: Spotty
     * 2: Stripy
     * 3: Starry
     * 4: Checkered
     */
    public Material[] materialArray = new Material[5];
    public string[] materialNameArray = new string[] { "Plain", "Spotty", "Stripy", "Starry", "Checkered"};

    //Array of meshes for the shape of the planets (assigned in editor)
    /* 0: Smooth
     * 1: Mountainous
     * 2: Craters
     */
    public Mesh[] meshArray = new Mesh[3];
    public string[] meshNameArray = new string[] { "Smooth", "Mountainous", "Craters"};

    void Start()
    {
        GenerateAttributes();
        ApplyAttributes();
    }

    //Randomly chooses a value for each attribute from the above arrays and constructs a new planet object
    void GenerateAttributes()
    {
        //Get a random value for each attribute
        int colorIndex = Random.Range(0, 6);
        int materialIndex = Random.Range(0, 5);
        int meshIndex = Random.Range(0, 3);

        //Pass attributes to a new planet class
        planetAttributes = new Planet(
            colorArray[colorIndex], 
            materialArray[materialIndex], 
            meshArray[meshIndex], 
            sizeArray[Random.Range(0, 4)],
            colorNameArray[colorIndex],
            materialNameArray[materialIndex],
            meshNameArray[meshIndex]);
    }

    //Gets the values from the planet object and applies them to the components of the visible planet model
    void ApplyAttributes()
    {
        GetComponentInChildren<MeshFilter>().mesh = planetAttributes.GetLandscape();
        GetComponentInChildren<MeshRenderer>().material = planetAttributes.GetPattern();
        GetComponentInChildren<MeshRenderer>().material.color = planetAttributes.GetColor();
        GetComponentInChildren<Transform>().localScale *= planetAttributes.GetSizeModifier();
        GetComponentInChildren<MeshCollider>().sharedMesh = planetAttributes.GetLandscape();

        //Each planet has a trail the same colour as itself
        TrailRenderer trail = GetComponent<TrailRenderer>();

        trail.startColor = planetAttributes.GetColor();
        trail.endColor = new Color(1, 1, 1, 0.2f);
        trail.startWidth = planetAttributes.GetSizeModifier() * 10;
        trail.endWidth = 0;
        
    }

}
