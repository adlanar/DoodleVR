using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorPathScript : MonoBehaviour {

    public Color rayColor = Color.white;
    //creates a list of paths
    public List<Transform> path_objs = new List<Transform>();
    Transform[] theArray;

    //draw gizomos
    void OnDrawGizmos() {
        Gizmos.color = rayColor;
        theArray = GetComponentsInChildren<Transform>();
        path_objs.Clear();

        //for all path locations in the array
        foreach (Transform path_obj in theArray) {
            //if the path not the same transform of the gameobject, add more path
            if(path_obj != this.transform) {
                path_objs.Add(path_obj);
            }
        }

        //store path position in each array
        for(int i = 0; i < path_objs.Count; i++) {
            Vector3 position = path_objs[i].position;
            if (i > 0) {
                Vector3 previous = path_objs[i - 1].position;
                Gizmos.DrawLine(previous, position);
                Gizmos.DrawWireSphere(position,0.05f);
            }
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
