using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(this);
	}

	public void StartGame() {
		SceneManager.LoadScene("Main");
	}

	public void EndGame() {
		SceneManager.LoadScene("Main");
	}
}
