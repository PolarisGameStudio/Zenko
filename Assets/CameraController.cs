using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MirzaBeig.ParticleSystems.Demos;

public class CameraController : MonoBehaviour {
	static public Vector3 cameraorigin;
	public static Vector3 cameraposition;
	public static Vector3 eulerangles;
	public GameObject topDownCam;
	public CameraShake shaker;
	bool topdown;
	// Use this for initialization
	void Awake () {
		topdown = false;
		cameraposition = transform.localPosition;
		eulerangles = transform.localEulerAngles;
		cameraorigin = new Vector3(Camera.main.gameObject.transform.position.x, Camera.main.gameObject.transform.position.y, Camera.main.gameObject.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.T)){
			//DrawNextLevel(LevelManager.levelnum);
			//Debug.Log(tileBank.Count);
			if(topdown){
				transform.position = cameraposition;
				transform.eulerAngles = eulerangles;
				gameObject.GetComponent<Camera>().fieldOfView = 27;	
				shaker.enabled = true;					
				topdown = false;	
			}
			else{
				shaker.enabled = false;
				transform.position = topDownCam.transform.position;
				transform.eulerAngles = topDownCam.transform.eulerAngles;
				gameObject.GetComponent<Camera>().fieldOfView = 60;		
				topdown = true;		
			}


		}
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
	