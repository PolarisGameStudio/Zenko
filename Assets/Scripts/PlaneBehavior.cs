using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlaneBehavior : MonoBehaviour {
	public static PlaneBehavior Instance;
	public GameObject camera;
	static Camera cam;
	static Plane plane;
	Vector3 m0;
	public static Vector3 planePos;
	public static int tilex;
	public static int tiley;
	public static bool readyToDrop;
	public static float offsetup;
	public static Vector2 curpos;
	public static Vector2 previouspos;
	public static bool hashad; //checks if previouspos is not empty.
	public static Vector2 quantizedposition;
	public static int currentx;
	public static int currenty;
	public static List<Vector3> hoverCandidates = new List<Vector3>();
	public static GameObject highlightedTile;
	public static Vector3 simulatedMouse;

	void Awake(){
		Instance = this;
	}
	void Start () {
		offsetup = .8f;
		cam = GameObject.Find ("Main Camera").GetComponent<Camera>();
		plane = new Plane(Vector3.up, transform.position);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!PieceHolders.hinting){
			if(LevelManager.isdragging){
				PlaneRay();
			}
		}
		else{
		RoboRay(simulatedMouse);
		}
	}
	
	public static void RoboRay(Vector3 target){
		Vector3 origin = cam.transform.position;
		Ray ray = new Ray(origin, target-origin);
		float rayDistance = 100;
		if(plane.Raycast(ray,out rayDistance)){
			planePos = ray.GetPoint(rayDistance);
			planePos = planePos + Vector3.up*offsetup;
			curpos = new Vector2(Mathf.RoundToInt(planePos.x), -Mathf.RoundToInt(planePos.z));
		}
	}

	public void PlaneRay(){
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		float rayDistance = 100;
		if(plane.Raycast(ray,out rayDistance)){
			planePos = ray.GetPoint(rayDistance);
			planePos = planePos + Vector3.up*offsetup;
			curpos = new Vector2(Mathf.RoundToInt(planePos.x), -Mathf.RoundToInt(planePos.z));
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

	public Vector3 forcePlanePos(){
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		float rayDistance = 100;
		if(plane.Raycast(ray,out rayDistance)){
			planePos = ray.GetPoint(rayDistance);
			planePos = planePos + Vector3.up*offsetup;
			return planePos;
		}
		else return new Vector3(0,0,0);
	}

	public void ClosestX(){
		bool isleft = true;
		if(PlaneBehavior.planePos.x-Mathf.RoundToInt(PlaneBehavior.planePos.x)>0){
			isleft = false;
		}
		Debug.Log(isleft + "isleft");
	}

	public void ClosestY(){

	}

	public static void ClosestTile(){
		hoverCandidates.Clear();
		currentx = Mathf.RoundToInt(planePos.x);
		currenty = -Mathf.RoundToInt(planePos.z);
		if(currentx < 0 ){
			currentx= 0;
		}
		if(currenty < 0){
			currenty = 0;
		}
		if(currentx > LevelBuilder.totaldimension-1){
			currentx = LevelBuilder.totaldimension-1;
		}
		if(currenty > LevelBuilder.totaldimension-1){
			currenty = LevelBuilder.totaldimension-1;
		}
		int i = 1;
		while(hoverCandidates.Count ==0){
			CycleThroughSurrounding(i);
			SortCandidates();
			i++;
		}
		FindGameObject(hoverCandidates[0].x, -hoverCandidates[0].y);
	}

	public static void CycleThroughSurrounding(int degree){
		for(int i =1; i-1<degree; i++){
			StoreIfAvailable(0, -i);
			StoreIfAvailable(0, i);
			StoreIfAvailable(-i, 0);
			StoreIfAvailable(i, 0);

			StoreIfAvailable(i, i);
			StoreIfAvailable(i, -i);
			StoreIfAvailable(-i, -i);
			StoreIfAvailable(-i, i);

			//fills in the spaces between cardinal directions and diagonal.
			for(int j = 1; j < i; j++){
				if(j!= i){
					StoreIfAvailable(j, -i);
					StoreIfAvailable(j, i);
					StoreIfAvailable(-j, i);
					StoreIfAvailable(-j, -i);

					StoreIfAvailable(i, j);
					StoreIfAvailable(i, -j);
					StoreIfAvailable(-i, -j);
					StoreIfAvailable(-i, j);			
				}
			}
		}
	}

	public static void StoreIfAvailable(int xtocheck, int ytocheck){
		int checkingx = currentx+xtocheck;
		int checkingy = currenty + ytocheck;
		if(checkingx < 0 || checkingx >= LevelBuilder.totaldimension){
			return;
		}
		if(checkingy < 0 || checkingy >= LevelBuilder.totaldimension){
			return;
		}
		if(LevelBuilder.tiles[checkingx, checkingy].isTaken == false){
			MeasureAndStore(checkingx,checkingy);
		}
		else{
		}
	}

	public static void MeasureAndStore(int xpoint, int ypoint){
		Vector2 origin = new Vector2(planePos.x, -planePos.z);
		Vector2 target = new Vector2(xpoint,ypoint);
		float distance = Vector2.Distance(origin, target);
		Vector3 candidate = new Vector3(xpoint,ypoint,distance);
		hoverCandidates.Add(candidate);
	}

	public static void SortCandidates(){
		hoverCandidates = hoverCandidates.OrderBy(x => x.z).ToList();
	}

	public static void FindGameObject(float positionx, float positiony){
		Collider[] colliders = Physics.OverlapSphere(new Vector3((int)positionx,0,positiony), .5f);	
		foreach (Collider component in colliders) {
			if(component.tag == "Tile"){
				highlightedTile = component.gameObject;			
			}
		}
	}
}
