using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Basic player controller script for 6DOF movement through the scene
public class PlayerController : MonoBehaviour
{
    //Movement speed variables (set in editor)
    public float baseThrustForce;
    public float shiftThrustForceMultiplier;

    public float baseTurnSpeed;
    public float shiftTurnSpeedMultiplier;
    
    //Boost Effects
    public AudioClip boostNoise;
    public ParticleSystem boostParticles;

    //Component references
    private Rigidbody rb;

    //Private variables
    private float thrustForce;
    private float turnSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); //Find rigidbody
        thrustForce = baseThrustForce; //Assign thrust
        turnSpeed = baseTurnSpeed; //Assign turn speed
    }

    private void Update()
    {
        MouseLook();
        adjustThrustForce();
    }

    private void FixedUpdate()
    {
        Thrust();
    }

    private void Thrust()
    {
        //WASD movement inputs, plus Q & E to roll
        //These inputs apply a physical force relative to the direction the player is facing
        //Slowing down is handled by the rigidbody's inbuilt drag values

        //Move forward
        if (Input.GetMouseButton(1)) rb.AddRelativeForce(Vector3.forward * thrustForce * shiftThrustForceMultiplier * Time.deltaTime); //Boost
        else if (Input.GetKey(KeyCode.W)) rb.AddRelativeForce(Vector3.forward * thrustForce * Time.deltaTime); //Regular

        //Move backward
        if (Input.GetKey(KeyCode.S)) rb.AddRelativeForce(-Vector3.forward * thrustForce * Time.deltaTime);

        //Move right
        if (Input.GetKey(KeyCode.D)) rb.AddRelativeForce(Vector3.right * thrustForce * Time.deltaTime);

        //Move left
        if (Input.GetKey(KeyCode.A)) rb.AddRelativeForce(-Vector3.right * thrustForce * Time.deltaTime);

        //Move up
        if (Input.GetKey(KeyCode.Space)) rb.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);

        //Move down
        if (Input.GetKey(KeyCode.LeftShift)) rb.AddRelativeForce(-Vector3.up * thrustForce * Time.deltaTime);

        //Roll left
        if (Input.GetKey(KeyCode.Q)) rb.AddRelativeTorque(Vector3.forward * turnSpeed * 0.5f * Time.deltaTime);

        //Roll right
        if (Input.GetKey(KeyCode.E)) rb.AddRelativeTorque(-Vector3.forward * turnSpeed * 0.5f * Time.deltaTime);
    }

    private void MouseLook()
    {
        //Mouse sensitivity is higher out of editor?????
        float mouseSensitivity = 1f;

        if (!Application.isEditor) mouseSensitivity = 0.5f;

        //Mouse directly rotates the player via transform, rather than physics, for better control
        transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) * mouseSensitivity * turnSpeed * Time.deltaTime);
    }

    private void adjustThrustForce()
    {
        //Turn boost effects on/off
        if (Input.GetMouseButtonDown(1) && Timer.timerIsRunning) { 
            GetComponent<AudioSource>().clip = boostNoise;
            GetComponent<AudioSource>().Play();
            turnSpeed = baseTurnSpeed * shiftTurnSpeedMultiplier;
            boostParticles.Play();
            Camera.main.GetComponent<CameraShake>().toggle();
        }
        if (Input.GetMouseButtonUp(1)) {
            if (GetComponent<AudioSource>().clip == boostNoise) GetComponent<AudioSource>().Stop();
            boostParticles.Stop();
            turnSpeed = baseTurnSpeed;
            Camera.main.GetComponent<CameraShake>().toggle();
        }
    }
}
