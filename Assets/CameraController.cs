using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	static public Vector3 cameraorigin;
	// Use this for initialization
	void Awake () {
		cameraorigin = new Vector3(Camera.main.gameObject.transform.position.x, Camera.main.gameObject.transform.position.y, Camera.main.gameObject.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	static public void changePosition(int shiftx, int shifty){
		Camera.main.gameObject.transform.position = new Vector3(cameraorigin.x+shiftx, cameraorigin.y, cameraorigin.z-shifty);
	}
}
