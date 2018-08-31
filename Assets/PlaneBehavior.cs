using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBehavior : MonoBehaviour {
	public GameObject camera;
	Camera cam;
	Plane plane;
	Vector3 m0;
	public static Vector3 planePos;
	public static int tilex;
	public static int tiley;
	public static bool readyToDrop;
	// Use this for initialization
	void Start () {
		cam = camera.GetComponent<Camera>();
//		plane = this.parent;
		plane = new Plane(Vector3.up, transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		PlaneRay();
	}
	void PlaneRay(){

		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		float rayDistance = 100;
		if(plane.Raycast(ray,out rayDistance)){
//		Debug.Log(ray.GetPoint(rayDistance));
		planePos = ray.GetPoint(rayDistance);
	}
      //  Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
	}
	Ray GenerateMouseRay(){
		Vector3 mousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
		Vector3 mousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
		Vector3 mousePosF = Camera.main.ScreenToWorldPoint(mousePosFar);
		Vector3 mousePosN = Camera.main.ScreenToWorldPoint(mousePosNear);
		Ray mr = new Ray(mousePosN, mousePosF-mousePosN);
		return mr;
			}

}
