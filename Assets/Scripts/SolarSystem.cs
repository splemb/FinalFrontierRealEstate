using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Triggers planets to be spawned, and calculates the number of them to create
public class SolarSystem : MonoBehaviour
{
    //Component references
    public GameObject sun;
    public GameObject planetBase;
    public GameObject orbitCentre;

    //Attributes
    private float sunScale;
    private int planetCount;

    public Material[] skyboxes;

    void Start()
    {
        Generate();
    }

    private void Generate()
    {
        //Sun can be randomly scaled to any float value between bounds, size affects number of planets that can spawn
        sunScale = Random.Range(0.5f, 3f);
        sun.transform.localScale *= sunScale;

        //Random skybox
        RenderSettings.skybox = skyboxes[Random.Range(0, skyboxes.Length)];

        //Randomly selects a number of planets to generate from a minimum of 4 to a maximum of 30
        //Number selected is based on the size of the sun and the number of levels completed this session
        planetCount = Mathf.Clamp(Mathf.RoundToInt(Random.Range(4,10) * sunScale),4,LevelTracker.completedLevels + 4);

        //For loop generates each planet
        for (int i = 0; i < planetCount; i++)
        {
            GameObject orbit = Instantiate(orbitCentre, this.gameObject.transform); //Instatiate the anchor object at the centre of the orbit
            
            Instantiate(planetBase,orbit.transform.position + (transform.TransformVector(Vector3.forward * ((i + 1) * 500f + 2000f))),Quaternion.identity,orbit.transform); //Instantiate the planet, anchor it to the orbit
            orbit.transform.Rotate(new Vector3(0f, Random.Range(0f, 360f), Random.Range(-7f, 7f)), Space.Self); //Randomly rotate the orbit to adjust the planet's position and tilt of the orbit

        }
    }

}
