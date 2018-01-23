using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Draw : MonoBehaviour {

	public enum BodyType {
		Line = 0,
		Plane = 1, 
		Ellipse = 2,
		Free = 3
	};

    public GameObject paperPrefab;
    public Transform fingertip;
    public GameObject hand;
    public GameObject pencil;

    private BodyType bodyType = BodyType.Line;
	private bool updating = false;

    List<Vector3> vertPositions;

    //For free draw
    public Material lMat;
    private MeshLineRenderer currLine;

    Stack<GameObjectPos> objects = new Stack<GameObjectPos>();

	private void Start()
	{
        vertPositions = new List<Vector3>();

		if (GetComponent<VRTK_ControllerEvents>() == null)
		{
			Debug.LogError("VRTK_ControllerEvents_ListenerExample is required to be attached to a SteamVR Controller that has the VRTK_ControllerEvents script attached to it");
			return;
		}

		//Setup controller event listeners
		GetComponent<VRTK_ControllerEvents>().TriggerPressed += new ControllerInteractionEventHandler(DoTriggerPressed);
		GetComponent<VRTK_ControllerEvents> ().TriggerReleased += new ControllerInteractionEventHandler(DoTriggerReleased);
	}

	private void Update() {
		if (updating) {
            GameObjectPos currentObj = objects.Peek();
            switch (bodyType) {
			case BodyType.Line:
                currentObj.obj.transform.position = (currentObj.startPos + fingertip.transform.position) / 2.0f;
				currentObj.obj.transform.LookAt(fingertip.transform.position);
				currentObj.obj.transform.localScale = new Vector3 (0.1f, 0.1f, Vector3.Distance (currentObj.startPos, fingertip.transform.position));
				break;
			case BodyType.Ellipse:
                currentObj.obj.transform.position = (currentObj.startPos + fingertip.transform.position) / 2.0f;
                currentObj.obj.transform.LookAt(fingertip.transform.position);
                currentObj.obj.transform.localScale = new Vector3(0.1f, 0.1f, Vector3.Distance(currentObj.startPos, fingertip.transform.position));
                break;
			case BodyType.Free:
                if(Vector3.Distance(vertPositions[vertPositions.Count - 1], fingertip.transform.position) > 0.03) {
                    currLine.AddPoint(fingertip.transform.position);
                    vertPositions.Add(fingertip.transform.position);
                    currLine.GetComponent<MeshCollider>().sharedMesh = currLine.ml;
                }               
				break;
			default:
				break;
			}

		}
	}

	private void DoTriggerPressed(object sender, ControllerInteractionEventArgs e) {
        updating = true;

        GameObject obj;
        GameObjectPos objpos;
        switch (bodyType) {
		case BodyType.Line:
            Debug.Log(0);
            hand.SetActive(false);
            pencil.SetActive(true);
            obj = GameObject.CreatePrimitive (PrimitiveType.Cube);
            obj.name = "Weapon";
            obj.AddComponent<BoxCollider>();
            obj.AddComponent<VRTK_InteractableObject>();
            obj.GetComponent<VRTK_InteractableObject>().precisionSnap = true;
            obj.GetComponent<VRTK_InteractableObject>().isGrabbable = true;
            obj.GetComponent<VRTK_InteractableObject>().grabAttachMechanic = VRTK_InteractableObject.GrabAttachType.Child_Of_Controller;
            obj.AddComponent<Rigidbody>();
            obj.GetComponent<Rigidbody>().isKinematic = false;
            obj.transform.position = fingertip.position;
            objpos.obj = obj;
            objpos.startPos = obj.transform.position;
            objects.Push(objpos);
            break;
		case BodyType.Plane:
            Debug.Log(2);
			obj = Instantiate<GameObject>(paperPrefab);
            obj.name = "Weapon";
            obj.transform.localScale = new Vector3(0.085f, 1, 0.110f);
            obj.transform.parent = this.transform;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localPosition = new Vector3(0, 0, 1);
            obj.transform.parent = null;
            obj.transform.Rotate(new Vector3(90, 0, 0));
            obj.SetActive(true);
            updating = false;
            return;
		case BodyType.Ellipse:
            Debug.Log(3);
            obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            break;
		case BodyType.Free:
            Debug.Log(4);
            hand.SetActive(false);
            pencil.SetActive(true);
            obj = new GameObject();
            obj.name = "Weapon";
            obj.transform.position = fingertip.position;
            obj.AddComponent<MeshFilter>();
            obj.AddComponent<MeshRenderer>();
            obj.GetComponent<MeshRenderer>().material.color = Color.black;
            obj.AddComponent<Rigidbody>();
            obj.GetComponent<Rigidbody>().isKinematic = true;
            obj.GetComponent<Rigidbody>().useGravity = false;


            currLine = obj.AddComponent<MeshLineRenderer>();
            currLine.lmat = lMat;
            currLine.setWidth(0.035f);
            vertPositions.Add(fingertip.transform.position);
            obj.AddComponent<MeshCollider>().convex = true;
            obj.GetComponent<MeshCollider>().sharedMesh = currLine.ml;

            obj.AddComponent<VRTK_InteractableObject>();
            obj.GetComponent<VRTK_InteractableObject>().precisionSnap = true;
            obj.GetComponent<VRTK_InteractableObject>().isGrabbable = true;
            obj.GetComponent<VRTK_InteractableObject>().grabAttachMechanic = VRTK_InteractableObject.GrabAttachType.Child_Of_Controller;

            objpos.obj = obj;
            objpos.startPos = obj.transform.position;
            objects.Push(objpos);

            break;
		default:
            Debug.Log(5);
            obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            break;
		}

	}

	private void DoTriggerReleased(object sender, ControllerInteractionEventArgs e) {
		//end the creation of the object
		updating = false;

        hand.SetActive(true);
        pencil.SetActive(false);

        currLine.GetComponent<Rigidbody>().isKinematic = false;
        currLine.GetComponent<Rigidbody>().useGravity = true;
    }

	protected struct GameObjectPos
	{
		public GameObject obj;
		public Vector3 startPos;
	}

	public void setShape(int type) {
		this.bodyType = (BodyType)type;
	}

}


