using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ConfettiGun : MonoBehaviour {

    public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
        //Setup controller event listeners
        GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoTriggerPressed);
    }

    // Update is called once per frame
    private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e) {
        GameObject obj = Instantiate<GameObject>(bulletPrefab);
        obj.transform.parent = this.transform;
        obj.transform.localScale = Vector3.one;
        obj.SetActive(true);
        obj.GetComponent<ParticleSystem>().Play();
        Destroy(obj, 3);
    }
}
