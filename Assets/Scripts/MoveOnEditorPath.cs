using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnEditorPath : MonoBehaviour {

    public EditorPathScript PathToFollow;
    public bool isLoop;

    public int CurrentWayPointID = 0;
    public float speed;
    private float reachDistance = 1.0f;
    public float rotationSpeed = 5.0f;
    
    // Update is called once per frame
    void Update() {
        if (PathToFollow == null) {
            transform.Translate(transform.right * speed * Time.deltaTime);
            return;
        }

        //get distance from one point to another
        //move towards another path
        float distance = Vector3.Distance(PathToFollow.path_objs[CurrentWayPointID].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position,
            PathToFollow.path_objs[CurrentWayPointID].position,
            Time.deltaTime * speed);

        var rotation = Quaternion.LookRotation(PathToFollow.path_objs[CurrentWayPointID].position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        //if distance is too far, find closest path
        if (distance <= reachDistance) {
            CurrentWayPointID++;
        }

        //if path is finished, destroy path
        if (CurrentWayPointID >= PathToFollow.path_objs.Count) {
            if (isLoop) {
                CurrentWayPointID = 0;
            }
            else {
                Destroy(PathToFollow);
                speed = 0;
            }
        }
        
    }

}
