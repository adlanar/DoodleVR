using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class EnemyBehavior : MonoBehaviour {
    
    public int health = 100;

    void OnTriggerEnter(Collider other) {
        if (other.name == "Weapon") {
            health -= 50;
            if(health <= 0) {
                Destroy(gameObject);
            }
        }
    }

}
