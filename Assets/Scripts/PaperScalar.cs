using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PaperScalar : MonoBehaviour {

    public float scalingSpeed = 1.25f;

    float centerX = 1.0f;
    float intensity = 1;
    bool scaling = false;

    ControllerInteractionEventHandler touchStart;
    ControllerInteractionEventHandler touchChanged;
    ControllerInteractionEventHandler touchEnd;

    // Use this for initialization
    void Start () {
        touchStart = new ControllerInteractionEventHandler(DoTouchpadTouchStart);
        touchChanged = new ControllerInteractionEventHandler(DoTouchpadAxisChanged);
        touchEnd = new ControllerInteractionEventHandler(DoTouchpadTouchEnd);
    }
	
	// Update is called once per frame
	void Update () {
        if (scaling && transform.localScale.magnitude < Vector3.one.magnitude * 8 && transform.localScale.magnitude > Vector3.one.magnitude * 0.5) {
            transform.localScale += intensity * transform.localScale * Time.deltaTime * scalingSpeed;
        }
	}

    void OnTriggerEnter(Collider other) {
        if (other.tag == "controller")
        {
            // add touchpad listeners to L controller
            other.GetComponent<VRTK_ControllerEvents>().TouchpadTouchStart += touchStart;
            other.GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged += touchChanged;
            other.GetComponent<VRTK_ControllerEvents>().TouchpadTouchEnd += touchEnd;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.tag == "controller") {
            // remove touchpad listeners to L controller
            other.GetComponent<VRTK_ControllerEvents>().TouchpadTouchStart -= touchStart;
            other.GetComponent<VRTK_ControllerEvents>().TouchpadAxisChanged -= touchChanged;
            other.GetComponent<VRTK_ControllerEvents>().TouchpadTouchEnd -= touchEnd;
            intensity = 0;
            scaling = false;
        }
    }


    private void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e) {
        intensity = e.touchpadAxis.x;
        /*if (e.touchpadAxis.x > centerX) {
            intensity *= 1.05f;
        }
        else {
            intensity *= 0.95f;
        }*/
    }

    private void DoTouchpadTouchStart(object sender, ControllerInteractionEventArgs e)  {
        //end the creation of the object
        centerX = e.touchpadAxis.x;
        scaling = true;
    }

    private void DoTouchpadTouchEnd(object sender, ControllerInteractionEventArgs e) {
        //end the creation of the object
        centerX = 1.0f;
        scaling = false;
        intensity = 0;
    }
}
