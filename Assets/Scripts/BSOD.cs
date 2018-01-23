using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BSOD : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Reset", 10f);
	}

    void Reset() {
        SceneManager.LoadScene("Main");
    }
}
