using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used on planets and orbit anchors to apply a definable rotation speed
public class Orbit : MonoBehaviour
{
    public float spinSpeed = 1f;

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime, relativeTo:Space.Self); //rotate around Y axis in degrees per second
    }
}
