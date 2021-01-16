using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    void Update()
    {
        //Compass arrow always points towards origin
        transform.LookAt(Vector3.zero - Camera.main.transform.position);
    }
}
