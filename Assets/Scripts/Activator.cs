using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour {

    public GameObject enableThis;

    void OnTriggerEnter(Collider other) {
        if(other.name == "Player") {
            enableThis.SetActive(true);
        }
    }
}
