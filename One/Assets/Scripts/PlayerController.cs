using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float orbitDistance = 1.0f;

	void Awake() {
		Input.gyro.enabled = true;
	}

	// Update is called once per frame
	void Update () {
		Quaternion deviceRotation = Quaternion.AngleAxis(90f, Vector3.right) * RightToLeftHandedRotation(Input.gyro.attitude);
		transform.position = deviceRotation * (Vector3.back * orbitDistance);
		transform.rotation = deviceRotation;
	}

	Quaternion RightToLeftHandedRotation(Quaternion q) {
		return new Quaternion(q.x, q.y, -q.z, -q.w);
	}
}
