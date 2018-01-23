using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLineManager : MonoBehaviour {

   

    public SteamVR_TrackedObject trackedObj;

    public Material lMat;
    private MeshLineRenderer currLine;

    private int numClicks = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
        //if trigger pressed, create line
        if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
            GameObject go = new GameObject();
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();
            
            currLine = go.AddComponent<MeshLineRenderer>();
            currLine.lmat = lMat;
            currLine.setWidth(0.1f);
            numClicks = 0;
            
        //if trigger pressed until click, set global position
        } else if(device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            //currLine.SetVertexCount(numClicks + 1);
            //currLine.SetPosition(numClicks, trackedObj.transform.position);
            currLine.AddPoint(trackedObj.transform.position);
            numClicks++;
        }
	}
}
