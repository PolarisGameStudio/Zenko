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
	public static float offsetup;
	public static Vector2 curpos;
	public static Vector2 previouspos;
	public static bool hashad; //checks if previouspos is not empty.
	// Use this for initialization
	void Start () {
		offsetup = .8f;
		
		cam = GameObject.Find ("Main Camera").GetComponent<Camera>();
//		plane = this.parent;
		plane = new Plane(Vector3.up, transform.position);
		Debug.Log(previouspos);
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
			planePos = planePos + Vector3.up*offsetup;
//			Debug.Log(planePos);
			curpos = new Vector2(Mathf.RoundToInt(planePos.x), -Mathf.RoundToInt(planePos.z));
			/*if(planePos.x<7.4 && planePos.x>-0.5 && planePos.z>-7.4 && planePos.z<.5){
				LevelBuilder.tiles[(int)curpos.x,(int)curpos.y].tileObj.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
				Debug.Log(curpos.x +"" + curpos.y);
				if(hashad){
					if((int)curpos.x != (int) previouspos.x || (int)curpos.y != curpos.y ){
						Debug.Log(curpos + "" + previouspos);
						LevelBuilder.tiles[Mathf.RoundToInt(previouspos.x), Mathf.RoundToInt(previouspos.y)].tileObj.GetComponentInChildren<MeshRenderer>().
						material.color = Color.white;
						//Debug.Log("Inboard");
					}
				}
				previouspos =curpos;
				hashad = true;
			}*/
			//Debug.Log("Giving prev"); //not getting here when previouspos doesnt exist. stops at l48.	

			//previouspos = curpos;
			//hashad = true;
			//float diff = Vector2.Distance(previouspos, curpos);
			//if(diff != 0){
			//	LevelBuilder.tiles[(int)previouspos.x, (int)previouspos.x].tileObj.GetComponentInChildren<MeshRenderer>().
				//material.color = Color.white;
			//}


		}
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
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
