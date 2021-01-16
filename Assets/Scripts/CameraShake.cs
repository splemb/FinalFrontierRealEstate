using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;

	Vector3 originalPos;
	bool isShaking;

	void OnEnable()
	{
		originalPos = transform.localPosition;
	}

	void Update()
	{
		if (isShaking) transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
		else transform.localPosition = originalPos;
	}

	public void toggle()
    {
		switch (isShaking)
        {
			case true:
				isShaking = false;
				break;
			case false:
				isShaking = true;
				break;
        }
    }
}