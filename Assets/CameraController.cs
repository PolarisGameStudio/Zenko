using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	static public Vector3 cameraorigin;
	public static Vector3 cameraposition;
	public static Vector3 eulerangles;
	// Use this for initialization
	void Awake () {
		cameraposition = transform.localPosition;
		eulerangles = transform.localEulerAngles;
		cameraorigin = new Vector3(Camera.main.gameObject.transform.position.x, Camera.main.gameObject.transform.position.y, Camera.main.gameObject.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	static public void changePosition(int shiftx, int shifty){
		Camera.main.gameObject.transform.position = new Vector3(cameraorigin.x+shiftx, cameraorigin.y, cameraorigin.z-shifty);
	}
	static public void changeFovAndRot(int fov, float rot){

		Camera.main.fieldOfView = fov;
		Camera.main.gameObject.transform.rotation = Quaternion.Euler(rot,0,0);
		cameraposition = Camera.main.transform.localPosition;
		eulerangles = Camera.main.transform.localEulerAngles;
	}

}
	