using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakePicture : MonoBehaviour
{

    //Variables
    List<GameObject> planets = new List<GameObject>();
    Planet selectedPlanet;
    Planet checkedPlanet;
    bool canTakePicture = true;

    //Public attributes
    public AudioClip cameraSound;
    [SerializeField] LayerMask layerMask;

    //Components
    public TMPro.TextMeshProUGUI colorText;
    public TMPro.TextMeshProUGUI landscapeText;
    public TMPro.TextMeshProUGUI patternText;

    void Start()
    {
        //Find and initialise HUD text boxes
        colorText = GameObject.FindGameObjectWithTag("Color Text").GetComponent<TMPro.TextMeshProUGUI>();
        landscapeText = GameObject.FindGameObjectWithTag("Landscape Text").GetComponent<TMPro.TextMeshProUGUI>();
        patternText = GameObject.FindGameObjectWithTag("Pattern Text").GetComponent<TMPro.TextMeshProUGUI>();

        colorText.color = Color.white;
        landscapeText.color = Color.white;
        patternText.color = Color.white;
    }

    void Update()
    {
        //If there's no planets in the list, keep checking until there are
        if (planets.Count == 0) while (GetPlanets()) { ; }

        //Select a planet if one isn't selected
        if (selectedPlanet == null) SelectPlanet();

        //Take a picture if the timer is running, the canTakePicture flag is true and the mouse is pressed
        if (Input.GetMouseButtonDown(0) && canTakePicture && Timer.timerIsRunning) TakeAPicture();

        //DebugScanPlanet();
    }

    //Get a list of the generated planets from their tag
    bool GetPlanets()
    {
        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Planet")) planets.Add(p);
        return (planets.Count == 0); //Return true once planets are found
    }

    //Choose a random planet to designate as the target
    void SelectPlanet()
    {
        int index = Random.Range(0, planets.Count); //Randomly select from list
        selectedPlanet = planets[index].GetComponent<CreatePlanet>().planetAttributes; //Get that planet's attributes
        OutputToHUD(); //Send that info to the HUD
    }

    //The picture taking routine
    void TakeAPicture()
    {
        //Set flags and initialise variables
        int addedScore = 0;
        canTakePicture = false;
        RaycastHit hit;

        //Play the camera shutter sound
        GetComponent<AudioSource>().clip = cameraSound;
        GetComponent<AudioSource>().Play();

        //A box cast is sent out from the camera to check for planets in view. The closest planet found has its attributes compared to the target planet's
        if (Physics.BoxCast(Camera.main.transform.position, new Vector3(400, 225, 1), Camera.main.transform.forward, out hit, Quaternion.identity, 3000f, layerMask) ||
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5000f, layerMask))
        {
            GameObject planetObject = hit.collider.gameObject;
            checkedPlanet = planetObject.GetComponentInParent<CreatePlanet>().planetAttributes;

            //Each matching attribute gives the player a point, for up to 3 points for an exact match
            //Matching attributes turn green on the HUD, while differing attributes turn red
            if (checkedPlanet.GetColor() == selectedPlanet.GetColor())
            {
                addedScore++;
                colorText.color = Color.green;
            } else colorText.color = Color.red;

            if (checkedPlanet.GetPattern() == selectedPlanet.GetPattern())
            {
                addedScore++;
                patternText.color = Color.green;
            }
            else patternText.color = Color.red;

            if (checkedPlanet.GetLandscape() == selectedPlanet.GetLandscape())
            {
                addedScore++;
                landscapeText.color = Color.green;
            }
            else landscapeText.color = Color.red;
        }
        else
        {
            //If no planet is found by the boxcast, award no points and declare no matching attributes
            colorText.color = Color.red;
            patternText.color = Color.red;
            landscapeText.color = Color.red;
        }

        //Add score to global score total
        Score.AddScore(addedScore);

        //Start end of level routine
        StartCoroutine(GameManager.CompleteLevel(1));
    }

    //Output target planet attributes to the HUD
    void OutputToHUD()
    {
        colorText.text = selectedPlanet.GetColorName();
        landscapeText.text = selectedPlanet.GetLandscapeName();
        patternText.text = selectedPlanet.GetPatternName();
    }

    //Debug function, outputs attributes of planet being looked at to the debug log
    void DebugScanPlanet()
    {
        RaycastHit hit;
        if (Physics.BoxCast(Camera.main.transform.position, new Vector3(400, 225, 1), Camera.main.transform.forward, out hit, Quaternion.identity, 3000f, layerMask))
        {
            checkedPlanet = hit.transform.gameObject.GetComponentInParent<CreatePlanet>().planetAttributes;
            colorText.text = (checkedPlanet.GetColorName() + ", " + checkedPlanet.GetLandscapeName() + ", " + checkedPlanet.GetPatternName());
        } 
        else colorText.text = ("None");
    }
}
