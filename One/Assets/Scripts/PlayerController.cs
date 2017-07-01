using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float initialOrbitDistance = 10f;
	public float arrivalDistance = 5f;
	public float timeLimit = 5;
	public float orbitDistance = 10f;

	void Awake() {
		Input.gyro.enabled = true;
		orbitDistance = initialOrbitDistance;
	}

	// Update is called once per frame
	void Update ()
	{
		var lerpRate = (initialOrbitDistance - arrivalDistance) / timeLimit;
		orbitDistance -= lerpRate * Time.deltaTime;
		if (orbitDistance < arrivalDistance / 2)
		{
			orbitDistance = initialOrbitDistance;
		}
		Quaternion deviceRotation = Quaternion.AngleAxis(90f, Vector3.right) * RightToLeftHandedRotation(Input.gyro.attitude);
		transform.position = deviceRotation * (Vector3.back * orbitDistance);
		transform.rotation = deviceRotation;
	}

	Quaternion RightToLeftHandedRotation(Quaternion q) {
		return new Quaternion(q.x, q.y, -q.z, -q.w);
	}
}
